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
    /// Логика взаимодействия для TotalDictionary.xaml
    /// </summary>
    public partial class TotalDictionary : Window
    {
        ObservableCollection<MyWord> collection;
        List<int> collDelete;
        MainWindow window;
        public TotalDictionary(ObservableCollection<MyWord> collection, MainWindow win)
        {
            InitializeComponent();
            //try
            //{
            //    collection = BdTools.ReadWord();
            //    listViewDictionary.ItemsSource = collection;
            //    collDelete = new List<int>();
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message);
            //}

            this.collection = collection;
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
            int index = (int)cb.Content;
            collDelete.Add(index);
        }

        private void checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            int index = (int)cb.Content;
            collDelete.Remove(index);
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            int index = (int)but.DataContext;

            MyWord wordDel = BdTools.DeleteWord(index);
            collection.Remove(wordDel); 

        }
    }
}
