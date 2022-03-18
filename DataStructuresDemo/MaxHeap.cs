using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresDemo
{
    public class MaxHeap
    {
        private int[] _heap;
        private readonly int _size;
        private const int _defaultSize = 10;

        public int Count { get; private set; }

        public MaxHeap(int size = _defaultSize)
        {
            if (size <= 0)
                throw new ArgumentOutOfRangeException("Heap size must be greater than 0.");

            _heap = new int[size];
            _size = size;
            Count = 0;
        }

        public void Insert(int value)
        {
            if (IsFull())
                throw new InvalidOperationException("Heap is full.");

            _heap[Count++] = value;

            BubbleUp();
        }

        private void BubbleUp()
        {
            int index = Count - 1;
            int parentIndex = GetParentIndexOf(index);

            while (index > 0 && _heap[index] > _heap[parentIndex])
            {
                SwapHeapValue(index, parentIndex);
                index = parentIndex;
                parentIndex = GetParentIndexOf(index);
            }
        }

        private void SwapHeapValue(int index, int parentIndex)
        {
            var temp = _heap[parentIndex];
            _heap[parentIndex] = _heap[index];
            _heap[index] = temp;
        }

        private int GetParentIndexOf(int index)
        {
            return (index - 1) / 2;
        }

        private bool IsFull()
        {
            return Count == _size;
        }

        private bool IsEmpty()
        {
            return Count == 0;
        }
    }
}
