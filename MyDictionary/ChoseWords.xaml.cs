using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<WordSample> collection = new ObservableCollection<WordSample>();
        MyXmlReader red;
        WordSample _wordsSample;
        private char[] splitTranslate;
        private char[] splitExample;
        private string messageNotAudio = "Аудиофайл не указан.\nПродожить без него?";
        private string messageWarning = "Внимание!";
        public ChoseWords()
        {
            InitializeComponent();
            FIleTools.TotalCreateDirectory();
            wordListView.ItemsSource = collection;

            _wordsSample = new WordSample();
            InitMyXmlReader();
            splitTranslate = new char[] { '\n', '.', ',', '!', ' ', ';', ':', '\r', '\t', '\v', '?', '/' };
            splitExample = new char[] { '\n', '\r', '\t', '\v' };
        }




        /// <summary>
        /// обработчик событий ввода текста в строку поиска слов(TextBox findWord)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxInputText(object sender, TextCompositionEventArgs e)
        {


            collection.Clear();
            if (findWord.Text.Length >= inputTextLenght)
            {
                string word = findWord.Text;

                if (red != null)
                {

                    foreach (WordSample item in red.FindWordsSample(word))
                    {
                        collection.Add(item);
                    }


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

            collection.Clear();
            if (findWord.Text.Length >= inputTextLenght)
            {
                string word = findWord.Text;

                if (red != null)
                {

                    foreach (WordSample item in red.FindWordsSample(word))
                    {
                        collection.Add(item);
                    }


                }
            }
        }

        private void ClickButtonNewWord(object sender, RoutedEventArgs e)
        {
            CardWordEmpty cwe = new CardWordEmpty();
            cwe.Topmost = true;
            cwe.Show();
        }



        private void KeyDownTextBoxFindWord(object sender, KeyEventArgs e)
        {
            //}
            collection.Clear();
            if (findWord.Text.Length >= inputTextLenght)
            {
                string word = findWord.Text;

                if (red != null)
                {

                    foreach (WordSample item in red.FindWordsSample(word))
                    {
                        collection.Add(item);
                    }


                }
            }

        }



        private void PreviewTextInputt(object sender, TextCompositionEventArgs e)
        {
            collection.Clear();
            if (findWord.Text.Length >= inputTextLenght)
            {
                string word = findWord.Text;

                if (red != null)
                {

                    foreach (WordSample item in red.FindWordsSample(word))
                    {
                        collection.Add(item);
                    }


                }
            }
        }

        private void KeyUpTextBoxFind(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                collection.Clear();
                if (findWord.Text.Length >= inputTextLenght)
                {
                    string word = findWord.Text;

                    if (red != null)
                    {

                        foreach (WordSample item in red.FindWordsSample(word))
                        {
                            collection.Add(item);
                        }


                    }
                }
            }
        }

        private void SelectionChanget(object sender, SelectionChangedEventArgs e)
        {

            ListBox lb = sender as ListBox;
            int ind = lb.SelectedIndex;
            WordSample wordSample = lb.Items[ind] as WordSample;
            inerGrid.DataContext = wordSample;
            _wordsSample = inerGrid.DataContext as WordSample;

        }
        /// <summary>
        /// обработчик вкладки меню "Открыть"
        /// открывает проводник и сохраняет в текстовый файл путь к внешней папке с аудиофайлами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickMenuItemOpen(object sender, RoutedEventArgs e)
        {
            DefaultDialogService dds = new DefaultDialogService();
            dds.OpenFileDialog();
            string pathDirectory = dds.GetDirectory(dds.FilePath);
            FIleTools.WritePath(FIleTools.NameFilePathes, pathDirectory, false);
        }
        /// <summary>
        /// обработчик вкладки меню "Добавить"
        /// открывает окно проводника
        /// копирует и переименовывает аудиофайл в папку приложения
        /// сохраняет новое имя файла в "WordSample"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickMenuItemAdd(object sender, RoutedEventArgs e)
        {
            DefaultDialogService dds = new DefaultDialogService();
            dds.OpenFileDialog();
            string fileName = dds.FilePath;
            FileInfo filesound = new FileInfo(fileName);
            FileInfo filecopy = FIleTools.CopyTo(filesound, FIleTools.NameDirectoryAudio);
            if (filecopy != null)
            {
                _wordsSample.SoundName = filecopy.Name;

            }
            else
            {
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void ClickButtonPlayAudio(object sender, RoutedEventArgs e)
        {
            FileInfo file;
            try
            {
                file = FIleTools.SearchFile(_wordsSample.SoundName, FIleTools.NameDirectoryAudio);

            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            if (file != null)
            {
                PlaySound(file);
                return;
            }
            string path;
            try
            {
                path = FIleTools.ReadPath(FIleTools.NameFilePathes);

            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            FileInfo fileInfo;
            try
            {
                fileInfo = FIleTools.SearchFile(_wordsSample.SoundName, path);
            }
            catch (IOException ex)
            {

                MessageBox.Show(ex.Message);
                return;
            }


            if (fileInfo == null)
            {
                System.Media.SystemSounds.Beep.Play();
                MessageBox.Show("Аудиофайл не найден.\nУкажите файл в ручную.");
            }
            else
            {
                FileInfo filcopy = FIleTools.CopyTo(fileInfo, FIleTools.NameDirectoryAudio);
                _wordsSample.SoundName = filcopy.Name;
                PlaySound(filcopy);
            }
        }


        private void PlaySound(FileInfo sound)
        {
            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri(sound.FullName));
            mediaPlayer.Play();
        }

        private void ClickButtonCreateNewWord(object sender, RoutedEventArgs e)
        {
            if (_wordsSample.SoundName == null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(messageNotAudio, messageWarning, MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)// ответ Да продолжаем без Аудиофайла
                {

                }
                return;

            }

        }
        private void GreateNewWord()
        {
            string word = textBoxWord.Text;
            if (word == "")
            {
                MessageBox.Show("Слово не указано.");
                return;
            }
            ItemCollection trItems = listBoxETranslate.Items;
            if (trItems.Count < 1)
            {
                MessageBox.Show("Перевод не указан.");
                return;
            }

        }

        private void ClickButtonSave(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// Создает диалоговое окно добавления Переводов и 
        /// Примеров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickButtonAddWord(object sender, RoutedEventArgs e)
        {
            WindowAddTranslation windowAdd = new WindowAddTranslation();
            if (windowAdd.ShowDialog() == true)
            {
                string translate = windowAdd.textBoxTranslate.Text;
                string example = windowAdd.textBoxExample.Text;
                IEnumerable<string> transl = GetEnumerable(translate, splitTranslate);
                IEnumerable<string> exampl = GetEnumerable(example, splitExample);

                if (transl != null)
                {
                    foreach (string item in transl)
                    {
                        _wordsSample.Translate.Add(item);
                    }
                }
                if (exampl != null)
                {
                    foreach (string item in exampl)
                    {
                        _wordsSample.Example.Add(item);
                    }
                }


            }
        }
        /// <summary>
        /// возвращает последовательность переводов и примеров
        /// </summary>
        /// <param name="text"></param>
        /// <param name="arr"></param>
        /// <returns></returns>
        private IEnumerable<string> GetEnumerable(string text, char[] arr)
        {
            IEnumerable<string> collect = text.Split(arr).Where(n => n.Length > 1);
            return collect;
        }
    }
}
