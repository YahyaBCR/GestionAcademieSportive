using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AcademyKadem
{
    public partial class Reçu : Form
    {
        public Reçu()
        {
            InitializeComponent();
        }
        int i = -1;
        private void timer1_Tick(object sender, EventArgs e)
        {
            string academy = "Academy kadem sport";
            i += 1;
            try
            {
                guna2HtmlLabel1.Text += academy[i].ToString();
            }
            catch { }
            
            if (guna2HtmlLabel1.Text == "Academy kadem")
            {
                pic2.Visible = true;
            }
            if (guna2HtmlLabel1.Text == "Academy")
            {
                pic1.Visible = true;
            }
            if (guna2HtmlLabel1.Text == academy)
            {
                pic3.Visible = true;
            }
            if (i == 25)
            {
                pic4.Visible = true;
            }
            if (i == 30)
            {
                this.Hide();
                Login login = new Login();
                login.Show();
            }
        }

        private void Reçu_Load(object sender, EventArgs e)
        {

        }
    }
}
