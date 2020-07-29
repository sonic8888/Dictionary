using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyDictionary.Tools
{
    public static class MyTools
    {
        public static string WrapTranscription(string transcription)
        {
            return "[" + transcription + "]";
        }
        public static string ExampleSpace(string example)
        {
            return example.Replace("--", " – ");
        }
        public static List<int> GetRandom(List<int> sourse, int count)
        {
            if (sourse.Count < count)
            {
                MessageBox.Show("Кол-во выбраных элементов больше длины колекции");
                return null;
            }
            List<int> listRandom = new List<int>();
            for (int i = 0; i < count; i++)
            {
                int index = App.random.Next(sourse.Count);
                int id = sourse[index];
                listRandom.Add(id);
                if (!sourse.Remove(id))
                {
                    break;
                }
            }
            return listRandom;
        }
    }
}
