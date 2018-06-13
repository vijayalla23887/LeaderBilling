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
    public partial class InvoiceEntryDetails : Form
    {
        SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["LeaderConn"].ConnectionString);
        public InvoiceEntryDetails()
        {
            InitializeComponent();
            LoadInvoiceDetails();
        }
        public void LoadInvoiceDetails()
        {
            dataConnection.Open();
            string strCmd = "spInvoiceDetails";
            SqlCommand cmd = new SqlCommand(strCmd, dataConnection);
            SqlDataAdapter da = new SqlDataAdapter(strCmd, dataConnection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmd.ExecuteNonQuery();
            dataConnection.Close();
            gvCountgrid.DataSource = ds.Tables[0];
            gvCountgrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
