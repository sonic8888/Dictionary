using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MyDictionary.EF;

using MyDictionary.XMLRead;

namespace MyDictionary.Tools
{
    public static class BdTools
    {

        public static int AddNewWord(string word, DateTime insert, DateTime lastCall, string soundname = "", string partofspeach = "", string transcription = "", int state = 1)
        {
            using (var context = new ApplicationContext())
            {
                try
                {
                    var words = new MyWord() { Word = word, SoundName = soundname, PartOfSpeach = partofspeach, Transcription = transcription, State = state, DataTimeInsert = insert, DataTimeLastCall = lastCall };
                    context.MyWords.Add(words);
                    context.SaveChanges();
                    return words.WordId;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return 0;
                }

            }

        }
        public static int AddNewTranslate(string str, int wordid)
        {
            using (var context = new ApplicationContext())
            {
                try
                {
                    var tr = new MyTranslate() { Translate = str, WordId = wordid };
                    context.MyTranslates.Add(tr);
                    context.SaveChanges();
                    return tr.TranslateId;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + " " + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return 0;
                }

            }
        }
        public static int AddNewExamle(string str, int wordid)
        {
            using (var context = new ApplicationContext())
            {
                try
                {
                    var tr = new MyExample() { Example = str, WordId = wordid };
                    context.MyExamples.Add(tr);
                    context.SaveChanges();
                    return tr.ExampleId;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + " " + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return 0;
                }

            }
        }


        public static int AddNewWords(WordSample wordSample)
        {
            int index = AddNewWord(wordSample.Word, wordSample.DateTimeInsert, wordSample.DateTimeLastCall, wordSample.SoundName, wordSample.PartOfSpeach, wordSample.Transcription);
            if (index > 0)
            {
                foreach (string trans in wordSample.Translate)
                {
                    AddNewTranslate(trans, index);
                }


                foreach (string item in wordSample.Example)
                {
                    AddNewExamle(item, index);
                }

            }
            return index;
        }
        public static MyWord DeleteWord(int id)
        {
            using (var context = new ApplicationContext())
            {
                try
                {
                    MyWord mw = context.MyWords.Find(id);
                    if (mw != null)
                    {
                        context.MyWords.Remove(mw);
                        context.SaveChanges();
                    }
                    return mw;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + " " + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;


                }

            }
        }
        public static ObservableCollection<MyWord> ReadWord()
        {
            ObservableCollection<MyWord> myWords = new ObservableCollection<MyWord>();
            using (var context = new ApplicationContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                try
                {
                    foreach (MyWord c in context.MyWords)
                    {
                        context.Entry(c).Collection(x => x.MyTranslates).Load();
                        context.Entry(c).Collection(x => x.MyExamples).Load();
                        myWords.Add(c);
                    }
                    context.Dispose();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + " " + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
                return myWords;
            }
        }
        public static ObservableCollection<MyWord> ReadWord(int take)
        {
            ObservableCollection<MyWord> myWords = new ObservableCollection<MyWord>();
            using (var context = new ApplicationContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                try
                {
                    foreach (MyWord c in context.MyWords.Take(take))
                    {
                        context.Entry(c).Collection(x => x.MyTranslates).Load();
                        context.Entry(c).Collection(x => x.MyExamples).Load();
                        myWords.Add(c);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + " " + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
                return myWords;
            }
        }

        public static MyWord FindMyWord(int id)
        {
            MyWord mw = null;
            using (var context = new ApplicationContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                try
                {
                    mw = context.MyWords.Find(id);
                    context.Entry(mw).Collection(x => x.MyTranslates).Load();
                    context.Entry(mw).Collection(x => x.MyExamples).Load();
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + " " + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }
            return mw;
        }
        public static MyWord UpdateStateMyWord(int id, int state)
        {
            MyWord mw = null;
            using (var context = new ApplicationContext())
            {
                try
                {
                    mw = context.MyWords.Find(id);
                    mw.State = state;
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + " " + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }
            return mw;
        }
        public static MyWord UpdateStateMyWord(MyWord mw, State state)
        {
            return UpdateStateMyWord(mw.WordId, (int)state);
        }
        public static ObservableCollection<MyWord> SelecWhereState(State state)
        {
            ObservableCollection<MyWord> myWords = new ObservableCollection<MyWord>();
            using (var context = new ApplicationContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                try
                {
                    foreach (var item in context.MyWords.Where(n => n.State == (int)state))
                    {
                        context.Entry(item).Collection(x => x.MyTranslates).Load();
                        context.Entry(item).Collection(x => x.MyExamples).Load();
                        myWords.Add(item);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " " + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }
            return myWords;
        }
        public static List<int> GetWordId()
        {
            using (var context = new ApplicationContext())
            {
                List<int> wordid;
                try
                {
                    wordid = new List<int>();
                    foreach (MyWord mw in context.MyWords)
                    {
                        wordid.Add(mw.WordId);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " " + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
                return wordid;
            }
        }
        /// <summary>
        /// выбирает слова которые не входя в список
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<MyWord> GetListMyWord(List<MyWord> list)
        {
            List<MyWord> myWords;

            IEnumerable<int> enumerable = list.Select(n => n.WordId);

            using (var context = new ApplicationContext())
            {
                try
                {
                    myWords = new List<MyWord>();
                    foreach (MyWord mw in context.MyWords)
                    {
                        if (!enumerable.Contains(mw.WordId))
                        {
                            myWords.Add(mw);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " " + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
                return myWords;
            }

        }
        /// <summary>
        /// выбирает слова которые не входя в список длинной lenght
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<MyWord> GetListMyWord(List<MyWord> list, int lenght)
        {
            List<MyWord> myWords;

            IEnumerable<int> enumerable = list.Select(n => n.WordId);

            using (var context = new ApplicationContext())
            {
                try
                {
                    myWords = new List<MyWord>();
                    foreach (MyWord mw in context.MyWords)
                    {
                        if (!enumerable.Contains(mw.WordId))
                        {

                            myWords.Add(mw);
                        }
                        if (myWords.Count >= lenght)
                        {
                            return myWords;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " " + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
                return myWords;
            }

        }
        public static List<string> GetListTranslate(List<MyWord> list)
        {
            List<string> translate;
            IEnumerable<int> enumerable = list.Select(n => n.WordId);
            using (var context = new ApplicationContext())
            {
                try
                {
                    translate = new List<string>();
                    foreach (MyTranslate mt in context.MyTranslates)
                    {
                        if (!enumerable.Contains(mt.WordId))
                        {
                            translate.Add(mt.Translate);
                        }
                    }
                  
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " " + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
                return translate;
            }

        }
        public static List<MyWord> GetRandomListMyWord(int count)
        {
            List<int> listId = GetWordId();
            if (listId.Count<=count)
            {
                MessageBox.Show("Кол-во выбираемых слов превышает кол-во слов в БД " + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            List<int> listRandomId = MyTools.GetRandomInt(listId, count);
            List<MyWord> listRandom = new List<MyWord>();
            using (var context = new ApplicationContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                try
                {
                    for (int i = 0; i < listRandomId.Count; i++)
                    {
                        MyWord myWord = context.MyWords.Find(listRandomId[i]);
                        context.Entry(myWord).Collection(x => x.MyTranslates).Load();
                        context.Entry(myWord).Collection(x => x.MyExamples).Load();
                        listRandom.Add(myWord);
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + " " + MethodBase.GetCurrentMethod().DeclaringType.FullName, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }

            }
            return listRandom;
        }


    }
}
