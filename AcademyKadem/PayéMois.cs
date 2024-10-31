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
    public partial class PayéMois : UserControl
    {
        public PayéMois()
        {
            InitializeComponent();
        }
        ADO d = new ADO();
        private void txtID_TextChanged(object sender, EventArgs e)
        {
            d.Connecter();
            guna2PictureBox1.Visible = false;
            try
            {
                if (txtID.Text == "")
                {
                    Vider();
                    return;
                }
                d.cmd.CommandText = "select ID,Nom,Prénom,DateInscription,Image,Mensualité,Categorie,Reste,Payer,Montant from " +
                    "Joueurs inner join FraisParmois on Joueurs.ID = FraisParmois.IDJoueur where ID like '%" + txtID.Text + "'";
                d.cmd.Connection = d.cnx;
                d.dr = d.cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(d.dr);
                if (dt.Rows.Count > 0)
                {
                    lblID.Text = dt.Rows[0][0].ToString();
                    lblNom.Text = dt.Rows[0][1].ToString();
                    lblPrenom.Text = dt.Rows[0][2].ToString();
                    lblDateIns.Text = dt.Rows[0][3].ToString().Substring(0, 10);
                    try
                    {
                        MemoryStream ms = new MemoryStream((byte[])dt.Rows[0][4]);
                        guna2CirclePictureBox1.Image = new Bitmap(ms);
                    }
                    catch { }
                    lblMensualité.Text = dt.Rows[0][5].ToString();
                    lblCat.Text = dt.Rows[0][6].ToString();
                    Jan.Checked = Convert.ToBoolean(dt.Rows[4][8].ToString());
                    Fev.Checked = Convert.ToBoolean(dt.Rows[3][8].ToString());
                    Mars.Checked = Convert.ToBoolean(dt.Rows[8][8].ToString());
                    Avr.Checked = Convert.ToBoolean(dt.Rows[1][8].ToString());
                    Mai.Checked = Convert.ToBoolean(dt.Rows[7][8].ToString());
                    Jun.Checked = Convert.ToBoolean(dt.Rows[6][8].ToString());
                    Jul.Checked = Convert.ToBoolean(dt.Rows[5][8].ToString());
                    Aug.Checked = Convert.ToBoolean(dt.Rows[0][8].ToString());
                    Sep.Checked = Convert.ToBoolean(dt.Rows[11][8].ToString());
                    Oct.Checked = Convert.ToBoolean(dt.Rows[10][8].ToString());
                    Nov.Checked  = Convert.ToBoolean(dt.Rows[9][8].ToString());
                    Dec.Checked = Convert.ToBoolean(dt.Rows[2][8].ToString());
                    lblJan.Text = dt.Rows[4][7].ToString();
                    lblFev.Text = dt.Rows[3][7].ToString();
                    lblMars.Text = dt.Rows[8][7].ToString();
                    lblAvr.Text = dt.Rows[1][7].ToString();
                    lblMai.Text = dt.Rows[7][7].ToString();
                    lblJun.Text = dt.Rows[6][7].ToString();
                    lblJul.Text = dt.Rows[5][7].ToString();
                    lblAug.Text = dt.Rows[0][7].ToString();
                    lblSep.Text = dt.Rows[11][7].ToString();
                    lblOct.Text = dt.Rows[10][7].ToString();
                    lblNov.Text = dt.Rows[9][7].ToString();
                    lblDec.Text = dt.Rows[2][7].ToString();
                    txtMontant.Text = lblMensualité.Text;
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
            lblMensualité.Text = "";
            lblCat.Text = "";
            lblJan.Text = "0";
            lblFev.Text = "0";
            lblMars.Text = "0";
            lblAvr.Text = "0";
            lblMai.Text = "0";
            lblJun.Text = "0";
            lblJul.Text = "0";
            lblAug.Text = "0";
            lblSep.Text = "0";
            lblOct.Text = "0";
            lblNov.Text = "0";
            lblDec.Text = "0";
            lblDateIns.Text = "";
            Jan.Checked = false;
            Fev.Checked = false;
            Mars.Checked = false;
            Avr.Checked = false;
            Mai.Checked = false;
            Jun.Checked = false;
            Jul.Checked = false;
            Aug.Checked = false;
            Sep.Checked = false;
            Oct.Checked = false;
            Nov.Checked = false;
            Dec.Checked = false;
            guna2CirclePictureBox1.Image = null;
            guna2PictureBox1.Visible = false;
        }

        private void PayéMois_Load(object sender, EventArgs e)
        {
            d.Connecter();
        }

        

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Vous avez besoin de confirmer pour effectuer cette opération", "Question", MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
            {
                for (int i = 0; i < listMonth.Count; i++)
                {
                    Label control = new Label();
                    control = this.Controls.Find("lbl" + listMonth[i].ToString(), true).FirstOrDefault() as Label;
                    double rest = Convert.ToDouble(control.Text) - Convert.ToDouble(txtMontant.Text);
                    d.cmd.CommandText = "update FraisParMois set Payer = 1,Montant = " + txtMontant.Text + ",Reste = " + rest + "  where Mois = '" + listMonth[i].ToString() + "' and IDJoueur = '" + lblID.Text + "'";
                    d.cmd.Connection = d.cnx;
                    d.cmd.ExecuteNonQuery();
                    guna2PictureBox1.Visible = true;
                }
                d.cmd.CommandText = "select Reste,Mois from FraisParMois where IDJoueur ='" + lblID.Text + "'";
                d.cmd.Connection = d.cnx;
                d.dr = d.cmd.ExecuteReader();
                DataTable data = new DataTable();
                data.Clear();
                data.Load(d.dr);
                lblJan.Text = data.Rows[4][0].ToString();
                lblFev.Text = data.Rows[3][0].ToString();
                lblMars.Text = data.Rows[8][0].ToString();
                lblAvr.Text = data.Rows[1][0].ToString();
                lblMai.Text = data.Rows[7][0].ToString();
                lblJun.Text = data.Rows[6][0].ToString();
                lblJul.Text = data.Rows[5][0].ToString();
                lblAug.Text = data.Rows[0][0].ToString();
                lblSep.Text = data.Rows[11][0].ToString();
                lblOct.Text = data.Rows[10][0].ToString();
                lblNov.Text = data.Rows[9][0].ToString();
                lblDec.Text = data.Rows[2][0].ToString();
                d.dr.Close();
                listMonth.Clear();
            }
            
        }
        List<string> listMonth = new List<string>();
        private void CheckMonths(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2CustomCheckBox checkBox = new Guna.UI2.WinForms.Guna2CustomCheckBox();
            checkBox = (Guna.UI2.WinForms.Guna2CustomCheckBox)sender;
            if (checkBox.Checked)
                listMonth.Add(checkBox.Name.ToString());
            else
            {
                for (int i= 0; i < listMonth.Count; i++)
                {
                    if (listMonth[i].ToString() == checkBox.Name)
                        listMonth.RemoveAt(i);
                }
            }    

        }

        private void guna2HtmlLabel16_Click(object sender, EventArgs e)
        {
            //Guna.UI2.WinForms.Guna2HtmlLabel control = new Guna.UI2.WinForms.Guna2HtmlLabel();
            
        }
    }
}
