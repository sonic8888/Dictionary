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

namespace MyDictionary.Trenings
{
    /// <summary>
    /// Логика взаимодействия для WindowBreyShtorm_3.xaml
    /// </summary>
    public partial class WindowBreyShtorm_3 : Window
    {
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
        Random random;
        public WindowBreyShtorm_3()
        {
            InitializeComponent();
            wordTrue = wordTrue.ToUpper();
            translate = translate.ToUpper();
            random = App.random;
            textBlockWord.Text = translate;
            string str = Mix(wordTrue);
            AddButtons(str);


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
                PaintGreen(buttontarget);
                buttonSender.Visibility = Visibility.Hidden;
            }
            else
            {
                ControlTemplate template = buttonSender.Template;
                Border border = (Border)template.FindName("borderButtonBottom", buttonSender);
                Animation(border);
            }
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
            List<char> lists = word.ToList();
            string mix = "";
            for (int i = lists.Count - 1; i >= 0; i--)
            {
                char ch = lists[random.Next(i)];
                mix += ch;
                lists.Remove(ch);
            }
            return mix;
        }
        private void PaintGreen(Button bt)
        {
            bt.Template = (ControlTemplate)this.FindResource("buttonTemplateGreen");
        }
        private void Animation(Border lab)
        {
            ColorAnimation ca = new ColorAnimation();
            SolidColorBrush background = (SolidColorBrush)lab.Background;
            ca.From = background.Color;
            ca.To = Colors.Red;
            ca.Duration = TimeSpan.FromSeconds(1);
            Storyboard.SetTargetName(lab, "borderButtonBottom");
            Storyboard.SetTargetProperty(ca, new PropertyPath(SolidColorBrush.ColorProperty));
            Storyboard storyboard = new Storyboard();
            

        }

    }
}
