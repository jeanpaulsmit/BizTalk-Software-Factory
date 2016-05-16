using System;
using System.Text;
using System.Runtime.Serialization;
using Microsoft.Practices.RecipeFramework.VisualStudio.Templates;
using EnvDTE;

namespace BizTalkSoftwareFactory.References
{
    /// <summary>
    /// Reference that makes sure an item only 
    /// applies to the BizTalk Maps project
    /// </summary>
    [Serializable]
    public class MapsProjectReference : UnboundTemplateReference
    {
        public MapsProjectReference(string template) : base(template)
        {
        }

        /// <summary>
        /// This method makes sure this item can only be added to a BizTalk Maps project 
        /// </summary>
        /// <param name="target">Project the user clicked on</param>
        /// <returns>True if the item can be added here, otherwise false</returns>
        public override bool IsEnabledFor(object target)
        {
            // Check if this is a multi project or single project solution
            if (target is Project)
            {
                return ((Project)target).Name.EndsWith(".Maps");
            }
            else if (target is ProjectItem)
            {
                return ((ProjectItem)target).Name.EndsWith("Maps");
            }
            else
            {
                return false;
            }
        }

        public override string AppliesTo
        {
            get { return "The Maps Project in the BizTalk Solution"; }
        }

        #region ISerializable Members

        /// <summary>
        /// Required constructor for deserialization.
        /// </summary>
        protected MapsProjectReference(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion ISerializable Members
    }
}
