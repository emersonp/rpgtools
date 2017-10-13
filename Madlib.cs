using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Tools
{
    public static class Madlib {
        public static string ParseAndPopulate(string phrase, Dictionary<string, List<string>> maps) {
            Dice dice = new Dice();
            string parsedPhrase = phrase;
            while (true) {
                int firstBracket = parsedPhrase.IndexOf('[');
                if (firstBracket < 0) { break; }
                int secondBracket = parsedPhrase.IndexOf(']');
                string table = parsedPhrase.Substring(firstBracket + 1, secondBracket - firstBracket - 1);
                string selection = dice.Sample(maps[table]);
                parsedPhrase = parsedPhrase.Remove(firstBracket, secondBracket - firstBracket + 1).Insert(firstBracket, selection);
            }
            return parsedPhrase;
        }
    }
}