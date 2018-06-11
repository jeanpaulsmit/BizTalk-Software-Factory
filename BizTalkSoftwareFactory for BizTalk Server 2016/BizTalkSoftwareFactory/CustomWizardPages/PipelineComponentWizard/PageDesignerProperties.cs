using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Microsoft.Practices.WizardFramework;
using BizTalkSoftwareFactory.BusinessEntities;
using System.ComponentModel.Design;
using System.Collections.Generic;

namespace BizTalkSoftwareFactory.CustomWizardPages.PipelineComponentWizard
{
	public class PageDesignerProperties : CustomWizardPage, IWizardControl
	{
		private bool _IsLoaded = false;
		private System.Windows.Forms.ErrorProvider ErrProv;
		private System.Windows.Forms.TextBox txtDesignerProperty;
		private System.Windows.Forms.Button cmdDesignerPropertyDel;
		private System.Windows.Forms.Button cmdDesignerPropertyAdd;
		private System.Windows.Forms.ComboBox cmbDesignerPropertyDataType;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox lstDesignerProperties;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblHelpDesignerProperties;
		private System.ComponentModel.IContainer components = null;

    DesignPropertyItemCollection designPropertyList = new DesignPropertyItemCollection();

    /// <summary>
    /// Gets or sets the property collection.
    /// </summary>
    /// <value>The property collection.</value>
    [RecipeArgument]
    public DesignPropertyItemCollection DesignPropertyList
    {
      get { return designPropertyList; }
      set { designPropertyList = value; }
    }

    public PageDesignerProperties(WizardForm parent)
      : base(parent)
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
      Init();
		}

    public override bool OnActivate()
    {
      // Init the object
      IDictionaryService dictionary = (IDictionaryService)GetService(typeof(IDictionaryService));
      dictionary.SetValue("DesignPropertyList", new DesignPropertyItemCollection());

      return base.OnActivate();
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
			get {	return true;	}
		}

		public bool NeedSummary
		{
			get {	return false;	}
		}

		private bool VarNameAlreadyExists(string strValue)
		{
			foreach(object o in lstDesignerProperties.Items)
			{
				string strObjVal = o.ToString();
				strObjVal = strObjVal.Remove(strObjVal.IndexOf(" ("),strObjVal.Length - strObjVal.IndexOf(" ("));
				if (strObjVal == strValue)
					return true;
			}
			return false;
		}

		/// <summary>
		/// Resets all of the errorproviders when anything succeeds
		/// </summary>
		private void ResetAllErrProviders()
		{
			foreach(Control ctl in this.Controls)
			{
				ErrProv.SetError(ctl, "");
			}
		}

    private void Init()
    {
      try
      {
        if (_IsLoaded)
          return;

        foreach (string strDataType in DesignerVariableType.ToArray())
        {
          cmbDesignerPropertyDataType.Items.Add(strDataType);
        }
        cmbDesignerPropertyDataType.SelectedIndex = 0;

        _IsLoaded = true;
      }
      catch (Exception err)
      {
        MessageBox.Show(err.Message);
        Trace.WriteLine(err.Message + Environment.NewLine + err.StackTrace);
      }
      cmbDesignerPropertyDataType.Focus();
    }

    private void cmdDesignerPropertyDel_Click(object sender, System.EventArgs e)
		{
			try
			{
				ResetAllErrProviders();
				if (lstDesignerProperties.SelectedItem == null)
				{
					ErrProv.SetError(cmdDesignerPropertyDel,
						"Please select a value in the property list");
					return;
				}

				Object objItem = lstDesignerProperties.SelectedItem;
				string strVal = objItem.ToString();
				string strPropName = strVal.Substring(0,strVal.IndexOf("(") - 1);
				lstDesignerProperties.Items.Remove(lstDesignerProperties.SelectedItem);

        // Also remove item from the wizard
        IDictionaryService dictionaryService = GetService(typeof(IDictionaryService)) as IDictionaryService;
        designPropertyList.Remove(new DesignPropertyItem(txtDesignerProperty.Text, cmbDesignerPropertyDataType.Text));
        dictionaryService.SetValue("DesignPropertyList", designPropertyList);

			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message);
				Trace.WriteLine(err.Message + Environment.NewLine + err.StackTrace);
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageDesignerProperties));
      this.ErrProv = new System.Windows.Forms.ErrorProvider(this.components);
      this.txtDesignerProperty = new System.Windows.Forms.TextBox();
      this.cmdDesignerPropertyDel = new System.Windows.Forms.Button();
      this.cmdDesignerPropertyAdd = new System.Windows.Forms.Button();
      this.cmbDesignerPropertyDataType = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.lstDesignerProperties = new System.Windows.Forms.ListBox();
      this.label1 = new System.Windows.Forms.Label();
      this.lblHelpDesignerProperties = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.ErrProv)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      this.SuspendLayout();
      // 
      // infoPanel
      // 
      resources.ApplyResources(this.infoPanel, "infoPanel");
      // 
      // ErrProv
      // 
      this.ErrProv.ContainerControl = this;
      resources.ApplyResources(this.ErrProv, "ErrProv");
      // 
      // txtDesignerProperty
      // 
      resources.ApplyResources(this.txtDesignerProperty, "txtDesignerProperty");
      this.txtDesignerProperty.Name = "txtDesignerProperty";
      // 
      // cmdDesignerPropertyDel
      // 
      resources.ApplyResources(this.cmdDesignerPropertyDel, "cmdDesignerPropertyDel");
      this.cmdDesignerPropertyDel.Name = "cmdDesignerPropertyDel";
      this.cmdDesignerPropertyDel.Click += new System.EventHandler(this.cmdDesignerPropertyDel_Click);
      // 
      // cmdDesignerPropertyAdd
      // 
      resources.ApplyResources(this.cmdDesignerPropertyAdd, "cmdDesignerPropertyAdd");
      this.cmdDesignerPropertyAdd.Name = "cmdDesignerPropertyAdd";
      this.cmdDesignerPropertyAdd.Click += new System.EventHandler(this.cmdDesignerPropertyAdd_Click);
      // 
      // cmbDesignerPropertyDataType
      // 
      this.cmbDesignerPropertyDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      resources.ApplyResources(this.cmbDesignerPropertyDataType, "cmbDesignerPropertyDataType");
      this.cmbDesignerPropertyDataType.Name = "cmbDesignerPropertyDataType";
      this.cmbDesignerPropertyDataType.SelectedIndexChanged += new System.EventHandler(this.cmbDesignerPropertyDataType_Changed);
      // 
      // label2
      // 
      resources.ApplyResources(this.label2, "label2");
      this.label2.Name = "label2";
      // 
      // lstDesignerProperties
      // 
      resources.ApplyResources(this.lstDesignerProperties, "lstDesignerProperties");
      this.lstDesignerProperties.Name = "lstDesignerProperties";
      // 
      // label1
      // 
      resources.ApplyResources(this.label1, "label1");
      this.label1.Name = "label1";
      // 
      // lblHelpDesignerProperties
      // 
      resources.ApplyResources(this.lblHelpDesignerProperties, "lblHelpDesignerProperties");
      this.lblHelpDesignerProperties.Name = "lblHelpDesignerProperties";
      // 
      // PageDesignerProperties
      // 
      resources.ApplyResources(this, "$this");
      this.Controls.Add(this.lblHelpDesignerProperties);
      this.Controls.Add(this.txtDesignerProperty);
      this.Controls.Add(this.cmdDesignerPropertyDel);
      this.Controls.Add(this.cmdDesignerPropertyAdd);
      this.Controls.Add(this.cmbDesignerPropertyDataType);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.lstDesignerProperties);
      this.Controls.Add(this.label1);
      this.Name = "PageDesignerProperties";
      this.Controls.SetChildIndex(this.infoPanel, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.lstDesignerProperties, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.cmbDesignerPropertyDataType, 0);
      this.Controls.SetChildIndex(this.cmdDesignerPropertyAdd, 0);
      this.Controls.SetChildIndex(this.cmdDesignerPropertyDel, 0);
      this.Controls.SetChildIndex(this.txtDesignerProperty, 0);
      this.Controls.SetChildIndex(this.lblHelpDesignerProperties, 0);
      ((System.ComponentModel.ISupportInitialize)(this.ErrProv)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

		}
		#endregion

		private void cmdDesignerPropertyAdd_Click(object sender, System.EventArgs e)
		{
			try
			{
				ResetAllErrProviders();
				if (!Regex.IsMatch(txtDesignerProperty.Text,@"^[_a-zA-Z][_a-zA-Z0-9]*$"))
				{
					ErrProv.SetError(txtDesignerProperty,
						"Please enter a valid name for the new property");
					return;
				}
				if (VarNameAlreadyExists(txtDesignerProperty.Text))
				{
					ErrProv.SetError(txtDesignerProperty,
						"Please enter a unique name. No two properties can have the same name");
					return;
				}
				lstDesignerProperties.Items.Add(txtDesignerProperty.Text + " (" + cmbDesignerPropertyDataType.Text + ")");

        // Also add item to the wizard
        IDictionaryService dictionaryService = GetService(typeof(IDictionaryService)) as IDictionaryService;
        designPropertyList.Add(new DesignPropertyItem(txtDesignerProperty.Text, cmbDesignerPropertyDataType.Text));
        dictionaryService.SetValue("DesignPropertyList", designPropertyList);

        txtDesignerProperty.Clear();
				cmbDesignerPropertyDataType.Text = "string";

			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message);
				Trace.WriteLine(err.Message + Environment.NewLine + err.StackTrace);
			}
		
		}

		private void cmbDesignerPropertyDataType_Changed(object sender, System.EventArgs e)
		{
			string currentSelection = cmbDesignerPropertyDataType.Items[cmbDesignerPropertyDataType.SelectedIndex].ToString();
			if(currentSelection == "SchemaList")
			{
				lblHelpDesignerProperties.Text = "SchemaList allows for a dialog to pick any number of referenced schemas";
				lblHelpDesignerProperties.Visible = true;
			} 
			else if(currentSelection == "SchemaWithNone")
			{
				lblHelpDesignerProperties.Text = "SchemaWithNone allows for a dropdown listbox with referenced schemas, selecting one only";
				lblHelpDesignerProperties.Visible = true;
			}
			else
			{
				lblHelpDesignerProperties.Visible = false;
			}
		}
	}
}

