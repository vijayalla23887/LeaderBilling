using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Leader
{
    public partial class ADEntryForm : Form
    {
        SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["LeaderConn"].ConnectionString);
        string PhFromConfig = ConfigurationManager.AppSettings["LeaderPhonenumberAndEmail"];
        string GSTNum = ConfigurationManager.AppSettings["GSTNum"];
        public ADEntryForm()
        {
            InitializeComponent();
            dtADDate.Value = DateTime.Today.AddDays(1);
            lblLeaderPh.Text = PhFromConfig;
            txtDescription.Text = "EDITION             :   VSP\nPAGE                  : \nSIZE (W x H)      : \nSq.Cm                 : ";
            cbAgents.DataSource = LoadAgents();
            cbAgents.ValueMember = "PhoneNumber";
            cbAgents.DisplayMember = "AgentName";
            cbAgents.SelectedIndex = cbAgents.FindStringExact("<Select Agent>");
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
        public string ConvertNumbertoWords(long number)
        {
            if (number == 0) return "ZERO";
            if (number < 0) return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 100000) + " LAKES ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            //if ((number / 10) > 0)  
            //{  
            // words += ConvertNumbertoWords(number / 10) + " RUPEES ";  
            // number %= 10;  
            //}  
            if (number > 0)
            {
                if (words != "") words += "AND ";
                var unitsMap = new[]
                {
            "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"
        };
                var tensMap = new[]
                {
            "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"
        };
                if (number < 20) words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0) words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }
        public ADEntryForm(int ID)
        {
            InitializeComponent();
            getBill(ID);
            lblLeaderPh.Text = PhFromConfig;
            btnPrint.Enabled = true;
            btnPayment.Enabled = true;
            btnInvoice.Enabled = true;
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
                if (txtAddress.Text.Trim() == "")
                {
                    errorMsg = errorMsg + "Please enter Address\n";
                }
                if (txtDescription.Text.Trim() == "")
                {
                    errorMsg = errorMsg + "Please enter Description\n";
                }
                if (txtPhoneNumber.Text.Trim() == "" || Convert.ToInt64(txtPhoneNumber.Text) < 0)
                {
                    errorMsg = errorMsg + "Enter Phone number\n";
                }
                if (Convert.ToInt32(txtAdCost.Text) < 0)
                {
                    errorMsg = errorMsg + "Enter cost for AD\n";
                }
                if (cbAgents.SelectedIndex == 0)
                {
                    errorMsg = errorMsg + "Select Commission Agent\n";
                }
                if (txtCommisionPercentage.Text != "")
                {
                    if (Convert.ToInt32(txtCommisionPercentage.Text) < 0)
                    {
                        errorMsg = errorMsg + "Commision Percentage can not be Negative\n";
                    }
                }
                else
                {
                    txtCommisionPercentage.Text = "0";
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
                        string ID = SaveData(dtADDate.Value, txtName.Text, txtAddress.Text, txtDescription.Text, txtPhoneNumber.Text, Convert.ToInt32(txtAdCost.Text),
                            Convert.ToInt32(txtGSTCost.Text), Convert.ToInt32(txtTotalCost.Text), cbAgents.Text, txtAgentPhNumber.Text, Convert.ToInt32(txtCommisionPercentage.Text));
                        if (ID != "Fail")
                        {
                            lblADID.Text = ID;
                            MessageBox.Show("Advertisement sucessfully saved with ID = " + ID);
                            btnPrint.Enabled = true;
                            btnPayment.Enabled = true;
                            btnInvoice.Enabled = true;
                        }
                        else
                        {
                            btnSave.Enabled = true;
                            MessageBox.Show("Error occurred while saving, Please check.");
                        }
                    }
                    else
                    {
                        string ID = UpdateData(Convert.ToInt32(lblADID.Text), dtADDate.Value, txtName.Text, txtAddress.Text, txtDescription.Text, txtPhoneNumber.Text, Convert.ToInt32(txtAdCost.Text),
                            Convert.ToInt32(txtGSTCost.Text), Convert.ToInt32(txtTotalCost.Text), cbAgents.Text, txtAgentPhNumber.Text, Convert.ToInt32(txtCommisionPercentage.Text));
                        if (ID != "Fail")
                        {
                            MessageBox.Show("Advertisement sucessfully updated with ID = " + lblADID.Text);
                            btnPrint.Enabled = true;
                            btnPayment.Enabled = true;
                            btnInvoice.Enabled = true;
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
                errorMsg = errorMsg + "Check Phone number or Cost for AD or Commission percentage \n";
                MessageBox.Show(errorMsg);
            }
        }

        public void getBill(int ID)
        {
            string strCmd = "[spADEntryPaymentDetails]";
            SqlCommand cmd = new SqlCommand(strCmd, dataConnection);
            SqlDataAdapter da = new SqlDataAdapter(strCmd, dataConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@ADID", SqlDbType.Int).Value = ID;

            DataSet ds = new DataSet();
            da.Fill(ds);

            txtName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
            dtADDate.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["AdDate"]);
            txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
            txtPhoneNumber.Text = ds.Tables[0].Rows[0]["PhoneNumber"].ToString();
            txtDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
            txtAdCost.Text = ds.Tables[0].Rows[0]["ADCost"].ToString();
            txtGSTCost.Text = ds.Tables[0].Rows[0]["GstCost"].ToString();
            txtTotalCost.Text = ds.Tables[0].Rows[0]["TotalADCost"].ToString();
            txtAgentPhNumber.Text = ds.Tables[0].Rows[0]["CommissionAgentPhNumber"].ToString();
            //txtAgentName.Text = ds.Tables[0].Rows[0]["CommissionAgent"].ToString();
            txtCommisionPercentage.Text = ds.Tables[0].Rows[0]["CommissionPercentage"].ToString();
            lblBalanceAmt.Text = "Amount Paid: " + ds.Tables[0].Rows[0]["TotalPaidAmt"].ToString() + "\nBalance Amount: " + ds.Tables[0].Rows[0]["BalanceAmt"].ToString();
            txtClientGstNo.Text = ds.Tables[0].Rows[0]["ClientGstNo"].ToString();
            cbAgents.DataSource = LoadAgents();
            cbAgents.ValueMember = "PhoneNumber";
            cbAgents.DisplayMember = "AgentName";
            cbAgents.SelectedIndex = cbAgents.FindStringExact(ds.Tables[0].Rows[0]["CommissionAgent"].ToString());
        }

        string SaveData(DateTime AdDate, string Name, string Address, string Description, string PhoneNumber, int ADCost, int GstCost, int TotalCost,
            string CommissionAgent, string CommissionAgentPhNumber, int CommisionPercentage)
        {
            try
            {
                //Insert the database with the record

                SqlCommand dataCommand = new SqlCommand();
                dataCommand.Connection = dataConnection;

                dataCommand.CommandText = ("spSaveADBillDataDetails");
                dataCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter ID = new SqlParameter("@ADID", SqlDbType.Int, 100) { Direction = ParameterDirection.Output };

                dataCommand.Parameters.AddWithValue("@Name", Name);
                dataCommand.Parameters.AddWithValue("@AdDate", AdDate);
                dataCommand.Parameters.AddWithValue("@Address", Address);
                dataCommand.Parameters.AddWithValue("@Description", Description);
                dataCommand.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                dataCommand.Parameters.AddWithValue("@ADCost", ADCost);
                dataCommand.Parameters.AddWithValue("@GstCost", GstCost);
                dataCommand.Parameters.AddWithValue("@TotalCost", TotalCost);
                dataCommand.Parameters.AddWithValue("@CommissionAgent", CommissionAgent);
                dataCommand.Parameters.AddWithValue("@CommissionAgentPhNumber", CommissionAgentPhNumber);
                dataCommand.Parameters.AddWithValue("@CommisionPercentage", CommisionPercentage);
                dataCommand.Parameters.AddWithValue("@ClientGstNo", txtClientGstNo.Text);

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

        string UpdateData(int ID, DateTime AdDate, string Name, string Address, string Description, string PhoneNumber, int ADCost, int GstCost, int TotalCost,
            string CommissionAgent, string CommissionAgentPhNumber, int CommisionPercentage)
        {
            try
            {
                //Insert the database with the record

                SqlCommand dataCommand = new SqlCommand();
                dataCommand.Connection = dataConnection;

                dataCommand.CommandText = ("spUpdateADBillDataDetails");
                dataCommand.CommandType = CommandType.StoredProcedure;

                dataCommand.Parameters.AddWithValue("@ADID", ID);
                dataCommand.Parameters.AddWithValue("@Name", Name);
                dataCommand.Parameters.AddWithValue("@AdDate", AdDate);
                dataCommand.Parameters.AddWithValue("@Address", Address);
                dataCommand.Parameters.AddWithValue("@Description", Description);
                dataCommand.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                dataCommand.Parameters.AddWithValue("@ADCost", ADCost);
                dataCommand.Parameters.AddWithValue("@GstCost", GstCost);
                dataCommand.Parameters.AddWithValue("@TotalCost", TotalCost);
                dataCommand.Parameters.AddWithValue("@CommissionAgent", CommissionAgent);
                dataCommand.Parameters.AddWithValue("@CommissionAgentPhNumber", CommissionAgentPhNumber);
                dataCommand.Parameters.AddWithValue("@CommisionPercentage", CommisionPercentage);
                dataCommand.Parameters.AddWithValue("@ClientGstNo", txtClientGstNo.Text);

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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtAdCost_TextChanged(object sender, EventArgs e)
        {
            string errorMsg;
            try
            {
                if (Convert.ToDouble(txtAdCost.Text) > 0)
                {
                    txtGSTCost.Text = Math.Round(Convert.ToDouble(txtAdCost.Text) * (0.05)).ToString();
                    txtTotalCost.Text = Math.Round(Convert.ToDouble(txtAdCost.Text) + Convert.ToDouble(txtAdCost.Text) * (0.05)).ToString();
                    lblRupees.Text = "RUPEES : " + ConvertNumbertoWords(Convert.ToInt64(txtTotalCost.Text)) + " ONLY";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter Cost in numbers");
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ROFormReport obj = new ROFormReport(Convert.ToInt32(lblADID.Text));
            obj.Show();
        }
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern long BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
        private Bitmap memoryImage;

        private void PrintScreen()
        {
            Graphics mygraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, mygraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            IntPtr dc1 = mygraphics.GetHdc();
            IntPtr dc2 = memoryGraphics.GetHdc();
            BitBlt(dc2, 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height, dc1, 0, 0, 13369376);
            mygraphics.ReleaseHdc(dc1);
            memoryGraphics.ReleaseHdc(dc2);
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            ADPaymentEntry obj = new ADPaymentEntry(Convert.ToInt32(lblADID.Text));
            //Bind the datagridview values in the second popup form
            obj.Controls["lblADID"].Text = lblADID.Text;
            obj.ShowDialog();
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            BillFormReport obj = new BillFormReport(Convert.ToInt32(lblADID.Text));
            obj.Show();
        }

        private void cbAgents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAgents.SelectedValue != null && cbAgents.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                txtAgentPhNumber.Text = cbAgents.SelectedValue.ToString();
            }
        }
        public DataTable getBillDate(int ID)
        {
            string strCmd = "[spADEntryPaymentDetails]";
            //SqlCommand cmd = new SqlCommand(strCmd, dataConnection);
            SqlDataAdapter da = new SqlDataAdapter(strCmd, dataConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@ADID", SqlDbType.Int).Value = ID;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        string GetEmailBody()
        {
            string mailbody = "<html><head><style>body {font-family: Lucida Sans Unicode, Lucida Grande, Sans-Serif; font-size: 14px; margin: 10px; padding: 0 0 5px;}\r\n";
            mailbody += "</style></head><body>\r\n<br /><br />";
            mailbody += "<div style='width: 600px; border-bottom: 2px solid #092668;'><img src='https://s15.postimg.cc/6769ylfrb/leader_logo.jpg' alt='Leader Telugu Daily Newspaper'></div><br /><br />";

            mailbody += "<b>Dear " + txtName.Text + "</b><br /><br />";
            mailbody += "Please find the attached tax invoice of advertisement that is published in Leader Telugu Daily Newspaper.";
            mailbody += "<br /><br /><b>Warm Regards,<br/>Leader Telugu Daily Newspaper.</b>";
            mailbody += "<br/></body></html>";
            return mailbody;
        }
        private string SavePDF()
        {
            try
            {
                ReportDocument rpt = new ReportDocument();
                rpt.Load(@"C:\Vijju\Leader\TaxInvoice.rpt");

                rpt.SetDataSource(getBillDate(Convert.ToInt32(lblADID.Text)));

                ExportOptions rptExportOption;
                DiskFileDestinationOptions rptFileDestOption = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions rptFormatOption = new PdfRtfWordFormatOptions();
                string reportFileName = @"C:\Vijju\Leader\PDF Reports\Invoice\Invoice No - " + lblADID.Text + ".pdf";
                rptFileDestOption.DiskFileName = reportFileName;
                rptExportOption = rpt.ExportOptions;
                {
                    rptExportOption.ExportDestinationType = ExportDestinationType.DiskFile;
                    //if we want to generate the report as PDF, change the ExportFormatType as "ExportFormatType.PortableDocFormat"
                    //if we want to generate the report as Excel, change the ExportFormatType as "ExportFormatType.Excel"
                    rptExportOption.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rptExportOption.ExportDestinationOptions = rptFileDestOption;
                    rptExportOption.ExportFormatOptions = rptFormatOption;
                }
                rpt.Export();
                return reportFileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while saving PDF.");
                return "Error";
            }
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            if (lblADID.Text != "" && Convert.ToInt32(lblADID.Text) > 0)
            {
                if (txtEmail.Text == "")
                {
                    MessageBox.Show("Email ID not entered.");
                }
                else
                {
                    string From = "leadernewspaperbilling@gmail.com";
                    //string To = "leaderramanamurthy@gmail.com";
                    string To = txtEmail.Text;

                    using (MailMessage mm = new MailMessage(From, To))
                    {
                        mm.Subject = "Leader Tax Invoice No - " + lblADID.Text;
                        mm.Body = GetEmailBody();
                        mm.Bcc.Add(new MailAddress("vijay.jagadesh@gmail.com"));

                        mm.Attachments.Add(new Attachment(SavePDF()));
                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential("leadernewspaperbilling@gmail.com", "Media@321");
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;

                        smtp.Port = 587;
                        smtp.Send(mm);
                        MessageBox.Show("Email has been sent to " + To, "Message");
                    }
                }
            }
            else
            {
                MessageBox.Show("AD not selected or Not created.");
            }
        }
    }
}
