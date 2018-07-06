using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Leader
{
    public partial class ReportByChart : Form
    {
        SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["LeaderConn"].ConnectionString);
        string ToAddress = ConfigurationManager.AppSettings["TO"];
        string CCAddress = ConfigurationManager.AppSettings["CC"];
        string Test = ConfigurationManager.AppSettings["Test"];
        string InvoiceFolder = ConfigurationManager.AppSettings["InvoiceFolder"];

        public ReportByChart()
        {
            InitializeComponent();
            DataTable dt = LoadOutStandingADDetailsByAgent(); 
            chart1.DataSource = dt;
            chart1.Series["Balance Amount"].XValueMember = "CommissionAgent";
            chart1.Series["Balance Amount"].YValueMembers = "Balance Amount";
            //chart1.Series["Total Amount"].YValueMembers = "TotalAmount";
            chart1.Titles.Add("Balance Amount By Commission Agent");
            chart1.Series["Balance Amount"]["PixelPointWidth"] = "40";
            gvAmtDetails.DataSource = dt;
            gvAmtDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            gvOutstandingGrid.DataSource = LoadOutStandingADDetails();
            gvOutstandingGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
        }

        public DataTable LoadOutStandingADDetailsByAgent()
        {
            string strCmd = "spGetOutstandingADs_CommissionAgentChart";
            SqlDataAdapter da = new SqlDataAdapter(strCmd, dataConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
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
                //chart1.SaveImage(InvoiceFolder + "\\Chart.Png", ImageFormat.Png);
                //mm.Attachments.Add(new Attachment(InvoiceFolder + "\\Chart.Png"));
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

            mailbody += "<b>Outstanding AD's List By Agent:</b><br />";
            mailbody += "<table class='table-data'>";
            mailbody += "<tr><th style='text-align: left; width: 250px;'>CommissionAgent</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>TotalAmount</th>\r\n";
            mailbody += "<th style='text-align: left; width: 100px;'>Balance Amount</th>\r\n";
            mailbody += "</tr><tbody>" + "\r\n";

            foreach (DataRow dr in LoadOutStandingADDetailsByAgent().Rows)
            {
                mailbody += "<tr>";
                mailbody += "<td style='text-align: left;'>" + dr["CommissionAgent"] + "</td>";
                mailbody += "<td style='text-align: left;'>" + dr["TotalAmount"] + "</td>";
                mailbody += "<td style='text-align: left;'>" + dr["Balance Amount"] + "</td>";
                mailbody += "</tr>" + "\r\n";
            }

            mailbody += "</tbody></table>" + "\r\n";
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

        private void button1_Click(object sender, EventArgs e)
        {
            SendOutstandingEmails();
        }
    }
}
