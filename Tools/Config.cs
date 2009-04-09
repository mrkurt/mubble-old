using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Tools
{
    class Config : Action
    {
        public override void Execute(Arguments args)
        {
            string mode = args["mode"] != null ? args["mode"] : "";
            string[] files = { "output", "settings", "template" };

            XmlDocument output = new XmlDocument();
            XmlDocument template = new XmlDocument();
            XmlDocument settings = new XmlDocument();

            template.Load(args["template"]);
            settings.Load(args["settings"]);

            XmlNode configuration = template.SelectSingleNode("configuration");

            foreach (XmlNode node in settings.SelectSingleNode("site").ChildNodes)
            {
                XmlNode value = node;
                if (value.Attributes["mode"] != null)
                {
                    List<string> modes = new List<string>(value.Attributes["mode"].Value.Split(",".ToCharArray()));

                    if (!modes.Contains(mode)) continue;
                    value.Attributes.Remove(value.Attributes["mode"]);
                }
                string path = value.Name;
                if (value.Name == "xpath")
                {
                    path = value.Attributes["path"].Value;
                    value = value.ChildNodes[0];
                }
                if (!"handlers".Equals(value.Name, StringComparison.CurrentCultureIgnoreCase))
                {
                    XmlNode nodeToReplace = configuration.SelectSingleNode(path);
                    if (nodeToReplace != null)
                    {
                        nodeToReplace.ParentNode.ReplaceChild(template.ImportNode(value, true), nodeToReplace);
                    }

                }
                else
                {
                    Dictionary<string, string> extensions = new Dictionary<string, string>();
                    foreach (XmlNode extension in value.ChildNodes)
                    {
                        extensions.Add(extension.Attributes["type"].Value, extension.Attributes["extension"].Value);
                    }

                    XmlNode handlers = configuration.SelectSingleNode("system.web/httpHandlers");
                    string primaryExtension = value.Attributes["primaryExtension"] != null ? value.Attributes["primaryExtension"].Value : null;
                    foreach (XmlNode handler in handlers.ChildNodes)
                    {
                        bool extensionSet = false;
                        foreach (string key in extensions.Keys)
                        {
                            if (handler.Attributes["type"].Value.Contains(key))
                            {
                                handler.Attributes["path"].Value = extensions[key];
                            }
                        }
                        if (!extensionSet && handler.Attributes["path"] != null && primaryExtension != null)
                        {
                            handler.Attributes["path"].Value = handler.Attributes["path"].Value.Replace(".aspx", primaryExtension);
                        }
                        Console.WriteLine("{0} mapped to {1}", handler.Attributes["path"].Value, handler.Attributes["type"].Value);
                    }
                }
            }

            Console.WriteLine(template.ChildNodes[1].Name);

            Console.WriteLine("Creating {0} using {1} and template {2}.", args["output"], args["settings"], args["template"]);

            template.Save(args["output"]);
            //Console.ReadLine();
        }
    }
}
