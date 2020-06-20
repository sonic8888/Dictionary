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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using XMLRead;

namespace MyDictionary
{
    /// <summary>
    /// Логика взаимодействия для ChoseWords.xaml
    /// </summary>
    public partial class ChoseWords : Window
    {
        private int inputTextLenght = 2;//длина текста TextBox после которой начинается обработка событий ввода
        private string pathDirectoryDictionary = @"./ABBYLingvoDic";// путь к папке с словарями ABBYLingvo
        MyCollection collection;
        MyXmlReader red;
        public ChoseWords()
        {
            InitializeComponent();
            FIleTools.TotalCreateDirectory();

            Object v = this.TryFindResource("collection");//получаем ссылку на ресурс колекции выборки слов из словарей ABBYLingvo
            if (v != null)
            {
                collection = v as MyCollection;
            }
            InitMyXmlReader();
        }




        /// <summary>
        /// обработчик событий ввода текста в строку поиска слов(TextBox findWord)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxInputText(object sender, TextCompositionEventArgs e)
        {


            if (findWord.Text.Length >= inputTextLenght)
            {
                string word = findWord.Text;

                if (red != null)
                {
                    List<WordForList> list = red.FindWordForList(word);
                    collection.Clear();
                    foreach (WordForList item in list)
                    {
                        collection.Add(item);
                    }
                }
            }
        }



        /// <summary>
        /// обработчик TextBlock при шелчке выбора нужного слова
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseDownTextBloc(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            string word = tb.Text.Trim();


            if (red != null)
            {
                List<Card> list = red.FindWords(word);
                if (list != null)
                {
                    MyCardWord myCard = new MyCardWord(list);
                    CardWord cardWord = new CardWord(myCard);
                    cardWord.Topmost = true;
                    cardWord.Show();

                }
                else
                {
                    MessageBox.Show("Слово не найдено!");
                }
            }


        }

        /// <summary>
        /// читает *.xml" файлы
        /// </summary>
        private void InitMyXmlReader()
        {
            DirectoryInfo directory = new DirectoryInfo(pathDirectoryDictionary);
            FileInfo[] fileInfos = directory.GetFiles("*.xml");
            red = new MyXmlReader(fileInfos);
        }




        /// <summary>
        /// обработчик тексвокса FindWord
        /// удаляет текст
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewKeyDownFindWord(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                TextCompositionEventArgs w = new EventArgs() as TextCompositionEventArgs;

                TextBoxInputText(sender, w);


            }
        }

        private void ClickButtonNewWord(object sender, RoutedEventArgs e)
        {
            CardWordEmpty cwe = new CardWordEmpty();
            cwe.Topmost = true;
            cwe.Show();
        }
    }
}
