using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.RecipeFramework;
using EnvDTE;

namespace BizTalkSoftwareFactory.Actions
{
    /// <summary>
    /// This action takes care of creating a single project solution where all artifacts are stored 
    /// as folders in one project
    /// </summary>
    class CreateSingleProjectSolutionAction : Action
    {
        #region Input variables

        /// <summary>
        /// Indicates in fact if this action should be executed because this is only needed for a single project solution
        /// </summary>
        private bool isMultiProjectSolution;

        /// <summary>
        /// Name of the project
        /// </summary>
        private string projectName;

        /// <summary>
        /// Indicates in fact if a folder for schemas should be added
        /// </summary>
        private bool addSchemasFolder;

        /// <summary>
        /// Indicates in fact if a folder for maps should be added
        /// </summary>
        private bool addMapsFolder;

        /// <summary>
        /// Indicates in fact if a folder for orchestrations should be added
        /// </summary>
        private bool addOrchestrationsFolder;

        /// <summary>
        /// Indicates in fact if a folder for pipelines should be added
        /// </summary>
        private bool addPipelinesFolder;

        /// <summary>
        /// Indicates in fact if a folder for business components should be added
        /// </summary>
        private bool addBusinessComponentsFolder;

        /// <summary>
        /// Gets or sets if this action should be executed.
        /// This can be set for example in the wizard and gives the possibility to skip the action
        /// </summary>
        [Input(Required = true)]
        public bool IsMultiProjectSolution
        {
            get { return isMultiProjectSolution; }
            set { isMultiProjectSolution = value; }
        }

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
        /// Gets or sets if the Schemas folder should be added.
        /// </summary>
        [Input(Required = true)]
        public bool AddSchemasFolder
        {
            get { return addSchemasFolder; }
            set { addSchemasFolder = value; }
        }

        /// <summary>
        /// Gets or sets if the Maps folder should be added.
        /// </summary>
        [Input(Required = true)]
        public bool AddMapsFolder
        {
            get { return addMapsFolder; }
            set { addMapsFolder = value; }
        }

        /// <summary>
        /// Gets or sets if the Orchestrations folder should be added.
        /// </summary>
        [Input(Required = true)]
        public bool AddOrchestrationsFolder
        {
            get { return addOrchestrationsFolder; }
            set { addOrchestrationsFolder = value; }
        }

        /// <summary>
        /// Gets or sets if the Pipelines folder should be added.
        /// </summary>
        [Input(Required = true)]
        public bool AddPipelinesFolder
        {
            get { return addPipelinesFolder; }
            set { addPipelinesFolder = value; }
        }

        /// <summary>
        /// Gets or sets if the BusinessComponents folder should be added.
        /// </summary>
        [Input(Required = true)]
        public bool AddBusinessComponentsFolder
        {
            get { return addBusinessComponentsFolder; }
            set { addBusinessComponentsFolder = value; }
        }

        #endregion
    
        /// <summary>
        /// Executes the action.
        /// </summary>
        public override void Execute()
        {
            // Skip action?
            if (IsMultiProjectSolution)
            {
                return;
            }

            DTE vs = this.GetService<DTE>(true);

            // Add a single project and add the necessary folders later
            string bsfTemplateLocation = @"Templates\Solutions\Projects\BizTalkApplication\BizTalkApplication.vstemplate";
            bsfTemplateLocation = BusinessComponents.Helper.GetBSFTemplateLocation() + bsfTemplateLocation;
            string projectToAddTemplateTo = BusinessComponents.Helper.GetProjectLocation(vs) + ProjectName;

            // Add the project to the solution and retrieve a reference (by default the returned project is null in this case)
            Project singleProject = null;
            vs.Solution.AddFromTemplate(bsfTemplateLocation, projectToAddTemplateTo, ProjectName, false);
            if (vs.Solution.Projects.Count == 1)
            {
                singleProject = vs.Solution.Projects.Item(1);
            }

            // Valid project reference found?
            if (singleProject != null)
            {
                // Add a binding folder
                singleProject.ProjectItems.AddFolder("Bindings", Constants.vsProjectItemKindPhysicalFolder);

                // Schemas folder wanted?
                if (AddSchemasFolder)
                {
                    singleProject.ProjectItems.AddFolder("Schemas", Constants.vsProjectItemKindPhysicalFolder);
                }
                // Maps folder wanted?
                if (AddMapsFolder)
                {
                    singleProject.ProjectItems.AddFolder("Maps", Constants.vsProjectItemKindPhysicalFolder);
                }
                // Orchestrations folder wanted?
                if (AddOrchestrationsFolder)
                {
                    singleProject.ProjectItems.AddFolder("Orchestrations", Constants.vsProjectItemKindPhysicalFolder);
                }
                // Pipelines folder wanted?
                if (AddPipelinesFolder)
                {
                    singleProject.ProjectItems.AddFolder("Pipelines", Constants.vsProjectItemKindPhysicalFolder);
                }
                // Business Components folder wanted?
                if (AddBusinessComponentsFolder)
                {
                    singleProject.ProjectItems.AddFolder("BusinessComponents", Constants.vsProjectItemKindPhysicalFolder);
                }
            }
        }

        /// <summary>
        /// Performs an undo of the action.
        /// </summary>
        public override void Undo()
        {
        }
    }
}
