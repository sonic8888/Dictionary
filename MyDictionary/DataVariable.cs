using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using XMLRead;
using System.Windows;
using System.Reflection;

namespace MyDictionary
{
    public class DataVariable
    {
        private List<string> defoltValueList;
        private int countTimeWork;//время работы упражнения Sprint
        private int countWordRepetition;// кол-во слов для упражнения повторения (Repetitio)
        private int countWordLearning;//кол-во слов для тренировки (Trenings)
        private int countSelectWord;//вспомогательная вывыбока для создания ресурса слов(Trenings)
        private int countMilisek;//время задержки таймера прогрессбара в WindowRepetition
        private int countMilisekDelay;//время задержки для смены слов в WindowRepetition
        private int countWordTrenings;//кол-во слов для тренировки(упражнений)
        private int countWordSprint;//кол-во слов в упражнении Спринт
        public FileInfo soundYes;//звуковой файл "Галочка" "Yes" правилиьный ответ
        private FileInfo soundNo;//звуковой файл "Нет" не правильный ответ (ошибка)
        public DataVariable()
        {
            defoltValueList = new List<string>();
            defoltValueList.Add("5");// defoltValue countWordLearning
            defoltValueList.Add("50");// defoltValue countSelectWord
            defoltValueList.Add("20");// defoltValue countWordRepetition
            defoltValueList.Add("10");// defoltValue countMilisek
            defoltValueList.Add("1000");// defoltValue countMilisekDelay
            defoltValueList.Add("30");// defoltValue countTimeWork
            defoltValueList.Add("10");// defoltValue countWordTrenings
            defoltValueList.Add("30");// defoltValue countWordSprint
            if (!File.Exists(FIleTools.NameFileDataVariable))
            {
                WriteDefoltValue();
            }
            else
            {
                try
                {
                    List<string> listValue;
                    using (StreamReader sr = File.OpenText(FIleTools.NameFileDataVariable))
                    {
                        string input = null;
                        listValue = new List<string>();
                        while ((input = sr.ReadLine()) != null)
                        {
                            int v = 0;
                            if (int.TryParse(input, out v))
                            {
                                listValue.Add(input);

                            }
                        }


                    }
                    if (listValue.Count != defoltValueList.Count)
                    {
                        WriteDefoltValue();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " " + MethodBase.GetCurrentMethod().DeclaringType.FullName);

                }
            }
            soundYes = new FileInfo(@".\SoundsApplication\soundYes.mp3");
            soundNo = new FileInfo(@".\SoundsApplication\soundNo.mp3");
            InitVariable();
        }
        private void WriteDefoltValue()
        {
            StreamWriter streamWriter;
            using (streamWriter = File.CreateText(FIleTools.NameFileDataVariable))
            {
                foreach (string item in defoltValueList)
                {
                    streamWriter.WriteLine(item);
                }
            }
        }
        private void InitVariable()
        {
            try
            {
                using (StreamReader sr = File.OpenText(FIleTools.NameFileDataVariable))
                {
                    string input = null;
                    List<string> listValue = new List<string>();
                    while ((input = sr.ReadLine()) != null)
                    {
                        listValue.Add(input);
                    }
                    try
                    {

                        countWordLearning = int.Parse(listValue[0]);//читаем countWordLearning
                        countSelectWord = int.Parse(listValue[1]);//читаем countSelectWord
                        countWordRepetition = int.Parse(listValue[2]);//читаем countWordRepetition
                        countMilisek = int.Parse(listValue[3]);//читаем countMilisek
                        countMilisekDelay = int.Parse(listValue[4]);//читаем countMilisekDelay
                        countTimeWork = int.Parse(listValue[5]);//читаем countTimeWork
                        countWordTrenings = int.Parse(listValue[6]);//читаем countWordTrenings
                        countWordSprint = int.Parse(listValue[7]);//читаем countWordSprint

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message + " " + MethodBase.GetCurrentMethod().DeclaringType.FullName);
                    }

                }

            }
            catch (IOException ex)
            {

                MessageBox.Show(ex.ToString() + " " + MethodBase.GetCurrentMethod().DeclaringType.FullName);
            }
        }
        /// <summary>
        /// кол-во тренируемых слов
        /// </summary>
        public int CountWordLearning
        {
            get { return countWordLearning; }
            set { countWordLearning = value; }
        }

        public void WriteFile()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(FIleTools.NameFileDataVariable))
                {
                    sw.WriteLine(countWordLearning);
                    sw.WriteLine(countSelectWord);
                    sw.WriteLine(countWordRepetition);
                    sw.WriteLine(countMilisek);
                    sw.WriteLine(countMilisekDelay);
                    sw.WriteLine(countTimeWork);
                    sw.WriteLine(countWordTrenings);
                    sw.WriteLine(countWordSprint);
                }
            }
            catch (IOException ex)
            {

                MessageBox.Show(ex.ToString() + " " + MethodBase.GetCurrentMethod().DeclaringType.FullName);
            }
        }
        /// <summary>
        /// кол-во слов выборки
        /// </summary>
        public int CountSelectWord
        {
            get { return countSelectWord; }
            set { countSelectWord = value; }
        }

        /// <summary>
        /// кол-во слов для повторения
        /// </summary>
        public int CountWordRepetition
        {
            get { return countWordRepetition; }
            set { countWordRepetition = value; }
        }

        /// <summary>
        /// определяет время для ответа при повторении слов
        /// </summary>
        public int CountMilisek
        {
            get { return countMilisek; }
            set { countMilisek = value; }
        }
        /// <summary>
        /// время задержки для смены слов в WindowRepetition
        /// </summary>
        public int CountMilisekDelay
        {
            get { return countMilisekDelay; }
            set { countMilisekDelay = value; }
        }
     
        /// <summary>
        /// звук утверждения
        /// </summary>
        public FileInfo SoundYes
        {
            get { return soundYes; }
            set { soundYes = value; }
        }
        /// <summary>
        /// звук отрицания
        /// </summary>
        public FileInfo SoundNo
        {
            get { return soundNo; }
            set { soundNo = value; }
        }
        /// <summary>
        /// время работы упражнения Sprint
        /// </summary>
        public int CountTimeWork
        {
            get { return countTimeWork; }
            set { countTimeWork = value; }
        }
        /// <summary>
        /// кол-во слов для тренировки
        /// </summary>
        public int CountWordTrenings
        {
            get { return countWordTrenings; }
            set { countWordTrenings = value; }
        }
        /// <summary>
        /// кол-во слов в упражнении Спринт
        /// </summary>
        public int CountWordSprint
        {
            get { return countWordSprint; }
            set { countWordSprint = value; }
        }









    }
}
