using System;
using System.Collections.Generic;
using System.Text;

using EnvDTE;
using System.Reflection;

namespace BizTalkSoftwareFactory.BusinessComponents
{
    /// <summary>
    /// Defines all BizTalk Project types
    /// </summary>
    public enum BizTalkProjectType
    {
        Maps = 0,
        Schemas = 1,
        Orchestrations = 2,
        Pipelines = 4,
        Unknown = 8
    }

    /// <summary>
    /// Contains common methods to help other classes
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// Gets the project of the specified 
        /// type in the specified solution
        /// </summary>
        /// <param name="vs">the DTE object</param>
        /// <param name="type">the project type</param>
        /// <returns>the project for the specified type</returns>
        public static Project GetProject(DTE vs, BizTalkProjectType type)
        {
            foreach (Project project in vs.Solution)
            {
                if (GetBizTalkProjectType(project) == type)
                {
                    return project;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the project type for the given project
        /// </summary>
        /// <param name="project">a Project object</param>
        /// <returns>the project type</returns>
        private static BizTalkProjectType GetBizTalkProjectType(Project project)
        {
            string projectType = project.Name.Substring(project.Name.LastIndexOf('.') + 1);
            switch (projectType)
            {
                case "Maps": 
                    return BizTalkProjectType.Maps;
                case "Schemas": 
                    return BizTalkProjectType.Schemas;
                case "Orchestrations": 
                    return BizTalkProjectType.Orchestrations;
                case "Pipelines": 
                    return BizTalkProjectType.Pipelines;
                default:
                    return BizTalkProjectType.Unknown;
            }
        }

        /// <summary>
        /// Looks up the project path for the supplied project
        /// </summary>
        /// <param name="project">Project to lookup the project path for</param>
        /// <returns>Project path</returns>
        public static string GetProjectLocation(DTE project)
        {
            string retVal = null;
            if(project != null)
            {
                // The Path item will look like below
                // "C:\\Projects\\BSFSolution26\\BSFSolution26.sln"
                retVal = project.Solution.Properties.Item("Path").Value as string;
                retVal = retVal.Substring(0, retVal.LastIndexOf(@"\")) + @"\";
            }
            return retVal;
        }

        /// <summary>
        /// Looks up the path of the BSF templates, in fact this is the install folder of BSF
        /// </summary>
        /// <returns>Path to the templates</returns>
        public static string GetBSFTemplateLocation()
        {
            string retVal = null;

            // Get the path to the executing assembly, we need the path only so get rid of the assembly part
            string executingAssemblyPath = Assembly.GetExecutingAssembly().CodeBase;
            if(executingAssemblyPath != null)
            {
                // The path will look like below so we need to convert it
                //file:///C:/Program Files/BizTalk Software Factory/BizTalk Software Factory/BizTalkSoftwareFactory.dll"
                // First check on what drive the Program Files are installed
                string programFilesPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                string driveLetter = programFilesPath.Substring(0, 2);
                retVal = executingAssemblyPath.Substring(executingAssemblyPath.IndexOf(driveLetter), executingAssemblyPath.LastIndexOf(@"/")-executingAssemblyPath.IndexOf(driveLetter));
                retVal = retVal.Replace(@"/", @"\") + @"\";
            }
            return retVal;
        }
    }
}
