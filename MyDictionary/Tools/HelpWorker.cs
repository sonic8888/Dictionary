using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MyDictionary.Tools
{
    public class HelpWorker
    {
        private int size;

        public int Size
        {
            get { return size; }
            set { size = value; }
        }
        public HelpWorker(int size,ProgressBar progressBar)
        {
            this.size = size;
            progresbar = progressBar;
        }
        private ProgressBar  progresbar;

        public ProgressBar ProgressBar
        {
            get { return progresbar; }
            set { progresbar = value; }
        }

    }
}
