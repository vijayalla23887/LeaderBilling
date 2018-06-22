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
    public partial class CommisionAgentEntry : Form
    {
        SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["LeaderConn"].ConnectionString);

        public CommisionAgentEntry()
        {
            InitializeComponent();
            DataGridViewLinkColumn link = new DataGridViewLinkColumn();
            link.UseColumnTextForLinkValue = true;
            link.HeaderText = "Click to View Details";
            link.DataPropertyName = "lnkColumn";
            link.ActiveLinkColor = Color.White;
            link.LinkBehavior = LinkBehavior.SystemDefault;
            link.LinkColor = Color.Blue;
            link.Text = "Edit Agent";
            link.VisitedLinkColor = Color.YellowGreen;
            gvAdListgrid.Columns.Add(link);

            gvAdListgrid.DataSource = LoadAgents();
            this.gvAdListgrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }
        public DataTable LoadAgents()
        {
            string strCmd = "spGetAgents";
            SqlCommand cmd = new SqlCommand(strCmd, dataConnection);
            SqlDataAdapter da = new SqlDataAdapter(strCmd, dataConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
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
                if (txtPhNo.Text.Trim() == "")
                {
                    errorMsg = errorMsg + "Please enter Phone Number\n";
                }
                if (errorMsg.Trim() != "")
                {
                    btnSave.Enabled = true;
                    MessageBox.Show(errorMsg);
                }
                else
                {
                    if (lblID.Text == "")
                    {
                        string ID = SaveData(txtName.Text, txtPhNo.Text);
                        if (ID != "Fail")
                        {
                            gvAdListgrid.DataSource = LoadAgents();
                            MessageBox.Show("Agent sucessfully saved with ID = " + ID);
                        }
                        else
                        {
                            btnSave.Enabled = true;
                            MessageBox.Show("Error occurred while saving Agent, Please check.");
                        }
                    }
                    else
                    {
                        string ID = UpdateData(txtName.Text, txtPhNo.Text, Convert.ToInt32(lblID.Text));
                        if (ID != "Fail")
                        {
                            gvAdListgrid.DataSource = LoadAgents();
                            MessageBox.Show("Agent sucessfully Updated with Agent ID = " + lblID.Text);
                        }
                        else
                        {
                            btnSave.Enabled = true;
                            MessageBox.Show("Error occurred while updating Agent, Please check.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                btnSave.Enabled = true;
                MessageBox.Show(errorMsg + " " + ex.Message);
            }
        }
        string SaveData(string Name, string PhoneNo)
        {
            try
            {
                //Insert the database with the record
                SqlCommand dataCommand = new SqlCommand();
                dataCommand.Connection = dataConnection;

                dataCommand.CommandText = ("spSaveAgents");
                dataCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter ID = new SqlParameter("@AgentID", SqlDbType.Int, 100) { Direction = ParameterDirection.Output };

                dataCommand.Parameters.AddWithValue("@Name", Name);
                dataCommand.Parameters.AddWithValue("@PhoneNumber", PhoneNo);
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
        string UpdateData(string Name, string PhoneNo, int AgentID)
        {
            try
            {
                //Insert the database with the record
                SqlCommand dataCommand = new SqlCommand();
                dataCommand.Connection = dataConnection;

                dataCommand.CommandText = ("spUpdateAgents");
                dataCommand.CommandType = CommandType.StoredProcedure;

                dataCommand.Parameters.AddWithValue("@AgentID", AgentID);
                dataCommand.Parameters.AddWithValue("@Name", Name);
                dataCommand.Parameters.AddWithValue("@PhoneNumber", PhoneNo);

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

        private void gvAdListgrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Check whether user click on the first column 
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                btnSave.Text = "Update";
                int row = e.RowIndex;
                if (gvAdListgrid.Rows[row].Cells["AgentID"].Value.ToString() == "")
                    return;
                txtName.Text = gvAdListgrid.Rows[row].Cells["AgentName"].Value.ToString();
                txtPhNo.Text = gvAdListgrid.Rows[row].Cells["PhoneNumber"].Value.ToString();
                lblID.Text = gvAdListgrid.Rows[row].Cells["AgentID"].Value.ToString();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtPhNo.Text = "";
            lblID.Text = "";
            btnSave.Text = "Save";
            btnSave.Enabled = true;
        }
    }
}
