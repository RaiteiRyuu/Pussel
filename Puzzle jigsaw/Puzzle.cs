using System;

namespace Puzzle_jigsaw
{
    class Puzzle
    {
        /*----------------------------------------------------------------*/
        /*                                                                */
        /*                                                                */
        /* Our Puzzle class is based on the Puzzle class from 15 Puzzles  */
        /* since we had trouble with puzzle image cutting / loading       */
        /*                                                                */
        /*                                                                */
        /*----------------------------------------------------------------*/
        public enum StartType
        {
            Normal,
            Random
        }

        #region fields
        //new zero positions for basic order
        public static int[,] newIndex = new int[16, 4] {{-1,1,-1,4},  {0,2,-1,5},   {1,3,-1,6},    {2,-1,-1,7},
                                                       {-1,5,0,8},   {4,6,1,9},    {5,7,2,10},    {6,-1,3,11},
                                                       {-1,9,4,12},  {8,10,5,13},  {9,11,6,14},   {10,-1,7,15},
                                                       {-1,13,8,-1}, {12,14,9,-1}, {13,15,10,-1}, {14,-1,11,-1}};


        private int[] puzzleArray = new int[16];
        private MainWindow winParent;

        #endregion

        #region constructor
        public Puzzle(StartType type, MainWindow parent)
        {
            this.winParent = parent;
                shuffle();
        }
        #endregion


        #region methods

        //check indexes and swaps them
        public int this[int index]
        {
            get { return puzzleArray[index]; }
        }

        /*Generic function for swapping tiles
        Para: a, b
        Return: None
        */
        public static void swap<T>(ref T a, ref T b)
        {
            T tmp = a;
            a = b;
            b = tmp;
        }

        /*position swap
        Para: pos1, pos2
        Return: None
        */
        public void swapPositions(int pos1, int pos2)
        {
            int tmp = puzzleArray[pos1];
            puzzleArray[pos1] = puzzleArray[pos2];
            puzzleArray[pos2] = tmp;
        }

        /*huffles the tiles
        Para: None
        Return: None
        */
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
        #endregion
    }
}
