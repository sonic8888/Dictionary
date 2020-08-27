using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyDictionary.XMLRead
{
    public class Card : IComparable
    {

        private int countMeaning;
        /// <summary>
        /// английское слово
        /// </summary>
        public string word { get; set; }
        /// <summary>
        /// колекция XElement значений слова
        /// </summary>
        IEnumerable<XElement> meanings;
        /// <summary>
        /// возвращает колекцию классов Meaning
        /// </summary>
        public List<Meaning> ListMeanings { get => listMeanings; }
        /// <summary>
        /// возврашет число значений перевода слова
        /// </summary>
        public int CountMeaning { get => countMeaning; }
        public IEnumerable<XElement> Meanings { get => meanings; }

        List<Meaning> listMeanings = new List<Meaning>();

        public Card(string word, IEnumerable<XElement> elements)
        {
            this.word = word;
            meanings = elements;
            InitElements();
            countMeaning = listMeanings.Count;
        }
        /// <summary>
        /// инициализирует   ListMeanings 
        /// </summary>
        private void InitElements()
        {
            if (meanings != null)
            {
                foreach (XElement meaning in meanings)
                {
                    IEnumerable<XAttribute> enumerable = meaning.Attributes();
                    XAttribute partOfSpeechAttribute = meaning.Attribute("partOfSpeech");
                    XAttribute soundNameAttribute = meaning.Element("sound")?.Attribute("name");
                    IEnumerable<XElement> translations = meaning.Element("translations").Elements("word");
                    IEnumerable<XElement> examples = meaning.Element("examples")?.Elements("example");
                    XAttribute transcriptionAtribute = meaning.Attribute("transcription");
                    ListMeanings.Add(new Meaning() { partOfSpeech = partOfSpeechAttribute?.Value, soundName = soundNameAttribute?.Value, Examples = examples, Translations = translations, transcription = transcriptionAtribute?.Value });

                }
            }
        }

        public int CompareTo(object obj)
        {
            Card card = obj as Card;
            if (card != null)
            {
                return this.word.CompareTo(card.word);
            }
            else
                throw new Exception("Невозможно сравнить два объекта");
        }
        public List<WordSample> GetWordSample()
        {
            List<WordSample> list = new List<WordSample>();
            if (ListMeanings != null)
            {
                foreach (Meaning mean in ListMeanings)
                {
                    ObservableCollection<string> colTranslate = new ObservableCollection<string>();
                    ObservableCollection<string> colExamples = new ObservableCollection<string>();
                    if (mean.Translations != null)
                    {
                        foreach (XElement item in mean.Translations)
                        {
                            colTranslate.Add(item?.Value);
                        }
                    }
                    if (mean.Examples != null)
                    {
                        foreach (XElement item in mean.Examples)
                        {
                            colExamples.Add(item?.Value);
                        }

                    }

                    list.Add(new WordSample(this.word, initPartOfSpeach(mean.partOfSpeech), mean.transcription, mean.soundName, colTranslate, colExamples));

                }

            }

            return list;
        }
        private string initPartOfSpeach(string part)
        {
            string str = "";
            switch (part)
            {
                case "1": str = "сущ"; break;
                case "2": str = "прил"; break;
                case "3": str = "глаг"; break;
                default:
                    break;
            }
            return str;
        }


    }
}
