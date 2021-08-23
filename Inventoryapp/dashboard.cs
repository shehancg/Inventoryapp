using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using FontAwesome.Sharp;
using System.Windows.Forms;
using Inventoryapp.mainforms;

namespace Inventoryapp
{
    public partial class dashboard : Form
    {
        //fields
        private IconButton currentbtn;
        private Panel leftborderbtn;
        private Form currentchildform;

        //constructor       
        public dashboard()
        {
            InitializeComponent();
            leftborderbtn = new Panel();
            leftborderbtn.Size = new Size(7, 40);
            Panel1.Controls.Add(leftborderbtn);
            //
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }
        //structs
        private struct rgbcolors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
            public static Color color7 = Color.FromArgb(172, 126, 241);
        }
        private void activatebutton(object senderbtn, Color color)
        {
            if (senderbtn != null)
            {
                diasblebutton();
                //
                currentbtn = (IconButton)senderbtn;
                currentbtn.BackColor = Color.FromArgb(39, 41, 61);
                currentbtn.ForeColor = color;
                currentbtn.TextAlign = ContentAlignment.MiddleCenter;
                currentbtn.IconColor = color;
                currentbtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentbtn.ImageAlign = ContentAlignment.MiddleRight;
                //left border button
                leftborderbtn.BackColor = color;
                leftborderbtn.Location = new Point(0, currentbtn.Location.Y);
                leftborderbtn.Visible = true;
                leftborderbtn.BringToFront();
                //current icon 
                currenttitleicon.IconChar = currentbtn.IconChar;
                currenttitleicon.IconColor = color;
                currenttitle.Text = currentbtn.Text;
            }
        }
        private void diasblebutton()
        {
            if (currentbtn != null)
            {
                currentbtn.BackColor = Color.Transparent;
                currentbtn.ForeColor = Color.White;
                currentbtn.TextAlign = ContentAlignment.MiddleLeft;
                currentbtn.IconColor = Color.White;
                currentbtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentbtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        //childforms
        private void openchildform(Form childform)
        {
            if (titlebar != null)
            {
                //open only one form
                currentchildform = childform;
                childform.TopLevel = false;
                childform.FormBorderStyle = FormBorderStyle.None;
                childform.Dock = DockStyle.Fill;
                homepanel.Controls.Add(childform);
                childform.BringToFront();
                childform.Show();
                //all forms color change
                childform.BackColor = Color.FromArgb(39, 41, 61);
            }
        }

        private void customerbutton_Click_1(object sender, EventArgs e)
        {
            activatebutton(sender, rgbcolors.color1);
            openchildform(new newuser());
        }

        private void productbutton_Click(object sender, EventArgs e)
        {
            activatebutton(sender, rgbcolors.color2);
            openchildform(new productlist());
        }

        private void salesbutton_Click(object sender, EventArgs e)
        {
            activatebutton(sender, rgbcolors.color3);
            openchildform(new salesrecord());
        }

        private void addstockbutton_Click(object sender, EventArgs e)
        {
            activatebutton(sender, rgbcolors.color4);
            openchildform(new purchase());
        }

        private void categorybutton_Click(object sender, EventArgs e)
        {
            activatebutton(sender, rgbcolors.color5);
            openchildform(new saleslist());
        }

        private void deletedbutton_Click(object sender, EventArgs e)
        {
            activatebutton(sender, rgbcolors.color6);
            openchildform(new purchasereport());
        }

        private void alertbutton_Click(object sender, EventArgs e)
        {
            activatebutton(sender, rgbcolors.color1);
            openchildform(new supplier());
        }

        private void exitbutton_Click(object sender, EventArgs e)
        {
            //activatebutton(sender, rgbcolors.color7);
            Application.Exit();
        }
        private void exitbutton_MouseMove(object sender, MouseEventArgs e)
        {
            exitbutton.BackColor = Color.FromArgb(222, 49, 99);
        }
        private void exitbutton_MouseLeave(object sender, EventArgs e)
        {
            exitbutton.BackColor = Color.Transparent;
        }

        private void homebtn_Click(object sender, EventArgs e)
        {
            currentchildform.Close();
            reset();
        }

        private void reset()
        {
            diasblebutton();
            leftborderbtn.Visible = false;
            currenttitleicon.IconChar = IconChar.Home;
            currenttitleicon.IconColor = Color.White;
            currenttitle.Text = "Home";
        }
        //Drag form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int WMsg, int wParam, int lParam);
        private void titlebar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
