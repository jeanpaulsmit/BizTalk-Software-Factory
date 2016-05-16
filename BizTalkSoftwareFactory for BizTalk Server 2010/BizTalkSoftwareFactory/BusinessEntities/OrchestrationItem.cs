using System;
using System.Collections.Generic;
using System.Text;

namespace BizTalkSoftwareFactory.BusinessEntities
{
    /// <summary>
    /// This is a Business Entity representing an orchestration, used by the Orchestration Project class in the Business Components
    /// </summary>
    class OrchestrationItem
    {
        public OrchestrationItem()
        {
        }

        // Constuctor to set the name
        public OrchestrationItem(string name)
        {
            this.Name = name;
        }

        private string name;

        public string Name
        {
          get { return name; }
          set { name = value; }
        }

        /// <summary>
        /// Override ToString method because that will be called by the wizard
        /// and we don't want to display the wrong name
        /// </summary>
        /// <returns>The OrchestrationName</returns>
        public override string ToString()
        {
          return string.Format("{0}", Name);
        }
    }
}
