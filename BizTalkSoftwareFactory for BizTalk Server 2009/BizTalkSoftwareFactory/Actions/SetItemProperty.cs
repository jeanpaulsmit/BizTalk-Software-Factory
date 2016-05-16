#region Using Directives

using System;
using System.IO;
using Microsoft.Practices.ComponentModel;
using Microsoft.Practices.RecipeFramework;
using EnvDTE;
using Microsoft.VisualStudio.Shell.Interop;
using BizTalkSoftwareFactory.BusinessComponents;


#endregion

namespace BizTalkSoftwareFactory.Actions
{
    /// <summary>
    /// This action renames an item after it is created.
    /// </summary>
    class SetItemProperty : Action
    {
        #region Input Properties

        /// <summary>
        /// The currentitem
        /// </summary>
        private ProjectItem currentItem;
       
        /// <summary>
        /// The name of the item
        /// </summary>
        private string propertyName;

        /// <summary>
        /// The value of the item
        /// </summary>
        private string propertyValue;

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
        public string PropertyName
        {
            get { return propertyName; }
            set { propertyName = value; }
        }

        /// <summary>
        /// Gets or sets the new item name
        /// </summary>
        [Input]
        public string PropertyValue
        {
            get { return propertyValue; }
            set { propertyValue = value; }
        }

        #endregion

        #region IAction Members

        /// <summary>
        /// Executes the action.
        /// </summary>
        public override void Execute()
        {
            IVsSolution solution = (IVsSolution)GetService(typeof(SVsSolution));
            Helper.SetProjectItemProperty(solution, CurrentItem, PropertyName, PropertyValue);
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
