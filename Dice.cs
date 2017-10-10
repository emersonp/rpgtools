using System;
using System.Text.RegularExpressions;

namespace Tools
{
    public class Dice
    {
        public Random rnd = new Random();

        public int Roll(int count, int faces) {
            int total = 0;
            for (int i = 0; i < count; i++) {
                total += rnd.Next(1, faces + 1);
            }
            return total;
        }

        public int Roll(string regex) {
            Match match = Regex.Match(regex, @"^(\d*)d(\d+)([\+\-]*)(\d*)");
            if (match.Success) {
                int rolled, group1;
                int group2 = Int32.Parse(match.Groups[2].Value);
                string group3 = match.Groups[3].Value;
                int group4 = Int32.Parse(match.Groups[4].Value);
                if (Int32.TryParse(match.Groups[1].Value, out group1)) {
                    rolled = Roll(group1, group2);
                } else {
                    rolled = Roll(1, group2);
                }
                if (group3 == "+" && group4 > 0) {
                    rolled += group4;
                }
                if (group3 == "-" && group4 > 0) {
                    rolled -= group4;
                }
                return rolled;
            }
            return 0;
        }
    }
}
