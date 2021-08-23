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
    public partial class purchasereport : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lenovo\OneDrive - NSBM\SEMESTER 3\C#\Inventorymngapp\Inventoryapp\Inventoryapp\inventoryapp.mdf;Integrated Security=True");
        string query = "";
        public purchasereport()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            gridviewfill();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            int i = 0;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from purchase";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            bunifuDataGridView1.DataSource = dt;
            foreach(DataRow dr in dt.Rows)
            {
                i = i + Convert.ToInt32(dr["producttotoal"].ToString());
            }
            label2.Text = i.ToString();

            query = "select * from purchase";
        }

        private void gridviewfill()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from purchase";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            bunifuDataGridView1.DataSource = dt;
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string startdate;
                string enddate;

                startdate = bunifuDatePicker1.Value.ToString("dd/MM/yyyy");
                enddate = bunifuDatePicker2.Value.ToString("dd/MM/yyyy");

                int i = 0;
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from purchase where purchasedate>='" + startdate.ToString() + "'AND purchasedate<='" + enddate.ToString() + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                bunifuDataGridView1.DataSource = dt;
                foreach (DataRow dr in dt.Rows)
                {
                    i = i + Convert.ToInt32(dr["producttotoal"].ToString());
                }
                label2.Text = i.ToString();

                query = "select * from purchase where purchasedate>='" + startdate.ToString() + "'AND purchasedate<='" + enddate.ToString() + "'";
            }
           catch(Exception)
            {
                throw;
            }
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            generatepurchaserepot gpr = new generatepurchaserepot();
            gpr.Getvalue(query.ToString());
            gpr.Show();
        }
    }
}
