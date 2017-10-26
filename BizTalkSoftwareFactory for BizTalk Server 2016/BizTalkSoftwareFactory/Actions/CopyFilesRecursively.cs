using System;
using System.IO;

using BizTalkSoftwareFactory.CustomWizardPages.PipelineComponentWizard.Properties;
using Microsoft.Practices.RecipeFramework.Configuration;
using Microsoft.Practices.RecipeFramework;
using EnvDTE;

namespace BizTalkSoftwareFactory.Actions
{
    public class CopyFilesRecursively : ConfigurableAction
    {
        // Fields
        private string _searchPattern;
        private string _sourceDir;
        private string _relativeTargetDir;

        #region Properties

        // Properties
        [Input(Required = true)]
        public string SearchPattern
        {
            get
            {
                return this._searchPattern;
            }
            set
            {
                this._searchPattern = value;
            }
        }

        [Input(Required = true)]
        public string SourceDir
        {
            get
            {
                return this._sourceDir;
            }
            set
            {
                this._sourceDir = value;
            }
        }

        [Input(Required = true)]
        public string RelativeTargetDir
        {
            get
            {
                return this._relativeTargetDir;
            }
            set
            {
                this._relativeTargetDir = value;
            }
        }

#endregion

        // Methods
        public override void Execute()
        {
            DTE dte = base.GetService<DTE>();

            string bsfTemplateLocation = SourceDir; // @"Templates\Projects\Deployment";
            bsfTemplateLocation = BusinessComponents.Helper.GetBSFTemplateLocation() + bsfTemplateLocation;

            string solutionDirectory = Path.GetDirectoryName((string)dte.Solution.Properties.Item("Path").Value);
            string targetPath = Path.Combine(solutionDirectory, this.RelativeTargetDir);
            CopyDirRecursively(new DirectoryInfo(bsfTemplateLocation), new DirectoryInfo(targetPath));
        }

        private void CopyDirRecursively(DirectoryInfo inDi, DirectoryInfo outDi)
        {
            foreach (DirectoryInfo di in inDi.GetDirectories())
            {
                string newOutDir = Path.Combine(outDi.FullName, di.Name);
                GuardFolderExists(newOutDir);
                CopyDirRecursively(di, new DirectoryInfo(newOutDir));
            }

            foreach (FileInfo fi in inDi.GetFiles(this.SearchPattern))
            {
                GuardFolderExists(outDi.FullName);
                string newOutFile = Path.Combine(outDi.FullName, fi.Name);
                File.Copy(fi.FullName, newOutFile);
            }
        }

        private void GuardFolderExists(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }

        public override void Undo()
        {
        }
    }
}