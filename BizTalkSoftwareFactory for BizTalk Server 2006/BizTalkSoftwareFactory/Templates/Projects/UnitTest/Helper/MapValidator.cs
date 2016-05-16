using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.XLANGs.BaseTypes;
using System.IO;
using System.Xml.XPath;
using System.Xml;
using System.Diagnostics;

/*
 * This was taken from the blog of Michael Stephenson
 * http://geekswithblogs.net/michaelstephenson/archive/2008/03/30/120851.aspx
 * 
 * Note from his blog:  
 * I developed the sample on a BizTalk 2006 R2 machine.  I think the signature of the BtsXsltTransform class has changed between versions as to get the helper to work on a standard BizTalk 2006 (non R2) machine I had to change the transform line within the helper to the below signature.
 * mapInstance.StreamingTransform.Transform(inputStream, mapInstance.TransformArgs, outputStream, resolver);
 * 
 * 
 */

namespace $safeprojectname$.Helper
{
  /// <summary>
  /// This class will provide helper methods for testing maps
  /// </summary>
  public sealed class MapValidator
  {
    /// <summary>
    /// Ctor
    /// </summary>
    private MapValidator()
    {

    }

    /// <summary>
    /// Executes the map using the in memory technique
    /// </summary>
    /// <param name="mapInstance"></param>
    /// <param name="inputFilePath"></param>
    /// <param name="outputFilePath"></param>
    public static void ExecuteMapInMemory(TransformBase mapInstance, string inputFilePath, string outputFilePath)
    {
      using (FileStream inputStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
      {
        using (FileStream outputStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
        {
          XPathDocument doc = new XPathDocument(inputStream);
          mapInstance.Transform.Transform(doc, mapInstance.TransformArgs, outputStream);
        }
      }
    }
    /// <summary>
    /// Executes the map using the scalable transform technique
    /// </summary>
    /// <param name="mapInstance"></param>
    /// <param name="inputFilePath"></param>
    /// <param name="outputFilePath"></param>
    public static void ExecuteMapScalable(TransformBase mapInstance, string inputFilePath, string outputFilePath)
    {
      ExecuteMapScalable(mapInstance, inputFilePath, outputFilePath, new XmlUrlResolver(), false);
    }
    /// <summary>
    /// Executes the map using the scalable transform technique
    /// </summary>
    /// <param name="mapInstance"></param>
    /// <param name="inputFilePath"></param>
    /// <param name="outputFilePath"></param>
    /// <param name="resolver"></param>
    /// <param name="whitespaceCorrect"></param>
    public static void ExecuteMapScalable(TransformBase mapInstance, string inputFilePath, string outputFilePath, XmlResolver resolver, bool whitespaceCorrect)
    {
      using (FileStream inputStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
      {
        using (FileStream outputStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
        {
          XmlReader xmlRdr = new XmlTextReader(inputStream);

          /* This is the BizTalk Server 2006 way of calling */
          mapInstance.StreamingTransform.Transform(inputStream, mapInstance.TransformArgs, outputStream, resolver);

          /* This is the R2 way of calling */
          // mapInstance.StreamingTransform.ScalableTransform(xmlRdr, mapInstance.TransformArgs, outputStream, resolver, whitespaceCorrect);
        }
      }
    }
    /// <summary>
    /// Compares the map output file against a predefined file which is what the map should produce
    /// </summary>
    /// <param name="outputFilePath"></param>
    /// <param name="expectedFilePath"></param>
    /// <returns></returns>
    public static void ValidateMapOutput(string outputFilePath, string expectedFilePath)
    {
      string output = ReadFile(outputFilePath);
      string expected = ReadFile(expectedFilePath);

      Debug.WriteLine(string.Format("The output file '{0}' has length {1} and contains: {2}", outputFilePath, outputFilePath.Length, output));
      Debug.WriteLine(string.Format("The expected file '{0}' has length {1} and contains: {2}", expectedFilePath, expectedFilePath.Length, expected));

      if (output.Length != expected.Length)
        throw new ApplicationException("The lengths of the output and expected files do not match");

      for (int index = 0; index <= output.Length - 1; index++)
      {
        char outputChar = output[index];
        char expectedChar = expected[index];

        if (!outputChar.Equals(expectedChar))
        {
          Debug.WriteLine("The output file so far is: " + Environment.NewLine + output.Substring(0, index));
          Debug.WriteLine(Environment.NewLine);
          Debug.WriteLine(Environment.NewLine);
          Debug.WriteLine("The expected file so far is: " + Environment.NewLine + expected.Substring(0, index));
          throw new ApplicationException("The char at index " + index.ToString() + " does not match");
        }
      }
    }
    /// <summary>
    /// Reads the contents of the file
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private static string ReadFile(string path)
    {
      string content;
      using (FileStream fs = new FileStream(path, FileMode.Open))
      {
        using (StreamReader rdr = new StreamReader(fs))
        {
          content = rdr.ReadToEnd();
        }
      }
      return content;
    }
  }


}
