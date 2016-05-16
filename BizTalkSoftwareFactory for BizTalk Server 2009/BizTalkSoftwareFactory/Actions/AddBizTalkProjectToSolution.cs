using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.RecipeFramework;
using EnvDTE;
using Microsoft.VisualStudio.Shell.Interop;
using BizTalkSoftwareFactory.BusinessComponents;

namespace BizTalkSoftwareFactory.Actions
{
    /// <summary>
    /// This action adds a BizTalk project template to a solution
    /// It is used to add multiple BizTalk projects to create a multi project solution
    /// </summary>
    class AddBizTalkProjectToSolution : Action
    {
        /// <summary>
        /// Indicates in fact if this action should be executed or not
        /// </summary>
        private bool addProject;

        /// <summary>
        /// Gets or sets if the project should be added.
        /// This can be set for example in the wizard and gives the possibility to skip the action
        /// </summary>
        [Input(Required = true)]
        public bool AddProject
        {
            get
            {
                return addProject;
            }
            set
            {
                addProject = value;
            }
        }

        /// <summary>
        /// Indicates in fact if this action should be executed or not
        /// </summary>
        private bool isMultiProjectSolution;

        /// <summary>
        /// Gets or sets if the project should be added.
        /// This can be set for example in the wizard and gives the possibility to skip the action
        /// </summary>
        [Input(Required = true)]
        public bool IsMultiProjectSolution
        {
            get
            {
                return isMultiProjectSolution;
            }
            set
            {
                isMultiProjectSolution = value;
            }
        }

        /// <summary>
        /// Name of the project to add
        /// </summary>
        private string projectName;

        /// <summary>
        /// Gets or sets the Maps project name.
        /// </summary>
        [Input(Required = true)]
        public string ProjectName
        {
            get
            {
                return projectName;
            }
            set
            {
                projectName = value;
            }
        }

        /// <summary>
        /// Name of the solution
        /// </summary>
        private string solutionName;

        /// <summary>
        /// Gets or sets the Solution  name.
        /// </summary>
        [Input(Required = true)]
        public string SolutionName
        {
            get
            {
                return solutionName;
            }
            set
            {
                solutionName = value;
            }
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public override void Execute()
        {
            // Skip action?
            if ((!AddProject) || (!IsMultiProjectSolution))
            {
                return;
            }

            DTE vs = this.GetService<DTE>(true);

            string bsfTemplateLocation = @"Templates\Solutions\Projects\BizTalkApplication\BizTalkApplication.vstemplate";
            bsfTemplateLocation = BusinessComponents.Helper.GetBSFTemplateLocation() + bsfTemplateLocation;
            string projectToAddTemplateTo = BusinessComponents.Helper.GetProjectLocation(vs) + ProjectName;

            vs.Solution.AddFromTemplate(bsfTemplateLocation, projectToAddTemplateTo, ProjectName, false);

            IVsSolution solution = (IVsSolution)GetService(typeof(SVsSolution));
            
            Helper.SetProjectProperty(solution, vs.Solution.Projects.Item(vs.Solution.Projects.Count), "ApplicationName", SolutionName, null, _PersistStorageType.PST_USER_FILE);
            Helper.SetProjectProperty(solution, vs.Solution.Projects.Item(vs.Solution.Projects.Count), "Server", "localhost", null, _PersistStorageType.PST_USER_FILE);
            Helper.SetProjectProperty(solution, vs.Solution.Projects.Item(vs.Solution.Projects.Count), "RestartHostInstances", "true", null, _PersistStorageType.PST_USER_FILE);
            
        }

        /// <summary>
        /// Performs an undo of the action.
        /// </summary>
        public override void Undo()
        {
        }

    }
}
