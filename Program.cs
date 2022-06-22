//Int32 n, m;
//String path = "B:/Desktop/C#_2021/gr2.txt";
//char[] delimiterChars = { ' ', ',', '.', ':', '\t', '\n' };
//String[] dims;
//using (var fin = new StreamReader(path))
//{

//    dims = fin.ReadToEnd().Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
//    n = Int32.Parse(dims[0]);
//    m = Int32.Parse(dims[1]);


//}
//int[,] graph = new int[n, m];
//for (int c = 0; c < n; c++)
//{
//    for (int j = 0; j < m; j++)
//    {
//        graph[c, j] = Int32.Parse(dims[2 + c * n + j]);
//    }
//}
//Graph gr = new();
//gr.n = n;
//gr.graph = graph;
//var arr = gr.findallinrange(0, 10);
//int i = 0;
//foreach (var b in arr)
//{

//    Console.WriteLine(b + "  " + i++);
//}
//Console.WriteLine();

Int32 n;
String path = "C:/Users/born2befree/source/repos/graphs/graphs/gr3.txt";
string str = null;
char[] delimiterChars = { ' ', ',', '.', ':', '\t', '\n','\u000d' };
String[] dims;
using (var fin = new StreamReader(path))
{

    dims = fin.ReadToEnd().Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
    n = Int32.Parse(dims[0]);
}
//Console.WriteLine(dims.Length);
//foreach(var dim in dims)
//    // Console.WriteLine(dim +" "+ "{0}",dim.Length);
//    foreach(var x in dim)
//    Console.WriteLine($"dim[] {x} ('\\u{(int)x:x4}')");
List<GraphofCities.City> cities = new List<GraphofCities.City>();
Boolean[,] graph = new Boolean[n,n];
for (int i = 1; i <= 3*n; i+=3)
{
    cities.Add(new GraphofCities.City(dims[i], (i-1) / 3, Double.Parse(dims[i + 1]), Double.Parse(dims[i + 2])));
}
for (int c = 0; c < n; c++)
{
    for (int j = 0; j < n; j++)
    {
        graph[c, j] = (String.Compare(dims[3*n+1+ c * n + j], "1")==0?true:false);
       
            

    }
}
Console.WriteLine();
GraphofCities example = new(cities,graph);
List<String> avoid = new();
int avoidcount = int.Parse(Console.ReadLine());
for(int i = 0;i<avoidcount;++i)
{
    avoid.Add(Console.ReadLine());
}
example.Show();
var dist = example.Shortestfromavoid("Березовка", "Октябрьское", avoid);
foreach(var d in dist)
{
    Console.WriteLine(d);
}
/*
for (int c = 0; c < n; c++,Console.WriteLine())
{
    for (int j = 0; j < n; j++)
    {
        Console.Write((graph[c, j] ? 1:0) + " ") ;
    }
}
*/

//public class Graph
//{
//    public int n;
//    public int[,] graph;
//    public Graph(int n ){ this.n = n; graph = new int[n, n]; }
//    public Graph() { }
//    public Boolean[] findallinrange(int a, int cap)//найти все вершины графа, попадающие в N-периферию для вершины А;
//    {
//        var marked = new Boolean[n];
//        PriorityQueue<Int32, Int32> next = new();
//       // var t = (a, cap);
//        next.Enqueue(a, -cap);
//        //marked[a] = true;
//        while (next.TryPeek(out int index, out int length))
//        {
//            marked[index] = true;
//            for (int i = 0; i < n; i++)
//            {
//                if (graph[index, i] != 0 && !marked[i] && graph[index, i] <= -length)
//                {
//                    next.Enqueue(i, length + graph[index, i]);
//                }
//            }
//            next.Dequeue();
//        }
//        return marked;
//    }
//    public Boolean Connect(int a, int b, int weight)
//    {
//        if (a >= 0 && b >= 0 && a < n && b < n && graph[a, b] == 0)
//            //graph[a, b] = graph[b, a] = weight;
//            graph[a, b] = weight;
//        else return false;
//        return true;
//    }
//}
/*Во входном файле задается: в первой сторке N – количество городов; начиная со
второй строки через пробел названия N-городов и их координаты в декартовой системе; с
новой строки матрица смежности графа, описывающая схему дорог (вес ребра
рассчитывается по координат городов).
AUFGABE:
Найти кратчайший путь, соединяющие города А и В, проходящий только через заданное
множество городов.
*/
/*Int32 N;
boolean матрица смекжности;
(string, double, double) массив координат городов;
я буду часто искать города, но не буду их добавлять, удалять.
String erlaubt_stadte // города через который возможен путь
String departure;
String destination;*/





class GraphofCities {
    public class City : IComparable<City>
    {
        public readonly String name;
        public readonly Int32 id;
        public readonly Double x;
        public readonly Double y;
        public City(String name, Int32 id, Double x, Double y){ this.name = name; this.id = id; this.x = x; this.y = y; }
        public static Double finddist(City dep, City des)
        { return Math.Sqrt((dep.x - des.x) * (dep.x - des.x) + (dep.y - des.y) * (dep.y - des.y)); }
        public override String ToString() =>  ( name + " " + id.ToString() + " " + x.ToString() + " " + y.ToString());
        public int CompareTo(City? other)
        {
            return this.name.CompareTo(other.name);
        }
    }
    List<City> westblock = null;
    Boolean[,] graph = null;
    public void Show()
    {

        foreach (City c in westblock)
        {
            Console.WriteLine(c.ToString());
        }
        
    }
    public GraphofCities(List<City> a,Boolean[,] admatrix) { westblock = a; graph = admatrix; }
    public  Double[] Shortestfromavoid(String departure, String destination, List<String> ostblock)
    {
        const Double INF = Double.PositiveInfinity;
        Int32 n = westblock.Count
        ,
        m = ostblock.Count;
        Int32 depI = -1, destI = -1;
        for (int i = 0; i < n; ++i)
        {
            if (String.Compare(westblock[i].name, departure) == 0)
            {
                depI = i;
            }
            if (String.Compare(westblock[i].name, destination) == 0)
            {
                destI = i;
            }
        }
        if (depI == -1 || destI == -1) return null;
        if (depI == destI) return null;
        //westblock is already sorted
       
        


        List<int> safecities = new List<int>();
       

        var dist = new Double[n];
        for (int i = 0; i < n; ++i)
        {
            dist[i] = INF;
        }
        dist[depI] = 0;
        var marked = new Boolean[n];
        foreach(var c in westblock)
        {
            foreach(var banned in ostblock)
            {
                if(String.Compare(c.name,banned)==0)
                {
                    marked[c.id] = true;
                }
            }
        }
        foreach (var m1 in marked)
            Console.WriteLine(m1);
        SortedSet<(Double, Int32)> next = new SortedSet<(Double, Int32)>();
        next.Add((0, depI));
        while (next.Count!=0)
        {
            var currentstation = next.Min;
            var length = currentstation.Item1;
            var index  = currentstation.Item2;
            next.Remove(currentstation);
            marked[index] = true;
            foreach (var i in westblock)
            {
                var path = City.finddist(westblock[index], i);
                if (graph[westblock[index].id, i.id ] && !marked[i.id] && path + dist[index] < dist[i.id])
                {
                    next.Remove((dist[i.id], i.id));
                    next.Add((path + dist[index], i.id));
                    dist[i.id] = path + dist[index];
                }
            }
        }
        return dist;
    }
}





















