﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Program start.");
            StreamReader file = new StreamReader("input.txt");
            int counter = 0;
            string line;
            //List<string> lines = new List<string>();
            List<int> lines = new List<int>();
            
            while ((line = file.ReadLine()) != null)
            {
                //if (counter == 3)
                //{
                //    break;
                //}

                lines.Add(Int32.Parse(line));
                counter++;
            }
            file.Close();
            int diff_1 = 0;
            int diff_3 = 0;
            int jolt = 0;
            lines = lines.OrderBy(x => x).ToList();
            Display(lines);
            for (int i = 0; i < lines.Count; i++) 
            {
                int jolt_diff = lines[i] - jolt;
                if (jolt_diff == 1) 
                {
                    diff_1++;
                } 
                else if (jolt_diff == 3) 
                {
                    diff_3++;
                }
                else 
                {
                    // do nothing
                }
                jolt = lines[i];
            }
            diff_3++; // for phone
            Console.WriteLine($"Answer: {diff_1 * diff_3}");
            Console.WriteLine($"\n{counter} lines read");
            Console.WriteLine("Program done.");

        }

        //}
        public static void Display<T>(List<T> arr, params string[] parameters)
        {
            //foreach (int num in arr)
            //{
            //    Console.WriteLine(num);
            //}
            arr.ForEach(x => Console.WriteLine(x));
        }

        public static void DisplayListList(List<List<string>> listArr)
        {
            foreach (List<string> arr in listArr)
            {
                Console.WriteLine(arr[0]);
                Console.WriteLine($"   {arr[1]}");
            }
        }

        //public static void Display(List<string> arr)
        //{
        //    foreach (string str in arr)
        //    {
        //        Console.WriteLine(str);
        //    }
        //}
    }
}
