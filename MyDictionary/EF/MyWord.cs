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
    public class MyWord : IComparable, IEquatable<MyWord>
    {
        private string translateStr = "";
        private int state;
        private string dataInsert;
        private string dataLastCall;
        private DateTime dataTimeInsert;
        private DateTime dateTimeLastCall;
        private int countAnswer;
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
        public int State
        {
            get { return state; }
            set
            {
                state = value;
                if (state < 1 || state > 3)
                {
                    state = 1;
                }
            }
        }
        public string DataInsert
        {
            get { return dataInsert; }
            set
            {
                dataInsert = value;
                dataTimeInsert = DateTime.Parse(dataInsert);
            }
        }
        [StringLength(50)]
        public string DataLastCall
        {
            get { return dataLastCall; }
            set
            {
                dataLastCall = value;
                dateTimeLastCall = DateTime.Parse(dataLastCall);
            }
        }
        public ICollection<MyTranslate> MyTranslates { get; set; }
        public ICollection<MyExample> MyExamples { get; set; }

        [NotMapped]
        public DateTime DataTimeInsert
        {
            get { return dataTimeInsert; }
            set
            {
                dataTimeInsert = value;
                dataInsert = dataTimeInsert.ToString();
            }
        }
        [NotMapped]
        public DateTime DataTimeLastCall
        {
            get { return dateTimeLastCall; }
            set
            {
                dateTimeLastCall = value;
                dataLastCall = dateTimeLastCall.ToString();
            }

        }

        [NotMapped]
        public string TranslateStr
        {
            get
            {
                if (translateStr == "")
                {
                    foreach (MyTranslate transl in MyTranslates)
                    {
                        translateStr += transl.Translate + ", ";
                    }

                }
                return translateStr;
            }

        }

        [NotMapped]
        public int CountAnswer
        {
            get { return countAnswer; }
            set { countAnswer = value; }
        }

        public int CompareTo(object obj)
        {
            MyWord mw = obj as MyWord;
            if (mw == null)
            {
                throw new Exception("сравнение не возможно");
            }
            return this.WordId.CompareTo(mw.WordId);
        }

        public bool Equals(MyWord other)
        {
            if (other is null)
                return false;
            return this.WordId.Equals(other.WordId);
        }
        public override bool Equals(object obj) => Equals(obj as MyWord);
        public override int GetHashCode() => this.WordId;
    }
}
