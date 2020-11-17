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
    public partial class Menu_Terlaris : Form
    {
        string constring;
        int trans_id;
        byte[] arr;
        int qty_temp;
        string temp_nama;
        int temp_harga;
        int makanan_id;
        string nomeja;
        public Menu_Terlaris(string getconstring, int gettransid, string getnomeja)
        {
            InitializeComponent();
            constring = getconstring;
            trans_id = gettransid;
            nomeja = getnomeja;
        }

        private void Menu_Terlaris_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(constring);
            conn.Open();
            string sql = "SELECT TOP 5 dt.daftarMakanan_id, dm.nama_makanan, dm.harga FROM detail_transaksi dt JOIN daftar_makanan dm ON dt.daftarMakanan_id = dm.daftarMakanan_id WHERE dm.kategori LIKE 'Makanan' AND dt.is_commit = 1 GROUP BY dt.daftarMakanan_id, dm.nama_makanan, dm.harga  ORDER BY COUNT(dt.transaksi_id) DESC";
            string sql1 = "SELECT TOP 5 dt.daftarMakanan_id, dm.nama_makanan, dm.harga FROM detail_transaksi dt JOIN daftar_makanan dm ON dt.daftarMakanan_id = dm.daftarMakanan_id WHERE dm.kategori LIKE 'Minuman' AND dt.is_commit = 1 GROUP BY dt.daftarMakanan_id, dm.nama_makanan, dm.harga ORDER BY COUNT(dt.transaksi_id) DESC";
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, conn);
            SqlDataAdapter dataadapter1 = new SqlDataAdapter(sql1, conn);
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            dataadapter.Fill(ds, "daftar_makanan");
            dataadapter1.Fill(ds1, "daftar_makanan");
            dataGridView1.DataSource = ds.Tables["daftar_makanan"].DefaultView;
            dataGridView2.DataSource = ds1.Tables["daftar_makanan"].DefaultView;
            conn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            nama_makanan.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            harga_lbl.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            string sql = "SELECT TOP 5 dt.daftarMakanan_id, dm.nama_makanan, dm.harga, dm.foto FROM detail_transaksi dt JOIN daftar_makanan dm ON dt.daftarMakanan_id = dm.daftarMakanan_id WHERE dm.kategori LIKE 'Makanan' AND dt.is_commit = 1 GROUP BY dt.daftarMakanan_id, dm.nama_makanan, dm.harga, dm.foto ORDER BY COUNT(dt.transaksi_id) DESC";
            SqlCommand cmd1 = new SqlCommand(sql, con);
            
            DataSet table = new DataSet();
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, con);
            dataadapter.Fill(table);
            SqlDataReader read = cmd1.ExecuteReader();
            read.Read();

            makanan_id = read.GetInt32(0);
            //arr= (byte[])(table.Tables[0].Rows[0]["foto"]);
            arr = (byte[])(read[3]);
            MemoryStream ms = new MemoryStream(arr);
            pictureBox1.Image = Image.FromStream(ms);
            pictureBox1.Image = new Bitmap(ms);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            con.Close();

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            nama_makanan.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            harga_lbl.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            string sql = "SELECT TOP 5 dt.daftarMakanan_id, dm.nama_makanan, dm.harga, dm.foto FROM detail_transaksi dt JOIN daftar_makanan dm ON dt.daftarMakanan_id = dm.daftarMakanan_id WHERE dm.kategori LIKE 'Minuman' AND dt.is_commit = 1 GROUP BY dt.daftarMakanan_id, dm.nama_makanan, dm.harga, dm.foto ORDER BY COUNT(dt.transaksi_id) DESC";
            SqlCommand cmd1 = new SqlCommand(sql, con);

            DataSet table = new DataSet();
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, con);
            dataadapter.Fill(table);
            SqlDataReader read = cmd1.ExecuteReader();
            read.Read();

            makanan_id = read.GetInt32(0);
            //arr= (byte[])(table.Tables[0].Rows[0]["foto"]);
            arr = (byte[])(read[3]);
            MemoryStream ms = new MemoryStream(arr);
            pictureBox1.Image = Image.FromStream(ms);
            pictureBox1.Image = new Bitmap(ms);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            con.Close();
        }
        private void plus_Click(object sender, EventArgs e)
        {
            minus.Enabled = true;
            int count = int.Parse(qty.Text);
            count++;
            qty.Text = count.ToString();
        }

        private void minus_Click(object sender, EventArgs e)
        {
            int count = int.Parse(qty.Text);
            if (count == 0)
            {
                minus.Enabled = false;
            }
            else
            {
                count--;
                qty.Text = count.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(qty.Text=="0")
            {
                MessageBox.Show("Tambah Quantity Terlebih Dahulu !");
            }
            else
            {
                int harga = int.Parse(harga_lbl.Text);
                int byk = int.Parse(qty.Text);
                int pricecount = harga * byk;
                bool status = false;
                SqlConnection con = new SqlConnection(constring);
                SqlCommand cmd = new SqlCommand("insert into detail_transaksi values (@id,@daftarMkanan_id, @qty, @price, @status)", con);
                cmd.Parameters.AddWithValue("@id", trans_id);
                cmd.Parameters.AddWithValue("@daftarMkanan_id", makanan_id);
                cmd.Parameters.AddWithValue("@qty", qty.Text);
                cmd.Parameters.AddWithValue("@price", pricecount);
                cmd.Parameters.AddWithValue("@status", status);
                con.Open();
                cmd.ExecuteNonQuery();

                this.Hide();
                new Order(nomeja, constring, trans_id).Show();


                con.Close();
            }
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Order(nomeja, constring, trans_id).Show();
        }
    }
}
