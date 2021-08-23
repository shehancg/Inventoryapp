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
    public partial class supplier : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lenovo\OneDrive - NSBM\SEMESTER 3\C#\Inventorymngapp\Inventoryapp\Inventoryapp\inventoryapp.mdf;Integrated Security=True");
        public supplier()
        {
            InitializeComponent();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into supply values('" + bunifuTextBox1.Text + "','" + bunifuTextBox2.Text + "','" + bunifuTextBox3.Text + "','" + bunifuTextBox4.Text + "','" + bunifuTextBox5.Text + "')";
            cmd.ExecuteNonQuery();
            bunifuTextBox1.Text = "";
            bunifuTextBox2.Text = "";
            bunifuTextBox3.Text = "";
            bunifuTextBox4.Text = "";
            bunifuTextBox5.Text = "";
            dg();
            MessageBox.Show("Record inserted successfully");
        }

        private void trader_Load(object sender, EventArgs e)
        {
            bunifuButton4.Visible = false;
            if(con.State==ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            dg();
        }
        public void dg()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from supply";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            bunifuDataGridView1.DataSource = dt;
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            int id;
            id = Convert.ToInt32(bunifuDataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from supply where id=" + id + "";
            cmd.ExecuteNonQuery();
            dg();
            MessageBox.Show("Record Successfully Deleted");
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            bunifuButton4.Visible = true;
            int id;
            id = Convert.ToInt32(bunifuDataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from supply where id=" + id + "";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                bunifuTextBox1.Text = dr["suppliername"].ToString();
                bunifuTextBox2.Text = dr["suppliercompany"].ToString();
                bunifuTextBox3.Text = dr["contact"].ToString();
                bunifuTextBox4.Text = dr["city"].ToString();
                bunifuTextBox5.Text = dr["email"].ToString();
            }
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            int id;
            id = Convert.ToInt32(bunifuDataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update supply set suppliername='" + bunifuTextBox1.Text + "',suppliercompany='" + bunifuTextBox2.Text + "',contact='" + bunifuTextBox3.Text + "',city='" + bunifuTextBox4.Text + "',email='" + bunifuTextBox5.Text + "' where id=" + id + "";
            cmd.ExecuteNonQuery();
            dg();
            MessageBox.Show("Updated Successfully");
            bunifuButton4.Visible = false;
            bunifuTextBox1.Clear();
            bunifuTextBox2.Clear();
            bunifuTextBox3.Clear();
            bunifuTextBox4.Clear();
            bunifuTextBox5.Clear();
        }
    }
}
