// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// 
// class Graph2
// {
// 
//     // No. of vertices	 
//     private int vertex;
//     public int TotalVertex { get { return adjList.Length; } }
//     public int TotalEachV(int i) { return adjList[i].Count; }
//     //Adjacency Lists 
//     List<int>[] adjList;
// 
//     public Graph(int V)
//     {
//         adjList = new List<int>[V];
//         for (int i = 0; i < TotalVertex; i++)
//         {
//             adjList[i] = new List<int>(5);
//         }
//         vertex = V;
//     }
// 
//     public void AddEdge(int v, int w)
//     {
//         adjList[v].Add(w);
//         adjList[w].Add(v);
//         adjList[v].Sort();
//         adjList[w].Sort();
//     }
// 
//     public void BFS(int source)
//     {
// 
//         bool[] vis = Enumerable.Repeat((bool)false, vertex).ToArray();
// 
//         vis[source] = true;
// 
//         List<int> L = new List<int>();
//         L.Add(source);
// 
//         while (L.Any())
//         {
//             source = L.First();
//             Console.Write(source);
//             Console.Write(" ");
//             L.RemoveAt(0);
// 
//             List<int> list = adjList[source];
// 
//             foreach (var item in list)
//             {
//                 if (!vis[item])
//                 {
//                     vis[item] = true;
//                     L.Add(item);
//                 }
//             }
//         }
//         Console.Write("\n");
//     }
// 
//     static List<List<string>> parsingFile(string path)
//     {
//         try
//         {
//             List<List<string>> res = new List<List<string>>();
//             List<string> lines = System.IO.File.ReadAllLines(path).ToList();
// 
//             lines = lines.Where((val, idx) => idx != 0).ToList();
// 
//             foreach (string line in lines)
//             {
//                 res.Add(line.Split(" ").ToList());
//             }
// 
//             return res;
//         }catch(Exception e)
//         {
//             Console.WriteLine(e.Message);
//         }
//     }
// 
//     static void Main(string[] args)
//     {
//         Graph G = new Graph2(10);
//         G.AddEdge(1, 0);
//         G.AddEdge(1, 2);
//         G.AddEdge(0, 4);
//         G.AddEdge(0, 3);
//         G.AddEdge(2, 7);
//         G.AddEdge(2, 8);
//         G.AddEdge(1, 6);
//         Console.Write("Contoh bfs dari 2 : \n");
//         G.BFS(1);
//     }
// }

