using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle_jigsaw
{
    class Puzzle
    {
        public enum StartType
        {
            Normal,
            Random
        }
        /* dir 0 = left
         * dir 1 = right
         * dir 2 = up
         * dir 3 = down
         */

        //new zero positions for basic order
        public static int[,] newIndex = new int[16, 4] {{-1,1,-1,4},  {0,2,-1,5},   {1,3,-1,6},    {2,-1,-1,7},
                                                       {-1,5,0,8},   {4,6,1,9},    {5,7,2,10},    {6,-1,3,11},
                                                       {-1,9,4,12},  {8,10,5,13},  {9,11,6,14},   {10,-1,7,15},
                                                       {-1,13,8,-1}, {12,14,9,-1}, {13,15,10,-1}, {14,-1,11,-1}};

        //private static byte[] final = new byte[16] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0 };


        private int[] puzzleArray = new int[16];
        private MainWindow winParent;

        public Puzzle(StartType type, MainWindow parent)
        {
            this.winParent = parent;
                shuffle();
        }



        public int this[int index]
        {
            get { return puzzleArray[index]; }
        }

        public static void swap<T>(ref T a, ref T b)
        {
            T tmp = a;
            a = b;
            b = tmp;
        }

        public void swapPositions(int pos1, int pos2)
        {
            int tmp = puzzleArray[pos1];
            puzzleArray[pos1] = puzzleArray[pos2];
            puzzleArray[pos2] = tmp;
        }



        public void shuffle()
        {
            Random rand = new Random();

            
            for (int i = 0; i < 16; ++i)
            {
                puzzleArray[i] = i;
            }
            for (int i = 16; i > 1; i--)
            {
                swap<int>(ref puzzleArray[i - 1], ref puzzleArray[rand.Next(i)]);
            }
            
        }

        private void startPos()
        {           
            for (byte i = 0; i < 16; ++i)
                puzzleArray[i] = i;
        }



       //Behövs detta här under? Vad gör det ens?

        //static public int zeroPos(ref int[] puz)
        //{
        //    for (byte i = 0; i < 16; ++i)
        //    {
        //        if (puz[i] == 0)
        //            return i;
        //    }
        //    return -1;
        //}

        //public int zeroPos()
        //{
        //    for (byte i = 0; i < 16; ++i)
        //    {
        //        if (puzzleArray[i] == 0)
        //            return i;
        //    }
        //    return -1;
        //}

        //static public int zeroPos(ref Puzzle puz)
        //{
        //    for (byte i = 0; i < 16; ++i)
        //    {
        //        if (puz[i] == 0)
        //            return i;
        //    }
        //    return -1;
        //}
    }
}
