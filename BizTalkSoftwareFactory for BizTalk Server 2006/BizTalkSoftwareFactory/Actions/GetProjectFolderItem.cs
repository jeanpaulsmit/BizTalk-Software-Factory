#region Using Directives

using System;
using System.IO;
using Microsoft.Practices.ComponentModel;
using Microsoft.Practices.RecipeFramework;
using EnvDTE;
using Microsoft.Practices.RecipeFramework.Library;


#endregion

namespace BizTalkSoftwareFactory.Actions
{
    /// <summary>
    /// This action returns the project item for a specific folder in a project.
    /// </summary>
  class GetProjectFolderItemAction : ConfigurableAction
    {
        #region Input/Output Properties

        /// <summary>
        /// The current project
        /// </summary>
        private Project currentProject;
        
        /// <summary>
        /// The name of the folder to find
        /// </summary>
        private string folderName;

        /// <summary>
        /// The project item to be returned
        /// </summary>
        private ProjectItem item;

        /// <summary>
        /// Gets or sets the current Project
        /// </summary>
        [Input]
        public Project CurrentProject
        {
            get { return currentProject; }
            set { currentProject = value; }
        }

        /// <summary>
        /// Gets or sets the folder name
        /// </summary>
        [Input]
        public string FolderName
        {
            get { return folderName; }
            set { folderName = value; }
        }

        /// <summary>
        /// Gets or sets the Project item
        /// </summary>
        [Output]
        public ProjectItem Item
        {
          get { return item; }
          set { item = value; }
        }

        #endregion

        #region IAction Members

        /// <summary>
        /// Executes the action.
        /// </summary>
        public override void Execute()
        {
          // Input validation
          if(string.IsNullOrEmpty(folderName) || currentProject == null)
          {
            Item = null;
            return;
          }

          DTE service = base.GetService<DTE>(true);
          this.Item = DteHelper.FindItemByName(currentProject.ProjectItems, folderName, true);
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
