using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Cypher
{
    public class Haffman
    {
        Dictionary<char,int> frequency = new Dictionary<char,int>();
        Dictionary<char, List<Boolean>> codes;
        public void ReadFromFile(string path) 
        {
            using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open)))
            {
                int b;
                while ((b = reader.Read()) != -1)
                { //var b = reader.Read();
                if (frequency.ContainsKey((char)b))
                    ++frequency[(char)b];
                else frequency.Add((char)b, 1);
                }
            };
            List<Node> subtrees = new List<Node>();
            foreach(var s in frequency)
            {
                subtrees.Add(new Node(s.Value,s.Key));
            }


            var sset = new SortedSet<Node>(subtrees);//for PQ construction is linear
            while(sset.Count > 1)
            {
                
                var minA = sset.Min();
                Console.WriteLine(minA.symbol.ToString() + " " + minA.freq.ToString() + " " + minA.height.ToString());
                sset.Remove(minA);
                var minB = sset.Min();
                Console.WriteLine(minB.symbol.ToString() + " " + minB.freq.ToString() + " " + minB.height.ToString());

                sset.Remove(minB);
                sset.Add(new Node(minA.freq + minB.freq, null,minA,minB));
            }
            //Dictionary<char, List<Boolean>> codes = new Dictionary<char, List<Boolean>>();
             codes = new Dictionary<char, List<Boolean>>();
            BuildTable( sset.Max(),ref codes);
            foreach (var x in codes)
            {
                Console.WriteLine();
                Console.Write( x.Key.ToString() + "\t"+'-' + "\t");
                foreach (var s in x.Value)
                    if (s)
                        Console.Write(1);
                    else
                        Console.Write(0);
            }

        }
        List<Boolean> code = new List<bool>();
        void BuildTable( Node root,ref  Dictionary<char, List<Boolean>> codes)
        {
            if(root.left !=  null)
            {
                code.Add(false);
                BuildTable(  root.left,ref  codes);
            }
            if (root.right != null)
            {
                code.Add(true);
                BuildTable( root.right, ref codes);
            }
            if (root.symbol != null)
            { codes.Add(root.symbol ?? 'a', new List < Boolean > (code.ToList()));  }// [root.symbol??'a'] }
            if(code.Count > 0)
            code.RemoveAt(code.Count-1);
        }
        public void writefile(string path)
        {
            using (var stream = File.Open(path, FileMode.Create))
            {
                using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
                {
                    foreach (var x in codes)
                    {
                        //writer.Write('\n');
                        //writer.Write(x.Key.ToString() + "\t" + '-' + "\t");
                        foreach (var s in x.Value)
                            writer.Write(s);
                            
                                
                    }
                    //writer.Write('\n');
                }
            }
        }
        //public void decypher(string path)
        //{
        //    using (var stream = File.Open(path, FileMode.Create))
        //    {
        //        using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
        //        {
        //            var value = reader.ReadBytes(2);
                  
        //            writer.Write('\n');
        //        }
        //    }
        //}
        //private List<char> search(byte[] arr,   Node root )
        //{
        //    List<char> result = new List<char>();
        //    if (root == null)
        //        return result;
        //    result.Add(root.symbol.Value);
        //    if(arr[0])

        //}

    }
}
