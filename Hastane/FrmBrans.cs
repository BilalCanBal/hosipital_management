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
    public partial class FrmBrans : Form
    {
        public FrmBrans()
        {
            InitializeComponent();
        }
        public void clear()
        {
            TxtBransAd.Text = "";
            TxtBransid.Text = "";
        }
        string deger = "";
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void FrmBrans_Load(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from Tbl_Branslar", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;


        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            if (TxtBransid.Text == ""  )
            {
                if (TxtBransAd.Text != "") 
                { 
                    try
                    {
                        SqlCommand komut = new SqlCommand("insert into Tbl_Branslar (BransAd) values (@p1)", bgl.baglanti());
                        komut.Parameters.AddWithValue("@p1", TxtBransAd.Text);
                        komut.ExecuteNonQuery();
                        MessageBox.Show("Branş Eklenmiştir.");
                        bgl.baglanti().Close();
                        clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata oluştu: " + ex.Message);
                        // Hata durumunda yapılması gerekenler buraya yazılabilir
                    } 
                }
                else
                    {
                    MessageBox.Show("!!!  Lütfen branş adı kısmını boş bırakmayınız  !!!");
                    }
            }
            else
            {
                MessageBox.Show("!!!  Lütfen eklemek için ID kısmını boş bırakınız  !!!");

                // Gerekli koşullar sağlanmadığında yapılması gerekenler buraya yazılabilir
            }


        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("delete from Tbl_Branslar where BransId=@q1",bgl.baglanti());
            komut1.Parameters.AddWithValue("@q1",TxtBransid.Text);
            komut1.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show(TxtBransAd.Text + "Branş Silinmiştir");
            clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = dataGridView1.SelectedCells[0].RowIndex;
            TxtBransid.Text = dataGridView1.Rows[x].Cells[0].Value.ToString();
            TxtBransAd.Text = dataGridView1.Rows[x].Cells[1].Value.ToString();
            deger = TxtBransAd.Text;
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut2  = new SqlCommand("update Tbl_Branslar set BransAd=@x1 where BransId=@x2",bgl.baglanti());
            komut2.Parameters.AddWithValue("@x1",TxtBransAd.Text);
            komut2.Parameters.AddWithValue("@x2", TxtBransid.Text);
            komut2.ExecuteNonQuery(); bgl.baglanti().Close(); MessageBox.Show(deger+"->"+ TxtBransAd.Text + " olarak değiştirilmiştir.");



            clear();
        }
    }
}
