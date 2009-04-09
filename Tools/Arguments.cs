using System;
using System.Collections.Generic;
using System.Text;

namespace Tools
{
    class Arguments
    {
        private string action;

        public string Action
        {
            get { return action; }
        }

        private Dictionary<string, string> parameters = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);

        public string this[string parameter]
        {
            get
            {
                if (this.parameters.ContainsKey(parameter))
                {
                    return this.parameters[parameter];
                }
                return null;
            }
        }

        private string[] raw = new string[0];
        public string[] Raw
        {
            get { return this.raw; }
        }

        public Dictionary<string, string>.KeyCollection Keys
        {
            get { return this.parameters.Keys; }
        }

        public Arguments(string[] args)
        {
            this.Parse(args);
        }

        public void Parse(string[] args)
        {
            this.raw = args;
            if (args == null || args.Length == 0) return;
            this.action = args[0];

            for (int i = 1; i < args.Length; i++)
            {
                string arg = args[i];
                if (arg[0] == '/')
                {
                    arg = arg.Substring(1);
                    int divider = arg.IndexOf('=');
                    string name = arg.Substring(0, divider);
                    string value = arg.Substring(divider + 1);

                    if (this.parameters.ContainsKey(name))
                    {
                        this.parameters[name] = value;
                    }
                    else
                    {
                        this.parameters.Add(name, value);
                    }
                }
            }
        }
    }
}
