using CodingChallengeServices.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodingChallengeServices.Services
{
    public class FileService : IFile
    {
        public List<string> ReadFileResults(string FilePath)
        {
            if (!File.Exists(FilePath))
                throw new ArgumentException("File does not exist");

           return System.IO.File.ReadLines(FilePath).ToList();            
        }

        public void WriteFile(string FilePath,List<string> standings)
        {            
            System.IO.File.WriteAllLines(FilePath, standings);
        }
    }
}
