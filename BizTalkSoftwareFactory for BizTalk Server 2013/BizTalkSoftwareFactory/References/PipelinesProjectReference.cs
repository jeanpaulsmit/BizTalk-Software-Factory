using System;
using System.Text;
using System.Runtime.Serialization;
using Microsoft.Practices.RecipeFramework.VisualStudio.Templates;
using EnvDTE;

namespace BizTalkSoftwareFactory.References
{
    /// <summary>
    /// Reference that makes sure an item only 
    /// applies to the BizTalk Pipelines project
    /// </summary>
    [Serializable]
    public class PipelinesProjectReference : UnboundTemplateReference
    {
        public PipelinesProjectReference(string template)
            : base(template)
        {
        }

        /// <summary>
        /// This method makes sure this item can only be added to a BizTalk Pipelines project 
        /// </summary>
        /// <param name="target">Project the user clicked on</param>
        /// <returns>True if the item can be added here, otherwise false</returns>
        public override bool IsEnabledFor(object target)
        {
            // Check if this is a multi project or single project solution
            if (target is Project)
            {
                return ((Project)target).Name.EndsWith(".Pipelines");
            }
            else if (target is ProjectItem)
            {
                return ((ProjectItem)target).Name.EndsWith("Pipelines");
            }
            else
            {
                return false;
            }
        }

        public override string AppliesTo
        {
            get { return "The Pipelines Project in the BizTalk Solution"; }
        }

        #region ISerializable Members

        /// <summary>
        /// Required constructor for deserialization.
        /// </summary>
        protected PipelinesProjectReference(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion ISerializable Members
    }
}
