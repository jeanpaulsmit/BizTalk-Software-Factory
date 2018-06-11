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
    /// This action deletes an item.
    /// </summary>
    class DeleteItem : Microsoft.Practices.RecipeFramework.Action
    {
        #region Input Properties

        /// <summary>
        /// The currentitem
        /// </summary>
        private Project project;
        /// <summary>
        /// The new name of the item
        /// </summary>
        private string filenameToDelete;

        /// <summary>
        /// Gets or sets the current item
        /// </summary>
        [Input]
        public Project Project
        {
            get { return project; }
            set { project = value; }
        }

        /// <summary>
        /// Gets or sets the new item name
        /// </summary>
        [Input]
        public string FilenameToDelete
        {
          get { return filenameToDelete; }
          set { filenameToDelete = value; }
        }

        #endregion

        #region IAction Members

        /// <summary>
        /// Executes the action.
        /// </summary>
        public override void Execute()
        {
          int projItemCounter = project.ProjectItems.Count;
          for (int i = 1; i <= projItemCounter; i++)
          {
            if (project.ProjectItems.Item(i).Name == FilenameToDelete)
            {
              project.ProjectItems.Item(i).Delete();
              return;
            }
          }
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
