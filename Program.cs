using bst;
var bst = new BST<int>();
string path = "C:/Users/born2befree/source/repos/bst/bst/nodes.txt";

using (var fin = new StreamReader(path))
{
    var text = fin.ReadToEnd();
    var nodes = text.Split(new char[] { ' ','\n' }, StringSplitOptions.RemoveEmptyEntries);
    foreach (var node in nodes)
    {
        bst.Add(int.Parse(node));
    }

}
bst.Add(1000);
bst.InOrder();
var res = bst.caniadd();
Console.WriteLine(res);