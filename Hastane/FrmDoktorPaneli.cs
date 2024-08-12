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
    public partial class FrmDoktorPaneli : Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();

        public void clear()
        {
            TxtAd.Text="";
            TxtSoyad.Text="";
            CmbBrans.Text="";
            MstTc.Text="";
            TxtSifre.Text="";
        }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }

        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
            //doktor
            DataTable dt1 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from Tbl_Doktorlar", bgl.baglanti());
            da2.Fill(dt1);
            dataGridView1.DataSource = dt1;


            SqlCommand komut2 = new SqlCommand("select BransAd From Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();




        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Doktorlar (DoktorAd,DoktorSoyad,DoktorBrans,DoktorTC,DoktorSifre) values (@d1,@d2,@d3,@d4,@d5)",bgl.baglanti());
            komut.Parameters.AddWithValue("@d1",TxtAd.Text);
            komut.Parameters.AddWithValue("@d2",TxtSoyad.Text);
            komut.Parameters.AddWithValue("@d3",CmbBrans.Text);
            komut.Parameters.AddWithValue("@d4",MstTc.Text);
            komut.Parameters.AddWithValue("@d5",TxtSifre.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            clear();
            MessageBox.Show("Kayıt Eklenmiştir.");


        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand sil = new SqlCommand("delete from Tbl_Doktorlar where DoktorTC=@p1",bgl.baglanti());
            sil.Parameters.AddWithValue("@p1",MstTc.Text);
            sil.ExecuteNonQuery();
            clear();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Silinmiştir.","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand gun = new SqlCommand("update Tbl_Doktorlar SET DoktorAd=@q1 , DoktorSoyad=@q2 , DoktorBrans=@q3 , DoktorSifre=@q4 where DoktorTC=@q5 ", bgl.baglanti());
            gun.Parameters.AddWithValue("@q1", TxtAd.Text);
            gun.Parameters.AddWithValue("@q2", TxtSoyad.Text);
            gun.Parameters.AddWithValue("@q3", CmbBrans.Text);
            gun.Parameters.AddWithValue("@q4", TxtSifre.Text);
            gun.Parameters.AddWithValue("@q5", MstTc.Text);
            gun.ExecuteNonQuery();
            clear();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt GÜncellenmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            CmbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            MstTc.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtSifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            

        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
}
