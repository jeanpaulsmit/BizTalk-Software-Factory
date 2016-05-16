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
    /// This action renames an item after it is created.
    /// </summary>
    class RenameItemAction : Action
    {
        #region Input Properties

        /// <summary>
        /// The currentitem
        /// </summary>
        private ProjectItem currentItem;
        /// <summary>
        /// The new name of the item
        /// </summary>
        private string newItemName;

        /// <summary>
        /// Gets or sets the current item
        /// </summary>
        [Input]
        public ProjectItem CurrentItem
        {
            get { return currentItem; }
            set { currentItem = value; }
        }

        /// <summary>
        /// Gets or sets the new item name
        /// </summary>
        [Input]
        public string NewItemName
        {
            get { return newItemName; }
            set { newItemName = value; }
        }

        #endregion

        #region IAction Members

        /// <summary>
        /// Executes the action.
        /// </summary>
        public override void Execute()
        {
            string currentPath = (string)CurrentItem.Properties.Item("FullPath").Value;
            string currentSolutionDir = Path.GetDirectoryName(currentPath);
            string newFilename = Path.Combine(currentSolutionDir, this.NewItemName);
            CurrentItem.SaveAs(newFilename);
            File.Delete(currentPath);
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
