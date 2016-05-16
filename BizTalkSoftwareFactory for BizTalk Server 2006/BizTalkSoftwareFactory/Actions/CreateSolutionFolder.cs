#region Using Directives

using System;
using Microsoft.Practices.ComponentModel;
using Microsoft.Practices.RecipeFramework;
using System.IO;
using System.Xml;
using System.Diagnostics;
using Microsoft.Practices.RecipeFramework.Library;
using EnvDTE80;

#endregion

namespace BizTalkSoftwareFactory.Actions
{
    /// <summary>
    /// This action creates a solution folder
    /// </summary>
    class CreateSolutionFolderAction : Action
    {
        /// <summary>
        /// The name of the folder to add as a Solution folder
        /// </summary>
        private string name;

        #region Input Properties

        /// <summary>
        /// Gets or sets the name of the solution folder
        /// </summary>
        [Input(Required=true)]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        #endregion

        #region IAction Members

        /// <summary>
        /// Executes the action.
        /// </summary>
        public override void Execute()
        {
            EnvDTE.DTE vs = this.GetService<EnvDTE.DTE>(true);

            // Add the solution folder
            Solution2 sln = (Solution2)vs.Solution;
            sln.AddSolutionFolder(Name);
        }

        /// <summary>
        /// Performs an undo of the action.
        /// </summary>
        public override void Undo()
        {
            
        }

        #endregion
    }
}
