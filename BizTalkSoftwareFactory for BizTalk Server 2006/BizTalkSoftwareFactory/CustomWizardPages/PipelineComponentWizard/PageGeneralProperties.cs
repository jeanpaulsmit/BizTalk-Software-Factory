using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Microsoft.Practices.WizardFramework;
using System.ComponentModel.Design;

/*
 * This is taken from the Pipeline Component Wizard on CodePlex by Martijn Hoogendoorn
 * http://www.codeplex.com/btsplcw
 * 
*/

namespace BizTalkSoftwareFactory.CustomWizardPages.PipelineComponentWizard
{
  public class PageGeneralProperties : CustomWizardPage
	{
    private const string ComponentVersionRegEx = @"[0-9]+\.[0-9]+$";
    private const string ComponentNameRegEx = @"(?i)^[a-z]+[0-9a-z]*$";
    private Label lblComponentName;
    private TextBox txtComponentName;
    private Label lblComponentDescription;
    private Label lblComponentVersion;
    private TextBox txtComponentVersion;
    private TextBox txtComponentDescription;
    private ErrorProvider ErrProv;
		private IContainer components = null;

    public PageGeneralProperties(WizardForm parent)
      : base(parent)
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
    }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

    public bool NextButtonEnabled
    {
      get { return GetAllStates(); }
    }

    public bool NeedSummary
    {
      get { return false; }
    }

    private bool GetAllStates()
    {
      return (Regex.IsMatch(txtComponentVersion.Text, ComponentVersionRegEx) &&
        Regex.IsMatch(txtComponentName.Text, ComponentNameRegEx));
    }

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageGeneralProperties));
      this.lblComponentName = new System.Windows.Forms.Label();
      this.txtComponentName = new System.Windows.Forms.TextBox();
      this.lblComponentDescription = new System.Windows.Forms.Label();
      this.lblComponentVersion = new System.Windows.Forms.Label();
      this.txtComponentVersion = new System.Windows.Forms.TextBox();
      this.txtComponentDescription = new System.Windows.Forms.TextBox();
      this.ErrProv = new System.Windows.Forms.ErrorProvider(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.ErrProv)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      this.SuspendLayout();
      // 
      // infoPanel
      // 
      resources.ApplyResources(this.infoPanel, "infoPanel");
      // 
      // lblComponentName
      // 
      resources.ApplyResources(this.lblComponentName, "lblComponentName");
      this.lblComponentName.Name = "lblComponentName";
      // 
      // txtComponentName
      // 
      resources.ApplyResources(this.txtComponentName, "txtComponentName");
      this.txtComponentName.Name = "txtComponentName";
      this.txtComponentName.TextChanged += new System.EventHandler(this.txtComponentName_TextChanged);
      this.txtComponentName.Validating += new System.ComponentModel.CancelEventHandler(this.txtComponentName_Validating);
      // 
      // lblComponentDescription
      // 
      resources.ApplyResources(this.lblComponentDescription, "lblComponentDescription");
      this.lblComponentDescription.Name = "lblComponentDescription";
      // 
      // lblComponentVersion
      // 
      resources.ApplyResources(this.lblComponentVersion, "lblComponentVersion");
      this.lblComponentVersion.Name = "lblComponentVersion";
      // 
      // txtComponentVersion
      // 
      resources.ApplyResources(this.txtComponentVersion, "txtComponentVersion");
      this.txtComponentVersion.Name = "txtComponentVersion";
      this.txtComponentVersion.TextChanged += new System.EventHandler(this.txtComponentVersion_TextChanged);
      this.txtComponentVersion.Validating += new System.ComponentModel.CancelEventHandler(this.txtComponentVersion_Validating);
      // 
      // txtComponentDescription
      // 
      resources.ApplyResources(this.txtComponentDescription, "txtComponentDescription");
      this.txtComponentDescription.Name = "txtComponentDescription";
      this.txtComponentDescription.TextChanged += new System.EventHandler(this.txtComponentDescription_TextChanged);
      // 
      // ErrProv
      // 
      this.ErrProv.ContainerControl = this;
      // 
      // PageGeneralProperties
      // 
      resources.ApplyResources(this, "$this");
      this.Controls.Add(this.lblComponentName);
      this.Controls.Add(this.txtComponentName);
      this.Controls.Add(this.lblComponentVersion);
      this.Controls.Add(this.txtComponentVersion);
      this.Controls.Add(this.lblComponentDescription);
      this.Controls.Add(this.txtComponentDescription);
      this.Name = "PageGeneralProperties";
      this.Controls.SetChildIndex(this.infoPanel, 0);
      this.Controls.SetChildIndex(this.lblComponentName, 0);
      this.Controls.SetChildIndex(this.txtComponentName, 0);
      this.Controls.SetChildIndex(this.lblComponentVersion, 0);
      this.Controls.SetChildIndex(this.txtComponentVersion, 0);
      this.Controls.SetChildIndex(this.lblComponentDescription, 0);
      this.Controls.SetChildIndex(this.txtComponentDescription, 0);
      ((System.ComponentModel.ISupportInitialize)(this.ErrProv)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

		}
		#endregion

    private void txtComponentVersion_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if (!Regex.IsMatch(txtComponentVersion.Text, ComponentVersionRegEx) && txtComponentVersion.Text.Length > 0)
      {
        ErrProv.SetError(txtComponentVersion, "ComponentVersion can only contain numbers and a single '.' character, e.g.: 1.0, 1.15 or 10.234");
      }
      else
      {
        ErrProv.SetError(txtComponentVersion, "");
      }
    }

    private void txtComponentName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if (!Regex.IsMatch(txtComponentName.Text, ComponentNameRegEx) && txtComponentName.Text.Length > 0)
      {
        ErrProv.SetError(txtComponentName, "txtComponentName can only contain alpha numeric characters and cannot start with a number");
      }
      else
      {
        ErrProv.SetError(txtComponentName, "");
      }
    }

    private void txtComponentName_TextChanged(object sender, EventArgs e)
    {
      IDictionaryService dictionaryService = GetService(typeof(IDictionaryService)) as IDictionaryService;
      dictionaryService.SetValue("ComponentName", txtComponentName.Text);
    }

    private void txtComponentVersion_TextChanged(object sender, EventArgs e)
    {
      IDictionaryService dictionaryService = GetService(typeof(IDictionaryService)) as IDictionaryService;
      dictionaryService.SetValue("ComponentVersion", txtComponentVersion.Text);
    }

    private void txtComponentDescription_TextChanged(object sender, EventArgs e)
    {
      IDictionaryService dictionaryService = GetService(typeof(IDictionaryService)) as IDictionaryService;
      dictionaryService.SetValue("ComponentDescription", txtComponentDescription.Text);
    }
  }
}
