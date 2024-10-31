using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
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
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();

            d.Connecter();
            d.cmd.CommandText = "select Mois,SUM(Montant+FraisInscription) from FraisParMois group by Mois";
            d.cmd.Connection = d.cnx;
            d.dr = d.cmd.ExecuteReader();
            d.dt.Load(d.dr);
            d.dr.Close();
            d.cmd.CommandText = "select Mois,Sum(FraisInternet+FraisEntraineurs+FraisMateriele+FraisTransport+Autre) from Charges group by Mois";
            d.cmd.Connection = d.cnx;
            d.dr = d.cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(d.dr);

            cartesianChart1.Series = new SeriesCollection
            {

                new LineSeries
                {
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(0,Convert.ToDouble(d.dt.Rows[4][1].ToString())),
                        new ObservablePoint(1,Convert.ToDouble(d.dt.Rows[3][1].ToString())),
                        new ObservablePoint(2,Convert.ToDouble(d.dt.Rows[8][1].ToString())),
                        new ObservablePoint(3,Convert.ToDouble(d.dt.Rows[1][1].ToString())),
                        new ObservablePoint(4,Convert.ToDouble(d.dt.Rows[7][1].ToString())),
                        new ObservablePoint(5,Convert.ToDouble(d.dt.Rows[6][1].ToString())),
                        new ObservablePoint(6,Convert.ToDouble(d.dt.Rows[5][1].ToString())),
                        new ObservablePoint(7,Convert.ToDouble(d.dt.Rows[0][1].ToString())),
                        new ObservablePoint(8,Convert.ToDouble(d.dt.Rows[11][1].ToString())),
                        new ObservablePoint(9,Convert.ToDouble(d.dt.Rows[10][1].ToString())),
                        new ObservablePoint(10,Convert.ToDouble(d.dt.Rows[9][1].ToString())),
                        new ObservablePoint(11,Convert.ToDouble(d.dt.Rows[2][1].ToString())),
                    },
                    PointGeometrySize = 15
                },
                new LineSeries
                {
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(0,Convert.ToDouble(dt.Rows[4][1].ToString())),
                        new ObservablePoint(1,Convert.ToDouble(dt.Rows[3][1].ToString())),
                        new ObservablePoint(2,Convert.ToDouble(dt.Rows[8][1].ToString())),
                        new ObservablePoint(3,Convert.ToDouble(dt.Rows[1][1].ToString())),
                        new ObservablePoint(4,Convert.ToDouble(dt.Rows[7][1].ToString())),
                        new ObservablePoint(5,Convert.ToDouble(dt.Rows[6][1].ToString())),
                        new ObservablePoint(6,Convert.ToDouble(dt.Rows[5][1].ToString())),
                        new ObservablePoint(7,Convert.ToDouble(dt.Rows[0][1].ToString())),
                        new ObservablePoint(8,Convert.ToDouble(dt.Rows[11][1].ToString())),
                        new ObservablePoint(9,Convert.ToDouble(dt.Rows[10][1].ToString())),
                        new ObservablePoint(10,Convert.ToDouble(dt.Rows[9][1].ToString())),
                        new ObservablePoint(11,Convert.ToDouble(dt.Rows[2][1].ToString())),
                    },
                    PointGeometrySize = 15
                }
            };
        }

        ADO d = new ADO();
        private void Dashboard_Load(object sender, EventArgs e)
        {
            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Mois",
                Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
            });
            cartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Revenue",
                LabelFormatter = value => value.ToString("C")
            });
            //SQL VALUE FROM DATA BASE
            PieCHart();
            d.Connecter();
            Statistique();
            
        }

        void PieCHart()
        {
            Func<ChartPoint, string> labelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            SeriesCollection seriesCollection = new SeriesCollection();
            //double[] Values = { 0.56, 0.66, 0.01, 0.43, 0.58 };
            d.cmd.CommandText = "select Categorie, count(*) from Joueurs group by Categorie";
            d.cmd.Connection = d.cnx;
            d.dr = d.cmd.ExecuteReader();
            d.dt2.Load(d.dr);
            for (int i = 0; i < d.dt2.Rows.Count; i++)
            {
                PieSeries pieSeries = new PieSeries
                {
                    Title = d.dt2.Rows[i][0].ToString(),
                    Values = new ChartValues<double> {Convert.ToDouble(d.dt2.Rows[i][1].ToString())},
                    DataLabels = true,
                    LabelPoint = labelPoint
                };
                seriesCollection.Add(pieSeries);
            }
            pieChart1.Series = seriesCollection;
        }
        
        void Statistique()
        {
            d.cmd.CommandText = "select Month(DateInscription),count(*) from Joueurs group by Month(DateInscription)";
            d.cmd.Connection = d.cnx;
            d.dr = d.cmd.ExecuteReader();
            d.dtStatistique.Load(d.dr);
            d.dr.Close();
            for (int i = 0; i < d.dtStatistique.Rows.Count; i++)
            {
                if (d.dtStatistique.Rows[i][0].ToString() == "9")
                    lbl9.Text = d.dtStatistique.Rows[i][1].ToString();
                if (d.dtStatistique.Rows[i][0].ToString() == "10")
                    lbl10.Text = d.dtStatistique.Rows[i][1].ToString();
                if (d.dtStatistique.Rows[i][0].ToString() == "11")
                    lbl11.Text = d.dtStatistique.Rows[i][1].ToString();
                if (d.dtStatistique.Rows[i][0].ToString() == "12")
                    lbl12.Text = d.dtStatistique.Rows[i][1].ToString();
                if (d.dtStatistique.Rows[i][0].ToString() == "1")
                    lbl1.Text = d.dtStatistique.Rows[i][1].ToString();
                if (d.dtStatistique.Rows[i][0].ToString() == "2")
                    lbl2.Text = d.dtStatistique.Rows[i][1].ToString();
                if (d.dtStatistique.Rows[i][0].ToString() == "3")
                    lbl3.Text = d.dtStatistique.Rows[i][1].ToString();
                if (d.dtStatistique.Rows[i][0].ToString() == "4")
                    lbl4.Text = d.dtStatistique.Rows[i][1].ToString();
                if (d.dtStatistique.Rows[i][0].ToString() == "5")
                    lbl5.Text = d.dtStatistique.Rows[i][1].ToString();
                if (d.dtStatistique.Rows[i][0].ToString() == "6")
                    lbl6.Text = d.dtStatistique.Rows[i][1].ToString();
            }
            d.dr.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            AllCategoriess all = new AllCategoriess("U13");
            all.ShowDialog();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            AllCategoriess all = new AllCategoriess("U9");
            all.ShowDialog();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AllCategoriess all = new AllCategoriess("U11");
            all.ShowDialog();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            AllCategoriess all = new AllCategoriess("U15");
            all.ShowDialog();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            AllCategoriess all = new AllCategoriess("U17");
            all.ShowDialog();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            AllCategoriess all = new AllCategoriess("U19");
            all.ShowDialog();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2CirclePictureBox2_Click(object sender, EventArgs e)
        {
            if (this.BackColor == Color.White)
                this.BackColor = Color.Maroon;
            else
                this.BackColor = Color.White;
        }
    }
}
