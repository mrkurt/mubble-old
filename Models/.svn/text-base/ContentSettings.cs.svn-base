using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace Mubble.Models
{
    /// <summary>
    /// Stores the settings for instances of content.  Serializes to XML and back from XML.
    /// </summary>
    public class ContentSettings
    {
        private Dictionary<string, object> _settings;

        [XmlIgnore]
        private Dictionary<string, object> settings
        {
            get 
            {
                if (_settings == null)
                {
                    _settings = new Dictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);
                    if (Keys != null && Values != null && keys.Count == values.Count)
                    {
                        for (int i = 0; i < Keys.Count; i++)
                        {
                            _settings.Add(Keys[i], Values[i]);
                        }
                    }
                }
                return _settings; 
            }
        }

        private List<string> keys = new List<string>();

        public List<string> Keys
        {
            get { return keys; }
            set { keys = value; }
        }

        private List<object> values = new List<object>();

        public List<object> Values
        {
            get { return values; }
            set { values = value; }
        }

        /// <summary>
        /// Gets or sets various settings properties
        /// </summary>
        /// <param name="key">The property key</param>
        /// <returns>An object matching the key in the collection.  Null if it doesn't exist.</returns>
        public object this[string key]
        {
            get
            {
                if (this.settings.ContainsKey(key))
                {
                    return this.settings[key];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.settings.ContainsKey(key))
                {
                    this.Values[this.Keys.IndexOf(key.ToLower())] = value;
                    this.settings[key] = value;
                }
                else if(key != null)
                {
                    this.Keys.Add(key.ToLower());
                    this.Values.Add(value);
                    this.settings.Add(key, value);
                }
            }
        }

        #region Property Helpers
        /// <summary>
        /// Removes the specified key/value pair.  Returns the removed value, if it exists.
        /// </summary>
        /// <param name="key">The key to search for</param>
        /// <returns>The matching value, if it exists</returns>
        public object Remove(string key)
        {
            object value = this[key];

            if (settings.ContainsKey(key))
            {
                settings.Remove(key);
            }

            int indexOfKey = Keys.IndexOf(key.ToLower());
            if (indexOfKey >= 0)
            {
                Keys.RemoveAt(indexOfKey);
                Values.RemoveAt(indexOfKey);
            }

            return value;
        }
        #endregion

        /// <summary>
        /// Gets the configuration object from the specified XML file
        /// </summary>
        /// <param name="fileName">The full path to the serialized XML</param>
        /// <returns>Requested Module configuration object</returns>
        public static ContentSettings Load(string xml)
        {
            if (xml == null) return new ContentSettings();
            XmlSerializer s = new XmlSerializer(typeof(ContentSettings));
            TextReader txt = new StringReader(xml);

            try
            {
                ContentSettings settings = s.Deserialize(txt) as ContentSettings;

                if (settings == null) return new ContentSettings();

                return settings;
            }
            catch
            {
                return new ContentSettings();
            }
        }

        /// <summary>
        /// Serializes the current settings instance and returns a string of XML.
        /// </summary>
        /// <returns>A string of serialized XML</returns>
        public string Serialize()
        {
            XmlSerializer s = new XmlSerializer(typeof(ContentSettings));
            StringWriter str = new StringWriter();

            s.Serialize(str, this);

            return str.ToString();            
        }
    }
}