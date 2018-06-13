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
    public partial class AreaDetailsEntry : Form
    {
        SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["LeaderConn"].ConnectionString);
        public AreaDetailsEntry()
        {
            InitializeComponent();
            DataGridViewLinkColumn link = new DataGridViewLinkColumn();
            link.UseColumnTextForLinkValue = true;
            link.HeaderText = "Click to View Details";
            link.DataPropertyName = "lnkColumn";
            link.ActiveLinkColor = Color.White;
            link.LinkBehavior = LinkBehavior.SystemDefault;
            link.LinkColor = Color.Blue;
            link.Text = "View Details";
            link.VisitedLinkColor = Color.YellowGreen;
            gvAreaGrid.Columns.Add(link);

            this.gvAreaGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            LoadAreas();
        }

        public void LoadAreas()
        {
            string strCmd = "spGetAreas";
            SqlCommand cmd = new SqlCommand(strCmd, dataConnection);
            SqlDataAdapter da = new SqlDataAdapter(strCmd, dataConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            da.Fill(dt);
            gvAreaGrid.DataSource = dt;
        }

        private void gvAreaGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Check whether user click on the first column 
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                int row;
                //Get the row index
                row = e.RowIndex;
                btnSave.Text = "Update";
                txtContactPhNumber.Text = gvAreaGrid.Rows[row].Cells["PhoneNumber"].Value.ToString();
                txtAreaName.Text = gvAreaGrid.Rows[row].Cells["AreaName"].Value.ToString();
                txtContactName.Text = gvAreaGrid.Rows[row].Cells["ContactName"].Value.ToString();
                lblADID.Text = gvAreaGrid.Rows[row].Cells["ID"].Value.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string errorMsg = "";
            try
            {
                btnSave.Enabled = false;
                if (txtAreaName.Text.Trim() == "")
                {
                    errorMsg = errorMsg + "Please enter Area Name\n";
                }
                if (txtContactName.Text.Trim() == "")
                {
                    errorMsg = errorMsg + "Please enter Contact Name\n";
                }
                if (txtContactPhNumber.Text.Trim() == "" || Convert.ToInt64(txtContactPhNumber.Text) < 0)
                {
                    errorMsg = errorMsg + "Please enter Contact Phone Number\n";
                }
                if (errorMsg.Trim() != "")
                {
                    btnSave.Enabled = true;
                    MessageBox.Show(errorMsg);
                }
                else
                {
                    if (lblADID.Text == "")
                    {
                        string ID = SaveData(txtAreaName.Text, txtContactName.Text, txtContactPhNumber.Text);
                        if (ID != "Fail")
                        {
                            lblADID.Text = ID;
                            LoadAreas();
                            MessageBox.Show("Area sucessfully saved with ID = " + ID);
                        }
                        else
                        {
                            btnSave.Enabled = true;
                            MessageBox.Show("Error occurred while saving, Please check.");
                        }
                    }
                    else
                    {
                        string ID = UpdateData(Convert.ToInt32(lblADID.Text),txtAreaName.Text, txtContactName.Text, txtContactPhNumber.Text);
                        if (ID != "Fail")
                        {
                            LoadAreas();
                            MessageBox.Show("Area sucessfully updated with ID = " + lblADID.Text);
                        }
                        else
                        {
                            btnSave.Enabled = true;
                            MessageBox.Show("Error occurred while updating, Please check.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                btnSave.Enabled = true;
                errorMsg = errorMsg + "Check Phone number\n";
                MessageBox.Show(errorMsg);
            }
        }

        string SaveData(string AreaName, string ContactName, string PhoneNumber)
        {
            try
            {
                SqlCommand dataCommand = new SqlCommand();
                dataCommand.Connection = dataConnection;

                dataCommand.CommandText = ("[spSaveAreaDetails]");
                dataCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter ID = new SqlParameter("@ID", SqlDbType.Int, 100) { Direction = ParameterDirection.Output };

                dataCommand.Parameters.AddWithValue("@Area", AreaName);
                dataCommand.Parameters.AddWithValue("@ContactName", ContactName);
                dataCommand.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);

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
        string UpdateData(int ID, string AreaName, string ContactName, string PhoneNumber)
        {
            try
            {
                //Insert the database with the record

                SqlCommand dataCommand = new SqlCommand();
                dataCommand.Connection = dataConnection;

                dataCommand.CommandText = ("[spUpdateAreaDetails]");
                dataCommand.CommandType = CommandType.StoredProcedure;

                dataCommand.Parameters.AddWithValue("@ID", ID);
                dataCommand.Parameters.AddWithValue("@Area", AreaName);
                dataCommand.Parameters.AddWithValue("@ContactName", ContactName);
                dataCommand.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);

                dataConnection.Open();
                dataCommand.ExecuteScalar();
                dataConnection.Close();
                return "True";
            }
            catch (Exception ex)
            {
                dataConnection.Close();
                return "Fail";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtContactPhNumber.Text = "";
            txtAreaName.Text = "";
            txtContactName.Text = "";
            lblADID.Text = "";
            btnSave.Text = "Save";
            btnSave.Enabled = true;
        }
    }
}
