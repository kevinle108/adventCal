﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            List<string> lines = new List<string>();
            // 0 - 89 rows, 0 - 94 cols
            while ((line = file.ReadLine()) != null)
            {
                //if (counter == 3)
                //{
                //    break;
                //}
                //Console.WriteLine(line.Length);
                lines.Add(line);
                counter++;
            }
            file.Close();
            int north = 0;
            int south = 0;
            int east = 0;
            int west = 0;
            string curDirection = "east";
            int xCoord = 0;
            int yCoord = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                string command = lines[i][0].ToString();
                int value = Int32.Parse(lines[i].Substring(1, lines[i].Length - 1));
                if (command == "L")
                {
                    switch (curDirection)
                    {
                        case "east":
                            curDirection = "north";
                            break;
                        case "north":
                            curDirection = "west";
                            break;
                        case "west":
                            curDirection = "south";
                            break;
                        case "south":
                            curDirection = "east";
                            break;
                        default:
                            Console.WriteLine("Cannot determine which direction to turn L");
                            break;
                    }
                }
                else if (command == "R")
                {
                    switch (curDirection)
                    {
                        case "east":
                            curDirection = "south";
                            break;
                        case "south":
                            curDirection = "west";
                            break;
                        case "west":
                            curDirection = "north";
                            break;
                        case "north":
                            curDirection = "east";
                            break;
                        default:
                            Console.WriteLine("Cannot determine which direction to turn R");
                            break;
                    }
                }
                else if (command == "F")
                {
                    switch (curDirection)
                    {
                        case "east":
                            east += value;
                            break;
                        case "south":
                            south += value;
                            break;
                        case "west":
                            west += value;
                            break;
                        case "north":
                            north += value;
                            break;
                        default:
                            Console.WriteLine("Cannot determine how to move forward");
                            break;
                    }
                }
                else // command is N, S, E, or W
                {
                    switch (command)
                    {
                        case "N":
                            north += value;
                            break;
                        case "S":
                            south += value;
                            break;
                        case "E":
                            east += value;
                            break;
                        case "W":
                            west += value;
                            break;
                        default:
                            Console.WriteLine("Cannot determine the command at all!");
                            break;
                    }
                }
                xCoord += (north - south);
                yCoord += (east - west);
                north = 0;
                south = 0;
                east = 0;
                west = 0;
            }

            int answer = Math.Abs(xCoord) + Math.Abs(yCoord);
            Console.WriteLine($"curent direction: {curDirection}");
            Console.WriteLine($"xCoord: {xCoord}");
            Console.WriteLine($"yCoord: {yCoord}");
            //Console.WriteLine($"north {north}");
            //Console.WriteLine($"south {south}");
            //Console.WriteLine($"east {east}");
            //Console.WriteLine($"west {west}");
            Console.WriteLine($"\nanswer: {answer}");
            //Console.WriteLine($" {}");


            Console.WriteLine($"\n{counter} lines read");
            Console.WriteLine("Program done.");

        }

        private static int CountOccupiedSeats(List<string> lines)
        {
            int sumSeats = 0;
            foreach (string str in lines)
            {
                sumSeats += str.Where(x => x == '#').ToList().Count;
            }
            return sumSeats;
        }

        private static void ExecuteRound(List<string> lines)
        {
            List<List<int>> toEmpty = new List<List<int>>();
            List<List<int>> toOccupied = new List<List<int>>();
            for (int i = 0; i < lines.Count; i++) // for each line string
            {
                List<int> rowToEmpty = new List<int>();
                List<int> rowToOccupied = new List<int>();

                for (int j = 0; j < lines[i].Length; j++) // for each char in string
                {
                    char self = lines[i][j];
                    if (self == 'L' || self == '#')
                    {
                        int surSeatsOccupied = OccupiedAdj(i, j, lines);
                        if (self == 'L')
                        {
                            if (surSeatsOccupied == 0)
                            {
                                rowToOccupied.Add(j);
                            }
                        }
                        else if (self == '#')
                        {
                            if (surSeatsOccupied >= 5)
                            {
                                rowToEmpty.Add(j);
                            }
                        }
                        else
                        {
                            // do nothing
                        }
                    }
                }
                toEmpty.Add(rowToEmpty);
                toOccupied.Add(rowToOccupied);
            }

            for (int i = 0; i < toEmpty.Count; i++)
            {
                StringBuilder sb = new StringBuilder(lines[i]);
                for (int j = 0; j < toEmpty[i].Count; j++)
                {
                    int index = toEmpty[i][j];
                    sb[index] = 'L';
                }
                lines[i] = sb.ToString();
            }

            for (int i = 0; i < toOccupied.Count; i++)
            {
                StringBuilder sb = new StringBuilder(lines[i]);
                for (int j = 0; j < toOccupied[i].Count; j++)
                {
                    int index = toOccupied[i][j];
                    sb[index] = '#';
                }
                lines[i] = sb.ToString();
            }
        }

        public static int OccupiedAdj(int row, int col, List<string> lines)
        {
            int lastColIndex = lines[row].Length - 1;
            int lastRowIndex = lines.Count - 1;
            char self = lines[row][col];
            int seatsOccupied = 0;
            // check row
            // go left
            int crawlLeft = col - 1;
            while (crawlLeft >= 0)
            {
                if (lines[row][crawlLeft] == 'L') 
                {
                    break;
                }
                if (lines[row][crawlLeft] == '#')
                {
                    //Console.WriteLine($"Found occupied chair at ({row},{crawlLeft})");
                    seatsOccupied++;
                    break;
                }
                crawlLeft--;
            }
            int crawlRight = col + 1;
            while (crawlRight <= lines[row].Length - 1)
            {
                if (lines[row][crawlRight] == 'L')
                {
                    break;
                }
                if (lines[row][crawlRight] == '#')
                {
                    //Console.WriteLine($"Found occupied chair at ({row},{crawlRight})");
                    seatsOccupied++;
                    break;
                }
                crawlRight++;
            }
            int crawlUp = row - 1;
            while (crawlUp >= 0)
            {
                if (lines[crawlUp][col] == 'L')
                {
                    break;
                }
                if (lines[crawlUp][col] == '#')
                {
                    //Console.WriteLine($"Found occupied chair at ({crawlUp},{col})");
                    seatsOccupied++;
                    break;
                }
                crawlUp--;
            }
            int crawlDown = row + 1;
            while (crawlDown <= lines.Count-1)
            {
                if (lines[crawlDown][col] == 'L')
                {
                    break;
                }
                if (lines[crawlDown][col] == '#')
                {
                    //Console.WriteLine($"Found occupied chair at ({crawlDown},{col})");
                    seatsOccupied++;
                    break;
                }
                crawlDown++;
            }

            int crawlUpLeftRow = row - 1;
            int crawlUpLeftCol = col -1;
            while (crawlUpLeftRow >= 0 && crawlUpLeftCol >= 0)
            {
                if (lines[crawlUpLeftRow][crawlUpLeftCol] == 'L')
                {
                    break;
                }
                if (lines[crawlUpLeftRow][crawlUpLeftCol] == '#')
                {
                    //Console.WriteLine($"Found occupied chair at ({crawlUpLeftRow},{crawlUpLeftCol})");
                    seatsOccupied++;
                    break;
                }
                crawlUpLeftRow--;
                crawlUpLeftCol--;
            }

            int crawlUpRightRow = row - 1;
            int crawlUpRightCol = col + 1;
            while (crawlUpRightRow >= 0 && crawlUpRightCol <= lines[crawlUpRightRow].Length-1)
            {
                if (lines[crawlUpRightRow][crawlUpRightCol] == 'L')
                {
                    break;
                }
                if (lines[crawlUpRightRow][crawlUpRightCol] == '#')
                {
                    //Console.WriteLine($"Found occupied chair at ({crawlUpRightRow},{crawlUpRightCol})");
                    seatsOccupied++;
                    break;
                }
                crawlUpRightRow--;
                crawlUpRightCol++;
            }

            int crawlDownLeftRow = row + 1;
            int crawlDownLeftCol = col - 1;
            while (crawlDownLeftRow <= lines.Count-1 && crawlDownLeftCol >= 0)
            {
                if (lines[crawlDownLeftRow][crawlDownLeftCol] == 'L')
                {
                    break;
                }
                if (lines[crawlDownLeftRow][crawlDownLeftCol] == '#')
                {
                    //Console.WriteLine($"Found occupied chair at ({crawlDownLeftRow},{crawlDownLeftCol})");
                    seatsOccupied++;
                    break;
                }
                crawlDownLeftRow++;
                crawlDownLeftCol--;
            }

            int crawlDownRightRow = row + 1;
            int crawlDownRightCol = col + 1;
            while (crawlDownRightRow <= lines.Count - 1 && crawlDownRightCol <= lines[crawlDownRightRow].Length-1)
            {
                if (lines[crawlDownRightRow][crawlDownRightCol] == 'L')
                {
                    break;
                }
                if (lines[crawlDownRightRow][crawlDownRightCol] == '#')
                {
                    //Console.WriteLine($"Found occupied chair at ({crawlDownRightRow},{crawlDownRightCol})");
                    seatsOccupied++;
                    break;
                }
                crawlDownRightRow++;
                crawlDownRightCol++;
            }
            return seatsOccupied;
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
