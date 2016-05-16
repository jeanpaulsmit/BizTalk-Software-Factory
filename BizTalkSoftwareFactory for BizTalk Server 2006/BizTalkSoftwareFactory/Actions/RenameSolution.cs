#region Using Directives

using System;
using System.IO;
using Microsoft.Practices.ComponentModel;
using Microsoft.Practices.RecipeFramework;
using EnvDTE;

#endregion

namespace BizTalkSoftwareFactory.Actions
{
    /// <summary>
    /// This action renames the solution and gives it its final name
    /// </summary>
    public class RenameSolutionAction : Action
    {
        /// <summary>
        /// The new name of the solution.
        /// </summary>
        private string m_NewSolutionName;

        /// <summary>
        /// Gets or sets the new name of the solution.
        /// </summary>
        [Input(Required = true)]
        public string NewSolutionName
        {
            get
            {
                return m_NewSolutionName;
            }
            set
            {
                m_NewSolutionName = value;
            }
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public override void Execute()
        {
            DTE vs = this.GetService<DTE>(true);

            string newFilename = this.NewSolutionName;
            string originalSolutionPath = (string)vs.Solution.Properties.Item("Path").Value;
            string originalSolutionDir = Path.GetDirectoryName(originalSolutionPath);

            // Check if it is an absolute or relative path.
            if (!Path.IsPathRooted(this.NewSolutionName))
            {
                // Relative path from the current solution root.
                newFilename = Path.Combine(originalSolutionDir, this.NewSolutionName);
            }

            // Make sure the destination directory exists.
            Directory.CreateDirectory(Path.GetDirectoryName(newFilename));

            // Save the current solution as the new file name.
            vs.Solution.SaveAs(newFilename);

            // Delete the old solution.
            File.Delete(originalSolutionPath);
            string suoFile = Path.GetFileNameWithoutExtension(originalSolutionPath) + ".suo";
            string suoFilePath = Path.Combine(originalSolutionDir, suoFile);
            File.Delete(suoFilePath);
        }

        /// <summary>
        /// Performs an undo of the action.
        /// </summary>
        public override void Undo()
        {
        }
    }
}
