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
    public partial class Menu : Form
    {
        string nomeja;
        string constring;
        int transid;
        public Menu(string getnomeja, string  getconstring, int gettransid)
        {
            InitializeComponent();
            nomeja = getnomeja;
            constring = getconstring;
            transid = gettransid;
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            no_meja.Text = nomeja;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            this.Tanggal.Text = datetime.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Order(nomeja,constring,transid).Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("update transaksi set status_pesanan = 1 WHERE transaksi_id = '" + transid + "'", con);

            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Silahkan Menuju Ke Kasir");
            con.Close();
            new Welcome_Page().Show();
            this.Hide();
           
        }
    }
}
