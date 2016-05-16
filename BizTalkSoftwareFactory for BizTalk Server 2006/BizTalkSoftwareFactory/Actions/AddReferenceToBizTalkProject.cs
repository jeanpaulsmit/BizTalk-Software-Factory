#region Using Directives

using System;
using Microsoft.Practices.ComponentModel;
using Microsoft.Practices.RecipeFramework;
using EnvDTE;

#endregion

namespace BizTalkSoftwareFactory.Actions
{
    /// <summary>
    /// This action adds references from one project to another project
    /// </summary>
    class AddReferenceToBizTalkProjectAction : Action
    {
        #region Input Properties

        /// <summary>
        /// The referring project
        /// </summary>
        private Project m_ReferringProject;
        /// <summary>
        /// The project that is being referenced
        /// </summary>
        private Project m_ReferencedProject;

        /// <summary>
        /// Gets or sets the reffering project
        /// </summary>
        [Input]
        public Project ReferringProject
        {
            get { return m_ReferringProject; }
            set { m_ReferringProject = value; }
        } 

        /// <summary>
        /// Gets or sets the project that is being referenced
        /// </summary>
        [Input]
        public Project ReferencedProject
        {
            get { return m_ReferencedProject; }
            set { m_ReferencedProject = value; }
        } 

        #endregion

        #region IAction Members

        /// <summary>
        /// Executes the action.
        /// </summary>
        public override void Execute()
        {
            AddProjectReference(ReferringProject, ReferencedProject); 
        }

        /// <summary>
        /// Performs an undo of the action.
        /// </summary>
        public override void Undo()
        {
            
        }
        /// <summary>
        /// Adds the referenced project to the referrring project
        /// </summary>
        /// <param name="referringProject">the referrring project</param>
        /// <param name="referencedProject">the referenced project</param>
        private void AddProjectReference(Project referringProject, Project referencedProject)
        {
            // Input validation
            if ((referringProject == null) || (referencedProject == null))
            {
                return;
            }
            Microsoft.BizTalk.Studio.Extensibility.IBtsProject referringProjectBts = (Microsoft.BizTalk.Studio.Extensibility.IBtsProject)referringProject.Object;
            if (referringProjectBts != null)
            {
                ((Microsoft.BizTalk.Studio.Extensibility.IBtsReferences)referringProjectBts.References).AddProject(referencedProject);
            }
        }


        #endregion
    }
}
