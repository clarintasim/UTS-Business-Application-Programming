using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Admin_Control : Form
    {
        string constring;
        public Admin_Control(string getconstring)
        {
            InitializeComponent();
            constring = getconstring;
        }

        private void DaftarMenu_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            new daftar_menu(constring).Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Kasir(constring).Show();
        }

        private void LapPenjualan_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Laporan_Penjualan(constring).Show();
        }
    }
}
