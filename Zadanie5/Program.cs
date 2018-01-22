using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Zadanie 5
 * Napisz metodę, która dla spójnego grafu nieskierowanego, dla którego dane są
 * liczbowe wagi krawędzi, znajduje drzewo rozpinające o maksymalnej wartości.
 * */

namespace Zadanie5
{
    class MST
    {
        // Number of vertices in the graph
        private int V;

        public MST(int v)
        {
            V = v;
        }

        // A utility function to find the vertex with minimum key
        // value, from the set of vertices not yet included in MST
        int minKey(int[] key, bool[] mstSet)
        {
            // Initialize min value
            int min = int.MaxValue, min_index = -1;

            for (int v = 0; v < V; v++)
                if (mstSet[v] == false && key[v] < min)
                {
                    min = key[v];
                    min_index = v;
                }

            return min_index;
        }

        // A utility function to print the constructed MST stored in
        // parent[]
        void printMST(int[] parent, int n, int[,] graph)
        {
            Console.WriteLine("Edge   Weight");
            for (int i = 1; i < V; i++)
                Console.WriteLine(parent[i] + " - " + i + "    " + graph[i, parent[i]]);
        }

        // Function to construct and print MST for a graph represented
        // using adjacency matrix representation
        public void primMST(int[,] graph)
        {
            // Array to store constructed MST
            int[] parent = new int[V];

            // Key values used to pick minimum weight edge in cut
            int[] key = new int[V];

            // To represent set of vertices not yet included in MST
            bool[] mstSet = new bool[V];

            // Initialize all keys as INFINITE
            for (int i = 0; i < V; i++)
            {
                key[i] = int.MaxValue;
                mstSet[i] = false;
            }

            // Always include first 1st vertex in MST.
            key[0] = 0;     // Make key 0 so that this vertex is
                            // picked as first vertex
            parent[0] = -1; // First node is always root of MST

            // The MST will have V vertices
            for (int count = 0; count < V - 1; count++)
            {
                // Pick thd minimum key vertex from the set of vertices
                // not yet included in MST
                int u = minKey(key, mstSet);

                // Add the picked vertex to the MST Set
                mstSet[u] = true;

                // Update key value and parent index of the adjacent
                // vertices of the picked vertex. Consider only those
                // vertices which are not yet included in MST
                for (int v = 0; v < V; v++)

                    // graph[u][v] is non zero only for adjacent vertices of m
                    // mstSet[v] is false for vertices not yet included in MST
                    // Update the key only if graph[u][v] is smaller than key[v]
                    if (graph[u, v] != 0 && mstSet[v] == false && graph[u, v] < key[v])
                    {
                        parent[v] = u;
                        key[v] = graph[u, v];
                    }
            }

            // print the constructed MST
            printMST(parent, V, graph);
        }

        public void PrimMaxST(int[,] graphOriginal)
        {
            int[,] graph = new int[graphOriginal.GetLength(0), graphOriginal.GetLength(1)];

            for (int i = 0; i < graphOriginal.GetLength(0); i++)
            {
                for (int j = 0; j < graphOriginal.GetLength(0); j++)
                    graph[i, j] = graphOriginal[i, j] * -1;
            }

            int[] parent = new int[V];  // Constructed MST
            int[] key = new int[V];
            bool[] mstSet = new bool[V];

            for (int i = 0; i < V; i++)
            {
                key[i] = int.MaxValue;
                mstSet[i] = false;
            }

            key[0] = 0;
            parent[0] = 1;

            for (int i = 0; i < V - 1; i++)
            {
                int u = minKey(key, mstSet);

                mstSet[u] = true;

                for (int v = 0; v < V; v++)
                {
                    if (graph[u, v] != 0 && mstSet[v] == false && graph[u, v] < key[v])
                    {
                        parent[v] = u;
                        key[v] = graph[u, v];
                    }
                }
            }
            printMST(parent, V, graphOriginal);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            /* Let us create the following graph
               2    3
            (0)--(1)--(2)
            |    / \   |
           6|  8/   \5 |7
            | /      \ |
            (3)-------(4)
                 9          */
            MST t = new MST(5);
            int[,] graph = new int[,] { {0, 2, 0, 6, 0},
                                        {2, 0, 3, 8, 5},
                                        {0, 3, 0, 0, 7},
                                        {6, 8, 0, 0, 9},
                                        {0, 5, 7, 9, 0}};

            // Print the solution
            t.primMST(graph);
            Console.WriteLine();
            t.PrimMaxST(graph);

            Console.ReadKey();
        }
    }
}
