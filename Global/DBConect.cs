using System;
using System.Data.SQLite;
using System.IO;

namespace ScaningSystem.Global
{
    internal class DBConect
    {
        public SQLiteConnection myConnection;
        public DBConect()
        {

            
            string conectionString= @"Data Source="+Global.Global_V.URLstringSQLite + "; Version=3";
            myConnection = new SQLiteConnection(conectionString);
            if (!File.Exists(Global.Global_V.URLstringSQLite))
            {
               
                Console.WriteLine("Database Not found ");
            }
    
            
        }
        public void OpenConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Open)
            {
                myConnection.Open();
            }
        }
        public void CloseConnection() 
        {
            if (myConnection.State != System.Data.ConnectionState.Closed)
            {
                myConnection.Close();
            }
        }
    }
}
