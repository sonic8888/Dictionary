﻿using MyDictionary.EF;
using MyDictionary.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using MyDictionary.XMLRead;
using MyDictionary.Trenings;
using MyDictionary.Repetition;
using System.IO;
using System.Net;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Media.Animation;
using Microsoft.Win32;

namespace MyDictionary
{
    public enum State
    {
        New = 1, Learn = 2, Know = 3
    }
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<MyWord> collection;
        Thread thread;


        private BackgroundWorker backgroundWorker;
        private BackgroundWorker backgroundWorkerLoadAudio;
        private BackgroundWorker backgroundWorkerWriteDB;
        private BackgroundWorker backgroundWorkerWriteSeverAudio;
        SolidColorBrush gbrash;

        public MainWindow()
        {
            InitializeComponent();
            backgroundWorker = ((BackgroundWorker)this.FindResource("backgroundWorker"));
            backgroundWorkerLoadAudio = ((BackgroundWorker)this.FindResource("backgroundWorkerLoadAudio"));
            backgroundWorkerWriteDB = ((BackgroundWorker)this.FindResource("backgroundWorkerWriteDB"));
            backgroundWorkerWriteSeverAudio = ((BackgroundWorker)this.FindResource("backgroundWorkerWriteSeverAudio"));
            FIleTools.CreateDirectory(FIleTools.NameDirectoryAudio);
            FIleTools.CreateDirectory(FIleTools.NameDirectoryStorage);
            //StartNewThread();
            textboxCountWord.Text = App.dataVariable.CountWordLearning.ToString();
            textboxCounSelekt.Text = App.dataVariable.CountSelectWord.ToString();
            textboxCountRepetition.Text = App.dataVariable.CountWordRepetition.ToString();
            textboxCountMlSekRepetition.Text = App.dataVariable.CountMilisek.ToString();
            textboxCountMlSekDelayRepetition.Text = App.dataVariable.CountMilisekDelay.ToString();
            textboxCountTimeWork.Text = App.dataVariable.CountTimeWork.ToString();
            textboxCountWordTrenings.Text = App.dataVariable.CountWordTrenings.ToString();
            textboxCountWordSprint.Text = App.dataVariable.CountWordSprint.ToString();
            gbrash = (SolidColorBrush)textblockMessage.Foreground;
            if (App.dataVariable.IsUpdateState == 1)
            {
                checkboxStatus.IsChecked = true;
            }

        }
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            HelpWorker hw = (HelpWorker)e.Argument;
            FTPSinchronisation.DownloadDb(backgroundWorker, hw.Size);
        }
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarDownload.IsIndeterminate = false;
            textblockMessage.Text = "БД скопирована!";
            TextAnimation();

        }
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarDownload.Value += e.ProgressPercentage;
        }

        private void clickNewWord(object sender, RoutedEventArgs e)
        {

            ChoseWords chw = new ChoseWords();
            chw.Show();

        }

        private void ReadDictionary()
        {

            try
            {
                collection = BdTools.ReadWord();

            }
            catch (Exception)
            {

                return;
            }
        }
        public void StartNewThread()
        {
            thread = new Thread(new ThreadStart(ReadDictionary));
            thread.Start();
        }

        private void buttonBreyShtorm_Click(object sender, RoutedEventArgs e)
        {
            //while (App.thread.IsAlive)
            //{
            //    Thread.Sleep(200);
            //}
            //if (App.collection.Count < App.dataVariable.CountWordSprint)
            //{
            //    MessageBox.Show("Для нормальной работы приложения кол-во слов в словаре должно быть не менее: " + App.dataVariable.CountWordSprint, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return;
            //}

            WindowsManager.CreateWindowBreyShtorm();
        }

        private void textboxCountWord_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string str = textboxCountWord.Text;
                int value;
                if (int.TryParse(str, out value))
                {
                    App.dataVariable.CountWordLearning = value;
                }
            }
        }



        private void textboxCounSelekt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string str = textboxCounSelekt.Text;
                int value;
                if (int.TryParse(str, out value))
                {
                    App.dataVariable.CountSelectWord = value;
                }
            }
        }



        private void textboxCountRepetition_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string str = textboxCountRepetition.Text;
                int value;
                if (int.TryParse(str, out value))
                {
                    App.dataVariable.CountWordRepetition = value;
                }
            }
        }

        private void textboxCountMlSekRepetition_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string str = textboxCountMlSekRepetition.Text;
                int value;
                if (int.TryParse(str, out value))
                {
                    App.dataVariable.CountMilisek = value;
                }
            }
        }

        private void textboxCountMlSekDelayRepetition_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string str = textboxCountMlSekDelayRepetition.Text;
                int value;
                if (int.TryParse(str, out value))
                {
                    App.dataVariable.CountMilisekDelay = value;
                }
            }
        }

        //private void buttonSprint_Click(object sender, RoutedEventArgs e)
        //{

        //    List<MyWord> lists = BdTools.GetRandomListMyWord(10);
        //    if (lists != null)
        //    {

        //        WindowsManager.CreateWindowSprint(lists);
        //    }
        //}

        private void textboxCountTimeWork_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string str = textboxCountTimeWork.Text;
                int value;
                if (int.TryParse(str, out value))
                {
                    App.dataVariable.CountTimeWork = value;
                }
            }
        }

        private void textboxCountWordTrenings_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string str = textboxCountWordTrenings.Text;
                int value;
                if (int.TryParse(str, out value))
                {
                    App.dataVariable.CountWordTrenings = value;
                }
            }
        }

        private void buttonDictionary_Click_1(object sender, RoutedEventArgs e)
        {
            //while (App.thread.IsAlive)
            //{
            //    Thread.Sleep(50);
            //}
            //WindowsManager.CreateTotalDictionary();
            //ObservableCollection<MyWord> collection = BdTools.ReadWord();
            ReadDictionary();
            TotalDictionary td = new TotalDictionary(collection);
            td.Show();
            this.WindowState = WindowState.Minimized;
        }

        private void buttonUpr_Click(object sender, RoutedEventArgs e)
        {
            //while (App.thread.IsAlive)
            //{
            //    Thread.Sleep(200);
            //}
            //if (App.collection.Count < App.dataVariable.CountWordSprint)
            //{
            //    MessageBox.Show("Для нормальной работы приложения кол-во слов в словаре должно быть не менее: " + App.dataVariable.CountWordSprint, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return;
            //}
            VisibilityElements(Visibility.Hidden, Visibility.Visible, Visibility.Collapsed);
        }

        private void buttonWordTranslate_Click(object sender, RoutedEventArgs e)
        {
            List<MyWord> lists = BdTools.GetRandomListMyWord(App.dataVariable.CountWordTrenings);
            //ObservableCollection<MyWord> collec_5 = BdTools.ReadWord(3);
            ObservableCollection<MyWord> collectionTotal
                = BdTools.ReadWord();
            if (lists != null && collectionTotal != null)
            {
                IEnumerable<MyWord> enumerable = collectionTotal.Except(lists);
                WindowsManager.GreateWindowBreyShtorm_2(lists, enumerable.ToList(), true);

            }

        }

        private void buttonWordConstructor_Click(object sender, RoutedEventArgs e)
        {
            List<MyWord> lists = BdTools.GetRandomListMyWord(App.dataVariable.CountWordTrenings);
            if (lists != null)
            {

                WindowsManager.CreateWindowBreyShtorm_3(lists, true);
            }
        }

        private void buttonAudirovanie_Click(object sender, RoutedEventArgs e)
        {
            List<MyWord> lists = BdTools.GetRandomListMyWord(App.dataVariable.CountWordTrenings);
            if (lists != null)

            {
                try
                {
                    WindowsManager.CreateWindowBreyShtorm_4(lists, true);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            }
        }

        private void buttonRepetition_Click(object sender, RoutedEventArgs e)
        {
            int count = App.dataVariable.CountWordRepetition;
            List<MyWord> lists = BdTools.GetRandomListMyWord(count);
            if (lists != null)
            {
                WindowsManager.CreateWindowRepetition(lists);

            }
        }

        private void buttonSprint_Click_1(object sender, RoutedEventArgs e)
        {
            List<MyWord> lists = BdTools.GetRandomListMyWord(App.dataVariable.CountWordSprint);
            if (lists != null)
            {

                WindowsManager.CreateWindowSprint(lists);
            }
        }
        private void VisibilityElements(Visibility visibilitiOne, Visibility visibilitiTwo, Visibility visibilitiThree)
        {
            buttonDictionary.Visibility = visibilitiOne;
            buttonToAudioCloud.Visibility = visibilitiOne;
            buttonToCloud.Visibility = visibilitiOne;
            buttonTrenings.Visibility = visibilitiOne;
            buttonUpr.Visibility = visibilitiOne;
            separator_1.Visibility = visibilitiOne;
            separator_2.Visibility = visibilitiOne;
            separator_3.Visibility = visibilitiOne;
            separator_4.Visibility = visibilitiOne;
            separator_5.Visibility = visibilitiOne;
            buttonBack.Visibility = visibilitiTwo;
            buttonWordTranslate.Visibility = visibilitiTwo;
            buttonWordConstructor.Visibility = visibilitiTwo;
            buttonAudirovanie.Visibility = visibilitiTwo;
            buttonRepetition.Visibility = visibilitiTwo;
            buttonSprint.Visibility = visibilitiTwo;
            buttonSinc.Visibility = visibilitiThree;
            buttonFromCloudAudio.Visibility = visibilitiThree;
            buttonToCloud.Visibility = visibilitiThree;
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {

            VisibilityElements(Visibility.Visible, Visibility.Collapsed, Visibility.Visible);
        }

        private void textboxCountWordSprint_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string str = textboxCountWordSprint.Text;
                int value;
                if (int.TryParse(str, out value))
                {
                    App.dataVariable.CountWordSprint = value;
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            App.dataVariable.IsUpdateState = 1;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            App.dataVariable.IsUpdateState = 0;
        }

        private void buttonSinc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime dtServ = FTPSinchronisation.GetDataServerBD();
                DateTime dtLoc = FTPSinchronisation.GetDataLocalBd();
                if (dtServ < dtLoc)
                {
                    string message = $"Сервер БД: {dtServ},\nЛокал БД: {dtLoc}\n" +
                        $"БД сервера старее локальной БД.\n" +
                        $"Продолжить скачивание?";
                    MessageBoxResult messageBoxResult = MessageBox.Show(message, "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (messageBoxResult == MessageBoxResult.No)
                    {
                        return;
                    }

                }

                //double size = FTPSinchronisation.GetSizeServerDB();
                //progressBarDownload.Maximum = size;
                //int oneProcent = (int)size / 100;
                progressBarDownload.IsIndeterminate = true;
                HelpWorker hw = new HelpWorker(0, progressBarDownload);
                backgroundWorker.RunWorkerAsync(hw);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }

        private void progressBarDownload_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void buttonToCloud_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime dataServ = FTPSinchronisation.GetDataServerBD();
                DateTime dataLocal = FTPSinchronisation.GetDataLocalBd();
                if (dataLocal < dataServ)
                {
                    MessageBox.Show("Локальная БД старее чем на сервере!");
                    return;
                }
                progressBarDownload.IsIndeterminate = true;
                backgroundWorkerWriteDB.RunWorkerAsync();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }


        private void buttonFromCloudAudio_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                progressBarDownload.IsIndeterminate = true;
                backgroundWorkerLoadAudio.RunWorkerAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        private void TextAnimation()
        {
            //SolidColorBrush gbrash = (SolidColorBrush)textblockMessage.Foreground;

            SolidColorBrush myBrush = new SolidColorBrush();
            ColorAnimation colAnim = new ColorAnimation();
            colAnim.Duration = TimeSpan.FromMilliseconds(2000);
            colAnim.From = gbrash.Color;
            colAnim.To = Color.FromArgb(0, 0, 0, 0);
            colAnim.Completed += BackColor;

            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, colAnim);
            textblockMessage.Foreground = myBrush;

        }

        private void BackgroundWorker_DoWork_1(object sender, DoWorkEventArgs e)
        {
            FTPSinchronisation.LoaderAudio();
        }

        private void BackgroundWorker_ProgressChanged_1(object sender, ProgressChangedEventArgs e)
        {
            progressBarDownload.Value = e.ProgressPercentage;
        }

        private void BackgroundWorker_RunWorkerCompleted_1(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarDownload.IsIndeterminate = false;
            textblockMessage.Text = "Аудиофайлы загружены!";
            TextAnimation();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.CustomPlaces.Add(new FileDialogCustomPlace(FIleTools.NameDirectoryAudio));
            //openFileDialog.Filter = "Wav files (*.wav)|*.wav|MP3 files (*.mp3)|*.mp3";
            //openFileDialog.FileName = FIleTools.NameDirectoryAudio;
            openFileDialog.ShowDialog();

        }

        private void BackgroundWorker_DoWork_2(object sender, DoWorkEventArgs e)
        {

            try
            {
                FTPSinchronisation.WriteBD();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void BackgroundWorker_RunWorkerCompleted_2(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarDownload.IsIndeterminate = false;
            textblockMessage.Text = "БД отправлена на сервер!";
            TextAnimation();
        }

        private void buttonToAudioCloud_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                progressBarDownload.IsIndeterminate = true;
                backgroundWorkerWriteSeverAudio.RunWorkerAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void BackgroundWorker_DoWork_3(object sender, DoWorkEventArgs e)
        {
            try
            {
                FTPSinchronisation.LoaderAudioToServer();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }

        private void BackgroundWorker_RunWorkerCompleted_3(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarDownload.IsIndeterminate = false;
            textblockMessage.Text = "Аудиофайлы отправлены на сервер!";
            TextAnimation();
        }
        private void BackColor(object sender, EventArgs e)
        {
            textblockMessage.Text = "";
            textblockMessage.Foreground = gbrash;

        }

        private void buttonDBCopy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (File.Exists(FTPSinchronisation.PatnLocalTempBD))
                {
                    FileInfo copy = new FileInfo(FTPSinchronisation.PatnLocalTempBD);
                    copy.CopyTo(FTPSinchronisation.PatnLocalBD, true);
                    textblockMessage.Text = "БД обновлена!";
                    TextAnimation();
                }
                else
                {
                    MessageBox.Show("Файла Базы Данных не обнаружено!\nСкачайте его.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        //private void buttonTesst_Click(object sender, RoutedEventArgs e)
        //{
        //    ObservableCollection<MyWord> col = BdTools.ReadWord(5);
        //    WindowsManager.CreateWindowBreyShtorm_3(col.ToList(), false);
        //}
    }
}
