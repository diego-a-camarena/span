using CodingChallengeServices.Interfaces;
using CodingChallengeServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodingChallengeServices.Services
{
    public class LeagueService:ILeague
    {
        readonly char teamsdelimiterChar = ',';
        readonly char scoredelimiterChar = ' ';
        private enum Points : ushort { Loss = 0, Draw = 1, Win = 3 }
   

        public List<Match> NormalizeData(List<string> games)
        {
            List<Match> matches = new List<Match>();
            matches.AddRange(
                games.Select(m =>
                {
                    var teams = m.Split(teamsdelimiterChar);
                    if(teams.Length > 2)
                        throw new ArgumentException("The file contains incorrect data");

                    var prts = teams[0].Split(scoredelimiterChar);
                    string localTeam = String.Join(" ", prts.Take(prts.Length - 1));
                    bool isValidScore = ushort.TryParse( prts[prts.Length - 1], out ushort localScore );

                    prts = teams[1].Split(scoredelimiterChar);
                    string visitTeam = String.Join(" ", prts.Take(prts.Length - 1));
                    bool isValidVisitScore = ushort.TryParse(prts[prts.Length - 1], out ushort visitScore);
                    isValidScore = isValidScore && isValidVisitScore;

                    if (!isValidScore)
                        throw new ArgumentException("The score of some matches does not have the correct format");

                    return new Match() { LocalTeam = localTeam, LocalScore = localScore, VisitTeam = visitTeam , VisitScore = visitScore };

                })
            );
            return matches;
        }

        public List<Ranking> GetRanking(List<Match> matches)
        {
            Dictionary<string, ushort?> ranking = new Dictionary<string, ushort?>();
            List<Ranking> rankingList = new List<Ranking>();
            ushort LocalPoints = 0;
            ushort VisitPoints = 0;

            matches.ForEach(m =>
            {
                if (m.LocalScore > m.VisitScore)
                {
                    LocalPoints = (ushort)Points.Win;
                    VisitPoints = (ushort)Points.Loss;
                }

                if (m.LocalScore < m.VisitScore)
                {
                    LocalPoints = (ushort)Points.Loss;
                    VisitPoints = (ushort)Points.Win;
                }

                if (m.LocalScore == m.VisitScore)
                {
                    LocalPoints = (ushort)Points.Draw;
                    VisitPoints = (ushort)Points.Draw;
                }

                if (!ranking.ContainsKey(m.LocalTeam))
                    ranking.Add(m.LocalTeam, LocalPoints);
                else
                    ranking[m.LocalTeam] += LocalPoints;

                if (!ranking.ContainsKey(m.VisitTeam))
                    ranking.Add(m.VisitTeam, VisitPoints);
                else
                    ranking[m.VisitTeam] += VisitPoints;

            });

            rankingList.AddRange(
                ranking.Select( r => 
                {
                    return new Ranking(){ Team = r.Key, Points = (ushort)r.Value };
                })
            );

            return rankingList.OrderByDescending(r => r.Points).ThenBy(r => r.Team).ToList();
        }
       
    }
}
