using System;
using System.Collections.Generic;
using System.Text;

namespace BayesianSpamFilter.Models
{
    public class WordsTable
    {
        public Dictionary<string, Word> Words { get; set; }
        public int SpamCount => CountSpam();
        public int NotSpamCount => CountNotSpam();
        public int TotalCount => SpamCount + NotSpamCount;

        public WordsTable()
        {
            Words = new Dictionary<string, Word>();
        }

        public void PopulateSpam(Folder spam)
        {
            this.PopulateTable(spam, true);
        }

        public void PopulateNotSpam(Folder notSpam)
        {
            this.PopulateTable(notSpam, false);
        }

        private void PopulateTable(Folder folder, bool isSpam)
        {
            foreach (var file in folder.Files)
            {
                foreach (var word in file.getWords())
                {
                    var exists = Words.ContainsKey(word.ToLower());
                    if (exists)
                    {
                        var dictWord = Words[word.ToLower()];
                        if (isSpam) {
                            dictWord.SpamCount++;
                        } else {
                            dictWord.NotSpamCount++;
                        }
                    } else {
                        var newWord = new Word(word.ToLower()) ;
                        if (isSpam)
                        {
                            newWord.SpamCount = 1;
                            newWord.NotSpamCount = 0;
                        } else {
                            newWord.SpamCount = 0;
                            newWord.NotSpamCount = 1;
                        }
                        Words.Add(word.ToLower(), newWord);
                    }               
                }
            }
        }

        private int CountSpam()
        {
            var spam = 0;
            foreach(var word in Words.Values)
            {
                spam = spam + word.SpamCount;
            }
            return spam;
        }

        private int CountNotSpam()
        {
            var notSpam = 0;
            foreach (var word in Words.Values)
            {
                notSpam = notSpam + word.NotSpamCount;
            }
            return notSpam;
        }
    }
}
