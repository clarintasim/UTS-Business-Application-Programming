using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Kasir : Form
    {
        string constring;
        int transaksi_id;
        DataSet dt;
        string tanggal;
        string transaksiid;
        PrintDocument pdoc = null;
        public Kasir(string getconstring)
        {
            InitializeComponent();
            constring = getconstring;
        }

        private void Kasir_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(constring);
            conn.Open();
            string sql = "SELECT transaksi_id, tanggal_transaksi,total FROM transaksi where status_pesanan=1";
            SqlCommand cmd1 = new SqlCommand(sql, conn);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, conn);
            
            DataSet ds = new DataSet();
            
            dataadapter.Fill(ds, "transaksi");
            

            dataGridView1.DataSource = ds.Tables["transaksi"].DefaultView;
            
            conn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            transaksi_id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            transaksiid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            total_lbl.Text= dataGridView1.CurrentRow.Cells[2].Value.ToString();
            tanggal = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            SqlConnection conn = new SqlConnection(constring);
            conn.Open();
            string sql = "SELECT dm.nama_makanan as [Nama Makanan], dt.quantity , dt.harga FROM transaksi t join detail_transaksi dt on t.transaksi_id = dt.transaksi_id join daftar_makanan dm on dt.daftarMakanan_id = dm.daftarMakanan_id where t.status_pesanan=1 and t.transaksi_id = '"+transaksi_id+"' group by dm.nama_makanan, dt.quantity, dt.harga order by dm.nama_makanan";
            SqlCommand cmd1 = new SqlCommand(sql, conn);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, conn);
            SqlDataAdapter dataadapter1 = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            dt = ds;
            dataadapter.Fill(dt, "transaksi");


            dataGridView2.DataSource = dt.Tables["transaksi"].DefaultView;

            conn.Close();
           

        }

        private void button1_Click(object sender, EventArgs e)
        {

            
            print();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void print()
        {
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Courier New", 15);


            PaperSize psize = new PaperSize("Custom", 100, 200);
            //ps.DefaultPageSettings.PaperSize = psize;



            pd.Document = pdoc;
            pd.Document.DefaultPageSettings.PaperSize = psize;
            //pdoc.DefaultPageSettings.PaperSize.Height =320;
            pdoc.DefaultPageSettings.PaperSize.Height = 820;

            pdoc.DefaultPageSettings.PaperSize.Width = 520;

            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);

            DialogResult result = pd.ShowDialog();
            if (result == DialogResult.OK)
            {
                PrintPreviewDialog pp = new PrintPreviewDialog();
                pp.Document = pdoc;
                result = pp.ShowDialog();
                if (result == DialogResult.OK)
                {
                    pdoc.Print();
                }
            }

        }
        void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 10);
            float fontHeight = font.GetHeight();
            int startX = 50;
            int startY = 55;
            int Offset = 40;
            graphics.DrawString("Bill", new Font("Courier New", 14), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("ID Transaksi:" +transaksiid, new Font("Courier New", 14), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Tanggal :"+tanggal , new Font("Courier New", 12), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            String underLine = "------------------------------------------";
            graphics.DrawString(underLine, new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                int ii = 1;
                ii++;

                graphics.DrawString(" " + dataGridView2.Rows[i].Cells[0].Value + "  " + dataGridView2.Rows[i].Cells[1].Value + "  " + dataGridView2.Rows[i].Cells[2].Value + "", new Font("Courier New", 12), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
            }
            Offset = Offset + 20;
            String underLine1 = "------------------------------------------";
            graphics.DrawString(underLine1, new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Total: "+total_lbl.Text, new Font("Courier New", 14), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            Offset = Offset + 20;
            graphics.DrawString("Thank You and come again!", new Font("Courier New", 14), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;





        }

        private void daftarMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new daftar_menu(constring).Show();
            this.Hide();
        }

        private void laporanPenjualanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Laporan_Penjualan(constring).Show();
            this.Hide();
        }
    }
}
