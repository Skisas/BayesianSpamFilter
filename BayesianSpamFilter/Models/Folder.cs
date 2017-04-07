using System;
using System.Collections.Generic;
using System.Text;

namespace BayesianSpamFilter.Models
{
    public class Folder
    {
        public IList<TextFile> Files { get; set; }
        public int Size => Files.Count;
    }
}
