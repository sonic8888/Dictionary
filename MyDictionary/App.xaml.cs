using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
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

    }
}
