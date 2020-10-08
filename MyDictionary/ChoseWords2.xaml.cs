using MyDictionary.Tools;
using MyDictionary.XMLRead;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace MyDictionary
{
    public class Handlerword
    {
        public string Word { get; set; }
        public Handlerword()
        {

        }
        public Handlerword(string word)
        {
            Word = word;
        }

    }
    public partial class ChoseWords2 : Window
    {
        string path = @".\Dictionary\dict.xdxf";
        FileInfo audioFile = null;
        public ChoseWords2()
        {
            InitializeComponent();
            textboxFindWord.Focus();
        }
        /// <summary>
        /// читает файл и возвращает текст
        /// (перевод слова и его примеры)
        /// path-путь к файлу
        /// word-слово которое ищем
        /// </summary>
        /// <param name="path">путь к файлу</param>
        /// <param name="word">искомое слово</param>
        /// <returns>текст</returns>
        private static string ReadFileAndGetValue(string path, string word)
        {
            XDocument xdoc = XDocument.Load(path);

            string str = "";
            foreach (XElement item in xdoc.Element("xdxf").Elements("ar"))
            {
                XElement element = item.Element("k");
                if (element.Value == word)
                {

                    element.Remove();
                    str = item.Value;

                    break;
                }

            }
            return str;

        }
        private string Replace(string line)
        {
            string replaceStr = "";
            if (Regex.IsMatch(line, ">"))
            {
                replaceStr = Regex.Replace(line, ">", ")");
            }
            else
            {
                replaceStr = line;
            }
            return replaceStr;
        }
        private string InsertTire(string sourse)
        {
            if (!Regex.IsMatch(sourse, @" *[0-9]\)") && Regex.IsMatch(sourse, " *[А-я]"))
            {
                Match first = Regex.Match(sourse, "[А-я]");
                char tire = '\u2014';//тире
                string tireSpace = tire + " ";
                return sourse.Insert(first.Index, tireSpace);
            }
            else
            {
                return sourse;
            }


        }
        private IEnumerable<string> GetWordValue(string path, string word)
        {
            List<string> lists = ReadFileAndGetValue(path, word).Split(new char[] { '\n' }).ToList();
            return lists.Select(n => Replace(n)).Select(n => InsertTire(n));
        }

        private void combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Handlerword hw = (Handlerword)comboBox.SelectedItem;
            if (hw == null)
            {
                return;
            }
            textboxFindWord.Text = hw.Word;
            combobox.IsDropDownOpen = false;
            textboxFindWord.Focus();
            textboxFindWord.SelectionStart = textboxFindWord.Text.Length;
            paragraf.Inlines.Clear();
            Procesing(path, hw.Word);
        }

        private void textboxFindWord_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = textboxFindWord.Text;
            if (text.Length > 3)
            {
                combobox.ItemsSource = null;
                IEnumerable<Handlerword> enu = ReadFileAndGetWords(path, text).Select(n => new Handlerword() { Word = n });
                if (enu.Count() > 0)
                {
                    //combobox.ItemsSource = ReadFileAndGetWords(path, text).Select(n => new Handlerword() { Word = n });
                    combobox.ItemsSource = enu;
                    combobox.IsDropDownOpen = true;

                }

            }
            else
            {
                combobox.ItemsSource = null;
                combobox.IsDropDownOpen = false;
                paragraf.Inlines.Clear();
            }
        }
        /// <summary>
        /// возвращает посл-сть слов содержащих вхождение partWord
        /// path - путь к файлу
        /// partWord - часть слова
        /// </summary>
        /// <param name="path">путь к файлу</param>
        /// <param name="partWord">часть слова</param>
        /// <returns>Enumerable<string></returns>
        private static IEnumerable<string> ReadFileAndGetWords(string path, string partWord)
        {
            XDocument xdoc = XDocument.Load(path);
            return xdoc.Element("xdxf").Elements("ar").Select(n => n.Element("k")).Select(n => n.Value).Where(n => n.StartsWith(partWord));

        }

        private void textboxFindWord_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                combobox.Focus();
                return;
            }
            if (e.Key == Key.Enter)
            {
                string word = textboxFindWord.Text.Trim();
                paragraf.Inlines.Clear();
                Procesing(path, word);
                combobox.ItemsSource = null;
                combobox.IsDropDownOpen = false;

            }
        }
        private void Procesing(string path, string word)
        {

            string text = ReadFileAndGetValue(path, word);
            text = Regex.Replace(text, "_", "");
            string[] vs = text.Split(new char[] { '\n' });
            List<string> lists = vs.ToList();
            IEnumerable<string> enu = lists.Select(n => Replace(n)).Select(n => InsertTire(n));
            string newtext = "";

            foreach (string item in enu)
            {
                newtext += item + "\n";
            }
            string[] vs1 = Regex.Split(newtext, @"( *[0-9]\) .*)");
            for (int i = 0; i < vs1.Length; i++)
            {
                string item = vs1[i];
                Run run = new Run(item);
                if (i == 0)
                {
                    run.FontSize = 26;
                    run.FontWeight = FontWeights.Bold;
                }
                if (Regex.IsMatch(item, @" *[0-9]\)"))
                {
                    run.Foreground = Brushes.BlueViolet;
                    run.FontStyle = FontStyles.Italic;
                    run.FontSize = 20;
                }
                paragraf.Inlines.Add(run);
            }


        }

        private void buttonFindAudio_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string word = textboxWord.Text;
                if (word == "")
                {
                    MessageBox.Show("Введите слово!");
                    return;
                }
                string path = FIleTools.ReadPath(FIleTools.NameFilePathes);
                word += ".wav";
                audioFile = FIleTools.SearchFile(word, path);
                if (audioFile == null)
                {
                    MessageBox.Show("Аудиофайл не найден!\nУкажите файл в ручную.");
                    return;
                }
                else
                {
                    textboxAudio.Text = audioFile.Name;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void buttonfile_Click(object sender, RoutedEventArgs e)
        {
            DefaultDialogService dds = new DefaultDialogService();
            dds.OpenFileDialog();
            string fileName = dds.FilePath;
            audioFile = new FileInfo(fileName);
            textboxAudio.Text = audioFile.Name;
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            string translate = textboxTranslation.Text;
            IEnumerable<string> translates = translate.Split(new char[] { ',', '.', '\n', '\r', ';' }).Select(n => n.Trim()).Where(n => Regex.IsMatch(n, "\\S"));
            string example = textboxExample.Text;
            IEnumerable<string> examples = example.Split(new char[] { ';' }).Select(n => n.Trim());

            string word = textboxWord.Text.ToLower().Trim();
            string audio = textboxAudio.Text.ToLower().Trim();
            string transcription = textboxTranscrition.Text.ToLower().Trim();
            if (word == "" || audio == "" || translate == "" || audioFile == null)
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            if (!File.Exists(FIleTools.NameDirectoryAudio + "/" + audioFile.Name))
            {
                FIleTools.CopyTo(audioFile, true);
            }

            WordSample _wordsSample = new WordSample();
            _wordsSample.Word = word;
            _wordsSample.Translate = new ObservableCollection<string>(translates);
            _wordsSample.DateTimeInsert = DateTime.Now;
            _wordsSample.DateTimeLastCall = DateTime.Now;
            _wordsSample.State = (int)State.New;
            _wordsSample.SoundName = audioFile.Name;
            int st = 0;
            try
            {
                st = BdTools.AddNewWords(_wordsSample);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Clear();
        }

        private void Clear()
        {
            audioFile = null;
            textboxWord.Text = "";
            textboxTranscrition.Text = "";
            textboxAudio.Text = "";
            textboxTranslation.Text = "";
            textboxExample.Text = "";

        }
    }
}
