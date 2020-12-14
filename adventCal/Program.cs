using System;
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
            int time = Int32.Parse(file.ReadLine());
            string buses = file.ReadLine();
            file.Close();
            List<int> busIds = new List<int>();
            List<int> busIdsTDiff = new List<int>();
            List<string> split = buses.Split(',').ToList();

            int tCount = 0;
            string strAdd = $"# {tCount}";
            for (int i = 0; i < split.Count; i++)
            {
                split[i] += " " + strAdd;
                tCount++;
                strAdd = $"# {tCount}";
            }

            foreach (string id in split)
            {
                if (id[0] != 'x')
                {
                    string[] splitId = id.Split('#');

                    busIds.Add(Int32.Parse(splitId[0]));
                    busIdsTDiff.Add(Int32.Parse(splitId[1]));
                }
            }


            //Display(split);
            //Console.WriteLine();
            //Display(busIds);
            //Console.WriteLine();
            //Display(busIdsTDiff);
            List<List<int>> multiples = new List<List<int>>();
            int idIndex = 0;
            foreach (int bus in busIds)
            {
                List<int> idMultiple = new List<int>();

                idMultiple.Add(busIds[idIndex]);
                idIndex++;
                multiples.Add(idMultiple);
            }
            bool keepGoing = true;
            int t0 = busIds[0]; // 17
            int t0Index = 0;
            int t1Index = 0;
            int exitLoop = 0;
 
            while (!(multiples[1].Contains(multiples[0].Last() + busIdsTDiff[1])))
            {
 
                if (multiples[1].Contains(multiples[0].Last() + busIdsTDiff[1]))
                {
                    while (!(multiples[2].Contains(multiples[0].Last() + busIdsTDiff[2])))
                    {

                        if (multiples[2].Contains(multiples[0].Last() + busIdsTDiff[2]))
                        {
                            Console.WriteLine("#####");
                            break;
                        }
                    }
                }
                if (multiples[1].Last() > multiples[0].Last() + busIdsTDiff[1])
                {
                    multiples[0].Add(multiples[0].Last() + multiples[0][0]);
                }
                multiples[1].Add(multiples[1].Last() + multiples[1][0]);
            }

            DisplayNestedList(multiples);






            //Console.WriteLine($"\n{counter} lines read");
            Console.WriteLine("Program done.");

        }

        private static void DisplayNestedList(List<List<int>> multiples)
        {
            foreach (var item in multiples)
            {
                Console.WriteLine();
                Display(item);
            }
        }

        private static void DiplayNestedList(List<List<int>> departures)
        {
            foreach (List<int> bus in departures)
            {
                Console.Write("\n ");
                foreach (int departTime in bus)
                {
                    Console.Write($" {departTime}");
                }
                Console.WriteLine();
            }
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
