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
    public partial class CashDepositInBank : Form
    {
        SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["LeaderConn"].ConnectionString);
        public CashDepositInBank()
        {
            InitializeComponent();
            dtFromDate.Value = DateTime.Now.AddDays(-10);
            dtToDate.Value = DateTime.Now.AddDays(1);
            dtDepositDate.Value = DateTime.Now;
            this.gvAdListgrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            gvAdListgrid.DataSource = LoadCashListDetails(dtFromDate.Value, dtToDate.Value);
        }
        public DataTable LoadCashListDetails(DateTime StartDate, DateTime EndDate)
        {
            string strCmd = "spListCashDepositsInBank";
            SqlCommand cmd = new SqlCommand(strCmd, dataConnection);
            SqlDataAdapter da = new SqlDataAdapter(strCmd, dataConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@startdate", SqlDbType.DateTime).Value = StartDate;
            da.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime).Value = EndDate;

            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            gvAdListgrid.DataSource = LoadCashListDetails(dtFromDate.Value, dtToDate.Value);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string errorMsg = "";
            try
            {
                btnSave.Enabled = false;
                if (txtName.Text.Trim() == "")
                {
                    errorMsg = errorMsg + "Please enter Name\n";
                }
                if (txtAcctNo.Text.Trim() == "")
                {
                    errorMsg = errorMsg + "Please enter Account Number\n";
                }
                if (txtCash.Text.Trim() == "")
                {
                    errorMsg = errorMsg + "Please Enter cash deposit amount\n";
                }
                if (Convert.ToInt32(txtCash.Text) < 0)
                {
                    errorMsg = errorMsg + "Please Enter cash deposit amount\n";
                }
                if (errorMsg.Trim() != "")
                {
                    btnSave.Enabled = true;
                    MessageBox.Show(errorMsg);
                }
                else
                {
                    string ID = SaveData(dtDepositDate.Value, txtName.Text, txtAcctNo.Text, txtDescription.Text, Convert.ToInt32(txtCash.Text));
                    if (ID != "Fail")
                    {
                        MessageBox.Show("Cash deposit sucessfully saved with ID = " + ID);                        
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        MessageBox.Show("Error occurred while saving Cash deposit, Please check.");
                    }
                }
            }
            catch
            {
                btnSave.Enabled = true;
                MessageBox.Show(errorMsg);
            }
        }
        string SaveData(DateTime DepositeDate, string Name, string AccountNumber, string Description, int Amount)
        {
            try
            {
                //Insert the database with the record

                SqlCommand dataCommand = new SqlCommand();
                dataCommand.Connection = dataConnection;

                dataCommand.CommandText = ("spSaveCashDepositsInBankDetails");
                dataCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter ID = new SqlParameter("@CashDepositID", SqlDbType.Int, 100) { Direction = ParameterDirection.Output };

                dataCommand.Parameters.AddWithValue("@Name", Name);
                dataCommand.Parameters.AddWithValue("@Amount", Amount);
                dataCommand.Parameters.AddWithValue("@DepositDate", DepositeDate);
                dataCommand.Parameters.AddWithValue("@Description", Description);
                dataCommand.Parameters.AddWithValue("@AccountNumber", AccountNumber);

                dataCommand.Parameters.Add(ID);

                dataConnection.Open();
                dataCommand.ExecuteScalar();
                dataConnection.Close();
                return ID.Value.ToString();
            }
            catch (Exception ex)
            {
                dataConnection.Close();
                return "Fail";
            }
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(txtCash.Text) > 0)
                {
                    
                }
            }
            catch (Exception ex)
            {
                txtCash.Text = "";
                MessageBox.Show("Enter Cost in numbers");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtAcctNo.Text = "";
            txtDescription.Text = "";
            txtCash.Text = "";
        }
    }
}
