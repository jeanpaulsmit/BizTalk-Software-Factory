using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using EnvDTE;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Microsoft.BizTalk.Studio.Extensibility;
using System.Diagnostics;
using BizTalkSoftwareFactory.OperationalManagement;
using BizTalkSoftwareFactory.BusinessEntities;
//using Microsoft.XLANGs.BaseTypes;

namespace BizTalkSoftwareFactory.BusinessComponents
{
    /// <summary>
    /// This business component reads maps from projects and assemblies
    /// It determines if it is a valid map
    /// </summary>
    class MapProject
    {
        // Constant to define the class name used in the logging
        const string CLASS_NAME = "MapProject";

        /// <summary>
        /// Get the list of available Maps from the projects folder.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>List of available Maps</returns>
        public static List<MapItem> GetMapsList(ITypeDescriptorContext context)
        {
            const string METHOD_NAME = "GetMapsList";
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "Begin");

            // Input validation
            if (context == null)
            {
                return null;
            }

            List<MapItem> mapItemList = new List<MapItem>();

            // Get the Maps from a possible Maps folder in this project
            // We need to iterate through the solution to get at the correct location for the maps
            GetMapsFromProjectFolder((DTE)context.GetService(typeof(DTE)), mapItemList);

            // Get the Maps from a possible Maps project in the solution
            GetMapsFromProject(Helper.GetProject((DTE)context.GetService(typeof(DTE)), BizTalkProjectType.Maps), mapItemList);

            // Return the list of Maps
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "End");
            return mapItemList;
        }

        /// <summary>
        /// This method returns the Maps found in the project folder named Maps
        /// First the correct project has to be found and then the correct folder
        /// </summary>
        /// <param name="btsProjectFolder">Project folder</param>
        /// <param name="mapList">Reference to a list of maps</param>
        private static void GetMapsFromProjectFolder(DTE dteObject, List<MapItem> mapsList)
        {
            const string METHOD_NAME = "GetMapsFromProjectFolder";
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "Begin");

            // Input validatation
            if (dteObject == null || mapsList == null || dteObject.Solution == null)
            {
                return;
            }

            // Go over every project in the DTE object and find the BizTalk typed projects
            // With DTE the counter starts at 1 instead of 0
            int projCounter = dteObject.Solution.Projects.Count;
            for(int i=1; i<=projCounter; i++)
            {
                if (dteObject.Solution.Projects.Item(i) != null)
                {
                    // Found a BizTalk project?
                    // if (dteObject.Solution.Projects.Item(i).Kind == BusinessComponents.Constants.BizTalkProjectType)
                    //{
                        // Try to find a 'Maps' folder
                        // Directly calling '....ProjectItems.Item("Maps")' failed for the multi project solution.
                        // Instead of catching an Argument exception, I decided to add another foreach
                        int projItemCounter = dteObject.Solution.Projects.Item(i).ProjectItems.Count;
                        for(int j=1; j<=projItemCounter; j++)
                        {
                            if (dteObject.Solution.Projects.Item(i).ProjectItems.Item(j).Name == "Maps")
                            {
                                // Go over every item and store the Schemas
                                foreach (ProjectItem item in dteObject.Solution.Projects.Item(i).ProjectItems.Item("Maps").ProjectItems)
                                {
                                    // Scan only the Maps, they end with 'btm'
                                    if (item.Name.EndsWith(".btm"))
                                    {
                                        // Create a new SchemaItem object, the name is without .xsd
                                        MapItem newMap = new MapItem(item.Name.Substring(0, item.Name.Length - 4));

                                        // Add the found Map to the list of Maps
                                        AddMapToList(mapsList, newMap);
                                    }
                                }
                            }
                        }
                    //}
                }
            }
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "End");
        }

        /// <summary>
        /// This method returns the Maps found in the supplied project type (mostly Map projects)
        /// </summary>
        /// <param name="btsProject">Project object referring to a project</param>
        /// <param name="mapList">Reference to a list of maps</param>
        private static void GetMapsFromProject(Project btsProject, List<MapItem> mapList)
        {
            const string METHOD_NAME = "GetMapsFromProject";
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "Begin");

            // Input validatation
            if (btsProject == null || mapList == null)
            {
                return;
            }

            // Go over every item and store the Maps
            foreach (ProjectItem item in btsProject.ProjectItems)
            {
                // Scan only the Map, which end with 'btm'
                if (item.Name.EndsWith(".btm"))
                {
                    // Create a new MapItem object, the name is without .btm
                    MapItem newMap = new MapItem(item.Name.Substring(0, item.Name.Length - 4));

                    // Add the found Map to the list of Maps
                    AddMapToList(mapList, newMap);
                }
            }
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "End");
        }

        /// <summary>
        /// This method adds a Map item to the map list and checks for duplicates
        /// </summary>
        /// <param name="mapList">List containing all maps</param>
        /// <param name="newMap">Map to add</param>
        private static void AddMapToList(List<MapItem> mapList, MapItem newMap)
        {
            const string METHOD_NAME = "AddMapToList";
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "Begin");

            // Input validatation
            if (newMap == null || mapList == null)
            {
                return;
            }
            if (!IsMapAlreadyInList(mapList, newMap))
            {
                mapList.Add(newMap);
            }
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "End");
        }

        /// <summary>
        /// Check if a supplied Map is already in the MapList. This is possible if a Map assembly is referenced more than one.
        /// </summary>
        /// <param name="mapList"></param>
        /// <param name="newMap"></param>
        /// <returns></returns>
        private static bool IsMapAlreadyInList(List<MapItem> mapList, MapItem newMap)
        {
            const string METHOD_NAME = "IsMapAlreadyInList";
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "Begin");

            bool retVal = false;
            foreach (MapItem mapToCheck in mapList)
            {
                if (mapToCheck.Name == newMap.Name)
                {
                    retVal = true;
                    break;
                }
            }
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "End");
            return retVal;
        }
    }
}
