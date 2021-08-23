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
    public partial class purchase : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lenovo\OneDrive - NSBM\SEMESTER 3\C#\Inventorymngapp\Inventoryapp\Inventoryapp\inventoryapp.mdf;Integrated Security=True");
        public purchase()
        {
            InitializeComponent();
        }

        private void purchase_Load(object sender, EventArgs e)
        {
            if(con.State==ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            fill_product();
            fill_suppliername();
        }

        public void fill_product()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from products";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                bunifuDropdown1.Items.Add(dr["productname"].ToString());
            }
        }

        private void bunifuDropdown1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from products where productname='"+bunifuDropdown1.Text+"'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                label11.Text = dr["units"].ToString(); 
            }
        }

        public void fill_suppliername()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from supply";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                bunifuDropdown2.Items.Add(dr["suppliername"].ToString());
            }
        }
        private void bunifuTextBox2_Leave_1(object sender, EventArgs e)
        {
            label12.Text = Convert.ToString(Convert.ToInt32(bunifuTextBox1.Text) * Convert.ToInt32(bunifuTextBox2.Text));
        }


        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            int i;
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "select * from stock where productname='" + bunifuDropdown1.Text + "'";
            cmd1.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            da1.Fill(dt1);
            i = Convert.ToInt32(dt1.Rows.Count.ToString());
            if(i==0)
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into purchase values('" + bunifuDropdown1.Text + "','" + bunifuTextBox1.Text + "','" + label11.Text + "','" + bunifuTextBox2.Text + "','" + label12.Text + "','" + bunifuDatePicker1.Value.ToString("dd-MM-yyyy") + "','" + bunifuDropdown2.Text + "','" + bunifuDropdown3.Text + "','" + bunifuTextBox3.Text + "','" + bunifuTextBox4.Text + "','" + bunifuDatePicker2.Value.ToString("dd-MM-yyyy") +"')";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Purchase Successfull");

                SqlCommand cmd3 = con.CreateCommand();
                cmd3.CommandType = CommandType.Text;
                cmd3.CommandText = "insert into stock values('" + bunifuDropdown1.Text + "','" + bunifuTextBox1.Text + "','" + label11.Text + "')";
                cmd3.ExecuteNonQuery();
            }
            else
            {
                SqlCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "insert into purchase values('" + bunifuDropdown1.Text + "','" + bunifuTextBox1.Text + "','" + label11.Text + "','" + bunifuTextBox2.Text + "','" + label12.Text + "','" + bunifuDatePicker1.Value.ToString("dd-MM-yyyy") + "','" + bunifuDropdown2.Text + "','" + bunifuDropdown3.Text + "','" + bunifuTextBox3.Text + "','" + bunifuTextBox4.Text + "','" + bunifuDatePicker2.Value.ToString("dd-MM-yyyy") + "')";
                cmd2.ExecuteNonQuery();
                MessageBox.Show("Purchase Successfull");

                SqlCommand cmd4 = con.CreateCommand();
                cmd4.CommandType = CommandType.Text;
                cmd4.CommandText = "update stock set productqty=productqty+" + bunifuTextBox1.Text + "where productname='" + bunifuDropdown1.Text + "'";
                cmd4.ExecuteNonQuery();
                MessageBox.Show("Purchase Successfull");
            }           
        }        
    }
}
