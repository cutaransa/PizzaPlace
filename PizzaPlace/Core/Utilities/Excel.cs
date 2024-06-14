using Elmah;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Syncfusion.XlsIO;
using System.Web.Hosting;

using Excel = Microsoft.Office.Interop.Excel;

namespace PizzaPlace.Core.Utilities
{
    public class Excel
    {
        public static DataTable ImportExcelData(string file)
        {
            DataTable data = null;

            try
            {
                ExcelEngine excelEngine = new ExcelEngine();
                IApplication application = excelEngine.Excel;

                application.UseFastRecordParsing = true;

                IWorkbook workbook = application.Workbooks.Open(file);
                IWorksheet sheet = workbook.Worksheets[0];

                data = sheet.ExportDataTable(sheet.UsedRange, ExcelExportDataTableOptions.ColumnNames);

                excelEngine.Dispose();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }

            return data;
        }

        public static List<T> ImportExcelDataList<T>(string file) where T : class, new()
        {
            List<T> data = null;

            try
            {
                ExcelEngine excelEngine = new ExcelEngine();
                IApplication application = excelEngine.Excel;

                application.UseFastRecordParsing = true;

                IWorkbook workbook = application.Workbooks.Open(file);
                IWorksheet sheet = workbook.Worksheets[0];

                data = sheet.ExportData<T>(1, 1, sheet.UsedRange.LastRow, sheet.UsedRange.LastColumn);

                excelEngine.Dispose();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }

            return data;
        }

        public static void ExportDataTable(DataTable table, string workSheetName, string fileName)
        {
            MemoryStream Result = new MemoryStream();
            ExcelPackage pack = new ExcelPackage();
            ExcelWorksheet ws = pack.Workbook.Worksheets.Add(workSheetName);

            int col = 1;
            int row = 1;
            foreach (DataColumn cl in table.Columns)
            {
                ws.Cells[row, col].Value = cl.ToString();
                col++;
            }
            row = row + 1;
            col = 1;
            foreach (DataRow rw in table.Rows)
            {
                foreach (DataColumn cl in table.Columns)
                {
                    if (rw[cl.ColumnName] != DBNull.Value)
                        ws.Cells[row, col].Value = rw[cl.ColumnName].ToString();
                    col++;
                }
                row++;
                col = 1;
            }
            pack.SaveAs(Result);
            Result.Seek(0, SeekOrigin.Begin);

            string path = Path.Combine(HostingEnvironment.MapPath("~\\Results\\"), workSheetName, fileName);
            FileInfo excelFile = new FileInfo(path);
            pack.SaveAs(excelFile);

            return;
        }

    }
}
