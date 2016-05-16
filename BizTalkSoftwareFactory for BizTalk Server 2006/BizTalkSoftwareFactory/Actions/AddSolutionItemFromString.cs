using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

using EnvDTE;
using Microsoft.Practices.ComponentModel;
using Microsoft.Practices.RecipeFramework;
using EnvDTE80;
using Microsoft.Practices.RecipeFramework.Library;
using Microsoft.Practices.RecipeFramework.Extensions.Actions.VisualStudio;

namespace BizTalkSoftwareFactory.Actions
{
    [ServiceDependency(typeof(DTE))]
    public class AddSolutionItemFromStringAction : ConfigurableAction
    {
        // Fields
        private string content;
        private string relativePath;

        // Methods
        public override void Execute()
        {
            DTE dte = base.GetService<DTE>();

            string solutionDirectory = Path.GetDirectoryName((string)dte.Solution.Properties.Item("Path").Value);
            string targetPath = Path.Combine(solutionDirectory, this.RelativePath);
            string targetDir = Path.GetDirectoryName(targetPath);
            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }
            using (StreamWriter writer = new StreamWriter(targetPath, false, new UTF8Encoding(true, true)))
            {
                writer.WriteLine(this.content);
            }

            DteHelper.SelectSolution(dte);
            dte.ItemOperations.AddExistingItem(targetPath);
            dte.ActiveWindow.Close(EnvDTE.vsSaveChanges.vsSaveChangesNo);
        }

        public override void Undo()
        {
            //
        }

        // Properties
        [Input(Required = true)]
        public string Content
        {
            get
            {
                return this.content;
            }
            set
            {
                this.content = value;
            }
        }

        [Input(Required = true)]
        public string RelativePath
        {
            get
            {
                return this.relativePath;
            }
            set
            {
                this.relativePath = value;
            }
        }
    }
}