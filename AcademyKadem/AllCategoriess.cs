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
    public partial class AllCategoriess : Form
    {
        public AllCategoriess()
        {
            InitializeComponent();
        }
        string categorie;

        public AllCategoriess(string cat)
        {
            InitializeComponent();
            this.categorie = cat;    

        }
        ADO d = new ADO();

        void Remplir(string cat,string type)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            d.cmd.CommandText = "select Nom,Prénom,DateInscription 'Date inscription ',TypeSport ' Type de sport',Mensualité,FraisAssurence 'Frais Assurence',FraisInscription 'Frais Inscription' from Joueurs where Categorie = '" + cat+"' and TypeSport = '"+comboType.Text+"'";
            d.cmd.Connection = d.cnx;
            d.dr = d.cmd.ExecuteReader();
            dt.Load(d.dr);
            guna2DataGridView1.DataSource = dt;
            d.dr.Close();
            lblTotal.Text = "le nombre de joueur au Catégorie " + guna2ComboBox1.Text + "  est  :" + guna2DataGridView1.Rows.Count;
        }

        private void AllCategoriess_Load(object sender, EventArgs e)
        {
            d.Connecter();
            guna2ComboBox1.Text = this.categorie;
            Remplir(this.categorie,comboType.Text);
            lblTotal.Text = "le nombre de joueur au Catégorie "+guna2ComboBox1.Text+"  est  :" + guna2DataGridView1.Rows.Count;
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Remplir(guna2ComboBox1.Text,comboType.Text);
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtt = new DataTable();
                dtt.Clear();
                d.cmd.CommandText = "select Nom, Prénom, DateInscription 'Date inscription ',TypeSport ' Type de sport',Mensualité,FraisAssurence 'Frais Assurence',FraisInscription 'Frais Inscription' from Joueurs where Categorie = '" + guna2ComboBox1.Text + "' and  TypeSport = '"+comboType.Text+"' AND Nom like '" + txtNom.Text + "%' ";
                d.cmd.Connection = d.cnx;
                d.dr = d.cmd.ExecuteReader();
                dtt.Load(d.dr);
                guna2DataGridView1.DataSource = dtt;
                d.dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtType = new DataTable();
            dtType.Clear();
            d.cmd.CommandText = "select Nom, Prénom, DateInscription 'Date inscription ',TypeSport ' Type de sport',Mensualité,FraisAssurence 'Frais Assurence',FraisInscription 'Frais Inscription' from Joueurs where Categorie = '" + guna2ComboBox1.Text + "' and TypeSport = '" + comboType.Text + "'";
            d.cmd.Connection = d.cnx;
            d.dr = d.cmd.ExecuteReader();
            dtType.Load(d.dr);
            guna2DataGridView1.DataSource = dtType;
        }
    }
}