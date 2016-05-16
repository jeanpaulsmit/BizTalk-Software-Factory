using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.RecipeFramework;
using EnvDTE;

namespace BizTalkSoftwareFactory.Actions
{
    /// <summary>
    /// This action is used to add a class library project to a solution
    /// A class library is used in this case as helper project, but it can also be a unit test project
    /// </summary>
    class AddClassLibraryProjectToSolution : Action
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
        /// Gets or sets the Class Library project name.
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
        /// Location of the template to add
        /// </summary>
        private string projectTemplate;

        /// <summary>
        /// Gets or sets the location of the template.
        /// </summary>
        [Input(Required = true)]
        public string ProjectTemplate
        {
            get
            {
                return projectTemplate;
            }
            set
            {
                projectTemplate = value;
            }
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public override void Execute()
        {
            // Skip action?
            if (!AddProject)
            {
                return;
            }

            DTE vs = this.GetService<DTE>(true);

            string bsfTemplateLocation = ProjectTemplate; // @"Templates\Projects\ClassLibrary\ClassLibrary.vstemplate";
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
