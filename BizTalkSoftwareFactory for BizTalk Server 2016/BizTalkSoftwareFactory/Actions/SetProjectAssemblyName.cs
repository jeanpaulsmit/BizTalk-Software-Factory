#region Using Directives

using System;
using Microsoft.Practices.ComponentModel;
using Microsoft.Practices.RecipeFramework;
using EnvDTE;
using System.Globalization;

#endregion

namespace BizTalkSoftwareFactory.Actions
{
    public class SetProjectAssemblyNameAction : ConfigurableAction
    {
        [Input(Required = true)]
        public bool SkipThis
        { get; set; }

        [Input(Required = true)]
        public Project Project
        { get; set; }

        [Input(Required = true)]
        public string ProjectName
        { get; set; }

        public override void Execute()
        {
            if (!SkipThis)
            {
                if (Project.Properties.Item("AssemblyName") != null)
                {
                    Project.Properties.Item("AssemblyName").Value = string.Format(CultureInfo.InvariantCulture, "{0}", ProjectName);
                }
                if (Project.Properties.Item("RootNamespace") != null)
                {
                    Project.Properties.Item("RootNamespace").Value = string.Format(CultureInfo.InvariantCulture, "{0}", ProjectName);
                }
                Project.Save();
            }
        }

        public override void Undo()
        {
        }
    }
}
