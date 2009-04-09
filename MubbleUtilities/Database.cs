using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using System.Collections.Specialized;

namespace MubbleUtilities
{
    public class DbTool
    {
        protected string serverName, dbName;
        public DbTool(string serverName, string dbName)
        {
            this.serverName = serverName;
            this.dbName = dbName;
        }
        public void Script()
        {
            Server s = new Server(serverName);

            Scripter scripter = new Scripter(s);

            List<SqlSmoObject> objects = new List<SqlSmoObject>();

            Database db = new Database();

            foreach (Database d in s.Databases)
            {
                if (d.Name == dbName)
                {
                    db = d;
                }
            }
            

            int ignoreCount = 0;

            foreach (Table t in db.Tables)
            {
                if (!t.IsSystemObject)
                {
                    objects.Add(t);
                }
                else
                {
                    ignoreCount++;
                }
            }

            foreach (StoredProcedure sp in db.StoredProcedures)
            {
                if (!sp.IsSystemObject)
                {
                    objects.Add(sp);
                }
                else
                {
                    ignoreCount++;
                }
            }

            foreach (UserDefinedFunction f in db.UserDefinedFunctions)
            {
                if (!f.IsSystemObject)
                {
                    objects.Add(f);
                }
                else
                {
                    ignoreCount++;
                }
            }

            Console.WriteLine("{0} objects to script, {1} ignored", objects.Count, ignoreCount);

            scripter.Options.Add(ScriptOption.WithDependencies);
            Console.WriteLine(scripter.Options);

            StringCollection commands = scripter.Script(objects.ToArray());

            foreach (string cmd in commands)
            {
                Console.WriteLine(cmd);
            }
        }
    }
}
