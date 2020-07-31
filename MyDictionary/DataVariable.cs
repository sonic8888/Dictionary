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
        private int countWordRepetition;// кол-во слов для упражнения повторения (Repetitio)
        private int countWordLearning;//кол-во слов для тренировки (Trenings)
        private int countSelectWord;//вспомогательная вывыбока для создания ресурса слов(Trenings)
        private int countMilisek;//время задержки таймера прогрессбара в WindowRepetition
        private int countMilisekDelay;//время задержки для смены слов в WindowRepetition
        public DataVariable()
        {
            if (!File.Exists(FIleTools.NameFileDataVariable))
            {
                defoltValueList = new List<string>();
                defoltValueList.Add("5");// defoltValue countWordLearning
                defoltValueList.Add("50");// defoltValue countSelectWord
                defoltValueList.Add("20");// defoltValue countWordRepetition
                defoltValueList.Add("10");// defoltValue countMilisek
                defoltValueList.Add("1000");// defoltValue countMilisekDelay
                StreamWriter streamWriter;
                using (streamWriter = File.CreateText(FIleTools.NameFileDataVariable))
                {
                    foreach (string item in defoltValueList)
                    {
                        streamWriter.WriteLine(item);
                    }
                }
            }
            else
            {

            }

            InitVariable();
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

                        countMilisekDelay = int.Parse(listValue[4]);//читаем countMilisekDelay
                        countMilisek = int.Parse(listValue[3]);//читаем countMilisek
                        countWordRepetition = int.Parse(listValue[2]);//читаем countWordRepetition
                        countSelectWord = int.Parse(listValue[1]);//читаем countSelectWord
                        countWordLearning = int.Parse(listValue[0]);//читаем countWordLearning

                    }
                    catch (ArgumentException aex)
                    {

                        MessageBox.Show(aex.Message + " " + MethodBase.GetCurrentMethod().DeclaringType.FullName);
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

        public int CountMilisekDelay
        {
            get { return countMilisekDelay; }
            set { countMilisekDelay = value; }
        }




    }
}
