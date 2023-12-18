using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engines
{
    public class TreeNode<T> 
    {
        public T? Data { get; set; }
        public TreeNode<T>? Left { get; set; }
        public TreeNode<T>? Right { get; set; }

        public TreeNode(T? data = default)
        {
            Data = data;
            Left = null;
            Right = null;
        }
        public TreeNode<T> Clone()
        {
            TreeNode<T> node = new(Data);

            if (Left is not null)
                node.Left = (TreeNode<T>)Left.Clone();
            else
                node.Left = null;

            if (Right is not null)
                node.Right = (TreeNode<T>)Right.Clone();
            else
                node.Right = null;
            return node;
        }
        public override string? ToString() => Data?.ToString();
    }
}
