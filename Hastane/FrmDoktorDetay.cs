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
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }
        FrmDoktorBilgiDuzenle fdd = new FrmDoktorBilgiDuzenle();
        FrmDoktorBilgiDuzenle fdbd = new FrmDoktorBilgiDuzenle();
        public string tc = "";
        SqlBaglantisi bgl = new SqlBaglantisi();

        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            lblDrTC.Text = tc;
            SqlCommand cmd = new SqlCommand("select * from Tbl_Doktorlar where DoktorTC=@q1",bgl.baglanti());
            cmd.Parameters.AddWithValue("@q1",lblDrTC.Text);
            SqlDataReader dr =cmd.ExecuteReader();
            while (dr.Read())
            {
                lblDrAdSoyad.Text = (dr[1] + " " + dr[2]);
                fdd.ad = (dr[1]).ToString();
                fdd.soyad = (dr[2]).ToString();
                fdd.brans = (dr[3]).ToString();
                fdd.tc = (dr[4]).ToString();
                fdd.sifre = (dr[5]).ToString();
            }
            bgl.baglanti().Close();
            


            DataTable dt= new DataTable();
            string ad = lblDrAdSoyad.Text;
            ad.Trim();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Randevular where RandevuDoktor='"+lblDrAdSoyad.Text+"'",bgl.baglanti());      
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnBilgiDuzenle_Click(object sender, EventArgs e)
        {
            lblDrTC.Text = tc;
            SqlCommand cmd = new SqlCommand("select * from Tbl_Doktorlar where DoktorTC=@q1", bgl.baglanti());
            cmd.Parameters.AddWithValue("@q1", lblDrTC.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                fdd.ad = (dr[1]).ToString();
                fdd.soyad = (dr[2]).ToString();
                fdd.brans = (dr[3]).ToString();
                fdd.tc = (dr[4]).ToString();
                fdd.sifre = (dr[5]).ToString();
            }
            bgl.baglanti().Close();
            fdd.Show();
        }

        private void BtnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular duyu = new FrmDuyurular();
            duyu.Show();
        }

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();

        }
    }
}
