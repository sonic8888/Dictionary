using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDictionary.EF
{
    [Table("Translates")]
    public class MyTranslate:IEquatable<MyTranslate>
    {
        [Key]
        public int TranslateId { get; set; }

        public int WordId { get; set; }
        [StringLength(50)]
        public string Translate { get; set; }

        public MyWord MyWord { get; set; }

        public bool Equals(MyTranslate other)
        {
          
            return this.Translate.Equals(other.Translate);
        }
        public override bool Equals(object obj) => Equals(obj as MyTranslate);


        public override int GetHashCode()
        {
            int translatehascod = Translate.GetHashCode();
            int wordidhascode = WordId.GetHashCode();
            return wordidhascode ^ translatehascod;
        }
    }
}
