using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Leader
{
    public partial class ADMainPage : Form
    {
        public ADMainPage()
        {
            InitializeComponent();
        }

        private void btnAdEntryForm_Click(object sender, EventArgs e)
        {
            ADEntryForm obj = new ADEntryForm();
            obj.Show(this);
        }

        private void btnAdDetailsList_Click(object sender, EventArgs e)
        {
            ADDetailstListBySearch obj = new ADDetailstListBySearch();
            obj.Show(this);
        }

        private void btnAdPaymentsList_Click(object sender, EventArgs e)
        {
            ADPaymentsList obj = new ADPaymentsList();
            obj.Show(this);
        }

        private void btnAdCommissionList_Click(object sender, EventArgs e)
        {
            ADDetailstListBySearch obj = new ADDetailstListBySearch();
            obj.Show(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CashDepositInBank obj = new CashDepositInBank();
            obj.Show(this);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AdminReport obj = new AdminReport();
            obj.Show(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CommisionAgentEntry obj = new CommisionAgentEntry();
            obj.Show(this);
        }
    }
}
