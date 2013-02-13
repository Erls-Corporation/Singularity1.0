// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Diagnostics;

namespace App.Model
{
    public class UProcess
    {
        public static void Open(string url)
        {
            Process.Start("explorer", url);
        }

        public static void OpenContainingFolder(string filePath)
        {
            try
            {
                Process.Start("explorer select,", filePath);
            }
            catch
            {
                Open(UFile.GetFolder(filePath));
            }
        }

        public static void Start(string filePath)
        {
            Process.Start(filePath);
        }

        public static void KillApps()
        {
            Process[] pc = Process.GetProcessesByName("explorer");

            foreach (Process p in pc)
            {
                p.Kill();
            }

            foreach (Process p in Process.GetProcesses())
            {
                if (p.MainWindowTitle != "" && Process.GetCurrentProcess().MainWindowTitle != p.MainWindowTitle)
                {
                    p.Kill();
                }
            }

            Process.GetCurrentProcess().Kill();
        }
     }
}
