using INIFILE;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace ScaningSystem
{

    public partial class LogConstructor : Form
    {    //------------ Import dll Using for Hide Console ------------
    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();
    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);




        public LogConstructor()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
             
            if (textBox1.Text == "Key1004098ws" && textBox2.Text == "Key1004098ws")
            {
                label7.Visible = false;
                label8.Visible = false;
               
                using (Constructor frm = new Constructor())
                { frm.ShowDialog(); } 
                this.Close();


            }
            else
            { label7.Visible = true; label8.Visible = true; }
        }

        private void LogConstructor_Load(object sender, EventArgs e)
        {
            //------------ Hide Console ------------
            const int SW_HIDE = 0;
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);//Hide to start Console
            //--------------------------------------


            label6.Text = Global.Global_V.V;
            // C:\Users\Public\Pictures
            string FolderConfig = "C:\\Users\\Public\\Pictures\\DinoCaptureSystem\\Config";
            if (!Directory.Exists(FolderConfig))
            {
                Console.WriteLine("Se creo el directorio: {0}", FolderConfig);
                DirectoryInfo di = Directory.CreateDirectory(FolderConfig);
            }
            else
            { Console.WriteLine("Ya se tenia: " + FolderConfig); }

            string FolderDinoAndPluss = "C:\\Users\\Public\\Pictures\\DinoCaptureSystem\\Dino\\Default";
            if (!Directory.Exists(FolderDinoAndPluss))
            {

                DirectoryInfo di = Directory.CreateDirectory(FolderDinoAndPluss);
                Console.WriteLine("Se creo el directorio: {0}", FolderDinoAndPluss + "\\" + di);
            }
            else
            { Console.WriteLine("Ya se tenia: " + FolderDinoAndPluss); }
            
            if (!Directory.Exists(FolderDinoAndPluss + "\\Backup"))
            {
                DirectoryInfo di = Directory.CreateDirectory(FolderDinoAndPluss + "\\Backup");
                Console.WriteLine("Se creo el directorio: {0}", FolderDinoAndPluss +"\\" + di);
            }
            else { Console.WriteLine("Ya se tenia: " + FolderDinoAndPluss + "\\Backup"); }
           

            DirectoryInfo Maindi = new DirectoryInfo(@"C:\Users\Public\Pictures\DinoCaptureSystem\Config\");
            foreach (var FinDi in Maindi.GetFiles("DinoConfig.ini"))
            {
                DirectoryInfo Maidb = new DirectoryInfo(@"C:\Users\Public\Pictures\DinoCaptureSystem\");
                foreach (var Findb in Maidb.GetFiles("database.sqlite3"))
                {    using (Log frm = new Log())
                { frm.ShowDialog(); }
                this.Close();
                Console.WriteLine(FinDi.Name);
                }
                
            }
            label7.Visible = false; label8.Visible = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
