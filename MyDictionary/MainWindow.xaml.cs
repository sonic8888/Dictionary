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
using System.IO;
using System.Net;

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
            textboxCountTimeWork.Text = App.dataVariable.CountTimeWork.ToString();
            textboxCountWordTrenings.Text = App.dataVariable.CountWordTrenings.ToString();
            textboxCountWordSprint.Text = App.dataVariable.CountWordSprint.ToString();
            if (App.dataVariable.IsUpdateState == 1)
            {
                checkboxStatus.IsChecked = true;
            }
        }

        private void clickNewWord(object sender, RoutedEventArgs e)
        {

            ChoseWords chw = new ChoseWords();

            chw.Show();

        }

        //private void buttonDictionary_Click(object sender, RoutedEventArgs e)
        //{


        //    while (App.thread.IsAlive)
        //    {
        //        Thread.Sleep(50);
        //    }
        //    WindowsManager.CreateTotalDictionary();
        //}
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
            while (App.thread.IsAlive)
            {
                Thread.Sleep(200);
            }
            if (App.collection.Count < App.dataVariable.CountWordSprint)
            {
                MessageBox.Show("Для нормальной работы приложения кол-во слов в словаре должно быть не менее: " + App.dataVariable.CountWordSprint, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

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

        //private void buttonBreyShtorm2_Click(object sender, RoutedEventArgs e)
        //{
        //    ObservableCollection<MyWord> collec_5 = BdTools.ReadWord(App.dataVariable.CountWordTrenings);
        //    //ObservableCollection<MyWord> collec_5 = BdTools.ReadWord(3);
        //    ObservableCollection<MyWord> collectionTotal
        //        = BdTools.ReadWord();
        //    if (collec_5 != null && collectionTotal != null)
        //    {
        //        IEnumerable<MyWord> enumerable = collectionTotal.Except(collec_5);
        //        WindowsManager.GreateWindowBreyShtorm_2(collec_5.ToList(), enumerable.ToList(), true);

        //    }

        //}

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

        //private void buttonBreyShtorm3_Click(object sender, RoutedEventArgs e)
        //{
        //    ObservableCollection<MyWord> collec_5 = BdTools.ReadWord(App.dataVariable.CountWordTrenings);
        //    if (collec_5 != null)
        //    {

        //        WindowsManager.CreateWindowBreyShtorm_3(collec_5.ToList(), true);
        //    }
        //}

        //private void buttonBreyShtorm4_Click(object sender, RoutedEventArgs e)
        //{
        //    ObservableCollection<MyWord> collec_5 = BdTools.ReadWord(3);
        //    if (collec_5 != null)
        //    {
        //        WindowsManager.CreateWindowBreyShtorm_4(collec_5.ToList(), true);

        //    }
        //}

        //private void buttonBreyShtormResult_Click(object sender, RoutedEventArgs e)
        //{
        //    ObservableCollection<MyWord> collec_5 = BdTools.ReadWord(5);
        //    if (collec_5 != null)
        //    {
        //        foreach (MyWord item in collec_5)
        //        {
        //            item.TrueAnswer = App.random.Next(4);
        //        }
        //        WindowBreyShtormResult wbr = new WindowBreyShtormResult(collec_5.ToList());
        //        wbr.Show();

        //    }
        //}

        //private void buttonTest_Click(object sender, RoutedEventArgs e)
        //{
        //    int count = App.dataVariable.CountWordRepetition;
        //    List<MyWord> lists = BdTools.GetRandomListMyWord(count);
        //    if (lists != null)
        //    {
        //        WindowsManager.CreateWindowRepetition(lists);

        //    }
        //}

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

        //private void buttonSprint_Click(object sender, RoutedEventArgs e)
        //{

        //    List<MyWord> lists = BdTools.GetRandomListMyWord(10);
        //    if (lists != null)
        //    {

        //        WindowsManager.CreateWindowSprint(lists);
        //    }
        //}

        private void textboxCountTimeWork_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string str = textboxCountTimeWork.Text;
                int value;
                if (int.TryParse(str, out value))
                {
                    App.dataVariable.CountTimeWork = value;
                }
            }
        }

        private void textboxCountWordTrenings_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string str = textboxCountWordTrenings.Text;
                int value;
                if (int.TryParse(str, out value))
                {
                    App.dataVariable.CountWordTrenings = value;
                }
            }
        }

        private void buttonDictionary_Click_1(object sender, RoutedEventArgs e)
        {
            while (App.thread.IsAlive)
            {
                Thread.Sleep(50);
            }
            WindowsManager.CreateTotalDictionary();
        }

        private void buttonUpr_Click(object sender, RoutedEventArgs e)
        {
            while (App.thread.IsAlive)
            {
                Thread.Sleep(200);
            }
            if (App.collection.Count < App.dataVariable.CountWordSprint)
            {
                MessageBox.Show("Для нормальной работы приложения кол-во слов в словаре должно быть не менее: " + App.dataVariable.CountWordSprint, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            VisibilityElements(Visibility.Hidden, Visibility.Visible);
        }

        private void buttonWordTranslate_Click(object sender, RoutedEventArgs e)
        {
            List<MyWord> lists = BdTools.GetRandomListMyWord(App.dataVariable.CountWordTrenings);
            //ObservableCollection<MyWord> collec_5 = BdTools.ReadWord(3);
            ObservableCollection<MyWord> collectionTotal
                = BdTools.ReadWord();
            if (lists != null && collectionTotal != null)
            {
                IEnumerable<MyWord> enumerable = collectionTotal.Except(lists);
                WindowsManager.GreateWindowBreyShtorm_2(lists, enumerable.ToList(), true);

            }

        }

        private void buttonWordConstructor_Click(object sender, RoutedEventArgs e)
        {
            List<MyWord> lists = BdTools.GetRandomListMyWord(App.dataVariable.CountWordTrenings);
            if (lists != null)
            {

                WindowsManager.CreateWindowBreyShtorm_3(lists, true);
            }
        }

        private void buttonAudirovanie_Click(object sender, RoutedEventArgs e)
        {
            List<MyWord> lists = BdTools.GetRandomListMyWord(App.dataVariable.CountWordTrenings);
            if (lists != null)
            {
                WindowsManager.CreateWindowBreyShtorm_4(lists, true);

            }
        }

        private void buttonRepetition_Click(object sender, RoutedEventArgs e)
        {
            int count = App.dataVariable.CountWordRepetition;
            List<MyWord> lists = BdTools.GetRandomListMyWord(count);
            if (lists != null)
            {
                WindowsManager.CreateWindowRepetition(lists);

            }
        }

        private void buttonSprint_Click_1(object sender, RoutedEventArgs e)
        {
            List<MyWord> lists = BdTools.GetRandomListMyWord(App.dataVariable.CountWordSprint);
            if (lists != null)
            {

                WindowsManager.CreateWindowSprint(lists);
            }
        }
        private void VisibilityElements(Visibility visibilitiOne, Visibility visibilitiTwo)
        {
            buttonDictionary.Visibility = visibilitiOne;
            buttonTrenings.Visibility = visibilitiOne;
            buttonUpr.Visibility = visibilitiOne;
            buttonBack.Visibility = visibilitiTwo;
            buttonWordTranslate.Visibility = visibilitiTwo;
            buttonWordConstructor.Visibility = visibilitiTwo;
            buttonAudirovanie.Visibility = visibilitiTwo;
            buttonRepetition.Visibility = visibilitiTwo;
            buttonSprint.Visibility = visibilitiTwo;
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {

            VisibilityElements(Visibility.Visible, Visibility.Hidden);
        }

        private void textboxCountWordSprint_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string str = textboxCountWordSprint.Text;
                int value;
                if (int.TryParse(str, out value))
                {
                    App.dataVariable.CountWordSprint = value;
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            App.dataVariable.IsUpdateState = 1;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            App.dataVariable.IsUpdateState = 0;
        }

        private void buttonSinc_Click(object sender, RoutedEventArgs e)
        {

            DateTime dtServ = FTPSinchronisation.GetDataServerBD();
            DateTime dtLoc = FTPSinchronisation.GetDataLocalBd();
            if (dtServ < dtLoc)
            {
            MessageBox.Show("Sev: " + dtServ.ToString() + "   Loc " + dtLoc.ToString());
          
            }

        }
    }
}
