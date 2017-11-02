using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizTalkSoftwareFactory.OperationalManagement
{
    public static class Helpers
    {
        public static bool Exists(this ProjectItems projectItems, string fileName)
        {
            // initial value
            bool fileExists = false;

            // iterate project items
            foreach (ProjectItem projectItem in projectItems)
            {
                // if the name matches
                if (projectItem.Name == fileName)
                {
                    // abort this add, file already exists
                    fileExists = true;

                    // break out of loop
                    break;
                }
                else if ((projectItem.ProjectItems != null) && (projectItem.ProjectItems.Count > 0))
                {
                    // check if the file exists in the project
                    fileExists = Exists(projectItem.ProjectItems, fileName);

                    // if the file does exist
                    if (fileExists)
                    {
                        // abort this add, file already exists
                        fileExists = true;
                        // break out of loop
                        break;
                    }
                }
            }

            // return value
            return fileExists;

        }
    }
}
