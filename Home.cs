/**
*   this code concatenates the information obtained from the Keyence IV2-50P system and moves the other folder.
*   @Param
*        --identify txt and jpg files from a specific folder
*   @Return
*        --rename and move files to folder
*   @Version 14.04.23
*   @sinse 07-04-2023
*   @author <jose.israel.cosme@aptiv.com>
*/
//------------ Using Library ------------
using INIFILE;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection.Emit;
using System.IO;
//---------------------------------------
namespace ScaningSystem
{
    public partial class Home : Form
    {



        INIFile file = new INIFile(Global.Global_V.PhatINIconstructor);
        Global.DBConect dbObject = new Global.DBConect();


        //------------ Import dll Using for Hide Console ------------
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        //----------------- Crear to restart proces -----------------
        public void Clear()
        {
            label1.BackColor = Color.White;
            // label1.Text = "Esperando Escaneo...";
            pictureBox1.Visible = true;
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox1.Focus();
        }
        //-----------------------------------------------------------
        //---------------------- First Scaning ----------------------
        public void Process()
        {
            Global.Global_V.BarCode_2 = textBox2.Text;
            Global.Global_V.BarCode_1 = textBox1.Text;
            label5.Text = Global.Global_V.BarCode_1;
            if (Global.Global_V.BarCode_1 != Global.Global_V.BarCode_2)
            {
                this.Hide();
                using (Cross frm = new Cross())
                { frm.ShowDialog(); }
                WindowState = FormWindowState.Normal;
                label1.Text = "Barcode no coincide";
                label1.BackColor = Color.Red;
                pictureBox1.Visible = false; //picture wait 
                textBox2.Focus();
                Global.Global_V.BarCode_2 = "";
                textBox2.Text = Global.Global_V.BarCode_2;
            }
        }
        //-----------------------------------------------------------
        public Home()
        {
            InitializeComponent();
        }

        //------------------ Action to Start form -------------------
        private void Form1_Load(object sender, EventArgs e)
        {
            if (file.Read("Setting", "DinoID") == "")
            {
                using (History frm = new History())
                { frm.ShowDialog(); }
                this.Close();
            }

            if (file.Read("Shift", "ShiftA") == "enable")
            { label12.Text = "A"; }
            if (file.Read("Shift", "ShiftB") == "enable")
            { label12.Text = "B"; }
            if (file.Read("Shift", "ShiftC") == "enable")
            { label12.Text = "C"; }


            Global.Global_V.Shift_A = file.Read("Setting", "ShiftA");
            Global.Global_V.Shift_B = file.Read("Setting", "ShiftB");
            Global.Global_V.Shift_C = file.Read("Setting", "ShiftC");
            timer1.Enabled = true;
            label9.Text = Global.Global_V.Operator;
            Global.DBConect dbObject = new Global.DBConect();
            label6.Text = Global.Global_V.V;
            textBox1.Focus();
            TopMost = false;
            Global.Global_V.ZCode = string.Empty;
            Global.Global_V.Date = string.Empty;
            Global.Global_V.Time = string.Empty;
            Global.Global_V.Meridiam = string.Empty;
            Global.Global_V.Console = file.Read("Setting", "ShowConsole");
            const int SW_HIDE = 0;
            const int SW_SHOW = 5;
            var handle = GetConsoleWindow();
            if (Global.Global_V.Console == "1")
            {
                ShowWindow(handle, SW_HIDE);//Hide to start Console
            }
            else
            {
                ShowWindow(handle, SW_SHOW);//To show to estar Console
            }

            if (Global.Global_V.CountToReset == 2)
            {
                Application.Restart();
                Thread.Sleep(100);
                Application.Exit();
            }
            WindowState = FormWindowState.Maximized;
            Clear();
        }
        //-----------------------------------------------------------

        //---------------- Wait scaning ining info ------------------
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        { if (e.KeyChar == Convert.ToChar(Keys.Enter)) Process(); }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        { if (e.KeyChar == (char)Keys.Enter) { Process(); } }
        private void Form1_KeyPress_1(object sender, KeyPressEventArgs e)
        { textBox1.Focus(); }
        //-----------------------------------------------------------

        private void pictureBox6_MouseDoubleClick(object sender, MouseEventArgs e)
        { }
        private void pictureBox4_MouseDoubleClick(object sender, MouseEventArgs e)
        { }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            using (History frm = new History())
            { frm.ShowDialog(); }
            this.Close();

        }



        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            String Meridian = DateTime.Now.ToString("tt");
            file.Write("Shift", "Meridian", Meridian);
            DateTime time = DateTime.Now;
            label10.Text = "";


            string timeR = DateTime.Now.ToString("%H");

            int TimeR = Int32.Parse(timeR);


            //Shift A_
            //TimeR >= 6 && TimeR <= 15

            if (TimeR >= 6 && TimeR <= 15 && file.Read("Shift", "ShiftA") == "disable")
            {   
                label12.Text = "A";
                file.Write("Shift", "ShiftA", "enable");
                file.Write("Shift", "ShiftB", "disable");
                file.Write("Shift", "ShiftC", "disable");
                if (file.Read("Setting", "DinoID") != "")
                { file.Write("Setting", "DinoID", ""); Application.Restart(); }
            }


            if (TimeR >= 16 && TimeR <= 23 && file.Read("Shift", "ShiftB") == "disable")
            {
                label12.Text = "B";
                file.Write("Shift", "ShiftA", "disable");
                file.Write("Shift", "ShiftB", "enable");
                file.Write("Shift", "ShiftC", "disable");
                if (file.Read("Setting", "DinoID") != "")
                { file.Write("Setting", "DinoID", ""); Application.Restart(); }

            }

            if (TimeR <= 5 && file.Read("Shift", "ShiftC") == "disable")
            {
                label12.Text = "C";
                file.Write("Shift", "ShiftA", "disable");
                file.Write("Shift", "ShiftB", "disable");
                file.Write("Shift", "ShiftC", "enable");
                if (file.Read("Setting", "DinoID") != "")
                {  file.Write("Setting", "DinoID", "");Application.Restart(); }
            }
        }

            private void label13_DoubleClick(object sender, EventArgs e)
            {
                Global.Global_V.Operator = ""; file.Write("Setting", "DinoID", ""); Application.Restart();
            }

            private void pictureBox3_DoubleClick(object sender, EventArgs e)
            {
                Application.Exit();
            }
        }
    }
