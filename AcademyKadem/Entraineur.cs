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
    public partial class Entraineur : UserControl
    {
        public Entraineur()
        {
            InitializeComponent();
        }

        private void Entraineur_Load(object sender, EventArgs e)
        {
            d.Connecter();
            Remplir();
        }
        ADO d = new ADO();
        void Remplir()
        {
            d.dt.Clear();
            d.cmd.CommandText = "select ID, Nom,Prenom as 'Prénom', Type as 'Type de sport',DateNaissance from Entraineur";
            d.cmd.Connection = d.cnx;
            d.dr = d.cmd.ExecuteReader();
            d.dt.Load(d.dr);
            guna2DataGridView1.DataSource = d.dt;
            d.dr.Close();
        }

        string chemin = "";
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
            dateNaissance.Value = DateTime.Now;
            lblID.Text = "";
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Vider();
        }

        int Existe()
        {
            d.cmd.CommandText = "select count(*) from Entraineur where Nom = '" + txtNom.Text + "' and Prenom = '"+txtPrenom.Text+"'";
            d.cmd.Connection = d.cnx;
            return (int)d.cmd.ExecuteScalar() ;
        }
        //insert into Entraineur values(REPLACE(CAST (('2000/07/30') as varchar(20))+CAST((NEXT VALUE FOR CountBy1) as varchar(max)),'/',''),'omar','2000/07/30')
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (txtNom.Text.Trim() != "" && txtPrenom.Text.Trim() != "" && comboType.Text != "")
            {
                try
                {
                    if (Existe() == 0)
                    {
                        byte[] tabimg = null;
                        FileStream fs = new FileStream(chemin, FileMode.Open);
                        BinaryReader br = new BinaryReader(fs);
                        tabimg = br.ReadBytes((int)fs.Length);
                        fs.Close();
                        string date = dateNaissance.Value.Date.ToShortDateString();
                        d.cmd.CommandText = "insert into Entraineur values (CAST (year('" + date + "') as varchar(4))+CAST(MONTH('" + date + "') as varchar(2))+CAST(day('" + date + "') as varchar(2))+CAST((NEXT VALUE FOR CountBy1)as varchar(max)),'" + txtNom.Text + "','" + comboType.Text + "','" + date + "',@img,'" + txtPrenom.Text + "')";
                        d.cmd.Parameters.Clear();
                        d.cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@img", tabimg));
                        d.cmd.Connection = d.cnx;
                        d.cmd.ExecuteNonQuery();
                        Remplir();
                        MessageBox.Show("L'entraineur "+txtNom.Text+" rejoindre au Academy Kadem Sport", "Academy Kadem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Cette Entraineur a deja existe ! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch
                {
                    MessageBox.Show("C'est obligé de ajouter la photo ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
                MessageBox.Show("C'est obligé de remplir tout les champs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (lblID.Text != "")
            {
                try
                {
                    d.cmd.CommandText = "update Entraineur set Nom = '" + txtNom.Text + "',Prenom ='" + txtPrenom.Text + "',DateNaissance ='" + dateNaissance.Value + "',Type ='" + comboType.Text + "' where ID = '" + lblID.Text + "'";
                    d.cmd.Connection = d.cnx;
                    d.cmd.ExecuteNonQuery();
                    Remplir();
                    MessageBox.Show("Vous avez modifier les information de l'entraineur" + txtNom.Text,"Academy Kadem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch { }
            }
            else
                MessageBox.Show("Sélectionner une entraineur pour éffectuer la modification", "Error",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
            
        }
        int index;
        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                index = guna2DataGridView1.CurrentRow.Index;
                txtNom.Text = guna2DataGridView1.Rows[index].Cells[1].Value.ToString();
                txtPrenom.Text = guna2DataGridView1.Rows[index].Cells[2].Value.ToString();
                dateNaissance.Value = Convert.ToDateTime(guna2DataGridView1.Rows[index].Cells[4].Value);
                comboType.Text = guna2DataGridView1.Rows[index].Cells[3].Value.ToString();
                lblID.Text = guna2DataGridView1.Rows[index].Cells[0].Value.ToString();
                d.cmd.CommandText = "select Picture from Entraineur where ID = '" + guna2DataGridView1.Rows[index].Cells[0].Value + "'";
                d.cmd.Connection = d.cnx;
                MemoryStream ms = new MemoryStream((byte[])d.cmd.ExecuteScalar());
                guna2CirclePictureBox1.Image = new Bitmap(ms);
            }
            catch { }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (lblID.Text != "")
            {
                try
                {
                    d.cmd.CommandText = "delete from Entraineur where ID = '" + lblID.Text + "'";
                    d.cmd.Connection = d.cnx;
                    d.cmd.ExecuteNonQuery();
                    MessageBox.Show("Vous avez supprimer l'entraineur " + txtNom.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Sélectionner une entraineur pour éffectuer la suppression", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
    }
}
