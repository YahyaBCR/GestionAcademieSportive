using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AcademyKadem
{
    public partial class UpdateJr : UserControl
    {
        public UpdateJr()
        {
            InitializeComponent();
        }
        ADO d = new ADO();

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtID.Text == "")
                {
                    Vider();
                    return;
                }
                d.cmd.CommandText = "select * from Joueurs where ID like '%" + txtID.Text + "'";
                d.cmd.Connection = d.cnx;
                d.dr = d.cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(d.dr);
                if (dt.Rows.Count > 0)
                {
                    lblID.Visible = true;
                    lblIDValue.Text = dt.Rows[0][0].ToString();
                    txtNom.Text = dt.Rows[0][1].ToString();
                    txtPrenom.Text = dt.Rows[0][2].ToString();
                    dateNaiss.Value = Convert.ToDateTime(dt.Rows[0][3].ToString());
                    dateInscr.Value= Convert.ToDateTime(dt.Rows[0][4].ToString());
                    comboGenre.Text = dt.Rows[0][5].ToString() == "H" ? "Homme" : "Femme";
                    txtAdresse.Text = dt.Rows[0][6].ToString();
                    try
                    {
                        MemoryStream ms = new MemoryStream((byte[])dt.Rows[0][7]);
                        guna2CirclePictureBox1.Image = new Bitmap(ms);
                    }
                    catch { }
                    checkAssurence.Checked = Convert.ToBoolean(dt.Rows[0][8].ToString());
                    checkInscription.Checked = Convert.ToBoolean(dt.Rows[0][9].ToString());
                    comboMensualité.Text = dt.Rows[0][10].ToString();
                    txtNomPere.Text = dt.Rows[0][11].ToString();
                    txtPrenomPere.Text = dt.Rows[0][12].ToString();
                    txtNumTele.Text = dt.Rows[0][13].ToString();
                    comboType.Text = dt.Rows[0][14].ToString();
                    comboCat.Text = dt.Rows[0][15].ToString();
                    dt.Rows.Clear();
                }
                else
                    Vider();

            }
            catch { }
        }

        private void Vider()
        {
            
        }

        private void UpdateJr_Load(object sender, EventArgs e)
        {
            d.Connecter();
        }
        string chemin;
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            int checkAssur = 0;
            int checkInscr = 0;
            if (checkAssurence.Checked)
                checkAssur = 1;
            if (checkInscription.Checked)
                checkInscr = 1;
            try
            {
                try
                {
                    
                    byte[] tabimg = null;
                    FileStream fs = new FileStream(chemin, FileMode.Open);
                    BinaryReader br = new BinaryReader(fs);
                    tabimg = br.ReadBytes((int)fs.Length);
                    fs.Close();
                    d.cmd.CommandText = "update Joueurs set Nom = '" + txtNom.Text + "'," +
                        "Prénom = '" + txtPrenom.Text + "',DateNaissance = '" + dateNaiss.Value + "',DateInscription ='" + dateInscr.Value + "'," +
                        "Genre = '" + comboGenre.Text[0].ToString() + "',Adresse = '" + txtAdresse.Text + "',Image = @img,FraisAssurence = " + checkAssur + "," +
                        "FraisInscription = " + checkInscr + ",Mensualité = " + comboMensualité.Text + ",NomTuteur = '" + txtNomPere.Text + "'," +
                        "PrenomTuteur ='" + txtPrenomPere.Text + "',TelTuteur = '" + txtNumTele.Text + "',TypeSport = '" + comboType.Text + "'," +
                        "Categorie = '" + comboCat.Text + "' where ID = '" + lblIDValue.Text + "'";
                    d.cmd.Parameters.Clear();
                    d.cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@img", tabimg));
                    d.cmd.Connection = d.cnx;
                    d.cmd.Connection = d.cnx;
                    d.cmd.ExecuteNonQuery();
                    MessageBox.Show("La modifcation a éffectuer avec succée", "Academy Kadem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    d.cmd.CommandText = "update Joueurs set Nom = '" + txtNom.Text + "'," +
                        "Prénom = '" + txtPrenom.Text + "',DateNaissance = '" + dateNaiss.Value + "',DateInscription ='" + dateInscr.Value + "'," +
                        "Genre = '" + comboGenre.Text[0].ToString() + "',Adresse = '" + txtAdresse.Text + "',FraisAssurence = " + checkAssur + "," +
                        "FraisInscription = " + checkInscr + ",Mensualité = " + comboMensualité.Text + ",NomTuteur = '" + txtNomPere.Text + "'," +
                        "PrenomTuteur ='" + txtPrenomPere.Text + "',TelTuteur = '" + txtNumTele.Text + "',TypeSport = '" + comboType.Text + "'," +
                        "Categorie = '" + comboCat.Text + "' where ID = '" + lblIDValue.Text + "'";
                    d.cmd.Connection = d.cnx;
                    d.cmd.ExecuteNonQuery();
                    MessageBox.Show("La modifcation a éffectuer avec succée", "Academy Kadem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

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
    }
}
