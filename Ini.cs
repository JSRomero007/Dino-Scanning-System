using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace INIFILE
{
public class   INIFile
{
    private string filePth;
    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section,
        string Key,
        string val,
        string filePath);
    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section,
        string key,
        string def,
        StringBuilder retVal,
        int size,
        String filePath);
    public INIFile(string filePath)
    {
        this.filePth = filePath;
    }
    public void Write(String section, string key, string value)
    {
        WritePrivateProfileString(section, key, value.ToLower(), this.filePth);
    }

    public string Read(String section, string key) 
    {
        StringBuilder SB = new StringBuilder(255);
        int i = GetPrivateProfileString(section, key, "", SB, 255, this.filePth);
        return SB.ToString();
    }

    public string FilePath
    {
        get { return this.filePth; }
        set { this.filePth = value; }
    }

}
}
