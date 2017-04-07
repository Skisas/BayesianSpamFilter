using System;
using System.Collections.Generic;
using System.Text;

namespace BayesianSpamFilter.Models
{
    public class Word
    {
        public string Name { get; set; }
        public int SpamCount { get; set; }
        public int NotSpamCount { get; set; }

        public double SpamRate { get; set; }
        public double NotSpamRate { get; set; }
        public double TotalProbability { get; set; }

        public Word(String name)
        {
            this.Name = name;
            SpamCount = 0;
            NotSpamCount = 0;
            SpamRate = 0.0;
            NotSpamRate = 0.0;
            TotalProbability = 0.0;
        }

        public void calculateProbability(int totSpam, int totHam)
        {
            SpamRate = SpamCount / Convert.ToDouble(totSpam);
            NotSpamRate = NotSpamCount / Convert.ToDouble(totHam);
            if(!(NotSpamRate > 0))
            {
                NotSpamRate = 0;
            }
            if (SpamRate + NotSpamRate > 0)
            {
                TotalProbability = SpamRate / (SpamRate + NotSpamRate);
            }

            if (TotalProbability < 0.01)
            {
                TotalProbability = 0.01;
            }
            else if (TotalProbability > 0.99)
            {
                TotalProbability = 0.99;
            }
        }
    }
}
