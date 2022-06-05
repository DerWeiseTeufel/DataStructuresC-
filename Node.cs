using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher
{

    public class Node:IComparable<Node>
    {
        public int freq;
        public char? symbol;
        public Node left;
        public Node right;
        public int height;
        public Node(int freq, char? symbol) { this.freq = freq; this.symbol = symbol; }
        public Node(int freq, char? symbol,Node Left, Node Right) { this.freq = freq; this.symbol = symbol; this.left = Left; right = Right;this.height = Math.Max(Left.height, Right.height)+1; }

        public int CompareTo(Node? other)
        {
            return (this.freq,this.height,this.symbol).CompareTo((other.freq,other.height,other.symbol));
        }

        public static Boolean operator < (Node a,Node other) { return a.freq < other.freq; }
        public static Boolean operator > (Node a,Node other) { return a.freq > other.freq; }
        
    }
}
