using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AcademyKadem
{
    public partial class Joueurs : UserControl
    {
        public Joueurs()
        {
            InitializeComponent();
        }
        ADO d = new ADO();
        private void Joueurs_Load(object sender, EventArgs e)
        {
            d.Connecter();
        }


        bool Check()
        {
            if (txtNom.Text.Trim() == "" || txtNomPere.Text.Trim() == "" || txtPrenom.Text.Trim() == "" || txtAdresse.Text.Trim() == "" || txtPrenomPere.Text.Trim() == "" || txtNumTele.Text.Trim() == "")
                return false;
            return true;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            int checkAssur = 0;
            int checkInscr = 0;
            //try
            //{
                try
                {
                    if (Check())
                    {
                        if (checkAssurence.Checked)
                            checkAssur = 1;
                        if (checkInscription.Checked)
                            checkInscr = 1;
                        byte[] tabimg = null;
                        FileStream fs = new FileStream(chemin, FileMode.Open);
                        BinaryReader br = new BinaryReader(fs);
                        tabimg = br.ReadBytes((int)fs.Length);
                        fs.Close();
                        d.cmd.CommandText = "insert into Joueurs values (CAST (year('" + dateNaiss.Value + "') as varchar(4))+CAST(MONTH('" + dateNaiss.Value + "') as varchar(2))+CAST(day('" + dateNaiss.Value + "') as varchar(2))+CAST((NEXT VALUE FOR SJOUEURS)as varchar(MAX)),'" + txtNom.Text + "','" + txtPrenom.Text + "','" + dateNaiss.Value + "','" + dateInscr.Value + "','" + comboGenre.Text[0].ToString() + "','" + txtAdresse.Text + "',@img," + checkAssur + "," + checkInscr + "," + comboMensualité.Text + ",'" + txtNomPere.Text + "','" + txtPrenomPere.Text + "','" + txtNumTele.Text + "','" + comboType.Text + "','" + comboCat.Text + "')";
                        d.cmd.Parameters.Clear();
                        d.cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@img", tabimg));
                        d.cmd.Connection = d.cnx;
                        d.cmd.ExecuteNonQuery();
                        d.cmd.CommandText = "select ID from Joueurs where Nom = '" + txtNom.Text + "' and Prénom= '" + txtPrenom.Text + "' and DateNaissance ='" + dateNaiss.Value + "'";
                        d.cmd.Connection = d.cnx;
                        lblID.Visible = true;
                        lblIDValue.Text = (string)d.cmd.ExecuteScalar();
                        timer1.Enabled = true;
                    }
                    else
                        MessageBox.Show(@"Remplir tout les champs /!\ ", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                catch
                {
                    d.cmd.CommandText = "insert into Joueurs values (CAST (year('" + dateNaiss.Value + "') as varchar(4))+CAST(MONTH('" + dateNaiss.Value + "') as varchar(2))+CAST(day('" + dateNaiss.Value + "') as varchar(2))+CAST((NEXT VALUE FOR SJOUEURS)as varchar(max)),'" + txtNom.Text + "','" + txtPrenom.Text + "','" + dateNaiss.Value + "','" + dateInscr.Value + "','" + comboGenre.Text[0].ToString() + "','" + txtAdresse.Text + "',NULL," + checkAssur + "," + checkInscr + "," + comboMensualité.Text + ",'" + txtNomPere.Text + "','" + txtPrenomPere.Text + "','" + txtNumTele.Text + "','" + comboType.Text + "','" + comboCat.Text + "')";
                    d.cmd.Connection = d.cnx;
                    d.cmd.ExecuteNonQuery();
                    d.cmd.CommandText = "select ID from Joueurs where Nom = '" + txtNom.Text + "' and Prénom= '" + txtPrenom.Text + "' and DateNaissance ='" + dateNaiss.Value + "'";
                    d.cmd.Connection = d.cnx;
                    lblID.Visible = true;
                    lblIDValue.Text = ((string)d.cmd.ExecuteScalar()).ToString();
                    timer1.Enabled = true;
                }
            //}
            //catch(Exception ex )
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        string chemin;
        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            of.Title = "selectionnez votre image";
            if (of.ShowDialog() == DialogResult.OK)
            {
                chemin = of.FileName;
                FileStream fs = new FileStream(chemin, FileMode.Open);   
                guna2CirclePictureBox1.Image = Image.FromStream(fs);
                fs.Close();
            }
        }

        void Vider()
        {
            txtNom.Clear();
            txtPrenom.Clear();
            txtAdresse.Clear();
            txtNomPere.Clear();
            txtPrenomPere.Clear();
            txtNumTele.Clear();
            guna2CirclePictureBox1.Image = null; 
        }

        
        int indice = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            indice++;
            if (indice == 60)
            {
                lblID.Visible = false;
                lblIDValue.Text = "";
                timer1.Enabled = false;
            }
        }

        private void checkInscription_Click(object sender, EventArgs e)
        {
            if (checkInscription.Checked)
                checkAssurence.Checked = true;
            else
                checkAssurence.Checked = false;
        }
    }
}