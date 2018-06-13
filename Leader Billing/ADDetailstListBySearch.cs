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
    public partial class ADDetailstListBySearch : Form
    {
        SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["LeaderConn"].ConnectionString);
        public ADDetailstListBySearch()
        {
            InitializeComponent();
            dtFromDate.Value = DateTime.Now;
            dtToDate.Value = DateTime.Now.AddDays(1);
            DataGridViewLinkColumn link = new DataGridViewLinkColumn();
            link.UseColumnTextForLinkValue = true;
            link.HeaderText = "Click to View Details";
            link.DataPropertyName = "lnkColumn";
            link.ActiveLinkColor = Color.White;
            link.LinkBehavior = LinkBehavior.SystemDefault;
            link.LinkColor = Color.Blue;
            link.Text = "AD/RO Details";
            link.VisitedLinkColor = Color.YellowGreen;
            gvAdListgrid.Columns.Add(link);

            DataGridViewLinkColumn Paylink = new DataGridViewLinkColumn();
            Paylink.UseColumnTextForLinkValue = true;
            Paylink.HeaderText = "Click to View Payment Details";
            Paylink.DataPropertyName = "lnkPayColumn";
            Paylink.ActiveLinkColor = Color.White;
            Paylink.LinkBehavior = LinkBehavior.SystemDefault;
            Paylink.LinkColor = Color.Blue;
            Paylink.Text = "Payment Details";
            Paylink.VisitedLinkColor = Color.YellowGreen;
            gvAdListgrid.Columns.Add(Paylink);
            this.gvAdListgrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            gvAdListgrid.DataSource = LoadAdListDetails(dtFromDate.Value, dtToDate.Value,txtAgentName.Text, txtName.Text, txtPhoneNumber.Text);
        }

        public DataTable LoadAdListDetails(DateTime StartDate, DateTime EndDate,string AgentName,string Name,string PhoneNumber)
        {
            string strCmd = "spADListDetailsBySearch";
            SqlCommand cmd = new SqlCommand(strCmd, dataConnection);
            SqlDataAdapter da = new SqlDataAdapter(strCmd, dataConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@startdate", SqlDbType.DateTime).Value = StartDate;
            da.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime).Value = EndDate;
            da.SelectCommand.Parameters.AddWithValue("@AgentName", AgentName);
            da.SelectCommand.Parameters.AddWithValue("@Name", Name);
            da.SelectCommand.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);

            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                lblPaymentAmt.Text = "Total AD's Cost : " + dt.Compute("Sum(TotalADCost)", "");
                DataRow NewRow = dt.NewRow();
                NewRow["TotalADCost"] = dt.Compute("Sum(TotalADCost)", "");
                NewRow["ADCost"] = dt.Compute("Sum(ADCost)", "" );
                NewRow["GstCost"] = dt.Compute("Sum(GstCost)", "");
                NewRow["TotalReceivedAmt"] = dt.Compute("Sum(TotalReceivedAmt)", "");
                NewRow["ReceivedGstAmount"] = dt.Compute("Sum(ReceivedGstAmount)", "");
                NewRow["BalanceAmt"] = dt.Compute("Sum(BalanceAmt)", "");
                NewRow["CommissionAmount"] = dt.Compute("Sum(CommissionAmount)", "");
                dt.Rows.Add(NewRow);
            }
            return dt;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            gvAdListgrid.DataSource = LoadAdListDetails(dtFromDate.Value, dtToDate.Value,txtAgentName.Text,txtName.Text,txtPhoneNumber.Text);
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
            ex.ExportToExcel(ExportFileDialog.FileName, gvAdListgrid);
        }

        private void gvAdListgrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != gvAdListgrid.RowCount-1)
            {
                //Check whether user click on the first column 
                if (e.ColumnIndex == 0 && e.RowIndex != -1)
                {
                    int row;
                    //Get the row index
                    row = e.RowIndex;
                    //Create instance of the form2 class
                    ADEntryForm obj = new ADEntryForm(Convert.ToInt32(gvAdListgrid.Rows[row].Cells["ADID"].Value));
                    //Bind the datagridview values in the second popup form
                    obj.Controls["lblADID"].Text = gvAdListgrid.Rows[row].Cells["ADID"].Value.ToString();
                    obj.Controls["btnSave"].Text = "Update";
                    obj.ShowDialog();
                }

                if (e.ColumnIndex == 1 && e.RowIndex != -1)
                {
                    int row;
                    //Get the row index
                    row = e.RowIndex;
                    //Create instance of the form2 class
                    ADPaymentEntry obj = new ADPaymentEntry(Convert.ToInt32(gvAdListgrid.Rows[row].Cells["ADID"].Value));
                    //Bind the datagridview values in the second popup form
                    obj.Controls["lblADID"].Text = gvAdListgrid.Rows[row].Cells["ADID"].Value.ToString();
                    obj.ShowDialog();
                }
            }
        }
    }
}
