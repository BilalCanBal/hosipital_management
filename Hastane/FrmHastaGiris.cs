using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Hastane
{
    public partial class FrmHastaGiris : Form
    {
        public FrmHastaGiris()
        {
            InitializeComponent();
        }
        
        SqlBaglantisi bgl = new SqlBaglantisi();
        FrmHastaDetay frh = new FrmHastaDetay();


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle fb = new FrmBilgiDuzenle();
            fb.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select HastaAd,HastaSoyad from Tbl_Hastalar where" +
                " HastaTC=@p1 AND HastaSifre=@p2",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",MskTC.Text);
            komut.Parameters.AddWithValue("@p2",TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                frh.tc = MskTC.Text;
                frh.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Hatalı TC & Şifre");
            }
            while (dr.Read())
            {

            }
            bgl.baglanti().Close();
        }








































        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
