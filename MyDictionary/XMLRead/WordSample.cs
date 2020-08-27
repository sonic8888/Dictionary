using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDictionary.XMLRead
{
    public class WordSample : IComparable
    {
        public string Word { get; set; }
        public string PartOfSpeach { get; set; }
        public string Transcription { get; set; }
        public string SoundName { get; set; }
        private ObservableCollection<string> translate;

        public ObservableCollection<string> Translate
        {
            get { return translate; }
            set { translate = value; }
        }
        public WordSample()
        {
            translate = new ObservableCollection<string>();
            Example = new ObservableCollection<string>();
        }

        public WordSample(string word, string partOfSpeach, string transcription, string soundName, ObservableCollection<string> translate, ObservableCollection<string> example,DateTime insert,
          DateTime lastcall,int state )
        {
            Word = word;
            PartOfSpeach = partOfSpeach;
            Transcription = transcription;
            SoundName = soundName;
            this.translate = translate;
            Example = example;
            InitTranslateLine();
            DateTimeInsert = insert;
            DateTimeLastCall = lastcall;
            State = state;

        }
        public WordSample(string word, string partOfSpeach, string transcription, string soundName, ObservableCollection<string> translate, ObservableCollection<string> example)  
   
        {
            Word = word;
            PartOfSpeach = partOfSpeach;
            Transcription = transcription;
            SoundName = soundName;
            this.translate = translate;
            Example = example;
            InitTranslateLine();
          

        }
        private string translateLine;

        public string TranslateLine
        {
            get { return translateLine; }

        }


        public ObservableCollection<string> Example { get; set; }
        public void InitTranslateLine()
        {

            if (translate != null)
            {
                foreach (string str in translate)
                {
                    translateLine += str + ", ";
                }

            }

        }

        public int CompareTo(object obj)
        {
            WordSample ws = obj as WordSample;
            if (ws != null)
            {
                return this.Word.CompareTo(ws.Word);
            }
            else

                throw new Exception("Невозможно сравнить два обьекта");

        }
        private int state;

        public int State
        {
            get { return state; }
            set { state = value; }
        }
        private DateTime datetimeinsert;

        public DateTime DateTimeInsert
        {
            get { return datetimeinsert; }
            set { datetimeinsert = value; }
        }
        private DateTime datatimelastcall;

        public DateTime DateTimeLastCall
        {
            get { return datatimelastcall; }
            set { datatimelastcall = value; }
        }



    }
}
