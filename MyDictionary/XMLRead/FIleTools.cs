using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Reflection;

namespace MyDictionary.XMLRead
{/// <summary>
/// вспомогательный класс для создания папок, поиска ,записи ,чтения и копирования файлов
/// </summary>
    public static class FIleTools
    {
        /// <summary>
        /// путь папки "FileStorage"(хранит файлы с данными)
        /// </summary>
        public const string NameDirectoryStorage = @"./FileStorage";

        /// <summary>
        /// имя файла для хранения путей к папкам источникам аудиофайлов
        /// </summary>
        public const string NameFilePathes = @"./FileStorage/pathes.txt";
        /// <summary>
        /// имя файла для хранения значений переменных
        /// </summary>
        public const string NameFileDataVariable = @"./FileStorage/datavariable.txt";
        /// <summary>
        /// папка для хранения аудиофайлов
        /// </summary>
        public const string NameDirectoryAudio = @"./SoundFiles";
        public const string NameDirectoryTempFiles = @"./TempFiles";
        /// <summary>
        /// создает папку 
        /// возвращает "truе" если папка создана или уже существует
        /// </summary>
        /// <param name="nameDirect"></param>
        /// <returns>bool</returns>
        public static bool CreateDirectory(string nameDirect)
        {
            if (nameDirect != null)
            {
                DirectoryInfo dir = new DirectoryInfo(nameDirect);
                if (!dir.Exists)
                {
                    try
                    {
                        dir.Create();
                        return true;
                    }
                    catch (Exception)
                    {

                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// записывает в файл данные.(путей к аудиофайлам)
        /// append true для добавления данных в файл; значение false для перезаписи файла.
        /// </summary>
        /// <param name="path">полное имя файла</param>
        /// <param name="str"> записываемая строка</param>
        /// <param name="append">флаг добавления или перезаписи</param>
        public static void WritePath(string path, string str, bool append)
        {
            StreamWriter writer = null;
            if (!File.Exists(path))
            {
                using (writer = File.CreateText(path))
                {
                    writer.WriteLine(str);
                }
            }
            else
            {
                using (writer = new StreamWriter(path, append))
                {
                    writer.WriteLine(str);
                }
            }
        }
        /// <summary>
        /// возвращает список строк из файла
        /// </summary>
        /// <param name="path">имя файла</param>
        /// <returns>список путей к внешним папкам с айдиофайлами</returns>
        public static List<string> ReadListPathes(string path)
        {
            List<string> pathes = new List<string>();
            using (StreamReader sr = File.OpenText(path))
            {
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    pathes.Add(line);
                }
            }
            return pathes;
        }
        /// <summary>
        /// возвращает первую строку из текстового файла содержашем пути к внешним источникам аудиофайлов
        /// </summary>
        /// <param name="path">путь к файлу</param>
        /// <returns>строка путь папки внешнего  источника аудиофайлов</returns>
        public static string ReadPath(string path)
        {
            string line = null;
            using (StreamReader sr = File.OpenText(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    break;
                }
            }
            return line;
        }
        /// <summary>
        /// возвращет найденый файл
        /// </summary>
        /// <param name="fileName">имя файла</param>
        /// <param name="directory">имя папки поиска</param>
        /// <returns></returns>
        public static FileInfo SearchFile(string fileName, string directory)
        {
            FileInfo info = null;
            if (directory != null && directory != "")
            {
                DirectoryInfo dir = new DirectoryInfo(directory);
                if (dir.Exists)
                {
                    FileInfo[] ar = dir.GetFiles();
                    foreach (FileInfo item in ar)
                    {
                        if (item.Name.ToLower() == fileName)
                        {
                            info = item;
                            return info;
                        }
                    }
                }

            }
            return info;
        }
        /// <summary>
        /// копирует файл из указаного источника в указанную папку
        /// возвращает "FileInfo" скопированого файла
        /// </summary>
        /// <param name="sours">файл источник</param>
        /// <param name="pathDirect">папка для копирования</param>
        /// <returns>FileInfo скопированого файла</returns>
        public static FileInfo CopyTo(FileInfo sours, string pathDirect)
        {
            FileInfo fileInfo = null;
            if (sours != null)
            {
                string path = @pathDirect + "/" + sours.Name.ToLower();
                if (!File.Exists(path))
                {
                    fileInfo = sours.CopyTo(path);


                }
            }
            return fileInfo;
        }

        //public static FileInfo CopyTo(string sours, string pathDirect, string fileName, string fileExtension)
        //{
        //    FileInfo fileInfo = null;
        //    if (sours != null || pathDirect != null || fileName != null || fileExtension != null)
        //    {
        //        string path = pathDirect + "/" + fileName + fileExtension;
        //        if (!File.Exists(path))
        //        {
        //            fileInfo = new FileInfo(sours).CopyTo(path);
        //        }
        //        else
        //        {
        //            fileInfo = new FileInfo(path);
        //        }
        //    }
        //    return fileInfo;
        //}
        /// <summary>
        /// копирует аудиофайл в директорию NameDirectoryAudio
        /// </summary>
        /// <param name="sours">файл источник</param>
        /// <param name="isWrite">true  разрешает перезапись файла</param>
        /// <returns></returns>
        public static FileInfo CopyTo(FileInfo sours, bool isWrite)
        {
            FileInfo fileInfo = null;
            if (sours != null)
            {
                string path = FIleTools.NameDirectoryAudio + "/" + sours.Name.ToLower();
                try
                {
                    fileInfo = sours.CopyTo(path, isWrite);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + MethodBase.GetCurrentMethod().DeclaringType.FullName);
                    return null;
                }
            }
            return fileInfo;

        }
        /// <summary>
        /// создает необходимые папки в проекте
        /// </summary>
        public static void TotalCreateDirectory()
        {
            CreateDirectory(NameDirectoryStorage);
            CreateDirectory(NameDirectoryAudio);
        }

        public static string GetExtensionFile(string file)
        {
            FileInfo fi = new FileInfo(file);
            return fi.Extension;
        }
        public static FileInfo DeletFile(string pathDirectory,string fileName)
        {
            FileInfo file;
            try
            {
                 file = new FileInfo(Path.Combine(pathDirectory, fileName));
                file.Delete();
                return file;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
    }
}
