using MyDictionary.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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
        public static List<int> GetRandomInt(List<int> sourse, int count)
        {
            if (sourse.Count < count)
            {
                MessageBox.Show(MethodBase.GetCurrentMethod().DeclaringType.FullName);
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
        public static List<string> GetRandomString(List<string> sourse, int count)
        {
            if (sourse.Count < count)
            {
                MessageBox.Show(MethodBase.GetCurrentMethod().DeclaringType.FullName);
                return null;
            }
            List<string> listRandom = new List<string>();
            for (int i = 0; i < count; i++)
            {
                int index = App.random.Next(sourse.Count);
                string str = sourse[index];
                listRandom.Add(str);
                if (!sourse.Remove(str))
                {
                    break;
                }

            }
            return listRandom;
        }
        public static List<MyWord> GetRandomListMyWord(List<MyWord> sourse, int count)
        {
            if (sourse.Count < count)
            {
                MessageBox.Show(MethodBase.GetCurrentMethod().DeclaringType.FullName);
                return null;
            }
            List<MyWord> listRandom = new List<MyWord>();
            for (int i = 0; i < count; i++)
            {
                int index = App.random.Next(sourse.Count);
                MyWord mw = sourse[index];
                listRandom.Add(mw);
                if (!sourse.Remove(mw))
                {
                    break;
                }
            }
            return listRandom;
        }
        public static BitmapImage CreateBitmapImage(string path)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(path, UriKind.Relative);
            bi.EndInit();
            return bi;
        }
    }
}
