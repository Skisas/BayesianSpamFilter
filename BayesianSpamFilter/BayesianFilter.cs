using BayesianSpamFilter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BayesianSpamFilter
{
    public class BayesianFilter
    {
        private readonly WordsTable table;
        private Dictionary<string, Word> probabilities;

        public BayesianFilter(WordsTable table)
        {
            this.table = table;
            this.probabilities = new Dictionary<string, Word>();
            foreach(var word in table.Words.Values)
            {
                probabilities.Add(word.Name, word);
            }
        }

        public void RecalculateProb()
        {
            var i = 0;
            foreach (var word in table.Words.Values)
            {
                word.calculateProbability(table.SpamCount, table.NotSpamCount);
                i = 2;
            }
        }

        public List<Word> findBiggest()
        {
            return this.table.Words.Values.OrderBy(x=>x.TotalProbability).ToList();
        }

        public double CheckIfSpam(ICollection<string> file)
        {
            var probabilitySpam = 1.0;
            var probabilitiesInverse = 1.0;
            foreach(var word in file)
            {
                if (probabilities.ContainsKey(word))
                {
                    probabilitySpam *= probabilities[word].TotalProbability;
                    probabilitiesInverse *= (1 - probabilities[word].TotalProbability);
                } else {
                    probabilitySpam *= 0.4;
                    probabilitiesInverse *= 0.6;
                }
            }
            var probabilitiesSum = probabilitySpam / (probabilitySpam + probabilitiesInverse);
            return probabilitiesSum;
        }
    }
}
