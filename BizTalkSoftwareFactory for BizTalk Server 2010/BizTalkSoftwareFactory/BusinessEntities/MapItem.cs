using System;
using System.Collections.Generic;
using System.Text;

namespace BizTalkSoftwareFactory.BusinessEntities
{
    /// <summary>
    /// This is a Business Entity representing a Map, used by the MapProject class in the Business Components
    /// </summary>
    class MapItem
    {
        public MapItem()
        {
        }

        // Constuctor to set the name
        public MapItem(string name)
        {
            this.Name = name;
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Override ToString method because that will be called by the wizard
        /// and we don't want to display the wrong name
        /// </summary>
        /// <returns>The MapName with rootnode in the class</returns>
        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
    }
}
