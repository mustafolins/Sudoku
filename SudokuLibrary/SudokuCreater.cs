using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuLibrary
{
    public class SudokuCreater
    {
        /// <summary>
        /// The numbers at the different positions
        /// </summary>
        public int[][] Positions { get; set; }
        /// <summary>
        /// The shown <seealso cref="Positions"/>.
        /// </summary>
        public bool[][] ShownPositions { get; set; }

        /// <summary>
        /// Creates a sudoku board.
        /// </summary>
        public SudokuCreater()
        {
            ShownPositions = new bool[9][];
            for (int i = 0; i < 9; i++)
            {
                ShownPositions[i] = new bool[9];
            }

            // initialize array
            Positions = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                Positions[i] = new int[9];
            }
            // initialize random positions
            InitializeWithRandomValues();
        }

        /// <summary>
        /// Initializes the sudoku board with random values.
        /// </summary>
        private void InitializeWithRandomValues()
        {
            var rand = new Random();

            // initialize everything else
            InitializeAll(rand);
        }

        /// <summary>
        /// Initializes the sudoku board with the given <paramref name="rand"/>.
        /// </summary>
        /// <param name="rand">The <see cref="Random"/> to use.</param>
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

        /// <summary>
        /// Gets a random value that satisfies the conditions of sudoku.
        /// </summary>
        /// <param name="rand">The <see cref="Random"/> to use.</param>
        /// <param name="i">The current row.</param>
        /// <param name="j">The current column.</param>
        /// <param name="currRand">The current random number.</param>
        /// <param name="attempts">A reference to the number of attempts to get a random.</param>
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

        /// <summary>
        /// Checks if a <paramref name="num"/> exists in the current box.
        /// </summary>
        /// <param name="num">The number to check for.</param>
        /// <param name="row">The current row.</param>
        /// <param name="col">The current column.</param>
        /// <returns>Returns true if the <paramref name="num"/> is in the current box.</returns>
        private bool ExistsInBox(int num, int row, int col)
        {
            List<int> tempList = GetBox(row, col);

            return tempList.Contains(num);
        }

        /// <summary>
        /// Get's a list of numbers in the current box.
        /// </summary>
        /// <param name="row">The current row.</param>
        /// <param name="col">The current column.</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="int"/>'s in the current box.</returns>
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

        /// <summary>
        /// Checks if the <paramref name="num"/> is in the current <paramref name="row"/>.
        /// </summary>
        /// <param name="num">The number to check for.</param>
        /// <param name="row">The row to look for the <paramref name="num"/> in.</param>
        /// <returns>Returns true if the <paramref name="num"/> is in the current <paramref name="row"/>.</returns>
        private bool ExistsInRow(int num, int row)
        {
            List<int> tempList = GetRow(row);

            return tempList.Contains(num);
        }

        /// <summary>
        /// Gets a <see cref="List{T}"/> of numbers in the <paramref name="row"/>.
        /// </summary>
        /// <param name="row">The row to get the numbers from.</param>
        /// <returns>A <see cref="List{T}"/> of numbers in the <paramref name="row"/>.</returns>
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

        /// <summary>
        /// Checks if the <paramref name="num"/> exists in the <paramref name="column"/>.
        /// </summary>
        /// <param name="num">The number to look for.</param>
        /// <param name="column">The column to look for the <paramref name="num"/> in.</param>
        /// <returns>True if the <paramref name="num"/> exists in the <paramref name="column"/>.</returns>
        private bool ExistsInColumn(int num, int column)
        {
            List<int> tempList = GetColumn(column);

            return tempList.Contains(num);
        }

        /// <summary>
        /// Gets a <see cref="List{T}"/> of numbers in the <paramref name="column"/>.
        /// </summary>
        /// <param name="column">The column to get the numbers for.</param>
        /// <returns>A <see cref="List{T}"/> of numbers in the <paramref name="column"/>.</returns>
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

        /// <summary>
        /// Randomly hides the <seealso cref="ShownPositions"/>.
        /// </summary>
        /// <param name="degree">The degree to hide the values by.</param>
        public void HideRandomValues(int degree = 4)
        {
            var rand = new Random();
            for (int i = 0; i < Positions.Length; i++)
            {
                for (int j = 0; j < Positions[i].Length; j++)
                {
                    ShownPositions[i][j] = rand.Next(degree) % (degree / 2) == 0;
                }
            }
        }

        /// <summary>
        /// Returns a string representation of the sudoku board.
        /// </summary>
        /// <returns>A string representation of the sudoku board.</returns>
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
