using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Vertex
{
    public char value;
    public List<char> edges;
    public Vertex(char _value)
    {
        this.value = _value;
        this.edges = new List<char>();
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

    private void addVertex(char c)
    {
        if (findVertex(c) == null)
        {
            vertices.Add(new Vertex(c));
        }
        return;
    }

    private Vertex findVertex(char c)
    {
            return vertices.Find(v => v.value == c);
    }

    private int findVertexIdx(char c)
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

    public void addEdge(char src, char dest)
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
            foreach (char edge in vertices[i].edges)
            {
                Console.Write("{0} ", edge);
            }
            Console.WriteLine();
        }
    }
    public void exploreFriendsDFS(char src, char dest)
    {
        bool[] visited = new bool[numVertices];
        foreach (Vertex v in vertices)
        {
            visited[findVertexIdx(v.value)] = false;
        }
        List<char> path = new List<char>();
        dfs(src, dest, visited, path);
        if (visited[findVertexIdx(dest)] == false)
        {
            Console.WriteLine("Tidak ada jalur koneksi yang tersedia\nAnda harus memulai koneksi baru itu sendiri.");
        }

    }
    private void printPath(List<char> path)
    {
        for (int i = 0; i < path.Count - 1; i++)
        {
            Console.Write("{0} -> ", path[i]);
        }
        Console.Write("{0}", path[path.Count - 1]);
        Console.WriteLine("\n{0} degree connection.", (path.Count) - 2);
    }
    private void dfs(char src, char dest, bool[] visited, List<char> path)
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
            foreach (char edge in vertices[findVertexIdx(src)].edges)
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
        g.addEdge('A', 'Z');
        g.addEdge('Z', 'O');
        g.addEdge('O', 'S');
        g.addEdge('S', 'F');
        g.addEdge('S', 'R');
        g.addEdge('F', 'B');
        g.addEdge('B', 'P');
        g.addEdge('P', 'C');
        g.addEdge('R', 'C');
        g.addEdge('P', 'R');
        g.addEdge('D', 'R');
        g.addEdge('M', 'D');
        g.addEdge('M', 'L');
        g.addEdge('T', 'L');
        g.addEdge('A', 'T');
        g.print();
        g.exploreFriendsDFS('A', 'B');
    }
}
