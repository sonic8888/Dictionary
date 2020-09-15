using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MyDictionary.XMLRead;
using System.ComponentModel;
using System.Reflection;

namespace MyDictionary.Tools
{
    public static class FTPSinchronisation
    {
        private const string PatnServerBD = @"ftp://andreysonic.asuscomm.com/sda1/Documents/BD/mobiles.db";
        public const string PatnLocalTempBD = @"./TempFiles/mobiles.db";
        public const string PatnLocalTempBDCopy = @"./TempFiles/mobilesCopy.db";
        public const string PatnLocalBD = @"./mobiles.db";
        private const string PatnDirectorySoundFilesServer = @"ftp://andreysonic.asuscomm.com/sda1/Documents/SoundFiles";
        private const string PathDirectorySoundFilesLocal = @"./SoundFiles/";
        private const string UrlServer = @"ftp://andreysonic.asuscomm.com";
        private const string PathServer = "/sda1/Documents/SoundFiles/";
        private const string UserName = "andrey";
        private const string Password = "sonic";
        /// <summary>
        /// скачивает БД с FTP сервера
        /// </summary>
        public static FtpWebResponse DownloadDb(BackgroundWorker worker, int oneprocent)
        {
            FtpWebRequest request = null;
            FtpWebResponse response = null;
            try
            {
                request = (FtpWebRequest)WebRequest.Create(PatnServerBD);
                request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(UserName, Password);
                response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                FileStream fs = new FileStream(PatnLocalTempBD, FileMode.Create);
                byte[] buffer = new byte[64];
                int size = 0;

                while ((size = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                {

                    fs.Write(buffer, 0, size);

                }
                fs.Close();
                response.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return response;
        }
        /// <summary>
        /// звписывает БД с локального ПК на сервер
        /// </summary>
        public static void WriteBD()
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(PatnServerBD);
                request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(UserName, Password);
                FileStream fs = new FileStream(PatnLocalBD, FileMode.Open);
                byte[] fileContents = new byte[fs.Length];
                fs.Read(fileContents, 0, fileContents.Length);
                fs.Close();
                request.ContentLength = fileContents.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        public static void WriteBD(string path)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(PatnServerBD);
                request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(UserName, Password);
                FileStream fs = new FileStream(path, FileMode.Open);
                byte[] fileContents = new byte[fs.Length];
                fs.Read(fileContents, 0, fileContents.Length);
                fs.Close();
                request.ContentLength = fileContents.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        public static FtpWebResponse WriteFile(string target, string sours)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(target);
                request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(UserName, Password);
                FileStream fs = new FileStream(sours, FileMode.Open);
                byte[] fileContents = new byte[fs.Length];
                fs.Read(fileContents, 0, fileContents.Length);
                fs.Close();
                request.ContentLength = fileContents.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();
                return response;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        /// <summary>
        /// возвращает дату создания БД на сервере
        /// </summary>
        /// <returns>DateTime</returns>
        public static DateTime GetDataServerBD()
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(PatnServerBD);
                request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                request.Credentials = new NetworkCredential(UserName, Password);
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                DateTime dt = response.LastModified.ToUniversalTime();
                response.Close();
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return DateTime.MinValue;
            }
        }
        /// <summary>
        /// возвращает дату последнего изменения файла БД на локальном ПК
        /// </summary>
        /// <returns>DateTime</returns>
        public static DateTime GetDataLocalBd()
        {
            try
            {
                FileInfo filebd = new FileInfo(PatnLocalBD);
                return filebd.LastWriteTime;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return DateTime.MinValue;
            }
        }
        /// <summary>
        /// Получить размер файла БД на серевере
        /// </summary>
        /// <returns></returns>
        public static long GetSizeServerDB()
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(PatnServerBD);
                request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.GetFileSize;
                request.Credentials = new NetworkCredential(UserName, Password);
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                return response.ContentLength;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }
        }

        /// <summary>
        /// получает список файлов из папки SoundFiles на сервере
        /// isFullName true - полное имя файла
        /// </summary>
        /// <param name="isFullName">true - полное имя файла</param>
        /// <returns>IEnumerable<string></returns>
        public static IEnumerable<string> GetListDirectoryServer(bool isFullName)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(PatnDirectorySoundFilesServer);
                request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(UserName, Password);
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                string str = reader.ReadToEnd();
                string[] stringSeparators = null;
                if (isFullName)
                {
                    stringSeparators = new string[] { "\r\n" };
                }
                else
                {
                    stringSeparators = new string[] { "\r\n", "/sda1/Documents/SoundFiles/" };

                }
                return str.Split(stringSeparators, StringSplitOptions.None).Where(n => n.Length > 0);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        /// <summary>
        /// получает список файлов из локальной папки SoundFiles
        /// isAddPath true - обавляет к имени файла path
        /// "path" путь к файлу ("/sda1/Documents/SoundFiles/") для удобства сравнения и последующей записи
        /// </summary>
        /// <param name="isAddPath">true - добавляет к имени файла path</param>
        /// <param name="path">путь к файлу ("/sda1/Documents/SoundFiles/") для удобства сравнения и последующей записи</param>
        /// <returns>IEnumerable<string></returns>
        public static IEnumerable<string> GetListDirectoryLocal(bool isAddPath, string path = null)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(PathDirectorySoundFilesLocal);
                FileInfo[] fileInfos = di.GetFiles();
                if (isAddPath)
                {
                    return fileInfos.Select(n => n.Name).Select(n => n.Insert(0, path));
                }
                else
                {
                    return fileInfos.Select(n => n.Name);

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        /// <summary>
        /// Загружает аудиофайл с FTPSever в локальную папку SoundFiles
        /// url-адресс файла на FTPSever (ftp://andreysonic.asuscomm.com/sda1/Documents/SoundFiles/apple.waw) 
        /// </summary>
        /// <param name="url">адресс файла на FTPSever</param>
        public static void DownLoadAudio(string url)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(UserName, Password);
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                string pathAudio = PathDirectorySoundFilesLocal + "/" + url.Remove(0, UrlServer.Length + PathServer.Length);
                FileInfo file = new FileInfo(pathAudio);
                FileStream fs = new FileStream(file.FullName, FileMode.Create);
                byte[] buffer = new byte[64];
                int size = 0;

                while ((size = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fs.Write(buffer, 0, size);

                }
                fs.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        /// <summary>
        /// загрузчик аудиофайлов с сервера на локальный ПК
        /// </summary>
        public static void LoaderAudio()
        {
            try
            {
                IEnumerable<string> soundsSever = GetListDirectoryServer(true);
                IEnumerable<string> sounsLocal = GetListDirectoryLocal(true, PathServer);
                IEnumerable<string> soundsExcept = soundsSever.Except(sounsLocal).Select(n => n.Insert(0, UrlServer));
                //IEnumerable<string> sounsLocal = BdTools.GetAudio();
                //IEnumerable<string> soundsExcept = soundsSever.Intersect(sounsLocal);
                if (soundsExcept.Count() > 0)
                {

                    foreach (string item in soundsExcept)
                    {

                        DownLoadAudio(item);

                    }
                }
                else
                {
                    MessageBox.Show("Нет новых аудиофайлов!");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        public static void LoaderAudioToServer()
        {
            try
            {
                IEnumerable<string> soundsSever = GetListDirectoryServer(true);
                IEnumerable<string> sounsLocal = GetListDirectoryLocal(true, PathServer);
                IEnumerable<string> soundsExcept = sounsLocal.Except(soundsSever).Select(n => n.Insert(0, UrlServer));
                if (soundsExcept.Count() > 0)
                {

                    foreach (string item in soundsExcept)
                    {
                        string s = item.Remove(0, PatnDirectorySoundFilesServer.Length + 1);
                        s = s.Insert(0, PathDirectorySoundFilesLocal);

                        WriteFile(item, s);

                    }

                }
                else
                {
                    MessageBox.Show("Нет новых аудиофайлов!");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }


    }
}
