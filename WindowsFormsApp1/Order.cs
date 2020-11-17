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
    public partial class Order : Form
    {
        string nomeja;
        string constring;
        int trans_id;
        byte[] arr;
        int qty_temp;
        string temp_nama;
        int temp_harga;
        int makanan_id;
        public Order(string getnomeja, string getconstring, int gettransid)
        {
            InitializeComponent();
            nomeja = getnomeja;
            constring = getconstring;
            trans_id = gettransid;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Order_Load(object sender, EventArgs e)
        {
            timer1.Start();
            this.no_meja.Text = nomeja;
            tambahPesanan_btn.Enabled = false;
            hapus_btn.Enabled = false;
            pesan_btn.Enabled = false;
            minus.Enabled = false;
            plus.Enabled = false;
            

            SqlConnection conn = new SqlConnection(constring);
            conn.Open();
            string sql = "SELECT nama_makanan as [Nama Makanan], harga FROM daftar_makanan where kategori='Makanan'";
            string sql1 = "SELECT nama_makanan as [Nama Minuman], harga FROM daftar_makanan where kategori='Minuman'";
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, conn);
            SqlDataAdapter dataadapter1 = new SqlDataAdapter(sql1, conn);
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            dataadapter.Fill(ds, "daftar_makanan");
            dataadapter1.Fill(ds1, "daftar_makanan");
            dataGridView1.DataSource = ds.Tables["daftar_makanan"].DefaultView;
            dataGridView2.DataSource = ds1.Tables["daftar_makanan"].DefaultView;
            conn.Close();
            dgv3refresh();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            this.label1.Text = datetime.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(qty.Text=="0")
            {
                minus.Enabled = false;
                plus.Enabled = true;
            }
            else
            {
                minus.Enabled = true;
                plus.Enabled = true;
            }
            tambahPesanan_btn.Enabled = true;
            hapus_btn.Enabled = false;
            pesan_btn.Enabled = true;
            
            nama_makanan.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            harga_lbl.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            SqlConnection con = new SqlConnection(constring);
            string queryString = "SELECT [daftarMakanan_id] as id, foto , kategori FROM daftar_makanan where nama_makanan = '" + nama_makanan.Text + "';";
            SqlCommand cmd1 = new SqlCommand(queryString, con);
            con.Open();
            DataSet table = new DataSet();
            SqlDataAdapter dataadapter = new SqlDataAdapter(queryString, con);
            dataadapter.Fill(table);
            SqlDataReader read = cmd1.ExecuteReader();
            read.Read();

            makanan_id = read.GetInt32(0);
            //arr= (byte[])(table.Tables[0].Rows[0]["foto"]);
            arr = (byte[])(read[1]);
            MemoryStream ms = new MemoryStream(arr);
            pictureBox1.Image = Image.FromStream(ms);
            pictureBox1.Image = new Bitmap(ms);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            con.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (qty.Text == "0")
            {
                minus.Enabled = false;
                plus.Enabled = true;
            }
            else
            {
                minus.Enabled = true;
                plus.Enabled = true;
            }
            tambahPesanan_btn.Enabled = true;
            hapus_btn.Enabled = false;
            pesan_btn.Enabled = true;

            nama_makanan.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            harga_lbl.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();

            SqlConnection con = new SqlConnection(constring);
            string queryString = "SELECT [daftarMakanan_id] as id, foto , kategori FROM daftar_makanan where nama_makanan = '" + nama_makanan.Text + "';";
            SqlCommand cmd1 = new SqlCommand(queryString, con);
            con.Open();
            DataSet table = new DataSet();
            SqlDataAdapter dataadapter = new SqlDataAdapter(queryString, con);
            dataadapter.Fill(table);
            SqlDataReader read = cmd1.ExecuteReader();
            read.Read();

            makanan_id = read.GetInt32(0);
            //arr= (byte[])(table.Tables[0].Rows[0]["foto"]);
            arr = (byte[])(read[1]);
            MemoryStream ms = new MemoryStream(arr);
            pictureBox1.Image = Image.FromStream(ms);
            pictureBox1.Image = new Bitmap(ms);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            con.Close();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            tambahPesanan_btn.Enabled = false;
            hapus_btn.Enabled = true;
            pesan_btn.Enabled = true;
            temp_nama = dataGridView3.CurrentRow.Cells[0].Value.ToString();
            temp_harga = int.Parse(dataGridView3.CurrentRow.Cells[2].Value.ToString());
            qty_temp = int.Parse(dataGridView3.CurrentRow.Cells[1].Value.ToString());
            
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
            if(count==0)
            {
                minus.Enabled = false;
            }
            else
            {
                count--;
                qty.Text = count.ToString();
            }
        }
        public void dgv3refresh()
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            string queryString = "SELECT dm.nama_makanan as Nama_Makanan, dt.quantity as QTY, dt.harga as harga FROM detail_transaksi dt INNER JOIN daftar_makanan dm ON dt.daftarMakanan_id = dm.daftarMakanan_id where dt.transaksi_id = '"+trans_id+"' and dt.is_commit = 0";
            //string queryString = "select nama_makanan, qty, price_temp from temp_table where transaksi_id ='"+trans_id+"';";
            SqlCommand cmd1 = new SqlCommand(queryString, con);
            
            DataSet table = new DataSet();
            SqlDataAdapter dataadapter = new SqlDataAdapter(queryString, con);
            DataSet ds = new DataSet();
            dataadapter.Fill(ds, "detail_transaksi");
            dataGridView3.DataSource = ds.Tables["detail_transaksi"].DefaultView;
            string queryString1 = "SELECT SUM(harga) as price FROM detail_transaksi WHERE transaksi_id LIKE '" + trans_id + "';";
            SqlCommand cmd = new SqlCommand(queryString1, con);
            SqlDataReader read = cmd.ExecuteReader();
            read.Read();
            string sum = read[0].ToString();
            total_pesan.Text = sum;
            con.Close();

        }

        private void tambahPesanan_btn_Click(object sender, EventArgs e)
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

                con.Close();

                dgv3refresh();

                
            }
            

        }

        private void hapus_btn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            string queryString = "delete from detail_transaksi where transaksi_id ='" + trans_id + "' and daftarMakanan_id = '" + makanan_id + "' and quantity = '" + qty_temp + "' and harga='" + temp_harga + "'and is_commit = 0";
            SqlCommand cmd1 = new SqlCommand(queryString, con);
            cmd1.ExecuteNonQuery();
            con.Close();
            dgv3refresh();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Menu_Terlaris(constring,trans_id,nomeja).Show();
        }

        private void pesan_btn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("update detail_transaksi set is_commit = 1 WHERE is_commit = 0 AND transaksi_id = '"+trans_id+"'", con);
            SqlCommand cmd1 = new SqlCommand("update transaksi set total = '"+total_pesan.Text+"' WHERE transaksi_id = '" + trans_id + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd1.ExecuteNonQuery();
            MessageBox.Show("Pesanan sedang di proses");
            con.Close();
            dgv3refresh();
            clearfield();
           
           

            
        }
        public void clearfield()
        {
            nama_makanan.Text = "";
            harga_lbl.Text = "";
            pictureBox1.Image = null;
            qty.Text = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new myBill(nomeja, constring, trans_id).Show();
            this.Hide();
        }

        private void back_btn_Click(object sender, EventArgs e)
        {
            new Menu(nomeja, constring, trans_id).Show();
            this.Hide();
        }
    }
}
