using System;
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
}

class Program
{
    static void Main(string[] args)
    {
        Graph g = new Graph(13);
        g.addEdge("alif", "bhadrika");
        g.addEdge("karel", "renaldi");
        g.addEdge("karel", "bhadrika");
        g.addEdge("rila", "karel");
        g.addEdge("karel", "mandala");
        g.print();
        g.exploreFriendsDFS("karel", "alif");

    }
}
