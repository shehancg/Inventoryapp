using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Inventoryapp
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private bool isvalidlogin()
        {
            if (adminTextBox1.Text.Trim() == string.Empty | adminTextBox1.Text.Length < 5)
            {
                MessageBox.Show("Admin Name is incorrect!", "Error!");
                return false;
            }
            else if (passwordTextBox2.Text.TrimStart() == string.Empty | passwordTextBox2.Text.Length < 8)
            {
                MessageBox.Show("Password is incorrect", "Error!");
                return false;
            }
            return true;
        }

        private void loginbutton_Click(object sender, EventArgs e)
        {
            if (isvalidlogin())
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lenovo\OneDrive - NSBM\SEMESTER 3\C#\Inventorymngapp\Inventoryapp\Inventoryapp\inventoryapp.mdf;Integrated Security=True"))
                {
                    string query = "SELECT * FROM login WHERE adminname='" + adminTextBox1.Text.Trim() + "'AND adminpassword='" + passwordTextBox2.Text.Trim() + "'";
                    SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                    DataTable dta = new DataTable();
                    sda.Fill(dta);
                    if (dta.Rows.Count == 1)
                    {
                        dashboard dashboard = new dashboard();
                        this.Hide();
                        dashboard.Show();
                    }
                }
            }
        }
        private void exitbutton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
