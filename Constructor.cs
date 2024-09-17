using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ScaningSystem
{
    public partial class Constructor : Form
    {
        //------------ Import dll Using for Hide Console ------------
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public Constructor()
        {
            InitializeComponent();
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string space = "";

            //Create the .ini Config

            try
            {
                string fullP = @"C:\Users\Public\Pictures\DinoCaptureSystem\Config\DinoConfig.ini";
                TextWriter Txt = new StreamWriter(fullP);
                Global.Global_V.UB = fullP;
                //Creat file in C:\Users\Public\Pictures\Config\
                Txt.WriteLine("[Setting]");
                //User Id
                Txt.WriteLine("DinoID=");
                //Console view
                Txt.WriteLine("ShowConsole=1");
                //Delay to save 
                Txt.WriteLine("StepDelay=0");
                //
                Txt.WriteLine(space);
                //Shift data
                Txt.WriteLine("[Shift]");
                //Turn Meridian
                Txt.WriteLine("Meridian=pm");
                //Shift actualy A
                Txt.WriteLine("ShiftA=disable");
                //Shift actualy B
                Txt.WriteLine("ShiftB=disable");
                //Shift actualy C
                Txt.WriteLine("ShiftC=disable");
                //
                Txt.WriteLine(space);
                //Constructor data
                Txt.WriteLine("[Constructor]");
                //Constructor state
                Txt.WriteLine("Constructor=enable");
                //URL Dino save
                string usr = Environment.UserName;

                Txt.WriteLine("URLDinoSaveCodeZ=C:\\Users\\" + usr + "\\Documents\\Digital Microscope\\Default\\Picture\\");
                //URL sqlite path
                Txt.WriteLine("URLDinoSQLite= C:\\Users\\Public\\Pictures\\DinoCaptureSystem\\database.sqlite3");
                //Shift
                Txt.WriteLine("URLShift=C:\\Users\\Public\\Pictures\\DinoCaptureSystem\\Config\\");
                //URL Searching picture file
                Txt.WriteLine("URLRenamingSearch= C:\\Users\\" + usr + "\\Documents\\Digital Microscope\\Default\\Picture\\");
                //URL Searching Video file
                Txt.WriteLine("URLRenamingVideoSearch= C:\\Users\\" + usr + "\\Documents\\Digital Microscope\\Default\\Video\\");
                //URL backup save
                Txt.WriteLine("URLRenamingBakup= C:\\Users\\Public\\Pictures\\DinoCaptureSystem\\Dino\\Default\\Backup");
                //
                Txt.WriteLine(space);
                //Info to creat DinoConfig.ini file
                Txt.WriteLine("[Info]");
                //Date
                string date = DateTime.UtcNow.ToString("MM/dd/yyyy");
                Txt.WriteLine("Date=" + date);
                //Hour
                string time = DateTime.Now.ToString("h:mm:ss tt");
                Txt.WriteLine("Time=" + time);
                //
                Txt.Close();
                label2.Text = "Se creo el archivo de configuracion DinoConfig.ini";
                pictureBox3.Visible = true;
            }
            catch { label2.Text = "Ya se tenia el archivo de configuracion DinoConfig.ini"; }



            try
            {
                string dp = @"C:\Users\Public\Pictures\DinoCaptureSystem\" + "database.sqlite3";//New ubication and Renaming
                if (!File.Exists(dp))
                {
                    DirectoryInfo Maindi = new DirectoryInfo(@".//DB//");
                    string pd = Maindi.ToString() + "database.sqlite3";//Old File
                    File.Copy(pd, dp);//Move to new paht
                    Console.WriteLine(pd);

                    label3.Text = "Se creo la base de datos ";
                }

                else { label3.Text = "Ya se tenia la base de datos "; Console.WriteLine("db ok"); }
                pictureBox5.Visible = true;
            }

            catch { label3.Text = "Error al crear la base datos"; }

            label4.Text = "\\Dino\\Config";
            pictureBox7.Visible = true;
            label5.Text = "\\Dino\\Default";
            pictureBox8.Visible = true;
            label7.Text = "\\Dino\\Default\\Backup";
            pictureBox9.Visible = true;
            label8.Text = "\\Dino\\Default\\Image";
            pictureBox10.Visible = true;
            label13.Visible = true;
            label9.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
            label12.Visible = true;
            label5.Visible = true;
            label4.Visible = true;
            label7.Visible = true;
            label8.Visible = true;




        }



        private void Constructor_Load(object sender, EventArgs e)
        {
            //------------ Hide Console ------------
            const int SW_HIDE = 0;
            var handle = GetConsoleWindow();
            //ShowWindow(handle, SW_HIDE);//Hide to start Console
            //--------------------------------------
            pictureBox3.Visible = false;
            pictureBox5.Visible = false;
            pictureBox7.Visible = false;
            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            pictureBox10.Visible = false;
            label13.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label2.Text = "";
            label3.Text = "";
            label6.Text = Global.Global_V.V;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
