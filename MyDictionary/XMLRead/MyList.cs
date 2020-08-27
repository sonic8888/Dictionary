using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDictionary.XMLRead
{
    class MyList : List<TranslateClass>
    {
        public MyList()
        {
        }

        public MyList(int capacity) : base(capacity)
        {
        }

        public MyList(IEnumerable<TranslateClass> collection) : base(collection)
        {
        }
    }
}
