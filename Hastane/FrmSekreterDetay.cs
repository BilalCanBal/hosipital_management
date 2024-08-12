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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        public string Tc;
        public string adsoyad;
        public string secilen;
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = Tc;
            lblAdSoyad.Text = adsoyad;
            TxtId.Text = secilen;

            SqlCommand komut = new SqlCommand("select SekreterAdSoyad from Tbl_Sekreter where SekreterTC=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",lblTC.Text);
            SqlDataReader dr1 = komut.ExecuteReader();

            while(dr1.Read())
            {
                lblAdSoyad.Text = dr1[0].ToString();

            }

            bgl.baglanti().Close();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from Tbl_Branslar",bgl.baglanti());
            da1.Fill(dt1);
            DgvBranslar.DataSource= dt1;
            //branş datagrid
            



                

            //doktor
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select (DoktorAd + ' ' + DoktorSoyad) as Doktorlar,DoktorBrans as Brans from Tbl_Doktorlar", bgl.baglanti());
            da2.Fill(dt2);
            DgvDoktorlar.DataSource = dt2;

            //branş combobox
            SqlCommand komut2 = new SqlCommand("select BransAd From Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();


        }
        
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutKaydet = new SqlCommand("insert into Tbl_Randevularr (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@r1,@r2,@r3,@r4,)",bgl.baglanti());
            komutKaydet.Parameters.AddWithValue("@r1", MstTarih.Text);
            komutKaydet.Parameters.AddWithValue("@r2", MstSaat.Text);
            komutKaydet.Parameters.AddWithValue("@r3", CmbBrans.Text);
            komutKaydet.Parameters.AddWithValue("@r4", cmbDoktor.Text);
            komutKaydet.ExecuteNonQuery();
            MessageBox.Show("Randevu Oluşturuldu");
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();
            SqlCommand komut = new SqlCommand("select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",CmbBrans.Text);
            SqlDataReader r1 = komut.ExecuteReader();
            while (r1.Read())
            {
                cmbDoktor.Items.Add(r1[0] +" "+ r1[1]);
            }
            bgl.baglanti().Close();
        }

        private void BtnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut5 = new SqlCommand("insert into Tbl_Duyurular (Duyuru) values (@d1)",bgl.baglanti());
            komut5.Parameters.AddWithValue("@d1", RchDuyuruOlustur.Text);
            komut5.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu");
        }


        private void BtnDoktorPaneli_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli drp = new FrmDoktorPaneli();
            drp.Show();
        }

        private void BtnBransPaneli_Click(object sender, EventArgs e)
        {
            FrmBrans brs = new FrmBrans();
            brs.Show();
        }

        private void BtnRandevuListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi rnd = new FrmRandevuListesi();
            rnd.Show();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {

        }

        private void BtnDuyuru_Click(object sender, EventArgs e)
        {
            FrmDuyurular duyu = new FrmDuyurular();
            duyu.Show();
        }
    }
}
