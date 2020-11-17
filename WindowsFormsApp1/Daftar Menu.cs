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
    public partial class daftar_menu : Form
    {
        string imageurl1 = null;
        string sel;
        byte[] arr;
        int flag;
        string id;
        string constring;
        public daftar_menu(string getconstring)
        {
            InitializeComponent();
            constring = getconstring;
        }

        private void sqlconn()
        {

        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void daftarMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void daftar_menu_Load(object sender, EventArgs e)
        {
            dataGridView1.Enabled = false;
            dataGridView2.Enabled = false;
            edit_btn.Enabled = true;
            tambah_btn.Enabled = true;
            Hapus_btn.Enabled = true;
            simpan_btn.Enabled = false;
            batal_btn.Enabled = false;
            nama_txt.Enabled = false;
            harga_txt.Enabled = false;
            comboBox1.Enabled = false;
            imprt_gbr.Enabled = false;


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
        }

        private void laporanPenjualanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Laporan_Penjualan(constring).Show();
        }

        private void kasirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Kasir(constring).Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            nama_txt.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            harga_txt.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            SqlConnection con = new SqlConnection(constring);
            string queryString = "SELECT [daftarMakanan_id] as id, foto , kategori FROM daftar_makanan where nama_makanan = '"+nama_txt.Text+"';";
            SqlCommand cmd1 = new SqlCommand(queryString, con);
            con.Open();
            DataSet table = new DataSet();
            SqlDataAdapter dataadapter = new SqlDataAdapter(queryString, con);
            dataadapter.Fill(table);
            SqlDataReader read = cmd1.ExecuteReader();
            read.Read();    
            id = read[0].ToString();
            comboBox1.SelectedItem = read[2].ToString();
            sel = comboBox1.Text;
            //arr= (byte[])(table.Tables[0].Rows[0]["foto"]);
            arr = (byte[])(read[1]);
            MemoryStream ms = new MemoryStream(arr);
            pictureBox1.Image = Image.FromStream(ms);
            pictureBox1.Image = new Bitmap(ms);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            con.Close();

        }

        public void clearfield()
        {
            nama_txt.Text = "";
            harga_txt.Text = "";
            comboBox1.SelectedIndex = -1;
            pictureBox1.Image = null;
            

        }

        public void firststate()
        {
            imprt_gbr.Enabled = false;
            edit_btn.Enabled = true;
            tambah_btn.Enabled = true;
            Hapus_btn.Enabled = true;
            simpan_btn.Enabled = false;
            batal_btn.Enabled = false;
            nama_txt.Enabled = false;
            harga_txt.Enabled = false;
            comboBox1.Enabled = false;
            dataGridView1.Enabled = false;
            dataGridView2.Enabled = false;
        }
        private void tambah_btn_Click(object sender, EventArgs e)
        {
            flag = 2;
            dataGridView1.Enabled = true;
            dataGridView2.Enabled = true;
            imprt_gbr.Enabled = true;
            edit_btn.Enabled = false;
            tambah_btn.Enabled = false;
            Hapus_btn.Enabled = false;
            simpan_btn.Enabled = true;
            batal_btn.Enabled = true;
            nama_txt.Enabled = true;
            harga_txt.Enabled = true;
            comboBox1.Enabled = true;
            
        }

        private void dgvrefresh()
        {
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
        }

        private void simpan_btn_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                if(imageurl1!=null)
                {
                    SqlConnection con = new SqlConnection(constring);
                    FileStream fstrm = new FileStream(this.imageurl1, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fstrm);
                    arr = br.ReadBytes((int)fstrm.Length);

                    string queryString = "update daftar_makanan set nama_makanan = '" + nama_txt.Text + "', kategori = '" + sel + "', harga =  convert(int,'" + harga_txt.Text + "'),foto = @gambar where daftarMakanan_id LIKE '"+id+"';";
                    SqlCommand cmd1 = new SqlCommand(queryString, con);
                    con.Open();
                    cmd1.Parameters.AddWithValue("@gambar", arr);
                    cmd1.ExecuteScalar();
                    cmd1.ExecuteNonQuery();
                    DataSet table = new DataSet();
                    SqlDataAdapter dataadapter = new SqlDataAdapter(queryString, con);
                    //dataadapter.Fill(table);
                    MessageBox.Show("Data Tersimpan");
                    dgvrefresh();
                    clearfield();
                    con.Close();
                }
                else
                {
                    SqlConnection con = new SqlConnection(constring);
                    con.Open();
                    MemoryStream ms = new MemoryStream();
                    pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            
                    arr = ms.ToArray();
                    
                    string queryString = "update daftar_makanan set nama_makanan = '" + nama_txt.Text + "', kategori = '" + sel + "', harga =  convert(int,'" + harga_txt.Text + "'),foto = @gambar where daftarMakanan_id LIKE '" + id + "';";

                    SqlCommand cmd1 = new SqlCommand(queryString, con);
                    
                    cmd1.Parameters.AddWithValue("@gambar", arr);
                    cmd1.ExecuteScalar();
                    cmd1.ExecuteNonQuery();
    
                    MessageBox.Show("Data Tersimpan");
                    dgvrefresh();
                    clearfield();
                    con.Close();
                }
            }
            else if(flag==2)
            {
                if (nama_txt != null && harga_txt != null && comboBox1.SelectedItem != null)
                {
                    
                    SqlConnection con = new SqlConnection(constring);


                    FileStream fstrm = new FileStream(this.imageurl1, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fstrm);
                    arr = br.ReadBytes((int)fstrm.Length);

                    string queryString = "SELECT TOP 1 [daftarMakanan_id] as id FROM daftar_makanan ORDER BY daftarMakanan_id DESC";
                    SqlCommand cmd1 = new SqlCommand(queryString, con);
                    con.Open();
                    DataSet table = new DataSet();
                    SqlDataAdapter dataadapter = new SqlDataAdapter(queryString, con);
                    dataadapter.Fill(table);
                    int id;
                    int i = table.Tables[0].Rows.Count;
                    if (i != 0)
                    {
                        //if id is present / not first data
                        SqlDataReader reader = cmd1.ExecuteReader();
                        reader.Read();
                        id = reader.GetInt32(0);
                        SqlCommand cmd = new SqlCommand("insert into daftar_makanan values (@id,@nama_makanan, @kategori, @harga, @gambar)", con);
                        cmd.Parameters.AddWithValue("@id", id + 1);
                        cmd.Parameters.AddWithValue("@nama_makanan", nama_txt.Text);
                        cmd.Parameters.AddWithValue("@harga", harga_txt.Text);
                        cmd.Parameters.AddWithValue("@kategori", comboBox1.Text.ToString());
                        cmd.Parameters.AddWithValue("@gambar", arr);
                        reader.Close();
                        cmd.ExecuteNonQuery();

                        con.Close();
                        MessageBox.Show("Data Tersimpan");
                        clearfield();
                        dgvrefresh();
                    }

                    else
                    {
                        //if id is not found yet
                        id = 1;
                        SqlCommand cmd2 = new SqlCommand("insert into daftar_makanan values (@id,@nama_makanan, @kategori, @harga, @gambar)", con);
                        cmd2.Parameters.AddWithValue("@id", id);
                        cmd2.Parameters.AddWithValue("@nama_makanan", nama_txt.Text);
                        cmd2.Parameters.AddWithValue("@harga", harga_txt.Text);
                        cmd2.Parameters.AddWithValue("@kategori", comboBox1.Text.ToString());
                        cmd2.Parameters.AddWithValue("@gambar", arr);
                        cmd2.ExecuteNonQuery();

                        con.Close();

                        MessageBox.Show("Data Tersimpan");
                        //clearfield();

                    }

                    edit_btn.Enabled = true;
                    tambah_btn.Enabled = true;
                    Hapus_btn.Enabled = true;
                    simpan_btn.Enabled = false;
                    batal_btn.Enabled = false;
                    nama_txt.Enabled = false;
                    harga_txt.Enabled = false;
                    comboBox1.Enabled = false;
                    firststate();
                }
                else
                {
                    //menandakan anda belum mengisi semua required field
                    MessageBox.Show("Ada data yang kurang");
                }
            }
            else
            {
                SqlConnection con = new SqlConnection(constring);
                

                string queryString = "delete from daftar_makanan where daftarMakanan_id LIKE '" + id + "';";
                SqlCommand cmd1 = new SqlCommand(queryString, con);
                con.Open();
                
                cmd1.ExecuteNonQuery();
                DataSet table = new DataSet();
                SqlDataAdapter dataadapter = new SqlDataAdapter(queryString, con);
                //dataadapter.Fill(table);
                MessageBox.Show("Data Tersimpan");
                dgvrefresh();
                clearfield();
                con.Close();
            }
            
        }

        private void imprt_gbr_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    imageurl1 = ofd.FileName;
                    pictureBox1.Image = new Bitmap(imageurl1);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
           sel = comboBox1.Text;
        }

        private void edit_btn_Click(object sender, EventArgs e)
        {
            flag = 1;
            dataGridView1.Enabled = true;
            dataGridView2.Enabled = true;
            imprt_gbr.Enabled = true;
            edit_btn.Enabled = false;
            tambah_btn.Enabled = false;
            Hapus_btn.Enabled = false;
            simpan_btn.Enabled = true;
            batal_btn.Enabled = true;
            nama_txt.Enabled = true;
            harga_txt.Enabled = true;
            comboBox1.Enabled = true;
        }

        private void batal_btn_Click(object sender, EventArgs e)
        {
            firststate();
        }

        private void Hapus_btn_Click(object sender, EventArgs e)
        {
            flag = 3;
            dataGridView1.Enabled = true;
            dataGridView2.Enabled = true;
            imprt_gbr.Enabled = false;
            edit_btn.Enabled = false;
            tambah_btn.Enabled = false;
            Hapus_btn.Enabled = false;
            simpan_btn.Enabled = true;
            batal_btn.Enabled = true;
            nama_txt.Enabled = false;
            harga_txt.Enabled = false;
            comboBox1.Enabled = false;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            nama_txt.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            harga_txt.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();

            SqlConnection con = new SqlConnection(constring);
            string queryString = "SELECT [daftarMakanan_id] as id, foto , kategori FROM daftar_makanan where nama_makanan = '" + nama_txt.Text + "';";
            SqlCommand cmd1 = new SqlCommand(queryString, con);
            con.Open();
            DataSet table = new DataSet();
            SqlDataAdapter dataadapter = new SqlDataAdapter(queryString, con);
            dataadapter.Fill(table);
            SqlDataReader read = cmd1.ExecuteReader();
            read.Read();
            id = read[0].ToString();
            comboBox1.SelectedItem = read[2].ToString();
            sel = comboBox1.Text;
            arr = (byte[])(read[1]);
            MemoryStream ms = new MemoryStream(arr);
            pictureBox1.Image = Image.FromStream(ms);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            con.Close();
        }
    }
}
