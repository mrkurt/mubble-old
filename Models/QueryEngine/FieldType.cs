using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Models.QueryEngine
{
    /// <summary>
    /// Describes the type of parsing and indexing to do on the fields
    /// </summary>
    public enum FieldType
    {
        /// <summary>
        /// Field is not parsed, but is indexed and stored
        /// </summary>
        Keyword,
        /// <summary>
        /// Field is parsed and indexed and stored
        /// </summary>
        Text,
        /// <summary>
        /// Field is parsed and indexed, but not stored
        /// </summary>
        TextUnStored,
        /// <summary>
        /// Field is stored but not indexed
        /// </summary>
        UnIndexed,
        /// <summary>
        /// Field is indexed but not stored
        /// </summary>
        UnStored
    }
}
