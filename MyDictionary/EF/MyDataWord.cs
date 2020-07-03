using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDictionary.EF
{
    [Table("DataWords")]
    public class MyDataWord
    {
        private string dataInsert;
        private string dataLastCall;
        private DateTime dataTimeInsert;
        private DateTime dateTimeLastCall;

        [Key]
        public int Id { get; set; }
        public int WordId { get; set; }
        [StringLength(50)]
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






    }
}
