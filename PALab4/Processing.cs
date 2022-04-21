using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PALab4
{
    internal static class Processing
    {
        public static bool CheckVertexes(bool[,] vs)
        {
            for (int i = 0; i < Program.N; i++)
            {
                int count = 0;
                for (int j = 0; j < Program.N; j++)
                {
                    if (vs[i, j] == true)
                    {
                        count++;
                    }
                }
                if (count < 2 || count > 30)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool[] NewArray(bool[] array)
        {
            bool[] newArray = new bool[array.Length];

            for (int i = 0; array.Length > 0; i++)
            {
                newArray[i] = array[i];
            }

            return newArray;
        }
        public static int CountValence(Graph graph, int point)
        {
            int valence = 0;
            for (int i = 0; i < Program.N; i++)
            {
                if (graph.GraphMatrix[point, i] == true) valence++;
            }
            return valence;
        }
    }
}
