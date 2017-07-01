using Microsoft.Win32;
using Roglaza.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Roglaza
{
    class RoglazaHelper
    {
        internal static string ReadTextFile(string path,string return_result="")
        {
            try
            {
                return System.IO.File.ReadAllText(path);
            }
            catch {
                if (return_result != "")
                    return return_result;
                return GeneralErros.Not_found.ToString();
            }
        }
        public static string ReverseText(string source)
        { 
            if (source == "")
                return source;
            string reversed = "";
            foreach (char c in source)
            {
                reversed = c + reversed;
            }
            return reversed;
        }
        private static void Startup(bool statue)
        {
            try
            {
                RegistryKey rk = Registry.CurrentUser.OpenSubKey
                    ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                string AppName = "Roglaza.exe";
                string appPath = Program.GetExecutablePath();   
               if(statue)
                rk.SetValue(AppName, appPath);
               else
                   rk.DeleteValue(AppName);
            }
            catch
            {                           
            }
        }
        internal static bool FileWriteText(string file, string data)
        {
            try
            {
                System.IO.File.WriteAllText(file, data);
                return true;
            }
            catch { return false; }
        }
        internal static int TextToInt(string value, int Return_value_on_fail)
        {
            bool suc = int.TryParse(value, out Return_value_on_fail);
            return Return_value_on_fail;

        }
        public static bool IsExistedDirectory(string Dirpath)
        {
            return System.IO.Directory.Exists(Dirpath);
        }
        internal static bool createDirectory(string Dirpath)
        {
            try
            {
                System.IO.Directory.CreateDirectory(Dirpath); return true;
            }
            catch { return false; }
        }
        internal static string Upload(string Path_, string key)
        {
            try
            {
                byte[] imageData;
                if (key == "")
                    key = AppInfo.ImgurKey;
                FileStream fileStream = File.OpenRead(Path_);
                imageData = new byte[fileStream.Length];
                fileStream.Read(imageData, 0, imageData.Length);
                fileStream.Close();

                string uploadRequestString = "image=" +/* Uri.EscapeDataString*/(System.Convert.ToBase64String(imageData)) + "&key=" + key;

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("http://api.imgur.com/2/upload");
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ServicePoint.Expect100Continue = false;

                StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream());
                streamWriter.Write(uploadRequestString);
                streamWriter.Close();

                WebResponse response = webRequest.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader responseReader = new StreamReader(responseStream);

                string responseString = responseReader.ReadToEnd();
                return "";
            }
            catch { return ""; }
        }
        internal static string RemoveInvalidPathChars(string p)
        {
            p= p.Replace("<","-");
            p = p.Replace(">", "-");
            p = p.Replace("|", "-");
            p = p.Replace("\\", "-");
            p = p.Replace("/", "-");
            p = p.Replace("*", "-");
            p = p.Replace("?", "-");
            p = p.Replace(":", "-");
            return p;

        }
        internal static string createDirectoryIfNotFound(string p)
        {
            char sep = '/';
            if (p.Contains('\\'))
                sep = '\\';

            string[] pieces = p.Split(sep);
            //C://v1/v2/v3
            string dir = pieces[0];
            for(int i=1;i<pieces.Length;i++)
            {
                dir += "\\" + RemoveInvalidPathChars(pieces[i]);
                RoglazaHelper.createDirectory(dir);
            }
            return dir;
        }
        public static bool SetFolderHidden(string path)
        {
            try
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                return true;
            }
            catch { return false; }
        }
        internal static void StartProcess(string p)
        {
            try
            {
                System.Diagnostics.Process.Start(p);
            }
            catch { }
        }
        
        internal static bool ReverseToBoolean(string value)
        {
            if (value.Length == 0)
                return false;
            value=value.Trim().ToLower();
            return (value == "1" || value == "yes" || value == "true" || value == "ok");
        }

        internal static bool IsExistedFile(string p)
        {
            return System.IO.File.Exists(p);
        }

       
        internal static bool createEmptyFile(string p )
        {
            try
            {
                System.IO.File.Create(p);
                return true;
            }
            catch { return false; }
        }

        internal static bool DeleteFile(string file)
        {
            try
            {
                System.IO.File.Delete(file);
                return true;
            }
            catch
            {
                return false;
            }
        }



        internal static string[] ReadFileLines(string path)
        {
            try
            {
                return System.IO.File.ReadAllLines(path); 
            }
            catch { return null; }
        }

        internal static string[] GetDirectories(string logsPath)
        {
            try
            {
                return System.IO.Directory.GetDirectories(logsPath);
            }
            catch { return null; }
        }

        internal static string ReturnLastSplit(string s, char p)
        {

            if (s.Contains(p))
            {
                string[] x = s.Split(p);
                return x[x.Length - 1];
            }
            return s;
        }

        internal static string[] GetFiles(string logsPath)
        {
            try
            {
                return System.IO.Directory.GetFiles(logsPath);
            }
            catch { return null; }
        }

        internal static int  str_to_int(string p1, int p2)
        {
            p1 = p1.Trim();
            if (RoglazaHelper.isInt(p1))
                return int.Parse(p1);
            else return p2;
        }

        private static bool isInt(string p1)
        {
            int i = 0;
            return int.TryParse(p1, out i);
        }

        internal static int Limit_int(int num, int min, int max)
        {
          if (num < min)
                num=min;

            if (num > max)
                num = max;
            return num;
        }

        internal static bool FileWriteBytes(string p1, byte[] p2)
        {
            bool res = false;
            try
            {
                System.IO.File.WriteAllBytes(p1, p2);
            }
            catch { res = false; }
            return res;
        }

        internal static bool MoveFile(string outputFileName, string dest)
        {
            try
            {
                System.IO.File.Move(outputFileName, dest);
                return true;
            }
            catch { }
            return false;
        }

        internal static string TextSpaces(string p1, int p2)
        {
            while (p1.Length < p2)
                p1 += " ";
            return p1;
        }
    }
}
