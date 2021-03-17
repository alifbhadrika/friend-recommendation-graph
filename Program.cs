// C# program to print BFS traversal 
// from a given source vertex. 
// BFS(int s) traverses vertices 
// reachable from s. 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// This class represents a directed 
// graph using adjacency list 
// representation 
class Graph
{

    // No. of vertices	 
    private int vertex;

    //Adjacency Lists 
    List<int>[] adjList;
    public Graph(int V)
    {
        adjList = new List<int>[V];
        for (int i = 0; i < adjList.Length; i++)
        {
            adjList[i] = new List<int>(5);
        }
        vertex = V;
    }

    public void AddEdge(int v, int w)
    {
        adjList[v].Add(w);
        adjList[w].Add(v);
        adjList[v].Sort();
        adjList[w].Sort();
    }

    public void BFS(int source)
    {

        bool[] vis = Enumerable.Repeat((bool)false, vertex).ToArray();

        vis[source] = true;

        List<int> L = new List<int>();
        L.Add(source);

        while (L.Any())
        {
            source = L.First();
            Console.Write(source);
            Console.Write(" ");
            L.RemoveAt(0);

            List<int> list = adjList[source];

            foreach (var item in list)
            {
                if (!vis[item])
                {
                    vis[item] = true;
                    L.Add(item);
                }
            }
        }
    }

    static void Main(string[] args)
    {
        Graph G = new Graph(10);
        G.AddEdge(1, 2);
        G.AddEdge(0, 1);
        G.AddEdge(0, 2);
        G.AddEdge(2, 3);
        G.AddEdge(3, 4);
        Console.Write("Contoh bfs dari 2 : \n");
        G.BFS(2);
    }
}

