using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresDemo
{
    public class AVLTree
    {
        private BinaryTreeNode? _root;

        public void Insert(int value)
        {
            _root = Insert(value, _root);
        }

        public bool Find(int value)
        {
            return GetNode(value) != null;
        }

        private BinaryTreeNode Insert(int value, BinaryTreeNode? node)
        {
            if (node == null)
                return new BinaryTreeNode(value);

            if (node.Value > value)
                node.Left = Insert(value, node.Left);
            else if (node.Value < value)
                node.Right = Insert(value, node.Right);
            else
                throw new InvalidOperationException("Node is already exist.");


            node.Height = GetHeight(node);

            node = Balancing(node);

            return node;
        }

        private int GetHeight(BinaryTreeNode node)
        {
            if (IsLeaf(node))
                return 0;

            if (node.Right == null)
                return node.Left.Height + 1;

            if (node.Left == null)
                return node.Right.Height + 1;

            return Math.Max(node.Left.Height, node.Right.Height) + 1;
        }

        private int GetBalancedFactor(BinaryTreeNode node)
        {
            if (node.Right == null)
                return node.Left.Height + 1;

            if (node.Left == null)
                return -node.Right.Height - 1;

            return node.Left.Height - node.Right.Height;
        }

        private bool IsLeftHeavy(BinaryTreeNode node)
        {
            return GetBalancedFactor(node) > 1;
        }

        private bool IsRightHeavy(BinaryTreeNode node)
        {
            return GetBalancedFactor(node) < -1;
        }

        private BinaryTreeNode Balancing(BinaryTreeNode node)
        {
            if (IsRightHeavy(node))
            {
                if (GetBalancedFactor(node.Right) > 0)
                    node.Right = RightRotating(node.Right);

                node = LeftRotating(node);
            }
            else if (IsLeftHeavy(node))
            {
                if (GetBalancedFactor(node.Left) < 0)
                    node.Left = LeftRotating(node.Left);

                node = RightRotating(node);
            }

            return node;
        }

        private BinaryTreeNode LeftRotating(BinaryTreeNode node)
        {
            var newRoot = node.Right;
            var disconnectedNode = newRoot.Left;
            newRoot.Left = node;
            node.Right = disconnectedNode;

            node.Height = GetHeight(node);
            newRoot.Height = GetHeight(newRoot);

            return newRoot;
        }

        private BinaryTreeNode RightRotating(BinaryTreeNode node)
        {
            var newRoot = node.Left;
            var disconnectedNode = newRoot.Right;
            newRoot.Right = node;
            node.Left = disconnectedNode;

            node.Height = GetHeight(node);
            newRoot.Height = GetHeight(newRoot);

            return newRoot;
        }

        //private BinaryTreeNode LeftRightRotating(BinaryTreeNode node)
        //{
        //    var newRoot = node.Right;
        //    var disconnectedNode = newRoot.Left;
        //    newRoot.Left = node;
        //    node.Right = disconnectedNode;

        //    node.Height = GetHeight(node);
        //    newRoot.Height = GetHeight(newRoot);

        //    return newRoot;
        //}

        private bool IsLeaf(BinaryTreeNode node)
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


    }
}
