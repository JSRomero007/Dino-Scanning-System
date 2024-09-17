/**
*   this code concatenates the information obtained from the Keyence IV2-50P system and moves the other folder.
*   @Param
*        --identify txt and jpg files from a specific folder
*   @Return
*        --rename and move files to folder
*   @Version 28.04.23
*   @sinse 07-04-2023
*   @author <jose.israel.cosme@aptiv.com>
*/
//------------ Using Library ------------
using System;
using System.Collections.Generic;
//---------------------------------------

namespace ScaningSystem.Global
{
    public static class extencion //Subroutine to remove extension
    {
        public static string Filter(this string str, List<string> ChartRemove)
        {
            foreach (string c in ChartRemove)
            {
                str = str.Replace(c.ToString(), String.Empty);
            }
            return str;
        }
    }
    internal class Global_V //Usinf global variables 
    {
        public static string Turn;
        public static string Operator;
        public static string Shift_A,Shift_B,Shift_C;
        public static int CountToReset;
        public static string URL = "";
        public static string BarCode_1 = "1", BarCode_2 = "2";
        public static string Jpeg, Z_code, str, Save_Z_Code;
        public static string Step_1, Step_2, Step_3;
        public static string UB,Console,Remove;
        public static string V = "V.6.17.2023";
        public static string ZCode, Date, Time, Meridiam;
        public static string ZcodeFilter;
        //--------Constructor--------//
        public static string PhatINIconstructor= @"C:\Users\Public\Pictures\DinoCaptureSystem\Config\DinoConfig.ini";
        public static string PhatShiftconstructor = @"C:\Users\Public\Pictures\DinoCaptureSystem\Config\";
        public static string Constructor;
        public static string URLstringSQLite;
        public static string URLDinoSaveCodeZ;
        public static string Search; //path to search .jpeg and .txt file
        public static string VSearch; //path to search .wmv file
        public static string Bakup; //path to backup
        public static string SqlZcode;
        public static string SqlDate;
        public static string SqlTime;
        public static string SqlOperator;
        public static string SqlShift;

    }
}
