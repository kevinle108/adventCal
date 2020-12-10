using System;
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
            
            lines = lines.OrderBy(x => x).ToList();
            List<int> diffList = new List<int>();
            int jolt = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                int diff = lines[i] - jolt;
                if (diff != 3 && diff != 1) 
                {
                    Console.WriteLine("FOUND OTHER DIFF VALUE besides 1 or 3!");
                }
                diffList.Add(diff);
                jolt = lines[i];
            }
            Display(diffList);
            Console.WriteLine($"\n{counter} lines read");
            Console.WriteLine("Program done.");

        }

        public static int Factorial(int number)
        {
            if (number == 1)
                return 1;
            else
                return Factorial(number - 1);
        }

        public static List<int> DeepCopyList(List<int> lines)
        {
            List<int> copyList = new List<int>();
            foreach (int line in lines)
            {
                copyList.Add(line);
            }
            return copyList;
        }

        public static bool IsValidArragement(List<int> lines)
        {
            bool isValid = true;
            int jolt = 0;
            int jolt_diff = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                jolt_diff = lines[i] - jolt;
                jolt = lines[i];
                if (!(jolt_diff >= 1 && jolt_diff <= 3))
                {
                    //Console.WriteLine($"line{i}, jolt_diff: {jolt_diff}");
                    isValid = false;
                    return isValid;
                }
            }
            return isValid;
        }
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
