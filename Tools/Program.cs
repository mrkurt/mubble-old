using System;
using System.Collections.Generic;
using System.Text;

namespace Tools
{
    class Program
    {
        static void Main(string[] args)
        {
            Arguments arguments = new Arguments(args);
            Action a = null;
            switch (arguments.Action)
            {
                case "config":
                    a = new Config();
                    break;
                case "counters":
                    a = new Counters();
                    break;
            }
            if (a != null)
            {
                a.Execute(arguments);
            }
        }
    }
}
