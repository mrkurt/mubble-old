using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Mubble
{
    public class NamedLock<T>
    {
        Dictionary<T, ReferenceCount> lockCollection = new Dictionary<T, ReferenceCount>();
        public IDisposable Lock(T name)
        {
            return Lock(name, Timeout.Infinite);
        }
        public IDisposable Lock(T name, int timeout)
        {
            Monitor.Enter(lockCollection);
            ReferenceCount obj = null;
            lockCollection.TryGetValue(name, out obj);
            if (obj == null)
            {
                obj = new ReferenceCount();
                Monitor.Enter(obj);
                lockCollection.Add(name, obj);
                Monitor.Exit(lockCollection);
            }
            else
            {
                obj.AddRef();
                Monitor.Exit(lockCollection);
                if (!Monitor.TryEnter(obj, timeout))
                {
                    throw new NamedLock<T>.TimeoutException("Timeout while waiting for lock on {0}", name);
                }
            }

            return new Token<T>(this, name);
        }

        public void Unlock(T name)
        {
            lock (lockCollection)
            {
                ReferenceCount obj = null;
                lockCollection.TryGetValue(name, out obj);
                if (obj != null)
                {
                    Monitor.Exit(obj);
                    if (0 == obj.Release())
                    {
                        lockCollection.Remove(name);
                    }
                }
            }
        }

        class Token<S> : IDisposable
        {
            NamedLock<S> parent;
            S name;

            public Token(NamedLock<S> parent, S name)
            {
                this.parent = parent;
                this.name = name;
            }

            #region IDisposable Members

            public void Dispose()
            {
                parent.Unlock(name);
            }

            #endregion
        }

        public class TimeoutException : System.TimeoutException
        {
            public TimeoutException() : base() { }

            public TimeoutException(string message) : base(message) { }

            public TimeoutException(string message, params object[] values) : base(string.Format(message, values)) { }

        }
    }
    public class ReferenceCount
    {
        public int count = 0;

        public ReferenceCount()
        {
            AddRef();
        }

        public int AddRef()
        {
            return Interlocked.Increment(ref count);
        }

        public int Release()
        {
            return Interlocked.Decrement(ref count);
        }
    }
}
