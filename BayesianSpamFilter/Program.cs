using BayesianSpamFilter.Base;
using BayesianSpamFilter.Models;
using BayesianSpamFilter.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace BayesianSpamFilter
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {
            ConfigureEnvironmentVariables();

            var settings = new FolderSettings();
            Configuration.Bind(settings);
            IFilesReader spamReader = new FolderFilesReader(settings.SpamFolder);
            IFilesReader notSpamReader = new FolderFilesReader(settings.NotSpamFolder);
            Folder spam = new Folder() { Files = spamReader.GetFiles() };
            Folder notSpam = new Folder() { Files = notSpamReader.GetFiles() };
            WordsTable words = new WordsTable();
            words.PopulateSpam(spam);
            words.PopulateNotSpam(notSpam);
            BayesianFilter filter = new BayesianFilter(words);
            filter.RecalculateProb();
            var biggest = filter.findBiggest();
            var spamas = new List<string>();
            //spamas.Add("ka");
            spamas.Add("a");
            //spamas.Add("off");
            var isSpam = filter.CheckIfSpam(spamas);
            Console.WriteLine("Tekstas: ");
            foreach(var word in spamas)
            {
                Console.Write(word + " ");
            }
            Console.WriteLine("\nTikimybe: " + isSpam);
            Console.WriteLine("Ar spamas:" + (isSpam > 0.5));
            var i = 2;
        }

        private static void ConfigureEnvironmentVariables()
        {
            var builder = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile("settings.json");
            Configuration = builder.Build();        
        }
    }
}