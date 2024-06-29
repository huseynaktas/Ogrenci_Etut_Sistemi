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

namespace Ogrenci_Etut_Sistemi
{
    public partial class OgretmenForm : Form
    {
        public OgretmenForm()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-3JI920O\SQLEXPRESS;Initial Catalog=DbEtut;Integrated Security=True");

        void dersListesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBLDERSLER", conn); //veri tabanından verileri çek
            DataTable dt = new DataTable(); //veri tablosu oluştur
            da.Fill(dt); //verileri doldur
            cmbBrans.ValueMember = "DERSID"; //veri tabanındaki ders id'yi al
            cmbBrans.DisplayMember = "DERSAD"; //veri tabanındaki ders adını al
            cmbBrans.DataSource = dt; //verileri combobox'a aktar
        }

        private void btnOgretmenEkle_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into TBLOGRETMEN (AD, SOYAD, DERSID) values (@p1, @p2, @p3)", conn);
            cmd.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd.Parameters.AddWithValue("@p2", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3", cmbBrans.SelectedValue);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Öğretmen Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            dersListesi();
        }
    }
}
