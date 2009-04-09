using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Mubble.Indexing;
using System.Text.RegularExpressions;

namespace Mubble.Tests
{
    [TestFixture(TimeOut=60)]
    public class Performance
    {
        static Regex reutersSgm= new Regex(
              "\\<REUTERS[^\\>]*NEWID=\\\"(?<ID>\\d+)\\\"\\>.*?\\<TITLE\\>(" +
              "?<Title>[^\\>]+)\\<\\/TITLE\\>.*?\\<BODY\\>(?<Body>[^\\>]+)\\<" +
              "\\/BODY\\>",
            RegexOptions.IgnoreCase
            | RegexOptions.Multiline
            | RegexOptions.Singleline
            | RegexOptions.ExplicitCapture
            | RegexOptions.CultureInvariant
            | RegexOptions.IgnorePatternWhitespace
            | RegexOptions.Compiled
            );

        [Test]
        [Explicit]
        public void Indexing()
        {
            var schema = new Schema("PerfTest", "1.0")
            {
                Fields =
                {
                    new SchemaField{ Name = "ID", Indexed = true, Stored = true, Tokenized = false, Unique = true},
                    new SchemaField{ Name = "Title",Indexed = true, Stored = true, Tokenized = true, Unique = false},
                    new SchemaField{ Name = "Body", Indexed = true, Stored = true, Tokenized = true, Unique = false}
                },
                DefaultSearchField = "Body"
            };

            var docs = new List<Document>();
            var fields = new string[] { "ID", "Title", "Body" };

            foreach (var file in System.IO.Directory.GetFiles("ReutersData", "*.sgm"))
            {
                var text = System.IO.File.ReadAllText(file);
                var matches = reutersSgm.Matches(text);

                
                foreach (Match match in matches)
                {
                    if (match.Success)
                    {
                        var doc = new Document();
                        doc.Schema = schema;

                        foreach(var key in fields)
                        {
                            doc.Fields.Add(new Field{ Name = key, Values = { match.Groups[key].Value }});
                        }
                        docs.Add(doc);
                    }
                }
            }

            if (System.IO.Directory.Exists("PerfIndex"))
            {
                System.IO.Directory.Delete("PerfIndex", true);
            }

            var idx = Index.Open("PerfIndex");
            idx.Update(schema);

            var rand = new Random();
            var max = docs.Count;

            var results = new List<Mubble.Indexing.IndexWorkItem>();

            var curr = 0;

            Mubble.Treadmill.Func<Mubble.Treadmill.Result> action = () =>
                {
                    var i = System.Threading.Interlocked.Increment(ref curr);
                    if (i > docs.Count)
                    {
                        i = i % docs.Count;
                    }

                    var wi = idx.Update(docs[i]);
                    results.Add(wi);
                    return new Mubble.Treadmill.Result(true, "Indexed!");
                };

            var cpu = new System.Diagnostics.PerformanceCounter("Processor", "% Processor Time", "_Total", true);

            Mubble.Treadmill.Func<Mubble.Treadmill.Benchmark> bench = () =>
                {
                    var start = DateTime.Now;
                    var r = idx.Search("Body:output");
                    var msg = string.Concat("Search for output - ", r.Hits.Count, " results");
                    var elapsed = DateTime.Now - start;

                    return new Mubble.Treadmill.Benchmark(msg, elapsed.Milliseconds, DateTime.Now);
                };

            Mubble.Treadmill.Func<Mubble.Treadmill.Benchmark> proc = () =>
            {
                var count = cpu.NextValue();

                return new Mubble.Treadmill.Benchmark("Processor", count, DateTime.Now);
            };

            var actions = new List<Mubble.Treadmill.Command>(1);
            var benchmarks = new List<Mubble.Treadmill.Collect>(1);
            actions.Add(new Mubble.Treadmill.Command(action, "Index doc"));
            benchmarks.Add(new Mubble.Treadmill.Collect(bench));
            benchmarks.Add(new Mubble.Treadmill.Collect(proc));

            var commands = new Mubble.Treadmill.Commands(actions, () => 
            {
                var total = results.Count;
                Mubble.Tests.Indexing.WaitFor(results);
                idx.Dispose();
                return new Mubble.Treadmill.Result(true, string.Concat("Waited for ", total, " operations"));
            });

            Mubble.Treadmill.Runner.Start(50000, 8, commands, benchmarks);
        }
    }
}
