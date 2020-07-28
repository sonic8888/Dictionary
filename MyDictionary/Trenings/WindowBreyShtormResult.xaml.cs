using MyDictionary.EF;
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

namespace MyDictionary.Trenings
{
    /// <summary>
    /// Логика взаимодействия для WindowBreyShtormResult.xaml
    /// </summary>
    public partial class WindowBreyShtormResult : Window
    {
        List<MyWord> myWords;
        Thread thread;
        public WindowBreyShtormResult(List<MyWord> words)
        {
            myWords = words;
            InitializeComponent();
            listwiewresult.ItemsSource = myWords;
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            //this.DialogResult = true;
            StartNewThread();
            this.Close();
        }

        private void buttonBace_Click(object sender, RoutedEventArgs e)
        {
            foreach (MyWord item in myWords)
            {
                item.TrueAnswer = 0;
            }
            WindowsManager.CreateWindowBreyShtorm(myWords);
            this.Close();
        }
        private void UpdateState()
        {
            foreach (MyWord item in myWords)
            {
                State st = State.Learn;
                if (3 - item.TrueAnswer == 0)
                {
                    st = State.Know;
                }
                BdTools.UpdateStateMyWord(item.WordId, (int)st);
            }
        }
        public Thread StartNewThread()
        {
            thread = new Thread(new ThreadStart(UpdateState));
            thread.Start();
            return thread;
        }
    }
}
