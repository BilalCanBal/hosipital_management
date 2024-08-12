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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        public string tc;
        SqlBaglantisi bgl = new SqlBaglantisi();
        private DataTable dt;
        private SqlDataAdapter da;

        private void RefreshDataGrid()
        {

            dt.Clear(); // Mevcut verileri temizle
            da.Fill(dt); // Yeni verileri veri tabanından çek
            dataGridView1.DataSource = dt; // DataGrid'e verileri ata
        }

        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = tc;// Ad soyad çekme
                SqlCommand komut1 = new SqlCommand("Select HastaAd,HastaSoyad from Tbl_Hastalar " +
                    "where HastaTC=@q1", bgl.baglanti());
                komut1.Parameters.AddWithValue("@q1", lblTC.Text);
                SqlDataReader dr = komut1.ExecuteReader();
                while (dr.Read())
                {
                    lblAdSoyad.Text = (dr[0] + " " + dr[1]).ToString();
                }
                bgl.baglanti().Close();

                // Randevu geçmişi
                dt = new DataTable();
                da = new SqlDataAdapter("select * from Tbl_Randevular where HastaTC=@tc", bgl.baglanti());
                da.SelectCommand.Parameters.AddWithValue("@tc", tc);
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                // Branşları Çekme
                SqlCommand komut2 = new SqlCommand("select BransAd From Tbl_Branslar", bgl.baglanti());
                SqlDataReader dr2 = komut2.ExecuteReader();
                while (dr2.Read())
                {
                    CmbBrans.Items.Add(dr2[0]);
                }
                bgl.baglanti().Close();
                RefreshDataGrid();
        }

        

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("select DoktorAd,DoktorSoyad from Tbl_Doktorlar " +
                "where DoktorBrans=@p1",bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1",CmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                cmbDoktor.Items.Add(dr3[0] +" " + dr3[1] );

            }
            bgl.baglanti().Close();
        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Randevular where RandevuBrans='"+CmbBrans.Text+ "'",bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void LbkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmUyeKayit fr = new FrmUyeKayit();
            fr.TCno = lblTC.Text;
            fr.Show();
        }

        private void BtnRandevuAl_Click(object sender, EventArgs e)
        {
            int doktorId = GetDoktorId(cmbDoktor.Text);
            SqlCommand cmd = new SqlCommand("insert into Tbl_Randevular " +
                "(RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor,HastaTc,RandevuDurum)" +
                " values (GETDATE(),DATEPART(hour, GETDATE()),@p1,@p2,@p3,@p4)", bgl.baglanti());
            SqlCommand komut = new SqlCommand("");
            cmd.Parameters.AddWithValue("@p1",CmbBrans.Text);
            cmd.Parameters.AddWithValue("@p2",cmbDoktor.Text);
            cmd.Parameters.AddWithValue("@p3",lblTC.Text);
            cmd.Parameters.AddWithValue("@p4",true);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Randevu Oluşturuldu");
        }
        private int GetDoktorId(string doktorAdi)
        {
            int doktorId = 0;
            // Diğer tablodan doktorId'yi sorgulayarak alın
            string query = "SELECT * FROM Tbl_Doktorlar WHERE DoktorAd = @doktorAdi";
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-DV6HOLT\\SQLEXPRESS;" +
                "Initial Catalog=HastaneProje;Integrated Security=True"))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@doktorAdi", doktorAdi);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    doktorId = reader.GetInt32(0);
                }
                reader.Close();
            }
            return doktorId;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            TxtId.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }
    }
}
