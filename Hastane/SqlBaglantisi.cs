using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Hastane
{
    internal class SqlBaglantisi
    {
        public SqlConnection baglanti()
        {
            //SqlConnection baglanti=new SqlConnection();
            SqlConnection baglan=new SqlConnection("Data Source=DESKTOP-DV6HOLT\\SQLEXPRESS;" +
                "Initial Catalog=HastaneProje;Integrated Security=True");
            baglan.Open();
            return baglan;


        }


    }
}
