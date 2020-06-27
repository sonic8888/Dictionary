using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDictionary.EF
{
    [Table("Examples")]
    public class MyExample
    {
        [Key]
        public int ExampleId { get; set; }

        public int WordId { get; set; }
        [StringLength(200)]
        public string Example { get; set; }
        public MyWord MyWord { get; set; }
    }
}
