using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDictionary.EF
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {

        }
        public virtual DbSet<MyWord> MyWords { get; set; }
        public virtual DbSet<MyTranslate> MyTranslates { get; set; }
        public virtual   DbSet<MyExample> MyExamples { get; set; }
        public virtual   DbSet<MyDataWord> MyDataWords { get; set; }
        public virtual   DbSet<MyState> MyStates { get; set; }
    }
}
