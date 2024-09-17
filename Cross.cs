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
using System;
using INIFILE; //This Library usin to path
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
//---------------------------------------

namespace ScaningSystem
{
    public partial class Cross : Form
    {
        //------------ Import dll Using for Hide Console ------------
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        //-----------------------------------------------------------
        //---------------------- Read INI file ----------------------
        INIFile file = new INIFile(Global.Global_V.PhatINIconstructor);
        Check lod;
        //-----------------------------------------------------------
        public Cross()
        { InitializeComponent(); }
        //---------------- Delay to open Check form -----------------
        public void time()
        {
            int Mtime = Int32.Parse(file.Read("Setting", "StepDelay"));//Convert String to ini File to Int  
            Thread.Sleep(2000 + Mtime);
        }
        public void check()
        {
            Global.Global_V.CountToReset = Global.Global_V.CountToReset + 1;//Counter to Reset           
            lod = new Check();
           
            lod.Show(); //Show the Cross form

        }
        public void hide()
        { if (lod != null) { lod.Close(); } }
        //-----------------------------------------------------------
        //---------------------- Compare Z Code ---------------------
        public async void Process()
        {
            Global.Global_V.BarCode_2 = textBox1.Text; //Save code Z
            label7.Text = Global.Global_V.BarCode_2; //Show Code Z
            if (Global.Global_V.BarCode_1 == Global.Global_V.BarCode_2)
            {
                
                check(); 
            
                
                Task oTask = new Task(time);
                oTask.Start();
                await oTask;
                this.Dispose();
                //    using (Home frm = new Home())
                //{ frm.ShowDialog(); }
                //this.Hide();
                Application.Restart();
                hide();
               
            }
            else if (Global.Global_V.BarCode_1 != Global.Global_V.BarCode_2)//Compare Code Z
            {
                label9.Visible = true;
                textBox1.Focus();
                Global.Global_V.BarCode_2 = "";
                textBox1.Text = Global.Global_V.BarCode_2;
            }
        }
        //-----------------------------------------------------------
        //------------------ Action to Start form -------------------
        private void Form1_Load(object sender, EventArgs e)
        {
            label11.Text= Global.Global_V.V;
            //------------------ Console Hide & Show  -------------------
            Global.Global_V.Console = file.Read("Setting", "ShowConsole");
            const int SW_HIDE = 0;
            const int SW_SHOW = 0;
            var handle = GetConsoleWindow();
            if (Global.Global_V.Console == "0")
            {
                ShowWindow(handle, SW_HIDE); //Hide to start Console
            }
            else
            {
                ShowWindow(handle, SW_SHOW); //To show to estar Console
            }
            //-----------------------------------------------------------
            //------------- Create the Txt File whit Z Code -------------
            TopMost = true;// Top Most Screen
            string PHAT = file.Read("Constructor", "URLDinoSaveCodeZ");//Read UrlDinoSaveCode Variable
            string fullP = @"" + PHAT + Global.Global_V.BarCode_1 + ".ini";
            TextWriter Txt = new StreamWriter(fullP);
            Global.Global_V.UB = fullP;
            Txt.WriteLine("[Setting]");
            //Zcode
            Txt.WriteLine("Zcode=" + Global.Global_V.BarCode_1);
            //DATE
            string date = DateTime.UtcNow.ToString("MM/dd/yyyy");
            Txt.WriteLine("Date=" + date);
            //Time
            string time = DateTime.Now.ToString("h:mm:ss");
            Txt.WriteLine("Time=" + time);
            string tt = DateTime.Now.ToString("tt");
            Txt.WriteLine("Meridian=" + tt);
            //Close TXT
            Txt.Close();
            
            string timeR = DateTime.Now.ToString("%H");
            string Minut = DateTime.Now.ToString("mm");
            int TimeR = Int32.Parse(timeR);
            int MinutR = Int32.Parse(Minut);
            
            if (TimeR >= 5 )
            { Global.Global_V.Turn = "A"; }

            if (TimeR >= 13 )
            { Global.Global_V.Turn = "B"; }

            if(TimeR <=6 || TimeR >=23)
            { Global.Global_V.Turn = "C"; }

            label9.Visible = false;
            label4.Text = Global.Global_V.BarCode_1;
            //-----------------------------------------------------------
        }
        //-----------------------------------------------------------
        //----------------- Start Key press Texbox1 -----------------
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        { if (e.KeyChar == Convert.ToChar(Keys.Enter)) { Process(); } }
        //-----------------------------------------------------------
        //-------------------- Focus in texbox1 ---------------------
        private void wait_KeyPress(object sender, KeyPressEventArgs e)
        { textBox1.Focus(); }
        //-----------------------------------------------------------
        //--------------------- Exit Application --------------------
        private void panel1_DoubleClick(object sender, EventArgs e)
        { Application.Exit(); }
        //-----------------------------------------------------------

        //------------------- Restart Application -------------------
        private void pictureBox4_DoubleClick(object sender, EventArgs e)
        {
            string DeleteP = file.Read("Setting", "URLDinoSaveCodeZ");
            string fullP = @"" + DeleteP + Global.Global_V.BarCode_1 + ".txt";
            if (File.Exists(fullP))
            {
                try
                {
                    File.Delete(fullP);
                }
                catch (Exception) { }
            }
            else
            {
                Console.WriteLine("Specified file doesn't exist");
            }
            Application.Restart();
        }
        //-----------------------------------------------------------
        //--------------------- Exit Application --------------------
        private void pictureBox6_MouseDoubleClick(object sender, MouseEventArgs e)
        { }
        //-----------------------------------------------------------
    }
}
