﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DslModeling = global::Microsoft.VisualStudio.Modeling;
using DslDesign = global::Microsoft.VisualStudio.Modeling.Design;
using DslValidation = global::Microsoft.VisualStudio.Modeling.Validation;
namespace BizTalkMessaging
{
	[DslValidation::ValidationState(DslValidation::ValidationState.Enabled)]
	public partial class BizTalkMessagingModel
	{
		/// <summary>
		/// Checks that the relationships that have a multiplicity of One or OneMany do actually have a link.
		/// </summary>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated code.")]
		[DslValidation::ValidationMethod(DslValidation::ValidationCategories.Open | DslValidation::ValidationCategories.Save | DslValidation::ValidationCategories.Menu)]
		private void ValidateBizTalkMessagingModelMultiplicity (DslValidation::ValidationContext context)
		{
			if (this.Broker == null)
			{
				context.LogViolation(DslValidation::ViolationType.Error,
					string.Format(global::System.Globalization.CultureInfo.CurrentCulture, 
						BizTalkMessaging.BizTalkMessagingDomainModel.SingletonResourceManager.GetString("MinimumMultiplicityMissingLink"), 
						"BizTalkMessagingModel", "", "Broker"),
						"DSL0001", this);
			}
		} // ValidateBizTalkMessagingModelMultiplicity
	} // class BizTalkMessagingModel
} // BizTalkMessaging

namespace BizTalkMessaging
{
	[DslValidation::ValidationState(DslValidation::ValidationState.Enabled)]
	public partial class Application
	{
		/// <summary>
		/// Checks that the relationships that have a multiplicity of One or OneMany do actually have a link.
		/// </summary>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated code.")]
		[DslValidation::ValidationMethod(DslValidation::ValidationCategories.Open | DslValidation::ValidationCategories.Save | DslValidation::ValidationCategories.Menu)]
		private void ValidateApplicationMultiplicity (DslValidation::ValidationContext context)
		{
			if (this.OutPorts.Count == 0)
			{
				context.LogViolation(DslValidation::ViolationType.Error,
					string.Format(global::System.Globalization.CultureInfo.CurrentCulture, 
						BizTalkMessaging.BizTalkMessagingDomainModel.SingletonResourceManager.GetString("MinimumMultiplicityMissingLink"), 
						"Application", this.Name, "OutPorts"),
						"DSL0001", this);
			}
		} // ValidateApplicationMultiplicity
	} // class Application
} // BizTalkMessaging

namespace BizTalkMessaging
{
	[DslValidation::ValidationState(DslValidation::ValidationState.Enabled)]
	public partial class OutPort
	{
		/// <summary>
		/// Checks that the relationships that have a multiplicity of One or OneMany do actually have a link.
		/// </summary>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated code.")]
		[DslValidation::ValidationMethod(DslValidation::ValidationCategories.Open | DslValidation::ValidationCategories.Save | DslValidation::ValidationCategories.Menu)]
		private void ValidateOutPortMultiplicity (DslValidation::ValidationContext context)
		{
			if (this.Applications.Count == 0)
			{
				context.LogViolation(DslValidation::ViolationType.Error,
					string.Format(global::System.Globalization.CultureInfo.CurrentCulture, 
						BizTalkMessaging.BizTalkMessagingDomainModel.SingletonResourceManager.GetString("MinimumMultiplicityMissingLink"), 
						"OutPort", "", "Applications"),
						"DSL0001", this);
			}
		} // ValidateOutPortMultiplicity
	} // class OutPort
} // BizTalkMessaging

	
 