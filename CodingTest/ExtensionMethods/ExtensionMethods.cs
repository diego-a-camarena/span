using CodingChallengeServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static List<String> ToStringRules(this List<Ranking> ranking)
        {
            List<string> rl = new();
            int pos = 0;
            int samePos = 0;
            rl.AddRange(
                ranking.Select((value, index) =>
                {
                    pos += 1;

                    if (index != 0 && ranking[index - 1].Points == value.Points)
                    {
                        samePos += 1;
                        return $"{pos - samePos}. {value.Team.Trim()}, {value.Points} pts";
                    }

                    samePos = 0;
                    return $"{pos}. {value.Team.Trim()}, {value.Points} pts";
                })
            );
            return rl;
        }
    }
}
