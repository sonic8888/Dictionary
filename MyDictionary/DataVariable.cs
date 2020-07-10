using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using XMLRead;
using System.Windows;

namespace MyDictionary
{
    public class DataVariable
    {
        private List<string> defoltValueList;
        private int countWordLearning;
        public DataVariable()
        {
            if (!File.Exists(FIleTools.NameFileDataVariable))
            {
                defoltValueList = new List<string>();
                defoltValueList.Add("5");// defoltValue countWordLearning
                StreamWriter streamWriter;
                using (streamWriter= File.CreateText(FIleTools.NameFileDataVariable))
                {
                    foreach (string item in defoltValueList)
                    {
                        streamWriter.WriteLine(item);
                    }
                }
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
                    while ((input=sr.ReadLine())!=null)
                    {
                        listValue.Add(input);
                    }
                    countWordLearning = int.Parse(listValue[0]);//читаем countWordLearning
                }

            }
            catch (IOException ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        public int CountWordLearning
        {
            get { return countWordLearning; }
            set { countWordLearning = value; }
        }

        public void WriteFile()
        {
            try
            {
                using (StreamWriter sw=new StreamWriter(FIleTools.NameFileDataVariable))
                {
                    sw.WriteLine(countWordLearning);
                }
            }
            catch (IOException ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
