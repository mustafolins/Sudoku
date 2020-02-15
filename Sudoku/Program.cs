using System;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            var creater = new SudokuCreater();
            Console.WriteLine(creater);
            Console.WriteLine(creater.PrintWithHiddenValues());
            Console.WriteLine(creater.PrintWithHiddenValues(3));
            Console.WriteLine(creater.PrintWithHiddenValues(2));
        }
    }
}
