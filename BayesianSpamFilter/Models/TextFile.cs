using System;
using System.Collections.Generic;

namespace BayesianSpamFilter
{
    public class TextFile
    {
        public IList<string> Lines { get; set; }
        public int Size => Lines.Count;

        internal IEnumerable<string> getWords()
        {
            List<string> words = new List<string>();
            foreach(var line in Lines)
            {
                foreach(var word in line.Split(' ', ',', '.', '?', '!', '<', '>', '/', '\\', '-', ':'))
                {
                    words.Add(word);
                }
            }
            return words;
        }
    }
}