using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDictionary.XMLRead
{
    public class MyCollection : ObservableCollection<WordForList>
    {
        public MyCollection()
        {
        }

        public MyCollection(List<WordForList> list) : base(list)
        {
        }

        public MyCollection(IEnumerable<WordForList> collection) : base(collection)
        {
        }

      
    }
}
