using MyDictionary.EF;
using MyDictionary.Tools;
using MyDictionary.Trenings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

using MyDictionary.XMLRead;

namespace MyDictionary
{
    /// <summary>
    /// Логика взаимодействия для WindowBreyShtorm_2.xaml
    /// </summary>
    public partial class WindowBreyShtorm_2 : Window
    {
        List<MyWord> trenings;
        List<MyWord> resurse;
        List<Button> arrButtons;
        int countButton = 5;
        int count = 0;
        string str = "Не знаю ):";
        string esAnswer = "Правильный ответ!";
        string noAnswer = "Не верный ответ!";
        string strFront;
        Random random;
        int currentRandowValue;
        Brush backgroundButtonDefault;
        Brush backgroundBorderDefault;
        Brush backgroundRed;
        Brush backgroundGreen;
        Brush buttonForegroundBlue;
        Brush buttonForegroundDefault;
        bool isEnabledButton;
        double fontsize;
        int countAnswerTrue = 0;
        bool isTrenings;
        int _countTrue = 0;
        int countFalse = 0;
        public WindowBreyShtorm_2(List<MyWord> trenings, List<MyWord> resurse, bool istrenings)
        {
            this.trenings = trenings;
            countFalse = trenings.Count;
            this.resurse = resurse;
            isTrenings = istrenings;
            InitializeComponent();
            arrButtons = new List<Button>();
            arrButtons.Add(buttonOne);
            arrButtons.Add(buttontwo);
            arrButtons.Add(buttonthree);
            arrButtons.Add(buttonfour);
            arrButtons.Add(buttonfive);
            isEnabledButton = true;
            random = App.random; ;
            InitValue();
            InitResurse();
            backgroundButtonDefault = buttonsix.Background;
            backgroundBorderDefault = border.Background;
            backgroundRed = new SolidColorBrush(Color.FromRgb(243, 182, 219));
            backgroundGreen = new SolidColorBrush(Color.FromRgb(179, 255, 215));
            buttonForegroundBlue = new SolidColorBrush(Colors.Blue);
            fontsize = buttonsix.FontSize;
            buttonForegroundDefault = buttonsix.Foreground;
            strFront = "Далее ➞";
            textblockTotal.Text = "Всего: " + trenings.Count().ToString();
        }

        private void buttonSound_Click(object sender, RoutedEventArgs e)
        {
            FileInfo fi = FIleTools.SearchFile(trenings[count].SoundName, FIleTools.NameDirectoryAudio);
            PlaySound(fi);
        }
        private void PlaySound(FileInfo sound)
        {
            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri(sound.FullName));
            mediaPlayer.Play();
        }
        private void InitValue()
        {
            textblockWord.Text = trenings[count].Word;
            currentRandowValue = random.Next(countButton);
            arrButtons[currentRandowValue].DataContext = trenings[count];
            arrButtons[currentRandowValue].Content = trenings[count].TranslateStr;
            buttonsix.Content = str;
            FileInfo fi = FIleTools.SearchFile(trenings[count].SoundName, FIleTools.NameDirectoryAudio);
            PlaySound(fi);
            textblockCountword.Text = (count + 1).ToString() + "/" + trenings.Count().ToString();
        }
        private void InitResurse()
        {
            List<int> list = new List<int>();
            for (int i = 0; i < resurse.Count; i++)
            {
                list.Add(random.Next(resurse.Count));
            }
            int j = 0;
            foreach (int item in list.Distinct().Take(countButton))
            {
                if (j == currentRandowValue)
                {
                    j++;
                    continue;
                }
                arrButtons[j].DataContext = resurse[item];
                arrButtons[j].Content = resurse[item].TranslateStr;
                j++;
            }
        }

        private void buttonOne_Click(object sender, RoutedEventArgs e)
        {
            if (!isEnabledButton)
            {
                return;
            }
            Button bt = sender as Button;
            MyWord mw = bt.DataContext as MyWord;
            if (trenings[count].WordId == mw.WordId)
            {
                MethodYes(bt);
            }
            else
            {
                MethodNo(bt);
            }
            isEnabledButton = false;
        }

        private void buttontwo_Click(object sender, RoutedEventArgs e)
        {
            if (!isEnabledButton)
            {
                return;
            }
            Button bt = sender as Button;
            MyWord mw = bt.DataContext as MyWord;
            if (trenings[count].WordId == mw.WordId)
            {
                MethodYes(bt);
            }
            else
            {
                MethodNo(bt);
            }
            isEnabledButton = false;
        }

        private void buttonthree_Click(object sender, RoutedEventArgs e)
        {
            if (!isEnabledButton)
            {
                return;
            }
            Button bt = sender as Button;
            MyWord mw = bt.DataContext as MyWord;
            if (trenings[count].WordId == mw.WordId)
            {
                MethodYes(bt);
            }
            else
            {
                MethodNo(bt);
            }
            isEnabledButton = false;
        }

        private void buttonfour_Click(object sender, RoutedEventArgs e)
        {
            if (!isEnabledButton)
            {
                return;
            }
            Button bt = sender as Button;
            MyWord mw = bt.DataContext as MyWord;
            if (trenings[count].WordId == mw.WordId)
            {
                MethodYes(bt);
            }
            else
            {
                MethodNo(bt);
            }
            isEnabledButton = false;
        }

        private void buttonfive_Click(object sender, RoutedEventArgs e)
        {
            if (!isEnabledButton)
            {
                return;
            }
            Button bt = sender as Button;
            MyWord mw = bt.DataContext as MyWord;
            if (trenings[count].WordId == mw.WordId)
            {
                MethodYes(bt);
            }
            else
            {
                MethodNo(bt);
            }
            isEnabledButton = false;
        }

        private void buttonsix_Click(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            if ((string)bt.Content == str)
            {
                arrButtons[currentRandowValue].Background = backgroundGreen;
                buttonsix.Foreground = buttonForegroundBlue;
                buttonsix.Content = strFront;
                buttonsix.FontSize = 24;

                return;
            }
            Next();
            InitDefault();
            isEnabledButton = true;

        }

        private void Next()
        {
            if (++count < trenings.Count)
            {
                InitValue();
                InitResurse();
            }
            else
            {
                GreateWindowBreyShtorm3();
            }
        }
        private void MethodYes(Button bt)
        {

            if (trenings[count].MyExamples.Count > 0)
            {
                MyExample ex = trenings[count].MyExamples.First();

                textblockExample.Text = ex.Example;
                textblockContext.Text = "Контекст:";

            }
            bt.Background = backgroundGreen;
            border.Background = backgroundGreen;
            textBlockAnswer.Text = esAnswer;
            buttonsix.Content = strFront;
            buttonsix.FontSize = 24;
            buttonsix.Foreground = buttonForegroundBlue;
            trenings[count].TrueAnswer++;
            countAnswerTrue++;
        }
        private void MethodNo(Button bt)
        {
            bt.Background = backgroundRed;
            arrButtons[currentRandowValue].Background = backgroundGreen;
            border.Background = backgroundRed;
            textBlockAnswer.Text = noAnswer;
            buttonsix.Foreground = buttonForegroundBlue;
            buttonsix.Content = strFront;
            buttonsix.FontSize = 24;
        }
        private void InitDefault()
        {
            foreach (Button button in arrButtons)
            {
                button.Background = backgroundButtonDefault;
            }
            buttonsix.FontSize = fontsize;
            buttonsix.Foreground = buttonForegroundDefault;
            buttonsix.Content = str;
            border.Background = backgroundBorderDefault;
            textblockContext.Text = null;
            textBlockAnswer.Text = null;
            textblockExample.Text = null;
        }
        private void GreateWindowBreyShtorm3()
        {

            if (isTrenings)
            {
                VisibilityElements();
                FinishAnimation();
            }
            else
            {
                WindowsManager.CreateWindowBreyShtorm_3(trenings, false);
                this.Close();

            }

        }
        private void VisibilityElements()
        {
            buttonOne.Visibility = Visibility.Hidden;
            buttontwo.Visibility = Visibility.Hidden;
            buttonthree.Visibility = Visibility.Hidden;
            buttonfour.Visibility = Visibility.Hidden;
            buttonfive.Visibility = Visibility.Hidden;
            buttonsix.Visibility = Visibility.Hidden;
            buttonSound.Visibility = Visibility.Hidden;
            textblockWord.Visibility = Visibility.Hidden;
            border.Visibility = Visibility.Hidden;
            canvasAnime.Visibility = Visibility.Visible;
            textblockLeft.Visibility = Visibility.Visible;
            textblockRight.Visibility = Visibility.Visible;
            textblockTotal.Visibility = Visibility.Visible;
        }
        private void FinishAnimation()
        {
            DoubleAnimation danime = new DoubleAnimation();

            danime.From = 1;
            DispatcherTimer timer = CreateDispetcherTrue();
            timer?.Start();
            danime.To = countAnswerTrue * recRed.ActualWidth / trenings.Count;
            danime.Duration = TimeSpan.FromMilliseconds(2000);
            recGreen.BeginAnimation(Rectangle.WidthProperty, danime);
            //dispatcherTimer.Start();
        }
        private DispatcherTimer CreateDispetcherTrue()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(InitTextBlock);
            int delay = 0;
            if (countAnswerTrue != 0)
            {
                delay = 2000 / (trenings.Count + 1);
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
            textblockLeft.Text = "Верно: " + _countTrue.ToString();
            textblockRight.Text = "Не верно: " + countFalse.ToString();
            countFalse--;
            if (++_countTrue > countAnswerTrue)
            {
                timer.Stop();
            }
        }
    }
}
