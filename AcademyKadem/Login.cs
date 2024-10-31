using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AcademyKadem
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }
        int i = -1;
        private void timer1_Tick(object sender, EventArgs e)
        {
            string academy = "Academy kadem sport";
            i += 1;
            guna2HtmlLabel1.Text += academy[i].ToString();
            if (guna2HtmlLabel1.Text == academy)
            {
                timer1.Enabled = false;
                this.BackColor = Color.White;
                guna2HtmlLabel1.ForeColor = Color.Black;
                txtPass.BackColor = Color.White;
                txtUser.BackColor = Color.White;
                txtUser.FillColor = Color.White;
                txtPass.FillColor = Color.White;
                guna2Button1.FillColor = Color.LightPink;
            }
        }
        ADO d = new ADO();
        int Nbr = 0;
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //int existe;
            //d.cmd.CommandText = "select count(*) from Profile where Username = '"++"' and Password = '"++"'";
            //d.cmd.Connection = d.cnx;
            //existe = (int)d.cmd.ExecuteScalar();
            string pass;
            Nbr++;
            if (Nbr <= 3)
            {
                d.cmd.Parameters.Clear();
                d.cmd.CommandType = CommandType.Text;
                d.cmd.CommandText = "select Password from Profile where Username = '" + txtUser.Text + "'";
                d.cmd.Connection = d.cnx;
                pass = (string)d.cmd.ExecuteScalar();
                d.cmd.CommandType = CommandType.StoredProcedure;
                d.cmd.CommandText = "Verifier";
                d.cmd.Parameters.Add("@Pass", SqlDbType.VarChar, 50).Value = txtPass.Text;
                SqlParameter ok = new SqlParameter("@PassCrypt", SqlDbType.VarChar, 50);
                d.cmd.Parameters.Add(ok);
                ok.Direction = ParameterDirection.Output;
                d.cmd.Connection = d.cnx;
                d.cmd.ExecuteNonQuery();
                if (ok.Value.ToString() == pass)
                {
                    Form1 frm = new Form1();
                    this.Hide();
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Username ou Password n'est pas correct", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (Nbr == 3)
                    {
                        txtUser.Enabled = false;
                        txtPass.Enabled = false;
                        MessageBox.Show("Cliquer sur Mot de passe oublié pour récupérer votre informations ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //try
                        //{
                        //    MailMessage msg = new MailMessage();
                        //    msg.From = new MailAddress("nilusilu3@gmail.com");
                        //    msg.To.Add("elkabousomar0@gmail.com");
                        //    msg.Subject ="Warning";
                        //    msg.Body = "Someone is trying to hack your account";
                        //    SmtpClient smt = new SmtpClient();
                        //    smt.Host = "smtp.gmail.com";
                        //    System.Net.NetworkCredential ntcd = new NetworkCredential();
                        //    ntcd.UserName = "istarochesnoirspacea@gmail.com";
                        //    ntcd.Password = "fdzjwowowaxiloui";
                        //    smt.Credentials = ntcd;
                        //    smt.EnableSsl = true;
                        //    smt.Port = 587;
                        //    smt.Send(msg);
                        //    MessageBox.Show("votre message a été envoyé au support technique, nous vous répondrons dans les plus brefs délais ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show(ex.Message);
                        //}
                    }
                }
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            d.Connecter();



        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2CirclePictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2CirclePictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void guna2CirclePictureBox2_Click(object sender, EventArgs e)
        {
            if (this.BackColor == Color.White)
            {
                this.BackColor = Color.Maroon;
                guna2HtmlLabel1.ForeColor = Color.White;
                txtPass.BackColor = Color.Maroon;
                txtUser.BackColor = Color.Maroon;
                txtUser.FillColor = Color.Maroon;
                txtPass.FillColor = Color.Maroon;
                guna2Button1.FillColor = Color.Maroon;
                return;
            }
            this.BackColor = Color.White;
            guna2HtmlLabel1.ForeColor = Color.Black;
            txtPass.BackColor = Color.White;
            txtUser.BackColor = Color.White;
            txtUser.FillColor = Color.White;
            txtPass.FillColor = Color.White;
            guna2Button1.FillColor = Color.LightPink;
        }
    }
}