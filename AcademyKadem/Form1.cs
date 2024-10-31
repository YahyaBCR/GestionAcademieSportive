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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard();
            addUserControl(dash);
        }
        ADO d = new ADO();
        public void addUserControl(UserControl uc)
        {
            guna2Panel1.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            uc.BringToFront();
            guna2Panel1.Controls.Add(uc);
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            addUserControl(dashboard);
        }

        private void guna2Button9_Click_1(object sender, EventArgs e)
        {
            Entraineur et = new Entraineur();
            addUserControl(et);
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            Joueurs joueurs = new Joueurs();
            addUserControl(joueurs);
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            SearchJoueurs searchJoueurs = new SearchJoueurs();
            addUserControl(searchJoueurs);
        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            PayéMois p = new PayéMois();
            addUserControl(p);
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            Catégorie cat = new Catégorie();
            addUserControl(cat);
        }

        private void guna2Button14_Click(object sender, EventArgs e)
        {
            Charges charges = new Charges();
            addUserControl(charges);
        }

        private void guna2Button15_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            UpdateJr updateJr = new UpdateJr();
            addUserControl(updateJr);
        }
    }
}