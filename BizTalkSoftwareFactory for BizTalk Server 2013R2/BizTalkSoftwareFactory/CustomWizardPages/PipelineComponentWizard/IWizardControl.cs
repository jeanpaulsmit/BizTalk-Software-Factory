using System;
using System.Collections.Generic;
using System.Text;

namespace BizTalkSoftwareFactory.CustomWizardPages.PipelineComponentWizard
{
  /// <summary>
  /// Summary description for WizardControlInterface.
  /// </summary>
  internal interface IWizardControl
  {
    bool NextButtonEnabled
    {
      get;
    }
    bool NeedSummary
    {
      get;
    }
  }
}
