using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Tools
{
    class Counters : Action
    {
        public override void Execute(Arguments args)
        {
            Console.WriteLine("Creating Mubble Performance counters");
            CounterCreationDataCollection col =  new CounterCreationDataCollection();

            // Create custom counter objects
            CounterCreationData counter1 = new CounterCreationData();
            counter1.CounterName = "Available ASP.NET Worker Threads";
            counter1.CounterHelp = "The difference between the maximum number " +
                                   "of thread pool worker threads and the " +
                                   "number currently active.";
            counter1.CounterType = PerformanceCounterType.NumberOfItems32;

            CounterCreationData counter2 = new CounterCreationData();
            counter2.CounterName = "Available ASP.NET IO Threads";
            counter2.CounterHelp = "The difference between the maximum number of " +
                                   "thread pool IO threads and the number " +
                                   "currently active.";
            counter2.CounterType = PerformanceCounterType.NumberOfItems32;

            CounterCreationData counter3 = new CounterCreationData();
            counter3.CounterName = "Max ASP.NET Worker Threads";
            counter3.CounterHelp = "The number of requests to the thread pool " +
                                   "that can be active concurrently. All " +
                                   "requests above that number remain queued until " +
                                   "thread pool worker threads become available.";
            counter3.CounterType = PerformanceCounterType.NumberOfItems32;

            CounterCreationData counter4 = new CounterCreationData();
            counter4.CounterName = "Max ASP.NET IO Threads";
            counter4.CounterHelp = "The number of requests to the thread pool " +
                                   "that can be active concurrently. All " +
                                   "requests above that number remain queued until " +
                                   "thread pool IO threads become available.";
            counter4.CounterType = PerformanceCounterType.NumberOfItems32;

            // Add custom counter objects to CounterCreationDataCollection.
            col.Add(counter1);
            col.Add(counter2);
            col.Add(counter3);
            col.Add(counter4);

            counter1 = new CounterCreationData(
                "Available Mubble Worker Threads", 
                "Threads available in the Mubble threadpool.", 
                PerformanceCounterType.NumberOfItems32
                );
            col.Add(counter1);

            counter1 = new CounterCreationData(
                "Max Mubble Worker Threads",
                "Maximum size of the Mubble threadpool.",
                PerformanceCounterType.NumberOfItems32
                );
            col.Add(counter1);

            counter1 = new CounterCreationData(
                "Work items in Mubble queue",
                "The number of work items waiting on a thread.",
                PerformanceCounterType.NumberOfItems32
                );
            col.Add(counter1);

            // delete the category if it already exists
            if (PerformanceCounterCategory.Exists("Mubble"))
            {
                PerformanceCounterCategory.Delete("Mubble");
            }
            // bind the counters to the PerformanceCounterCategory
            PerformanceCounterCategory category =
                    PerformanceCounterCategory.Create("Mubble",
                                                      "", col);

        }
    }
}
