using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresDemo
{
    public class MyMaxHeap
    {
        private BinaryTreeNode?[] _heap;
        private const int _defaultSize = 10;

        public int Count { get; private set; }

        public MyMaxHeap(int size = _defaultSize)
        {
            _heap = new BinaryTreeNode[size];
            Count = 0;
        }

        public void Insert(int value)
        {
            Count++;

            if (_heap[0] == null)
            {
                _heap[0] = new BinaryTreeNode(value);
                return;
            }

            int index = Count - 1;
            _heap[index] = new BinaryTreeNode(value);
            int parentIndex = (index - 1) / 2;
            int grandparentIndex = (parentIndex - 1) / 2;

            if (IsLeftChild(index))
                _heap[parentIndex].Left = _heap[index];
            else
                _heap[parentIndex].Right = _heap[index];

            while (_heap[index].Value > _heap[parentIndex].Value)
            {
                _heap[parentIndex].Left = _heap[index].Left;
                _heap[parentIndex].Right = _heap[index].Right;

                if (IsLeftChild(index))
                {
                    _heap[index].Left = _heap[parentIndex];
                    _heap[index].Right = _heap[index + 1];
                }
                else
                {
                    _heap[index].Left = _heap[index - 1];
                    _heap[index].Right = _heap[parentIndex];
                }

                if (parentIndex > 0)
                {
                    if (IsLeftChild(parentIndex))
                        _heap[grandparentIndex].Left = _heap[index];
                    else
                        _heap[grandparentIndex].Right = _heap[index];
                }

                var temp = _heap[parentIndex];
                _heap[parentIndex] = _heap[index];
                _heap[index] = temp;

                index = parentIndex;
                parentIndex = grandparentIndex;
                grandparentIndex = (parentIndex - 1) / 2;
            }

        }

        public void Remove()
        {
            Count--;

            _heap[0] = null;
            if (Count == 0)
                return;

            if (Count == 1)
            {
                _heap[0] = _heap[1];
                _heap[1] = null;
                return;
            }

            if (Count == 2)
            {
                if (_heap[1].Value >= _heap[2].Value)
                {
                    _heap[0] = _heap[1];
                    _heap[1] = _heap[2];
                }
                else
                    _heap[0] = _heap[2];

                _heap[2] = null;
                _heap[0].Left = _heap[1];
                return;
            }

            int index = Count;
            int parentIndex = (index - 1) / 2;

            if (IsLeftChild(index))
                _heap[parentIndex].Left = null;
            else
                _heap[parentIndex].Right = null;

            _heap[0] = _heap[index];
            _heap[index] = null;
            _heap[0].Left = _heap[1];
            _heap[0].Right = _heap[2];

            index = 0;
            int maxChildIndex = _heap[1].Value >= _heap[2].Value ? 1 : 2;

            while (_heap[index].Value < _heap[maxChildIndex].Value)
            {
                _heap[index].Left = _heap[maxChildIndex].Left;
                _heap[index].Right = _heap[maxChildIndex].Right;

                if (IsLeftChild(maxChildIndex))
                {
                    _heap[maxChildIndex].Left = _heap[index];
                    _heap[maxChildIndex].Right = _heap[2 * index + 2];
                }
                else
                {
                    _heap[maxChildIndex].Left = _heap[2 * index + 1];
                    _heap[maxChildIndex].Right = _heap[index];
                }

                if (index > 0)
                {
                    if (IsLeftChild(index))
                        _heap[parentIndex].Left = _heap[maxChildIndex];
                    else
                        _heap[parentIndex].Right = _heap[maxChildIndex];
                }

                var temp = _heap[maxChildIndex];
                _heap[maxChildIndex] = _heap[index];
                _heap[index] = temp;

                parentIndex = index;
                index = maxChildIndex;

                if (IsLeaf(_heap[index]))
                    break;

                if (_heap[index].Right != null)
                    maxChildIndex = _heap[index].Left.Value >= _heap[index].Right.Value ? 2 * index + 1 : 2 * index + 2;
                else
                    maxChildIndex = Count - 1;
            }
        }

        private bool IsLeftChild(int index)
        {
            return index % 2 == 1;
        }

        private bool IsLeaf(BinaryTreeNode? node)
        {
            return node.Left == null && node.Right == null;
        }


    }
}
