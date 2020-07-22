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
    /// Логика взаимодействия для WindowBreyShtorm.xaml
    /// </summary>
    public partial class WindowBreyShtorm : Window
    {
        ObservableCollection<MyWord> words;
        ObservableCollection<MyWord> wordsTrenings;
        int count = 0;
        public WindowBreyShtorm(ObservableCollection<MyWord> coll)
        {
            words = coll;
            InitializeComponent();
            InitDbContext(words[count]);
            wordsTrenings = new ObservableCollection<MyWord>();
        }
        private void InitDbContext(MyWord w)
        {
            this.DataContext = w;
            FileInfo fi = FIleTools.SearchFile(w.SoundName, FIleTools.NameDirectoryAudio);
            PlaySound(fi);
        }

        private void buttonSound_Click(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            string sound = bt.DataContext.ToString();
            FileInfo fi = FIleTools.SearchFile(sound, FIleTools.NameDirectoryAudio);
            PlaySound(fi);

        }
        private void PlaySound(FileInfo sound)
        {
            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri(sound.FullName));
            mediaPlayer.Play();
        }

        private void buttonNotKnow_Click(object sender, RoutedEventArgs e)
        {
            wordsTrenings.Add(words[count]);
            if (++count < words.Count && count < App.dataVariable.CountWordLearning)
            {
                InitDbContext(words[count]);

            }
            else
            {
                ObservableCollection<MyWord> total_50 = BdTools.ReadWord(App.dataVariable.CountSelectWord);
                IEnumerable<MyWord> resurse = total_50.Except(wordsTrenings);
                WindowBreyShtorm_2 wb2 = new WindowBreyShtorm_2(wordsTrenings.ToList(), resurse.ToList());
                wb2.Show();
                this.Close();
            }
        }

        private void buttonKnow_Click(object sender, RoutedEventArgs e)
        {
            if (++count < words.Count)
            {
                BdTools.UpdateStateMyWord(words[count], State.Know);

                InitDbContext(words[count]);

            }
            else
            {
                if (wordsTrenings.Count != 0)
                {
                    ObservableCollection<MyWord> total_50 = BdTools.ReadWord(App.dataVariable.CountSelectWord);
                    IEnumerable<MyWord> resurse = total_50.Except(wordsTrenings);
                    WindowBreyShtorm_2 wb2 = new WindowBreyShtorm_2(wordsTrenings.ToList(), resurse.ToList());
                    wb2.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Нет новых слов");
                    this.Close();

                }
            }



        }
    }
}
