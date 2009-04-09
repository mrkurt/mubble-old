using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.ComponentModel;

namespace Mubble.UI
{
    [ToolboxItem(false)]
    public class PathDependantRule : Mubble.UI.CompositeControl
    {
        private string pathPattern;
        
        [PersistenceMode(PersistenceMode.Attribute)]
        public string PathPattern
        {
            get { return pathPattern; }
            set { pathPattern = value; }
        }

        private PathDependantPatternType patternType = PathDependantPatternType.Simple;

        [PersistenceMode(PersistenceMode.Attribute)]
        public PathDependantPatternType PatternType
        {
            get { return patternType; }
            set { patternType = value; }
        }

        public bool MatchesPath(Mubble.Models.Controller content)
        {
            string pattern = this.PathPattern;
            if (this.PatternType == PathDependantPatternType.Simple)
            {
                pattern = string.Format("^{0}$", pattern.Replace("*", ".*"));
            }
            return Regex.IsMatch(content.Path, pattern, RegexOptions.IgnoreCase);
        }
    }

    public enum PathDependantPatternType
    {
        Simple,
        Regex
    }
}
