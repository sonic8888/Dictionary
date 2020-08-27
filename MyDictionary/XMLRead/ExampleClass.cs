using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDictionary.XMLRead
{
    public class ExampleClass
    {
        string example;
        int meaning;
        string partOfSpeach;

        public ExampleClass(string example, int meaning)
        {
            this.example = example;
            this.meaning = meaning;
        }
        public ExampleClass(string example, string partOfSpeach)
        {
            this.example = example;
            this.partOfSpeach = partOfSpeach;
        }

        public string Example
        {
            get
            {
                return example.Replace("--", "—");
            }
            set
            {
                example = value;
            }
        }
        public int Meaning { get => meaning; }
        public string PartOfSpeach { get => partOfSpeach; set => partOfSpeach = value; }

        public override string ToString()
        {
            return Example;
        }
    }
}
