using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adventCal
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Program start.");
            StreamReader file = new StreamReader("input.txt");
            int counter = 0;
            string line;
            List<string> arr = new List<string>();
            while ((line = file.ReadLine()) != null)
            {
                arr.Add(line);
                counter++;
            }
            Console.WriteLine($"{counter} lines read");

            //Console.WriteLine(arr.Count); // 0 - 322
            // original line 0 - 30
            int treesFound = 0;
            int right = 0;
            int down = 0;
            for (int i = 0; i < arr.Count; i++)
            {
                //Console.WriteLine($"down:{down}, right:{right}");
                if (down < arr.Count)
                {
                    //Console.WriteLine("  yIndex is good");
                }
                if (right < arr[down].Length)
                {
                    //Console.WriteLine("  xIndex is good");
                }
                else
                {
                    while (right > arr[down].Length-1)
                    {
                        //string original = arr[down];
                        string subStr = arr[down].Substring(0, 31);
                        arr[down] += subStr;
                    }
                    //Console.WriteLine("  xIndex is now good");
                    //Console.WriteLine($"{arr[down]}");
                }
                if (arr[down][right] == '#')
                {
                    //Console.WriteLine("Found tree!");
                    StringBuilder sb = new StringBuilder(arr[down]);
                    sb[right] = 'X';
                    arr[down] = sb.ToString();
                    treesFound++;
                }
                else
                {
                    //Console.WriteLine("No tree!");
                    StringBuilder sb = new StringBuilder(arr[down]);
                    sb[right] = 'O';
                    arr[down] = sb.ToString();
                }
                Console.WriteLine($"{arr[down]}");
                right += 3;
                down += 1;
            }
            Console.WriteLine($"trees: {treesFound}");
            file.Close();
            Console.WriteLine("Program done.");
        }
    }
}
