using System;
using System.Drawing.Design;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using Microsoft.Practices.RecipeFramework.Services;
using Microsoft.Practices.RecipeFramework;
using System.Globalization;
using System.IO;


namespace BizTalkSoftwareFactory.Editors
{
    /// <summary>
    /// Extension of FileChooser: 
    /// this filedialog filters on keyfiles
    /// </summary>
    public class KeyFileChooser : FileChooser
    {
        public override string Title
        {
            get { return "Please choose a keyfile"; }
        }
        public override string FileFilter
        {
            get { return "Keyfiles (*.snk)|*.snk"; }
        }
    }
}
