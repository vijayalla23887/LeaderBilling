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
    public partial class DailyCirculationEntry : Form
    {
        SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["LeaderConn"].ConnectionString);
        public DailyCirculationEntry()
        {
            InitializeComponent();
            dtDate.Value= DateTime.Now;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            LoadCirculationDailyDetails(dtDate.Value);
        }

        public void LoadCirculationDailyDetails(DateTime Date)
        {
            string strCmd = "[spGetDailyCirculationDetails]";
            SqlDataAdapter da = new SqlDataAdapter(strCmd, dataConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@CirculationDate", SqlDbType.DateTime).Value = Date;            

            DataTable dt = new DataTable();
            da.Fill(dt);

            DataTable DtAreas = GetAreaDetails();

            for (int i = 1; i < dt.Columns.Count; i++)
            {
                for (int j = 0; j < DtAreas.Rows.Count; j++)
                {
                    if (dt.Columns[i].ColumnName == DtAreas.Rows[j][0].ToString())
                    {
                        dt.Columns[i].ColumnName = DtAreas.Rows[j][1].ToString();
                        break;
                    }
                }
            }

            gvDailyData.DataSource = dt;
        }

        public DataTable GetAreaDetails()
        {
            string strCmd = "[spGetAreaDetails]";
            SqlCommand cmd = new SqlCommand(strCmd, dataConnection);
            SqlDataAdapter da = new SqlDataAdapter(strCmd, dataConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
