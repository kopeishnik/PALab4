using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PALab4
{
    internal static class Algorithms
    {
        public static bool[] ApproxVertexCover(Graph graph)
        {
            bool[] member = new bool[Program.N];
            List<int> result = new();
            List<(int, int)> edges = GetListOfEdges(graph.GraphMatrix);
            int iterations = 0;

            while (edges.Count > 0)
            {
                (int, int) chosenEdge = GetNextEdge(edges);
                List<(int, int)> toDelete = new();
                foreach (var x in edges)
                {
                    iterations++;
                    if (x.Item1 == chosenEdge.Item1 || x.Item2 == chosenEdge.Item1 || x.Item1 == chosenEdge.Item2 || x.Item2 == chosenEdge.Item2)
                    {
                        toDelete.Add(x);
                    }
                }
                foreach (var x in toDelete)
                {
                    iterations++;
                    edges.Remove(x);
                }

                result.Add(chosenEdge.Item1 + 1);
                result.Add(chosenEdge.Item2 + 1);
                member[chosenEdge.Item1] = true;
                member[chosenEdge.Item2] = true;
            }
            return member;
        }
        public static List<(int, int)> GetListOfEdges(bool[,] graphMatrix)
        {
            int n = (int)Math.Sqrt(graphMatrix.Length);

            List<(int, int)> edges = new();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (graphMatrix[i, j] == true)
                    {
                        edges.Add((i, j));
                    }
                }
            }
            return edges;
        }
        public static (int, int) GetNextEdge(List<(int, int)> edges)
        {
            Random random = new();
            return edges[random.Next(0, edges.Count)];
        }
        public static bool IsValid(bool[] solution, Graph graph)
        {
            List<(int, int)> edges = GetListOfEdges(graph.GraphMatrix);
            for (int i = 0; i < solution.Length; i++)
            {
                if (solution[i] == true)
                {
                    List<(int, int)> toDelete = new();
                    foreach (var x in edges)
                    {
                        if (x.Item1 == i || x.Item2 == i)
                        {
                            toDelete.Add(x);
                        }
                    }
                    foreach (var x in toDelete)
                    {
                        edges.Remove(x);
                    }
                }
            }
            if (edges.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static int GetSolutionF(bool[] solution)
        {
            int num = 0;
            for (int i = 0; i < solution.Length; i++)
            {
                if (solution[i] == true)
                {
                    num++;
                }
            }
            return num;
        }
        public static bool[] GetBestSolution(Population population)
        {
            int prev = GetSolutionF(population.CurrentPopulation[0]);
            int bestLocation = 0;
            for (int i = 1; i < population.CurrentPopulation.Count; i++)
            {
                var zis = GetSolutionF(population.CurrentPopulation[i]);
                if (zis < prev)
                {
                    prev = zis;
                    bestLocation = i;
                }
            }
            return population.CurrentPopulation[bestLocation];
        }
        public static bool[] GetWorstSolution(Population population)
        {
            int prev = GetSolutionF(population.CurrentPopulation[0]);
            int worstLocation = 0;
            for (int i = 1; i < population.CurrentPopulation.Count; i++)
            {
                var zis = GetSolutionF(population.CurrentPopulation[i]);
                if (zis > prev)
                {
                    prev = zis;
                    worstLocation = i;
                }
            }
            return population.CurrentPopulation[worstLocation];
        }
        //
        public static bool[] GeneticAlgorithm(Population population)
        {
            for (int i = 0; i < Program.IterationsNumber; i++)
            {
                Random random = new();
                int position1 = random.Next(0, population.CurrentPopulation.Count);
                int position2 = random.Next(0, population.CurrentPopulation.Count);
                while (position2 == position1)
                {
                    position2 = random.Next(0, population.CurrentPopulation.Count);
                }
                List<bool[]> list = new();
                int next = random.Next(0, 3);
                bool[] newSolution;
                if (next == 0)
                {
                    newSolution = OnePointOperator(population.CurrentPopulation[position1], population.CurrentPopulation[position2]);
                }
                else if (next == 2)
                {
                    newSolution = EqualOperator(population.CurrentPopulation[position1], population.CurrentPopulation[position2]);
                }
                else
                {
                    newSolution = ComparativeOperator(population.CurrentPopulation[position1], population.CurrentPopulation[position2]);
                }
                list.Add(newSolution);
                if (i % 5 == 0)
                {
                    next = random.Next(0, 2);
                    if (next == 0)
                    {
                        newSolution = SwapOneMutationOperator(newSolution); 
                    }
                    else
                    {
                        newSolution = SwapTwoMutationOperator(newSolution);
                    }
                    list.Add(newSolution);
                }
                int count = list.Count;
                for (int j = 0; j < count; j++)
                {
                    next = random.Next(0, 2);
                    if (next == 0)
                    {
                        newSolution = BestLocalUpgradingOperator(newSolution, population.ThisGraph);
                    }
                    else
                    {
                        newSolution = RandomLocalUpgradingOperator(newSolution);
                    }
                    list.Add(newSolution);
                }
                foreach (var element in list)
                {
                    if (IsValid(element, population.ThisGraph))
                    {
                        //Console.WriteLine("Valid " + GetSolutionF(element));
                        var x = GetWorstSolution(population);
                        if (GetSolutionF(x) > GetSolutionF(element))
                        {
                            population.CurrentPopulation.Remove(x);
                            population.CurrentPopulation.Add(element);
                        }
                    }
                    else
                    {
                        //Console.WriteLine("Invalid");
                    }
                }
                if (i % 100 == 0)
                {
                    Console.WriteLine($"Iterations: {i}, best: {GetSolutionF(GetBestSolution(population))}");
                }
            }
            return GetBestSolution(population);
        }
        //добавляет самый успешный ген как единицу (сос мыслом)
        public static bool[] BestLocalUpgradingOperator(bool[] solution, Graph graph)
        {
            bool[] newSolution = new bool[Program.N];
            for (int i = 0; i < solution.Length; i++)
            {
                newSolution[i] = solution[i];
            }
            List<int> notAdded = new();
            for (int i = 0; i < newSolution.Length; i++)
            {
                if (newSolution[i] == false)
                {
                    notAdded.Add(i);
                }
            }
            int toInclude = 0;
            int toIncludeValence = -1;
            for (int i = 0; i < notAdded.Count; i++)
            {
                int iValence = Processing.CountValence(graph, notAdded[i]);
                if (iValence > toIncludeValence) {
                    toInclude = notAdded[i];
                    toIncludeValence = iValence;
                }
            }
            newSolution[toInclude] = true;
            return newSolution;
        }
        //добавляет рандомный ген как единицу (сомнительное улучшение)
        public static bool[] RandomLocalUpgradingOperator(bool[] solution)
        {
            bool[] newSolution = new bool[Program.N];
            for (int i = 0; i < solution.Length; i++)
            {
                newSolution[i] = solution[i];
            }
            List<int> notAdded = new();
            for (int i = 0; i < newSolution.Length; i++)
            {
                if (newSolution[i] == false)
                {
                    notAdded.Add(i);
                }
            }
            Random random = new();
            int toInclude = random.Next(0, notAdded.Count);
            newSolution[toInclude] = true;
            return newSolution;
        }
        //само собой понятно, сам смогу обяснить
        public static bool[] OnePointOperator(bool[] solution1, bool[] solution2)
        {
            Random random = new();
            int bound = random.Next(1, Program.N - 1);
            bool[] newSolution = new bool[Program.N];
            for (int i = 0; i < bound; i++)
            {
                newSolution[i] = solution1[i];
            }
            for (int i = bound; i < Program.N; i++)
            {
                newSolution[i] = solution2[i];
            }
            return newSolution;
        }
        //50 на 50 шанс получить ген
        public static bool[] EqualOperator(bool[] solution1, bool[] solution2)
        {
            Random random = new();
            bool[] newSolution = new bool[Program.N];
            for (int i = 0; i < Program.N; i++)
            {
                int next = random.Next(0, 2);
                if (next == 0)
                {
                    newSolution[i] = solution1[i];
                }
                else
                {
                    newSolution[i] = solution2[i];
                }
            }
            return newSolution;
        }
        //тоже 50 на 50 но теперь только если гены родителей не равны (не знаю что это меняет)
        public static bool[] ComparativeOperator(bool[] solution1, bool[] solution2)
        {
            Random random = new();
            bool[] newSolution = new bool[Program.N];
            for (int i = 0; i < Program.N; i++)
            {
                if (solution1[i] == solution2[i])
                {
                    newSolution[i] = solution1[i];
                }
                else
                {
                    int next = random.Next(0, 2);
                    if (next == 0)
                    {
                        newSolution[i] = solution1[i];
                    }
                    else
                    {
                        newSolution[i] = solution2[i];
                    }
                }
            }
            return newSolution;
        }
        // свопает один ген, дает ему противоаоложное значение
        public static bool[] SwapOneMutationOperator(bool[] solution)
        {
            bool[] newSolution = new bool[Program.N];
            for (int i = 0; i < Program.N; i++)
            {
                newSolution[i] = solution[i];
            }
            Random random = new();
            int position = random.Next(0, Program.N);
            if (newSolution[position] == true)
            {
                newSolution[position] = false;
            }
            else
            {
                newSolution[position] = true;
            }
            return newSolution;
        }
        //свопает два гена между собой но ему все равно равны ли они
        //(могу сделать чтобы не было всеравно, но смысл?)
        public static bool[] SwapTwoMutationOperator(bool[] solution)
        {
            bool[] newSolution = new bool[Program.N];
            for (int i = 0; i < Program.N; i++)
            {
                newSolution[i] = solution[i];
            }
            Random random = new();
            int position1 = random.Next(0, Program.N);
            int position2 = random.Next(0, Program.N);
            while (position2 == position1)
            {
                position2 = random.Next(0, Program.N);
            }
            (newSolution[position1], newSolution[position2]) = (newSolution[position2], newSolution[position1]);
            return newSolution;
        }
    }
}
