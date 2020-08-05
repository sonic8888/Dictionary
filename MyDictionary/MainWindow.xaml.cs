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
            textboxCountRepetition.Text = App.dataVariable.CountWordRepetition.ToString();
            textboxCountMlSekRepetition.Text = App.dataVariable.CountMilisek.ToString();
            textboxCountMlSekDelayRepetition.Text = App.dataVariable.CountMilisekDelay.ToString();
        }

        private void clickNewWord(object sender, RoutedEventArgs e)
        {

            ChoseWords chw = new ChoseWords();

            chw.Show();

        }

        private void buttonDictionary_Click(object sender, RoutedEventArgs e)
        {

         
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
            IEnumerable<MyWord> enumerable = collectionTotal.Except(collec_5);
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
            WindowsManager.CreateWindowBreyShtorm_3(collec_5.ToList());
        }

        private void buttonBreyShtorm4_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<MyWord> collec_5 = BdTools.ReadWord(5);
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
            int count = App.dataVariable.CountWordRepetition;
            List<MyWord> lists = BdTools.GetRandomListMyWord(count);
            WindowsManager.CreateWindowRepetition(lists);
        }

        private void textboxCountRepetition_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string str = textboxCountRepetition.Text;
                int value;
                if (int.TryParse(str, out value))
                {
                    App.dataVariable.CountWordRepetition = value;
                }
            }
        }

        private void textboxCountMlSekRepetition_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string str = textboxCountMlSekRepetition.Text;
                int value;
                if (int.TryParse(str, out value))
                {
                    App.dataVariable.CountMilisek = value;
                }
            }
        }

        private void textboxCountMlSekDelayRepetition_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string str = textboxCountMlSekDelayRepetition.Text;
                int value;
                if (int.TryParse(str, out value))
                {
                    App.dataVariable.CountMilisekDelay = value;
                }
            }
        }
    }
}
