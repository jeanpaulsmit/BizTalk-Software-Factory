#region Using Directives

using System;
using Microsoft.Practices.ComponentModel;
using Microsoft.Practices.RecipeFramework;
using System.IO;
using System.Xml;
using System.Diagnostics;
using Microsoft.Practices.RecipeFramework.Library;
using Microsoft.Win32;

#endregion

namespace BizTalkSoftwareFactory.Actions
{
  /// <summary>
  /// This action creates a strong name key file and adds that to the solution
  /// as a solution item
  /// </summary>
  class CreateStrongNamedKeyAction : Action
  {
    /// <summary>
    /// The name of the keyfile (used if keyfile not specified)
    /// </summary>
    private string name;
    /// <summary>
    /// The path of the keyfile (existing)
    /// </summary>
    private string keyFile;

    #region Input Properties

    /// <summary>
    /// Gets or sets the name of the keyfile
    /// </summary>
    [Input(Required = true)]
    public string Name
    {
      get { return name; }
      set { name = value; }
    }

    /// <summary>
    /// Gets or sets the path of the existing keyfile
    /// </summary>
    [Input]
    public string KeyFile
    {
      get { return keyFile; }
      set { keyFile = value; }
    }

    #endregion

    #region IAction Members

    /// <summary>
    /// Executes the action.
    /// </summary>
    public override void Execute()
    {
      if (this.KeyFile == null)
      {
        EnvDTE.DTE vs = this.GetService<EnvDTE.DTE>(true);
        string solutionPath = (string)vs.Solution.Properties.Item("Path").Value;
        string solutionDir = Path.GetDirectoryName(solutionPath);

        // Changed by GOP. Relative path instead of absolute.
        string keyFileName = this.Name + ".snk";
        string keyFilePath = Path.Combine(solutionDir, keyFileName);

        // Find the .NET SDK directory to execute the sn.exe command.
        //string visualStudioDir = Path.GetDirectoryName(vs.Application.FileName);
        //string sdkDir = Path.Combine(visualStudioDir, @"..\..\SDK\v2.0\Bin");
        //string snPath = Path.Combine(sdkDir, "sn.exe");
        //if (!File.Exists(snPath))
        //{
        //  throw new InvalidOperationException("Could not find the Visual Studio SDK directory to execute the sn.exe command.");
        //}

        // Find the .NET SDK directory to execute the sn.exe command.
        RegistryKey regKeySN = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SDKs\Windows\v6.0A\WinSDKNetFxTools");
        if (regKeySN == null)
        {
            // Second try
            regKeySN = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SDKs\.NETFramework\v2.0");
        }
        string sdkDir = regKeySN.GetValue(@"InstallationFolder").ToString() + "bin";
        if (!Directory.Exists(sdkDir))
        {
            throw new InvalidOperationException("Could not find the Visual Studio SDK directory to execute the sn.exe command. Searching folder " + sdkDir);
        }
        string snPath = Path.Combine(sdkDir, "sn.exe");
        if (!File.Exists(snPath))
        {
            throw new InvalidOperationException("Could not find the sn.exe command in the Visual Studio SDK directory. Searching for " + snPath);
        }


        // Make sure the directory exists.
        Directory.CreateDirectory(Path.GetDirectoryName(keyFilePath));

        // Launch the process and wait until it's done (with a 10 second timeout).
        ProcessStartInfo startInfo = new ProcessStartInfo(snPath, string.Format("-k \"{0}\"", keyFilePath));
        startInfo.CreateNoWindow = true;
        startInfo.UseShellExecute = false;
        Process snProcess = Process.Start(startInfo);
        snProcess.WaitForExit(10000);

        // Add the key file to the Solution Items.
        DteHelper.SelectSolution(vs);
        vs.ItemOperations.AddExistingItem(keyFilePath);

        // The AddExistingItem operation also shows the item in a new window, close that.
        vs.ActiveWindow.Close(EnvDTE.vsSaveChanges.vsSaveChangesNo);

        //set the assemblykeyfile property for every BizTalk Project
        foreach (EnvDTE.Project project in vs.Solution.Projects)
        {
          if (project.Properties != null)
          {
            if (project.Kind == BusinessComponents.Constants.BizTalkProjectType)
            {
              // Changed by GOP. Relative path instead of absolute.
              ((System.Collections.Hashtable)(project.Properties.Item(5).Value))["AssemblyKeyFile"] = @"..\\..\\..\\" + keyFileName;
            }
            else if(project.Kind == BusinessComponents.Constants.ClassLibraryProjectType)
            {
              // Changed by GOP. Relative path instead of absolute.
              project.Properties.Item("SignAssembly").Value = true;
              project.Properties.Item("AssemblyOriginatorKeyFile").Value = @"..\\" + keyFileName;
            }
          }
        }
      }
    }

    /// <summary>
    /// Performs an undo of the action.
    /// </summary>
    public override void Undo()
    {

    }

    #endregion
  }
}
