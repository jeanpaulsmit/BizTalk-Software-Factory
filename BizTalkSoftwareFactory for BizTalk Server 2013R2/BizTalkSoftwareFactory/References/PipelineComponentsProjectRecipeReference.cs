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
  public class PipelineComponentsProjectRecipeReference : UnboundRecipeReference
  {
    public PipelineComponentsProjectRecipeReference(string template)
      : base(template)
    {
    }

    /// <summary>
    /// This method makes sure this item can only be added to the PipelineComponents project, and only if the target is present
    /// </summary>
    /// <param name="target">Project the user clicked on</param>
    /// <returns>True if the item can be added here, otherwise false</returns>
    public override bool IsEnabledFor(object target)
    {
      // Input validation
      if (target == null)
      {
        return false;
      }

      // It cannot not be a BizTalk project and it must end with 'UnitTests'
      bool retVal = false;
      if (target is Project)
      {
        // Check if this was called on the UnitTest project, if not we don't even have to look further
        retVal = ((Project)target).Name.EndsWith(".PipelineComponents");
      }
      return retVal;
    }

    public override string AppliesTo
    {
      get { return "The PipelineComponent Project in the Solution"; }
    }

    #region ISerializable Members

    /// <summary>
    /// Required constructor for deserialization.
    /// </summary>
    protected PipelineComponentsProjectRecipeReference(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion ISerializable Members
  }
}
