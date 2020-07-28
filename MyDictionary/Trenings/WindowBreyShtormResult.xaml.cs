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

namespace MyDictionary.Trenings
{
    /// <summary>
    /// Логика взаимодействия для WindowBreyShtormResult.xaml
    /// </summary>
    public partial class WindowBreyShtormResult : Window
    {
        List<MyWord> myWords;
        public WindowBreyShtormResult(List<MyWord> words)
        {
            myWords = words;
            InitializeComponent();
            listwiewresult.ItemsSource = myWords;
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
