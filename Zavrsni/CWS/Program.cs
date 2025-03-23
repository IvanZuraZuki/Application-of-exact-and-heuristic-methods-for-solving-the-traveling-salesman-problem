using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CWS
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // number vertex
            int vertex = 4;

            Graf g = new Graf(vertex);

            // static weight between vertex
            g.dodavanjeBridova(0, 1, 6);
            g.dodavanjeBridova(0, 2, 3);
            g.dodavanjeBridova(0, 3, 1);
            g.dodavanjeBridova(1, 2, 7);
            g.dodavanjeBridova(1, 3, 9);
            g.dodavanjeBridova(3, 2, 2);

            g.CWS(vertex);
        }

        public class Graf
        {
            //save edges and cycle
            private Dictionary<int, Dictionary<int, int>> edgesCWS;

            private Dictionary<int, Dictionary<int, int>> savings;
            private List<int> CiklusiCWS = new List<int>();

            public Graf(int vertex)
            {
                edgesCWS = new Dictionary<int, Dictionary<int, int>>();
                savings = new Dictionary<int, Dictionary<int, int>>();

                for (int i = 0; i < vertex; i++)
                {
                    edgesCWS[i] = new Dictionary<int, int>();
                    savings[i] = new Dictionary<int, int>(); // Initialize the savings dictionary
                }
            }

            public void dodavanjeBridova(int u, int v, int tezina)
            {
                // weight between vertex
                edgesCWS[u][v] = tezina;
                edgesCWS[v][u] = tezina;
            }

            //metode CWS
            public void CWS(int vertex)
            {
                int depo = 0;
                List<Tuple<int, int, int>> sortedSavings = new List<Tuple<int, int, int>>();

                // Step 1: Compute savings for all pairs (i, j)
                for (int i = 1; i < vertex; i++)
                {
                    for (int j = i + 1; j < vertex; j++)
                    {
                        int saving = edgesCWS[i][depo] + edgesCWS[depo][j] - edgesCWS[i][j];
                        sortedSavings.Add(Tuple.Create(i, j, saving));
                    }
                }

                // Step 2: Sort savings in descending order (prioritize highest savings)
                sortedSavings = sortedSavings.OrderByDescending(x => x.Item3).ToList();

                // Initialise route with the highest savings pair.
                CiklusiCWS.Add(sortedSavings[0].Item1);
                CiklusiCWS.Add(sortedSavings[0].Item2);

                // Step 3: Iterate over the sorted savings
                foreach (var saving in sortedSavings)
                {
                    int i = saving.Item1;
                    int j = saving.Item2;
                    int first_element = CiklusiCWS[0];
                    int last_element = CiklusiCWS[CiklusiCWS.Count - 1];

                    // Step 4: Check if i and j are already in a cycle
                    if (CiklusiCWS.Contains(i) && CiklusiCWS.Contains(j))
                    {
                        continue;
                    }

                    // Step 6: Check if i and j are in different cycles
                    if (i == last_element && !CiklusiCWS.Contains(j))
                    {
                        CiklusiCWS.Add(j);
                    }
                    else if (!CiklusiCWS.Contains(i) && j == first_element)
                    {
                        CiklusiCWS.Insert(0, i);
                    }
                }

                // Step 7: Reintroduce depots to the path
                CiklusiCWS.Insert(0, depo);
                CiklusiCWS.Add(depo);

                string final = string.Join("->", CiklusiCWS);

                Console.WriteLine(final);
            }
        }
    }   
}




