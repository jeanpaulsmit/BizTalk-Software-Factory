using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.RecipeFramework;
using EnvDTE;
using System.Reflection;
using System.IO;

namespace BizTalkSoftwareFactory.Actions
{
    /// <summary>
    /// Adds a new solution item.
    /// </summary>
    /// <remarks>This action was developed by Bernardo Heynemann(heynemann@gmail.com)
    /// and is part of the NMVP Software Factory (http://www.codeplex.com/nmvpfactory).</remarks>
    public class AddSolutionItemAction : ConfigurableAction
    {

        #region Fields
        private string from;
        private string to;
        #endregion Fields

        #region Properties
        /// <summary>
        /// The file to be added.
        /// </summary>
        [Input(Required = true)]
        public string From
        {
            get { return from; }
            set { from = value; }
        }
        /// <summary>
        /// The path to add the file to.
        /// </summary>
        [Input(Required = true)]
        public string To
        {
            get { return to; }
            set { to = value; }
        }
        #endregion Properties

        #region Execute and Undo
        /// <summary>
        /// Executes the action.
        /// </summary>
        public override void Execute()
        {
            Assembly execAsm = Assembly.GetExecutingAssembly();
            string fromPath = Path.Combine(Path.GetDirectoryName(execAsm.Location), From);
            if (!File.Exists(fromPath))
            {
                throw new InvalidOperationException(string.Format("From file does not exist at path {0}", fromPath));
            }

            DTE dte = GetService<DTE>();
            string solutionDirectory = Path.GetDirectoryName((string)dte.Solution.Properties.Item("Path").Value);
            string toPath = Path.Combine(solutionDirectory, To);
            string toDir = Path.GetDirectoryName(toPath);
            if (!Directory.Exists(toDir))
            {
                Directory.CreateDirectory(toDir);
            }

            if (!File.Exists(toPath))
            {
                File.Copy(fromPath, toPath);
            }

            dte.ItemOperations.AddExistingItem(toPath);
            dte.ActiveWindow.Close(EnvDTE.vsSaveChanges.vsSaveChangesNo);
        }


        /// <summary>
        /// Undo the action.
        /// </summary>
        public override void Undo() { }

        #endregion Execute and Undo
    }
}