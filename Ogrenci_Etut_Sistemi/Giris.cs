using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ogrenci_Etut_Sistemi
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-3JI920O\SQLEXPRESS;Initial Catalog=DbEtut;Integrated Security=True");

        void dersListesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBLDERSLER", conn); //veri tabanından verileri çek
            DataTable dt = new DataTable(); //veri tablosu oluştur
            da.Fill(dt); //verileri doldur
            cmbDers.ValueMember = "DERSID"; //veri tabanındaki ders id'yi al
            cmbDers.DisplayMember = "DERSAD"; //veri tabanındaki ders adını al
            cmbDers.DataSource = dt; //verileri combobox'a aktar
        }

        //Etüt listesi
        void etutListesi()
        {
            SqlDataAdapter da3 = new SqlDataAdapter("Execute Etut", conn); //veri tabanından verileri çek
            DataTable dt3 = new DataTable(); //veri tablosu oluştur
            da3.Fill(dt3); //verileri doldur
            dataGridView1.DataSource = dt3; //verileri datagridview'a aktar
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dersListesi();
            etutListesi();
        }

        private void cmbDers_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataAdapter da2 = new SqlDataAdapter("Select OGRTID, CONCAT(AD, ' ', SOYAD) AS TamAd, DERSID from TBLOGRETMEN where DERSID = " + cmbDers.SelectedValue, conn); //veri tabanından verileri çek
            DataTable dt2 = new DataTable(); //veri tablosu oluştur
            da2.Fill(dt2); //verileri doldur
           // SqlDataAdapter da3 = new SqlDataAdapter("Select OGRTID, CONCAT(AD, ' ', SOYAD) AS TamAd from TBLOGRETMEN where DERSID = " + cmbDers.SelectedValue, conn);
            cmbOgretmen.ValueMember = "OGRTID"; //veri tabanındaki öğretmen id'yi al
            cmbOgretmen.DisplayMember = "TamAd"; // Alias kullanarak birleştirilmiş sütunu atayın
            cmbOgretmen.DataSource = dt2; //verileri combobox'a aktar
        }

        private void btnEtutOlustur_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Insert into TBLETUT (DERSID, OGRETMENID, TARIH, SAAT) values (@p1, @p2, @p3, @p4)", conn);
            cmd.Parameters.AddWithValue("@p1", cmbDers.SelectedValue);
            cmd.Parameters.AddWithValue("@p2", cmbOgretmen.SelectedValue);
            cmd.Parameters.AddWithValue("@p3", mskTarih.Text);
            cmd.Parameters.AddWithValue("@p4", mskSaat.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Etüt oluşturuldu.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtEtutId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
        }

        private void btnEtutVer_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Update TBLETUT set OGRENCIID = @p1, DURUM = @p2 where ID = @p3", conn);
            cmd.Parameters.AddWithValue("@p1", txtOgrNo.Text);
            cmd.Parameters.AddWithValue("@p2", "True");
            cmd.Parameters.AddWithValue("@p3", txtEtutId.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Etüt öğrenciye verildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnOgrKayit_Click(object sender, EventArgs e)
        {
            OgrenciForm frm = new OgrenciForm();
            frm.Show();
        }

        private void btnDersGirisi_Click(object sender, EventArgs e)
        {
            DersForm frm = new DersForm();
            frm.Show();
        }

        private void btnOgrtKayit_Click(object sender, EventArgs e)
        {
            OgretmenForm frm = new OgretmenForm();
            frm.Show();
        }
    }
}
