using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{
	public partial class Author
	{
        /// <summary>
        /// Retrieves a list of authors from an array of names.  If there is no author for an associated name, 
        /// a new author instance is created
        /// (but not saved) and added to the list.  If a resulting DisplayName is blank, it's set to the username.
        /// </summary>
        /// <param name="names">An array of author usernames</param>
        /// <returns>A list of authors, including usernames that didn't return a corresponding author.</returns>
        public static List<Author> GetAuthors(params string[] names)
        {
            List<Author> authors = new List<Author>();
            foreach (string a in names)
            {
                Author author = Author.FindFirst(a);
                if (author == null)
                {
                    author = new Author();
                    author.UserName = a;
                }
                if (author.DisplayName == null) author.DisplayName = author.UserName;
                authors.Add(author);
            }
            return authors;
        }

        public bool Exists
        {
            get { return this.ID != Guid.Empty; }
        }
    }
}
