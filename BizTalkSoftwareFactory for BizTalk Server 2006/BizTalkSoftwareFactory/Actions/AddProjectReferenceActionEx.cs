using System;
using System.Collections.Generic;
using System.Text;
using VSLangProj;
using Microsoft.Practices.RecipeFramework.Library;
using EnvDTE;
using Microsoft.Practices.ComponentModel;
using System.ComponentModel;
using Microsoft.Practices.RecipeFramework;

namespace BizTalkSoftwareFactory.Actions
{
  /// <summary>
  /// This is a copy of the AddProjectReferenceAction from the Microsoft.Practices.RecipeFramework.Extensions assembly
  /// The problem with that action was that it failed when the project didn't exist.
  /// That whole part is removed so only action is taken if the project exists
  /// This issue showed up on a single solution project where the reference to the Map project was added to the UnitTest project
  /// </summary>
  [ServiceDependency(typeof(DTE)), DesignerCategory("Code")]
  public class AddProjectReferenceActionEx : Action
  {
    // Fields
    private bool isMultiProjectSolution;
    private string referencedAssembly;
    private Project referencedProject;
    private Project referencedProject2;
    private Project referringProject;

    // Methods
    public override void Execute()
    {
      if (isMultiProjectSolution)
      {
        if (this.ReferencedProject != null)
        {
          if (!this.ReferencedProject.UniqueName.Equals(this.ReferringProject.UniqueName, StringComparison.InvariantCultureIgnoreCase))
          {
            VSProject project = this.referringProject.Object as VSProject;
            if (project != null)
            {
              project.References.AddProject(this.ReferencedProject);
            }
          }
        }
      }
      else
      {
        if (this.ReferencedProject2 != null)
        {
          if (!this.ReferencedProject2.UniqueName.Equals(this.ReferringProject.UniqueName, StringComparison.InvariantCultureIgnoreCase))
          {
            VSProject project = this.referringProject.Object as VSProject;
            if (project != null)
            {
              project.References.AddProject(this.ReferencedProject2);
            }
          }
        }
      }
    }

    private bool IsAlreadyReferenced(VSProject vsProject, string assemblyName)
    {
      foreach (Reference reference in vsProject.References)
      {
        if (reference.Name.Equals(assemblyName, StringComparison.InvariantCultureIgnoreCase))
        {
          return true;
        }
      }
      return false;
    }

    public override void Undo()
    {
    }

    // Properties
    [Input(Required = false)]
    public string ReferencedAssembly
    {
      get
      {
        return this.referencedAssembly;
      }
      set
      {
        this.referencedAssembly = value;
      }
    }

    [Input(Required = false)]
    public Project ReferencedProject
    {
      get
      {
        return this.referencedProject;
      }
      set
      {
        this.referencedProject = value;
      }
    }

    [Input(Required = false)]
    public Project ReferencedProject2
    {
      get
      {
        return this.referencedProject2;
      }
      set
      {
        this.referencedProject2 = value;
      }
    }

    [Input(Required = true)]
    public Project ReferringProject
    {
      get
      {
        return this.referringProject;
      }
      set
      {
        this.referringProject = value;
      }
    }

    [Input(Required = true)]
    public bool IsMultiProjectSolution
    {
      get
      {
        return this.isMultiProjectSolution;
      }
      set
      {
        this.isMultiProjectSolution = value;
      }
    }
  }
}