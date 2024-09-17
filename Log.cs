using INIFILE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScaningSystem
{
    public partial class Log : Form
    {
        //------------ Import dll Using for Hide Console ------------
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        //-----------------------------------------------------------
        INIFile file = new INIFile(Global.Global_V.PhatINIconstructor);
        public Log()
        {
            InitializeComponent();
        }

        private void Log_Load(object sender, EventArgs e)
        {
            //Save URL'S
            Global.Global_V.URLDinoSaveCodeZ = file.Read("Constructor", "URLDinoSaveCodeZ");
            Global.Global_V.Search = @""+file.Read("Constructor", "URLRenamingSearch"); //URL IMAGEN
            Global.Global_V.VSearch = @"" + file.Read("Constructor", "URLRenamingVideoSearch");//URL VIDEO
            Global.Global_V.Bakup = @""+file.Read("Constructor",  "URLRenamingBakup");
            Global.Global_V.URLstringSQLite = file.Read("Constructor", "URLDinoSQLite");
                 
            Global.DBConect dbObject = new Global.DBConect();
            label7.Visible = false;
            label8.Visible = false;
            label6.Text = Global.Global_V.V;
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
            Global.Global_V.Operator = file.Read("Setting", "DinoID");
           
            

                 if (file.Read("Setting", "DinoID") != "")
                {

                    using (Home frm = new Home())
                    { frm.ShowDialog(); }
                    this.Close();
                }

            
                if (file.Read("Setting", "DinoID") != "")
                {

                    using (Home frm = new Home())
                    { frm.ShowDialog(); }
                    this.Close();
                }


                if (file.Read("Setting", "DinoID") != "")
                {

                    using (Home frm = new Home())
                    { frm.ShowDialog(); }
                    this.Close();
                }
          

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                if (textBox1.Text == textBox2.Text)
                {
                    label7.Visible = false; label8.Visible = false;
                    Global.Global_V.Operator = textBox1.Text;
                    file.Write("Setting", "DinoID", Global.Global_V.Operator);
                    using (Home frm = new Home())
                    { frm.ShowDialog(); }
                     this.Close();
                }
                else { label7.Visible = true; label8.Visible = true; textBox1.Text = string.Empty; textBox2.Text = string.Empty; }

            }


        }
    }
}
