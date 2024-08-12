using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hastane
{
    public partial class FrmSekreter : Form
    {
        public FrmSekreter()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();
        FrmSekreterDetay fr = new FrmSekreterDetay();

        private void btngiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * from Tbl_Sekreter " +
                "where SekreterTC=@p1 AND SekreterSifre=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txttc.Text);
            komut.Parameters.AddWithValue("@p2", txtsifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                fr.Tc = txttc.Text;
                fr.adsoyad = dr[1].ToString();
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC & Şifre");
            }
            while (dr.Read())
            {
                fr.adsoyad = dr[1].ToString();
            }
            bgl.baglanti().Close();
        }
    }
}



