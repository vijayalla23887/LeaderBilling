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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Leader
{
    public partial class ROFormReport : Form
    {
        SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["LeaderConn"].ConnectionString);
        int ADID=0;
        public ROFormReport()
        {
            InitializeComponent();
        }
        public ROFormReport(int ID)
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

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            ROReport obj = new ROReport();
            obj.SetDataSource(getBill(ADID));
            crystalReportViewer1.ReportSource = obj;
        }

    }
}
