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
    public partial class BillFormReport : Form
    {
        public BillFormReport()
        {
            InitializeComponent();
        }
        SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["LeaderConn"].ConnectionString);
        int ADID = 0;
        
        public BillFormReport(int ID)
        {
            InitializeComponent();
            ADID = ID;
        }
        public DataTable getBill(int ID)
        {
            string strCmd = "[spADEntryPaymentDetails]";
            SqlCommand cmd = new SqlCommand(strCmd, dataConnection);
            SqlDataAdapter da = new SqlDataAdapter(strCmd, dataConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@ADID", SqlDbType.Int).Value = ID;

            DataTable dt = new DataTable();
            da.Fill(dt);
            //dt.Columns.Add("InWords", typeof(System.String));
            //dt.Rows[0]["InWords"] = "RUPEES : " + Common.ConvertNumbertoWords(Convert.ToInt64(dt.Rows[0]["TotalADCost"])); 
            return dt;
        }
                
        private void crystalReportViewer1_Load_1(object sender, EventArgs e)
        {
            BillReport obj = new BillReport();
            obj.SetDataSource(getBill(ADID));
            crystalReportViewer1.ReportSource = obj;
        }
    }
}
