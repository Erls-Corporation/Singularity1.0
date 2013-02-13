// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace App.Model
{
    public class UFile
    {
        public static void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void CreateFolderFromFilePath(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Directory.CreateDirectory(UFile.GetFolder(filePath));
            }
        }

        public static string GetFolder(string filePath)
        {
            return Path.GetDirectoryName(filePath) + @"\";
        }

        public static void CopyFiles(string sourceFolder, string targetFolder, bool recursive)
        {
            if (sourceFolder == null)
                throw new ArgumentNullException(@"C:/Source");
            if (targetFolder == null)
                throw new ArgumentNullException(@"C:/Target");

            DirectoryInfo sourced = new DirectoryInfo(sourceFolder);
            DirectoryInfo targetd = new DirectoryInfo(targetFolder);

            if (!sourced.Exists)
                sourced.Create();

            foreach (FileInfo file in sourced.GetFiles())
            {
                file.CopyTo(Path.Combine(targetd.FullName, file.Name), true);
            }

            if (!recursive) 
                return;

            foreach (DirectoryInfo directory in sourced.GetDirectories())
            {
                CopyFiles(directory.FullName, Path.Combine(targetd.FullName, directory.Name), recursive);
            }
        }

        public static bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }

        public static string GetFileName(string filePath)
        {
            return Path.GetFileName(filePath);
        }

        public static string GetFileNameNoExtension(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }

        public static string GetExtension(string filePath)
        {
            return Path.GetExtension(filePath);
        }

        public static void TrimNewLine(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            File.WriteAllText(filePath, UStr.TrimNewLine(File.ReadAllText(filePath)));
        }

        public static void RemoveReadOnly(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            File.SetAttributes(filePath, File.GetAttributes(filePath) & (~FileAttributes.ReadOnly));
        }

        public static void Delete(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            RemoveReadOnly(filePath);

            File.Delete(filePath);
        }

        public static void Write(string filePath, string text)
        {
            UFile.RemoveReadOnly(filePath);

            File.WriteAllText(filePath, text);
        }
    }
}
