using MyDictionary.EF;
using MyDictionary.Repetition;
using MyDictionary.Sprint;
using MyDictionary.Trenings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDictionary.Tools
{
    public static class WindowsManager
    {
        public static ChoseWords CreateChoseWords()
        {
            ChoseWords chw = new ChoseWords();
            chw.Show();
            return chw;
        }
        public static TotalDictionary CreateTotalDictionary()
        {
            TotalDictionary td = new TotalDictionary(App.collection);
            td.Show();
            return td;
        }
        public static WindowBreyShtorm CreateWindowBreyShtorm()
        {
            ObservableCollection<MyWord> collLearn = BdTools.SelecWhereState(State.Learn);
            ObservableCollection<MyWord> collNew = BdTools.SelecWhereState(State.New);
            foreach (MyWord m in collNew)
            {
                collLearn.Add(m);
            }

            WindowBreyShtorm wbs = new WindowBreyShtorm(collLearn);
            wbs.Show();
            return wbs;
        }
        public static WindowBreyShtorm CreateWindowBreyShtorm(List<MyWord> list)
        {

            WindowBreyShtorm wbs = new WindowBreyShtorm(new ObservableCollection<MyWord>(list));
            wbs.Show();
            return wbs;
        }
        public static WindowBreyShtorm_2 GreateWindowBreyShtorm_2(List<MyWord> trenings, List<MyWord> resurse, bool istrenings)
        {
            WindowBreyShtorm_2 wb2 = new WindowBreyShtorm_2(trenings, resurse, istrenings);
            wb2.Show();
            return wb2;
        }
        public static WindowBreyShtorm_3 CreateWindowBreyShtorm_3(List<MyWord> words, bool istrening)
        {
            WindowBreyShtorm_3 wb3 = new WindowBreyShtorm_3(words, istrening);
            wb3.Show();
            return wb3;
        }
        public static WindowBreyShtorm_4 CreateWindowBreyShtorm_4(List<MyWord> words)
        {
            WindowBreyShtorm_4 wb4 = new WindowBreyShtorm_4(words);
            wb4.Show();
            return wb4;
        }
        public static WindowBreyShtormResult CreatewindowBreyShtormResult(List<MyWord> words)
        {
            WindowBreyShtormResult wbr = new WindowBreyShtormResult(words);
            wbr.Show();
            return wbr;
        }
        public static WindowRepetition CreateWindowRepetition(List<MyWord> list)
        {
            WindowRepetition wr = new WindowRepetition(list);
            wr.Show();
            return wr;
        }
        public static WindowSprint CreateWindowSprint(List<MyWord> list)
        {
            WindowSprint wr = new WindowSprint(list);
            wr.Show();
            return wr;
        }



    }
}
