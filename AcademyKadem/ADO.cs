using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace AcademyKadem
{
    class ADO
    {
        public SqlConnection cnx = new SqlConnection(@"Data Source=AKS\SQLEXPRESS;Initial Catalog=AcademyKadem;Integrated Security=True");
        //public SqlConnection cnx = new SqlConnection(@"Data Source=CASPER\SQLEXPRESS;Initial Catalog=AcademyKadem;Integrated Security=True");
        public SqlDataReader dr;
        public DataTable dtStatistique = new DataTable();
        public SqlDataAdapter dap;
        public SqlCommand cmd = new SqlCommand();
        public DataTable dt = new DataTable();
        public DataTable dt2 = new DataTable();


        public void Connecter()
        {
            if (cnx.State == ConnectionState.Closed || cnx.State == ConnectionState.Broken)
                cnx.Open();
        }
        public void Deconnecter()
        {
            if (cnx.State == ConnectionState.Open)
                cnx.Close();
        }
    }
}
