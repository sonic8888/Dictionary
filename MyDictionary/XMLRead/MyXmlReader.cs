using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace MyDictionary.XMLRead
{
    public class MyXmlReader
    {
        /// <summary>
        /// список xml файлов словаря
        /// </summary>
        FileInfo[] fileNames;

        public MyXmlReader(FileInfo[] fileNames)
        {
            this.fileNames = fileNames;
        }


        /// <summary>
        /// возвращает Card
        /// </summary>
        /// <param name="wordName"></param>
        /// <returns></returns>
        public Card FindWord(string wordName)
        {
            Card card = null;

            foreach (FileInfo file in fileNames)
            {
                XDocument xdoc = XDocument.Load(file.FullName);

                foreach (XElement c in xdoc.Element("dictionary").Elements("card"))
                {
                    XElement word = c.Element("word");
                    if (word.Value.ToLower() == wordName.ToLower())
                    {

                        card = new Card(word.Value, c.Element("meanings").Elements("meaning"));
                        return card;
                    }
                }
            }
            return card;
        }
        /// <summary>
        /// возвращает колекцию Card по первым буквам
        /// </summary>
        /// <param name="partOfWord"></param>
        /// <returns></returns>
        public List<Card> FindWords(string partOfWord)
        {
            List<Card> cardsList = new List<Card>();
            foreach (FileInfo file in fileNames)
            {
                XDocument xdoc = XDocument.Load(file.FullName);
                Card card = null;
                foreach (XElement c in xdoc.Element("dictionary").Elements("card"))
                {
                    XElement word = c.Element("word");
                    if (word.Value.ToLower().StartsWith(partOfWord.ToLower()))
                    {

                        card = new Card(word.Value, c.Element("meanings").Elements("meaning"));
                        cardsList.Add(card);
                    }
                }
            }
            cardsList.Sort();
            return cardsList;
        }
        public List<WordForList> FindWordForList(string partOfWord)
        {
            List<WordForList> cardsList = new List<WordForList>();
            foreach (FileInfo file in fileNames)
            {
                XDocument xdoc = XDocument.Load(file.FullName);
                foreach (XElement c in xdoc.Element("dictionary").Elements("card"))
                {
                    XElement word = c.Element("word");
                    if (word.Value.ToLower().StartsWith(partOfWord.ToLower()))
                    {
                        IEnumerable<XElement> meanings = c.Element("meanings").Elements("meaning");
                        foreach (XElement meaning in meanings)
                        {
                            string partOfSpeach = "";
                            if (meaning.HasAttributes)
                            {
                                partOfSpeach = GetPartOfSpeach(meaning.FirstAttribute.Value);
                            }
                            IEnumerable<XElement> wordsTranslate = meaning.Element("translations").Elements("word");
                            string translate = GetTranslate(wordsTranslate);
                            cardsList.Add(new WordForList() { Word = word.Value, PartOfSpeach = partOfSpeach, Translite = translate });

                        }
                    }
                }
            }
            cardsList.Sort();
            return cardsList;
        }
        /// <summary>
        /// обозначаем часть речи
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        private string GetPartOfSpeach(string st)
        {
            string partOfSpeach = "";
            switch (st)
            {
                case "1": partOfSpeach = "Существительное"; break;
                case "2": partOfSpeach = "Прилагательное"; break;
                case "3": partOfSpeach = "Глагол"; break;

                default:
                    break;
            }
            return partOfSpeach;
        }
        /// <summary>
        /// формируем строку перевода англ. слова
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        private string GetTranslate(IEnumerable<XElement> elements)
        {
            string tranlate = "";
            foreach (XElement word in elements)
            {
                tranlate += word.Value + ", ";
            }
            return tranlate;
        }
        public IEnumerable<WordSample> FindWordsSample(string partOfWord)
        {
            List<Card> lCard = FindWords(partOfWord);
            IEnumerable<WordSample> samples = lCard.SelectMany(n => n.GetWordSample());
            return samples;
        }
    }

}
