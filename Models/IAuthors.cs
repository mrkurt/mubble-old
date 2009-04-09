using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Models
{
    public interface IAuthors
    {
        IList<string> GetAuthors();
    }
}
