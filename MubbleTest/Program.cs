using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Mubble.MessageQueue;

using MQueue = System.Messaging.MessageQueue;
using System.Xml.Linq;

namespace Mubble.Tests
{
    class Program
    {
        static System.Timers.Timer timer = new System.Timers.Timer(5000);
        static void Main(string[] args)
        {
            using (var runner = new MbUnit.Core.AutoRunner())
            {
                runner.Run();
                runner.ReportToHtml();
            }

        //    Manager.RegisterSender("test", "formatname:multicast=234.1.1.1:8001");

        //    Manager.RegisterHandler(
        //        delegate(TestMessage msg)
        //        {
        //            Console.WriteLine(
        //                "Received {0} on thread {1}",
        //                msg.Message,
        //                System.Threading.Thread.CurrentThread.ManagedThreadId
        //                );
        //        }
        //    );

        //    Manager.ReceiveError += new MessageQueueErrorDelegate
        //        (
        //            delegate(string path, QueueException ex)
        //            {
        //                Console.WriteLine("Watcher failed.  Trying again");
        //                Manager.Watch(path);
        //            }
        //        );

        //    for (int i = 1; i <= 2; i++)
        //    {
        //        Manager.Watch(Create(".\\PRIVATE$\\multicast-test" + i));
        //    }

        //    int j = 1;
        //    timer.Elapsed += delegate
        //    {
        //        Console.WriteLine(
        //            "Sending on thread {0}", 
        //            System.Threading.Thread.CurrentThread.ManagedThreadId
        //            );
        //        try
        //        {
        //            Manager.Send("test", new TestMessage("Test " + j++));
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //        }
        //    };

        //    Console.WriteLine("Starting on thread {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
        //    timer.Start();

        //    while (Console.ReadLine() != "q")
        //    {
        //        Console.WriteLine("Thread {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
        //    }

        //    for (int i = 1; i <= 2; i++)
        //    {
        //        Delete(".\\PRIVATE$\\multicast-test" + i);
        //    }
        }

        [Serializable]
        class TestMessage
        {
            public string Message { get; set; }
            public DateTime Date { get; set; }

            public TestMessage(string message)
            {
                this.Message = message;
                this.Date = DateTime.Now;
            }

            public override string ToString()
            {
                return string.Format("Message sent at {0}.  {1} old", this.Date, DateTime.Now - this.Date);
            }
        }

        //static void Delete(string path)
        //{
        //    Manager.StopWatching(path);
        //    if (MQueue.Exists(path))
        //    {
        //        MQueue.Delete(path);
        //    }
        //}

        //static string Create(string path)
        //{
        //    if (!MQueue.Exists(path))
        //    {
        //        MQueue q = MQueue.Create(path);
        //        q.MulticastAddress = "234.1.1.1:8001";
        //    }
        //    return path;
        //}
    }
}
