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
using INIFILE;
using System.IO;
using System.Threading;
using ScaningSystem.Global;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Data.SQLite;
using System.Text;
//---------------------------------------
//https://www.youtube.com/watch?v=anTP-mgktiI&t=195s

namespace ScaningSystem
{
    public partial class Check : Form
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
        public Check()
        {
            InitializeComponent();
        }
        //------------------ Action to Start form -------------------
        private void Check_Load(object sender, EventArgs e)
        {
            label9.Text = Global.Global_V.V;
            Global.DBConect dbObject = new Global.DBConect();
            //------------------ Console Hide & Show  -------------------
          

            //NewConnection Ini

            INIFile Info = new INIFile(@"" + file.Read("Constructor", "URLDinoSaveCodeZ") +"/"+Global.Global_V.BarCode_1+".ini");
            
            Global_V.ZCode = Info.Read("Setting","Zcode");
            Global_V.Date = Info.Read("Setting","Date");
            Global_V.Time = Info.Read("Setting","Time");
            Global_V.Meridiam = Info.Read("Setting","Meridian");
            Console.WriteLine("Zcode: "+Global_V.ZCode);
            Console.WriteLine("Date: "+Global_V.Date);
            Console.WriteLine("Time: "+Global_V.Time);
            Console.WriteLine("Meridian: "+Global_V.Meridiam);
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
            //-----------------------------------------------------------
            //------------------ Console Hide & Show  -------------------
            TopMost = true;// Top Most Screen
            label6.Text = Global.Global_V.BarCode_1;
            label7.Text = Global.Global_V.BarCode_2;
            label8.Text = Global.Global_V.Operator;

            //--------------------- Search Z Code -----------------------
            DirectoryInfo di = new DirectoryInfo(Global.Global_V.Search + "/");//Paht to search files
            foreach (var fi in di.GetFiles("*.ini"))//Only read .txt files
            {
                Global.Global_V.Save_Z_Code = fi.Name;
                string str = fi.Name;
                List<string> ChartRemove = new List<string> { ".ini" }; //Extension remover
                str = str.Filter(ChartRemove);
                Global.Global_V.Z_code = str;

                //--------- Delete file & register --------
                File.Delete(str);//Delete .txt file
                File.Delete(Global.Global_V.Search + "/" + fi.Name);
                //Console.WriteLine(str);
                //-----------------------------------------
            }
            //-----------------------------------------------------------

            Thread.Sleep(300);
            //------------------- Search jpeg Files ---------------------
            try
            {                
                DirectoryInfo di2 = new DirectoryInfo(Global.Global_V.Search );//Paht to search files
                    foreach (var fi in di2.GetFiles("*.jpg"))//Only read .jpeg files
                    {
            
                    Global.Global_V.Jpeg = fi.Name;   
                    string dp = Global.Global_V.Bakup + "/" + Global.Global_V.Z_code + "_" + Global.Global_V.Jpeg;//New ubication and Renaming
                    string pd = Global.Global_V.Search + "/" + Global.Global_V.Jpeg;//Old File
                    File.Move(pd, dp);//Move to new paht
                    File.Delete(pd);//Delete Old file
                    Console.WriteLine("Renaming and Move:__" + dp);
                    //--SQLite Conecction--//
                    string query = "INSERT INTO ZcodeINFO (ZCode,Date,Time,Meridian,Picture,Operator,Shift,Info) VALUES (@ZCode,@Date,@Time,@Meridian,@Picture,@Operator,@Shift,@Info)";
                    SQLiteCommand mycomand = new SQLiteCommand(query, dbObject.myConnection);
                    dbObject.OpenConnection();
                    mycomand.Parameters.AddWithValue("@ZCode", Global.Global_V.ZCode);
                    mycomand.Parameters.AddWithValue("@Date", Global.Global_V.Date);
                    mycomand.Parameters.AddWithValue("@TIME",Global.Global_V.Time);
                    mycomand.Parameters.AddWithValue("@Meridian",Global.Global_V.Meridiam);  
                    //--Convert img to binary--//
                    byte[] image = null;
                    FileStream stream = new FileStream(dp,FileMode.Open,FileAccess.Read);
                    BinaryReader br = new BinaryReader(stream);
                    image = br.ReadBytes((int)stream.Length);
                    mycomand.Parameters.AddWithValue("@Picture",image);
                    mycomand.Parameters.AddWithValue("@Operator",Global.Global_V.Operator);
                    mycomand.Parameters.AddWithValue("@Shift", Global.Global_V.Turn);
                    mycomand.Parameters.AddWithValue("@Info", "NA");
                    mycomand.ExecuteNonQuery();
                    dbObject.CloseConnection();
                    }
            }//-----------------------------------------------------------
             //------------- Print Renaming and new path -----------------
            catch (Exception ex)
            {               Console.WriteLine("waiting new file");
                Console.WriteLine("----------------Path Info----------------");
                Console.WriteLine("Search Path");
                Console.WriteLine(Global.Global_V.Search);
                Console.WriteLine("Backup Path");
                Console.WriteLine(Global.Global_V.Bakup);
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine(ex.Message);    
            } 
            //-----------------------------------------------------------
            Thread.Sleep(300);
            //--------------------- Search Video Files -----------------------
            try
            {
                DirectoryInfo di3 = new DirectoryInfo(Global.Global_V.VSearch);
                foreach (var vi in di3.GetFiles("*.wmv"))
                {
                    Console.WriteLine(vi);
                    
                      /* 
                     string dp = Global.Global_V.Bakup + "/" + Global.Global_V.Z_code + "_" + Global.Global_V.Jpeg;//New ubication andRenaming
                     string pd = Global.Global_V.Search + "/" + Global.Global_V.Jpeg;//Old File
                     File.Move(pd, dp);//Move to new paht
                     File.Delete(pd);//Delete Old file
                    */
                   


                }
            }
            catch
            {
                Console.WriteLine("No se encontraron videos para cargar");
            }

            //-----------------------------------------------------------
            Thread.Sleep(300);

        }
    }
}

