using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomQueue
{
    public class Queue<T> : IEnumerable<T>
    {
        private T[] queArr;
        private int size;
        private const int defaultCapacity = 10;
        private int capacity;
        private int head;
        private int tail;

        public Queue()
        {
            capacity = defaultCapacity;
            this.queArr = new T[defaultCapacity];
            this.size = 0;
            this.head = -1;
            this.tail = 0;
        }

        public bool isEmpty() 
        {
            return size == 0;
        }

        public void Enqueue(T newElement)
        {
            if (this.size == this.capacity)
            {
                T[] newQueue = new T[2 * capacity];
                Array.Copy(queArr, 0, newQueue, 0, queArr.Length);
                queArr = newQueue;
                capacity *= 2;
            }
            size++;
            queArr[tail++] = newElement;
        }

        public T Dequeue()
        {
            if (this.size == 0)
            {
                throw new InvalidOperationException();
            }
            size--;
            return queArr[++head];
        }
        public T Peek ()
        {
            if (this.size == 0)
            {
                throw new InvalidOperationException();
            }
            return queArr[head];
        }


        public int Count
        {
            get
            {
                return this.size;
            }
        }
        public T this[int index]
        {
            get { return queArr[index]; }
            set { queArr[index] = value; }
        }
        public IEnumerator<T> GetEnumerator()
        {
            return new CustomIterator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        private struct CustomIterator : IEnumerator<T>
        {
            private readonly Queue<T> queArr;
            private int currentIndex;

            public CustomIterator(Queue<T> arr)
            {
                this.currentIndex = -1;
                this.queArr = arr;
            }

            public T Current
            {
                get
                {
                    if (currentIndex == -1 || currentIndex == queArr.Count)
                    {
                        throw new InvalidOperationException();
                    }
                    return queArr[currentIndex];
                }
            }

            object System.Collections.IEnumerator.Current
            {
                get { throw new NotImplementedException(); }
            }

            public void Reset()
            {
                currentIndex = -1;
                //throw new NotSupportedException();
            }

            public bool MoveNext()
            {
                return ++currentIndex < queArr.Count;
            }

            void IDisposable.Dispose() { }
        }
    }
}


