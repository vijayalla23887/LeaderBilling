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
    public partial class PaymentReceiptReport : Form
    {
        SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["LeaderConn"].ConnectionString);
        int ADID = 0;
        int PaymentID = 0;
        public PaymentReceiptReport()
        {
            InitializeComponent();
        }
        public PaymentReceiptReport(int AID,int PayID)
        {
            InitializeComponent();
            ADID = AID;
            PaymentID = PayID;

        }
        public DataTable getBill(int ID,int PaymentID)
        {
            DataTable dt = new DataTable();
            string strCmd = "[spGetCashReceipt]";
            SqlCommand cmd = new SqlCommand(strCmd, dataConnection);
            SqlDataAdapter da = new SqlDataAdapter(strCmd, dataConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@ADID", SqlDbType.Int).Value = ID;
            da.SelectCommand.Parameters.Add("@PaymentID", SqlDbType.Int).Value = PaymentID;

            da.Fill(dt);
            return dt;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            CashReport obj = new CashReport();
            obj.SetDataSource(getBill(ADID,PaymentID));
            crystalReportViewer1.ReportSource = obj;
        }
    }
}
