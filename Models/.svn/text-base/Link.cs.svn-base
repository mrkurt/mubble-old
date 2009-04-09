using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Models
{
    /// <summary>
    /// Represents a link to Mubble content.  It's comprised of a controller path and extra parameters.
    /// </summary>
    public class Link : IEquatable<Link>, ICloneable
    {
        private string path;

        /// <summary>
        /// Controller path
        /// </summary>
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        private string extra;

        /// <summary>
        /// Controller parameters
        /// </summary>
        public string Extra
        {
            get { return extra; }
            set { extra = value; }
        }

        private string title;

        /// <summary>
        /// Link title
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private LinkParameters _params = new LinkParameters();

        public LinkParameters Params
        {
            get { return _params; }
            set { _params = value; }
        }


        /// <summary>
        /// Creates a new Link instance
        /// </summary>
        public Link() { }

        /// <summary>
        /// Creates a new Link instance
        /// </summary>
        /// <param name="path">Path to a controller</param>
        public Link(string path) : this()
        {
            this.Path = path;
        }
        /// <summary>
        /// Creates a new Link instance
        /// </summary>
        /// <param name="path">Path to a controller</param>
        /// <param name="extra">Extra path items to pass to controller</param>
        public Link(string path, string extra) : this(path)
        {
            this.Extra = extra;
        }
        /// <summary>
        /// Creates a new Link instance
        /// </summary>
        /// <param name="path">Path to a controller</param>
        /// <param name="extra">Extra path items to pass to controller</param>
        /// <param name="title">The link's title</param>
        public Link(string path, string extra, string title)
            :
            this(path, extra)
        {
            this.Title = title;
        }

        #region IEquatable<Link> Members

        /// <summary>
        /// Determines whether the passed in link points to the same location as the instance.
        /// </summary>
        /// <param name="other">A link to compare to</param>
        /// <returns>True if the links point to the same place, false otherwise</returns>
        public bool Equals(Link other)
        {
            return other != null && this.Path == other.Path && this.Extra == other.Extra;
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return new Link(this.Path, this.Extra, this.Title);
        }

        #endregion
    }
}
