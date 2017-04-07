using System;
using System.Collections.Generic;
using System.Text;

namespace BayesianSpamFilter.Base
{
    public interface IFilesReader
    {
        IList<TextFile> GetFiles();
        int CountFiles();
    }
}
