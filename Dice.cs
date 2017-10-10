﻿using System;

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

        public int RollD6(int count) {
            return Roll(count, 6);
        }

        public int RollD8(int count) {
            return Roll(count, 8);
        }
    }
}
