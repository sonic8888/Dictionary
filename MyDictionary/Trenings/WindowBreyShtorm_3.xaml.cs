using MyDictionary.EF;
using MyDictionary.Tools;
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
using System.Reflection;

namespace MyDictionary.Trenings
{
    /// <summary>
    /// Логика взаимодействия для WindowBreyShtorm_3.xaml
    /// </summary>
    public partial class WindowBreyShtorm_3 : Window
    {
        List<MyWord> myWords;
        string translate = "завтра";
        string wordTrue = "tomorrow";
        List<Button> listButtonsTop = new List<Button>();
        List<Button> listButtonsBottom = new List<Button>();
        double widthButton = 50;
        double heightButton = 50;
        double mardinButtonLeft = 5;
        double mardinButtonTop = 5;
        double mardinButtonRight = 5;
        double mardinButtonBottom = 5;

        double pappingButtonLeft = 15;
        double pappingButtonTop = 5;
        double pappingButtonRight = 5;
        double pappingButtonBottom = 5;
        private int currentLitter = 0;
        private int currentWord = 0;
        private string strFront;
        private string strDef;
        private int countAnswerTrue = 0;
        private int trueAnswer = 0;
        Random random;
        MyWord curentMyWord;
        Brush backgroundButtonNextDefault;
        Brush backgroundButtonNextColor;
        bool isTrening;
        int countFalse;
        int _countTrue = 0;
        public WindowBreyShtorm_3(List<MyWord> words, bool isTrening)
        {
            myWords = words;
            InitializeComponent();
            random = App.random;
            strFront = "Далее ➞";
            strDef = "Не знаю ):";
            backgroundButtonNextDefault = buttonNext.Background;
            backgroundButtonNextColor = Brushes.LightCyan;
            this.isTrening = isTrening;
            countFalse = myWords.Count();
            Init();
        }
        private void Init()
        {
            curentMyWord = myWords[currentWord];
            wordTrue = curentMyWord.Word.ToUpper();
            translate = curentMyWord.MyTranslates.First().Translate;
            string str = Mix(wordTrue);
            AddButtons(str);
            textBlockWord.Text = translate;
            buttonNext.Content = strDef;
            buttonNext.Background = backgroundButtonNextDefault;
            textblockCountword.Text = (currentWord + 1).ToString() + "/" + myWords.Count;
        }
        private void AddButtons(string word)
        {

            foreach (char ch in wordTrue)
            {
                Button bt = CreateButton("templateButtonTop");
                bt.Content = ch;
                listButtonsTop.Add(bt);
                wrapPanelTop.Children.Add(bt);

            }
            foreach (char ch in word)
            {
                Button bt = CreateButton("templateButtonBottom");
                bt.Content = ch;
                bt.Click += button_Click;
                listButtonsBottom.Add(bt);
                wrapPanelBottom.Children.Add(bt);
            }

        }
        private void RemoveButtons()
        {
            listButtonsTop.Clear();
            listButtonsBottom.Clear();
            wrapPanelTop.Children.Clear();
            wrapPanelBottom.Children.Clear();
        }

        private Button CreateButton(string nameControleTemplate)
        {
            Button bt = new Button();
            bt.Width = widthButton;
            bt.Height = heightButton;


            bt.Padding = new Thickness(pappingButtonLeft, pappingButtonTop, pappingButtonRight, pappingButtonBottom);
            bt.Margin = new Thickness(mardinButtonLeft, mardinButtonTop, mardinButtonRight, mardinButtonBottom);
            bt.Template = (ControlTemplate)this.FindResource(nameControleTemplate);
            return bt;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Button buttonSender = sender as Button;
            Button buttontarget = listButtonsTop[currentLitter];
            char contentSender = (char)buttonSender.Content;
            char contentTarget = (char)buttontarget.Content;
            if (contentSender == contentTarget)
            {
                LitterTrue(buttontarget, buttonSender);
            }
            else
            {
                LitterFalse(buttonSender);
            }
        }
        private void LitterTrue(Button buttontarget, Button buttonSender)
        {
            PaintGreen(buttontarget);
            buttonSender.Visibility = Visibility.Hidden;
            countAnswerTrue++;
            if (currentLitter < wordTrue.Length - 1)
            {
                currentLitter++;

                ColorBorderBrush();
            }
            else
            {
                //успешное завершние слова
                if (myWords[currentWord].MyExamples.Count > 0)
                {
                    textblockexample.Text = MyTools.ExampleSpace(myWords[currentWord].MyExamples.First().Example);

                }
                FileInfo fi = FIleTools.SearchFile(myWords[currentWord].SoundName, FIleTools.NameDirectoryAudio);
                if (fi != null)
                {
                    PlaySound(fi);

                }
                buttonNext.Content = strFront;
                buttonNext.Background = backgroundButtonNextColor;
                imageWord.Visibility = Visibility.Visible;
            }

        }
        private void LitterFalse(Button buttonSender)
        {
            countAnswerTrue--;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ColorBorderBrush();
        }
        private void ColorBorderBrush()
        {
            Button button = listButtonsTop[currentLitter];
            ControlTemplate template = button.Template;
            Border border = template.FindName("borderButtonTop", button) as Border;
            border.BorderBrush = new SolidColorBrush(Colors.Blue);
        }
        private string Mix(string word)
        {
            string mix;
            do
            {
                mix = null;
                List<char> lists = word.ToList();
                for (int i = lists.Count - 1; i >= 0; i--)
                {
                    char ch = lists[random.Next(i)];
                    mix += ch;
                    lists.Remove(ch);
                }
            } while (mix == word);
            return mix;
        }
        private void PaintGreen(Button bt)
        {
            bt.Template = (ControlTemplate)this.FindResource("buttonTemplateGreen");
        }

        private void Finish()
        {
            if (countAnswerTrue == wordTrue.Length)
            {
                myWords[currentWord].TrueAnswer++;
                trueAnswer++;
            }

            if (++currentWord >= myWords.Count)
            {
                if (isTrening)
                {
                    VisibilityElements();
                    textBlockWord.Text = "Всего: " + myWords.Count.ToString();
                    FinishAnimation();
                    buttonNext.Content = "Завершить";
                    buttonNext.Click -= buttonNext_Click;
                    buttonNext.Click += Close;
                }
                else
                {
                    WindowsManager.CreateWindowBreyShtorm_4(myWords,false);
                    this.Close();

                }
                return;
            };
            currentLitter = 0;
            RemoveButtons();
            imageWord.Visibility = Visibility.Hidden;
            textblockexample.Text = "";
            countAnswerTrue = 0;
            Init();
        }

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {


            Finish();


        }
        private void PlaySound(FileInfo sound)
        {
            try
            {
                MediaPlayer mediaPlayer = new MediaPlayer();
                mediaPlayer.Open(new Uri(sound.FullName));
                mediaPlayer.Play();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        private void FinishAnimation()
        {
            DoubleAnimation danime = new DoubleAnimation();

            danime.From = 1;
            DispatcherTimer timer = CreateDispetcherTrue();
            timer?.Start();
            danime.To = trueAnswer * recRed.ActualWidth / myWords.Count;
            danime.Duration = TimeSpan.FromMilliseconds(2000);
            recGreen.BeginAnimation(Rectangle.WidthProperty, danime);
            //dispatcherTimer.Start();
        }
        private void VisibilityElements()
        {
            wrapPanelTop.Visibility = Visibility.Hidden;
            wrapPanelBottom.Visibility = Visibility.Hidden;
            canvasAnime.Visibility = Visibility.Visible;
            textblockLeft.Visibility = Visibility.Visible;
            textblockLeft.Foreground = new SolidColorBrush(Colors.Green);
            textblockRight.Foreground = new SolidColorBrush(Colors.Red);
            textblockRight.Visibility = Visibility.Visible;
        }
        private DispatcherTimer CreateDispetcherTrue()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(InitTextBlock);
            int delay = 0;
            if (trueAnswer != 0)
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
            if (++_countTrue > trueAnswer)
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
