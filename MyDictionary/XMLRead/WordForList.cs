using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyDictionary.XMLRead
{
    public class WordForList : IComparable, INotifyPropertyChanged
    {
        private string word;
        private string translite;
        private string partOfSpeach;

        public string Word { get => word; set { word = value; OnPropertyChanged("Word"); } }
        public string Translite { get => translite; set { translite = value; OnPropertyChanged("Translite"); } }
        public string PartOfSpeach { get => partOfSpeach; set { partOfSpeach = value; OnPropertyChanged("PartOfSpeach"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public int CompareTo(object obj)
        {
            WordForList wl = obj as WordForList;
            if (wl != null)
            {
                return this.Word.CompareTo(wl.Word);
            }
            else
            {
                throw new Exception("Невозможно сравнить два объекта");
            }
        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public override string ToString()
        {
            return $"{Word} - {PartOfSpeach}) {Translite}";
        }
    }
}
