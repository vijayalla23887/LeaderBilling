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
    public partial class ADPaymentEntry : Form
    {
        SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["LeaderConn"].ConnectionString);
        int balanceAmt = 0;
        string PhFromConfig = ConfigurationManager.AppSettings["LeaderPhonenumberAndEmail"];
        string GSTNum = ConfigurationManager.AppSettings["GSTNum"];
        public ADPaymentEntry()
        {
            InitializeComponent();
        }
        public ADPaymentEntry(int ID)
        {
            InitializeComponent();
            dtPaymentDate.Value = DateTime.Now;
            getBill(ID);
            DataGridViewLinkColumn link = new DataGridViewLinkColumn();
            link.UseColumnTextForLinkValue = true;
            link.HeaderText = "Click to View Details";
            link.DataPropertyName = "lnkColumn";
            link.ActiveLinkColor = Color.White;
            link.LinkBehavior = LinkBehavior.SystemDefault;
            link.LinkColor = Color.Blue;
            link.Text = "View Details";
            link.VisitedLinkColor = Color.YellowGreen;
            gvAdPaymentgrid.Columns.Add(link);
            getPaymentDetails(ID);
            lblGstNumber.Text = "GST Number :   " + GSTNum;
            lblLeaderPh.Text = PhFromConfig;
        }
        public void getBill(int ID)
        {
            string strCmd = "[spADEntryPaymentDetails]";
            SqlCommand cmd = new SqlCommand(strCmd, dataConnection);
            SqlDataAdapter da = new SqlDataAdapter(strCmd, dataConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@ADID", SqlDbType.Int).Value = ID;

            DataSet ds = new DataSet();
            da.Fill(ds);

            lblName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
            lblAdDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["AdDate"]).ToShortDateString(); 
            lblClentAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
            lblCost.Text = ds.Tables[0].Rows[0]["TotalADCost"].ToString();
            lblPhoneNumber.Text = ds.Tables[0].Rows[0]["PhoneNumber"].ToString();
            lblDescription.Text = ds.Tables[0].Rows[0]["description"].ToString();
            lblBalanceAmt.Text = "Received Amount: " + ds.Tables[0].Rows[0]["TotalPaidAmt"].ToString() + "\nBalance Amount: " + ds.Tables[0].Rows[0]["BalanceAmt"].ToString();
            balanceAmt = Convert.ToInt32(ds.Tables[0].Rows[0]["BalanceAmt"]);
            txtClientGstNumber.Text = ds.Tables[0].Rows[0]["ClientGstNo"].ToString();

            if (balanceAmt == 0)
            {
                btnSave.Enabled = false;
            }
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
        public void getPaymentDetails(int ID)
        {
            string strCmd = "[spGetADEntryPaymentDetails]";
            SqlCommand cmd = new SqlCommand(strCmd, dataConnection);
            SqlDataAdapter da = new SqlDataAdapter(strCmd, dataConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@ADID", SqlDbType.Int).Value = ID;
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvAdPaymentgrid.DataSource = dt;
            if(dt.Rows.Count > 0)
                lblBalanceAmt.Text = "Received Amount: " + dt.Compute("Sum(TotalAmount)", "").ToString() +
                                "\nBalance Amount: " + (Convert.ToInt64(lblCost.Text)-Convert.ToInt64(dt.Compute("Sum(TotalAmount)", "").ToString())).ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string errorMsg = "";
            try
            {
                btnSave.Enabled = false;
                if (lblName.Text.Trim() == "")
                {
                    errorMsg = errorMsg + "Please enter Name\n";
                }
                if (lblADID.Text == "")
                {
                    errorMsg = errorMsg + "Please select ID\n";
                }
                if (Convert.ToInt32(txtPayAmt.Text) < 0)
                {
                    errorMsg = errorMsg + "Enter Paid amount for AD\n";
                }
                if (btnSave.Text == "Save")
                {
                    if (Convert.ToInt32(txtPayAmt.Text) > balanceAmt)
                    {
                        errorMsg = errorMsg + "Entered Pay amount is greater than Balance amount\n";
                    }
                }
                if (errorMsg.Trim() != "")
                {
                    btnSave.Enabled = true;
                    MessageBox.Show(errorMsg);
                }
                else
                {
                    if (lblADID.Text != "")
                    {
                        if (btnSave.Text == "Save")
                        {
                            string ID = SaveData(Convert.ToInt32(lblADID.Text), dtPaymentDate.Value, Convert.ToInt32(txtAdAmt.Text), Convert.ToInt32(txtGstAmt.Text),
                            Convert.ToInt32(txtPayAmt.Text), txtClientGstNumber.Text, txtCheque.Text, txtBankName.Text,rbCmAmtPaidYes.Checked,chkTDS.Checked);
                            if (ID != "Fail")
                            {
                                lblPaymentID.Text = ID;
                                getPaymentDetails(Convert.ToInt32(lblADID.Text));
                                MessageBox.Show("Payment sucessfully saved with Payment ID = " + ID);
                                dtPaymentDate.Enabled = false;
                                txtPayAmt.ReadOnly = true;
                            }
                            else
                            {
                                btnSave.Enabled = true;
                                MessageBox.Show("Error occurred while saving, Please check.");
                            }
                        }
                        else
                        {
                            string ID = UpdateData(Convert.ToInt32(lblADID.Text), Convert.ToInt32(lblPaymentID.Text), dtPaymentDate.Value, Convert.ToInt32(txtAdAmt.Text), Convert.ToInt32(txtGstAmt.Text),
                            Convert.ToInt32(txtPayAmt.Text), txtClientGstNumber.Text, txtCheque.Text, txtBankName.Text, rbCmAmtPaidYes.Checked, chkTDS.Checked);
                            if (ID != "Fail")
                            {
                                getPaymentDetails(Convert.ToInt32(lblADID.Text));
                                MessageBox.Show("Payment sucessfully Updated with Payment ID = " + lblPaymentID.Text);
                                dtPaymentDate.Enabled = false;
                                txtPayAmt.ReadOnly = true;
                            }
                            else
                            {
                                btnSave.Enabled = true;
                                MessageBox.Show("Error occurred while updating, Please check.");
                            }
                        }
                    }                    
                }
            }
            catch (Exception ex)
            {
                btnSave.Enabled = true;
                errorMsg = errorMsg + "Enter Pay amount for AD \n";
                MessageBox.Show(errorMsg);
            }
        }

        string SaveData(int AdID,DateTime PaymentDate, int ADAmount, int GstAmount, int TotalAmount, string ClientGstNumber, string Cheque, 
            string BankName, bool CMPaid, bool TDSDeducted)
        {
            try
            {
                SqlCommand dataCommand = new SqlCommand();
                dataCommand.Connection = dataConnection;

                dataCommand.CommandText = ("spSaveADPaymentDetails");
                dataCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter ID = new SqlParameter("@PaymentID", SqlDbType.Int, 100) { Direction = ParameterDirection.Output };
                dataCommand.Parameters.AddWithValue("@ADID", AdID);
                dataCommand.Parameters.AddWithValue("@PaymentDate", PaymentDate);
                dataCommand.Parameters.AddWithValue("@ADAmount", ADAmount);
                dataCommand.Parameters.AddWithValue("@GstAmount", GstAmount);
                dataCommand.Parameters.AddWithValue("@TotalAmount", TotalAmount);
                dataCommand.Parameters.AddWithValue("@ClientGstNumber", ClientGstNumber);
                dataCommand.Parameters.AddWithValue("@Cheque", Cheque);
                dataCommand.Parameters.AddWithValue("@BankName", BankName);
                dataCommand.Parameters.AddWithValue("@CommissionAmtPaid", CMPaid);
                dataCommand.Parameters.AddWithValue("@TDSDeducted", TDSDeducted);

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

        string UpdateData(int AdID, int PaymentID, DateTime PaymentDate, int ADAmount, int GstAmount, int TotalAmount, string ClientGstNumber, 
            string Cheque, string BankName, bool CMPaid, bool TDSDeducted)
        {
            try
            {
                SqlCommand dataCommand = new SqlCommand();
                dataCommand.Connection = dataConnection;

                dataCommand.CommandText = ("spUpdateADPaymentDetails");
                dataCommand.CommandType = CommandType.StoredProcedure;
                dataCommand.Parameters.AddWithValue("@ADID", AdID);
                dataCommand.Parameters.AddWithValue("@PaymentID", PaymentID);
                dataCommand.Parameters.AddWithValue("@PaymentDate", PaymentDate);
                dataCommand.Parameters.AddWithValue("@ADAmount", ADAmount);
                dataCommand.Parameters.AddWithValue("@GstAmount", GstAmount);
                dataCommand.Parameters.AddWithValue("@TotalAmount", TotalAmount);
                dataCommand.Parameters.AddWithValue("@ClientGstNumber", ClientGstNumber);
                dataCommand.Parameters.AddWithValue("@Cheque", Cheque);
                dataCommand.Parameters.AddWithValue("@BankName", BankName);
                dataCommand.Parameters.AddWithValue("@CommissionAmtPaid", CMPaid);
                dataCommand.Parameters.AddWithValue("@TDSDeducted", TDSDeducted);
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

        private void txtPayAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(txtPayAmt.Text) > 0)
                {
                    txtAdAmt.Text = (Math.Round(Convert.ToDouble(txtPayAmt.Text) / (1.05))).ToString();
                    txtGstAmt.Text = Math.Round((Convert.ToDouble(txtPayAmt.Text) / (1.05)) * (0.05)).ToString();
                    lblRupees.Text = "RUPEES : " + ConvertNumbertoWords(Convert.ToInt64(txtPayAmt.Text)) + " ONLY";
                }
            }
            catch (Exception ex)
            {
                txtGstAmt.Text = "";
                txtAdAmt.Text = "";
                txtPayAmt.Text = "";
                MessageBox.Show("Enter Amount in Numbers");
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(txtPayAmt.Text) > 0 && Convert.ToInt64(lblPaymentID.Text) > 0)
                {
                    try
                    {
                        btnSave.Visible = false; btnPrint.Visible = false; btnClose.Visible = false;
                        PaymentReceiptReport obj = new PaymentReceiptReport(Convert.ToInt32(lblADID.Text), Convert.ToInt32(lblPaymentID.Text));
                        obj.Show();
                        btnSave.Visible = true; btnPrint.Visible = true; btnClose.Visible = true; 
                    }
                    catch
                    {
                        MessageBox.Show("Close the Window and restart");
                        btnPrint.Visible = true; btnClose.Visible = true; groupBox4.Visible = true; gvAdPaymentgrid.Visible = true;
                    }
                }
                else
                {
                    MessageBox.Show("Enter Paid Amount.");
                }
            }
            catch
            {
                MessageBox.Show("Enter Paid Amount.");
            }
        }
        
        private void gvAdPaymentgrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != gvAdPaymentgrid.RowCount)
            {
                //Check whether user click on the first column 
                if (e.ColumnIndex == 0 && e.RowIndex != -1)
                {
                    int row;
                    //Get the row index
                    row = e.RowIndex;
                    //Create instance of the form2 class
                    lblPaymentID.Text = gvAdPaymentgrid.Rows[row].Cells["PaymentID"].Value.ToString();
                    txtPayAmt.Text = gvAdPaymentgrid.Rows[row].Cells["TotalAmount"].Value.ToString();
                    txtGstAmt.Text = gvAdPaymentgrid.Rows[row].Cells["GstAmount"].Value.ToString();
                    txtAdAmt.Text = gvAdPaymentgrid.Rows[row].Cells["ADAmount"].Value.ToString();
                    dtPaymentDate.Value = Convert.ToDateTime(gvAdPaymentgrid.Rows[row].Cells["PaymentDate"].Value);
                    txtClientGstNumber.Text = gvAdPaymentgrid.Rows[row].Cells["ClientGstNumber"].Value.ToString();
                    txtCheque.Text = gvAdPaymentgrid.Rows[row].Cells["Cheque"].Value.ToString();
                    txtBankName.Text = gvAdPaymentgrid.Rows[row].Cells["BankName"].Value.ToString();
                    rbCmAmtPaidYes.Checked = Convert.ToBoolean(gvAdPaymentgrid.Rows[row].Cells["CommissionAmtPaid"].Value);
                    chkTDS.Checked = Convert.ToBoolean(gvAdPaymentgrid.Rows[row].Cells["TDSDeducted"].Value);
                    if (rbCmAmtPaidYes.Checked == true)
                    {
                        rbCmAmtPaidYes.Enabled = false;
                        rbCmAmtPaidNo.Enabled = false;
                    }

                    btnSave.Text = "Update";
                    btnSave.Enabled = true;
                    dtPaymentDate.Enabled = true;
                }
            }
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            if (lblPaymentID.Text != "" && Convert.ToInt32(lblPaymentID.Text) > 0)
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
                        mm.Subject = "Leader Cash Receipt No - " + lblPaymentID.Text;
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
                MessageBox.Show("Payment ID not selected or Not created.");
            }
        }

        public DataTable getBillDate(int ID, int PaymentID)
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
        string GetEmailBody()
        {
            string mailbody = "<html><head><style>body {font-family: Lucida Sans Unicode, Lucida Grande, Sans-Serif; font-size: 14px; margin: 10px; padding: 0 0 5px;}\r\n";
            mailbody += "</style></head><body>\r\n<br /><br />";
            mailbody += "<div style='width: 600px; border-bottom: 2px solid #092668;'><img src='https://s15.postimg.cc/6769ylfrb/leader_logo.jpg' alt='Leader Telugu Daily Newspaper'></div><br /><br />";

            mailbody += "<b>Dear " + lblName.Text + "</b><br /><br />";
            mailbody += "Please find the attached cash receipt of advertisement that is published in Leader Telugu Daily Newspaper.";
            mailbody += "<br /><br /><b>Warm Regards,<br/>Leader Telugu Daily Newspaper.</b>";
            mailbody += "<br/></body></html>";
            return mailbody;
        }
        private string SavePDF()
        {
            try
            {
                ReportDocument rpt = new ReportDocument();
                rpt.Load(@"C:\Vijju\Leader\Reports\CashReport.rpt");

                rpt.SetDataSource(getBillDate(Convert.ToInt32(lblADID.Text),Convert.ToInt32(lblPaymentID.Text)));

                ExportOptions rptExportOption;
                DiskFileDestinationOptions rptFileDestOption = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions rptFormatOption = new PdfRtfWordFormatOptions();
                string reportFileName = @"C:\Vijju\Leader\PDF Reports\Cash Receipt\Cash Receipt No - " + lblPaymentID.Text + ".pdf";
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
    }
}
