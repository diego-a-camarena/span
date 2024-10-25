using CodingChallengeServices.Interfaces;
using CodingChallengeServices.Models;
using CodingChallengeServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodingChallenge.Tests
{
    public class LeagueServiceTests
    {
        private readonly ILeague Service;

        public LeagueServiceTests()
        {
            Service = new LeagueService();
        }

        //Verify to get the correct model
        [Fact]
        public void CheckNormalizeData()
        {
            List<string> matches = new List<string>(new string[] { "Lions 3, Snakes 3", "Tarantulas 1, FC Awesome 0", "Lions 1, FC Awesome 1" });
            var result = Service.NormalizeData(matches);
            Assert.IsType<List<Match>>(result);
        }

        //Verify correct ranking of the games.
        [Fact]
        public void MatchRanking()
        {
            List<Match> matches = new List<Match>()
            {
                new Match(){ LocalTeam = "Team A", LocalScore = 3, VisitTeam = "Team B", VisitScore = 2  },
                new Match(){ LocalTeam = "Team C", LocalScore = 1, VisitTeam = "Team B", VisitScore = 0  },
                new Match(){ LocalTeam = "Team C", LocalScore = 2, VisitTeam = "Team A", VisitScore = 1  },
            };

            List<Ranking> ranking = new List<Ranking>()
            {
                new Ranking(){ Team = "Team C", Points = 6 },
                new Ranking(){ Team = "Team A", Points = 3 },
                new Ranking(){ Team = "Team B", Points = 0 },
            };

            var result = Service.GetRanking(matches);
            Assert.Equal(ranking.ToString(), result.ToString());
        }
    }
}
