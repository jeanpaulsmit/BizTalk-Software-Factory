using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.RecipeFramework;
using EnvDTE;

namespace BizTalkSoftwareFactory.Actions
{
    /// <summary>
    /// This action adds a BizTalk project template to a solution
    /// It is used to add multiple BizTalk projects to create a multi project solution
    /// </summary>
    class AddBizTalkProjectToSolutionAction : Action
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
        }

        /// <summary>
        /// Performs an undo of the action.
        /// </summary>
        public override void Undo()
        {
        }

    }
}
