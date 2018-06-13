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
    public partial class JobDataEntry : Form
    {
        SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["LeaderConn"].ConnectionString);
        DataTable Dt;
        public JobDataEntry()
        {
            InitializeComponent();
            LoadStockDetails();
            LoadPaperDesignTypes();
        }
        public void LoadStockDetails()
        {
            dataConnection.Open();
            string strCmd = "spStockDetails";
            SqlCommand cmd = new SqlCommand(strCmd, dataConnection);
            SqlDataAdapter da = new SqlDataAdapter(strCmd, dataConnection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmd.ExecuteNonQuery();
            dataConnection.Close();
            gvCountgrid.DataSource = ds.Tables[0];
            Dt = ds.Tables[0];
            gvCountgrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        public void LoadPaperDesignTypes()
        {
            dataConnection.Open();
            string strCmd = "select ID, DesignType from PaperDesignTypes";
            SqlCommand cmd = new SqlCommand(strCmd, dataConnection);
            SqlDataAdapter da = new SqlDataAdapter(strCmd, dataConnection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmd.ExecuteNonQuery();
            dataConnection.Close();
            cbPaperDesignType.DisplayMember = "DesignType";
            cbPaperDesignType.ValueMember = "ID";
            cbPaperDesignType.DataSource = ds.Tables[0];
        }
        private void cbPaperDesignType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbGSMNumber.DataSource = null;
            dataConnection.Open();
            string strCmd = "select IndentityID, PaperType from GSMNumbers where ID = " + cbPaperDesignType.SelectedValue;
            SqlCommand cmd = new SqlCommand(strCmd, dataConnection);
            SqlDataAdapter da = new SqlDataAdapter(strCmd, dataConnection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmd.ExecuteNonQuery();
            dataConnection.Close();
            cbGSMNumber.DisplayMember = "PaperType";
            cbGSMNumber.ValueMember = "IndentityID";
            cbGSMNumber.DataSource = ds.Tables[0];
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string errorMsg = "";
            try
            {
                btnSave.Enabled = false;
                if (dtJobDate.Value > DateTime.Now)
                {
                    errorMsg = errorMsg + "Job Date should be Less than Today\n";
                }
                if (Convert.ToInt32(cbPaperDesignType.SelectedValue) < 1)
                {
                    errorMsg = errorMsg + "Select Paper Design Type \n";
                }
                if (Convert.ToInt32(cbGSMNumber.SelectedValue) < 1)
                {
                    errorMsg = errorMsg + "Select GSM Number \n";
                }
                if (Convert.ToInt32(txtQuantity.Text) < 1)
                {
                    errorMsg = errorMsg + "Enter Paper Qantity/Sheets \n";
                }
                if (txtDescription.Text.Trim() == "")
                {
                    errorMsg = errorMsg + "Please enter Description\n";
                }
                if (errorMsg.Trim() != "")
                {
                    btnSave.Enabled = true;
                    MessageBox.Show(errorMsg);
                }
                else
                {
                    int Quantity =  GetTotalStockValue(Convert.ToInt32(cbPaperDesignType.SelectedValue), Convert.ToInt32(cbGSMNumber.SelectedValue));
                    if (Quantity > Convert.ToInt32(txtQuantity.Text))
                    {
                        string ID = SaveData(dtJobDate.Value, txtDescription.Text, Convert.ToInt32(cbPaperDesignType.SelectedValue),
                        Convert.ToInt32(cbGSMNumber.SelectedValue), Convert.ToInt32(txtQuantity.Text));
                        if (ID != "Fail")
                        {
                            txtJobNumber.Text = ID;
                            LoadStockDetails();
                            MessageBox.Show("Invoice sucessfully saved with ID = " + ID);
                        }
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        MessageBox.Show("Enter Quantity is More/Not available, Please check.");
                    }
                }
            }
            catch (Exception ex)
            {
                btnSave.Enabled = true;
                errorMsg = errorMsg + "Enter Paper Qantity/Sheets \n";
                MessageBox.Show(errorMsg);
            }
        }
        int GetTotalStockValue(int DesignTypeID, int PaperTypeID)
        {
            try
            {
                //Insert the database with the record

                SqlCommand dataCommand = new SqlCommand();
                dataCommand.Connection = dataConnection;

                dataCommand.CommandText = ("spGetPaperStockQuantity");
                dataCommand.CommandType = CommandType.StoredProcedure;

                dataCommand.Parameters.AddWithValue("@DesignTypeID", DesignTypeID);
                dataCommand.Parameters.AddWithValue("@PaperTypeID", PaperTypeID);
                SqlParameter returnParameter = dataCommand.Parameters.Add("@Quantity", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                                               
                dataConnection.Open();
                dataCommand.ExecuteNonQuery();
                int id = (int)returnParameter.Value;
                dataConnection.Close();
                return id;
            }
            catch (Exception ex)
            {
                dataConnection.Close();
                return -1;
            }
        }
        string SaveData(DateTime InvoiceDate, string Description, int DesignTypeID, int PaperTypeID, int Quantity)
        {
            try
            {
                //Insert the database with the record

                SqlCommand dataCommand = new SqlCommand();
                dataCommand.Connection = dataConnection;

                dataCommand.CommandText = ("spSavePaperJobDetails");
                dataCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter ID = new SqlParameter("@ID", SqlDbType.Int, 100) { Direction = ParameterDirection.Output };

                dataCommand.Parameters.AddWithValue("@JobDate", InvoiceDate);
                dataCommand.Parameters.AddWithValue("@Description", Description);
                dataCommand.Parameters.AddWithValue("@DesignTypeID", DesignTypeID);
                dataCommand.Parameters.AddWithValue("@PaperTypeID", PaperTypeID);
                dataCommand.Parameters.AddWithValue("@Quantity", Quantity);

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
    }
}
