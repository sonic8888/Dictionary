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
using MyDictionary.Trenings;
using MyDictionary.Repetition;

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




        public MainWindow()
        {
            InitializeComponent();
            FIleTools.CreateDirectory(FIleTools.NameDirectoryAudio);
            FIleTools.CreateDirectory(FIleTools.NameDirectoryStorage);
            StartNewThread();
            textboxCountWord.Text = App.dataVariable.CountWordLearning.ToString();
            textboxCounSelekt.Text = App.dataVariable.CountSelectWord.ToString();
        }

        private void clickNewWord(object sender, RoutedEventArgs e)
        {

            ChoseWords chw = new ChoseWords();

            chw.Show();

        }

        private void buttonDictionary_Click(object sender, RoutedEventArgs e)
        {

            //while (thread.IsAlive)
            //{
            //    Thread.Sleep(50);
            //}
            //TotalDictionary td = new TotalDictionary(collection);
            //td.Show();
            while (App.thread.IsAlive)
            {
                Thread.Sleep(50);
            }
            WindowsManager.CreateTotalDictionary();
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

            //ObservableCollection<MyWord> collLearn = BdTools.SelecWhereState(State.Learn);
            //ObservableCollection<MyWord> collNew = BdTools.SelecWhereState(State.New);
            //foreach (MyWord m in collNew)
            //{
            //    collLearn.Add(m);
            //}
            ////ObservableCollection<MyWord> collLearn = BdTools.ReadWord(3);
            //WindowBreyShtorm wbs = new WindowBreyShtorm(collLearn);
            //wbs.Show();
            WindowsManager.CreateWindowBreyShtorm();
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
            ObservableCollection<MyWord> collec_5 = BdTools.ReadWord(8);
            ObservableCollection<MyWord> collectionTotal
                = BdTools.ReadWord();
            //IEnumerable<MyWord> enumer_5 = collec_5.AsEnumerable();
            IEnumerable<MyWord> enumerable = collectionTotal.Except(collec_5);
            //WindowBreyShtorm_2 w2 = new WindowBreyShtorm_2(collec_5.ToList(), enumerable.ToList());
            //w2.Show();
            WindowsManager.GreateWindowBreyShtorm_2(collec_5.ToList(), enumerable.ToList());

        }

        private void textboxCounSelekt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string str = textboxCounSelekt.Text;
                int value;
                if (int.TryParse(str, out value))
                {
                    App.dataVariable.CountSelectWord = value;
                }
            }
        }

        private void buttonBreyShtorm3_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<MyWord> collec_5 = BdTools.ReadWord(7);

            //WindowBreyShtorm_3 wb3 = new WindowBreyShtorm_3(collec_5.ToList());
            //wb3.Show();
            WindowsManager.CreateWindowBreyShtorm_3(collec_5.ToList());
        }

        private void buttonBreyShtorm4_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<MyWord> collec_5 = BdTools.ReadWord(5);
            //WindowBreyShtorm_4 wb4 = new WindowBreyShtorm_4(collec_5.ToList());
            //wb4.Show();
            WindowsManager.CreateWindowBreyShtorm_4(collec_5.ToList());
        }

        private void buttonBreyShtormResult_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<MyWord> collec_5 = BdTools.ReadWord(5);
            foreach (MyWord item in collec_5)
            {
                item.TrueAnswer = App.random.Next(4);
            }
            WindowBreyShtormResult wbr = new WindowBreyShtormResult(collec_5.ToList());
            wbr.Show();
        }

        private void buttonTest_Click(object sender, RoutedEventArgs e)
        {
            WindowRepetition wr = new WindowRepetition();
            wr.Show();
           
        }
    }
}
