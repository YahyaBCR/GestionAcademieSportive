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
    public partial class SearchJoueurs : UserControl
    {
        public SearchJoueurs()
        {
            InitializeComponent();
        }
        ADO d = new ADO();
        private void txtID_TextChanged(object sender, EventArgs e)
        {
            d.Connecter();
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
                    lblID.Text = dt.Rows[0][0].ToString();
                    lblNom.Text = dt.Rows[0][1].ToString();
                    lblPrenom.Text = dt.Rows[0][2].ToString();
                    lblDateNaiss.Text = dt.Rows[0][3].ToString().Substring(0,10);
                    lblDateIns.Text = dt.Rows[0][4].ToString().Substring(0, 10);
                    lblGenre.Text = dt.Rows[0][5].ToString() == "H" ? "Homme" : "Femme";
                    lblAdresse.Text = dt.Rows[0][6].ToString();
                    try
                    {
                        MemoryStream ms = new MemoryStream((byte[])dt.Rows[0][7]);
                        guna2CirclePictureBox1.Image = new Bitmap(ms);
                    }
                    catch { }
                    checkAssurence.Checked = Convert.ToBoolean(dt.Rows[0][8].ToString());
                    checkInscription.Checked = Convert.ToBoolean(dt.Rows[0][9].ToString());
                    lblMensualité.Text = dt.Rows[0][10].ToString();
                    lblNomPére.Text = dt.Rows[0][11].ToString();
                    lblPrenomPére.Text = dt.Rows[0][12].ToString();
                    lblTél.Text = dt.Rows[0][13].ToString();
                    lblType.Text = dt.Rows[0][14].ToString();
                    lblCat.Text = dt.Rows[0][15].ToString();
                    dt.Rows.Clear();
                }
                else
                    Vider();

            }
            catch { }
        }

        void Vider()
        {
            lblID.Text = "";
            lblNom.Text = "";
            lblPrenom.Text = "";
            lblAdresse.Text = "";
            lblDateIns.Text = "";
            lblDateNaiss.Text = "";
            lblMensualité.Text = "";
            lblNomPére.Text = "";
            lblPrenomPére.Text = "";
            lblTél.Text = "";
            lblType.Text = "";
            lblGenre.Text = "";
            lblCat.Text = "";
            checkAssurence.Checked = false;
            checkInscription.Checked = false;
            guna2CirclePictureBox1.Image = null;
        }
    }
}
