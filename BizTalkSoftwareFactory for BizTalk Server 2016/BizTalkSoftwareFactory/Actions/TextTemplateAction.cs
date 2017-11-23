using Microsoft.Practices.ComponentModel;
using Microsoft.Practices.RecipeFramework.VisualStudio.Library.Templates;
using Microsoft.Practices.RecipeFramework.Extensions.References;
using System;
using System.ComponentModel.Design;
using System.IO;
using Microsoft.Practices.RecipeFramework;

namespace BizTalkSoftwareFactory.Actions
{
    [ServiceDependency(typeof(ITypeResolutionService))]
    public sealed class TextTemplateAction : T4Action
    {
        private string template;
        private string customTemplate;
        private string rendered;
        [Input(Required = true)]
        public string Template
        {
            get
            {
                return this.template;
            }
            set
            {
                this.template = value;
            }
        }
        [Input(Required = false)]
        public string CustomTemplate
        {
            get
            {
                return this.customTemplate;
            }
            set
            {
                this.customTemplate = value;
            }
        }
        [Output]
        public string Content
        {
            get
            {
                return this.rendered;
            }
            set
            {
                this.rendered = value;
            }
        }
        public override void Execute()
        {
            string text = this.Template;
            string templateCode = string.Empty;
            if (text == null)
            {
                throw new ArgumentNullException("Template");
            }

            //check for custom Template
            if (string.IsNullOrEmpty(CustomTemplate))
            {
                string templateBasePath = base.GetTemplateBasePath();
                if (!Path.IsPathRooted(text))
                {
                    text = Path.Combine(templateBasePath, text);
                }
                text = new FileInfo(text).FullName;
                if (!text.StartsWith(templateBasePath))
                {
                    throw new ArgumentException("TemplatePath Not In PackageFolder");
                }
            }
            else
            {
                text = new FileInfo(CustomTemplate).FullName;
            }


            templateCode = File.ReadAllText(text);
            this.Content = base.Render(templateCode, text);
        }
        public override void Undo()
        {
        }
    }
}
