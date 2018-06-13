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
    public partial class PaperInvoiceMainPage : Form
    {
        public PaperInvoiceMainPage()
        {
            InitializeComponent();
        }

        private void JobEntry_Click(object sender, EventArgs e)
        {
            JobDataEntry obj = new JobDataEntry();
            obj.Show(this);
        }

        private void StockDetails_Click(object sender, EventArgs e)
        {
            StockDetails obj = new StockDetails();
            obj.Show(this);
        }

        private void InvoiceStockEntry_Click(object sender, EventArgs e)
        {
            InvoiceEntry obj = new InvoiceEntry();
            obj.Show(this);
        }

        private void btnInvoiceEntryDetails_Click(object sender, EventArgs e)
        {
            InvoiceEntryDetails obj = new InvoiceEntryDetails();
            obj.Show(this);
        }

        private void btnJobEntryDetails_Click(object sender, EventArgs e)
        {
            JobEntryDetails obj = new JobEntryDetails();
            obj.Show(this);
        }
    }
}
