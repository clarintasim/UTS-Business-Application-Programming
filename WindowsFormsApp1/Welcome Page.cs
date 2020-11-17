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
    public partial class Welcome_Page : Form
    {
        string nomeja;
        string conString;
        public Welcome_Page()
        {
            InitializeComponent();
            conString = @"Data Source=LAPTOP-O41MMOBN\SQL2019EXPRESS;Initial Catalog=aplikasiKasir;Integrated Security=True";
        }

        private void Home_Page_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            this.label3.Text = datetime.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nomeja = textBox1.Text.ToString();
            SqlConnection con = new SqlConnection(conString);
            string query = "SELECT TOP 1 [transaksi_id] as id FROM transaksi ORDER BY transaksi_id DESC";

            //string queryString = "INSERT INTO transaksi (transaksi_id, status_pesanan) VALUES (1, 0)";
            SqlCommand cmd1 = new SqlCommand(query, con);

            con.Open();
            DataSet table = new DataSet();
            SqlDataAdapter dataadapter = new SqlDataAdapter(query, con);
            dataadapter.Fill(table);
            int id;
            bool status = false;
            int i = table.Tables[0].Rows.Count;
            if (i != 0)
            {
                //if id is present / not first data
                SqlDataReader reader = cmd1.ExecuteReader();
                reader.Read();
                id = reader.GetInt32(0);
                int newid = id + 1;
                SqlCommand cmd = new SqlCommand("insert into transaksi (transaksi_id, status_pesanan) values (@id,@status_pesanan)", con);
                cmd.Parameters.AddWithValue("@id", newid);
                cmd.Parameters.AddWithValue("@status_pesanan", status);
                reader.Close();
                cmd.ExecuteNonQuery();

                con.Close();
                new Menu(nomeja, conString, newid).Show();
                this.Hide();
            }
            else
            {
                //if id is not found yet
                id = 1;
                SqlCommand cmd2 = new SqlCommand("insert into transaksi (transaksi_id, status_pesanan) values (@id,@status_pesanan)", con);
                cmd2.Parameters.AddWithValue("@id", id);
                cmd2.Parameters.AddWithValue("@status_pesanan", status);

                cmd2.ExecuteNonQuery();
                con.Close();
                new Menu(nomeja, conString, id).Show();
                this.Hide();
            }

        }

        private void Welcome_Page_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode.ToString() == "F2")
            {
                this.Hide();
                new Admin_Control(conString).Show();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F2")
            {
                this.Hide();
                new Admin_Control(conString).Show();
            }
        }
    }
}
