﻿using MyDictionary.EF;
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
using System.Windows.Media.Animation;
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
        int countTrue = 0;//кол-во верных ответов
        int countFalse = 0;
        int _countTrue = 0;
        int countMilisekAnimationPath = 100;
        int countMilisekFinishAnimation = 2000;
        int countMilisekDelayFinishAnimation = 2000;
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
                //imageTop.Visibility = Visibility.Visible;
                isLock = false;
            }
            else
            {
                textblocktop.Visibility = Visibility.Visible;
                elipsecount.Visibility = Visibility.Visible;
                //imageTop.Visibility = Visibility.Hidden;
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
            progressbar.Value = 100;
            textblocktop.Visibility = Visibility.Hidden;
         
            if (answer)
            {
                StartAnimationV();
            }
            else
            {
                StartAnimationX();

            }
            if (currentIndex >= (myWords.Count - 1))
            {
                DispatcherTimer timer = new DispatcherTimer();
                timer.Tick += new EventHandler(Summarizing);
                timer.Interval = new TimeSpan(0, 0, 0, 0, countMilisekDelayFinishAnimation);
                timer.Start();
                //Summarizing();
                return;
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
                dispatcherTimer.Stop();
                if (mw.WordId == currentMyWord.WordId)
                {
                    
                    but.Template = templateGreen;
                    AnswerTrue(but);
                }
                else
                {
                   
                    but.Template = templateRed;
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
                dispatcherTimer.Stop();
                if (mw.WordId == currentMyWord.WordId)
                {
                    
                    but.Template = templateGreen;
                    AnswerTrue(but);
                }
                else
                {
                   
                    but.Template = templateRed;
                    AnswerFalse(but);
                }
            }
        }
        private void AnswerTrue(Button but)
        {
            but.Template = templateGreen;
            countTrue++;
            EndTime(true);
        }
        private void AnswerFalse(Button but)
        {
            //SystemSounds.Beep.Play();
            but.Template = templateRed;
            EndTime(false);
        }

      
        private void HiddenLines()
        {
        
            BackAnimationV();
            BackAnimationX();
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
        private void Summarizing(object sender, EventArgs e)
        {
            DispatcherTimer timer = sender as DispatcherTimer;
            timer.Stop();
            textblocktop.Visibility = Visibility.Visible;
            textblocktop.Text = myWords.Count.ToString();
            textblocktop.Foreground = new SolidColorBrush(Colors.Black);
            HiddenContent();
            VisibleAnimation();
            FinishAnimation();
        }
        private void HiddenContent()
        {
            HiddenLines();
            elipsecount.Visibility = Visibility.Hidden;
            textblockword.Visibility = Visibility.Hidden;
            buttonleft.Visibility = Visibility.Hidden;
            buttonright.Visibility = Visibility.Hidden;
            progressbar.Visibility = Visibility.Hidden;
            checkbox.Visibility = Visibility.Hidden;
        }
        private void VisibleAnimation()
        {
            textblockTrue.Visibility = Visibility.Visible;
            textblockFalse.Visibility = Visibility.Visible;
            recRed.Visibility = Visibility.Visible;
            recGreen.Visibility = Visibility.Visible;
            buttonBack.Visibility = Visibility.Visible;
            buttonEnd.Visibility = Visibility.Visible;
            labelTotal.Visibility = Visibility;
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            int count = App.dataVariable.CountWordRepetition;
            List<MyWord> lists = BdTools.GetRandomListMyWord(count);
            WindowsManager.CreateWindowRepetition(lists);
        }

        private void buttonEnd_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void FinishAnimation()
        {
            DoubleAnimation danime = new DoubleAnimation();
            countFalse = myWords.Count;
            DispatcherTimer timer = CreateDispetcherTrue();
            timer?.Start();
            danime.From = 1;
            danime.To = countTrue * recRed.Width / myWords.Count;
            danime.Duration = TimeSpan.FromMilliseconds(countMilisekFinishAnimation);
            recGreen.BeginAnimation(Rectangle.WidthProperty, danime);
            //dispatcherTimer.Start();
        }
        private DispatcherTimer CreateDispetcherTrue()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(InitTextBlock);
            int delay = 0;
            if (countTrue != 0)
            {
                delay = countMilisekFinishAnimation / (countTrue + 2);
            }
            else
            {
                delay = 1;
            }
            timer.Interval = new TimeSpan(0, 0, 0, 0, delay);
            return timer;

        }
        private void InitTextBlock(object sender, EventArgs e)
        {
            DispatcherTimer timer = sender as DispatcherTimer;
            textblockFalse.Text = countFalse.ToString();
            textblockTrue.Text = _countTrue.ToString();
            countFalse--;
            if (++_countTrue > countTrue)
            {
                timer.Stop();
            }
        }

        private void buttontest_Click_1(object sender, RoutedEventArgs e)
        {
            StartAnimationV();
        }
        private void StartAnimationX()
        {
            pathX.Visibility = Visibility.Visible;
            PointAnimation myPointAnimation = new PointAnimation();
            myPointAnimation.Duration = TimeSpan.FromMilliseconds(countMilisekAnimationPath);
            myPointAnimation.From = lineRed.EndPoint;
            myPointAnimation.To = new Point(100, 70);
            myPointAnimation.Completed += CreateСontinuationAnimationX;
            lineRed.BeginAnimation(LineGeometry.EndPointProperty, myPointAnimation);
        }
        private void CreateСontinuationAnimationX(object sender, EventArgs e)
        {

            PointAnimation myPointAnimation = new PointAnimation();
            myPointAnimation.Duration = TimeSpan.FromMilliseconds(countMilisekAnimationPath);
            myPointAnimation.From = lineRed2.EndPoint;
            myPointAnimation.To = new Point(70, 70);
            lineRed2.BeginAnimation(LineGeometry.EndPointProperty, myPointAnimation);

        }
        private void BackAnimationX()
        {
            lineRed.BeginAnimation(LineGeometry.EndPointProperty, null);
            lineRed.EndPoint = new Point(70, 40);
            lineRed2.BeginAnimation(LineGeometry.EndPointProperty, null);
            lineRed2.EndPoint = new Point(100, 40);
            pathX.Visibility = Visibility.Hidden;

        }
        private void StartAnimationV()
        {
            pathV.Visibility = Visibility.Visible;
            PointAnimation myPointAnimation = new PointAnimation();
            myPointAnimation.Duration = TimeSpan.FromMilliseconds(countMilisekAnimationPath);
            myPointAnimation.From = lineWhite.EndPoint;
            myPointAnimation.To = new Point(85, 70);
            myPointAnimation.Completed += CreateСontinuationAnimationV;
            lineWhite.BeginAnimation(LineGeometry.EndPointProperty, myPointAnimation);

        }
        private void CreateСontinuationAnimationV(object sender, EventArgs e)
        {
            PointAnimation myPointAnimation = new PointAnimation();
            myPointAnimation.Duration = TimeSpan.FromMilliseconds(countMilisekAnimationPath);
            myPointAnimation.From = lineWhite2.EndPoint;
            myPointAnimation.To = new Point(100, 40);
            lineWhite2.BeginAnimation(LineGeometry.EndPointProperty, myPointAnimation);
        }
        private void BackAnimationV()
        {
            lineWhite.BeginAnimation(LineGeometry.EndPointProperty, null);
            lineWhite.EndPoint = new Point(70, 40);
            lineWhite2.BeginAnimation(LineGeometry.EndPointProperty, null);
            lineWhite2.EndPoint = new Point(85, 70);
            pathV.Visibility = Visibility.Hidden;

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            dispatcherTimerNext.Stop();
            dispatcherTimer.Stop();
        }
    }
}
