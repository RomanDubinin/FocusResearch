using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;

namespace Download
{
    public class ExelReader
    {
        private readonly int InnColumn = 2;
        private readonly int OgrnColumn = 4;

        public List<(string inn, string ogrn)> GetOrganizationIdentifiers(string fileName)
        {
            var result = new List<(string inn, string ogrn)>();
            using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(fileName)))
            {
                var myWorksheet = xlPackage.Workbook.Worksheets.First();
                var totalRows = myWorksheet.Dimension.End.Row;

                if (myWorksheet.Cells[1, InnColumn, 1, InnColumn].First().Value.ToString() != "ИНН")
                    throw new Exception($"Incorrect INN Column number in {fileName}.");
                if (myWorksheet.Cells[1, OgrnColumn, 1, OgrnColumn].First().Value.ToString() != "ОГРН")
                    throw new Exception($"Incorrect OGRN Column number in {fileName}.");

                for (int rowNum = 2; rowNum <= totalRows; rowNum++)
                {
                    var inn = myWorksheet.Cells[rowNum, InnColumn, rowNum, InnColumn].First().Value.ToString();
                    var ogrn = myWorksheet.Cells[rowNum, OgrnColumn, rowNum, OgrnColumn].First().Value.ToString();
                    result.Add((inn, ogrn));
                }
            }

            return result;
        }
    }
}