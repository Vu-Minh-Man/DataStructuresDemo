using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresDemo
{
    internal sealed class BinaryTreeNode
    {
        internal int Value { get; set; }
        internal BinaryTreeNode? Left { get; set; }
        internal BinaryTreeNode? Right { get; set; }

        internal int Height { get; set; }

        internal BinaryTreeNode(int value)
        {
            Value = value;
            Height = 0;
        }
    }
}
