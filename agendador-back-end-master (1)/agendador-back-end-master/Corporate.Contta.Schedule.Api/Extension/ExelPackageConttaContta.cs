//using Corporate.Contta.Schedule.Domain.Entities.EstoqueAgg;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using OfficeOpenXml;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;

//namespace Corporate.Contta.Schedule.Api.Extension
//{
//    public class ExelPackageConttaContta
//    {
//        public List<string> GetListaCnpj(string diretory)
//        {
//            ExcelPackage.LicenseContext = LicenseContext.Commercial;
//            List<string> listCnpj = new List<string>();

//            try
//            {
//                var bytes = File.ReadAllBytes(diretory);
//                var stream = new MemoryStream(bytes);
//                var package = new ExcelPackage(stream);
//                var worksheet = package.Workbook.Worksheets[0];
//                var rowCount = worksheet.Dimension.Rows;
//                var colCount = worksheet.Dimension.Columns;
//                var header = ExcelUtils.CellsToArray(worksheet, 1, colCount);

//                for (var row = 2; row <= rowCount; row++)
//                {
//                    var data = ExcelUtils.CellsToArray(worksheet, row, colCount);

//                    foreach (var item in data.ToList())
//                    {
//                        if (item != "-" && item != null)
//                        {
//                            if (item.Contains("/"))
//                            {
//                                var cnpj = item.Trim();
//                                cnpj = item.Replace(".", "").Replace("/", "").Replace("-", "");

//                                if (cnpj.Length == 14)
//                                {
//                                    listCnpj.Add(cnpj);
//                                }
//                                else
//                                {

//                                }

//                            }
//                        }
//                    }
//                }

//                return listCnpj;
//            }
//            catch (Exception ex)
//            {

//                throw;
//            }
//        }

//        public List<Estoque> GetListaEstoque(string diretory)
//        {
//            ExcelPackage.LicenseContext = LicenseContext.Commercial;
//            List<Estoque> listEstoque = new List<Estoque>();


//            try
//            {
//                var bytes = File.ReadAllBytes(diretory);
//                var stream = new MemoryStream(bytes);
//                var package = new ExcelPackage(stream);
//                var worksheet = package.Workbook.Worksheets[0];
//                var rowCount = worksheet.Dimension.Rows;
//                var colCount = worksheet.Dimension.Columns;
//                var header = ExcelUtils.CellsToArray(worksheet, 1, colCount);

//                for (var row = 2; row <= rowCount; row++)
//                {
//                    var data = ExcelUtils.CellsToArray(worksheet, row, colCount);
//                    var qtd = 0;
//                    var EstoqueDto = new Estoque();

//                    foreach (var item in data.ToList())
//                    {
//                        if(item != null)
//                        {
//                            if (qtd == 0)
//                                EstoqueDto.CodProd = item;
//                            else if (qtd == 1)
//                                EstoqueDto.Descricao = item;
//                            else if (qtd == 2)
//                                EstoqueDto.Marca = item;
//                            else if (qtd == 3)
//                                EstoqueDto.VlUnitario = item.Trim();
//                            else if (qtd == 4)
//                                EstoqueDto.UniMedida = item;
//                            else if (qtd == 5)
//                            {
//                                bool ehValido =  item.All(char.IsDigit);
//                                if (ehValido)
//                                    EstoqueDto.Quantidade = (decimal.Parse(item));
//                                else
//                                    EstoqueDto.Quantidade = 0;
//                            }

                         
//                            else if (qtd == 6)
//                                EstoqueDto.CodBarra = item;

//                            qtd = qtd + 1;
//                        }                      
//                    }
//                    EstoqueDto.Id = Guid.NewGuid();
//                    listEstoque.Add(EstoqueDto);
//                }

//                return listEstoque;
//            }
//            catch (Exception ex)
//            {

//                throw;
//            }
//        }

//        public static class ExcelUtils
//        {
//            public static string[] CellsToArray(ExcelWorksheet worksheet, int row, int colCount)
//            {
//                var array = new string[colCount];
//                for (int col = 0; col < colCount; col++) array[col] = worksheet.Cells[row, (col + 1)].Value?.ToString();
//                return array;
//            }

//            public static string GetData(string prop, string[] header, string[] data) => header
//                .Zip(data, (h, d) => new KeyValuePair<string, string>(h, d))
//                .FirstOrDefault(d => d.Key.Equals(prop))
//                .Value;
//        }
//    }
//}
