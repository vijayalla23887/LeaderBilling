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
    public partial class ReportByChart : Form
    {
        SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["LeaderConn"].ConnectionString);

        public ReportByChart()
        {
            InitializeComponent();
            DataTable dt = LoadOutStandingADDetails(); ;
            chart1.DataSource = dt;
            chart1.Series["Balance Amount"].XValueMember = "CommissionAgent";
            chart1.Series["Balance Amount"].YValueMembers = "Balance Amount";
            //chart1.Series["Total Amount"].YValueMembers = "TotalAmount";
            chart1.Titles.Add("Balance Amount By Commission Agent");
            chart1.Series["Balance Amount"]["PixelPointWidth"] = "40";
            gvAmtDetails.DataSource = dt;
            gvAmtDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
        }

        public DataTable LoadOutStandingADDetails()
        {
            string strCmd = "spGetOutstandingADs_CommissionAgentChart";
            SqlDataAdapter da = new SqlDataAdapter(strCmd, dataConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
