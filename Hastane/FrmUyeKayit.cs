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
    public partial class FrmUyeKayit : Form
    {
        public FrmUyeKayit()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();

        private void label6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Buraya Tıklama");
        }

        public string TCno;

        private void FrmUyeKayit_Load(object sender, EventArgs e)
        {
            msktc.Text = TCno;
            SqlCommand komut = new SqlCommand("select * from Tbl_Hastalar where HastaTC=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",msktc.Text);
            SqlDataReader r1 = komut.ExecuteReader();
            while (r1.Read())
            {
                txtad.Text = r1[1].ToString();
                txtsoyad.Text= r1[2].ToString();
                msktel.Text = r1[4].ToString();
                txtsifre.Text = r1[5].ToString();
                cmbcinsiyet.Text = r1[6].ToString();
            }
            bgl.baglanti().Close();


        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE Tbl_Hastalar SET HastaAd=@p1,HastaSoyad=@p2,HastaTelefon=@p3,HastaSifre=@p4,HastaCinsiyet=@p5 WHERE HastaTC='"+TCno+"'",bgl.baglanti());

            komut.Parameters.AddWithValue("@p1",txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3", msktel.Text);
            komut.Parameters.AddWithValue("@p4", txtsifre.Text);
            komut.Parameters.AddWithValue("@p5", cmbcinsiyet.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgileriniz Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
            Close();
        }

    }
}
