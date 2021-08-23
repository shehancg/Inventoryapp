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

namespace Inventoryapp.mainforms
{
    public partial class generatepurchaserepot : Form
    {
        string j;
        int tot = 0;

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lenovo\OneDrive - NSBM\SEMESTER 3\C#\Inventorymngapp\Inventoryapp\Inventoryapp\inventoryapp.mdf;Integrated Security=True");
        
        public void Getvalue(string i)
        {
            j = i;
        }

        public generatepurchaserepot()
        {
            InitializeComponent();
        }

        private void generatepurchaserepot_Load(object sender, EventArgs e)
        {
            if(con.State==ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

            DataSet1 ds = new DataSet1();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from purchase where id='" + j + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds.DataTable1);

            tot = 0;
            foreach(DataRow dr in dt.Rows)
            {
                tot = tot + Convert.ToInt32(dr["total"].ToString());

            }
            CrystalReport1 myreport = new CrystalReport1();
            myreport.SetDataSource(ds);
            myreport.SetParameterValue("Total", tot.ToString());
            crystalReportViewer1.ReportSource = myreport;
        }
    }
}
