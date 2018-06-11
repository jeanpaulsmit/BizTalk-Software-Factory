using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Xml;
using BizTalkSoftwareFactory.BusinessEntities;
using BizTalkSoftwareFactory.OperationalManagement;
using EnvDTE;
using VSLangProj;
using Microsoft.XLANGs.BaseTypes;
using Microsoft.BizTalk.TestTools.Schema;

namespace BizTalkSoftwareFactory.BusinessComponents
{
    /// <summary>
    /// This business component reads schemas from projects and assemblies
    /// It determines if it is a valid schema and is also capable of determining the root nodes
    /// </summary>
    class SchemaProject
    {
        // Constant to define the class name used in the logging
        const string CLASS_NAME = "SchemaProject";

        /// <summary>
        /// Get the list of available Schemas from both projects and references.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>List of available Schemas</returns>
        public static List<SchemaItem> GetSchemaList(ITypeDescriptorContext context)
        {
            const string METHOD_NAME = "GetSchemaList";
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "Begin");

            // Input validation
            if (context == null)
            {
                return null;
            }

            List<SchemaItem> schemaItem = new List<SchemaItem>();

            // Get the Schemas from a possible Schemas folder in this project
            // We need to iterate through the solution to get at the correct location for the schemas
            GetSchemasFromProjectFolder((DTE)context.GetService(typeof(DTE)), schemaItem);

            // Get the Schemas from a possible Schemas project in the solution
            GetSchemasFromProject(Helper.GetProject((DTE)context.GetService(typeof(DTE)), BizTalkProjectType.Schemas), schemaItem);

            // Get the Schemas from referenced assemblies in the Maps Project or folder
            GetSchemasFromReferences(Helper.GetProject((DTE)context.GetService(typeof(DTE)), BizTalkProjectType.Maps), schemaItem);

            // Return the list of Schemas
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "End");
            return schemaItem;
        }

        /// <summary>
        /// This method returns the Schemas found in the project folder named Schemas
        /// First the correct project has to be found and then the correct folder
        /// </summary>
        /// <param name="btsProjectFolder">Project folder</param>
        /// <param name="schemaList">Reference to a list of schemas</param>
        private static void GetSchemasFromProjectFolder(DTE dteObject, List<SchemaItem> schemaList)
        {
            const string METHOD_NAME = "GetSchemasFromProjectFolder";
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "Begin");

            // Input validatation
            if (dteObject == null || schemaList == null || dteObject.Solution == null)
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
                    //if (dteObject.Solution.Projects.Item(i).Kind == BusinessComponents.Constants.BizTalkProjectType)
                    //{
                        // Try to find a 'Schemas' folder
                        // Directly calling '....ProjectItems.Item("Schemas")' failed for the multi project solution.
                        // Instead of catching an Argument exception, I decided to add another foreach
                        int projItemCounter = dteObject.Solution.Projects.Item(i).ProjectItems.Count;
                        for(int j=1; j<=projItemCounter; j++)
                        {
                            if (dteObject.Solution.Projects.Item(i).ProjectItems.Item(j).Name == "Schemas")
                            {
                              // Go over every item and store the Schemas
                              foreach (ProjectItem item in dteObject.Solution.Projects.Item(i).ProjectItems.Item("Schemas").ProjectItems)
                              {
                                  string projectNamespace = item.Properties.Item("RootNamespace").Value.ToString();

                                // Scan only the Schemas, which end with 'xsd'
                                if (item.Name.EndsWith(".xsd"))
                                {
                                  // Get the path, necessary for Unit testing
                                  string schemaItemPath = item.Properties.Item("FullPath").Value as string;

                                  // Get the namespace, also necessery for Unit testing
                                  XmlDocument schemaDoc = new XmlDocument();
                                  schemaDoc.Load(schemaItemPath);
                                  string schemaTargetNamespace = schemaDoc.DocumentElement.Attributes["targetNamespace"].Value;

                                  // Create a new SchemaItem object, the name is without .xsd
                                  SchemaItem newSchema = new SchemaItem(schemaTargetNamespace, item.Name.Substring(0, item.Name.Length - 4), item.Name, schemaItemPath, projectNamespace);

                                  // Add the found Schema to the list of Schemas
                                  AddSchemaToList(schemaList, newSchema);
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
        /// This method returns the Schemas found in the supplied project type (mostly Schema projects)
        /// </summary>
        /// <param name="btsProject">Project object referring to a project</param>
        /// <param name="schemaList">Reference to a list of schemas</param>
        private static void GetSchemasFromProject(Project btsProject, List<SchemaItem> schemaList)
        {
            const string METHOD_NAME = "GetSchemasFromProject";
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "Begin");

            // Input validatation
            if (btsProject == null || schemaList == null)
            {
                return;
            }

            // Go over every item and store the Schemas
            string projectNamespace = btsProject.Properties.Item("RootNamespace").Value.ToString();
            foreach (ProjectItem item in btsProject.ProjectItems)
            {
                // Scan only the Schemas, which end with 'xsd'
                if (item.Name.EndsWith(".xsd"))
                {
                  // Get the path, necessary for Unit testing
                  string schemaItemPath = item.Properties.Item("FullPath").Value as string;

                  // Get the namespace, also necessery for Unit testing
                  XmlDocument schemaDoc = new XmlDocument();
                  schemaDoc.Load(schemaItemPath);
                  string schemaTargetNamespace = schemaDoc.DocumentElement.Attributes["targetNamespace"].Value;

                  // Create a new SchemaItem object, the name is without .xsd
                  SchemaItem newSchema = new SchemaItem(schemaTargetNamespace, item.Name.Substring(0, item.Name.Length - 4), item.Name, schemaItemPath, projectNamespace);

                  // Add the found Schema to the list of Schemas
                  AddSchemaToList(schemaList, newSchema);
                }
            }
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "End");
        }

        /// <summary>
        /// This method returns the Schemas found in the references of the supplied Schema project type
        /// </summary>
        /// <param name="project">Project object referring to a project</param>
        /// <param name="schemaList">Reference to a list of schemas</param>
        private static void GetSchemasFromReferences(Project project, List<SchemaItem> schemaList)
        {
            const string METHOD_NAME = "GetSchemasFromReferences";
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "Begin");

            // Input validation
            if (project == null || schemaList == null)
            {
                return;
            }

            // The following is better in a try/catch because of the number of type casts
            try
            {
                // First convert the project in the argument to an IBtsProject typed object, which is a real BizTalk project type
                VSProject btsProject = (VSProject)project.Object;
                if (btsProject == null)
                {
                    return;
                }

                // Go over the list of references in the supplied project
                for (int i = 1; i <= ((int)(btsProject.References).Count); i++)
                {
                    // Get the path of the assembly and determine if it is a BizTalk assembly that contains Schemas
                    string assemblyPath = btsProject.References.Item(i).Path;
                    
                    // We know the path of the referenced assembly, dive into it and get the Schemas from it
                    GetSchemasFromAssembly(assemblyPath, schemaList);
                }
            }
            catch (Exception ex)
            {
                Logging.LogMessage(CLASS_NAME, METHOD_NAME, string.Format("Exception - {0}", ex.Message));
            }
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "End");
        }

        /// <summary>
        /// This method adds a Schema item to the schema list and checks for duplicates
        /// </summary>
        /// <param name="schemaList">List containing all schemas</param>
        /// <param name="newSchema">Schema to add</param>
        private static void AddSchemaToList(List<SchemaItem> schemaList, SchemaItem newSchema)
        {
            const string METHOD_NAME = "AddSchemaToList";
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "Begin");

            // Input validatation
            if (newSchema == null || schemaList == null)
            {
                return;
            }
            if (!IsSchemaAlreadyInList(schemaList, newSchema))
            {
                schemaList.Add(newSchema);
            }
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "End");
        }

        /// <summary>
        /// Check if a supplied Schema is already in the SchemaList. This is possible if a Schema assembly is referenced more than once.
        /// </summary>
        /// <param name="schemaList"></param>
        /// <param name="newSchema"></param>
        /// <returns></returns>
        private static bool IsSchemaAlreadyInList(List<SchemaItem> schemaList, SchemaItem newSchema)
        {
            const string METHOD_NAME = "IsSchemaAlreadyInList";
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "Begin");

            bool retVal = false;
            foreach (SchemaItem schemaToCheck in schemaList)
            {
                if (schemaToCheck.Name == newSchema.Name)// && (schemaToCheck.RootNode == newSchema.RootNode))
                {
                    retVal = true;
                    break;
                }
            }
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "End");
            return retVal;
        }

        /// <summary>
        /// This method scans an assembly for BizTalk Schemas and adds them to the supplied Schema List
        /// </summary>
        /// <param name="assemblyPath"></param>
        /// <param name="schemaList"></param>
        public static void GetSchemasFromAssembly(string assemblyPath, List<SchemaItem> schemaList)
        {
            const string METHOD_NAME = "GetSchemasFromAssembly";
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "Begin");

            if(string.IsNullOrEmpty(assemblyPath))
            {
                return;
            }

            try
            {
                // Load the assembly from the path specified
                Assembly schemaAssembly = Assembly.LoadFrom(assemblyPath);

                // See if it is a BizTalk Assembly
                if (IsBizTalkAssembly(schemaAssembly))
                {
                    // It is a BizTalk Assembly, is it a Schema assembly?
                    foreach (Type type in schemaAssembly.GetTypes())
                    {
                        // Check if the current assembly is of a Schema type and not a property schema
                        if (IsBizTalkSchemaType(type) && !IsBizTalkPropertySchema(type))
                        {
                            // Valid Schema found. Find out if it contains a multi rooted schema
                            object[] assemblyAttributes = type.GetCustomAttributes(typeof(SchemaRootsAttribute), false);
                            if (assemblyAttributes != null && assemblyAttributes.Length > 0)
                            {
                                // Schema found, add the rootnode of the schema
                                SchemaBase sba = Activator.CreateInstance(type) as SchemaBase;

                                // Iterate over the root nodes and add them to the list
                                foreach (string rootNode in sba.RootNodes)
                                {
                                    Logging.LogMessage(CLASS_NAME, METHOD_NAME, string.Format("Schema '{0}' found with rootnode '{1}', add it to the list", type.FullName, rootNode));

                                    // Create a new SchemaItem object, the name is including the rootnode
                                    // The path can be empty, it is not relevant for schema assemblies
                                    SchemaItem newSchema = new SchemaItem(type.Namespace, type.Name, rootNode, null, type.Namespace);

                                    // Add the found Schema to the list of Schemas
                                    AddSchemaToList(schemaList, newSchema);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.LogMessage(CLASS_NAME, METHOD_NAME, ex.Message);
            }

            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "End");
        }

        /// <summary>
        /// This method checks if the assembly specified is a BizTalk assembly. This is checked based on attributes
        /// </summary>
        /// <param name="schemasAssembly">Assembly object loaded with an assembly</param>
        /// <returns>True if a BizTalk assembly, otherwise false</returns>
        private static bool IsBizTalkAssembly(Assembly schemasAssembly)
        {
            const string METHOD_NAME = "IsBizTalkAssembly";
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "Begin");

            bool retVal;

            // Get the custom attributes applied to this assembly
            object[] assemblyAttributes = schemasAssembly.GetCustomAttributes(typeof(BizTalkAssemblyAttribute), true);
            if (assemblyAttributes == null || assemblyAttributes.Length == 0)
            {
                retVal = false;
            }
            else
            {
                retVal = true;
            }
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "End");

            return retVal;
        }

        /// <summary>
        /// Checks if the supplied type is a BizTalk Schema type
        /// </summary>
        /// <param name="assemblyType">Type of object</param>
        /// <returns>True if it is a BizTalk Schema, otherwise false</returns>
        private static bool IsBizTalkSchemaType(Type assemblyType)
        {
            const string METHOD_NAME = "IsBizTalkSchemaType";
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "Begin");

            bool retVal;

            // A BizTalk object is of type Schema if it is derived from SchemaBase
            if (((assemblyType.BaseType == typeof(SchemaBase)) || (assemblyType.BaseType == typeof(TestableSchemaBase))) && !assemblyType.IsNestedPublic)
            {
                retVal = true;
            }
            else
            {
                retVal = false;
            }

            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "Begin");

            return retVal;
        }

        /// <summary>
        /// This method determines if this is a PropertySchema
        /// </summary>
        /// <param name="assemblyType">Type of object</param>
        /// <returns>True if it is a PropertySchema, otherwise false</returns>
        private static bool IsBizTalkPropertySchema(Type assemblyType)
        {
            const string METHOD_NAME = "IsBizTalkPropertySchema";
            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "Begin");

            bool retVal = true;

            // Make sure it also isn't a property schema
            SchemaTypeAttribute schemaTypeAttr = null;
            object[] assemblyAttributes = assemblyType.GetCustomAttributes(typeof(SchemaTypeAttribute), false);
            if (assemblyAttributes != null && assemblyAttributes.Length > 0)
            {
                schemaTypeAttr = (SchemaTypeAttribute)assemblyAttributes[0];
                if (schemaTypeAttr != null && schemaTypeAttr.Type != SchemaTypeEnum.Property)
                {
                    retVal = false;
                }
            }

            Logging.LogMessage(CLASS_NAME, METHOD_NAME, "End");

            return retVal;
        }
    }
}
