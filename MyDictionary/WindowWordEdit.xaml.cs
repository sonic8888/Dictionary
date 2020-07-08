using MyDictionary.EF;
using MyDictionary.Tools;
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
    /// Логика взаимодействия для WindowWordEdit.xaml
    /// </summary>
    public partial class WindowWordEdit : Window
    {
        MyWord MyWord;
        private char[] splitTranslate;
        private char[] splitExample;
        WordSample wordSample;
        private string messageNotAudio = "Аудиофайл не указан.\nПродожить без него?";
        private string messageWarning = "Внимание!";
        public WindowWordEdit(MyWord word)
        {
            MyWord = word;
            InitializeComponent();
            this.DataContext = MyWord;
            initElements();
            splitTranslate = new char[] { '\n', '.', ',', '!', ' ', ';', ':', '\r', '\t', '\v', '?', '/' };
            splitExample = new char[] { '\n', '\r', '\t', '\v' };

        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            BdTools.DeleteWord(MyWord.WordId);
            initNewWord();
            if (wordSample!=null)
            {
                BdTools.AddNewWords(wordSample);
            }
            this.DialogResult = true;

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
        private void initNewWord()
        {
            string word = textboxWord.Text.Trim();
            if (word == "")
            {
                MessageBox.Show("Слово не указано");
                return;
            }
            string transcription = textboxTranscription.Text.Trim();
            string partOfSpeach = textboxPartOfSpeach.Text.Trim();
            int result = 0;
            if (!int.TryParse(textboxStatus.Text.Trim(), out result))
            {
                result = 1;
            }
            string audio = textboxAudio.Text.Trim();
            DateTime insert = MyWord.DataTimeInsert;
            DateTime lastCall = DateTime.Now;
            IEnumerable<string> translate = GetEnumerable(textboxTranslate.Text, splitTranslate);
            IEnumerable<string> example = GetEnumerable(textboxExample.Text, splitExample);
            if (audio==null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(messageNotAudio, messageWarning, MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.No)
                {
                    return; 
                }
            }
            wordSample = new WordSample(word, partOfSpeach, transcription, audio, new ObservableCollection<string>(translate), new ObservableCollection<string>(example), insert, lastCall, result);
        }
        /// <summary>
        /// обработчик вкладки меню "Копировать аудиофайл"
        /// открывает проводник и  копирует файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickMenuItemOpen(object sender, RoutedEventArgs e)
        {
            DefaultDialogService dds = new DefaultDialogService();
            dds.OpenFileDialog();
            string path = dds.FilePath;
            FileInfo fileInfo = new FileInfo(path);

            FIleTools.CopyTo(fileInfo, FIleTools.NameDirectoryAudio);
            textboxAudio.Text = fileInfo.Name.ToLower();
       
        }

        private void PlaySound(FileInfo sound)
        {
            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri(sound.FullName));
            mediaPlayer.Play();
        }
        private FileInfo SearchFile(string name, string nameDirectory)
        {
            FileInfo file = null;
            try
            {
                file = FIleTools.SearchFile(name, nameDirectory);

            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            return file;
        }
 
  

        private void ClickButtonPlayAudio(object sender, RoutedEventArgs e)
        {
            FileInfo file = SearchFile(textboxAudio.Text, FIleTools.NameDirectoryAudio);


            if (file != null)
            {
                PlaySound(file);
                return;
            }

        }

    }
}
