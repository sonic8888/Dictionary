using MyDictionary.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для WindowStateChose.xaml
    /// </summary>
    public partial class WindowStateChose : Window
    {
        TotalDictionary totalDictionary;
        int state;
        public WindowStateChose(TotalDictionary win,int state)
        {
            InitializeComponent();
            this.state = state;
            totalDictionary = win;
            switch (state)
            {
                case 1:
                    radioBattonGrey.IsChecked = true;
                    break;
                case 2:
                    radioBattonGreen.IsChecked = true;
                    break;
                case 3:
                    radioBattonGold.IsChecked = true;
                    break;
                default:
                    break;
            }
        }



        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
  

        private void radioBattonGrey_Checked(object sender, RoutedEventArgs e)
        {
            totalDictionary.StateWord = 1;
        }

    

        private void radioBattonGreen_Checked(object sender, RoutedEventArgs e)
        {
            totalDictionary.StateWord = 2;
        }

        private void radioBattonGold_Checked(object sender, RoutedEventArgs e)
        {
            totalDictionary.StateWord = 3;
        }
    }
}
