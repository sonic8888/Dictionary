using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
