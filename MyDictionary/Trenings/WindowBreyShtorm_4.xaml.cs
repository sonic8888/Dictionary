using MyDictionary.EF;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using XMLRead;
using MyDictionary.Tools;
using System.Threading;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace MyDictionary.Trenings
{
    /// <summary>
    /// Логика взаимодействия для WindowBreyShtorm_4.xaml
    /// </summary>
    public partial class WindowBreyShtorm_4 : Window
    {
        List<MyWord> myWords;
        int currentword = 0;
        string message = "Введите слово";
        string contentNext = "Далее ➞";
        string contentControl = "Проверить";
        Thread thread;
        bool isTrenings;
        int countAnswerTrue;
        int _countTrue = 0;
        int countFalse;
        public WindowBreyShtorm_4(List<MyWord> words, bool istrenings)
        {
            myWords = words;
            countFalse = myWords.Count;
            isTrenings = istrenings;
            InitializeComponent();
            textboxkword.Focus();
            Play();
            textblockTotal.Text = "Всего: " + myWords.Count();
        }

        private void buttonSound_Click(object sender, RoutedEventArgs e)
        {
            FileInfo fi = FIleTools.SearchFile(myWords[currentword].SoundName, FIleTools.NameDirectoryAudio);
            PlaySound(fi);
        }
        private void PlaySound(FileInfo sound)
        {
            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri(sound.FullName));
            mediaPlayer.Play();
        }

        private void textboxkword_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {



                ControlAnswer();

            }
        }
        private void AnswerYes()
        {
            VisibilityOn();
            InitTextBlock();
            myWords[currentword].TrueAnswer++;
            countAnswerTrue++;
        }
        private void AnswerNo()
        {
            VisibilityOn();
            textblockerror.Visibility = Visibility.Visible;
            textblockerror.Text = textboxkword.Text;
            InitTextBlock();
        }
        private void InitTextBlock()
        {
            textblockword.Text = myWords[currentword].Word;
            textblocktranscription.Text = MyTools.WrapTranscription(myWords[currentword].Transcription);
            textblocktranslation.Text = myWords[currentword].MyTranslates.First().Translate;
            if (myWords[currentword].MyExamples.Count > 0)
            {
                textblockexample.Text = MyTools.ExampleSpace(myWords[currentword].MyExamples.First().Example);

            }
        }
        private void VisibilityOn()
        {
            textboxkword.Visibility = Visibility.Collapsed;
            textblockmessage.Visibility = Visibility.Collapsed;
            textblockword.Visibility = Visibility.Visible;
            textblocktranscription.Visibility = Visibility.Visible;
            textblockexample.Visibility = Visibility.Visible;
            textblocktranslation.Visibility = Visibility.Visible;
            textblockword.Foreground = new SolidColorBrush(Colors.Green);
            textblocktranscription.Foreground = new SolidColorBrush(Colors.Gray);

        }
        private void VisibilityOf()
        {
            textboxkword.Visibility = Visibility.Visible;
            textblockmessage.Visibility = Visibility.Visible;
            textblockword.Visibility = Visibility.Collapsed;
            textblocktranscription.Visibility = Visibility.Collapsed;
            textblocktranslation.Visibility = Visibility.Collapsed;
            textblockerror.Visibility = Visibility.Collapsed;
            textblockexample.Visibility = Visibility.Collapsed;
        }
        private void Play()
        {
            FileInfo fi = FIleTools.SearchFile(myWords[currentword].SoundName, FIleTools.NameDirectoryAudio);
            PlaySound(fi);
        }

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            if ((string)bt.Content == contentControl)
            {

                ControlAnswer();
            }
            else
            {
                NextWord();
            }
        }
        private void ControlAnswer()
        {
            if (textboxkword.Text == "")
            {
                MessageBox.Show(message);
                return;
            }
            if (textboxkword.Text.ToUpper() == myWords[currentword].Word.ToUpper())
            {
                AnswerYes();
            }
            else
            {
                AnswerNo();
            }
            buttonNext.Content = contentNext;
            buttonNext.Background = new SolidColorBrush(Colors.Blue);
            buttonNext.Foreground = new SolidColorBrush(Colors.White);
        }
        private void NextWord()
        {
            if (currentword < myWords.Count - 1)
            {
                currentword++;
                VisibilityOf();
                Play();
                textboxkword.Text = "";
                textboxkword.Focus();
                buttonNext.Content = contentControl;
            }
            else
            {
                if (isTrenings)
                {
                    VisibilityElements();
                    FinishAnimation();
                }
                else
                {
                    //завершение и подведение итога
                    FinishAndChageState();

                }
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((string)buttonNext.Content == contentNext)
            {
                NextWord();
            }
        }
        private void FinishAndChageState()
        {
            WindowBreyShtormResult wbr = new WindowBreyShtormResult(myWords);
            wbr.Show();
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
        private void VisibilityElements()
        {
            textboxkword.Visibility = Visibility.Hidden;
            textblockmessage.Visibility = Visibility.Hidden;
            textblockerror.Visibility = Visibility.Hidden;
            textblockword.Visibility = Visibility.Hidden;
            textblocktranscription.Visibility = Visibility.Hidden;
            textblocktranslation.Visibility = Visibility.Hidden;
            textblockexample.Visibility = Visibility.Hidden;
            buttonSound.Visibility = Visibility.Hidden;
            canvasAnime.Visibility = Visibility.Visible;
            textblockLeft.Visibility = Visibility.Visible;
            textblockRight.Visibility = Visibility.Visible;
            textblockTotal.Visibility = Visibility.Visible;
            buttonNext.Click -= buttonNext_Click;
            buttonNext.Click += Close;
            buttonNext.Content = "Завершить";

        }
        private void FinishAnimation()
        {
            DoubleAnimation danime = new DoubleAnimation();

            danime.From = 1;
            DispatcherTimer timer = CreateDispetcherTrue();
            timer?.Start();
            danime.To = countAnswerTrue * recRed.ActualWidth / myWords.Count;
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
                delay = 2000 / (myWords.Count + 1);
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
        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }


}
