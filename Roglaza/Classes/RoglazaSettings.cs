using Roglaza.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roglaza
{
   public enum GeneralErros 
   {
       Not_found 
   }
   public enum RoglazaSettingsParameters
   {
       password, GeneralSettingsFilePath, WaitInPasswordFailed
   }
   public class RoglazaSettings
    {
       public string LogsPath = RoglazaInstaller.GetRoglazaAPPDataPath()+"\\Logs";
       public string GeneralSettingsFilePath = "config.rog";
       public static string DefaultPassword = "weloveyou";    
       public string RoglazaIconPath = "";
       public string RoglazaName = "Roglaza";
       public string KeyLoggerStorePath = "kslogs.mp3";
       public string history_files = "history_files.rog";
       public bool showDadMessage = false;
       public bool AllowPornoBlocker = true;
       public string DadMessage = "";

       public int WaitInPasswordFailed = 60;
       public int ScreenShotInterValMinutes = 5;

       public bool Loaded = false;
       public bool AllowKeyLogger = true;
       public bool AllowScreenShots = true;
       public bool AllowCamShots = false;
       public bool AllowBrowserHistory = true;
       public bool ShouldBewrittenToFile = true;
       public RoglazaSettings()
       {
         //  PornMatches = MessageStrings.StaticPornMatches;
           matches_lists = new List<string>();
           GeneralSettingsFilePath = RoglazaInstaller.GetRoglazaAPPDataPath() + "\\config.rog";
           LogsPath = RoglazaInstaller.GetRoglazaAPPDataPath() + "\\Logs";
           KeyLoggerStorePath = RoglazaInstaller.GetRoglazaAPPDataPath() + "\\Logs\\kslogs.rog";
           ContentMatchesPath = RoglazaInstaller.GetRoglazaAPPDataPath() + "\\matches.rog";

           if (RoglazaHelper.IsExistedFile(ContentMatchesPath) == false)
               SetupContentMatchesFile();
         
       }

       private void SetupContentMatchesFile()
       {
           string x = "";
           matches_lists.Clear();
           foreach (string s in MessageStrings.ContentBlockerMatches)
           {
               x += s + "\r\n";
               matches_lists.Add(s);
           }
           RoglazaHelper.FileWriteText(ContentMatchesPath, x);
       }

       public RoglazaSettings(bool p)
       {
           ShouldBewrittenToFile = p;
       }
       public List<string> matches_lists;
       public bool LoadFromFile()
       {
           //Load settings from file as text
           try
           {
               string data=  RoglazaHelper.ReadTextFile(Program.ProgramSettings.GeneralSettingsFilePath);
               if (data == GeneralErros.Not_found.ToString())
                   return false;
               else
               {
                  return TranslateSettings(data);              
               }
           }
           catch
           {
               return false;
           }
       }
       private bool TranslateSettings(string data)
       {
           // unserializing text settings
           if (data.Contains("\n"))
           {
               string[] lines = data.Split('\n');
               foreach (string l in lines)
               {
                   if (!l.Contains(":"))
                       continue;
                   string[] key_value = l.Split(':');
                   string key = key_value[0].Trim();
                   string value = key_value[1].Trim();
                   switch (key)
                   {
                           // boolean values
                       case "allowbrowserhistory":
                       case "AllowBrowserHistory": AllowBrowserHistory = RoglazaHelper.ReverseToBoolean(value); break;
                       case "allowcamshots":
                       case "AllowCamShots": AllowCamShots = RoglazaHelper.ReverseToBoolean(value); break;
                       case "allowkeylogger":
                       case "AllowKeyLogger": AllowKeyLogger = RoglazaHelper.ReverseToBoolean(value); break;
                       case "allowscreenshots":
                       case "AllowScreenShots": AllowScreenShots = RoglazaHelper.ReverseToBoolean(value); break;
                       case "allowpornoblocker":
                       case "AllowPornoBlocker": AllowPornoBlocker = RoglazaHelper.ReverseToBoolean(value); break;

               
                       case "allowappmanager":
                       case "AllowAppManager": AllowAppManager = RoglazaHelper.ReverseToBoolean(value); break;

                       case "showdadmessage":
                       case "ShowDadMessage":
                       case "showDadMessage": showDadMessage = RoglazaHelper.ReverseToBoolean(value); break;
                       case "dadmessage":
                       case "DadMessage": DadMessage = Decode(value); break;


                           //strings
                       case "history_files":
                       case "Files_urls_path":
                       case "Files_Urls_Path":
                       case "files_urls_path": history_files = System.IO.Path.GetFullPath(Decode(value)); break;

                       case "history_urls.rog":
                       case "history_urls":
                       case "History_Urls": history_urls = System.IO.Path.GetFullPath(Decode(value)); break;
                       case "ContentMatchesPath":
                       case "contentmatchespath": ContentMatchesPath = System.IO.Path.GetFullPath(Decode(value)); LoadContentMatches(); break;
                       case "LogsPath":
                       case "logspath": LogsPath = System.IO.Path.GetFullPath(Decode(value)); break;
                       case "KeyLoggerPath":
                       case "KeyLoggerStore":
                       case "KeyLoggerStorePath":
                       case "kslogs":
                       case "kslogspath":
                       case "keyloggerstorepath": KeyLoggerStorePath = System.IO.Path.GetFullPath(Decode(value)); InitKeylogger(); break;
                       case "RoglazaName":
                       case "roglazaname": RoglazaName = Decode(value); break;
                       case "RoglazaIconPath":
                       case "roglazaiconpath": RoglazaIconPath = Decode(value); break;
                       case "generalsettingsfilepath":
                       case "GeneralSettingsFilePath": GeneralSettingsFilePath = System.IO.Path.GetFullPath(Decode(value)); break;
                       case "managedapps":
                       case "ManagedApps": DeserializeApps(Decode(value)); break;
                       case "password":
                       case "key":
                       case "access":
                       case "credit":
                           {
                               if (value == "") 
                                   value = Crypter.GetMd5Hash(DefaultPassword);
                               else
                                   PasswordHash = RoglazaHelper.ReverseText(value);
                               break;
                           }
                           //int values
                       case "WaitInPasswordFailed":
                       case "waitinpasswordfailed": WaitInPasswordFailed = RoglazaHelper.TextToInt(value, 60); break;
                       case "ScreenShotInterValMinutes":
                       case "screenshotIntervalminutes": ScreenShotInterValMinutes = RoglazaHelper.TextToInt(value, 5); break;
                   }

               }

               return true;
           }
           else return false;
       }

       private void InitKeylogger()
       {
           string olddata = RoglazaHelper.ReadTextFile(KeyLoggerStorePath);
           olddata += "\r\r\r\r" + DateTime.Now.ToShortDateString() + "\r\n-----------------------\r\n";
       }

       private void LoadContentMatches()
       {
           string[] lines = RoglazaHelper.ReadFileLines(ContentMatchesPath);
           if (lines == null)
               return;
           if (lines.Length < 1)
               return;

           Program.ProgramSettings.matches_lists.Clear();
           foreach (string s in lines)
           {
               Program.ProgramSettings.matches_lists.Add(s.ToLower());
           }

       }
       internal List<ManagedApp> Managed__apps_list = new List<ManagedApp>();
       public string Serialize_app_list()
       {

           string result = "";
           foreach (var a in Managed__apps_list)
               result += a.ToString().Replace("From:","").Replace("To:","") + "$sep$";
           if (result.Length > 5)
               result = result.Substring(0, result.Length - 5);
           return result;

       }
       public void DeserializeApps(string s)
       {
           Managed__apps_list.Clear();
           string[] appListarr = s.Split(new string[]{"$sep$"},StringSplitOptions.RemoveEmptyEntries);
           foreach (string sa in appListarr)
           {
               ManagedApp m = new ManagedApp(sa);
               this.Managed__apps_list.Add(m);
           }

       }
       public bool SaveSettings()
       {
           if (ShouldBewrittenToFile == false)
               return false;
           string revHash = RoglazaHelper.ReverseText(PasswordHash);
           string data = "";
           data += "ContentMatchesPath:" + Encode(ContentMatchesPath) + "\r\n";
           data += "GeneralSettingsFilePath:" + Encode(GeneralSettingsFilePath)+"\r\n";
           data += "DadMessage:" + Encode(DadMessage) + "\r\n";
           data += "key:" + revHash + "\r\n";
           data += "WaitInPasswordFailed:" + WaitInPasswordFailed.ToString() + "\r\n";
           data += "ScreenShotInterValMinutes:" + ScreenShotInterValMinutes.ToString()+"\n\r";
           data += "AllowBrowserHistory:" + AllowBrowserHistory + "\r\n";
           data += "AllowCamShots:" + AllowCamShots + "\r\n";
           data += "AllowAppManager:" + AllowAppManager + "\r\n";
           data += "AllowKeyLogger:" + AllowKeyLogger + "\r\n";
           data += "AllowPornoBlocker:" + showDadMessage + "\r\n";
           data += "AllowPornoBlocker:" + AllowPornoBlocker + "\r\n";
           data += "AllowScreenShots:" + AllowScreenShots + "\r\n";
           data += "RoglazaIconPath:" + Encode(RoglazaIconPath)+"\r\n";
           data += "RoglazaName:" + RoglazaName + "\r\n";
           data += "LogsPath:" + Encode(LogsPath)+"\r\n";
           data += "KeyLoggerStorePath:" + Encode(KeyLoggerStorePath) + "\r\n";
           data += "DadMessage:" + Encode(DadMessage) + "\r\n";
           data += "showDadMessage:" + showDadMessage + "\r\n";
           data += "ManagedApps:" + Encode(Serialize_app_list());
           data += "history_files" + Encode(history_files) + "\r\n";
           data += "history_urls" + Encode(history_urls) + "\r\n"; 



           
           return RoglazaHelper.FileWriteText(GeneralSettingsFilePath, data);         
            
       }

       private  static string Encode(string s)
       {
           return s.Replace(":", "$COLON$");
       }
       private static string Decode(string s)
       {
           return s.Replace("$COLON$",":");
       }
       public void SetNewPassword(string plain)
       {
       PasswordHash =Crypter.GetMd5Hash(plain);
       SaveSettings();

       }
       public string PasswordHash =Crypter.GetMd5Hash(DefaultPassword);
       public static string ConvertTextPasswordToRevHash(string s)
       {
           string hash = Crypter.GetMd5Hash(s);
           hash = RoglazaHelper.ReverseText(hash);
           return hash;
       }
      
       internal void SETScreenShotInterValMinutes(int p)
       {
           this.ScreenShotInterValMinutes = p;
           SaveSettings();
       }
       public string ContentMatchesPath =""; 
       internal void saveMatches(System.Windows.Forms.ListBox.ObjectCollection objectCollection)
       {
           if (objectCollection.Count < 1)
               return;

           string z = "";
           foreach (string s in objectCollection)
           {
               z += s + "\r\n";
           }
           if (RoglazaHelper.FileWriteText(ContentMatchesPath, z))
               System.Windows.Forms.MessageBox.Show("Success");
           
       }


       public bool AllowAppManager = true;

       internal void add_new_managed_app(ManagedApp m)
       {
           if (m.path.Length < 3)
               return;

           for (int i = 0; i < Managed__apps_list.Count; i++)
           {
               if (Managed__apps_list[i].path == m.path)
                   Managed__apps_list.RemoveAt(i);
           }
           Managed__apps_list.Add(m);
       }

       internal string Get_History_files_Path()
       {
           return this.LogsPath+"\\"+history_files ;
       }

       internal string GetAbsolutePath(RoglazaSettingsMember typ)
       {
           switch (typ)
           {
               case RoglazaSettingsMember.history_files: return LogsPath+"\\"+ history_files;
               case RoglazaSettingsMember.History_urls: return LogsPath + "\\" + history_urls;  

           }

           return "";
       }


       internal string Get_History_Urls_Path()
       {
           return this.LogsPath + "\\" + history_urls;
       }

       public string history_urls="History_urls.rog";

       internal string AppDataPath()
       {
           return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
       }
    }
   enum RoglazaSettingsMember {keystrokes_file,history_files,History_urls }
}
