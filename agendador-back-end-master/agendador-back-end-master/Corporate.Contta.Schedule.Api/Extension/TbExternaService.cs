using Corporate.Contta.Schedule.Domain.Entities.ExternalTable;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Corporate.Contta.Schedule.Api.Extension
{
    public class TbExternaService
    {
        public List<IcmsSt> GetListIcmsSt(string diretory)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.Commercial;

                List<IcmsSt> listIcms = new List<IcmsSt>();

                using (var package = new ExcelPackage(new FileInfo(diretory)))
                {
                    var bytes = File.ReadAllBytes(diretory);
                    var stream = new MemoryStream(bytes);
                    //var package = new ExcelPackage(stream);
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    var colCount = worksheet.Dimension.Columns;
                    var header = ExcelUtils.CellsToArray(worksheet, 1, colCount);

                    for (var row = 1; row <= rowCount; row++)
                    {
                        var data = ExcelUtils.CellsToArray(worksheet, row, colCount);

                        listIcms.Add(new IcmsSt
                        {
                            Item = data.GetValue(0).ToString(),
                            Subitem = data.GetValue(1).ToString(),
                            Descricao = data.GetValue(2).ToString(),
                            CEST = data.GetValue(3).ToString(),
                            NCM = data.GetValue(4).ToString(),
                            MVA1 = Convert.ToInt32(data.GetValue(5)),
                            MVA2 = Convert.ToInt32(data.GetValue(6)),
                            MVA3 = Convert.ToInt32(data.GetValue(7)),
                            MVA4 = Convert.ToInt32(data.GetValue(8)),
                            DataInicial = Convert.ToDateTime(data.GetValue(9)),
                            DataFinal = Convert.ToDateTime(data.GetValue(10))
                        });
                    }

                    return listIcms;
                }
            }
            catch ( Exception ex)
            {
                throw;
            } 
        }
    }

    public static class ExcelUtils
    {
        public static string[] CellsToArray(ExcelWorksheet worksheet, int row, int colCount)
        {
            var array = new string[colCount];
            for (int col = 0; col < colCount; col++) array[col] = worksheet.Cells[row, (col + 1)].Value?.ToString();
            return array;
        }

        public static string GetData(string prop, string[] header, string[] data) => header
            .Zip(data, (h, d) => new KeyValuePair<string, string>(h, d))
            .FirstOrDefault(d => d.Key.Equals(prop))
            .Value;
    }
}

