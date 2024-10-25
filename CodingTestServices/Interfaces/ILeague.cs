using CodingChallengeServices.Models;
using System.Collections.Generic;


namespace CodingChallengeServices.Interfaces
{
    public interface ILeague
    {
        List<Match> NormalizeData(List<string> games);
        List<Ranking> GetRanking(List<Match> matches);
    }
}
