using CodingChallengeServices.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using CodingChallenge.Models;
using System.IO;
using CodingChallengeServices.Models;
using CodingChallenge.ExtensionMethods;
using System.Threading;

namespace CodingChallenge
{
    partial class Program
    {
        internal static IServiceProvider ServiceProvider { get; private set; }
        internal static IConfiguration Configuration { get; private set; }
        internal static IFile FileService { get; private set; }
        internal static ILeague LeagueService { get; private set; }        
        internal static FilePath FilePath { get; private set; }     

        private static void Main(string[] args)
        {
            StartUp();
            StartProcess();            
        }

        private static void StartProcess()
        {
            try
            {
                string filePath = String.IsNullOrWhiteSpace(FilePath.InputPath) ? System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) : FilePath.InputPath;
                string fileOutPath = String.IsNullOrWhiteSpace(FilePath.OutputPath) ? System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) : FilePath.OutputPath;
                Console.WriteLine(Directory.GetCurrentDirectory());
                Console.WriteLine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
                Console.WriteLine("Enter the file name with the extension:");
                string fileName = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(fileName))
                    throw new ArgumentException("Invalid name");

                //Read the file
                List<string> gamesResults = FileService.ReadFileResults(Path.Combine(filePath, fileName));

                //Normalize the results
                List<Match> matches = LeagueService.NormalizeData(gamesResults);

                //Build the Ranking list
                List<Ranking> ranking = LeagueService.GetRanking(matches);

                //write the results into a new file.
                Path.GetExtension(fileName);
                string fileResultPath = Path.Combine(fileOutPath, $"{Path.GetFileNameWithoutExtension(fileName)}_Results{Path.GetExtension(fileName)}");
                var results = ranking.ToStringRules();
                FileService.WriteFile(fileResultPath, results);

                Console.WriteLine($"\nthe result was stored in the following path:{fileResultPath}");
                Console.WriteLine("\nResults:\n");
                Console.WriteLine(string.Join("\n", results));

                Console.WriteLine(Directory.GetCurrentDirectory());
                Console.WriteLine("Press any key to exit");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
