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
        ObservableCollection<MyWord> collection;
        int index;
        int state;
        MyWord myWord;
        int colInd;
        public WindowStateChose(int wordId)
        {
            InitializeComponent();
            
            index = wordId;
            state = 1;
        }

        public ObservableCollection<MyWord> Collection { get => collection; set => collection = value; }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        public void InitRadioButton()
        {
            int state = 0; 

            for (int i = 0; i < collection.Count; i++)
            {
                if (collection[i].WordId==index)
                {
                    colInd = i;
                    state = collection[i].State;
                }
            }
            switch (state)
            {
                case 1:
                    radioBattonGrey.IsChecked = true;
                    break;
                case 2:radioBattonGreen.IsChecked = true;
                    break;
                case 3:radioBattonGold.IsChecked = true;
                    break;
                default:
                    break;
            }
        }

        private void radioBattonGrey_Checked(object sender, RoutedEventArgs e)
        {
            state = 1;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            collection[colInd].State = state;
        }

        private void radioBattonGreen_Checked(object sender, RoutedEventArgs e)
        {
            state = 2;
        }

        private void radioBattonGold_Checked(object sender, RoutedEventArgs e)
        {
            state = 3;
        }
    }
}
