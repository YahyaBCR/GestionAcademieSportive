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
    public partial class Charges : UserControl
    {
        public Charges()
        {
            InitializeComponent();
        }
        ADO d = new ADO();
        private void Charges_Load(object sender, EventArgs e)
        {
            d.Connecter();
            d.cmd.CommandText = "select * from Mois";
            d.cmd.Connection = d.cnx;
            d.dr = d.cmd.ExecuteReader();
            d.dt.Load(d.dr);
            guna2ComboBox1.DataSource = d.dt;
            guna2ComboBox1.DisplayMember = "Mois";
            guna2ComboBox1.ValueMember = "Mois";
            d.dr.Close();
            guna2ComboBox1.StartIndex = 0;
        }

        void Remplir(string mois)
        {
            DataTable dttt = new DataTable();
            dttt.Clear();
            d.cmd.CommandText = "select FraisInternet,FraisMateriele,FraisTransport,FraisEntraineurs,Autre from Charges where Mois = '" + mois + "' ";
            d.cmd.Connection = d.cnx;
            d.dr = d.cmd.ExecuteReader();
            dttt.Load(d.dr);
            lblInternet.Text = dttt.Rows[0][0].ToString();
            lblMateriel.Text = dttt.Rows[0][1].ToString();
            lblTransport.Text = dttt.Rows[0][2].ToString();
            lblEntraineur.Text = dttt.Rows[0][3].ToString();
            lblAutre.Text = dttt.Rows[0][4].ToString();
            //txtInternet.Text = dttt.Rows[0][0].ToString();
            //txtMateriele.Text = dttt.Rows[0][1].ToString();
            //txtTransport.Text = dttt.Rows[0][2].ToString();
            //txtEntrainet.Text = dttt.Rows[0][3].ToString();
            //txtAutre.Text = dttt.Rows[0][4].ToString();
        }
        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Remplir(guna2ComboBox1.Text);
            }
            catch { }


        }
        
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Vous avez besoin de confirmer pour effectuer cette opération","Question",MessageBoxButtons.OKCancel, MessageBoxIcon.Question) ==DialogResult.OK)
            {
                try
                {
                    d.cmd.CommandText = "update Charges set FraisEntraineurs= FraisEntraineurs + " + txtEntrainet.Text + " where Mois = '" + guna2ComboBox1.Text + "'";
                    d.cmd.Connection = d.cnx;
                    d.cmd.ExecuteNonQuery();
                    Remplir(guna2ComboBox1.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Vous avez besoin de confirmer pour effectuer cette opération", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    d.cmd.CommandText = "update Charges set FraisMateriele= FraisMateriele+ " + txtMateriele.Text + " where Mois = '" + guna2ComboBox1.Text + "'";
                    d.cmd.Connection = d.cnx;
                    d.cmd.ExecuteNonQuery();
                    Remplir(guna2ComboBox1.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Vous avez besoin de confirmer pour effectuer cette opération", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    d.cmd.CommandText = "update Charges set FraisInternet=FraisInternet+ " + txtInternet.Text + " where Mois = '" + guna2ComboBox1.Text + "'";
                    d.cmd.Connection = d.cnx;
                    d.cmd.ExecuteNonQuery();
                    Remplir(guna2ComboBox1.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
            
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Vous avez besoin de confirmer pour effectuer cette opération", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    d.cmd.CommandText = "update Charges set FraisTransport=FraisTransport+ " + txtTransport.Text + " where Mois = '" + guna2ComboBox1.Text + "'";
                    d.cmd.Connection = d.cnx;
                    d.cmd.ExecuteNonQuery();
                    Remplir(guna2ComboBox1.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Vous avez besoin de confirmer pour effectuer cette opération", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    d.cmd.CommandText = "update Charges set Autre=Autre+ " + txtAutre.Text + " where Mois = '" + guna2ComboBox1.Text + "'";
                    d.cmd.Connection = d.cnx;
                    d.cmd.ExecuteNonQuery();
                    Remplir(guna2ComboBox1.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }
    }
}
