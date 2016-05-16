using System.Collections.Generic;
using System.ComponentModel;
using BizTalkSoftwareFactory.BusinessEntities;
using BizTalkSoftwareFactory.OperationalManagement;
using EnvDTE;

namespace BizTalkSoftwareFactory.BusinessComponents
{
    /// <summary>
    /// This business component reads orchestrations from projects and assemblies
    /// </summary>
    class OrchestrationProject
    {
        // Constant to define the class name used in the logging
        const string CLASS_NAME = "OrchestrationProject";

        /// <summary>
        /// Get the list of available Orchestrations from both projects and references.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>List of available Orchestrations</returns>
        public static List<OrchestrationItem> GetOrchestrationList(ITypeDescriptorContext context)
        {
            const string METHOD_NAME = "GetOrchestrationList";
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "Begin");

            // Input validation
            if (context == null)
            {
                return null;
            }

            List<OrchestrationItem> OrchestrationItem = new List<OrchestrationItem>();

            // Get the Orchestrations from a possible Orchestrations project in the solution
            GetOrchestrationsFromProject(Helper.GetProject((DTE)context.GetService(typeof(DTE)), BizTalkProjectType.Orchestrations), OrchestrationItem);

            // Return the list of Orchestrations
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "End");
            return OrchestrationItem;
        }

        /// <summary>
        /// This method returns the Orchestrations found in the supplied project type (mostly Orchestrations projects)
        /// </summary>
        /// <param name="btsProject">Project object referring to a project</param>
        /// <param name="orchestrationList">Reference to a list of orchestrations</param>
        private static void GetOrchestrationsFromProject(Project btsProject, List<OrchestrationItem> orchestrationList)
        {
            const string METHOD_NAME = "GetOrchestrationsFromProject";
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "Begin");

            // Input validatation
            if (btsProject == null || orchestrationList == null)
            {
                return;
            }

            // Go over every item and store the Orchestrations
            foreach (ProjectItem item in btsProject.ProjectItems)
            {
                // Scan only the Orchestrations, which end with 'odx'
                if (item.Name.EndsWith(".odx"))
                {
                  // Create a new OrchestrationItem object, the name is without .odx
                  OrchestrationItem newOrchestration = new OrchestrationItem(item.Name.Substring(0, item.Name.Length - 4));

                  // Add the found Orchestration to the list of Orchestrations
                  AddOrchestrationToList(orchestrationList, newOrchestration);
                }
            }
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "End");
        }

        /// <summary>
        /// This method adds a Orchestration item to the Orchestration list and checks for duplicates
        /// </summary>
        /// <param name="orchestrationList">List containing all Orchestrations</param>
        /// <param name="newOrchestration">Orchestration to add</param>
        private static void AddOrchestrationToList(List<OrchestrationItem> orchestrationList, OrchestrationItem newOrchestration)
        {
            const string METHOD_NAME = "AddOrchestrationToList";
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "Begin");

            // Input validatation
            if (newOrchestration == null || orchestrationList == null)
            {
                return;
            }
            orchestrationList.Add(newOrchestration);
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "End");
        }
    }
}
