#region Using Directives

using System;
using System.IO;
using Microsoft.Practices.ComponentModel;
using Microsoft.Practices.RecipeFramework;
using EnvDTE;
using Microsoft.VisualStudio.Shell.Interop;
using BizTalkSoftwareFactory.BusinessComponents;
using BizTalkSoftwareFactory.BusinessEntities;
using System.Xml;


#endregion

namespace BizTalkSoftwareFactory.Actions
{
    /// <summary>
    /// This action renames an item after it is created.
    /// </summary>
    class SetMapRootNodes : Microsoft.Practices.RecipeFramework.Action
    {
        #region Input Properties

        /// <summary>
        /// The currentitem
        /// </summary>
        private ProjectItem currentItem;
        /// <summary>
        /// The new name of the item
        /// </summary>
        private SchemaItem sourceSchema;
        private SchemaItem destinationSchema;
        private string mapName;

        /// <summary>
        /// Gets or sets the current item
        /// </summary>
        [Input]
        public ProjectItem CurrentItem
        {
            get { return currentItem; }
            set { currentItem = value; }
        }

        /// <summary>
        /// Gets or sets the new item name
        /// </summary>
        [Input]
        public SchemaItem Source
        {
            get { return sourceSchema; }
            set { sourceSchema = value; }
        }
        [Input]
        public SchemaItem Destination
        {
            get { return destinationSchema; }
            set { destinationSchema = value; }
        }
        [Input]
        public string MapName
        {
            get { return mapName; }
            set { mapName = value; }
        }

        #endregion

        #region IAction Members

        /// <summary>
        /// Executes the action.
        /// </summary>
        public override void Execute()
        {
            string currentPath = (string)CurrentItem.Properties.Item("FullPath").Value;

            // replace <SrcTree/> with this:
            // <SrcTree><Reference Location="$SourceNamespace$.Schemas.$SourceSchema$"/><SrcTree/>
            // replace <TrgTree/> with this:
            // <TrgTree><Reference Location="$DestinationNamespace$.Schemas.$DestinationSchema$"/><TrgTree/>

            string sourceTemplate = "<Reference Location='$SourceNamespace$.$SourceSchema$'/>";
            string destinationTemplate = "<Reference Location='$DestinationNamespace$.$DestinationSchema$'/>";

            // This is necessary because the file has already been renamed but the properties haven't been updated yet
            string correctFilepath = Path.GetDirectoryName(currentPath) + @"\" + mapName;
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(correctFilepath);
            string source = sourceTemplate.Replace("$SourceNamespace$", Source.SchemaNamespace)
                                        .Replace("$DestinationNamespace$", Destination.SchemaNamespace)
                                        .Replace("$SourceSchema$", Source.Name)
                                        .Replace("$DestinationSchema$", Destination.Name);
            string destination = destinationTemplate.Replace("$SourceNamespace$", Source.SchemaNamespace)
                                        .Replace("$DestinationNamespace$", Destination.SchemaNamespace)
                                        .Replace("$SourceSchema$", Source.Name)
                                        .Replace("$DestinationSchema$", Destination.Name);
            xdoc.SelectSingleNode("//SrcTree").InnerXml = source;
            xdoc.SelectSingleNode("//TrgTree").InnerXml = destination;

            xdoc.Save(correctFilepath);
        }

        /// <summary>
        /// Performs an undo of the action.
        /// </summary>
        public override void Undo()
        {

        }

        #endregion
    }
}
