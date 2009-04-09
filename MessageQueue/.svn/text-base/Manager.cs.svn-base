using System;
using System.Collections.Generic;
using System.Text;

using System.Messaging;

using MQueue = System.Messaging.MessageQueue;

namespace Mubble.MessageQueue
{
    /// <summary>
    /// A delegate for handling received messages.
    /// </summary>
    /// <typeparam name="T">Type of the expected message</typeparam>
    /// <param name="value">A received message</param>
    public delegate void MessageReceivedDelegate<T>(T value);

    public delegate void MessageQueueErrorDelegate(string path, QueueException exception);

    /// <summary>
    /// A static, threadsafe queue manager class.  Useful for sending and receiving
    /// message from MS Message Queues
    /// </summary>
    public static class Manager
    {
        delegate void MessageReceivedDelegate(object value);

        static Dictionary<object, MessageReceivedDelegate> convertedDelegates;

        static MessageReceivedDelegate handlers;
        static System.Threading.ReaderWriterLockSlim handlerLock;

        static Dictionary<string, MQueue> senders;
        static System.Threading.ReaderWriterLockSlim senderLock;

        static Dictionary<string, MQueue> receivers;
        static System.Threading.ReaderWriterLockSlim receiverLock;

        static Manager()
        {
            convertedDelegates = new Dictionary<object, MessageReceivedDelegate>();

            handlerLock = new System.Threading.ReaderWriterLockSlim();

            senderLock = new System.Threading.ReaderWriterLockSlim();
            senders = new Dictionary<string, MQueue>();

            receiverLock = new System.Threading.ReaderWriterLockSlim();
            receivers = new Dictionary<string, MQueue>();
        }

        public static event MessageQueueErrorDelegate ReceiveError;

        /// <summary>
        /// Registers a message handler.  This handler will be called when a message 
        /// matching type T is received.  
        /// </summary>
        /// <typeparam name="T">Expected message type</typeparam>
        /// <param name="handler">The handler function, receives a typed message</param>
        public static void RegisterHandler<T>(MessageReceivedDelegate<T> handler)
        {
            handlerLock.EnterWriteLock();
            try
            {
                MessageReceivedDelegate del = Convert(handler);
                handlers += del;
            }
            finally
            {
                handlerLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Unregisters a previously registered handler.
        /// </summary>
        /// <typeparam name="T">The handler's expected message type</typeparam>
        /// <param name="handler">The handler function to unregister</param>
        public static void RemoveHandler<T>(MessageReceivedDelegate<T> handler)
        {
            handlerLock.EnterWriteLock();
            try
            {
                MessageReceivedDelegate del = Convert(handler);
                handlers -= del;

                convertedDelegates.Remove(handler);
            }
            finally
            {
                handlerLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Registers a queue for sending purposes.  The name is intended to be a friendly name
        /// for use elsewhere in code.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="queue"></param>
        public static void RegisterSender(string name, string address)
        {
            senderLock.EnterWriteLock();

            try
            {
                MQueue q = new MQueue(address);
                q.Formatter = new BinaryMessageFormatter();
                if (senders.ContainsKey(name))
                {
                    senders[name] = q;
                }
                else
                {
                    senders.Add(name, q);
                }
            }
            finally
            {
                senderLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Sends a message to the specified queue.  The queue must have already been registered
        /// using the RegisterSender directive
        /// </summary>
        /// <param name="queue">The named queue to send a message to</param>
        /// <param name="message">The message object itself</param>
        public static void Send(string queue, object message)
        {
            senderLock.EnterReadLock();

            MQueue q = null;
            try
            {
                if (senders.ContainsKey(queue))
                {
                    q = senders[queue];
                }
                else
                {
                    throw new ArgumentException("There is no queue specified with that name", "queue");
                }
            }
            finally
            {
                senderLock.ExitReadLock();
            }

            if (q != null)
            {
                lock (q)
                {
                    try
                    {
                        q.Send(message);
                    }
                    catch (MessageQueueException ex)
                    {
                        throw new QueueSendException(
                            "An error occurred while sending the message",
                            message,
                            ex
                            );
                    }
                }
            }
        }

        static MessageReceivedDelegate Convert<T>(MessageReceivedDelegate<T> handler)
        {
            if (!convertedDelegates.ContainsKey(handler))
            {
                convertedDelegates.Add(handler,
                    delegate(object o)
                    {
                        if (o is T)
                        {
                            handler((T)o);
                        }
                    });
            }
            return convertedDelegates[handler];
        }

        /// <summary>
        /// Watches a queue for messages.
        /// </summary>
        /// <param name="path">The path to watch</param>
        public static void Watch(string path)
        {
            receiverLock.EnterWriteLock();

            MQueue q = null;
            try
            {
                if (receivers.ContainsKey(path))
                {
                    StopWatching(path);
                }
                q = new MQueue(path);

                q.Formatter = new BinaryMessageFormatter();

                q.ReceiveCompleted += ReceiveCompleted;

                receivers.Add(path, q);
            }
            finally
            {
                receiverLock.ExitWriteLock();
            }

            if (q != null)
            {
                try
                {
                    q.BeginReceive(MQueue.InfiniteTimeout, q);
                }
                catch (System.Messaging.MessageQueueException ex)
                {
                    FireReceiveError(path, ex);
                }
            }
        }

        /// <summary>
        /// Closes out a watched queue.  If there is no watched queue with the specified path,
        /// does nothing.
        /// </summary>
        /// <param name="path">The path of the queue to close</param>
        public static void StopWatching(string path)
        {
            if (!receiverLock.IsWriteLockHeld)
            {
                receiverLock.EnterWriteLock();
            }
            try
            {
                if (receivers.ContainsKey(path))
                {
                    receivers[path].Close();
                    receivers.Remove(path);
                }
            }
            finally
            {
                receiverLock.ExitWriteLock();
            }
        }

        static void ReceiveCompleted(IAsyncResult result)
        {
            bool isCompleted = result.IsCompleted;
        }

        static void ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            MQueue queue = e.AsyncResult.AsyncState as MQueue;
            object body = null;

            receiverLock.EnterReadLock();
            try
            {

                if (!receivers.ContainsKey(queue.Path))
                {
                    return;
                }
                using (Message m = e.Message) { body = m.Body; }

            }
            // HACK: A serialization exception means we can't handle the type.  This seems ugly
            catch (System.Runtime.Serialization.SerializationException) { }
            catch (System.Messaging.MessageQueueException) { }
            finally
            {
                receiverLock.ExitReadLock();
            }

            MessageReceivedDelegate del = null;
            if (body != null && (del = handlers) != null)
            {
                del(body);
            }

            try
            {
                queue.BeginReceive(MQueue.InfiniteTimeout, queue);
            }
            catch (System.Messaging.MessageQueueException ex)
            {
                FireReceiveError(queue.Path, ex);
            }
        }

        static void FireReceiveError(string path, MessageQueueException exception)
        {
            if (Manager.ReceiveError != null)
            {
                QueueException ex = new QueueException(
                    "An error occurred while receiving a message",
                    exception
                    );
                Manager.ReceiveError(path, ex);
            }
        }
    }
}
