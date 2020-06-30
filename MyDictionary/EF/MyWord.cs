using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyDictionary.EF
{
    [Table("Words")]
    public class MyWord
    {
        [Key]
        public int WordId { get; set; }
        [StringLength(50)]
        public string Word { get; set; }
        [StringLength(100)]
        public string SoundName { get; set; }
        [StringLength(50)]
        public string PartOfSpeach { get; set; }
        [StringLength(50)]
        public string Transcription { get; set; }
        public ICollection<MyTranslate> MyTranslates { get; set; }
        public ICollection<MyExample> MyExamples { get; set; }
        private string translateStr;

        public string TranslateStr
        {
            get
            {
                foreach (MyTranslate transl in MyTranslates)
                {
                    translateStr += transl.Translate + ", ";
                }
                return translateStr;
            }

        }

    }
}
