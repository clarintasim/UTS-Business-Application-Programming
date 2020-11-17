using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
namespace WindowsFormsApp1
{
    public partial class printBill : Form
    {
        
        DataSet dt;
        int transid;
        string constring;
        public printBill(DataSet gettable, int gettrnsid, string getconstring)
        {
            InitializeComponent();
            dt = gettable;
            transid = gettrnsid;
            constring = getconstring;
        }

        private void printBill_Load(object sender, EventArgs e)
        {
            //ReportDocument report = new ReportDocument();
            
            //report.Load(@"C:\Users\liuja\source\repos\WindowsFormsApp1\WindowsFormsApp1\CrystalReport1.rpt");
            //report.SetDataSource(dt);
            //crystalReportViewer1.ReportSource = report;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = constring;
            con.Open();
            //string sql = "SELECT dm.nama_makanan as [Nama Makanan], dt.quantity , dt.harga FROM transaksi t join detail_transaksi dt on t.transaksi_id = dt.transaksi_id join daftar_makanan dm on dt.daftarMakanan_id = dm.daftarMakanan_id where t.status_pesanan=1 and t.transaksi_id = '" + transid + "' group by dm.nama_makanan, dt.quantity, dt.harga order by dm.nama_makanan";
            //SqlDataAdapter dscmd = new SqlDataAdapter(sql, con);
            //DataSet ds = new DataSet();
            //dscmd.Fill(ds, "transaksi");
            //con.Close();
            SqlDataAdapter sqlda = new SqlDataAdapter("invoice", con);
            sqlda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlda.SelectCommand.Parameters.AddWithValue("@transid", transid);
            DataSet ds = new DataSet();
            sqlda.Fill(ds);
            con.Close();
            CrystalReport1 objRpt = new CrystalReport1();
            objRpt.SetDataSource(ds.Tables["transaksi"]);
            crystalReportViewer1.ReportSource = objRpt;
            objRpt.SetParameterValue("transid",transid);
            objRpt.SetParameterValue("tanggal", DateTime.Now.ToString("MM/dd/yyyy"));
            objRpt.Database.Tables["Nama Makanan"].SetDataSource(ds.Tables[1]);
            objRpt.Database.Tables["quantity"].SetDataSource(ds.Tables[2]);
            objRpt.Database.Tables["harga"].SetDataSource(ds.Tables[3]);
            crystalReportViewer1.Refresh();
        }
    }
}
