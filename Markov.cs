using System;
using System.Text.RegularExpressions;

namespace Tools {

    public class MarkovChain 
    {
        private int _prefixCount {get; set;}
        public MarkovChain(int prefixCount) {
            _prefixCount = prefixCount;
        }

        public void Feed(string[] words) {
            foreach (string word in words) {
                
            }
        }

        // A chain of letters, including probability
        private class Chain {
            private char symbol;
            private char[] _prefix;

            public Chain(char s) {
                symbol = s;
                for (int i = 0; i < _prefixCount; i++) {

                }
            }

        }
    }
}