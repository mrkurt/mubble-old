using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Tools
{
    public class Path
    {
        private Path() { }

        public static string Combine(params string[] paths)
        {
            string path = "";
            if (paths.Length > 0)
            {
                path = paths[0];
            }
            for (int i = 1; i < paths.Length; i++)
            {
                path = System.IO.Path.Combine(path, paths[i]);
            }
            return path;
        }
    }
}
