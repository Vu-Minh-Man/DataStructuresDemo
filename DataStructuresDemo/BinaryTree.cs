using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresDemo
{
    public class BinaryTree
    {
        private BinaryTreeNode? _root;

        /// <summary>
        /// depth must be greater than or equals to 0.
        /// position (from left to right) must be greater than or equal to 0.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="depth"></param>
        /// <param name="position"></param>
        public void Insert(int value, int depth = 0, int position = 0)
        {
            if (depth < 0)
                throw new ArgumentOutOfRangeException("Depth must be greater than or equal to 0.");

            if (position < 0 || position >= 1 << depth)
                throw new ArgumentOutOfRangeException("Invalid value of position.");

            //if (Find(value))
            //    throw new InvalidOperationException("Node is already exist.");

            if (_root != null && depth == 0 && position == 0)
                throw new InvalidOperationException("Root is already exist. Use InsertOrSet() to change value instead.");

            if (_root == null)
            {
                if (depth == 0 && position == 0)
                {
                    _root = new BinaryTreeNode(value);
                    return;
                }
                else
                    throw new InvalidOperationException("Can't reach this node.");
            }

            LocalBinaryTree parent = GetParentNode(depth, position);

            if (IsTurningLeft(parent.Depth, parent.Position))
            {
                if (parent.Root.Left == null)
                    parent.Root.Left = new BinaryTreeNode(value);
                else
                    throw new InvalidOperationException("Node is already exist. Use InsertOrSet() to change value instead.");
            }
            else
            {
                if (parent.Root.Right == null)
                    parent.Root.Right = new BinaryTreeNode(value);
                else
                    throw new InvalidOperationException("Node is already exist. Use InsertOrSet() to change value instead.");
            }

        }

        /// <summary>
        /// depth must be greater than or equals to 0.
        /// position (from left to right) must be greater than or equal to 0.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="depth"></param>
        /// <param name="position"></param>
        public void InsertOrSet(int value, int depth = 0, int position = 0)
        {
            if (depth < 0)
                throw new ArgumentOutOfRangeException("Depth must be greater than or equal to 0.");

            if (position < 0 || position >= 1 << depth)
                throw new ArgumentOutOfRangeException("Invalid value of position.");

            //if (Find(value))
            //    throw new InvalidOperationException("Node is already exist.");

            if (_root != null && depth == 0 && position == 0)
            {
                _root.Value = value;
                return;
            }

            if (_root == null)
            {
                if (depth == 0 && position == 0)
                {
                    _root = new BinaryTreeNode(value);
                    return;
                }
                else
                    throw new InvalidOperationException("Can't reach this node.");
            }

            LocalBinaryTree parent = GetParentNode(depth, position);

            if (IsTurningLeft(parent.Depth, parent.Position))
            {
                if (parent.Root.Left == null)
                    parent.Root.Left = new BinaryTreeNode(value);
                else
                    parent.Root.Left.Value = value;
            }
            else
            {
                if (parent.Root.Right == null)
                    parent.Root.Right = new BinaryTreeNode(value);
                else
                    parent.Root.Right.Value = value;
            }

        }

        public bool Find(int value)
        {
            return FindPreOrder(value, _root);
        }

        public bool IsBinarySearchTree()
        {
            if (_root == null)
                throw new InvalidOperationException("Binary tree is empty.");

            return IsBinarySearchTree(_root, int.MinValue, int.MaxValue);
        }

        public void PrintAllNodeAtDepth(int depth)
        {
            if (depth < 0)
                throw new ArgumentOutOfRangeException("depth must be greater than or equal to 0");

            if (_root == null)
                throw new InvalidOperationException("Root is not exist.");

            PrintAllNodeAtDepth(depth, _root);
        }

        public void PrintAllNodeAtDepthIteration(int depth)
        {
            if (depth < 0)
                throw new ArgumentOutOfRangeException("depth must be greater than or equal to 0");

            if (_root == null)
                throw new InvalidOperationException("Root is not exist.");

            if (depth == 0)
            {
                Console.WriteLine(_root.Value);
                return;
            }

            for (int i = 1; i < 1 << depth; i += 2)
            {
                var parent = GetParentNodeOrDefault(depth, i);
                if (parent.Root != null)
                {
                    if (parent.Root.Left != null)
                        Console.WriteLine(parent.Root.Left.Value);

                    if (parent.Root.Right != null)
                        Console.WriteLine(parent.Root.Right.Value);
                }
            }
        }

        public bool Equals(BinaryTree? other)
        {
            if (_root == null && other._root == null)
                throw new InvalidOperationException("Both trees are not exist.");

            if (_root == null)
                throw new InvalidOperationException("Compared tree is not exist.");

            if (other._root == null)
                throw new InvalidOperationException("Comparing tree is not exist.");

            return Equals(_root, other._root);
        }

        public void TraversePreOrder()
        {
            TraversePreOrder(_root);
        }

        public void TraverseInOrder()
        {
            TraverseInOrder(_root);
        }

        public void TraversePostOrder()
        {
            TraversePostOrder(_root);
        }

        private LocalBinaryTree GetParentNode(int depth, int position)
        {
            var local = new LocalBinaryTree(_root, depth - 1, position);

            while (local.Root != null && local.Depth > 0)
            {
                if (IsTurningLeft(local.Depth, local.Position))
                    local.Root = local.Root.Left;
                else
                {
                    local.Root = local.Root.Right;
                    local.Position -= 1 << local.Depth;
                }

                local.Depth--;
            }

            if (local.Root == null)
                throw new InvalidOperationException("Can't reach this node.");

            return local;
        }

        private bool IsTurningLeft(int localDepth, int localPosition)
        {
            return localPosition >> localDepth == 0;
        }

        /*
        private bool IsTurningRight(int localDepth, int localPosition)
        {
            return (localPosition >> localDepth) == 1;
        }
        */

        private LocalBinaryTree GetParentNodeOrDefault(int depth, int position)
        {
            var local = new LocalBinaryTree(_root, depth - 1, position);

            while (local.Root != null && local.Depth > 0)
            {
                if (IsTurningLeft(local.Depth, local.Position))
                    local.Root = local.Root.Left;
                else
                {
                    local.Root = local.Root.Right;
                    local.Position -= 1 << local.Depth;
                }

                local.Depth--;
            }

            return local;
        }

        private bool FindPreOrder(int value, BinaryTreeNode? node)
        {
            if (node == null)
                return false;

            if (node.Value == value)
                return true;

            return FindPreOrder(value, node.Left) || FindPreOrder(value, node.Right);
        }

        private bool IsBinarySearchTree(BinaryTreeNode? node, int leftBoundary, int rightBoundary)
        {
            if (node == null)
                return true;

            if (node.Value > leftBoundary && node.Value < rightBoundary)
                return IsBinarySearchTree(node.Left, leftBoundary, node.Value) && IsBinarySearchTree(node.Right, node.Value, rightBoundary);

            return false;
        }

        private void PrintAllNodeAtDepth(int depth, BinaryTreeNode? node)
        {
            if (node == null)
                return;

            if (depth == 0)
            {
                Console.WriteLine(node.Value);
                return;
            }

            PrintAllNodeAtDepth(depth - 1, node.Left);
            PrintAllNodeAtDepth(depth - 1, node.Right);
        }

        private bool Equals(BinaryTreeNode? thisNode, BinaryTreeNode? otherNode)
        {
            if (thisNode == null && otherNode == null)
                return true;

            if (thisNode != null && otherNode != null)
                return thisNode.Value == otherNode.Value &&
                            Equals(thisNode.Left, otherNode.Left) &&
                            Equals(thisNode.Right, otherNode.Right);

            return false;
        }

        private bool IsLeaf(BinaryTreeNode? node)
        {
            return node.Left == null && node.Right == null;
        }

        // Pre-Order: Root-Left-Right
        private void TraversePreOrder(BinaryTreeNode? node)
        {
            if (node == null)
                return;

            Console.WriteLine(node.Value);
            TraversePreOrder(node.Left);
            TraversePreOrder(node.Right);
        }

        // In-Order: Left-Root-Right
        private void TraverseInOrder(BinaryTreeNode? node)
        {
            if (node == null)
                return;

            TraverseInOrder(node.Left);
            Console.WriteLine(node.Value);
            TraverseInOrder(node.Right);
        }

        // Post-Order: Left-Right-Root
        private void TraversePostOrder(BinaryTreeNode? node)
        {
            if (node == null)
                return;

            TraversePostOrder(node.Left);
            TraversePostOrder(node.Right);
            Console.WriteLine(node.Value);
        }

        private struct LocalBinaryTree
        {
            internal BinaryTreeNode? Root { get; set; }
            internal int Depth { get; set; }
            internal int Position { get; set; }

            internal LocalBinaryTree(BinaryTreeNode root, int depth, int position)
            {
                Root = root;
                Depth = depth;
                Position = position;
            }
        }

    }
}
