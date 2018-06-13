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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if((txtLogin.Text=="Madhavi" && txtPassword.Text=="Media@321") || (txtLogin.Text == "Murthy" && txtPassword.Text == "Lea@321"))
            {
                ADMainPage obj = new ADMainPage();
                obj.Show(this);
            }
            else
            {
                MessageBox.Show("Login ID or Password is wrong. Please check");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtLogin.Text = "";
            txtPassword.Text = "";
        }
    }
}
