using System;
using System.Collections.Generic;
using System.Text;

namespace BizTalkSoftwareFactory.BusinessComponents
{
  public abstract class TemplateHelper
  {
    public static void DebugThis(object obj)
		{
      // Check the content of the object here
      // Add the following line in the T4 template:
      // <#  TemplateHelper.DebugThis(this); #>
      // And a reference to this class of course"
      // <#@ Import Namespace="BizTalkSoftwareFactory.BusinessComponents" #>
    }
  }
}
