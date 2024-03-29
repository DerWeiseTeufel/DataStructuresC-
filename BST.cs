﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace bst
{
    class BST<T> : ICollection<T> where T : IComparable<T>
    {
        Node root = null;

        public int Count { get { return NodeCount; } }
        private int NodeCount = 0;

        public bool IsReadOnly => false;//undefined

        public void Add(T item)
        {

            if (!this.Contains(item))
            {
                root = this.add(root, item);
                NodeCount++;

            }
        }
        private Node add(Node root, T item)
        {
            if (root != null)
            {
                if (item.CompareTo(root.data) == 1)
                    root.right = add(root.right, item);
                else root.left = add(root.left, item);
            }
            else
            {
                root = new Node(item);
            }
            update(root);
            return root;
        }
        private void update(Node node)
        {
            if (node != null)
            {
                Int32 lh = node.left == null ? -1 : node.left.height;
                Int32 rh = node.right == null ? -1 : node.right.height;
                node.height = 1 + Math.Max(lh, rh);
                node.bf = rh - lh;

            }
        }
        public void Clear()
        {
            root = null;
            NodeCount = 0;
        }

        public bool Contains(T item)
        {
            return contains(root, item);
        }
        private bool contains(Node root, T item)
        {
            if (root == null)
                return false;
            int cmp = item.CompareTo(root.data);


            if (cmp == 0)
                return true;

            if (cmp == 1)
                return contains(root.right, item);
            else return contains(root.left, item);

        }
        public void CopyTo(T[] array, int arrayIndex)//undefined
        {

        }
        public void CopyTo(T[] array)//undefined
        {

        }
        public void CopyTo(T[] array, int arrayIndex, int endindex)//undefined
        {
            //if (array != null)
            //{
            //    if (endindex >= array.Length || arrayIndex < 0 || endindex - arrayIndex < array.Count())
            //        throw new ArgumentOutOfRangeException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");

            //    for (int i = arrayIndex; i < endindex; i++)
            //    {
            //        Node temp = root;
            //        if (temp != null)
            //        {
            //            array[i] = temp.data;

            //        }

            //    }
            //}
            //throw new ArgumentNullException("Array is null, check the reference.");
        }
        //private Array preorderarr(Node root)
        //{
        //    if(root == null)
        //}
        public IEnumerator<T> GetEnumerator()//undefined
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            if (contains(root, item))
            { root = remove(root, item); NodeCount--; return true; }
            return false;
        }
        private Node remove(Node root, T item)
        {

            if (root == null) return root;
            int cmp = item.CompareTo(root.data);

            if (cmp == 0)
            {

                if (root.left == null)
                    return root.right;
                if (root.right == null)
                    return root.left;
                Node fin = findMin(root.right);
                root.data = fin.data;
                root.right = remove(root.right, fin.data);

            }
            else
            {
                if (cmp == 1)
                    root.right = remove(root.right, item);
                else root.left = remove(root.left, item);

            }
            return root;
        }
        public void PreOrder()
        {
            if (root != null)
                preorder(root);
        }
        void preorder(Node root)
        {
            if (root != null)
            {
                Console.WriteLine(root.data);
                preorder(root.left);
                preorder(root.right);
            }
        }
        public void InOrder()
        {
            if (root != null)
                inorder(root);
        }
        void inorder(Node root)
        {
            if (root != null)
            {

                inorder(root.left);
                Console.WriteLine(root.data);
                inorder(root.right);
            }
        }
        public int CountEven()
        {
            if (root != null)
                return ccount(root, 2);
            return 0;
        }

        private int ccount(Node root, int div)
        {
            Type itemType = typeof(T);
            if (root == null)
                return 0;
            if (itemType == typeof(int) && root.data is int intData)
            {
                int add = 0;
                if (intData % div == 0)
                    add = 1;
                return add + ccount(root.left, div) + ccount(root.right, div);
            }
            else { throw new InvalidDataException(); }

        }
        public int Height()
        {
            if (root != null)
                return privatHeight(root) - 1;
            return 0;
        }
        public int Height(T val)
        {
            if (Contains(val))
            {
                Node temp = root;
                int cmp = val.CompareTo(temp.data);

                while (cmp != 0)
                {

                    if (cmp == 1)
                        temp = temp.right;
                    else temp = temp.left;
                    cmp = val.CompareTo(temp.data);
                }
                return privatHeight(temp) - 1;


            }
            throw new KeyNotFoundException();
        }
        private int privatHeight(Node root)
        {
            if (root != null)
                return 1 + Math.Max(privatHeight(root.left), privatHeight(root.right));
            return 0;
        }
        public void PostOrder()
        {
            if (root != null)
                postorder(root);
        }
        void postorder(Node root)
        {
            if (root != null)
            {
                postorder(root.left);
                postorder(root.right);
                Console.WriteLine(root.data);
            }
        }
        public Boolean isBalanced()
        {
            return isbal(root);

        }
        private Boolean isbal(Node cur)
        {
            if (cur == null) return true;
            return (Math.Abs(cur.bf) < 2 && isbal(cur.left) && isbal(cur.right));
        }
        public (string, string) caniadd()
        {
            if (isBalanced()) { return (null,null); }
            var q = new Queue<Node>();
            q.Enqueue(root);
            while (q.TryPeek(out Node cur))
            {
                q.Dequeue();
                if (cur != null)
                {
                    if (cur.left == null)
                    {
                        cur.left = new Node();
                        update(cur);
                        if (isBalanced())
                        {

                            (String, String) result;
                            cur.left = null;
                            var range = findRange(cur);
                            result.Item1 = (range.Item2 == null ? "-INF" : range.Item2.data.ToString());
                            result.Item2 = cur.data.ToString();
                            cur.left = null;
                            return result;
                        }
                        else { cur.left = null; update(cur); }
                    }
                    if (cur.right == null)
                    {
                        cur.right = new Node();
                        update(cur);
                        if (isBalanced())
                        {

                            (String, String) result;
                            cur.right = null;
                            var range = findRange(cur);
                            result.Item1 = cur.data.ToString();
                            result.Item2 = range.Item1 != null ? range.Item1.data.ToString() : "+INF";

                            return result;
                        }
                        else { cur.right = null; update(cur); }
                    }

                    //для правого
                    q.Enqueue(cur.left);
                    q.Enqueue(cur.right);
                }
              
            }
            throw new Exception("Impossible");
        }
        private (Node, Node) findRange(Node ziel)
        {
            var queue = new Queue<(Node, Node, Node)>();

            queue.Enqueue((root, null, null));//node,MAX,MIN values
            while (queue.TryPeek(out (Node, Node, Node) cur))
            {
                queue.Dequeue();
                if (cur.Item1 == ziel)
                {
                    return (cur.Item2, cur.Item3);
                }
                else
                {
                    if (cur.Item1 != null)
                    {

                        queue.Enqueue((cur.Item1.left, cur.Item1, cur.Item3));
                        queue.Enqueue((cur.Item1.right, cur.Item2, cur.Item1));
                    }

                }
                //if(cur.Item1 == root)
                //{
                //    queue.Enqueue((cur.Item1.left, cur.Item1.data, findMin(root).data));//root.left,MAX = root.data,MIN = default(T)
                //    queue.Enqueue((cur.Item1.right, findMax(root).data,cur.Item1.data));//root.left,MAX = MAXIMUM,MIN = 
                //}


            }
            return (null, null);
        }
        private Node findMin(Node root)
        {
            // while (root.left != null) root = root.left;
            if (root == null) return null;
            // while (root.left != null) root = root.left;
            if (root.left == null)
                return root;
            return findMin(root.left);

        }
        private Node findMax(Node root)
        {
            // while (root.left != null) root = root.left;
            if (root == null) return null;
            if (root.right == null)
                return root;
            return findMax(root.right);

        }

       

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        class Node
        {
            public T data; //inf
            public Node left;
            public Node right;
            public Int32 height = 0;
            public Int32 bf = 0;
            public Node(T val = default(T), Node left = null, Node right = null)// Node constructor
            {
                this.left = left;
                this.right = right;
                data = val;
            }


        }

    }
}
