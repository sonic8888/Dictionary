using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDictionary.XMLRead
{
    public class MyCollectionTranslateClass : ObservableCollection<TranslateClass>
    {
        public MyCollectionTranslateClass()
        {
        }

        public MyCollectionTranslateClass(List<TranslateClass> list) : base(list)
        {
        }

        public MyCollectionTranslateClass(IEnumerable<TranslateClass> collection) : base(collection)
        {
        }

        public List<TranslateClass> GetCollection()
        {
            return this.ToList<TranslateClass>();
        }
    }
}
