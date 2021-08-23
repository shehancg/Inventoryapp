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
    public partial class saleslist : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lenovo\OneDrive - NSBM\SEMESTER 3\C#\Inventorymngapp\Inventoryapp\Inventoryapp\inventoryapp.mdf;Integrated Security=True");
        DataTable dt = new DataTable();
        int tot = 0;
        public saleslist()
        {
            InitializeComponent();
        }

        private void saleslist_Load(object sender, EventArgs e)
        {
            if(con.State==ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            customerfill();
            dt.Clear();
            dt.Columns.Add("products");
            dt.Columns.Add("price");
            dt.Columns.Add("quantity");
            dt.Columns.Add("total");

        }

        //fill customer dropdownmenu
        public void customerfill()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from register";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                bunifuDropdown2.Items.Add(dr["firstname"].ToString());
            }
        }

        //change of last name label after first name is selected
        private void bunifuDropdown2_Leave(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from register where firstname='" + bunifuDropdown2.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                label11.Text = dr["lastname"].ToString();
            }
        }

        //what happens when typed on product textbox 
        private void bunifuTextBox3_KeyUp(object sender, KeyEventArgs e)
        {
            Products.Visible = true;
            Products.Items.Clear();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from stock where productname like('" + bunifuTextBox3.Text + "%')";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                Products.Items.Add(dr["productname"].ToString());
            }
        }

        //change of focus to list box
        private void bunifuTextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Down)
            {
                Products.Focus();
                Products.SelectedIndex=0;
            }
        }

        //how to select data from list box
        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if(e.KeyCode==Keys.Down)
                {
                    this.Products.SelectedIndex = this.Products.SelectedIndex + 1;
                }
                if(e.KeyCode==Keys.Up)
                {
                    this.Products.SelectedIndex = this.Products.SelectedIndex - 1;
                }
                if(e.KeyCode==Keys.Enter)
                {
                    bunifuTextBox3.Text = Products.SelectedItem.ToString();
                    Products.Visible = false;
                    bunifuTextBox4.Focus();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        //getting price for the selected product
        private void bunifuTextBox4_Enter(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 1 * from purchase where productname='" + bunifuTextBox3.Text + "'order by id desc";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                bunifuTextBox4.Text = dr["productprice"].ToString();
            }
        }

        //assigning total price for total text box
        private void bunifuTextBox5_Leave(object sender, EventArgs e)
        {
            try
            {
                bunifuTextBox6.Text = Convert.ToString(Convert.ToInt32(bunifuTextBox4.Text) * Convert.ToInt32(bunifuTextBox5.Text));
            }
            catch(Exception)
            {
                throw;
            }
        }

        //what happens when Add button is clicked
        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            int stock = 0;
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandText = "select * from stock where productname='" + bunifuTextBox3.Text + "'";
            cmd1.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            da1.Fill(dt1);
            foreach(DataRow dr1 in dt1.Rows)
            {
                stock = Convert.ToInt32(dr1["productqty"].ToString());               
            }
            if(Convert.ToInt32(bunifuTextBox5.Text)>stock)
            {
                MessageBox.Show("Insuffiecient Stock");
            }
            else
            {                
                //data addition to permanet db
                SqlCommand cmd3 = con.CreateCommand();
                cmd3.CommandType = CommandType.Text;
                cmd3.CommandText = "insert into sales values('" + bunifuDropdown2.Text + "','" + label11.Text + "','" + bunifuDropdown1.Text + "','" + bunifuDatePicker1.Value.ToString("dd-MM-yyyy") + "','" + bunifuTextBox3.Text + "','" + bunifuTextBox4.Text + "','" + bunifuTextBox5.Text + "','" + bunifuTextBox6.Text + "')";
                cmd3.ExecuteNonQuery();
                MessageBox.Show("Purchase Successfull");

                
                //data addition to temporary datatable
                DataRow dr = dt.NewRow();
                dr["products"] = bunifuTextBox3.Text;
                dr["price"] = bunifuTextBox4.Text;
                dr["quantity"] = bunifuTextBox5.Text;
                dr["total"] = bunifuTextBox6.Text;
                dt.Rows.Add(dr);
                bunifuDataGridView1.DataSource = dt;
                tot = tot + Convert.ToInt32(dr["total"].ToString());
                label10.Text = tot.ToString();

                bunifuTextBox3.Clear();
                bunifuTextBox4.Clear();
                bunifuTextBox5.Clear();
                bunifuTextBox6.Clear();

                //decrease purchased stock amount from stock table
                foreach (DataRow dr1 in dt1.Rows)
                {
                    int qty = 0;
                    string productnm = "";
                    qty = Convert.ToInt32(dr["quantity"].ToString());
                    productnm = dr1["productname"].ToString();

                    SqlCommand cmd4 = con.CreateCommand();
                    cmd4.CommandType = CommandType.Text;
                    cmd4.CommandText = "update stock set productqty=productqty-" + qty + "where productname='" + productnm.ToString() + "'";
                    cmd4.ExecuteNonQuery();
                }
            }
        }

        //delete of selected sales
        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            try
            {
                /*int id;
                id = Convert.ToInt32(bunifuDataGridView1.SelectedCells[0].Value.ToString());
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from sales where id=" + id + "";
                Console.WriteLine(cmd.CommandText);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Successfully Deleted");*/

                tot = 0;
                dt.Rows.RemoveAt(Convert.ToInt32(bunifuDataGridView1.CurrentCell.RowIndex.ToString()));
                foreach(DataRow dr1 in dt.Rows)
                {
                    tot = tot + Convert.ToInt32(dr1["total"].ToString());
                    label10.Text = tot.ToString();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        //return button
        private void bunifuButton4_Click(object sender, EventArgs e)
        {

        }
    }
}
