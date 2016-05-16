using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using System.Collections;
using EnvDTE;
using BizTalkSoftwareFactory.BusinessEntities;
using BizTalkSoftwareFactory.BusinessComponents;

namespace BizTalkSoftwareFactory.Converters
{
    /// <summary>
    /// This TypeConverter reads a list of Orchestrations using the Orchestration Project business component 
    /// and returns it so it can be displayed in the Orchestration selector on the new Orchestration Unit test wizard 
    /// </summary>
    class OrchestrationConverter : TypeConverter
    {
        public OrchestrationConverter()
        {
        }

        /// <summary>
        // Indicates this converter provides a list of standard values.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>True</returns>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        // Returns a StandardValuesCollection of standard value objects.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>List of Orchestration objects</returns>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            // Get the list of Orchestration objects
            List<OrchestrationItem> OrchestrationItem = OrchestrationProject.GetOrchestrationList(context);

            // Passes the local array.
            return new StandardValuesCollection(OrchestrationItem);
        }
    }
}