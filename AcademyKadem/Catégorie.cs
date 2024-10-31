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
    public partial class Catégorie : UserControl
    {
        public Catégorie()
        {
            InitializeComponent();
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {
            txtJrFin.Text = txtJrDebut.Text;
        }
        ADO d = new ADO();

        void Remplir()
        {
            d.dt.Clear();
            d.cmd.CommandText = " select Categorie as 'Catégorie', DebutJr as 'Le Jour de début',FinJr as 'Le jour de la fin',DebutHr as 'Le Temp du début',FinHr as 'Le Temp de la fin',TypeSport as 'Type sport' from Catégories ";
            d.cmd.Connection = d.cnx;
            d.dr = d.cmd.ExecuteReader();
            d.dt.Load(d.dr);
            guna2DataGridView1.DataSource = d.dt;
        }
        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                d.cmd.CommandText = "select Categorie as 'Catégorie', DebutJr as 'Le Jour de début',FinJr as 'Le jour de la fin',DebutHr as 'Le Temp du début',FinHr as 'Le Temp de la fin',TypeSport as 'Type  Sport ' from Catégories where Categorie = '" + guna2ComboBox1.Text + "' and TypeSport ='"+comboType.Text+"'";
                d.cmd.Connection = d.cnx;
                d.dr = d.cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(d.dr);
                //txtJrDebut.Text = dt.Rows[0][1].ToString();
                //txtJrFin.Text = dt.Rows[0][2].ToString();
                //txtTempsDebut.Text = dt.Rows[0][3].ToString();
                //txtTempFin.Text = dt.Rows[0][4].ToString();
                guna2DataGridView1.DataSource = dt;
            }
            catch(Exception ex )
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void Catégorie_Load(object sender, EventArgs e)
        {
            d.Connecter();
            Remplir();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if(Convert.ToInt32(txtTempFin.Text.Substring(0,2).ToString()).GetType() == typeof(int) 
                        && Convert.ToInt32(txtTempsDebut.Text.Substring(0, 2).ToString()).GetType() == typeof(int)
                        && Convert.ToChar(txtTempFin.Text.Substring(2, 1).ToString()) == 'h'
                        &&Convert.ToChar(txtTempsDebut.Text.Substring(2, 1).ToString()) == 'h'
                        && Convert.ToChar(txtTempFin.Text.Substring(6, 1).ToString()) == 'm'
                        && Convert.ToChar(txtTempsDebut.Text.Substring(6, 1).ToString()) == 'm'
                        && Convert.ToChar(txtTempsDebut.Text.Substring(3, 1).ToString()) == ':'
                        && Convert.ToChar(txtTempsDebut.Text.Substring(3, 1).ToString()) == ':')
                {
                    d.cmd.CommandText = "update Catégories set DebutJr = '" + txtJrDebut.Text + "',FinJr = '" + txtJrFin.Text + "', DebutHr = '" + txtTempsDebut.Text + "',FinHr = '" + txtTempFin.Text + "' where Categorie = '"+guna2ComboBox1.Text+"' and TypeSport = '"+comboType.Text+"' and DebutJr = '"+guna2DataGridView1.Rows[index].Cells[1].Value+"'";
                    d.cmd.Connection = d.cnx;
                    d.cmd.ExecuteNonQuery();
                    Remplir();
                }
            }
            catch
            {
                MessageBox.Show("Saisir un autre jour ","Academy Kadem",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void comboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable data = new DataTable();
            data.Clear();
            d.cmd.CommandText = " select Categorie as 'Catégorie', DebutJr as 'Le Jour de début',FinJr as 'Le jour de la fin',DebutHr as 'Le Temp du début',FinHr as 'Le Temp de la fin',TypeSport as 'Type sport' from Catégories where Categorie = '" + guna2ComboBox1.Text + "' and TypeSport = '" + comboType.Text + "'";
            d.cmd.Connection = d.cnx;
            d.dr = d.cmd.ExecuteReader();
            data.Load(d.dr);
            guna2DataGridView1.DataSource = data;
        }
        int index;
        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = guna2DataGridView1.CurrentRow.Index;
            guna2ComboBox1.Text = guna2DataGridView1.Rows[index].Cells[0].Value.ToString();
            comboType.Text = guna2DataGridView1.Rows[index].Cells[5].Value.ToString();
            txtJrDebut.Text = guna2DataGridView1.Rows[index].Cells[1].Value.ToString();
            txtJrFin.Text = guna2DataGridView1.Rows[index].Cells[2].Value.ToString();
            txtTempsDebut.Text = guna2DataGridView1.Rows[index].Cells[3].Value.ToString();
            txtTempFin.Text = guna2DataGridView1.Rows[index].Cells[4].Value.ToString();
        }
    }
}
