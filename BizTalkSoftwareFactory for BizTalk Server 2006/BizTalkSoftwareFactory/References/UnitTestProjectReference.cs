using System;
using System.Text;
using System.Runtime.Serialization;
using Microsoft.Practices.RecipeFramework.VisualStudio.Templates;
using EnvDTE;

namespace BizTalkSoftwareFactory.References
{
    /// <summary>
    /// Reference that makes sure an item only 
    /// applies to the Unit test project
    /// </summary>
    [Serializable]
    public class UnitTestProjectReference : UnboundTemplateReference
    {
        public UnitTestProjectReference(string template) : base(template)
        {
        }

        /// <summary>
        /// This method makes sure this item can only be added to the Unit test Maps project 
        /// </summary>
        /// <param name="target">Project the user clicked on</param>
        /// <returns>True if the item can be added here, otherwise false</returns>
        public override bool IsEnabledFor(object target)
        {
            // It cannot be a BizTalk project and it must end with 'UnitTests'
            if (target is Project)
            {
                return ((Project)target).Kind != BusinessComponents.Constants.BizTalkProjectType && ((Project)target).Name.EndsWith(".UnitTests");
            }
            else
            {
                return false;
            }
        }

        public override string AppliesTo
        {
            get { return "The UnitTest Project in the Solution"; }
        }

        #region ISerializable Members

        /// <summary>
        /// Required constructor for deserialization.
        /// </summary>
        protected UnitTestProjectReference(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion ISerializable Members
    }
}
