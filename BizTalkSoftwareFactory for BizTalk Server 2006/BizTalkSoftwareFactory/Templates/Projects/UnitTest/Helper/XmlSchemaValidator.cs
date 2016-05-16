using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Xml.Schema;

/*
 * This was taken from the blog of Michael Stephenson
 * http://geekswithblogs.net/michaelstephenson/archive/2008/03/30/120850.aspx
 * 
 */
namespace $safeprojectname$.Helper
{
  /// <summary>
  /// Provides methods for validating xml for tests
  /// </summary>
  public class XmlSchemaValidator
  {
    private bool _IsValid = true;

    /// <summary>
    /// Ctor
    /// </summary>
    private XmlSchemaValidator()
    {
    }
    /// <summary>
    /// Exposes the isvalid property
    /// </summary>
    public bool IsValid
    {
      get { return _IsValid; }
      set { _IsValid = value; }
    }
    /// <summary>
    /// Validates the document
    /// </summary>
    /// <param name="xmlPath"></param>
    /// <param name="schemaPath"></param>
    private void Validate(string xmlPath, string schemaPath)
    {
      using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
      {
        using (FileStream schemaStream = new FileStream(schemaPath, FileMode.Open, FileAccess.Read))
        {
          XmlSchema schema = XmlSchema.Read(schemaStream, null);
          XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
          xmlReaderSettings.Schemas.Add(schema);
          xmlReaderSettings.ValidationType = ValidationType.Schema;
          xmlReaderSettings.ValidationEventHandler += new ValidationEventHandler(ValidationEventHandler);
          XmlReader xmlReader = XmlReader.Create(xmlStream, xmlReaderSettings);
          while (xmlReader.Read())
          {
            if (!_IsValid)
              break;
          }
        }
      }
    }
    /// <summary>
    /// Validates an xml file against a schema
    /// </summary>
    /// <param name="xmlPath"></param>
    /// <param name="schemaPath"></param>
    /// <returns></returns>
    public static bool ValidateAgainstSchema(string xmlPath, string schemaPath)
    {
      XmlSchemaValidator validator = new XmlSchemaValidator();
      validator.Validate(xmlPath, schemaPath);
      return validator.IsValid;
    }
    /// <summary>
    /// Handles the validation event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void ValidationEventHandler(object sender, ValidationEventArgs args)
    {
      Trace.WriteLine(args.Message);
      if (args.Exception != null)
      {
        _IsValid = false;
        Trace.WriteLine(args.Exception.ToString());
      }
    }
  }
}
