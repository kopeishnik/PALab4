using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PALab4
{
    internal class Graph
    {
        /// <summary>
        /// boolean matrix graph
        /// </summary>
        public bool[,] GraphMatrix { get; set; }

        /// <summary>
        /// boolean matrix graph constructor
        /// </summary>
        /// <param name="matrix"> bool matrix </param>
        public Graph(bool[,] matrix)
        {
            if (matrix.Length != Program.N * Program.N)
            {
                Console.WriteLine($"Wrong matrix size ({matrix.Length}, not {Program.N * Program.N})!");

                Environment.Exit(1);
            }
            GraphMatrix = new bool[Program.N, Program.N];
            for (int i = 0; i < Program.N; i++)
            {
                int num = 0;
                for (int j = 0; j < Program.N; j++)
                {
                    if (matrix[i, j] == true)
                    {
                        GraphMatrix[i, j] = true;
                        num++;
                    }
                    else
                    {
                        GraphMatrix[i, j] = false;
                    }
                }
                if (num < 2 || num > 30)
                {
                    //Console.WriteLine("Wrong valence number!");
                    //Environment.Exit(1);
                }
            }
        }

        /// <summary>
        /// string array graph constructor
        /// </summary>
        /// <param name="strings"> string array </param>
        public Graph(string[] strings)
        {
            if (strings.Length != Program.N)
            {
                Console.WriteLine($"Wrong amount of strings ({strings.Length}, not {Program.N})!");
                Environment.Exit(1);
            }
            GraphMatrix = new bool[Program.N, Program.N];
            for (int i = 0; i < Program.N; i++)
            {
                if (strings[i].Length != Program.N)
                {
                    Console.WriteLine($"Wrong amount of symbols at {i + 1}!");
                    Environment.Exit(1);
                }
                int num = 0;
                for (int j = 0; j < Program.N; j++)
                {
                    if (strings[i][j] == '1')
                    {
                        GraphMatrix[i, j] = true;
                        num++;
                    }
                    else if (strings[i][j] == '0')
                    {
                        GraphMatrix[i, j] = false;
                    }
                    else
                    {
                        Console.WriteLine("Wrong symbols!");
                        Environment.Exit(1);
                    }
                }
                if (num < 2 || num > 30)
                {
                    Console.WriteLine("Wrong valence number!");
                    Environment.Exit(1);
                }
            }
        }

        /// <summary>
        /// int matrix graph constructor
        /// </summary>
        /// <param name="numbers"> int array </param>
        public Graph(int[,] numbers)
        {
            if (numbers.Length != Program.N * Program.N) 
            {
                Console.WriteLine($"Wrong matrix size ({numbers.Length}, not {Program.N * Program.N})!");
                Environment.Exit(1);
            }
            GraphMatrix = new bool[Program.N, Program.N];
            for (int i = 0; i < Program.N; i++)
            {
                int num = 0;
                for (int j = 0; j < Program.N; j++)
                {
                    if (numbers[i, j] == 1)
                    {
                        GraphMatrix[i, j] = true;
                        num++;
                    }
                    else if (numbers[i, j] == 0)
                    {
                        GraphMatrix[i, j] = false;
                    }
                    else
                    {
                        Console.WriteLine("Wrong number!");
                        Environment.Exit(1);
                    }
                    if (num < 2 || num > 30)
                    {
                        Console.WriteLine("Wrong valence number!");
                        Environment.Exit(1);
                    }
                }
            }
        }

        /// <summary>
        /// prints graph visualisation to console
        /// </summary>
        public void PrintGraph()
        {
            string result = "";
            for (int i = 0; i < Program.N; i++)
            {
                for (int j = 0; j < Program.N; j++)
                {
                    if (GraphMatrix[i, j] == true)
                    {
                        result += '1';
                    }
                    else
                    {
                        result += '0';
                    }
                }
                result += '\n';
            }
            Console.Write(result);
        }

        /// <summary>
        /// get string graph visualisation
        /// </summary>
        /// <returns> string visualisation of graph </returns>
        public string GetStringGraph()
        {
            string result = "";
            for (int i = 0; i < Program.N; i++)
            {
                for (int j = 0; j < Program.N; j++)
                {
                    if (GraphMatrix[i, j] == true)
                    {
                        result += '1';
                    }
                    else
                    {
                        result += '0';
                    }
                }
                result += '\n';
            }
            return result;
        }
        /// <summary>
        /// get bool matrix graph visualisation
        /// </summary>
        /// <returns> bool matrix graph </returns>
        public bool[,] GetGraph()
        {
            return GraphMatrix;
        }
    }
}
