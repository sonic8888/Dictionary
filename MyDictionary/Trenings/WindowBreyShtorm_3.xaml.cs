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

namespace MyDictionary.Trenings
{
    /// <summary>
    /// Логика взаимодействия для WindowBreyShtorm_3.xaml
    /// </summary>
    public partial class WindowBreyShtorm_3 : Window
    {
        string translate = "завтра";
        string word = "tomorrow";
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
        public WindowBreyShtorm_3()
        {
            InitializeComponent();
            textBlockWord.Text = translate;
            AddButtons();
          


        }
        private void AddButtons()
        {
            foreach (char ch in word)
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
                listButtonsBottom.Add(bt);
                wrapPanelBottom.Children.Add(bt);
            }

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
    }
}
