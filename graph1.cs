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

        path.Add(new List<string>(){ src });

        while(!isFinish && q.Count() > 1)
        {
            string temp = q.First();
            q.RemoveAt(0);

            if(temp == null)
            {
                level++;
                q.Add(null);
            }else
            {
                List<string> path_temp = path.First().ConvertAll(val => val); // Deep Copy
                path.RemoveAt(0);

                foreach (string edge in vertices[findVertexIdx(temp)].edges)
                {
                    if(visited[findVertexIdx(edge)] == false)
                    {
                        visited[findVertexIdx(edge)] = true;

                        q.Add(edge);
                        List<string> path_temp_2 = path_temp.ConvertAll(val => val); // Deep Copy
                        path_temp_2.Add(edge);
                        path.Add(path_temp_2);

                        if(visited[findVertexIdx(dest)] == true)
                        {
                            isFinish = true;
                            break;
                        }
                    }
                }
            }
        }
        

        if(isFinish)
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
        }catch(Exception e){
            Console.WriteLine(e.Message);
        }

        return res;
    }

    static void Main(string[] args)
    {
        List<List<string>> data = Program.parsingFile("test1.txt");


        Graph g = new Graph(data.Count);
        foreach(List<string> vertices in data)
        {
            g.addEdge(vertices.First(), vertices.Last());
        }
        g.exploreFriendBFS("A", "H");
    }
}
