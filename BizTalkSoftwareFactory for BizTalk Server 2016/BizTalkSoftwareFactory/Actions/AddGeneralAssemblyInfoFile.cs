#region Using Directives

using System;
using Microsoft.Practices.ComponentModel;
using Microsoft.Practices.RecipeFramework;
using System.IO;
using System.Xml;
using System.Diagnostics;
using Microsoft.Practices.RecipeFramework.Library;
using Microsoft.Win32;
using VSLangProj;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using BizTalkSoftwareFactory.OperationalManagement;

#endregion

namespace BizTalkSoftwareFactory.Actions
{
    /// <summary>
    /// This action add a GeneralAssemblyInfoFile
    /// </summary>
    class AddGeneralAssemblyInfoFile : Microsoft.Practices.RecipeFramework.Action
    {
        /// <summary>
        /// The name of the generalAssemblyInfoFile (used if generalAssemblyInfoFile not specified)
        /// </summary>
        private string name;
        /// <summary>
        /// The path of the generalAssemblyInfoFile (existing)
        /// </summary>
        private string generalAssemblyInfoFile;

        #region Input Properties

        /// <summary>
        /// Gets or sets the name of the generalAssemblyInfoFile
        /// </summary>
        [Input(Required = true)]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets or sets the path of the existing generalAssemblyInfoFile
        /// </summary>
        [Input]
        public string GeneralAssemblyInfoFile
        {
            get { return generalAssemblyInfoFile; }
            set { generalAssemblyInfoFile = value; }
        }

        #endregion

        #region IAction Members

        /// <summary>
        /// Executes the action.
        /// </summary>
        public override void Execute()
        {
            if (this.GeneralAssemblyInfoFile == null)
            {
                return;
            }

            EnvDTE.DTE vs = this.GetService<EnvDTE.DTE>(true);

            //find Assembly Attribute
            var regex = new Regex(@"(?<=(\[assembly:(\W*)))Assembly(.*)(?=\()", RegexOptions.IgnoreCase);

            //get all attribute names
            var generallAttributes = regex.Matches(File.ReadAllText(generalAssemblyInfoFile)).Cast<Match>().Select(match => match.Value).ToList();

            //set the assemblykeyfile property for every Project
            foreach (EnvDTE.Project project in vs.Solution.Projects)
            {
                if (project.Properties != null)
                {
                    if (project.Kind == BusinessComponents.Constants.ClassLibraryProjectType)
                    {
                        //add GeneralAssemblyInfoFile in PropertiesFolder
                        var propertiesFolder = project.ProjectItems.Item("Properties");

                        //check generalAssemblyFile allready added, then continue
                        if (propertiesFolder.ProjectItems.Exists(Path.GetFileName(generalAssemblyInfoFile)))                        
                            continue;                        

                        propertiesFolder.ProjectItems.AddFromFile(generalAssemblyInfoFile);
                        
                        // determine filename of AssemblyInfo file
                        var assemblyInfoFile = propertiesFolder.ProjectItems.Item("AssemblyInfo.cs").FileNames[0];

                        // get all lines
                        var assemblyInfoContent = File.ReadAllLines(assemblyInfoFile);

                        List<string> newLines = new List<string>();
                        //loop trough lines of default assembly
                        foreach (var l in assemblyInfoContent)
                        {
                            //check line has Attribute, if so continue
                            if (regex.IsMatch(l) && generallAttributes.Contains(regex.Match(l).Value))
                            {
                                continue;
                            }
                            //add line to new lines list
                            newLines.Add(l);
                        }
                        //save new lines in assemblyinfo file
                        File.WriteAllLines(assemblyInfoFile, newLines);
                        
                    }
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
