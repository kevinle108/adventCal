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
            List<long> lines = new List<long>();
            while ((line = file.ReadLine()) != null)
            {
                //if (counter == 3)
                //{
                //    break;
                //}

                lines.Add(long.Parse(line));
                counter++;
            }

            file.Close();

            List<long> contig = new List<long>();
            long LOOK_NUM = 1639024365;
            long contigSum = 0;

            for (int i = 0; i < lines.Count; i++)
            {
                contigSum += lines[i];
                if (contigSum > LOOK_NUM)
                {
                    contigSum = 0;
                    continue;
                }
                else
                {
                    for (int j = i + 1; j < lines.Count; j++)
                    {

                        if (contigSum > LOOK_NUM)
                        {
                            contigSum = 0;
                            break;
                        }
                        else
                        {
                            contigSum += lines[j];
                            if (contigSum == LOOK_NUM)
                            {
                                Console.WriteLine($"FOUND! from {lines[i]} (line{i}) to {lines[j]} (line{j})");
                            }
                        }
                    }
                }

            }


            var foundRange = lines.GetRange(537, 17);
            Display(foundRange);
            long smallest = foundRange.Min();
            long largest = foundRange.Max();
            Console.WriteLine($"Answer: {smallest + largest}");
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
