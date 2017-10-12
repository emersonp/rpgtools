using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Tools {

    public class MarkovChain  
    {
        public int prefixCount {get; set;}
        static Random rnd = new Random();
        public MarkovChain(int count) {
            prefixCount = count;
        }

        public Dictionary<Chain, int> FullSet = new Dictionary<Chain, int>();

        // static void Main() {
        //     MarkovChain chain = new MarkovChain(2);
        //     List<string> words = new List<string>() {
        //         "frank",
        //         "frimble",
        //         "beans"
        //     };
        //     chain.ConvertWords(words);
        //     // foreach(KeyValuePair<Chain, int> entry in chain.FullSet) {
        //     //     Console.WriteLine("Symbol: {0}; Prefix: {1}; Frequency: {2}", entry.Key.symbol, string.Join("", entry.Key.prefix.ToArray()), entry.Value);
        //     // }
        //     Console.WriteLine(chain.Generate());
        // }

        // Converts a word to chains and adds them to FullSet
        public void AddChainToSet(Chain chain){
            if(!FullSet.ContainsKey(chain)) {
                FullSet.Add(chain, 1);
            } else {
                FullSet[chain] += 1;
            }
        }
        public void ConvertWordToChains(string word) {
            List<char> prefix = new List<char>();
            for (var i = 0; i < prefixCount; i++ ){
                prefix.Add(' ');
            }
            foreach(char c in word) {
                Chain tempChain = new Chain(c, prefix.ConvertAll(l => l));
                AddChainToSet(tempChain);
                prefix.RemoveAt(0);
                prefix.Add(c);
            }
            for (var i = 0; i < prefixCount; i++ ){
                Chain tempChain = new Chain(' ', prefix.ConvertAll(l => l));
                AddChainToSet(tempChain);
                prefix.RemoveAt(0);
                prefix.Add(' ');
            }
        }

        public void ConvertWords(List<string> words) {
            foreach (string word in words) {
                ConvertWordToChains(word);
            }
        }

        public string Generate() {
            List<char> prefix = new List<char>();
            List<char> word = new List<char>();
            char next = ' ';

            for (int i = 0; i < prefixCount; i++) {
                prefix.Add(' ');
            }
            var tempChain = new Chain('f', prefix.ConvertAll(l => l));
            //Console.WriteLine("Equal? {0}", tempChain.prefix == prefix);
            do {
                // Build prefix pool
                var availablePrefixes = FullSet.Where(e => e.Key.prefix.SequenceEqual(prefix));
                List<char> availableChars = new List<char>();
                foreach(KeyValuePair<Chain,int> entry in availablePrefixes) {
                    for (int j = 0; j < entry.Value; j++) {
                        availableChars.Add(entry.Key.symbol);
                    }
                }
                next = availableChars[rnd.Next(availableChars.Count)];
                word.Add(next);
                prefix.Add(next);
                prefix.RemoveAt(0);
                Console.WriteLine("Next: {0}", next);
            } while(next != ' ');

            word.RemoveAt(word.Count - 1);

            return string.Join("", word);
        }

        // A chain of letters, including probability
        public class Chain {
            public char symbol;
            public List<char> prefix;

            public Chain(char s, List<char> prefixes) {
                symbol = s;
                prefix = prefixes;
            }

            public override bool Equals(Object obj) 
            {
                // Check for null values and compare run-time types.
                if (obj == null || GetType() != obj.GetType()) 
                    return false;

                Chain c = (Chain)obj;
                return (symbol == c.symbol) && prefix.SequenceEqual(c.prefix);
            }
            public override int GetHashCode() {
                return (int)Char.GetNumericValue(symbol) ^ (int)Char.GetNumericValue(prefix[0]);
            }


        }
    }
}