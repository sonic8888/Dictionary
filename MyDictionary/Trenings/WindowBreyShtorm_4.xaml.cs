using MyDictionary.EF;
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

namespace MyDictionary.Trenings
{
    /// <summary>
    /// Логика взаимодействия для WindowBreyShtorm_4.xaml
    /// </summary>
    public partial class WindowBreyShtorm_4 : Window
    {
        List<MyWord> myWords;
        int currentword = 0;
        public WindowBreyShtorm_4(List<MyWord> words)
        {
            myWords = words;

            InitializeComponent();
            textboxkword.Focus();
        }

        private void buttonSound_Click(object sender, RoutedEventArgs e)
        {
            FileInfo fi = FIleTools.SearchFile(myWords[currentword].SoundName, FIleTools.NameDirectoryAudio);
            PlaySound(fi);
        }
        private void PlaySound(FileInfo sound)
        {
            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri(sound.FullName));
            mediaPlayer.Play();
        }

        private void textboxkword_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (textboxkword.Text.ToUpper() == myWords[currentword].Word.ToUpper())
                {
                    VisibilityOn();
                    textblockword.Text = myWords[currentword].Word;
                    textblocktranscription.Text = myWords[currentword].Transcription;
                    textblocktranslation.Text = myWords[currentword].MyTranslates.First().Translate;
                }
            }
        }
        private void AnsweYes()
        {

        }
        private void VisibilityOn()
        {
            textboxkword.Visibility = Visibility.Collapsed;
            textblockmessage.Visibility = Visibility.Collapsed;
            textblockword.Visibility = Visibility.Visible;
            textblocktranscription.Visibility = Visibility.Visible;
            textblocktranslation.Visibility = Visibility.Visible;
            textblockword.Foreground = new SolidColorBrush(Colors.Green);
            textblocktranscription.Foreground = new SolidColorBrush(Colors.Gray);
            
        }
        private void VisibilityOf()
        {
            textboxkword.Visibility = Visibility.Visible;
            textblockmessage.Visibility = Visibility.Visible;
            textblockword.Visibility = Visibility.Collapsed;
            textblocktranscription.Visibility = Visibility.Collapsed;
            textblocktranslation.Visibility = Visibility.Collapsed;
        }
    }
}
