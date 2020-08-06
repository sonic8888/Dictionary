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
using XMLRead;

namespace MyDictionary.Sprint
{
    /// <summary>
    /// Логика взаимодействия для WindowSprint.xaml
    /// </summary>
    public partial class WindowSprint : Window
    {
        int countMilisekAnimationPath = 100;
        List<MyWord> myList;
        bool isAnswerTrue = true;
        MediaPlayer mediaPlayer;
        Random random;
        MyWord currentMyWord;
        int currentIndex = 0;
        bool isCurrentBool = true;
        bool isBoolRight = true;
        bool isBoolLeft = false;
        int countTimeWork = 23;
        int countAnswerTrue = 0;
        int myltiRings = 20;
        int currentTotal = 0;
        string pathFile = @"./FileStorage/totalRings.txt";
        DispatcherTimer dispatcherTimer;
        public WindowSprint(List<MyWord> list)
        {
            mediaPlayer = new MediaPlayer();
            myList = list;
            random = App.random;
            InitializeComponent();
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(TimeDeduction);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            Init();
            dispatcherTimer.Start();
        }
        private void Init()
        {
            currentMyWord = myList[currentIndex];
            if (NextRandomBool())//если true то вставляем правильное значение
            {
                isCurrentBool = true;
                InitTextBloks(currentMyWord.Word, MyTools.GetTranslate(currentMyWord));
            }
            else
            {
                isCurrentBool = false;
                MyWord myWordRandom = MyTools.GetRandomMyWord(myList, currentIndex);
                InitTextBloks(currentMyWord.Word, MyTools.GetTranslate(myWordRandom));
            }
        }
        private void InitTextBloks(string word, string translate)
        {
            textblockWord.Text = word;
            textblockTranslate.Text = translate;
        }
        private bool NextRandomBool()
        {
            int r = random.Next(2);
            if (r == 1)
            {
                return true;
            }
            return false;
        }
        private void buttonAnswerFalse_Click(object sender, RoutedEventArgs e)
        {
            if (isCurrentBool == isBoolLeft)
            {
                isAnswerTrue = true;
                PlaySound(App.dataVariable.SoundYes);
                StartAnimationV();
            }
            else
            {
                isAnswerTrue = false;
                PlaySound(App.dataVariable.SoundNo);
                StartAnimationX();
            }

        }

        private void buttonAnswerTrue_Click(object sender, RoutedEventArgs e)
        {
            //FileInfo fi = App.dataVariable.SoundYes;
            //PlaySound(fi);
            //StartAnimationV();
            if (isCurrentBool == isBoolRight)
            {
                isAnswerTrue = true;
                PlaySound(App.dataVariable.SoundYes);
                StartAnimationV();
            }
            else
            {
                isAnswerTrue = false;
                PlaySound(App.dataVariable.SoundNo);
                StartAnimationX();
            }
        }
        //=======================================================================================================//
        /// <summary>
        /// запускает анимацию "Крестика" и анимацию елипса
        /// => CreateСontinuationAnimationX => ColorAnimationElipse => ColorAnimationLineRed
        /// => BackAnimationX => BackColor
        /// </summary>
        private void StartAnimationX()
        {
            elipseBottom.Visibility = Visibility;
            pathX.Visibility = Visibility.Visible;
            PointAnimation myPointAnimation = new PointAnimation();
            myPointAnimation.Duration = TimeSpan.FromMilliseconds(countMilisekAnimationPath);
            myPointAnimation.From = lineRed.EndPoint;
            myPointAnimation.To = new Point(80, 80);
            myPointAnimation.Completed += CreateСontinuationAnimationX;
            lineRed.BeginAnimation(LineGeometry.EndPointProperty, myPointAnimation);
        }
        private void CreateСontinuationAnimationX(object sender, EventArgs e)
        {

            PointAnimation myPointAnimation = new PointAnimation();
            myPointAnimation.Duration = TimeSpan.FromMilliseconds(countMilisekAnimationPath);
            myPointAnimation.From = lineRed2.EndPoint;
            myPointAnimation.To = new Point(50, 80);
            myPointAnimation.Completed += ColorAnimationElipse;
            lineRed2.BeginAnimation(LineGeometry.EndPointProperty, myPointAnimation);

        }
        private void BackAnimationX(object sender, EventArgs e)
        {
            lineRed.BeginAnimation(LineGeometry.EndPointProperty, null);
            lineRed.EndPoint = new Point(50, 50);
            lineRed2.BeginAnimation(LineGeometry.EndPointProperty, null);
            lineRed2.EndPoint = new Point(80, 50);
            pathX.Visibility = Visibility.Hidden;
            elipseBottom.Visibility = Visibility.Hidden;
            BackColor();

        }
        private void ColorAnimationLineRed()
        {
            SolidColorBrush myBrush = new SolidColorBrush();
            ColorAnimation colAnim = new ColorAnimation();
            colAnim.Duration = TimeSpan.FromMilliseconds(400);
            colAnim.From = Colors.Red;
            colAnim.To = Color.FromRgb(196, 198, 249);
            colAnim.Completed += BackAnimationX;
            colAnim.Completed += NextStep;
            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, colAnim);
            pathX.Fill = myBrush;
            pathX.Stroke = myBrush;
        }

        //+===============================================================================================//
        /// <summary>
        /// запускает анимацию "Галочки" и анимацию элипса
        /// => CreateСontinuationAnimationV => ColorAnimationElipse => ColorAnimationLineWhite
        /// => BackAnimationV => BackColor
        /// </summary>
        private void StartAnimationV()
        {
            countAnswerTrue++;
            InitTotal();
            pathV.Visibility = Visibility.Visible;
            elipseBottom.Visibility = Visibility;
            PointAnimation myPointAnimation = new PointAnimation();
            myPointAnimation.Duration = TimeSpan.FromMilliseconds(countMilisekAnimationPath);
            myPointAnimation.From = lineWhite.EndPoint;
            myPointAnimation.To = new Point(65, 80);
            myPointAnimation.Completed += CreateСontinuationAnimationV;
            lineWhite.BeginAnimation(LineGeometry.EndPointProperty, myPointAnimation);
        }
        private void CreateСontinuationAnimationV(object sender, EventArgs e)
        {

            PointAnimation myPointAnimation = new PointAnimation();
            myPointAnimation.Duration = TimeSpan.FromMilliseconds(countMilisekAnimationPath);
            myPointAnimation.From = lineWhite2.EndPoint;
            myPointAnimation.To = new Point(80, 50);
            myPointAnimation.Completed += ColorAnimationElipse;
            lineWhite2.BeginAnimation(LineGeometry.EndPointProperty, myPointAnimation);

        }
        private void BackAnimationV(object sender, EventArgs e)
        {
            lineWhite.BeginAnimation(LineGeometry.EndPointProperty, null);
            lineWhite.EndPoint = new Point(55, 55);
            lineWhite2.BeginAnimation(LineGeometry.EndPointProperty, null);
            lineWhite2.EndPoint = new Point(65, 80);
            pathV.Visibility = Visibility.Hidden;
            elipseBottom.Visibility = Visibility.Hidden;
            BackColor();
        }
        /// <summary>
        /// анимация исчезновения элипса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorAnimationElipse(object sender, EventArgs e)
        {
            SolidColorBrush myBrush = new SolidColorBrush();
            ColorAnimation colAnim = new ColorAnimation();
            colAnim.Duration = TimeSpan.FromMilliseconds(400);
            colAnim.From = Colors.Green;
            colAnim.To = Color.FromRgb(196, 198, 249);
            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, colAnim);
            elipseBottom.Fill = myBrush;
            elipseBottom.Stroke = myBrush;
            if (isAnswerTrue)
            {
                ColorAnimationLineWhite();

            }
            else
            {
                ColorAnimationLineRed();
            }

        }
        /// <summary>
        /// возвращает цвет после анимации элипсу и линиям
        /// </summary>
        private void BackColor()
        {
            SolidColorBrush myBrush = new SolidColorBrush();
            myBrush.Color = Colors.Green;
            elipseBottom.Fill = myBrush;
            elipseBottom.Stroke = myBrush;
            SolidColorBrush brushColor = new SolidColorBrush();
            if (isAnswerTrue)
            {
                brushColor.Color = Colors.White;
                pathV.Fill = brushColor;
                pathV.Stroke = brushColor;

            }
            else
            {
                brushColor.Color = Colors.Red;
                pathX.Fill = brushColor;
                pathX.Stroke = brushColor;
            }

        }
        private void ColorAnimationLineWhite()
        {
            SolidColorBrush myBrush = new SolidColorBrush();
            ColorAnimation colAnim = new ColorAnimation();
            colAnim.Duration = TimeSpan.FromMilliseconds(400);
            colAnim.From = Colors.White;
            colAnim.To = Color.FromRgb(196, 198, 249);
            colAnim.Completed += BackAnimationV;
            colAnim.Completed += NextStep;
            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, colAnim);
            pathV.Fill = myBrush;
            pathV.Stroke = myBrush;
        }
        //============================================================================================//


        private void PlaySound(FileInfo sound)
        {
            mediaPlayer.Stop();
            mediaPlayer.Open(new Uri(sound.FullName));
            mediaPlayer.Play();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Close();
        }
        private void NextStep(object sender, EventArgs e)
        {

            if (++currentIndex < myList.Count - 1)
            {
                Init();

            }
            else
            {
                MessageBox.Show("Finish");
                //finish
            }
        }
        private void TimeDeduction(object sender, EventArgs e)
        {
            if (countTimeWork >= 0)
            {
                textblockTime.Text = countTimeWork.ToString();
                --countTimeWork;
            }
            else
            {
                dispatcherTimer.Stop();
                Finish();
                //MessageBox.Show("Finish");
                //finish
            }
        }
        private void Finish()
        {
            buttonAnswerFalse.IsEnabled = false;
            buttonAnswerTrue.IsEnabled = false;
            SaveResult();
        }
        private void InitTotal()
        {
            currentTotal = countAnswerTrue * myltiRings;
            textblockCountRings.Text = currentTotal.ToString();
        }
        private void SaveResult()
        {
            if (File.Exists(pathFile))
            {
                string str = FIleTools.ReadPath(pathFile);

                int result = 0;
                int.TryParse(str, out result);
                if (currentTotal > result)
                {
                    result = currentTotal;
                    FIleTools.WritePath(pathFile, result.ToString(), false);
                }
                textBlockTop.Text = "Лучший результат: " + result + " очков";


            }
            else
            {
                FIleTools.WritePath(pathFile, currentTotal.ToString(), false);
                textBlockTop.Text = "Лучший результат: " + currentTotal + " очков";

            }
        }
    }
}
