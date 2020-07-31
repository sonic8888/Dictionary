using MyDictionary.EF;
using MyDictionary.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
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
using XMLRead;

namespace MyDictionary.Repetition
{
    /// <summary>
    /// Логика взаимодействия для WindowRepetition.xaml
    /// </summary>
    public partial class WindowRepetition : Window
    {
        bool isLock = true;
        bool isPlay = true;
        //bool IsAnswer = true;
        //string pathCheck = @"/MyDictionary;component/Picture/GalkaLow.png";
        //string pathCross = @"/MyDictionary;component/Picture/krestikLow.png";

        //private int countdown;
        int countMilisek;//скорость прогрессбара-время для ответа
        int countMilisekDelay;//время задержки смены слов
        List<MyWord> myWords;
        Random random;
        int currentIndex = 0;
        MyWord currentMyWord;
        Button[] arrButtons;
        ControlTemplate templateDefault;
        ControlTemplate templateGreen;
        ControlTemplate templateRed;
        //SolidColorBrush colorDefault;
        //SolidColorBrush colorGreen;
        //SolidColorBrush colorRed;

        ProgressBar pr;
        DispatcherTimer dispatcherTimer;
        DispatcherTimer dispatcherTimerNext;
        public WindowRepetition(List<MyWord> words)
        {
            myWords = words;
            currentMyWord = myWords[currentIndex];
            countMilisek = App.dataVariable.CountMilisek;
            countMilisekDelay = App.dataVariable.CountMilisekDelay;
            CreateDispetherTime();
            CreateDispetherTimeNext();
            InitializeComponent();

            pr = progressbar;
            random = App.random;
            arrButtons = new Button[2];
            arrButtons[0] = buttonleft;
            arrButtons[1] = buttonright;
            InitElements();
            templateDefault = buttonleft.Template;
            templateGreen = (ControlTemplate)TryFindResource("buttonTemplateGreen");
            templateRed = (ControlTemplate)TryFindResource("buttonTemplateRed");

            //colorDefault = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            //colorGreen = new SolidColorBrush(Color.FromRgb(158, 235, 142));
            //colorRed = new SolidColorBrush(Color.FromRgb(235, 152, 142));
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
            if (isPlay)
            {
                FileInfo fi = FIleTools.SearchFile(currentMyWord.SoundName, FIleTools.NameDirectoryAudio);
                PlaySound(fi);
            }
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
            dispatcherTimerNext.Interval = new TimeSpan(0, 0, 0, 0, countMilisekDelay);

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
            isLock = false;
            if (currentIndex >= (myWords.Count - 1)) return;
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
            if (currentIndex < (myWords.Count - 1))
            {
                currentMyWord = myWords[++currentIndex];
            }
            else
            {
                dispatcherTimerNext.Stop();
            }
        }

        private void buttonleft_Click(object sender, RoutedEventArgs e)
        {
            if (isLock)
            {
                Button but = (Button)sender;
                MyWord mw = (MyWord)but.DataContext;
                if (mw.WordId == currentMyWord.WordId)
                {
                    //but.Background = colorGreen;
                    //but.Template = templateGreen;
                    AnswerTrue(but);
                }
                else
                {
                    //but.Background = colorRed;
                    //but.Template = templateRed;
                    AnswerFalse(but);
                }
            }
        }

        private void buttonright_Click(object sender, RoutedEventArgs e)
        {
            if (isLock)
            {
                Button but = (Button)sender;
                MyWord mw = (MyWord)but.DataContext;
                if (mw.WordId == currentMyWord.WordId)
                {
                    //but.Background = colorGreen
                    //but.Template = templateGreen;
                    AnswerTrue(but);
                }
                else
                {
                    //but.Background = colorRed;
                    //but.Template = templateRed;
                    AnswerFalse(but);
                }
            }
        }
        private void AnswerTrue(Button but)
        {
            but.Template = templateGreen;
            EndTime(true);
        }
        private void AnswerFalse(Button but)
        {
            //SystemSounds.Beep.Play();
            but.Template = templateRed;
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
            isLock = true;
            arrButtons[0].Template = templateDefault;
            arrButtons[1].Template = templateDefault;
        }
        private void PlaySound(FileInfo sound)
        {
            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri(sound.FullName));
            mediaPlayer.Play();
        }

        private void checkbox_Checked(object sender, RoutedEventArgs e)
        {
            isPlay = true;
        }

        private void checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            isPlay = false;
        }
    }
}
