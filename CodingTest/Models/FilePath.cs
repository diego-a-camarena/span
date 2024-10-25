using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Models
{
    public class FilePath
    {
        public string InputPath { get; set; }
        public string OutputPath { get; set; }

        public string InputResource => string.IsNullOrWhiteSpace(InputPath) ? $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}" : InputPath;
        public string OuputResource => string.IsNullOrWhiteSpace(OutputPath) ? $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}" : OutputPath;      
    }
}