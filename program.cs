using System;

namespace Consoleapppjm
{
    class Program
    {
        const int Size = 9;

        static void Main()
        {
            // if are more elements than the size it returns false
            try
            {
                string[,] sudokuSquare = GetSudokuSquare();
                Console.WriteLine(IsValid(sudokuSquare) && CheckSquares(sudokuSquare) ? "True" : "False");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("False");
            }
        }

        static string[,] GetSudokuSquare()
        {
            // Initial array
            string[,] sudokuSquare = new string[9, 9];

            // I used the counterLine to skip the blank lines
            int counterLine = 0;

            for (int i = 0; i < Size + 2; i++)
            {
                // get the line as array
                string[] line = Console.ReadLine().Split();

                // i used the counterColumn to skip the blank elements
                int counterColumn = 0;

                // If a input line is blank than continue
                if (line.Length == 1)
                {
                    continue;
                }

                for (int j = 0; j < line.Length; j++)
                {
                    // check if an element is null and break
                    if (line[j] == null)
                    {
                        break;
                    }

                    // check if an element is blank
                    if (line[j] != "")
                    {
                        // Add the element and increase the counterColumn
                        sudokuSquare[counterLine, counterColumn] = line[j];
                        counterColumn++;
                    }
                }

                counterLine++;
            }

            return sudokuSquare;
        }

        static bool IsValid(string[,] sudokuSquare)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    // Not enough elements on a line or Non Numeric Values or Check range
                    if (sudokuSquare[i, Size - 1] == null || CheckForNonNumeric(sudokuSquare[i, j]) ||
                        (Convert.ToInt32(sudokuSquare[i, j]) > Size || Convert.ToInt32(sudokuSquare[i, j]) < 0))
                    {
                        return false;
                    }
                }

                // Check lines
                if (!UniqueInArray(LineOrColumn(sudokuSquare, i, "line")))
                {
                    return false;
                }

                // Check columns
                else if (!UniqueInArray(LineOrColumn(sudokuSquare, i, "column")))
                {
                    return false;
                }
            }

            return true;
        }

        static bool CheckSquares(string[,] sudokuSquare)
        {
            // check squares
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (!UniqueInArray(GetSquare(sudokuSquare, i, j)))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        static bool CheckForNonNumeric(string element)
        {
            foreach (char c in element)
            {
                if (!char.IsDigit(c))
                {
                    return true;
                }
            }

            return false;
        }

        static bool UniqueInArray(string[] uniqueValues)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (i != j && uniqueValues[i] == uniqueValues[j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        static string[] LineOrColumn(string[,] sudokuSquare, int index, string where)
        {
            string[] elements = new string[9];

            if (where == "line")
            {
                for (int i = 0; i < Size; i++)
                {
                    elements[i] = sudokuSquare[index, i];
                }
            }
            else
            {
                for (int i = 0; i < Size; i++)
                {
                    elements[i] = sudokuSquare[i, index];
                }
            }

            return elements;
        }

        static string[] GetSquare(string[,] sudokuSquare, int i, int j)
        {
            string[] block = new string[9];
            int counter = 0;
            for (var m = 0; m < 3; m++)
            {
                for (var n = 0; n < 3; n++)
                {
                    block[counter] = sudokuSquare[i * 3 + m, j * 3 + n];
                    counter++;
                }
            }

            return block;
        }
    }
}
