using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PALab4
{
    internal class Population
    {
        public Graph ThisGraph { get; set; }
        public List<bool[]> CurrentPopulation { get; set; }
        public Population(Graph graph)
        {
            ThisGraph = graph;
            CurrentPopulation = new();
        }
        public Population(Graph graph, List<bool[]> currentPopulation)
        {
            ThisGraph = graph;
            CurrentPopulation = currentPopulation;
        }
        public void GenerateRandomPopulation()
        {
            CurrentPopulation = new();
            for (int i = 0; i < Program.NumberOfPopulationMembers; i++)
            {
                CurrentPopulation.Add(Algorithms.ApproxVertexCover(ThisGraph));
                //Console.WriteLine($"Start member {i + 1}: {Algorithms.GetSolutionF(CurrentPopulation[i])}");
            }
        }
        public bool[] GetCurrentBestMember()
        {
            return Algorithms.GetBestSolution(this);
        }
        public int GetCurrentBestMemberValence()
        {
            return Algorithms.GetSolutionF(Algorithms.GetBestSolution(this));
        }
    }
}
