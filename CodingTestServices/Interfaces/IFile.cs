using System;
using System.Collections.Generic;


namespace CodingChallengeServices.Interfaces
{
    public interface IFile
    {
        List<String> ReadFileResults(string FilePath);
        void WriteFile(string FilePath,List<string> standings);
    }
}
