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
    public partial class FrmDoktorBilgiDuzenle : Form
    {
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }
        public string ad;
        public string soyad;
        public string brans;
        public string sifre;
        public string tc;
        SqlBaglantisi bgl = new SqlBaglantisi();

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
                if (tc==MskTC.Text.ToString()){ 
                SqlCommand komut = new SqlCommand("update Tbl_Doktorlar set DoktorAd=@q1,DoktorSoyad=@q2,DoktorBrans=@q3,DoktorSifre=@q4 where DoktorTC=@q5 ", bgl.baglanti());
                komut.Parameters.AddWithValue("@q1", TxtAd.Text);
                komut.Parameters.AddWithValue("@q2", TxtSoyad.Text);
                komut.Parameters.AddWithValue("@q3", CmbBrans.Text);
                komut.Parameters.AddWithValue("@q4", TxtSifre.Text);
                komut.Parameters.AddWithValue("@q5", MskTC.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Bilgileriniz güncellenmiştir");
                }

                else{
                MessageBox.Show("Lütfen TC nizi değiştirmeyiniz ");
                }
        }

        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            TxtAd.Text = ad;
            TxtSoyad.Text = soyad;
            TxtSifre.Text = sifre;
            CmbBrans.Text = brans;
            MskTC.Text = tc;
        }
    }
}
