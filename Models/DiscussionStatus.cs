using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Models
{
    public enum DiscussionStatus
    {
        NotCreated = 0,
        Exists = 1,
        CreationException = 2,
        PendingCreation = 3,
        Inactive = 4,
        Duplicate = 5
    }
}
