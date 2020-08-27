using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyDictionary.XMLRead
{
    public class TranslateClass : IComparable, IEquatable<TranslateClass>, INotifyPropertyChanged
    {
        string translate;
        int meaning;
        int partOfSpeach;
        string partofspeach;

        public event PropertyChangedEventHandler PropertyChanged;

        public TranslateClass(string translate, int meaning, int partofspeach)
        {
            this.translate = translate;
            if (partofspeach == 0)
            {
                partofspeach = 4;
            }
            this.partOfSpeach = partofspeach;
            this.meaning = meaning;
            init();
        }
        public TranslateClass(string translate, string partofspeach)
        {
            this.translate = translate;
            if (partofspeach == "друг")
            {
                partofspeach = "";
            }
            Partofspeach = partofspeach;

            initPart();
        }

        public string Translate { get => translate; set { translate = value; OnPropertyChanged("Translate"); } }
        public int Meaning { get => meaning; }
        public int PartOfSpeach { get => partOfSpeach; }
        public string Partofspeach { get => partofspeach; set { partofspeach = value; OnPropertyChanged("Partofspeach"); } }

        public int CompareTo(object obj)
        {
            if (obj != null)
            {
                TranslateClass tc = obj as TranslateClass;
                return this.PartOfSpeach.CompareTo(tc.PartOfSpeach);
            }
            else
            {
                throw new ArgumentException("Object is not TranslateClass");
            }
        }

        public bool Equals(TranslateClass other)
        {
            if (Object.ReferenceEquals(other, null)) return false;
            if (Object.ReferenceEquals(this, other)) return true;
            return Translate.Equals(other.Translate);
        }

        public override string ToString()
        {
            return translate;
        }
        public override int GetHashCode()
        {
            return Translate.GetHashCode();
        }
        private void init()
        {
            string str = "";
            switch (partOfSpeach)
            {
                case 1: str = "сущ"; break;
                case 2: str = "прил"; break;
                case 3: str = "глаг"; break;
                default:
                    break;
            }
            partofspeach = str;
        }
        private void initPart()
        {
            int p = 4;
            switch (partofspeach)
            {
                case "сущ": p = 1; break;
                case "прил": p = 2; break;
                case "глаг": p = 3; break;
                default:
                    break;
            }
            partOfSpeach = p;

        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
