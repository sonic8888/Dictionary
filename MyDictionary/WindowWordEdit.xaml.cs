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
    /// Логика взаимодействия для WindowWordEdit.xaml
    /// </summary>
    public partial class WindowWordEdit : Window
    {
        MyWord MyWord;
        public WindowWordEdit(MyWord word)
        {
            MyWord = word;
            InitializeComponent();
            this.DataContext = MyWord;
            initElements();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            //this.DialogResult = true;
            initMyWord();
        }
        private void initElements()
        {
            string str = "";
            foreach (MyTranslate item in MyWord.MyTranslates)
            {
                str += item.Translate + '\n';
            }
            textboxTranslate.Text = str;
            str = "";
            foreach (MyExample item in MyWord.MyExamples)
            {
                str += item.Example + '\n';
            }
            textboxExample.Text = str;
        }
        private void initMyWord()
        {
            string trans = textboxTranslate.Text;
            string[] vs = trans.Split(new char[] { '\n', ' ', ',' });
            IEnumerable<string> enumerable = vs.Where(n => (n.Length > 1));
         
       
        }
    }
}
