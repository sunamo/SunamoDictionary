
namespace SunamoDictionary;
internal delegate void TreeVisitor<T>(T nodeData);




internal class NTree<T>
{
    public T data;
    public LinkedList<NTree<T>> children;
    public NTree(T data)
    {
        this.data = data;
        children = new LinkedList<NTree<T>>();
    }
    public NTree<T> AddChild(T data)
    {
        var child = new NTree<T>(data);
        children.AddFirst(child);
        return child;
    }
    public NTree<T> GetChild(int i)
    {
        foreach (NTree<T> n in children)
            if (--i == 0)
                return n;
        return null;
    }
    public void Traverse(NTree<T> node, TreeVisitor<T> visitor)
    {
        visitor(node.data);
        foreach (NTree<T> kid in node.children)
            Traverse(kid, visitor);
    }
}
