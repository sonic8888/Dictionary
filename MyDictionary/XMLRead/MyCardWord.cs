using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyDictionary.XMLRead
{
    /// <summary>
    /// Класс служит для заполнения значениями окна CardWord
    /// и последующим сохранения в БД
    /// </summary>
    public class MyCardWord
    {
        Card card;
        string word;
        //string translates;
        //string examples;
        string transcription;
        List<TranslateClass> translateList;
        List<ExampleClass> exampleList;
        List<string> partOfSpeachList;
        List<Card> listCard;
        List<string> listTranslateAndPartOfSpeach;



        public MyCardWord()
        {

        }
      
        public MyCardWord(List<Card> listCard)
        {
            this.listCard = listCard;
            InitList();
        }


        public Card Card { get => card; set => card = value; }
        public string SoundName { get; set; }










        public string Word { get => word; set => word = value; }
        //public string Translates { get => translates; set => translates = value; }

        //public string Examples { get => examples; set => examples = value; }
        public string Transcription { get => transcription; set => transcription = value; }
        public List<TranslateClass> TranslateList { get => translateList; set => translateList = value; }
        public List<ExampleClass> ExampleList { get => exampleList; set => exampleList = value; }
        public List<string> PartOfSpeachList { get => partOfSpeachList; set => partOfSpeachList = value; }
        public List<string> ListTranslateAndPartOfSpeach { get => listTranslateAndPartOfSpeach; set => listTranslateAndPartOfSpeach = value; }

 
        private void InitList()
        {
            if (listCard == null)
                return;
            word = listCard[0].word;
            SoundName = word + ".wav";
            translateList = new List<TranslateClass>();
            exampleList = new List<ExampleClass>();
            partOfSpeachList = new List<string>();
            transcription = listCard[0].ListMeanings.ToArray()[0].transcription;
            foreach (Card card in listCard)
            {
                int meaning = 1;
                for (int i = 0; i < card.CountMeaning; i++)
                {
                    Meaning m = card.ListMeanings[i];
                    PartOfSpeachList.Add(m.partOfSpeech);
                    int partofspeach = 0;
                    int.TryParse(m.partOfSpeech, out partofspeach);
                    foreach (XElement trans in m.Translations)
                    {

                        translateList.Add(new TranslateClass(trans.Value, meaning, partofspeach));
                    }

                    if (m.Examples != null)
                    {
                        foreach (XElement examp in m.Examples)
                        {
                            exampleList.Add(new ExampleClass(examp.Value, meaning));
                        }
                    }
                    meaning++;
                }
            }
            IEnumerable<TranslateClass> enumerable = translateList.Distinct<TranslateClass>();
            translateList = new List<TranslateClass>(enumerable);
            translateList.Sort();
            translateObservable = new ObservableCollection<TranslateClass>(translateList);
            exampleObservable = new ObservableCollection<ExampleClass>(exampleList);


        }
        private ObservableCollection<TranslateClass> translateObservable;

        public ObservableCollection<TranslateClass> TranslateObservable
        {
            get { return translateObservable; }
            set { translateObservable = value; }
        }
        private ObservableCollection<ExampleClass> exampleObservable;

        public ObservableCollection<ExampleClass> ExampleObservable
        {
            get { return exampleObservable; }
            set { exampleObservable = value; }
        }

        public List<WordSample> GetListWordSample()
        {
            List<WordSample> list = new List<WordSample>();
            if (listCard!=null)
            {
                foreach (Card card in listCard)
                {
                    list.AddRange(card.GetWordSample());
                }
            }
            return list;
        }



    }

}
