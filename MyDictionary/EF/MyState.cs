using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDictionary.EF
{
    public enum StateWord
    {
        Start = 1, Teach = 2, Finish = 2
    }
    [Table("StateWords")]
    public class MyState
    {
        private int state;
        [Key]
        public int Id { get; set; }
        public int WordId { get; set; }

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

    }
}
