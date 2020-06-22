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
using XMLRead;
using System.IO;

namespace MyDictionary
{
    /// <summary>
    /// Логика взаимодействия для CardWordEmpty.xaml
    /// окно для добавления нового слова без использования
    /// ABBYLingvo словаря
    /// </summary>
    public partial class CardWordEmpty : Window
    {
        public MyCardWord Mycardword { get; private set; }
  
        /// <summary>
        /// "true" если нужный аудиофайл скопирован в папку с аудиофайлами приложения
        /// и имя файла записано в Mycardword.soundName
        /// </summary>
        private bool IsCopySoundFile = false;
        /// <summary>
        /// имя окна которое нужно закрыть
        /// </summary>
        private string windowName = "windowNewWord";
        public CardWordEmpty()
        {
            InitializeComponent();
            Mycardword = new MyCardWord();
        }

        /// <summary>
        /// обработчик вкладки меню "Открыть"
        /// открывает проводник и сохраняет в текстовый файл путь к внешней папке с аудиофайлами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickMenuItemOpen(object sender, RoutedEventArgs e)
        {
            //FIleTools.TotalCreateDirectory();
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
            //FIleTools.TotalCreateDirectory();
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
            if (FIleTools.SerachFile(Mycardword.SoundName, FIleTools.NameDirectoryAudio) != null)
            {
                MessageBox.Show("Файл: " + Mycardword.SoundName + "\n" +
                    " уже скопирован.");
                return;
            }
            string pathDirectSearch = FIleTools.ReadPath(FIleTools.NameFilePathes);
            if (pathDirectSearch == "")
            {
                MessageBox.Show("не указана папка для поиска файлов");
                return;
            }
            FileInfo fileInfosound = FIleTools.SerachFile(Mycardword.SoundName, pathDirectSearch);
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
                IsCopySoundFile = true;
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

            FileInfo fileInfo = FIleTools.SerachFile(Mycardword.SoundName, FIleTools.NameDirectoryAudio);
            if (fileInfo == null)
            {
                MessageBox.Show("Аудио файл не найден");
                return;
            }
            FileInfo sound = FIleTools.SerachFile(Mycardword.SoundName, FIleTools.NameDirectoryAudio);
            if (sound != null)
            {
                MediaPlayer mediaPlayer = new MediaPlayer();
                mediaPlayer.Open(new Uri(sound.FullName));
                mediaPlayer.Play();
            }

        }
        /// <summary>
        /// обработчик кнопки "Сохранить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClickSave(object sender, RoutedEventArgs e)
        {
            string textTranslate = textboxTranslate.Text;//строка из переводов
            string textWord = textboxNewWord.Text;//строка слова

            if (textTranslate == "" && textWord == "")
            {
                return;
            }
            Mycardword = new MyCardWord();
            List<string> listWordsTranslation = new List<string>();//спиок переводов
            if (textTranslate.Contains('\n'))
            {
                //string[] arr = text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                string[] arr = textTranslate.Split(new char[] { '\n', '\r', ' ' });
                var listTranslation = from t in arr
                                      where t.Length > 1
                                      select t;
                listWordsTranslation.AddRange(listTranslation);
            }
            else
            {
                listWordsTranslation.Add(textTranslate);
            }

            string transcription = textboxTranscription.Text.Trim(new char[] { '[', ']', ' ' });//строка транскрипции
            string examples = textboxExample.Text;
            List<string> listExamles = new List<string>();//спиок примеров
            if (examples != "")
            {
                if (examples.Contains('\n') && examples.Contains('\r'))
                {
                    string[] arr = examples.Split(new char[] { '\n', '\r' });
                    var lexamp = from t in arr
                                 where t.Length > 1
                                 select t;
                    listExamles.AddRange(lexamp);
                }
                else
                {
                    listExamles.Add(examples);
                }
            }
            //получаем часть речи
            string patrOfSpeach = "";
            StackPanel panel = GroupBoxPartofSpeach.Content as StackPanel;
            UIElementCollection children = panel.Children;
            foreach (UIElement item in children)
            {
                RadioButton rb = item as RadioButton;
                if (rb != null && rb.IsChecked == true)
                {
                    patrOfSpeach = rb.Content.ToString();
                    break;
                }
            }
            //сохраняем все в Mycardword
            Mycardword.Word = textWord;
            Mycardword.Transcription = transcription;
            Mycardword.SoundName = textWord + ".wav";
            foreach (string item in listWordsTranslation)
            {
                Mycardword.TranslateList.Add(new TranslateClass(item, patrOfSpeach));
            }
            foreach (string item in listExamles)
            {
                Mycardword.ExampleList.Add(new ExampleClass(item, patrOfSpeach));
            }
            //включаем видимость ToolBar чтобы загрузить аудио
            toolBaraudio.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// обработчик кнопки "Назад"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClickBack(object sender, RoutedEventArgs e)
        {
            if (!IsCopySoundFile)
            {
                // выводим предупреждение
                string message = "Нет звукового файла!\nПродолжить без него?";
                string caption = "Предупреждение";
                MessageBoxButton buttons = MessageBoxButton.OKCancel;
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBoxResult defaultResult = MessageBoxResult.OK;
                MessageBoxResult result = MessageBox.Show(message, caption, buttons, icon, defaultResult);
                if (result == MessageBoxResult.OK)
                {
                    this.Close();
                    foreach (Window item in Application.Current.Windows)
                    {
                        if (item.Name==windowName)
                        {
                            item.Close();
                        }
                    }
                    
                    // добавляем Mycardword в базу данных
                }
                else
                {
                    return;
                }

            }
            else
            {
                this.Close();
                foreach (Window item in Application.Current.Windows)
                {
                    if (item.Name == windowName)
                    {
                        item.Close();
                    }
                }
                // добавляем Mycardword в базу данных
            }


          
        }
    }
}
