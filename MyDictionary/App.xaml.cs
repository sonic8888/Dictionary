using MyDictionary.EF;
using MyDictionary.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using MyDictionary.XMLRead;


namespace MyDictionary
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static DataVariable dataVariable;
        public static Random random;
        public static ObservableCollection<MyWord> collection;
        public static Thread thread;
        public static string User;
        public static string PathDbYandexDisc;
        public static string PathDbReservCopy;
        public static string PathAudioReservCopy;
        public static string PathDirectoryAudioYandexDisc;
        private static DateTime currentData = DateTime.Now;
        public static int countDayPeriodSave = 7;
        private static TimeSpan periodSave = new TimeSpan(countDayPeriodSave, 0, 0, 0);
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (!Directory.Exists(FIleTools.NameDirectoryStorage))
            {
                Directory.CreateDirectory(FIleTools.NameDirectoryStorage);
            }
            if (!Directory.Exists(FIleTools.NameDirectoryAudio))
            {
                Directory.CreateDirectory(FIleTools.NameDirectoryAudio);
            }
            if (!Directory.Exists(FIleTools.NameDirectoryTempFiles))
            {
                Directory.CreateDirectory(FIleTools.NameDirectoryTempFiles);
            }
            dataVariable = new DataVariable();
            random = new Random();
            User = System.Environment.GetEnvironmentVariable("HOMEPATH");
            PathDbYandexDisc = @"C:\" + User + "\\YandexDisk\\Dictionary\\Db\\mobiles.db";
            PathDirectoryAudioYandexDisc = @"C:\" + User + "\\YandexDisk\\Dictionary\\FilesSound\\";
            PathDbReservCopy = @"C:\" + User + "\\YandexDisk\\ReservCopyDictionary\\Db\\";
            PathAudioReservCopy = @"C:\" + User + "\\YandexDisk\\ReservCopyDictionary\\Filesound\\";

        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            if (!MyDictionary.App.dataVariable.IsChoseStorage)
            {
#if !DEBUG
                FTPSinchronisation.copyDbAndSoundFilesInYandexDisc(PathDbYandexDisc, PathDirectoryAudioYandexDisc);
                ReservCopydb();
#endif

            }
            dataVariable.WriteFile();
        }
        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }
        public static Thread StartNewThread()
        {
            thread = new Thread(new ThreadStart(ReadDictionary));
            thread.Start();
            return thread;
        }
        private static void ReadDictionary()
        {

            try
            {
                collection = BdTools.ReadWord();

            }
            catch (Exception)
            {

                return;
            }
        }
        private void ReservCopydb()
        {
            if ((currentData - dataVariable.TimeSave) >= periodSave)
            {
                string path = PathDbReservCopy + "mobiles_" + currentData.Year + "_" + currentData.Month + "_" + currentData.Day + "_" + currentData.Hour + ".db";
                FTPSinchronisation.copyDbAndSoundFilesInYandexDisc(path, PathAudioReservCopy);
                dataVariable.TimeSave = currentData;
            }
        }

    }
}
