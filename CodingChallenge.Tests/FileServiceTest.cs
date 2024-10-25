using CodingChallengeServices.Interfaces;
using CodingChallengeServices.Services;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace CodingChallenge.Tests
{
    public class FileServiceTest
    {
        private readonly IFile Service;
        private readonly string CurrentPath = Directory.GetCurrentDirectory();

        public FileServiceTest()
        {
            Service = new FileService();
        }

        //Verify correct reading of an existing file
        [Fact]
        public void ReadExistingFile()
        {
            var result = Service.ReadFileResults(Path.Combine(CurrentPath, "Assets", "test.txt"));
            Assert.IsType<List<string>>(result);
        }

        //Check exception for bad file
        [Fact]
        public void ReadNonExistingFile()
        {        
            ArgumentException ex = Assert.Throws<ArgumentException>(() => Service.ReadFileResults(Path.Combine(CurrentPath, "Assets", "test2.txt")));
            Assert.Equal("File does not exist", ex.Message);
        }

        [Fact]
        public void CreateResultsFile()        {
            string path = Path.Combine(CurrentPath,"results.txt");
            List<string> standings = new List<string>(new string[] { "1. Team A, 10 pts", "2. Team B, 8 pts", "3. Team C, 5 pts" });           
            Service.WriteFile(path, standings);
            Assert.True(File.Exists(path));
        }

    }
}
