using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresDemo
{
    public class BinarySearchTree
    {
        private BinaryTreeNode? _root;

        public void Insert(int value)
        {
            if (_root == null)
            {
                _root = new BinaryTreeNode(value);
                return;
            }

            if (Find(value))
                throw new InvalidOperationException("Node is already exist.");

            var parentNode = GetParentNode(value);

            if (parentNode.Value > value)
                parentNode.Left = new BinaryTreeNode(value);
            else /* if (parentNode.Value < value) */
                parentNode.Right = new BinaryTreeNode(value);

        }

        public bool Find(int value)
        {
            return GetNode(value) != null;
        }

        private bool IsLeaf(BinaryTreeNode? node)
        {
            return node.Left == null && node.Right == null;
        }

        private BinaryTreeNode? GetNode(int value)
        {
            var node = _root;

            while (node != null)
            {
                if (node.Value > value)
                    node = node.Left;
                else if (node.Value < value)
                    node = node.Right;
                else
                    break;
            }

            return node;
        }

        private BinaryTreeNode? GetParentNode(int value)
        {
            var node = _root;

            while (true)
            {
                if (node.Value > value)
                {
                    if (node.Left == null)
                        return node;
                    else
                        node = node.Left;
                }
                else /* if (node.Value < value) */
                {
                    if (node.Right == null)
                        return node;
                    else
                        node = node.Right;
                }
            }
        }
    }
}
