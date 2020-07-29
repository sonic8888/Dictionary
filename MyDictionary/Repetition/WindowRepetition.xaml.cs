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

namespace MyDictionary.Repetition
{
    /// <summary>
    /// Логика взаимодействия для WindowRepetition.xaml
    /// </summary>
    public partial class WindowRepetition : Window
    {
        bool isLock = true;
        double elapsed = 100;
        Timer timer;
        ProgressBar pr;
        public WindowRepetition()
        {
            InitializeComponent();
            pr = progressbar;
            GreateTimer();
        }
        private void GreateTimer()
        {
            TimerCallback tm = new TimerCallback(InitProgressBar);
            timer = new Timer(tm, null, 2000, 10);

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
        private void InitProgressBar(object ob)
        {
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (ThreadStart)
                delegate ()
                {
                    if (progressbar.Value > 0)
                    {
                        progressbar.Value--;
                    }
                });
        }

    }
}
