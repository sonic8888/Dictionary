using MyDictionary.EF;
using System;
using System.Collections.Generic;
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

namespace MyDictionary.Sprint
{
    /// <summary>
    /// Логика взаимодействия для WindowSprint.xaml
    /// </summary>
    public partial class WindowSprint : Window
    {
        int countMilisekAnimationPath = 100;
        List<MyWord> myList;
        public WindowSprint(List<MyWord> list)
        {
            myList = list;
            InitializeComponent();
        }

        private void buttonAnswerFalse_Click(object sender, RoutedEventArgs e)
        {
            BackAnimationV();
            BackColor();
        }

        private void buttonAnswerTrue_Click(object sender, RoutedEventArgs e)
        {
            StartAnimationV();
        }
        private void StartAnimationX()
        {
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
            lineRed2.BeginAnimation(LineGeometry.EndPointProperty, myPointAnimation);

        }
        private void BackAnimationX()
        {
            lineRed.BeginAnimation(LineGeometry.EndPointProperty, null);
            lineRed.EndPoint = new Point(50, 50);
            lineRed2.BeginAnimation(LineGeometry.EndPointProperty, null);
            lineRed2.EndPoint = new Point(80, 50);
            pathX.Visibility = Visibility.Hidden;

        }
        private void StartAnimationV()
        {
            pathV.Visibility = Visibility.Visible;
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
            myPointAnimation.Completed += TestColorAnimationElipse;
            lineWhite2.BeginAnimation(LineGeometry.EndPointProperty, myPointAnimation);

        }
        private void BackAnimationV()
        {
            lineWhite.BeginAnimation(LineGeometry.EndPointProperty, null);
            lineWhite.EndPoint = new Point(55, 55);
            lineWhite2.BeginAnimation(LineGeometry.EndPointProperty, null);
            lineWhite2.EndPoint = new Point(65, 80);
            pathV.Visibility = Visibility.Hidden;

        }
        private void TestColorAnimationElipse(object sender, EventArgs e)
        {
            SolidColorBrush myBrush = new SolidColorBrush();
            ColorAnimation colAnim= new ColorAnimation();
            colAnim.Duration = TimeSpan.FromMilliseconds(400);
            colAnim.From = Colors.Green;
            colAnim.To = Color.FromRgb (196,198,249);
            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, colAnim);
            elipseBottom.Fill = myBrush;
            elipseBottom.Stroke = myBrush;
            TestColorAnimationLineWhite();

        }
        private void BackColor()
        {
            SolidColorBrush myBrush = new SolidColorBrush();
            myBrush.Color = Colors.Green;
            elipseBottom.Fill = myBrush;
            elipseBottom.Stroke = myBrush;
            SolidColorBrush whiteBrash = new SolidColorBrush();
            whiteBrash.Color = Colors.White;
            pathV.Fill = whiteBrash;
            pathV.Stroke = whiteBrash;
        }
        private void TestColorAnimationLineWhite()
        {
            SolidColorBrush myBrush = new SolidColorBrush();
            ColorAnimation colAnim = new ColorAnimation();
            colAnim.Duration = TimeSpan.FromMilliseconds(400);
            colAnim.From = Colors.White;
            colAnim.To = Color.FromRgb(196, 198, 249);
            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, colAnim);
            pathV.Fill = myBrush;
            pathV.Stroke = myBrush;
        }

    }
}
