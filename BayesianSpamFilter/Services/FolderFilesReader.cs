using BayesianSpamFilter.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BayesianSpamFilter.Services
{
    public class FolderFilesReader : IFilesReader
    {
        private readonly string path;

        public FolderFilesReader(string path)
        {
            this.path = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + path;
        }

        public int CountFiles()
        {
            if (Directory.Exists(path))
            {
                return Directory.GetFiles(path).Length;
            }
            return 0;
        }

        public IList<TextFile> GetFiles()
        {
            IList<TextFile> files = new List<TextFile>();
            if (Directory.Exists(path))
            {
                var directoryFiles = Directory.GetFiles(path);
                StringBuilder sb = new StringBuilder();
                foreach (string fileName in directoryFiles)
                {
                    var file = File.ReadAllLines(fileName);
                    files.Add(new TextFile() { Lines = file });
                }
            }
            return files;
        }
    }
}
