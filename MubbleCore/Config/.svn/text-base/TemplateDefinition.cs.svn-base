using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Mubble.Config
{
    /// <summary>
    /// Defines a Template set
    /// </summary>
    public class Template
    {
        private Guid id;
        /// <summary>
        /// The GUID associated with this template
        /// </summary>
        [XmlAttribute]
        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }
        private List<TemplateControl> controls = new List<TemplateControl>();

        /// <summary>
        /// Gets or sets the TemplateControls associated with this Template set
        /// </summary>
        public List<TemplateControl> Controls
        {
            get { return controls; }
            set { controls = value; }
        }

        /// <summary>
        /// Gets the configuration object from the specified XML file
        /// </summary>
        /// <param name="fileName">The full path to the serialized XML</param>
        /// <returns>Requested Template configuration object</returns>
        public static Template Load(string fileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                XmlSerializer s = new XmlSerializer(typeof(Mubble.Config.Template));
                TextReader txt = new StreamReader(fileName);

                Template t = (Template)s.Deserialize(txt);

                txt.Close();

                return t;
            }
            else
            {
                Template t = new Template();
                System.IO.FileInfo info = new FileInfo(fileName);
                int count = 0;
                foreach (FileInfo file in info.Directory.GetFiles())
                {
                    if (file.Name.EndsWith(".master"))
                    {
                        TemplateControl c = new TemplateControl();
                        c.Name = file.Name.Substring(0, file.Name.LastIndexOf('.'));
                        c.FileName = file.Name;
                        c.Order = count++;

                        t.Controls.Add(c);
                    }
                }
                return t;
            }
        }
    }

    /// <summary>
    /// Defines a TemplateControl
    /// </summary>
    public class TemplateControl : IComparable<TemplateControl>
    {
        private int order;

        /// <summary>
        /// Gets or sets the visual display order of this template control
        /// </summary>
        [XmlAttribute]
        public int Order
        {
            get { return order; }
            set { order = value; }
        }
        private string name;

        /// <summary>
        /// Gets or sets the friendly template control name
        /// </summary>
        [XmlAttribute]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string fileName;

        /// <summary>
        /// Gets or sets the filename for this template control
        /// </summary>
        [XmlAttribute]
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        #region IComparable<TemplateControl> Members

        public int CompareTo(TemplateControl other)
        {
            return this.Order.CompareTo(other.Order);
        }

        #endregion
}

}
