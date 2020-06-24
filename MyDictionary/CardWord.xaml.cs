using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using System.IO;



namespace MyDictionary
{
    /// <summary>
    /// Логика взаимодействия для CardWord.xaml
    /// окно для добавления нового слова с использованием 
    /// имеющегося ABBYLingvo словаря
    /// </summary>
    public partial class CardWord : Window
    {
        public MyCardWord Mycardword { get; private set; }
        private MyCollectionTranslateClass translates;
        public CardWord(MyCardWord myCard)
        {
            InitializeComponent();
            Mycardword = myCard;


            Object v = this.TryFindResource("collectioTranslate");//получаем ссылку на ресурс колекции выборки слов из словарей ABBYLingvo
            if (v != null)
            {
                translates = v as MyCollectionTranslateClass;
            }

            translates.Clear();
            foreach (TranslateClass item in myCard.TranslateList)
            {
                translates.Add(item);
            }


            this.DataContext = Mycardword;

        }



        /// <summary>
        /// Удаляет выделеные слова перевода из ListView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView lw = (ListView)sender;
            TranslateClass select = lw.SelectedItem as TranslateClass;
            translates.Remove(select);
        }
        /// <summary>
        /// обработчик кнопки "Добавить перевод"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClickAddTranslate(object sender, RoutedEventArgs e)
        {
            if (textBoxAddTranslate.Visibility == Visibility.Hidden)
            {
                textBoxAddTranslate.Visibility = Visibility.Visible;
                buttonAddTranslate.Content = "Скрыть";
                textBoxAddTranslate.Focus();
                //buttonSave.IsEnabled = false;

            }
            else
            {
                buttonAddTranslate.Content = "Добавить\nперевод";
                textBoxAddTranslate.Visibility = Visibility.Hidden;
                textBoxAddTranslate.Text = "";
                radiobuttonDrug.IsChecked = true;
                GroupBoxPartofSpeach.Visibility = Visibility.Hidden;
                buttonSave.IsEnabled = false; ;
            }
        }
        /// <summary>
        /// обработчик текстового поля добавить перевод
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyDownTextBoxddTranslate(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GroupBoxPartofSpeach.Visibility = Visibility.Visible;
                buttonSave.IsEnabled = true;
            }
        }


        /// <summary>
        /// обработчик кнопки "Сохранить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClickSave(object sender, RoutedEventArgs e)
        {
            string trans = textBoxAddTranslate.Text;
            if (trans == "" && GroupBoxPartofSpeach.Visibility == Visibility.Hidden)
            {
                return;
            }
            StackPanel panel = GroupBoxPartofSpeach.Content as StackPanel;
            UIElementCollection children = panel.Children;
            foreach (UIElement item in children)
            {
                RadioButton rb = item as RadioButton;
                if (rb != null && rb.IsChecked == true)
                {

                    TranslateClass tr = new TranslateClass(trans, rb.Content.ToString());
                    translates.Add(tr);
                    Mycardword.TranslateList = translates.GetCollection();
                    break;
                }
            }

            textBoxAddTranslate.Text = "";
            textBoxAddTranslate.Focus();
            radiobuttonDrug.IsChecked = true;
            GroupBoxPartofSpeach.Visibility = Visibility.Hidden;
            buttonSave.IsEnabled = false;
        }

        /// <summary>
        /// Обработчик кнопки назад
        /// сохраняем данные в Mycardword
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClickBack(object sender, RoutedEventArgs e)
        {

            //Mycardword.TranslateList = translates.ToList<TranslateClass>();
            //Process.Start(@"D:\Library\Music\words"); ;
            DefaultDialogService df = new DefaultDialogService();
            if (df.OpenFileDialog())
            {

                df.ShowMessage(df.FilePath);
            };
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
        /// сохраняет новое имя файла в "Mycardword"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickMenuItemAdd(object sender, RoutedEventArgs e)
        {
            DefaultDialogService dds = new DefaultDialogService();
            dds.OpenFileDialog();
            string fileName = dds.FilePath;
            string extension = FIleTools.GetExtensionFile(fileName);
            FileInfo filecopy = FIleTools.CopyTo(fileName, FIleTools.NameDirectoryAudio, Mycardword.Word, extension);
            if (filecopy != null)
            {
                Mycardword.SoundName = filecopy.Name;
                MessageBox.Show("Файл скопирован: \n" + filecopy.FullName);
            }
            else
            {
                MessageBox.Show("файл скопировать не удалось");
            }
        }
        /// <summary>
        /// обработчик события кнопки buttonDownLoadAudio
        /// копирует аудиофайл из внешней папки в папку приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickButtonDownLoadAudio(object sender, RoutedEventArgs e)
        {
            string word = textboxNewWord.Text;
            if (word == "")
            {
                MessageBox.Show("Укажите слово!");
                return;
            }
            if (FIleTools.SearchFile(Mycardword.SoundName, FIleTools.NameDirectoryAudio) != null)
            {
                MessageBox.Show("Файл: " + Mycardword.SoundName + "\n" +
                    " уже скопирован.");
                return;
            }
            string pathDirectSearch = FIleTools.ReadPath(FIleTools.NameFilePathes);
            if (pathDirectSearch=="")
            {
                MessageBox.Show("не указана папка для поиска файлов");
                return;
            }
            FileInfo fileInfosound = FIleTools.SearchFile(Mycardword.SoundName, pathDirectSearch);
            if (fileInfosound == null)
            {
                MessageBox.Show("Файл:" + Mycardword.SoundName + "\n" +
                    "в папке: " + pathDirectSearch + " не найден." + "\n" +
                    "укажите новый файл.");
                return;
            }
            FileInfo filecopy = FIleTools.CopyTo(fileInfosound, FIleTools.NameDirectoryAudio);
            if (filecopy != null)
            {
                Mycardword.SoundName = filecopy.Name;
                MessageBox.Show("Файл:" + filecopy.Name + "\n" +
                    " успешно скопирован в папку: " + "\n" +
                   FIleTools.NameDirectoryAudio);
            }
            else
            {
                MessageBox.Show("файл скопировать не удалось");
            }
        }
        /// <summary>
        /// обработчик buttonPlaySound
        /// воспроизводит аудиофайл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickButtonPlaySound(object sender, RoutedEventArgs e)
        {

            FileInfo fileInfo = FIleTools.SearchFile(Mycardword.SoundName, FIleTools.NameDirectoryAudio);
            if (fileInfo == null)
            {
                MessageBox.Show("Аудио файл не найден");
                return;
            }
            FileInfo sound = FIleTools.SearchFile(Mycardword.SoundName, FIleTools.NameDirectoryAudio);
            if (sound != null)
            {
                MediaPlayer mediaPlayer = new MediaPlayer();
                mediaPlayer.Open(new Uri(sound.FullName));
                mediaPlayer.Play();
            }

        }
    }
}
