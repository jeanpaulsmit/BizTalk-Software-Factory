using System;
using System.Collections.Generic;
using System.Text;

namespace BizTalkSoftwareFactory.BusinessEntities
{
    /// <summary>
    /// This is a Business Entity representing a schema, used by the SchemaProject class in the Business Components
    /// </summary>
    class SchemaItem
    {
        public SchemaItem()
        {
        }

        // Constuctor to set the name and other attributes
        public SchemaItem(string targetNamespace, string name, string schemaRootNode, string path)
        {
            this.TargetNamespace = targetNamespace;
            this.Name = name;
            this.RootNode = schemaRootNode;
            this.Path = path;
        }

        /// <summary>
        /// The name of the schema
        /// </summary>
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// The path of the Schema
        /// </summary>
        private string path;

        public string Path
        {
          get { return path; }
          set { path = value; }
        }

        /// <summary>
        /// The rootnode of the schema
        /// </summary>
        private string rootNode;

        public string RootNode
        {
            get { return rootNode; }
            set { rootNode = value; }
        }

        /// <summary>
        /// The namespace of the schema
        /// </summary>
        private string targetNamespace;

        public string TargetNamespace
        {
            get { return targetNamespace; }
            set { targetNamespace = value; }
        }

        /// <summary>
        /// Override ToString method because that will be called by the wizard
        /// and we don't want to display the wrong name
        /// </summary>
        /// <returns>The name with rootnode in the class</returns>
        public override string ToString()
        {
          return string.Format("{0}", Name);
        }
    }
}
