using System;
using System.Collections.Generic;
using System.IO;

namespace Sudoku
{
    // Responsible for file operations
    public class FileIO
    {
        // Reads puzzle data from file into a list
        public static List<List<int>> readBoard(string path)
        {
            List<List<int>> result = new List<List<int>>();
            string line;
            List<int> row;
            int value;

            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    row = new List<int>();
                    line = reader.ReadLine();
                    foreach (var ch in line)
                    {
                        if(ch > 47 && ch < 58)
                        {
                            value = Convert.ToInt32(ch.ToString());
                        }
                        else
                        {
                            value = 0;
                        }
                        row.Add(value);
                    }
                    result.Add(row);
                }
            }

            return result;
        }
    }
}
