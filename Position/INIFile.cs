using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace Position
{
    public class INIFile
    {
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        [DllImport("kernel32")]
        private static extern int WritePrivateProfileString(string lpApplicationName, string lpKeyName, string lpString, string lpFileName);

        public string Read(string section, string key, string def)
        {
            StringBuilder sb = new StringBuilder(1024);
            GetPrivateProfileString(section, key, def, sb, 1024, mFilePath);
            return sb.ToString();
        }

        public int Write(string section, string key, string value)
        {
            return WritePrivateProfileString(section, key, value, mFilePath);
        }

        public int DeleteSection(string section, string filePath)
        {
            return Write(section, null, null);
        }
        
        public int DeleteKey(string section, string key)
        {
            return Write(section, key, null);
        }

        public INIFile(string path)
        {
            mFilePath = path;
        }

        private string mFilePath;
    }
}
