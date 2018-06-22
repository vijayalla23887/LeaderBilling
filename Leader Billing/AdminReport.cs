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
    public partial class AdminReport : Form
    {
        SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["LeaderConn"].ConnectionString);
        string ToAddress = ConfigurationManager.AppSettings["TO"];
        string CCAddress = ConfigurationManager.AppSettings["CC"];
        string Test = ConfigurationManager.AppSettings["Test"];
        public AdminReport()
        {
            InitializeComponent();
            dtFromDate.Value = DateTime.Now;
            dtToDate.Value = DateTime.Now.AddDays(1);
            gvCashDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            gvAdDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            gvPaymentDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

            gvCashDetails.DataSource = LoadCashListDetails(dtFromDate.Value, dtToDate.Value);
            gvAdDetails.DataSource = LoadAdListDetails(dtFromDate.Value, dtToDate.Value);
            gvPaymentDetails.DataSource = LoadPaymentListDetails(dtFromDate.Value, dtToDate.Value);
            if(gvAdDetails.Rows.Count>0)
                gvAdDetails.Rows[gvAdDetails.Rows.Count - 1].DefaultCellStyle.Font = new Font(gvAdDetails.Font, FontStyle.Bold);
            if (gvPaymentDetails.Rows.Count > 0)
                gvPaymentDetails.Rows[gvPaymentDetails.Rows.Count - 1].DefaultCellStyle.Font = new Font(gvPaymentDetails.Font, FontStyle.Bold);
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
        public DataTable LoadAdListDetails(DateTime StartDate, DateTime EndDate)
        {
            string strCmd = "spADListDetailsBySearch";
            SqlCommand cmd = new SqlCommand(strCmd, dataConnection);
            SqlDataAdapter da = new SqlDataAdapter(strCmd, dataConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@startdate", SqlDbType.DateTime).Value = StartDate;
            da.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime).Value = EndDate;            

            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                DataRow NewRow = dt.NewRow();
                NewRow["TotalADCost"] = dt.Compute("Sum(TotalADCost)", "");
                NewRow["ADCost"] = dt.Compute("Sum(ADCost)", "");
                NewRow["GstCost"] = dt.Compute("Sum(GstCost)", "");
                NewRow["TotalReceivedAmt"] = dt.Compute("Sum(TotalReceivedAmt)", "");
                NewRow["ReceivedGstAmount"] = dt.Compute("Sum(ReceivedGstAmount)", "");
                NewRow["BalanceAmt"] = dt.Compute("Sum(BalanceAmt)", "");
                NewRow["CommissionAmount"] = dt.Compute("Sum(CommissionAmount)", "");
                dt.Rows.Add(NewRow);
            }
            return dt;
        }
        public DataTable LoadPaymentListDetails(DateTime StartDate, DateTime EndDate)
        {
            string strCmd = "spAdPaymentListDetails";
            SqlCommand cmd = new SqlCommand(strCmd, dataConnection);
            SqlDataAdapter da = new SqlDataAdapter(strCmd, dataConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@startdate", SqlDbType.DateTime).Value = StartDate;
            da.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime).Value = EndDate;

            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                DataRow NewRow = dt.NewRow();
                NewRow["ReceivedAmount"] = dt.Compute("Sum(ReceivedAmount)", "");
                NewRow["CommissionAmount"] = dt.Compute("Sum(CommissionAmount)", "");
                dt.Rows.Add(NewRow);
            }
            return dt;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            gvCashDetails.DataSource = LoadCashListDetails(dtFromDate.Value, dtToDate.Value);
            gvAdDetails.DataSource = LoadAdListDetails(dtFromDate.Value, dtToDate.Value);
            gvPaymentDetails.DataSource = LoadPaymentListDetails(dtFromDate.Value, dtToDate.Value);
            if (gvAdDetails.Rows.Count > 0)
                gvAdDetails.Rows[gvAdDetails.Rows.Count - 1].DefaultCellStyle.Font = new Font(gvAdDetails.Font, FontStyle.Bold);
            if (gvPaymentDetails.Rows.Count > 0)
                gvPaymentDetails.Rows[gvPaymentDetails.Rows.Count - 1].DefaultCellStyle.Font = new Font(gvPaymentDetails.Font, FontStyle.Bold);
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            string From = "leadernewspaperbilling@gmail.com";
            
            string To = ToAddress;
            string CC = CCAddress;
            if (Test == "Test")
            {
                To = "vijay.jagadesh@gmail.com";
                CC = "vijay.jagadesh@gmail.com";
            }
            using (MailMessage mm = new MailMessage(From, To))
            {
                mm.Subject = "Leader Admin Report - " + dtFromDate.Value.ToString("dd/MM/yyyy") + " to "+ dtToDate.Value.ToString("dd/MM/yyyy");
                mm.Body = GetEmailBody();
                mm.Bcc.Add(new MailAddress("vijay.jagadesh@gmail.com")); 
                //foreach (string filePath in openFileDialog1.FileNames)
                //{
                //    if (File.Exists(filePath))
                //    {
                //        string fileName = Path.GetFileName(filePath);
                //        mm.Attachments.Add(new Attachment(filePath));
                //    }
                //}


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

        string GetEmailBody()
        {
            string mailbody = "<html><head><style>body {font-family: Lucida Sans Unicode, Lucida Grande, Sans-Serif; font-size: 14px; margin: 10px; padding: 0 0 5px;}\r\n";
            mailbody += ".table-data {margin: 1px 1px 0px 1px; border-collapse: collapse; border: 1px solid #69c;}\r\n";
            mailbody += ".table-data th {padding: 2px 1px 1px 1px; font-weight: normal; color: #039; }\r\n";
            mailbody += ".table-data td { padding: 5px; color: #669; border-top: 1px solid #69c; vertical-align: top; background-color: #e8edff;}\r\n";
            mailbody += "</style></head><body>\r\n<br /><br />";
            mailbody += "<div style='width: 600px; border-bottom: 2px solid #092668;'><img src='https://s15.postimg.cc/6769ylfrb/leader_logo.jpg' alt='Leader Telugu Daily Newspaper'></div><br /><br />";

            mailbody += "<b>Cash Deposits:</b><br />";
            mailbody += "<table class='table-data'>";
            mailbody += "<tr><th style='text-align: left; width: 50px;'>DepositID</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>DepositDate</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>Amount</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>Name</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>Description</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>AccountNumber</th>\r\n";
            mailbody += "</tr><tbody>" + "\r\n";

            foreach (DataRow dr in (gvCashDetails.DataSource as DataTable).Rows)
            {
                mailbody += "<tr>";
                mailbody += "<td style='text-align: left;'>" + dr["DepositID"] + "</td>";                
                mailbody += "<td style='text-align: left;'>" + dr["DepositDate"] + "</td>";
                mailbody += "<td style='text-align: left;'>" + dr["Amount"] + "</td>";
                mailbody += "<td style='text-align: left;'>" + dr["Name"] + "</td>";
                mailbody += "<td style='text-align: left;'>" + dr["Description"] + "</td>";
                mailbody += "<td style='text-align: left;'>" + dr["AccountNumber"] + "</td>";
                mailbody += "</tr>" + "\r\n";
            }
            mailbody += "</tbody></table>" + "\r\n";
            //ClientName	Address	ADdate	PaymentDate	PhoneNumber	TotalADCost	ReceivedAmount	CommissionAgent	ComAgtPercentage	CommissionAmount	PaymentCreateDate	Cheque

            mailbody += "<b>Payments:</b><br />";
            mailbody += "<table class='table-data'>";
            mailbody += "<tr><th style='text-align: left; width: 150px;'>ClientName</th>\r\n";
            mailbody += "<th style='text-align: left; width: 120px;'>Address</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>PublishedDate</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>PaymentDate</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>TotalADCost</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>ReceivedAmount</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>Cheque</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>CommissionAgent</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>ComAgtPercentage</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>CommissionAmount</th>\r\n";

            mailbody += "</tr><tbody>" + "\r\n";

            foreach (DataRow dr in (gvPaymentDetails.DataSource as DataTable).Rows)
            {
                mailbody += "<tr>";
                mailbody += "<td style='text-align: left;'>" + dr["ClientName"] + "</td>";
                mailbody += "<td style='text-align: left;'>" + dr["Address"] + "</td>";                
                mailbody += "<td style='text-align: left;'>" + dr["PublishedDate"] + "</td>";                
                mailbody += "<td style='text-align: left;'>" + dr["PaymentDate"] + "</td>";
                mailbody += "<td style='text-align: left;'>" + dr["TotalADCost"] + "</td>";
                mailbody += "<td style='text-align: left;'>" + dr["ReceivedAmount"] + "</td>";
                mailbody += "<td style='text-align: left;'>" + dr["Cheque"] + "</td>";
                mailbody += "<td style='text-align: left;'>" + dr["CommissionAgent"] + "</td>";
                mailbody += "<td style='text-align: center;'>" + dr["ComAgtPercentage"] + "</td>";
                mailbody += "<td style='text-align: left;'>" + dr["CommissionAmount"] + "</td>";

                mailbody += "</tr>" + "\r\n";
            }
            mailbody += "</tbody></table>" + "\r\n";

            mailbody += "<b>AD List:</b><br />";
            mailbody += "<table class='table-data'>";
            mailbody += "<tr><th style='text-align: left; width: 50px;'>ADID</th>\r\n";
            mailbody += "<th style='text-align: left; width: 120px;'>ClientName</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>Address</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>PublishedDate</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>TotalADCost</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>TotalReceivedAmt</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>BalanceAmt</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>CommissionAgent</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>ComAgtPercentage</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>CommissionAmount</th>\r\n";
            mailbody += "<th style='text-align: left; width: 300px;'>Description</th>\r\n";

            //ADID	ClientName Address	PublishedDate	TotalADCost	TotalReceivedAmt	BalanceAmt	ADCost	ReceivedADAmount	CommissionAgent	
            //CommissionPercentage	CommissionAmount	Description	PhoneNumber	GstCost	ReceivedGstAmount	CommissionAgentPhNumber	CreateDate

            mailbody += "</tr><tbody>" + "\r\n";

            foreach (DataRow dr in (gvAdDetails.DataSource as DataTable).Rows)
            {
                mailbody += "<tr>";
                mailbody += "<td style='text-align: left;'>" + dr["ADID"] + "</td>";
                mailbody += "<td style='text-align: left;'>" + dr["ClientName"] + "</td>";
                mailbody += "<td style='text-align: left;'>" + dr["Address"] + "</td>";                
                mailbody += "<td style='text-align: left;'>" + dr["PublishedDate"] + "</td>";
                mailbody += "<td style='text-align: left;'>" + dr["TotalADCost"] + "</td>";
                mailbody += "<td style='text-align: left;'>" + dr["TotalReceivedAmt"] + "</td>";
                mailbody += "<td style='text-align: left;'>" + dr["BalanceAmt"] + "</td>";
                mailbody += "<td style='text-align: left;'>" + dr["CommissionAgent"] + "</td>";
                mailbody += "<td style='text-align: center;'>" + dr["CommissionPercentage"] + "</td>";
                mailbody += "<td style='text-align: left;'>" + dr["CommissionAmount"] + "</td>";
                mailbody += "<td style='text-align: left;'>" + dr["Description"] + "</td>";

                mailbody += "</tr>" + "\r\n";
            }
            mailbody += "</tbody></table>" + "\r\n";

            return mailbody;
        }       

        private void btnShow_Click_1(object sender, EventArgs e)
        {
            gvCashDetails.DataSource = LoadCashListDetails(dtFromDate.Value, dtToDate.Value);
            gvAdDetails.DataSource = LoadAdListDetails(dtFromDate.Value, dtToDate.Value);
            gvPaymentDetails.DataSource = LoadPaymentListDetails(dtFromDate.Value, dtToDate.Value);
            if (gvAdDetails.Rows.Count > 0)
                gvAdDetails.Rows[gvAdDetails.Rows.Count - 1].DefaultCellStyle.Font = new Font(gvAdDetails.Font, FontStyle.Bold);
            if (gvPaymentDetails.Rows.Count > 0)
                gvPaymentDetails.Rows[gvPaymentDetails.Rows.Count - 1].DefaultCellStyle.Font = new Font(gvPaymentDetails.Font, FontStyle.Bold);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendOutstandingEmails();
        }
        public void SendOutstandingEmails()
        {
            DateTime StartDate = DateTime.Now; DateTime EndDate = DateTime.Now.AddDays(1);
            string From = "leadernewspaperbilling@gmail.com";
            string To = ToAddress;
            string CC = CCAddress;
            if (Test == "Test")
            {
                To = "vijay.jagadesh@gmail.com";
                CC = "vijay.jagadesh@gmail.com";
            }

            using (MailMessage mm = new MailMessage(From, To))
            {
                mm.Subject = "Leader Outstanding Bills till " + DateTime.Today.AddDays(-30).ToString("dd/MM/yyyy");
                mm.Body = GetOutstandingEmailBody();
                mm.CC.Add(new MailAddress(CC));
                mm.Bcc.Add(new MailAddress("vijay.jagadesh@gmail.com"));

                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("leadernewspaperbilling@gmail.com", "Media@321");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;

                smtp.Port = 587;
                smtp.Send(mm);
                MessageBox.Show("Email has been sent to " + To);
            }
        }
        public DataTable LoadOutStandingADDetails()
        {
            string strCmd = "spGetOutstandingADs";
            SqlDataAdapter da = new SqlDataAdapter(strCmd, dataConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        string GetOutstandingEmailBody()
        {
            string mailbody = "<html><head><style>body {font-family: Lucida Sans Unicode, Lucida Grande, Sans-Serif; font-size: 14px; margin: 10px; padding: 0 0 5px;}\r\n";
            mailbody += ".table-data {margin: 1px 1px 0px 1px; border-collapse: collapse; border: 1px solid #69c;}\r\n";
            mailbody += ".table-data th {padding: 2px 1px 1px 1px; font-weight: normal; color: #039; }\r\n";
            mailbody += ".table-data td { padding: 5px; color: #669; border-top: 1px solid #69c; vertical-align: top; background-color: #e8edff;}\r\n";
            mailbody += "</style></head><body>\r\n<br /><br />";
            mailbody += "<div style='width: 600px; border-bottom: 2px solid #092668;'><img src='https://s15.postimg.cc/6769ylfrb/leader_logo.jpg' alt='Leader Telugu Daily Newspaper'></div><br /><br />";

            //ADID ClientName PublishedDate TotalADCost BalanceAmt CommissionAgent CommissionAgentPhNumber                                                                                      CommissionAgentPhNumber Addate

            mailbody += "<b>Outstanding AD's List:</b><br />";
            mailbody += "<table class='table-data'>";
            mailbody += "<tr><th style='text-align: left; width: 50px;'>ADID</th>\r\n";
            mailbody += "<th style='text-align: left; width: 120px;'>ClientName</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>PublishedDate</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>TotalADCost</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>BalanceAmt</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>CommissionAgent</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>CommissionAgentPhNumber</th>\r\n";

            mailbody += "</tr><tbody>" + "\r\n";

            foreach (DataRow dr in LoadOutStandingADDetails().Rows)
            {
                mailbody += "<tr>";
                mailbody += "<td style='text-align: left;'>" + dr["ADID"] + "</td>";
                mailbody += "<td style='text-align: left;'>" + dr["ClientName"] + "</td>";
                mailbody += "<td style='text-align: left;'>" + dr["PublishedDate"] + "</td>";
                mailbody += "<td style='text-align: left;'>" + dr["TotalADCost"] + "</td>";
                mailbody += "<td style='text-align: left;'>" + dr["BalanceAmt"] + "</td>";
                mailbody += "<td style='text-align: left;'>" + dr["CommissionAgent"] + "</td>";
                mailbody += "<td style='text-align: left;'>" + dr["CommissionAgentPhNumber"] + "</td>";

                mailbody += "</tr>" + "\r\n";
            }
            mailbody += "</tbody></table></body ></html >";

            return mailbody;
        }
    }
}
