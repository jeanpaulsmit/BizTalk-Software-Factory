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
  public class PageGeneralSetup : CustomWizardPage
	{
		private const string TransportRegEx = @"^[_a-zA-Z][_a-zA-Z0-9]*$";
    private Label Classname;
		private TextBox txtClassName;
		private Label lblComponentType;
		private ComboBox cboPipelineType;
		private Label lblPipelineType;
    private CheckBox chkImplementIProbeMessage;
    private ComboBox cboComponentStage;
    private ErrorProvider ErrProv;
		private IContainer components = null;

    public PageGeneralSetup(WizardForm parent)
      : base(parent)
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// re-clear all items from the stage dropdown
			cboComponentStage.Items.Clear();
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

		private void txtClassName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (!Regex.IsMatch(txtClassName.Text,TransportRegEx) && txtClassName.Text.Length > 0)
			{
				ErrProv.SetError(txtClassName, "TransportType must start with a non-alphanumeric character and may only include special character '_'");
			}
			else
			{
				ErrProv.SetError(txtClassName,"");
			}		
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageGeneralSetup));
      this.Classname = new System.Windows.Forms.Label();
      this.txtClassName = new System.Windows.Forms.TextBox();
      this.lblComponentType = new System.Windows.Forms.Label();
      this.cboPipelineType = new System.Windows.Forms.ComboBox();
      this.lblPipelineType = new System.Windows.Forms.Label();
      this.chkImplementIProbeMessage = new System.Windows.Forms.CheckBox();
      this.cboComponentStage = new System.Windows.Forms.ComboBox();
      this.ErrProv = new System.Windows.Forms.ErrorProvider(this.components);
      this.infoPanel.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ErrProv)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      this.SuspendLayout();
      // 
      // infoPanel
      // 
      this.infoPanel.Controls.Add(this.chkImplementIProbeMessage);
      resources.ApplyResources(this.infoPanel, "infoPanel");
      this.infoPanel.Controls.SetChildIndex(this.chkImplementIProbeMessage, 0);
      // 
      // Classname
      // 
      resources.ApplyResources(this.Classname, "Classname");
      this.Classname.Name = "Classname";
      // 
      // txtClassName
      // 
      resources.ApplyResources(this.txtClassName, "txtClassName");
      this.txtClassName.Name = "txtClassName";
      this.txtClassName.TextChanged += new System.EventHandler(this.txtClassName_TextChanged);
      this.txtClassName.Validating += new System.ComponentModel.CancelEventHandler(this.txtClassName_Validating);
      // 
      // lblComponentType
      // 
      resources.ApplyResources(this.lblComponentType, "lblComponentType");
      this.lblComponentType.Name = "lblComponentType";
      // 
      // cboPipelineType
      // 
      this.cboPipelineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      resources.ApplyResources(this.cboPipelineType, "cboPipelineType");
      this.cboPipelineType.Items.AddRange(new object[] {
            resources.GetString("cboPipelineType.Items"),
            resources.GetString("cboPipelineType.Items1"),
            resources.GetString("cboPipelineType.Items2")});
      this.cboPipelineType.Name = "cboPipelineType";
      this.cboPipelineType.SelectedIndexChanged += new System.EventHandler(this.PipelineType_Changed);
      // 
      // lblPipelineType
      // 
      resources.ApplyResources(this.lblPipelineType, "lblPipelineType");
      this.lblPipelineType.Name = "lblPipelineType";
      // 
      // chkImplementIProbeMessage
      // 
      resources.ApplyResources(this.chkImplementIProbeMessage, "chkImplementIProbeMessage");
      this.chkImplementIProbeMessage.Name = "chkImplementIProbeMessage";
      this.chkImplementIProbeMessage.CheckedChanged += new System.EventHandler(this.chkImplementIProbeMessage_CheckedChanged);
      // 
      // cboComponentStage
      // 
      this.cboComponentStage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      resources.ApplyResources(this.cboComponentStage, "cboComponentStage");
      this.cboComponentStage.Items.AddRange(new object[] {
            resources.GetString("cboComponentStage.Items"),
            resources.GetString("cboComponentStage.Items1"),
            resources.GetString("cboComponentStage.Items2")});
      this.cboComponentStage.Name = "cboComponentStage";
      this.cboComponentStage.SelectedIndexChanged += new System.EventHandler(this.cboComponentStage_Changed);
      // 
      // ErrProv
      // 
      this.ErrProv.ContainerControl = this;
      // 
      // PageGeneralSetup
      // 
      resources.ApplyResources(this, "$this");
      this.Controls.Add(this.cboComponentStage);
      this.Controls.Add(this.cboPipelineType);
      this.Controls.Add(this.Classname);
      this.Controls.Add(this.txtClassName);
      this.Controls.Add(this.lblPipelineType);
      this.Controls.Add(this.lblComponentType);
      this.Controls.Add(this.chkImplementIProbeMessage); 
      this.Name = "PageGeneralSetup";
      this.Controls.SetChildIndex(this.infoPanel, 0);
      this.Controls.SetChildIndex(this.lblComponentType, 0);
      this.Controls.SetChildIndex(this.lblPipelineType, 0);
      this.Controls.SetChildIndex(this.txtClassName, 0);
      this.Controls.SetChildIndex(this.Classname, 0);
      this.Controls.SetChildIndex(this.cboPipelineType, 0);
      this.Controls.SetChildIndex(this.cboComponentStage, 0);
      this.Controls.SetChildIndex(this.chkImplementIProbeMessage, 0);
      this.infoPanel.ResumeLayout(false);
      this.infoPanel.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ErrProv)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

		}
		#endregion

		private void PipelineType_Changed(object sender, System.EventArgs e)
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PageGeneralSetup));

			this.cboComponentStage.Items.Clear();
			this.cboComponentStage.Enabled = true;

      Debug.WriteLine(string.Format("PipelineType_Changed to : {0}", cboPipelineType.SelectedIndex));

			switch(cboPipelineType.SelectedIndex)
			{
				// do we have a receive pipeline component selected?
				case 0:
					this.cboComponentStage.Items.AddRange(new object[] 
					                                                {
						                                                componentTypes.Decoder.ToString(),
						                                                componentTypes.DisassemblingParser.ToString(),
						                                                componentTypes.Validate.ToString(),
						                                                componentTypes.PartyResolver.ToString(),
						                                                componentTypes.Any.ToString()
					                                                }
                                                );
					this.cboComponentStage.SelectedIndex = 0;
					break;
				case 1:
					this.cboComponentStage.Items.AddRange(new object[] 
					                                                {
						                                                componentTypes.Encoder.ToString(),
						                                                componentTypes.AssemblingSerializer.ToString(),
						                                                componentTypes.Any.ToString()
					                                                }
                                                );
					this.cboComponentStage.SelectedIndex = 0;
					break;
				case 2:
					this.cboComponentStage.Items.Add(componentTypes.Any.ToString());
					this.cboComponentStage.Enabled = false;
					this.cboComponentStage.SelectedIndex = 0;
					break;
				default:
					throw new ApplicationException("Unsupported pipeline type selected");
			}
		}

		private void cboComponentStage_Changed(object sender, System.EventArgs e)
		{
      IDictionaryService dictionaryService = GetService(typeof(IDictionaryService)) as IDictionaryService;
      dictionaryService.SetValue("ComponentType", cboComponentStage.Items[cboComponentStage.SelectedIndex].ToString());

			// do we have a disassembler selected?
			// only disassemblers can implement IProbeMessage
			if(cboComponentStage.Items[cboComponentStage.SelectedIndex].ToString() == componentTypes.DisassemblingParser.ToString())
			{
				chkImplementIProbeMessage.Visible = true;
			}
			else 
			{
				chkImplementIProbeMessage.Visible = false;
				chkImplementIProbeMessage.Checked = false;
        dictionaryService.SetValue("ImplementIProbeMessage", chkImplementIProbeMessage.Checked);
      }
		}

    private void txtClassName_TextChanged(object sender, EventArgs e)
    {
      IDictionaryService dictionaryService = GetService(typeof(IDictionaryService)) as IDictionaryService;
      dictionaryService.SetValue("ClassName", txtClassName.Text);
    }

    private void chkImplementIProbeMessage_CheckedChanged(object sender, EventArgs e)
    {
      IDictionaryService dictionaryService = GetService(typeof(IDictionaryService)) as IDictionaryService;
      dictionaryService.SetValue("ImplementIProbeMessage", chkImplementIProbeMessage.Checked);
    }
	}
}
