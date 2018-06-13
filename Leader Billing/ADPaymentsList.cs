using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Leader
{
    public partial class ADPaymentsList : Form
    {
        SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["LeaderConn"].ConnectionString);
        public ADPaymentsList()
        {
            InitializeComponent();
            dtFromDate.Value = DateTime.Now;
            dtToDate.Value = DateTime.Now;
            this.gvAdPaymentsListgrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            gvAdPaymentsListgrid.DataSource = LoadPaymentListDetails(dtFromDate.Value, dtToDate.Value);
        }
        
        public DataTable LoadPaymentListDetails(DateTime StartDate, DateTime EndDate)
        {
            string strCmd = "spAdPaymentListDetails";
            SqlCommand cmd = new SqlCommand(strCmd, dataConnection);
            SqlDataAdapter da = new SqlDataAdapter(strCmd, dataConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@startdate", SqlDbType.DateTime).Value = StartDate;
            da.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime).Value = EndDate;

            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                lblPaymentAmt.Text = "Total Amount Collected : " + dt.Compute("Sum(ReceivedAmount)", "").ToString();
                DataRow NewRow = dt.NewRow();
                NewRow["ReceivedAmount"] = dt.Compute("Sum(ReceivedAmount)", "");
                NewRow["CommissionAmount"] = dt.Compute("Sum(CommissionAmount)", "");
                dt.Rows.Add(NewRow);
            }
            return dt;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            gvAdPaymentsListgrid.DataSource = LoadPaymentListDetails(dtFromDate.Value, dtToDate.Value);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportFileDialog.Filter = "Excel File|*.xls";
            ExportFileDialog.Title = "Save an Excel File";
            ExportFileDialog.ShowDialog();
        }

        private void ExportFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            ExportExcel ex = new ExportExcel();
            ex.ExportToExcel(ExportFileDialog.FileName,gvAdPaymentsListgrid);
        }
    }
}
