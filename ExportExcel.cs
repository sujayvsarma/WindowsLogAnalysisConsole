using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Data;
using System.Linq;

namespace LogAnalysisConsole
{
    internal class ExportExcel
    {

        public static bool SaveFile(DataSet data, string filePath)
        {
            try
            {
                using (SpreadsheetDocument excelFile = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
                {  
                    WorkbookPart wbPart = excelFile.AddWorkbookPart();
                    wbPart.Workbook = new Workbook();
                    wbPart.Workbook.Save();

                    wbPart.Workbook.Sheets = new Sheets();

                    foreach (DataTable tab in data.Tables)
                    {
                        SheetData sd = new SheetData();
                        WorksheetPart wsPart = wbPart.AddNewPart<WorksheetPart>();
                        wsPart.Worksheet = new Worksheet(sd);
                        wsPart.Worksheet.Save();

                        string relId = wbPart.GetIdOfPart(wsPart);
                        uint shId = 1;

                        if (wbPart.Workbook.Sheets.Elements<Sheet>().Count() > 0)
                        {
                            shId = wbPart.Workbook.Sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
                        }
                        string shName = tab.TableName;
                        Sheet thisSheet = new Sheet()
                        {
                            Id = relId,
                            SheetId = shId,
                            Name = shName
                        };

                        wbPart.Workbook.Sheets.Append(thisSheet);
                        wbPart.Workbook.Save();

                        // write column headers
                        Row hr = new Row();
                        foreach (DataColumn dc in tab.Columns)
                        {
                            Cell sc = new Cell();
                            sc.DataType = CellValues.InlineString;
                            InlineString value = new InlineString();
                            Text valueText = new Text();
                            valueText.Text = dc.ColumnName.ToString();
                            value.AppendChild(valueText);
                            sc.AppendChild(value);
                            hr.Append(sc);
                        }
                        sd.Append(hr);

                        // write data
                        foreach (DataRow dr in tab.Rows)
                        {
                            Row sr = new Row();
                            foreach (DataColumn dc in tab.Columns)
                            {
                                Cell sc = new Cell();

                                sc.DataType = CellValues.InlineString;
                                InlineString value = new InlineString();
                                Text valueText = new Text();
                                if (dr[dc.ColumnName] != null)
                                {
                                    valueText.Text = dr[dc.ColumnName].ToString();
                                }
                                else
                                {
                                    valueText.Text = "";
                                }
                                value.AppendChild(valueText);
                                sc.AppendChild(value);
                                sr.Append(sc);
                            }

                            sd.Append(sr);
                        }
                    }

                    excelFile.WorkbookPart.Workbook.Save();
                    excelFile.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
    }
}
