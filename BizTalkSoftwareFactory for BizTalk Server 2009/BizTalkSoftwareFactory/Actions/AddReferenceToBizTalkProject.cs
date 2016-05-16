#region Using Directives

using System;
using Microsoft.Practices.ComponentModel;
using Microsoft.Practices.RecipeFramework;
using EnvDTE;
using VSLangProj;

#endregion

namespace BizTalkSoftwareFactory.Actions
{
    /// <summary>
    /// This action adds references from one project to another project
    /// </summary>
    class AddReferenceToBizTalkProject : Action
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
        /// <param name="referringProject">the referring project</param>
        /// <param name="referencedProject">the referenced project</param>
        private void AddProjectReference(Project referringProject, Project referencedProject)
        {
            // Input validation
            if ((referringProject == null) || (referencedProject == null))
            {
                return;
            }
            VSProject referringProjectBts = (VSProject)referringProject.Object;
            if (referringProjectBts != null)
            {
                referringProjectBts.References.AddProject(referencedProject);
            }
        }


        #endregion
    }
}
