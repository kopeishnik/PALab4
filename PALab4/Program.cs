using System;

namespace PALab4
{
    public class Program
    {
        public static readonly int N = 300;
        public static readonly int NumberOfPopulationMembers = 20;
        public static readonly int IterationsNumber = 1000;
        internal static void Main()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            var matrix = InputOutput.ReadFile();
            if (matrix == null)
            {
                Console.WriteLine("End of program because of wrong input file.");
            }
            else
            {
                if (!Processing.CheckVertexes(matrix))
                {
                    //Console.WriteLine("Wrong number of vertexes. Wrong format of matrix. All is wrong.");
                    //return;
                }
                Graph graph = new(matrix);
                Population population = new(graph);
                population.GenerateRandomPopulation();
                _ = Algorithms.GeneticAlgorithm(population);
                InputOutput.PrintBest(population);
            }
        }
    }
}