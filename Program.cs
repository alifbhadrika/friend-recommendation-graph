using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Vertex
{
    public string value;
    public List<string> edges;
    public Vertex(string _value)
    {
        this.value = _value;
        this.edges = new List<string>();
    }
}

public class Graph
{
    private int numVertices;
    private List<Vertex> vertices;
    public Graph(int _numVertices)
    {
        this.numVertices = _numVertices;
        this.vertices = new List<Vertex>();
    }

    private void addVertex(string c)
    {
        if (findVertex(c) == null)
        {
            vertices.Add(new Vertex(c));
        }
        return;
    }

    private Vertex findVertex(string c)
    {
        return vertices.Find(v => v.value == c);
    }

    private int findVertexIdx(string c)
    {
        for (int i = 0; i < numVertices; i++)
        {
            if (vertices[i].value == c)
            {
                return i;
            }
        }
        return -1;
    }
    private void printPath(List<string> path)
    {
        for (int i = 0; i < path.Count - 1; i++)
        {
            Console.Write("{0} -> ", path[i]);
        }
        Console.Write("{0}", path[path.Count - 1]);
        Console.WriteLine("\n{0} degree connection.", (path.Count) - 2);
    }

    private void dfs(string src, string dest, bool[] visited, List<string> path)
    {
        path.Add(src);
        if (src == dest)
        {
            printPath(path);
            visited[findVertexIdx(dest)] = true;
            return;
        }
        visited[findVertexIdx(src)] = true;
        if (visited[findVertexIdx(dest)] == false)
        {
            foreach (string edge in vertices[findVertexIdx(src)].edges)
            {
                if (visited[findVertexIdx(edge)] == false)
                {
                    dfs(edge, dest, visited, path);
                }
            }
            path.RemoveAt(path.Count - 1);
        }
    }
    public void addEdge(string src, string dest)
    {
        addVertex(src);
        addVertex(dest);

        Vertex vsrc = findVertex(src);
        Vertex vdest = findVertex(dest);

        vsrc.edges.Add(dest);
        vdest.edges.Add(src);

        vsrc.edges.Sort();
        vdest.edges.Sort();
    }
    public void print()
    {
        for (int i = 0; i < vertices.Count; i++)
        {
            Console.Write("({0})-", vertices[i].value);
            foreach (string edge in vertices[i].edges)
            {
                Console.Write("{0} ", edge);
            }
            Console.WriteLine();
        }
    }
    public void friendRecommendationBFS(string source)
    {
        string AkunSource = source;
        bool[] vis = new bool[numVertices];
        int[] level = new int[numVertices];

        foreach (Vertex v in vertices)
        {
            vis[findVertexIdx(v.value)] = false;
            level[findVertexIdx(v.value)] = 0;
        }

        vis[findVertexIdx(source)] = true;
        level[findVertexIdx(source)] = 0;
        List<string> L = new List<string>();
        List<string> Friend = new List<string>();
        L.Add(source);
        int lvl = 0;
        while (L.Any())
        {
            source = L.First();
            L.RemoveAt(0);

            foreach (string edge in vertices[findVertexIdx(source)].edges)
            {
                if (!vis[findVertexIdx(edge)])
                {
                    vis[findVertexIdx(edge)] = true;
                    level[findVertexIdx(edge)] = level[findVertexIdx(source)] + 1;
                    L.Add(edge);
                    if (level[findVertexIdx(edge)] == 2)
                    {
                        Friend.Add(edge);
                    }
                }
            }
            lvl++;
        }
        Console.Write("\n");
        Console.Write("\n");
        Console.Write("Daftar rekomendasi teman untuk akun {0}:  \n", AkunSource);
        List<Tuple<string, int, List<string>>> Recs = new List<Tuple<string, int, List<string>>>();
        while (Friend.Any())
        {
            string s = Friend.First();
            Friend.RemoveAt(0);
            int mutual = 0;
            List<string> Rec = new List<string>();
            foreach (string edge in vertices[findVertexIdx(s)].edges)
            {
                if (level[findVertexIdx(edge)] == 1)
                {
                    Rec.Add(edge);
                    mutual++;
                }
            }
            Tuple<string, int, List<string>> RM = new Tuple<string, int, List<string>>(s, mutual, Rec);
            Recs.Add(RM);
        }
        Recs = Recs.OrderByDescending(i => i.Item2).ToList();
        foreach (var item in Recs)
        {
            string name = item.Item1;
            int value = item.Item2;
            Console.WriteLine("Nama akun: {0} ", name);
            Console.WriteLine("{0} mutual friends:", value.ToString());
            foreach (var items in item.Item3)
            {
                Console.WriteLine("{0}", items);
            }
        }
    }
    public void friendRecommendationDFS(string source)
    {
        bool[] vis = Enumerable.Repeat((bool)false, numVertices).ToArray();
        Console.Write("\n");
        Dictionary<string, int> maps = new Dictionary<string, int>();
        Dictionary<string, List<string>> mutual = new Dictionary<string, List<string>>();
        Dictionary<string, List<string>> mList = new Dictionary<string, List<string>>();
        dfs1(source, 0, vis, ref maps, ref mutual);
        foreach (var x in mutual)
        {
            foreach (var y in x.Value)
            {
                int res = maps[y];

                if (mList.ContainsKey(y) && res == 2)
                {
                    mList[y].Add(x.Key);
                }
                else
                {
                    if (res == 2)
                        mList[y] = new List<string> { x.Key };
                }
            }
        }
        var ListMutual = from pair in mList
                         orderby pair.Value.Count descending
                         select pair;
        Console.Write("Daftar rekomendasi teman untuk akun {0}:  \n", source);
        foreach (var x in ListMutual)
        {
            int value = x.Value.Count;
            Console.WriteLine("Nama akun: {0} ", x.Key);
            Console.WriteLine("{0} mutual friends:", value.ToString());
            foreach (var y in x.Value)
            {
                Console.Write(y + " ");
            }
            Console.WriteLine("");
        }
    }

    private void dfs1(string source, int depth, bool[] vis, ref Dictionary<string, int> L, ref Dictionary<string, List<string>> Mutual)
    {
        L[source] = depth;
        if (depth == 2)
        {
            return;
        }
        vis[findVertexIdx(source)] = true;

        foreach (string edge in vertices[findVertexIdx(source)].edges)
        {
            if (!vis[findVertexIdx(edge)])
            {

                if (depth == 1)
                {
                    if (Mutual.ContainsKey(source))
                    {
                        Mutual[source].Add(edge);
                    }
                    else
                    {
                        Mutual[source] = new List<string> { edge };
                    }
                }
                dfs1(edge, depth + 1, vis, ref L, ref Mutual);
            }
        }
        return;
    }

    public void exploreFriendBFS(string src, string dest)
    {
        int level = 0;

        List<List<string>> path = new List<List<string>>();

        List<string> q = new List<string>();

        bool[] visited = Enumerable.Repeat((bool)false, numVertices).ToArray();
        bool isFinish = false;

        visited[findVertexIdx(src)] = true;

        q.Add(src);
        q.Add(null);

        path.Add(new List<string>() { src });

        while (!isFinish && q.Count() > 1)
        {
            string temp = q.First();
            q.RemoveAt(0);

            if (temp == null)
            {
                level++;
                q.Add(null);
            }
            else
            {
                List<string> path_temp = path.First().ConvertAll(val => val); // Deep Copy
                path.RemoveAt(0);

                foreach (string edge in vertices[findVertexIdx(temp)].edges)
                {
                    if (visited[findVertexIdx(edge)] == false)
                    {
                        visited[findVertexIdx(edge)] = true;

                        q.Add(edge);
                        List<string> path_temp_2 = path_temp.ConvertAll(val => val); // Deep Copy
                        path_temp_2.Add(edge);
                        path.Add(path_temp_2);

                        if (visited[findVertexIdx(dest)] == true)
                        {
                            isFinish = true;
                            break;
                        }
                    }
                }
            }
        }


        if (isFinish)
        {
            Console.WriteLine("Nama akun: " + src + " dan " + dest);
            for (int i = 0; i < path[path.Count - 1].Count - 1; i++)
            {
                Console.Write("{0} -> ", path[path.Count - 1][i]);
            }
            Console.Write("{0}\n", path[path.Count - 1][path[path.Count - 1].Count - 1]);
            Console.WriteLine(level + "nd-degree connection");
        }
        else
        {
            Console.WriteLine("Nama akun: " + src + " dan " + dest);
            Console.WriteLine("Tidak ada jalur koneksi yang tersedia");
            Console.WriteLine("Anda harus memulai koneksi baru itu sendiri.");
        }

    }
    public void exploreFriendsDFS(string src, string dest)
    {
        bool[] visited = new bool[numVertices];
        foreach (Vertex v in vertices)
        {
            visited[findVertexIdx(v.value)] = false;
        }
        List<string> path = new List<string>();
        dfs(src, dest, visited, path);
        if (visited[findVertexIdx(dest)] == false)
        {
            Console.WriteLine("Tidak ada jalur koneksi yang tersedia\nAnda harus memulai koneksi baru itu sendiri.");
        }

    }
}

class Program
{
    static List<List<string>> parsingFile(string path)
    {
        List<List<string>> res = new List<List<string>>();

        try
        {
            List<string> lines = System.IO.File.ReadAllLines(path).ToList();

            lines = lines.Where((val, idx) => idx != 0).ToList();
            foreach (string line in lines)
            {
                res.Add(line.Split(" ").ToList());
            }

            return res;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return res;
    }
    static void Main(string[] args)
    {
        // List<List<string>> data = Program.parsingFile("test1.txt");
        // Graph gg = new Graph(data.Count);
        // foreach (List<string> vertices in data)
        // {
        //     gg.addEdge(vertices.First(), vertices.Last());
        // }
        Graph g = new Graph(13);
        g.addEdge("alif", "bhadrika");
        g.addEdge("karel", "renaldi");
        g.addEdge("karel", "bhadrika");
        g.addEdge("rila", "karel");
        g.addEdge("karel", "mandala");
        g.print();
        g.exploreFriendsDFS("karel", "alif");
        Graph G = new Graph(13);
        G.addEdge("A", "B");
        G.addEdge("A", "C");
        G.addEdge("A", "D");
        G.addEdge("B", "C");
        G.addEdge("B", "E");
        G.addEdge("B", "F");
        G.addEdge("C", "F");
        G.addEdge("C", "G");
        G.addEdge("D", "G");
        G.addEdge("D", "F");
        G.addEdge("E", "H");
        G.addEdge("E", "F");
        G.addEdge("F", "H");
        Graph gg = new Graph(3);
        gg.addEdge("1", "2");
        gg.addEdge("2", "3");
        gg.addEdge("1", "3");
        G.friendRecommendationBFS("B");
        Console.WriteLine("");
        G.friendRecommendationDFS("A");
    }
}