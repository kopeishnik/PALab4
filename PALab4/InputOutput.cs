using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PALab4
{
    internal class InputOutput
    {
        public static bool[,]? ReadFile()
        {
            Console.WriteLine("Enter file path or empty string to use default file:");
            string? path = Console.ReadLine();
            if (path == null || path == "")
            {
                Console.WriteLine("Using standart file.");
                path = @"C:\Users\zeeel\source\labs\PALab4\PALab4\input.txt";
            }
            else if (!File.Exists(@"C:\Users\zeeel\source\labs\PALab4\PALab4\" + path + @".txt"))
            {
                Console.WriteLine("File does not exist. Using standart file.");
                path = @"C:\Users\zeeel\source\labs\PALab4\PALab4\input.txt";
            }
            else
            {
                path = @"C:\Users\zeeel\source\labs\PALab4\PALab4\" + path + @".txt";
            }
            StreamReader sr = new(path);
            var strings = sr.ReadToEnd().Split(new char[] { '\n' },
                StringSplitOptions.RemoveEmptyEntries);
            if (strings.Length != Program.N)
            {
                Console.WriteLine($"Number of strings is not {Program.N}");
                Console.WriteLine(strings.Length);
                return null;
            }
            bool[,] result = new bool[strings.Length, strings.Length];
            for (int i = 0; i < strings.Length; i++)
            {
                if (strings[i].Length != Program.N)
                {
                    Console.WriteLine($"Symbols number in string {i + 1} is not {Program.N}");
                    return null;
                }
                for (int j = 0; j < strings[i].Length; j++)
                {
                    if (strings[i][j] != '0' && strings[i][j] != '1')
                    {
                        Console.WriteLine($"Wrong symbol at ({i + 1}, {j + 1})");
                        return null;
                    }
                    result[i, j] = strings[i][j] == '1';
                }
            }
            return result;
        }

        public static void PrintBest(Population population)
        {
            var solution = Algorithms.GetBestSolution(population);
            var f = Algorithms.GetSolutionF(solution);
            Console.Write($"Best solution:");
            PrintSolution(solution);
            Console.WriteLine($"\nNumber of vertexes: {f}.");
        }

        public static void PrintSolution(bool[] solution)
        {
            for (int i = 0; i < solution.Length; i++)
            {
                if (!solution[i])
                {
                    Console.Write(" 0");
                }
                else
                {
                    Console.Write(" 1");
                }
            }
        }
    }
}
