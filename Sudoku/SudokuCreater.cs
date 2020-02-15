using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    class SudokuCreater
    {
        public int[][] Positions { get; set; }

        public SudokuCreater()
        {
            // initialize array
            Positions = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                Positions[i] = new int[9];
            }
            // initialize random positions
            InitializeWithRandomValues();
        }

        private void InitializeWithRandomValues()
        {
            var rand = new Random();

            // initialize everything else
            InitializeAll(rand);
        }

        private void InitializeAll(Random rand)
        {
            for (int i = 0; i < 9; i++)
            {
                var attempts = 1;
                for (int j = 0; j < 9; j++)
                {
                    if (Positions[i][j] == 0)
                    {
                        var currRand = rand.Next(1, 10);
                        GetRandom(rand, ref i, ref j, ref currRand, ref attempts);
                        Positions[i][j] = currRand;
                    }
                }
            }
        }

        private void GetRandom(Random rand, ref int i, ref int j, ref int currRand, ref int attempts)
        {
            while (ExistsInColumn(currRand, j) || ExistsInRow(currRand, i) || ExistsInBox(currRand, i, j))
            {
                var availableRand = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }
                                    .Except(GetRow(i))
                                    .Except(GetColumn(j))
                                    .Except(GetBox(i, j))
                                    .ToArray();

                // reset row if can't find any available
                if (availableRand.Length == 0)
                {
                    for (int k = 0; k < 9; k++)
                    {
                        Positions[i][k] = 0;
                    }
                    currRand = rand.Next(1, 10);
                    j = 0;

                    attempts++;
                    // it's taken to many attempts reset the table
                    if (attempts == 10)
                    {
                        for (int l = 0; l < 9; l++)
                        {
                            for (int k = 0; k < 9; k++)
                            {
                                Positions[l][k] = 0;
                            } 
                        }
                        i = 0;
                        break;
                    }
                    continue;
                }
                // get a random value for the available numbers
                currRand = availableRand[rand.Next(0, availableRand.Length)];
            }
        }

        private bool ExistsInBox(int num, int row, int col)
        {
            List<int> tempList = GetBox(row, col);

            return tempList.Contains(num);
        }

        private List<int> GetBox(int row, int col)
        {
            List<int> tempList = new List<int>();
            for (int i = (row / 3) * 3; i < (row / 3 + 1) * 3; i++)
            {
                for (int j = (col / 3) * 3; j < (col / 3 + 1) * 3; j++)
                {
                    if (Positions[i][j] != 0)
                    {
                        tempList.Add(Positions[i][j]);
                    }
                }
            }

            return tempList;
        }

        private bool ExistsInRow(int num, int row)
        {
            List<int> tempList = GetRow(row);

            return tempList.Contains(num);
        }

        private List<int> GetRow(int row)
        {
            var tempList = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                if (Positions[row][i] != 0)
                {
                    tempList.Add(Positions[row][i]);
                }
            }

            return tempList;
        }

        private bool ExistsInColumn(int num, int column)
        {
            List<int> tempList = GetColumn(column);

            return tempList.Contains(num);
        }

        private List<int> GetColumn(int column)
        {
            var tempList = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                if (Positions[i][column] != 0)
                {
                    tempList.Add(Positions[i][column]);
                }
            }

            return tempList;
        }

        public string PrintWithHiddenValues(int degree = 4)
        {
            var rand = new Random();
            var result = "";
            for (int i = 0; i < Positions.Length; i++)
            {
                for (int j = 0; j < Positions[i].Length; j++)
                {
                    result += $"{((rand.Next(degree) % degree == 0) ? " " : "" + Positions[i][j])}{((j < Positions[i].Length - 1) ? "|" : "")}";
                }
                if ((i + 1) % 3 == 0 && i != Positions.Length - 1)
                {
                    result += "\n-----------------";
                }
                result += "\n";
            }
            return result;
        }

        public override string ToString()
        {
            var result = "";
            for (int i = 0; i < Positions.Length; i++)
            {
                for (int j = 0; j < Positions[i].Length; j++)
                {
                    result += $"{Positions[i][j]}{((j < Positions[i].Length - 1) ? "|" : "")}";
                }
                if ((i + 1) % 3 == 0 && i != Positions.Length - 1)
                {
                    result += "\n-----------------";
                }
                result += "\n";
            }
            return result;
        }
    }
}
