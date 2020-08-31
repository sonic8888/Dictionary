using MyDictionary.EF;
using MyDictionary.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MyDictionary.XMLRead;
using Path = System.IO.Path;

namespace MyDictionary
{
    /// <summary>
    /// Логика взаимодействия для TotalDictionary.xaml
    /// </summary>
    public partial class TotalDictionary : Window
    {
        ObservableCollection<MyWord> collection;
        List<int> collDelete;
        //MainWindow window;
        Thread thread;
        string messageQuestion = "Вы хотите удалить выделеные слова?";
        string messageWarning = "Внимание!";
        string text = "Всего слов: ";
        //string colorForegraund = "#FFCFCDCD";//серый цвет 
        string pathGrey = @"Picture/Cub_grey.png";
        string pathGreen = @"Picture/Cub_green.png";
        string pathGold = @"Picture/Cub_Gold.png";
        private bool IsOrder = true;
        public TotalDictionary(ObservableCollection<MyWord> col)
        {
            InitializeComponent();

            collection = col;
            listViewDictionary.ItemsSource = collection;
            collDelete = new List<int>();
            textblockTotalWords.Text = text + collection.Count.ToString();



            //window = win;
        }
        public int StateWord { get; set; }

        private void buttonSound_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string str = button.DataContext as string;
            if (str != null)
            {
                FileInfo sound;
                try
                {
                    sound = FIleTools.SearchFile(str, FIleTools.NameDirectoryAudio);

                }
                catch (IOException ex)
                {

                    MessageBox.Show(ex.Message);
                    return;
                };

                try
                {
                    MediaPlayer mediaPlayer = new MediaPlayer();
                    mediaPlayer.Open(new Uri(sound.FullName));
                    mediaPlayer.Play();
                }
                catch (Exception ex)
                {


                    MessageBox.Show(ex.Message, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

        }

        private void checkbox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            int index = (int)cb.DataContext;
            collDelete.Add(index);
        }

        private void checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            int index = (int)cb.DataContext;
            collDelete.Remove(index);
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            int index = (int)but.DataContext;

            MyWord wordDel = BdTools.DeleteWord(index);
            FileInfo fileInfo = FIleTools.SearchFile(wordDel.SoundName, FIleTools.NameDirectoryAudio);
            collection.Remove(wordDel);
            try
            {
                File.Delete(fileInfo.FullName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + MethodBase.GetCurrentMethod().DeclaringType.FullName);
                return;
            }

        }

        private void buttonReffresh_Click(object sender, RoutedEventArgs e)
        {
            Reffresh();
        }

        private void Reffresh()
        {
            Thread thread = App.StartNewThread();
            while (thread.IsAlive)
            {
                Thread.Sleep(50);
            }
            listViewDictionary.ItemsSource = App.collection;
            collection = App.collection;
            textblockTotalWords.Text = text + collection.Count.ToString();
        }
        private void ReadDictionary()
        {

            try
            {
                collection = BdTools.ReadWord();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        public void StartNewThread()
        {
            thread = new Thread(new ThreadStart(ReadDictionary));
            thread.Start();
        }

        private void buttonDelete_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(messageQuestion, messageWarning, MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                foreach (int index in collDelete)
                {
                    DeleteWord(index);
                    textblockTotalWords.Text = text + collection.Count.ToString();
                }
                Reffresh();
                collDelete.Clear();
            }
        }
        private void DeleteWord(int wordId)
        {
            BdTools.DeleteWord(wordId);
            //MyWord myWord = collection.Where(n => n.WordId == wordId).Single();
            MyWord myWord = null;
            foreach (MyWord item in collection)
            {
                if (item.WordId == wordId)
                {
                    myWord = item;
                }
            }
            if (myWord == null)
            {
                MessageBox.Show("Слово не найдено!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            collection.Remove(myWord);
            FileInfo fileInfo = FIleTools.SearchFile(myWord.SoundName, FIleTools.NameDirectoryAudio);
            if (fileInfo != null)
            {
                fileInfo.IsReadOnly = false;
                try
                {
                    File.Delete(fileInfo.FullName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Файл отсутствует в папке SoundFiles", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        /// <summary>
        /// при получение фокуса textboxFindWord убираем посказку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textboxFindWord_GotFocus(object sender, RoutedEventArgs e)
        {
            textboxFindWord.Text = "";
            textboxFindWord.Foreground = new SolidColorBrush(Colors.Black);
        }
        /// <summary>
        /// при потере фокуса textboxFindWord возврашаем подсказку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textboxFindWord_LostFocus(object sender, RoutedEventArgs e)
        {
            textboxFindWord.Text = "найти слово";
            textboxFindWord.Foreground = new BrushConverter().ConvertFrom("#FFCFCDCD") as SolidColorBrush;
        }

        private void buttonState_Click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            int id = (int)but.DataContext;

            MyWord mw = collection.Where(n => n.WordId == id).First();


            WindowStateChose wsc = new WindowStateChose(this, mw.State);

            if (wsc.ShowDialog() == true)
            {
                Image im = but.Content as Image;
                im.Source = InitBitMap();
                mw.State = StateWord;
                BdTools.UpdateStateMyWord(id, StateWord);
            }

        }

        private void buttonStateGrey_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<MyWord> en = collection.OrderByDescending(n => n.State == 1).Select(n => n);
            listViewDictionary.ItemsSource = new ObservableCollection<MyWord>(en);
        }
        private BitmapImage InitBitMap()
        {
            string path = "";
            switch (StateWord)
            {
                case 1: path = pathGrey; break;
                case 2: path = pathGreen; break;
                case 3: path = pathGold; break;
                default:
                    break;
            }
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(path, UriKind.Relative);
            bi.EndInit();

            return bi;
        }

        private void textboxFindWord_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string text = textboxFindWord.Text;
            IEnumerable<MyWord> en = collection.Where(n => n.Word.StartsWith(text));
            listViewDictionary.ItemsSource = new ObservableCollection<MyWord>(en);
        }

        private void textboxFindWord_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                string text = textboxFindWord.Text;
                IEnumerable<MyWord> en = collection.Where(n => n.Word.StartsWith(text));
                listViewDictionary.ItemsSource = new ObservableCollection<MyWord>(en);
            }
        }

        private void buttonDateOrderBy_Click(object sender, RoutedEventArgs e)
        {
            if (IsOrder)
            {
                ObservableCollection<MyWord> collection = listViewDictionary.ItemsSource as ObservableCollection<MyWord>;
                IEnumerable<MyWord> en = collection.OrderByDescending(n => n.DataTimeInsert);

                listViewDictionary.ItemsSource = new ObservableCollection<MyWord>(en);
                IsOrder = false;
            }
            else
            {
                ObservableCollection<MyWord> collection = listViewDictionary.ItemsSource as ObservableCollection<MyWord>;
                IEnumerable<MyWord> en = collection.OrderBy(n => n.DataTimeInsert);

                listViewDictionary.ItemsSource = new ObservableCollection<MyWord>(en);
                IsOrder = true;
            }
        }

        private void comboboxListElement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null)
            {
                ComboBox comboBox = (ComboBox)sender;
                TextBlock tb = (TextBlock)comboBox.SelectedItem;
                string text = tb.Text;
                text = new string(text.ToArray(), 0, 3);
                SelectCollection(text);

            }

        }
        private void SelectCollection(string part)
        {
            if (part == "все")
            {

                listViewDictionary.ItemsSource = new ObservableCollection<MyWord>(collection.OrderBy(n => n.Word));
                return;
            }
            IEnumerable<MyWord> en = collection.OrderByDescending(n => n.PartOfSpeach.StartsWith(part));
            listViewDictionary.ItemsSource = new ObservableCollection<MyWord>(en);

        }

        private void buttonStateGreen_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<MyWord> en = collection.OrderByDescending(n => n.State == 2).Select(n => n);
            listViewDictionary.ItemsSource = new ObservableCollection<MyWord>(en);
        }

        private void buttonStateGold_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<MyWord> en = collection.OrderByDescending(n => n.State == 3).Select(n => n);
            listViewDictionary.ItemsSource = new ObservableCollection<MyWord>(en);
        }



        private void buttonState_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Button bt = sender as Button;
            int wordid = (int)bt.DataContext;
            MyWord mw = BdTools.FindMyWord(wordid);
            WindowWordEdit wwe = new WindowWordEdit(mw);
            if (wwe.ShowDialog() == true)
            {

            }

        }

        private void buttonAddWord_Click(object sender, RoutedEventArgs e)
        {
            ChoseWords chw = new ChoseWords();

            chw.Show();
        }

        private void MenuItemControlAudio_Click(object sender, RoutedEventArgs e)
        {
            //IEnumerable<string> enumerable = FTPSinchronisation.GetListDirectoryLocal(false);
            IEnumerable<string> enumerable = BdTools.GetAudio();
            string str = "";
            foreach (string item in enumerable)
            {
                str += item + "\n";
            }
            MessageBox.Show(str);
        }
    }
}
