using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyDictionary.XMLRead
{/// <summary>
/// значение слова
/// </summary>
   public class Meaning
    {
       /// <summary>
       /// часть речи
       /// </summary>
        public string partOfSpeech { get; set; }
        /// <summary>
        /// имя аудиофайла
        /// </summary>
        public string soundName { get; set; }
        /// <summary>
        /// транскрипция слова
        /// </summary>
        public string transcription { get; set; }
        /// <summary>
        /// коллеция переводов слова
        /// </summary>
        public IEnumerable<XElement> Translations { get; set; }
        /// <summary>
        /// колекция примеров употребления слова
        /// </summary>
        public IEnumerable<XElement> Examples { get; set; }

        
       
        public override string ToString()
        {
            return $"partOfSpeech:{partOfSpeech},soundName {soundName}";
        }
    }
}
