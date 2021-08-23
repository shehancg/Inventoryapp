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
    public partial class salesrecord : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lenovo\OneDrive - NSBM\SEMESTER 3\C#\Inventorymngapp\Inventoryapp\Inventoryapp\inventoryapp.mdf;Integrated Security=True");
        public salesrecord()
        {
            InitializeComponent();
        }

        private void salesrecord_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            display();
        }

        public void display()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from sales";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            bunifuDataGridView1.DataSource = dt;
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
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
                cmd.CommandText = "select * from sales where billdate>='" + startdate.ToString() + "'AND billdate<='" + enddate.ToString() + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                bunifuDataGridView1.DataSource = dt;
                foreach (DataRow dr in dt.Rows)
                {
                    i = i + Convert.ToInt32(dr["total"].ToString());
                }
                label2.Text = i.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            int i = 0;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from sales";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            bunifuDataGridView1.DataSource = dt;
            foreach (DataRow dr in dt.Rows)
            {
                i = i + Convert.ToInt32(dr["total"].ToString());
            }
            label2.Text = i.ToString();
        }
    }
}
