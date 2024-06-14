using BetPlacer.Core.Controllers;
using BetPlacer.Punter.API.Models;
using BetPlacer.Punter.API.Repositories;
using BetPlacer.Punter.API.Services;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.ComponentModel;

namespace BetPlacer.Punter.API.Controllers
{
    [Route("api/punter")]
    public class PunterController : BaseController
    {
        private readonly PunterRepository _punterRepository;
        private readonly CalculateStatsService _calculateStatsService;
        
        public PunterController(PunterRepository punterRepository)
        {
            _punterRepository = punterRepository;
            _calculateStatsService = new CalculateStatsService();
        }

        [HttpGet]
        public async Task<ActionResult> GetMatchBaseData(int leagueCode, bool generateExcel)
        {
            List<MatchBaseData> info = await _punterRepository.GetMatchBaseDataAsync(leagueCode);

            if (generateExcel)
                GenerateExcel<MatchBaseData>(info);

            _calculateStatsService.CalculateStats(info);

            return OkResponse(info);
        }

        #region Private methods

        public void GenerateExcel<T>(List<T> lista)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Dados");

                var properties = typeof(T).GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = properties[i].Name;
                }

                for (int i = 0; i < lista.Count; i++)
                {
                    var item = lista[i];
                    for (int j = 0; j < properties.Length; j++)
                    {
                        worksheet.Cells[i + 2, j + 1].Value = properties[j].GetValue(item);
                    }
                }

                var range = worksheet.Cells[1, 1, lista.Count + 1, properties.Length];
                var table = worksheet.Tables.Add(range, "TabelaDados");
                table.TableStyle = TableStyles.Medium1;

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var fileInfo = new FileInfo("C:\\Users\\bob_l\\Documents\\teste.xlsx");
                package.SaveAs(fileInfo);
            }
        }

        #endregion
    }
}
