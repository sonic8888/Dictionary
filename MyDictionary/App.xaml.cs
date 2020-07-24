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
using XMLRead;

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
            dataVariable = new DataVariable();
            random = new Random();
            StartNewThread();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
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

    }
}
