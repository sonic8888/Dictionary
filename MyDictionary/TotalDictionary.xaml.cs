﻿using MyDictionary.EF;
using MyDictionary.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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
using XMLRead;



namespace MyDictionary
{
    /// <summary>
    /// Логика взаимодействия для TotalDictionary.xaml
    /// </summary>
    public partial class TotalDictionary : Window
    {
        ObservableCollection<MyWord> collection;
        List<int> collDelete;
        MainWindow window;
        Thread thread;
        string messageQuestion = "Вы хотите удалить выделеные слова?";
        string messageWarning = "Внимание!";
        string colorForegraund = "#FFCFCDCD";//серый цвет 
        public TotalDictionary(ObservableCollection<MyWord> col, MainWindow win)
        {
            InitializeComponent();

            collection = col;
            listViewDictionary.ItemsSource = collection;
            collDelete = new List<int>();

           
           

            window = win;
        }

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
                MediaPlayer mediaPlayer = new MediaPlayer();
                mediaPlayer.Open(new Uri(sound.FullName));
                mediaPlayer.Play();
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
            collection.Remove(wordDel);

        }

        private void buttonReffresh_Click(object sender, RoutedEventArgs e)
        {
            StartNewThread();
            while (thread.IsAlive)
            {
                Thread.Sleep(50);
            }
            listViewDictionary.ItemsSource = collection;
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
                }
            }
        }
        private void DeleteWord(int wordId)
        {
            BdTools.DeleteWord(wordId);
            MyWord myWord = collection.Where(n => n.WordId == wordId).First();
            collection.Remove(myWord);

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
            int state = 0;
            WindowStateChose wsc = new WindowStateChose(id) { Collection = collection };
            wsc.InitRadioButton();
            if (wsc.ShowDialog() == true)
            {
                // сохраняем в бд
            }
            else
            {

            }
        }

        private void buttonStateGrey_Click(object sender, RoutedEventArgs e)
        {
            int id = 6;
        }
    }
}
