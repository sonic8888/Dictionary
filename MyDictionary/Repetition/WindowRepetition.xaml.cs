using MyDictionary.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MyDictionary.Repetition
{
    /// <summary>
    /// Логика взаимодействия для WindowRepetition.xaml
    /// </summary>
    public partial class WindowRepetition : Window
    {
        bool isLock = true;
        string pathCheck = @"/MyDictionary;component/Picture/GalkaLow.png";
        string pathCross = @"/MyDictionary;component/Picture/krestikLow.png";
        Timer timer;
        ProgressBar pr;
        DispatcherTimer dispatcherTimer;
        public WindowRepetition()
        {
            InitializeComponent();
            pr = progressbar;
            CreateDispetherTime();
        }
         
        private void CreateDispetherTime()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(ProgressBar);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dispatcherTimer.Start();
        }

        private void buttontest_Click(object sender, RoutedEventArgs e)
        {

            if (isLock)
            {
                textblocktop.Visibility = Visibility.Hidden;
                elipsecount.Visibility = Visibility.Hidden;
                imageTop.Visibility = Visibility.Visible;
                isLock = false;
            }
            else
            {
                textblocktop.Visibility = Visibility.Visible;
                elipsecount.Visibility = Visibility.Visible;
                imageTop.Visibility = Visibility.Hidden;
                isLock = true;
            }
        }
      
        private void ProgressBar(object sender, EventArgs e)
        {
            if (progressbar.Value > 0)
            {
                progressbar.Value--;
            }
            else
            {
                EndTime();
            }
        }
        private void EndTime()
        {
            dispatcherTimer.Stop();
            progressbar.Value = 100;
            textblocktop.Visibility = Visibility.Hidden;
            elipsecount.Visibility = Visibility.Hidden;
            imageTop.Visibility = Visibility.Visible;
            imageTop.Source = MyTools.CreateBitmapImage(pathCross);
        }

    }
}
