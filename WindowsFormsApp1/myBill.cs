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
using System.Configuration;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class myBill : Form
    {
        string nomeja;
        string constring;
        int transid;
        public myBill(string getnomeja, string getconstring, int gettransid)
        {
            InitializeComponent();
            nomeja = getnomeja;
            constring = getconstring;
            transid = gettransid;

        }

        private void myBill_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(constring);
            conn.Open();
            string sql = "SELECT dm.nama_makanan as [Nama Makanan], dt.quantity as Quantity, dt.harga as Harga FROM detail_transaksi dt JOIN daftar_makanan dm ON dt.daftarMakanan_id = dm.daftarMakanan_id WHERE dt.is_commit = 1 and dt.transaksi_id = '"+transid+"' GROUP BY dm.nama_makanan, dt.quantity, dt.harga ORDER BY dm.nama_makanan";

            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, conn);

            DataSet ds = new DataSet();

            dataadapter.Fill(ds, "detail_transaksi");

            dataGridView1.DataSource = ds.Tables["detail_transaksi"].DefaultView;

            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Order(nomeja, constring, transid).Show();
        }
    }
}
