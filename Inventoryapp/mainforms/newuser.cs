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
    public partial class newuser : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lenovo\OneDrive - NSBM\SEMESTER 3\C#\Inventorymngapp\Inventoryapp\Inventoryapp\inventoryapp.mdf;Integrated Security=True");
        public newuser()
        {
            InitializeComponent();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            int i = 0;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from register where firstname='" + bunifuTextBox1.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());
            if (i == 0)
            {
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "insert into register values('" + bunifuTextBox1.Text + "','" + bunifuTextBox2.Text + "','" + bunifuTextBox3.Text + "','" + bunifuTextBox4.Text + "','" + bunifuTextBox5.Text + "','" + bunifuTextBox6.Text + "')";
                cmd1.ExecuteNonQuery();

                bunifuTextBox1.Text = "";
                bunifuTextBox2.Text = "";
                bunifuTextBox3.Text = "";
                bunifuTextBox4.Text = "";
                bunifuTextBox5.Text = "";
                bunifuTextBox6.Text = "";
                display();
                MessageBox.Show("New Customer added Successfully");
            }
            else
            {
                MessageBox.Show("This Customer already Exist Please Enter new username");
            }

        }

        private void newuser_Load(object sender, EventArgs e)
        {
            bunifuButton4.Visible = false;
            if(con.State==ConnectionState.Open)
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
            cmd.CommandText = "select * from register";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            bunifuDataGridView1.DataSource = dt;
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            int id;
            id = Convert.ToInt32(bunifuDataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from register where id=" + id + "";
            cmd.ExecuteNonQuery();
            display();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            bunifuButton4.Visible = true;
            int id;
            id = Convert.ToInt32(bunifuDataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from register where id=" + id + "";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                bunifuTextBox1.Text = dr["firstname"].ToString();
                bunifuTextBox2.Text = dr["lastname"].ToString();
                bunifuTextBox3.Text = dr["city"].ToString();
                bunifuTextBox4.Text = dr["company"].ToString();
                bunifuTextBox5.Text = dr["email"].ToString();
                bunifuTextBox6.Text = dr["contact"].ToString();
            }
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            int id;
            id = Convert.ToInt32(bunifuDataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update register set firstname='" + bunifuTextBox1.Text + "',lastname='" + bunifuTextBox2.Text + "',city='" + bunifuTextBox3.Text + "',company='" + bunifuTextBox4.Text + "',email='" + bunifuTextBox5.Text + "',contact='" + bunifuTextBox6.Text+"' where id=" + id + "";
            cmd.ExecuteNonQuery();
            display();
            MessageBox.Show("Updated Successfully");
            bunifuButton4.Visible = false;
            bunifuTextBox1.Clear();
            bunifuTextBox2.Clear();
            bunifuTextBox3.Clear();
            bunifuTextBox4.Clear();
            bunifuTextBox5.Clear();
            bunifuTextBox6.Clear();
        }
    }
}
