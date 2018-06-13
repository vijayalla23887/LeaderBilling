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
    public partial class CirculationMainPage : Form
    {
        public CirculationMainPage()
        {
            InitializeComponent();
        }

        private void btnAAreaEntryandList_Click(object sender, EventArgs e)
        {
            AreaDetailsEntry obj = new AreaDetailsEntry();
            obj.Show(this);
        }

        private void btnDailyCirculationEntryForm_Click(object sender, EventArgs e)
        {
            DailyCirculationEntry obj = new DailyCirculationEntry();
            obj.Show(this);
        }

        private void btnCirculationReport_Click(object sender, EventArgs e)
        {
            
        }
    }
}
