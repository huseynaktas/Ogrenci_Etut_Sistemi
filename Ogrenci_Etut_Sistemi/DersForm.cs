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
    public partial class DersForm : Form
    {
        public DersForm()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-3JI920O\SQLEXPRESS;Initial Catalog=DbEtut;Integrated Security=True");

        private void btnDersEkle_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into TBLDERSLER (DERSAD) values (@p1)", conn);
            cmd.Parameters.AddWithValue("@p1", txtDersAd.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Ders Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
