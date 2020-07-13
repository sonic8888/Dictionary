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
    /// Логика взаимодействия для WindowBreyShtorm.xaml
    /// </summary>
    public partial class WindowBreyShtorm : Window
    {
        ObservableCollection<MyWord> words;
        public WindowBreyShtorm(ObservableCollection<MyWord> coll)
        {
            words = coll;
            InitializeComponent();
            InitDbContext(words[0]);
        }
        private void InitDbContext(MyWord w)
        {
            this.DataContext = w;
        }
    }
}
