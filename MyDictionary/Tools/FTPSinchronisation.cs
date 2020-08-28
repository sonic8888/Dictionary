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

namespace MyDictionary.Tools
{
    public static class FTPSinchronisation
    {
        private const string PatnServerBD = @"ftp://andreysonic.asuscomm.com/sda1/Documents/BD/mobiles.db";
        public const string PatnLocalTempBD = @"./TempFiles/mobiles.db";
        public const string PatnLocalBD = @"./mobiles.db";
        private const string PatnDirectorySoundFiles = @"ftp://andreysonic.asuscomm.com/sda1/Documents/SoundFiles";
        private const string UserName = "andrey";
        private const string Password = "sonic";
        /// <summary>
        /// скачивает БД с FTP сервера
        /// </summary>
        public static FtpWebResponse DownloadDb(BackgroundWorker worker, int oneprocent)
        {

            int tempByte = 0;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(PatnServerBD);
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(UserName, Password);
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            try
            {
                Stream responseStream = response.GetResponseStream();
                FileStream fs = new FileStream(PatnLocalBD, FileMode.Create);
                byte[] buffer = new byte[64];
                int size = 0;

                while ((size = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    //tempByte += size;
                    //if (tempByte >= oneprocent)
                    //{
                    //    worker.ReportProgress(tempByte);
                    //    tempByte = 0;
                    //}
                    fs.Write(buffer, 0, size);

                }
                fs.Close();
                response.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            return response;
        }
        public static void WriteBD()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(PatnServerBD);
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(UserName, Password);
            FileStream fs = new FileStream(PatnLocalTempBD, FileMode.Open);
            byte[] fileContents = new byte[fs.Length];
            fs.Read(fileContents, 0, fileContents.Length);
            fs.Close();
            request.ContentLength = fileContents.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            MessageBox.Show("Загрузка файлов завершена. Статус:" + response.StatusDescription);
            response.Close();
        }
        public static FtpWebResponse WriteFile(string target, string sours)
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
        public static FtpWebResponse DownloadFile(string pathSours, string pathTarget)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(pathSours);
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(UserName, Password);
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            FileStream fs = new FileStream(pathTarget, FileMode.Create);
            byte[] buffer = new byte[64];
            int size = 0;

            while ((size = responseStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                fs.Write(buffer, 0, size);

            }
            fs.Close();
            response.Close();
            return response;
        }
        public static DateTime GetDataServerBD()
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
        public static DateTime GetDataLocalBd()
        {
            FileInfo filebd = new FileInfo(PatnLocalBD);
            return filebd.LastWriteTime;
        }
        public static long GetSizeServerDB()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(PatnServerBD);
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.GetFileSize;
            request.Credentials = new NetworkCredential(UserName, Password);
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            return response.ContentLength;
        }
        public static void GetListDirectory()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(PatnDirectorySoundFiles);
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = new NetworkCredential(UserName, Password);
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string str = reader.ReadToEnd();
            string[] vs = str.Split(new char[] { '\r'});
            MessageBox.Show(reader.ReadToEnd());


        }


    }
}
