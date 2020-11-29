using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace MyDictionary.Tools
{
    public static class FileLogger
    {
        public static string Path = @"./log.txt";

        public static void WriteLine(string line, bool append = true)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(Path, append, Encoding.UTF8))
                {
                    sw.WriteLine(line);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return;
            }
        }
        public static void WriteLine(IEnumerable<string> col, bool append = true)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(Path, append, Encoding.UTF8))
                {
                    foreach (string item in col)
                    {
                        sw.WriteLine(item);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
