using System;
using System.Text;
using System.Runtime.Serialization;
using Microsoft.Practices.RecipeFramework;
using Microsoft.Practices.RecipeFramework.VisualStudio.Templates;
using EnvDTE;
using BizTalkSoftwareFactory.BusinessComponents;
using Microsoft.Practices.RecipeFramework.Library;

namespace BizTalkSoftwareFactory.References
{
    /// <summary>
    /// Reference that makes sure an item only 
    /// applies to the Unit test project
    /// </summary>
    [Serializable]
    public class UnitTestProjectRecipeReference : UnboundRecipeReference
    {
        public UnitTestProjectRecipeReference(string template) : base(template)
        {
        }

        /// <summary>
        /// This method makes sure this item can only be added to the Unit test Maps project, and only if the target is present
        /// so that means if this is called by the recipe "NewItemUnitTestMaps" we must make sure there is a Map project
        /// </summary>
        /// <param name="target">Project the user clicked on</param>
        /// <returns>True if the item can be added here, otherwise false</returns>
        public override bool IsEnabledFor(object target)
        {
          // Input validation
          if(target ==null)
          {
            return false;
          }

          // It cannot not be a BizTalk project and it must end with 'UnitTests'
          bool retVal = false;
          if (target is Project)
          {
            // Check if this was called on the UnitTest project, if not we don't even have to look further
            retVal = (((Project)target).Kind != BusinessComponents.Constants.BizTalkProjectType && ((Project)target).Name.EndsWith(".UnitTests"));

            // Then check if the requested project is present
            if (retVal == true)
            {
              DTE vs = base.GetService<DTE>(true);
              foreach (Project proj in vs.Solution.Projects)
              {
                // This depends on the AssetName (or recipe name) containing the suffix of the project type like 'Maps' or 'Schemas'
                if (Key.IndexOf(proj.Name.Substring(proj.Name.LastIndexOf('.') + 1)) > 0)
                {
                  retVal = true;
                  break;
                }
              }
            }
          }
          return retVal;
        }

        public override string AppliesTo
        {
            get { return "The UnitTest Project in the Solution"; }
        }

        #region ISerializable Members

        /// <summary>
        /// Required constructor for deserialization.
        /// </summary>
        protected UnitTestProjectRecipeReference(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion ISerializable Members
    }
}
