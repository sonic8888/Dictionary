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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
        Random random;
        int currentRandowValue;
        Brush backgroundDefault;
        Brush backgroundRed;
        Brush backgroundGreen;
        public WindowBreyShtorm_2(List<MyWord> trenings, List<MyWord> resurse)
        {
            this.trenings = trenings;
            this.resurse = resurse;
            InitializeComponent();
            arrButtons = new List<Button>();
            arrButtons.Add(buttonOne);
            arrButtons.Add(buttontwo);
            arrButtons.Add(buttonthree);
            arrButtons.Add(buttonfour);
            arrButtons.Add(buttonfive);
         
            random = new Random();
            InitValue();
            InitResurse();
            backgroundDefault = buttonsix.Background;
            backgroundRed = new SolidColorBrush(Color.FromRgb(243, 182, 219));
            backgroundGreen = new SolidColorBrush(Color.FromRgb(101, 219, 148));
        }

        private void buttonSound_Click(object sender, RoutedEventArgs e)
        {

        }
        private void InitValue()
        {
            textblockWord.Text = trenings[count].Word;
            currentRandowValue = random.Next(countButton);
            arrButtons[currentRandowValue].DataContext = trenings[count];
            arrButtons[currentRandowValue].Content = trenings[count].TranslateStr;
            buttonsix.Content = str;
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
                if (j== currentRandowValue)
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
            Button bt = sender as Button;
            MyWord mw = bt.DataContext as MyWord;
            if (trenings[count].WordId==mw.WordId)
            {
                MethodYes(bt);
            }
            else
            {
                MethodNo(bt);
            }
        }

        private void buttontwo_Click(object sender, RoutedEventArgs e)
        {
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
        }

        private void buttonthree_Click(object sender, RoutedEventArgs e)
        {
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
        }

        private void buttonfour_Click(object sender, RoutedEventArgs e)
        {
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
        }

        private void buttonfive_Click(object sender, RoutedEventArgs e)
        {
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
        }

        private void buttonsix_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Next()
        {
            if (++count< trenings.Count)
            {
                InitValue();
                InitResurse();
            }
            else
            {

            }
        }
        private void MethodYes( Button bt)
        {
            bt.Background = backgroundGreen;
        }
        private void MethodNo(Button bt)
        {
            bt.Background = backgroundRed;
        }
    }
}
