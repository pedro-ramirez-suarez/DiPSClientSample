using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCount
{
    public class SearchWordResult
    {
        public string Word { get; set; }
        public string BookName { get; set; }
        public int LineNumber { get; set; }
        public string Line { get; set; }
    }
}
