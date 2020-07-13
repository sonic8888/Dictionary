using MyDictionary.EF;
using MyDictionary.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using XMLRead;

namespace MyDictionary
{
    public enum State
    {
        New = 1, Learn = 2, Know = 3
    }
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<MyWord> collection;
        Thread thread;
        private int countWord;



        public MainWindow()
        {
            InitializeComponent();
            FIleTools.CreateDirectory(FIleTools.NameDirectoryAudio);
            FIleTools.CreateDirectory(FIleTools.NameDirectoryStorage);
            StartNewThread();

            countWord = App.dataVariable.CountWordLearning;
            textboxCountWord.Text = countWord.ToString();
        }

        private void clickNewWord(object sender, RoutedEventArgs e)
        {

            ChoseWords chw = new ChoseWords();

            chw.Show();

        }

        private void buttonDictionary_Click(object sender, RoutedEventArgs e)
        {

            while (thread.IsAlive)
            {
                Thread.Sleep(50);
            }
            TotalDictionary td = new TotalDictionary(collection, this);
            td.Show();
        }
        private void ReadDictionary()
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
        public void StartNewThread()
        {
            thread = new Thread(new ThreadStart(ReadDictionary));
            thread.Start();
        }

        private void buttonBreyShtorm_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<MyWord> coll= BdTools.SelecWhereState(State.New);
            WindowBreyShtorm wbs = new WindowBreyShtorm(coll);
            wbs.Show();
        }

        private void textboxCountWord_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string str = textboxCountWord.Text;
                int value;
                if (int.TryParse(str, out value))
                {
                    App.dataVariable.CountWordLearning = value;
                }
            }
        }

        private void buttonBreyShtorm2_Click(object sender, RoutedEventArgs e)
        {
            WindowBreyShtorm_2 w2 = new WindowBreyShtorm_2();
            w2.Show();
        }
    }
}
