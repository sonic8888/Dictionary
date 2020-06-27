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
    public class MyTranslate
    {
        [Key]
        public int TranslateId { get; set; }

        public int WordId { get; set; }
        [StringLength(50)]
        public string Translate { get; set; }

        public MyWord MyWord { get; set; }
    }
}
