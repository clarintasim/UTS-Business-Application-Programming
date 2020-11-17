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
    public partial class Laporan_Penjualan : Form
    {
        string constring;
        public Laporan_Penjualan(string getconstring)
        {
            InitializeComponent();
            constring = getconstring;
        }

        private void Laporan_Penjualan_Load(object sender, EventArgs e)
        {
            DateTime date = DateTime.Parse("Nov 16, 2020") ;
            date = dateTimePicker1.Value;
            SqlConnection conn = new SqlConnection(constring);
            conn.Open();
            string sql = "SELECT CONVERT(VARCHAR, tanggal_transaksi, 107) AS [Tanggal Transaksi], transaksi_id, total FROM transaksi ";
            SqlCommand cmd1 = new SqlCommand(sql, conn);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, conn);

            DataSet ds = new DataSet();

            dataadapter.Fill(ds, "transaksi");


            dataGridView1.DataSource = ds.Tables["transaksi"].DefaultView;
            string queryString1 = "SELECT  SUM(total) FROM transaksi WHERE  CONVERT(VARCHAR, tanggal_transaksi, 107) = '"+date+"';";
            SqlCommand cmd = new SqlCommand(queryString1, conn);
            SqlDataReader read = cmd.ExecuteReader();
            read.Read();
            string sum = read[0].ToString();
            label3.Text = sum;
            conn.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime date = new DateTime();
            date = dateTimePicker1.Value;
            string date_new = date.ToString("MM/dd/yyyy");
            
            
           
            SqlConnection conn = new SqlConnection(constring);
            conn.Open();
            string sql = "SELECT CONVERT(VARCHAR, tanggal_transaksi, 107) AS [Tanggal Transaksi], transaksi_id, total FROM transaksi where CONVERT(VARCHAR, tanggal_transaksi, 101) like '" + date_new+"'; ";
            SqlCommand cmd1 = new SqlCommand(sql, conn);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, conn);

            DataSet ds = new DataSet();

            dataadapter.Fill(ds, "transaksi");
            string queryString1 = "SELECT  SUM(total) FROM transaksi WHERE  CONVERT(VARCHAR, tanggal_transaksi, 101) = '" + date_new + "';";
            SqlCommand cmd = new SqlCommand(queryString1, conn);
            SqlDataReader read = cmd.ExecuteReader();
            read.Read();
            string sum = read[0].ToString();
            label3.Text = sum;

            dataGridView1.DataSource = ds.Tables["transaksi"].DefaultView;

            conn.Close();
        }

        private void kasirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Kasir(constring).Show();
            this.Hide();
        }

        private void daftarMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new daftar_menu(constring).Show();
            this.Hide();
        }
    }
}
