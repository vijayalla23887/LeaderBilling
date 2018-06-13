using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Leader
{
    class ExportExcel
    {
        public void ExportToExcel(string FileName,DataGridView DtGridView)
        {

            // creating Excel Application  
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            try
            {
                // creating new WorkBook within Excel application  
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                // creating new Excelsheet in workbook  
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                // see the excel sheet behind the program  
                app.Visible = false;
                // get the reference of first sheet. By default its name is Sheet1.  
                // store its reference to worksheet  
                worksheet = workbook.Sheets["Sheet1"];
                worksheet = workbook.ActiveSheet;
                // changing the name of active sheet  
                worksheet.Name = "Exported from gridview";
                // storing header part in Excel  
                for (int i = 1; i < DtGridView.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = DtGridView.Columns[i - 1].HeaderText;
                }
                // storing Each row and column value to excel sheet  
                for (int i = 0; i < DtGridView.Rows.Count; i++)
                {
                    for (int j = 0; j < DtGridView.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = DtGridView.Rows[i].Cells[j].Value.ToString();
                    }
                }
                // save the application  
                workbook.SaveAs(FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                // Exit from the application  
                app.Quit();
            }
            catch (Exception ex)
            {
                app.Quit();
            }
        }
    }
}
