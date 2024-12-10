using BetPlacer.Scrapper.Worker.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Globalization;

namespace BetPlacer.Scrapper.Worker.Services
{
    public class ScrapperService
    {
        private IWebDriver _driver;
        private string _url;
        private readonly int firstSeason = 2014;
        private WebDriverWait _wait;

        public ScrapperService(string url)
        {
            _url = url;

            string enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var chromeOptions = new ChromeOptions();

            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.SuppressInitialDiagnosticInformation = true;  // Suprime logs de inicialização
            chromeDriverService.EnableVerboseLogging = false;  // Desativa logs detalhados
            chromeDriverService.HideCommandPromptWindow = true;  // Oculta a janela de comando (somente no Windows)

            if (enviroment == "Docker")
            {
                chromeOptions.AddArgument("--headless");  // Para rodar no Docker sem interface gráfica
                chromeOptions.AddArgument("--no-sandbox");  // Recomendado para Docker
                chromeOptions.AddArgument("--disable-dev-shm-usage");  // Evita problemas com espaço limitado
            }

            _driver = new ChromeDriver(chromeDriverService, chromeOptions);
            _driver.Navigate().GoToUrl(_url);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            Thread.Sleep(5000);
        }

        public void GetMatches()
        {
            AcceptCookies();
            List<string> seasons = GetSeasons();
            List<string> matchLinks = new List<string>();
            List<MatchInfo> matches = new List<MatchInfo>();

            foreach (string season in seasons)
            {
                _driver.Navigate().GoToUrl(season);
                Thread.Sleep(5000);

                List<IWebElement> pageLinks = GetLinksPagination();

                if (pageLinks.Count > 0)
                {
                    List<string> links = GetMatchLinksFromPage();
                    matchLinks.AddRange(links);

                    pageLinks.RemoveAt(0);

                    foreach (IWebElement pageLink in pageLinks)
                    {
                        string str = pageLink.GetAttribute("innerHTML");

                        try
                        {
                            // Certifica-se de que o elemento está visível na viewport
                            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center', inline: 'nearest'});", pageLink);

                            // Clica no elemento diretamente
                            pageLink.Click();
                        }
                        catch (ElementClickInterceptedException)
                        {
                            Console.WriteLine($"Erro ao clicar na página {str}. Tentando com JavaScript...");

                            // Fallback: Usa JavaScript para clicar
                            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", pageLink);
                        }

                        Thread.Sleep(5000);

                        links = GetMatchLinksFromPage();
                        matchLinks.AddRange(links);
                    }
                }
                
                foreach (string matchLink in matchLinks)
                {
                    MatchInfo match = GetMatchInfo(matchLink);
                }

                // Buscar dados dos jogos
                // Salvar dados no banco
            }
        }

        #region Private methods

        private void AcceptCookies()
        {
            IWebElement button = _driver.FindElement(By.Id("onetrust-accept-btn-handler"));
            Thread.Sleep(2000);

            button.Click();

            Thread.Sleep(2000);
        }

        private List<string> GetSeasons()
        {
            List<string> seasons = new List<string>();
            List<IWebElement> seasonElements =
                _driver.FindElements(By.XPath("//*[@id=\"app\"]/div[1]/div[1]/div/main/div[3]/div[3]/div/div[2]/a")).ToList();

            foreach (IWebElement element in seasonElements)
            {
                string elementString = element.GetAttribute("innerHTML");
                List<string> seasonsSplitted = elementString.Split('/').ToList();

                if (seasonsSplitted.Count == 2 && Convert.ToInt32(seasonsSplitted[0]) >= firstSeason && Convert.ToInt32(seasonsSplitted[1]) >= firstSeason)
                    seasons.Add(element.GetAttribute("href"));
            }

            return seasons;
        }

        private List<IWebElement> GetLinksPagination()
        {
            List<IWebElement> elements = _driver.FindElements(By.XPath("//*[@id=\"app\"]/div[1]/div[1]/div/main/div[3]/div[4]/div[1]/div[3]/div/a")).ToList();

            List<IWebElement> filteredElements = elements
                .Where(e => int.TryParse(e.GetAttribute("innerHTML"), out _))
                .ToList();

            return filteredElements;
        }

        private List<string> GetMatchLinksFromPage()
        {
            List<string> matchLinks = new List<string>();

            ScrollToLoadAllElements();

            List<IWebElement> elements = _driver.FindElements(By.XPath("//*[@class=\"eventRow flex w-full flex-col text-xs\"]")).ToList();

            if (elements.Count > 0)
            {
                foreach (IWebElement element in elements)
                {
                    string elementString = element.GetAttribute("innerHTML");

                    try
                    {
                        IWebElement matchLinkElement = element.FindElement(By.XPath("div[@class=\"border-black-borders border-b border-l border-r hover:bg-[#f9e9cc]\"]/div/a"));

                        if (matchLinkElement != null)
                            matchLinks.Add(matchLinkElement.GetAttribute("href"));
                    }
                    catch (NoSuchElementException)
                    {
                        // Log se não encontrar o elemento (opcional)
                        Console.WriteLine("Elemento esperado não encontrado em: " + elementString);
                    }
                }
            }

            return matchLinks;
        }

        private void ScrollToLoadAllElements()
        {
            long lastHeight = (long)((IJavaScriptExecutor)_driver).ExecuteScript("return document.body.scrollHeight");

            while (true)
            {
                ((IJavaScriptExecutor)_driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");

                Thread.Sleep(1000);

                long newHeight = (long)((IJavaScriptExecutor)_driver).ExecuteScript("return document.body.scrollHeight");
                
                if (newHeight == lastHeight)
                    break;

                lastHeight = newHeight;
            }
        }

        private MatchInfo GetMatchInfo(string matchLink)
        {
            MatchInfo match = new MatchInfo();
            _driver.Navigate().GoToUrl(matchLink);
            Thread.Sleep(3000);

            GetMatchDetails(match);

            return match;
        }

        private void GetMatchDetails(MatchInfo match)
        {
            List<IWebElement> dateElements = _driver.FindElements(By.XPath("/html/body/div[1]/div[1]/div[1]/div/main/div[3]/div[2]/div[1]/div[2]/div[1]/p")).ToList();

            if (dateElements != null && dateElements.Count == 3)
            {
                string dateString = $"{dateElements[1].GetAttribute("innerHTML").Replace(",", "")} {dateElements[2].GetAttribute("innerHTML")}";
                string format = "dd MMM yyyy HH:mm"; // Formato da string
                CultureInfo culture = CultureInfo.InvariantCulture; // Cultura para o formato da data

                // Parseando a string para DateTime
                match.Date = DateTime.ParseExact(dateString, format, culture);
            }
            
            IWebElement matchNameElement = _driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[1]/div/main/div[3]/div[2]/div[1]/div[1]"));
            
            if (matchNameElement != null)
            {
                IWebElement homeTeamElement = matchNameElement.FindElement(By.XPath("div[@class=\"min-sm:gap-2 min-mm:flex-row justify-content flex items-center gap-1\"]/div"));
                match.HomeTeamName = homeTeamElement.FindElement(By.XPath("div/span")).GetAttribute("innerHTML");
                match.HomeTeamGoals = Convert.ToInt32(homeTeamElement.FindElement(By.XPath("div[@class=\"max-mm:gap-2 flex items-center justify-end\"]/div")).GetAttribute("innerHTML"));

                IWebElement awayTeamElement = matchNameElement.FindElement(By.XPath("div[@class=\"min-mm:gap-2 max-mm:justify-between max-mm:w-full flex items-center gap-1\"]"));
                match.AwayTeamName = awayTeamElement.FindElement(By.XPath("div/span")).GetAttribute("innerHTML");
                match.AwayTeamGoals = Convert.ToInt32(awayTeamElement.FindElement(By.XPath("div[@class=\"max-mm:order-last max-mm:gap-2 order-first flex\"]/div")).GetAttribute("innerHTML"));
            }
        }

        #endregion
    }
}
