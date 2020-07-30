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
using System.Windows.Threading;

namespace MyDictionary.Repetition
{
    /// <summary>
    /// Логика взаимодействия для WindowRepetition.xaml
    /// </summary>
    public partial class WindowRepetition : Window
    {
        bool isLock = true;
        //bool IsAnswer = true;
        //string pathCheck = @"/MyDictionary;component/Picture/GalkaLow.png";
        //string pathCross = @"/MyDictionary;component/Picture/krestikLow.png";

        //private int countdown;
        int countMilisek;//скорость прогрессбара-время для ответа
        List<MyWord> myWords;
        Random random;
        int currentIndex = 0;
        MyWord currentMyWord;
        Button[] arrButtons;


        ProgressBar pr;
        DispatcherTimer dispatcherTimer;
        DispatcherTimer dispatcherTimerNext;
        public WindowRepetition(List<MyWord> words)
        {
            myWords = words;
            currentMyWord = myWords[currentIndex];
            countMilisek = App.dataVariable.CountMilisek;
            CreateDispetherTime();
            CreateDispetherTimeNext();
            InitializeComponent();

            pr = progressbar;
            random = App.random;
            arrButtons = new Button[2];
            arrButtons[0] = buttonleft;
            arrButtons[1] = buttonright;
            InitElements();
        }
        private void InitElements()
        {
            
            textblockword.Text = currentMyWord.Word;
            MyWord[] arrMw = new MyWord[] { currentMyWord, GetRandoMyWord() };
            List<int> random = MyTools.GetRandomInt(new List<int>() { 0, 1 }, 2);
            for (int i = 0; i < 2; i++)
            {
                MyWord mw = arrMw[random[i]];
                arrButtons[i].DataContext = mw;
                TextBlock textblock = (TextBlock)arrButtons[i].Content;
                textblock.Text = GetTranslate(mw);
            }
            textblocktop.Text = (myWords.Count - currentIndex).ToString();
        }

        private void CreateDispetherTime()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(ProgressBar);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, countMilisek);
            dispatcherTimer.Start();
        }
        private void CreateDispetherTimeNext()
        {
            dispatcherTimerNext = new DispatcherTimer();
            dispatcherTimerNext.Tick += new EventHandler(NextStep);
            dispatcherTimerNext.Interval = new TimeSpan(0, 0, 1);
            
        }   
        private string GetTranslate(MyWord mw)
        {
            int count = mw.MyTranslates.Count;
            List<MyTranslate> lt = mw.MyTranslates.ToList();
            MyTranslate tr = lt[random.Next(count)];
            return tr.Translate;
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
                EndTime(false);
            }
        }
        private void EndTime(bool answer)
        {
            dispatcherTimer.Stop();
            //progressbar.Value = 100;
            textblocktop.Visibility = Visibility.Hidden;
            //elipsecount.Visibility = Visibility.Hidden;
            //imageTop.Visibility = Visibility.Visible;
            //imageTop.Source = MyTools.CreateBitmapImage(pathCross);
            if (answer)
            {
                lineyes1.Visibility = Visibility.Visible;
                lineyes2.Visibility = Visibility.Visible;
            }
            else
            {
                lineNo1.Visibility = Visibility.Visible;
                lineNo2.Visibility = Visibility.Visible;

            }
            dispatcherTimerNext.Start();
        }
        private MyWord GetRandoMyWord()
        {
            int index = 0;
            while ((index = random.Next(myWords.Count)) == currentIndex) ;
            return myWords[index];
        }
        private void NextCurrentMyWord()
        {
            currentMyWord = myWords[++currentIndex];
        }

        private void buttonleft_Click(object sender, RoutedEventArgs e)
        {
            Button but = (Button)sender;
            MyWord mw = (MyWord)but.DataContext;
            if (mw.WordId == currentMyWord.WordId)
            {
                AnswerTrue();
            }
            else
            {
                AnswerFalse();
            }
        }

        private void buttonright_Click(object sender, RoutedEventArgs e)
        {
            Button but = (Button)sender;
            MyWord mw = (MyWord)but.DataContext;
            if (mw.WordId == currentMyWord.WordId)
            {
                AnswerTrue();
            }
            else
            {
                AnswerFalse();
            }
        }
        private void AnswerTrue()
        {
            EndTime(true);
        }
        private void AnswerFalse()
        {
            EndTime(false);
        }

        //public int CountDown
        //{
        //    get { return countdown; }
        //    set { countdown = value; }
        //}
        private void HiddenLines()
        {
            lineyes1.Visibility = Visibility.Hidden;
            lineyes2.Visibility = Visibility.Hidden;
            lineNo1.Visibility = Visibility.Hidden;
            lineNo2.Visibility = Visibility.Hidden;
        }
        private void NextStep(object sender, EventArgs e)
        {
            NextCurrentMyWord();
            HiddenLines();
            textblocktop.Visibility = Visibility.Visible;
            InitElements();
            progressbar.Value = 100;
            dispatcherTimerNext.Stop();
            dispatcherTimer.Start();
        }

    }
}
