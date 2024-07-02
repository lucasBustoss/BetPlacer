using System.Xml.Linq;

namespace BetPlacer.Fixtures.API.Utils
{
    public static class FixtureUtils
    {
        public static DateTime TimestampToDatetime(long unixTimeStamp)
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return unixEpoch.AddSeconds(unixTimeStamp).ToLocalTime();
        }

        public static string GetFootyStatsNameByPinnacleName(string name)
        {
            if (name == "Atletico Madrid")
                return "Atlético Madrid";

            if (name == "Getafe")
                return "Getafe CF";

            if (name == "Sevilla FC")
                return "Sevilla";

            if (name == "AC Ajaccio")
                return "Ajaccio";

            if (name == "Valenciennes FC")
                return "Valenciennes";

            if (name == "Pau FC")
                return "Pau";

            if (name == "Troyes AC")
                return "Troyes";

            if (name == "Paris FC")
                return "Paris";

            if (name == "Quevilly")
                return "Quevilly Rouen";

            if (name == "Albacete")
                return "Albacete Balompié";

            if (name == "Eldense")
                return "CD Eldense";

            if (name == "Elche")
                return "Elche CF";

            if (name == "Mirandes")
                return "Mirandés";

            if (name == "Espanyol")
                return "RCD Espanyol";

            if (name == "CD Burgos")
                return "Burgos CF";

            if (name == "Barcelona")
                return "FC Barcelona";

            if (name == "Almeria")
                return "Almería";

            if (name == "Keciorengucu")
                return "Keçiörengücü";

            if (name == "Bandirmaspor")
                return "Bandırmaspor";

            if (name == "Erzurumspor")
                return "BB Erzurumspor";

            if (name == "Umraniyespor")
                return "Ümraniyespor.";

            if (name == "Goztepe")
                return "Göztepe";

            if (name == "Eyupspor")
                return "Eyüpspor.";

            if (name == "Villareal CF")
                return "Villarreal";

            if (name == "Celta Vigo")
                return "Celta de Vigo";

            if (name == "Eibar")
                return "SD Eibar";

            if (name == "Gijon")
                return "Sporting Gijón";

            if (name == "Leganes")
                return "Leganés";

            if (name == "Tenerife")
                return "CD Tenerife";

            if (name == "Toulouse FC")
                return "Toulouse";

            if (name == "AS Monaco")
                return "Monaco";

            if (name == "Montpellier HSC")
                return "Montpellier";

            if (name == "Marseille")
                return "Olympique Marseille";

            if (name == "Mallorca")
                return "RCD Mallorca";

            if (name == "Osasuna")
                return "CA Osasuna";

            if (name == "Villarreal B")
                return "Villarreal II";

            if (name == "Real Oviedo CF")
                return "Real Oviedo";

            if (name == "Amorebieta")
                return "SD Amorebieta";

            if (name == "Alcorcon")
                return "AD Alcorcón";

            if (name == "Real Valladolid CF")
                return "Real Valladolid";

            if (name == "Racing Club Ferrol")
                return "Racing Club de Ferrol";

            if (name == "Sanliurfaspor")
                return "Şanlıurfaspor";

            if (name == "Corum Belediyespor")
                return "Çorum Belediyespor";

            if (name == "Gent")
                return "KAA Gent";

            if (name == "Oud-Heverlee Leuven")
                return "OH Leuven";

            if (name == "Al-Khaleej")
                return "Al Khaleej";

            if (name == "Al-Fayha")
                return "Al Feiha";

            if (name == "Damac FC")
                return "Dhamk";

            if (name == "Al-Taee")
                return "Al Taee";

            if (name == "Al-Hilal")
                return "Al Hilal";

            if (name == "Abha Club")
                return "Abha";

            if (name == "Maastricht")
                return "MVV";

            if (name == "TOP Oss")
                return "Oss";

            if (name == "Hapoel Ramat Gan Givatayim FC")
                return "Hapoel Ramat Gan";

            if (name == "Hapoel Afula FC")
                return "Hapoel Afula";

            if (name == "Hapel Nof Hagalif FC")
                return "Nazareth Illit";

            if (name == "Hapoel Rishon Lezion FC")
                return "Hapoel Rishon LeZion";

            if (name == "Hapoel Kfar Saba FC")
                return "Hapoel Kfar Saba";

            if (name == "Hapoel Acre FC")
                return "Hapoel Acre";

            if (name == "FC Kafr Qasim")
                return "Kafr Qasim";

            if (name == "Sektzia Ness Ziona FC")
                return "Sektzia Nes Tziona";

            if (name == "Al-Akhdoud")
                return "Al Akhdoud";

            if (name == "Al Shabab (KSA)")
                return "Al Shabab";

            if (name == "Al-Nassr")
                return "Al Nassr";

            if (name == "Al-Ettifaq")
                return "Al Ittifaq";

            if (name == "Al-Hazem")
                return "Al Hazm";

            if (name == "Al-Ahli")
                return "Al Ahli";

            if (name == "UD Leiria")
                return "União de Leiria";

            if (name == "Pacos Ferreira")
                return "Paços de Ferreira";

            if (name == "Karagumruk")
                return "Fatih Karagümrük";

            if (name == "Gaziantep")
                return "Gazişehir Gaziantep";

            if (name == "Al-Taawon")
                return "Al Taawon";

            if (name == "Al-Fateh")
                return "Al Fateh";

            if (name == "Vizela")
                return "FC Vizela";

            if (name == "Moreirense")
                return "Moreirense FC";

            if (name == "Vitoria Guimaraes")
                return "Vitória Guimarães";

            if (name == "Rio Ave")
                return "Rio Ave FC";

            if (name == "Maritimo")
                return "CS Marítimo";

            if (name == "Penafiel")
                return "FC Penafiel";

            if (name == "Wydad AC")
                return "Wydad Casablanca";

            if (name == "CA Youssoufia Berrechid")
                return "Youssoufia Berrechid";

            if (name == "Sint Truidense")
                return "Sint-Truiden";

            if (name == "Charleroi")
                return "Sporting Charleroi";

            if (name == "Al-Wehda")
                return "Al Wahda";

            if (name == "Al-Riyadh")
                return "Al Riyadh";

            if (name == "Al Ittihad (KSA)")
                return "Al Ittihad";

            if (name == "Al-Raed")
                return "Al Raed";

            if (name == "FC Eindhoven")
                return "Eindhoven";

            if (name == "Jong Ajax")
                return "Ajax II";

            if (name == "Anderlecht")
                return "RSC Anderlecht";

            if (name == "Genk")
                return "KRC Genk";

            if (name == "Rsb Berkane")
                return "RSB Berkane";

            if (name == "Raja Casablanca Athletic")
                return "Raja Casablanca";

            if (name == "Paykan FC")
                return "Paykan";

            if (name == "Foolad Khuzestan FC")
                return "Foolad";

            if (name == "FC Nassaji Mazandaran")
                return "Nassaji Mazandaran";

            if (name == "Havadar SC")
                return "Havadar";

            if (name == "Esteghlal Khuzestan FC")
                return "Esteghlal Khuzestan";

            if (name == "Mes Rajsanjan FC")
                return "Mes Rafsanjan";

            if (name == "Baladiyat El Mahalla")
                return "Baladiyyat Al Mehalla";

            if (name == "National Bank Egypt")
                return "National Bank of Egypt";

            if (name == "Kasimpasa")
                return "Kasımpaşa";

            if (name == "Caykur Rizespor")
                return "Rizespor";

            if (name == "Ihud Bnei Shefa-Amr")
                return "Ihud Bnei Shfaram";

            if (name == "Hapoel Nir Ramat Hasharon FC")
                return "Ironi Ramat HaSharon";

            if (name == "Malavan Bandar Anzali FC")
                return "Malavan";

            if (name == "Tractor")
                return "Tractor Sazi";

            if (name == "sanat Naft Abadan FC")
                return "Sanat Naft";

            if (name == "Zob Ahan Isfahan FC")
                return "Zob Ahan";

            if (name == "Al Ittihad Al Sakandary")
                return "Al Ittihad";

            if (name == "Pharco FC")
                return "Pharco";

            if (name == "ZED")
                return "Masr";

            if (name == "El Daklyeh")
                return "El Daklyeh FC";

            if (name == "Smouha Sc")
                return "Smouha SC";

            if (name == "Talaea El Gaish")
                return "El Geish";

            if (name == "Moghreb Athletic de Tetouan")
                return "Moghreb Tétouan";

            if (name == "Union Touarga")
                return "UTS Rabat";

            if (name == "Gol Gohar Sirjan FC")
                return "Gol Gohar";

            if (name == "Foolad Mobarakeh Sepahan SC")
                return "Sepahan";

            if (name == "Estrela da Amadora")
                return "Estrela Amadora";

            if (name == "Arouca")
                return "FC Arouca";

            if (name == "Renaissance Zemamra")
                return "CR Khemis Zemamra";

            if (name == "Mouloudia Club of Oujda")
                return "Mouloudia Oujda";

            if (name == "Hassania Union Sport Agadir")
                return "Hassania Agadir";

            if (name == "Maghreb AS de Fes")
                return "Maghreb Fès";

            if (name == "JS Soualem")
                return "Riadi Salmi";

            if (name == "AS Far Rabat")
                return "FAR Rabat";

            if (name == "Mansfield")
                return "Mansfield Town";

            if (name == "Doncaster")
                return "Doncaster Rovers";

            if (name == "FC Porto")
                return "Porto";

            if (name == "Chaves")
                return "GD Chaves";

            if (name == "Tondela")
                return "CD Tondela";

            if (name == "Oliveirense")
                return "UD Oliveirense";

            if (name == "Porto B")
                return "Porto II";

            if (name == "Nacional da Madeira")
                return "CD Nacional";

            if (name == "Leixoes")
                return "Leixões";

            if (name == "Academico de Viseu")
                return "Academico Viseu";

            if (name == "Valencia")
                return "Valencia CF";

            if (name == "Villarreal CF")
                return "Villarreal";

            if (name == "Alavés")
                return "Deportivo Alavés";

            if (name == "Cádiz")
                return "Cádiz";

            if (name == "Athletico Paranaense")
                return "Atlético PR";

            if (name == "Vitoria BA")
                return "Vitória";

            if (name == "Botafogo FR RJ")
                return "Botafogo";

            if (name == "Atletico Goianiense")
                return "Atlético GO";

            if (name == "Atlanta United")
                return "Atlanta United FC";

            if (name == "Toronto FC")
                return "Toronto";

            if (name == "CF Montreal")
                return "Montreal Impact";

            if (name == "New York Red Bulls")
                return "New York RB";

            if (name == "D.C. United")
                return "DC United";

            if (name == "Dallas")
                return "FC Dallas";

            if (name == "Charlotte FC")
                return "Charlotte";

            if (name == "St. Louis City SC")
                return "St. Louis City";

            if (name == "San Jose Earthquakes")
                return "SJ Earthquakes";

            //if (name == "")
            //    return "Sporting KC";

            //if (name == "")
            //    return "Austin";

            //if (name == "")
            //    return "Nashville SC";

            //if (name == "")
            //    return "Inter Miami";

            //if (name == "")
            //    return "Vancouver Whitecaps";

            if (name == "Goias")
                return "Goiás";

            if (name == "America Mineiro")
                return "América Mineiro";

            return name;
        }

    }
}
