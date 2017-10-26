using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace $safeprojectname$
{
    /// <summary>
    /// Generic helper class to support unit testing
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Get the namespace from a schema type
        /// </summary>
        /// <param name="schemaType"></param>
        /// <returns></returns>
        public static string GetSchemaNamespaceFromBizTalkType(Type schemaType)
        {
            // First check if this is actually a BizTalk assembly
            if (schemaType.BaseType != typeof(Microsoft.XLANGs.BaseTypes.SchemaBase) &&
                schemaType.BaseType != typeof(Microsoft.BizTalk.TestTools.Schema.TestableSchemaBase))
            {
                throw new Exception("Expected type: Microsoft.XLANGs.BaseTypes.SchemaBase");
            }

            Object schemaobject = schemaType.InvokeMember(  null,
                                                            System.Reflection.BindingFlags.DeclaredOnly |
                                                            System.Reflection.BindingFlags.Public |
                                                            System.Reflection.BindingFlags.NonPublic |
                                                            System.Reflection.BindingFlags.Instance |
                                                            System.Reflection.BindingFlags.CreateInstance,
                                                            null, null, null);

            Microsoft.XLANGs.BaseTypes.SchemaBase schemaBase = schemaobject as Microsoft.XLANGs.BaseTypes.SchemaBase;

            return schemaBase.Schema.TargetNamespace;
        }

        /// <summary>
        /// Common method to abstract the use of Compare, to check if the actual is equal to the expected
        /// </summary>
        /// <param name="actualFile">File with output of the map</param>
        /// <param name="expectedFile">File with the expected output</param>
        /// <returns></returns>
        public static void SetXpathValue(string dataFile, string xpath, string value)
        {
            // First read the XML file
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(dataFile);
            XmlNodeList xnodeList = xdoc.SelectNodes(xpath);
            foreach (XmlNode xnode in xnodeList)
            {
                if (xnode != null)
                {
                    xnode.InnerXml = value;
                }
            }
            // And write the updated file back
            xdoc.Save(dataFile);
        }

        /// <summary>
        /// Common method to abstract the use of Compare, to check if the actual is equal to the expected
        /// </summary>
        /// <param name="actualFile">File with output of the map</param>
        /// <param name="expectedFile">File with the expected output</param>
        /// <returns></returns>
        public static bool IsActualEqualToExpected(string actualFile, string expectedFile)
        {
            XmlUnit.XmlInput xmlOutput = new XmlUnit.XmlInput(File.ReadAllText(actualFile));
            XmlUnit.XmlInput xmlExpected = new XmlUnit.XmlInput(File.ReadAllText(expectedFile));

            XmlUnit.DiffConfiguration dc = new XmlUnit.DiffConfiguration(System.Xml.WhitespaceHandling.None);
            XmlUnit.XmlDiff diff = new XmlUnit.XmlDiff(xmlOutput, xmlExpected, dc);

            XmlUnit.DiffResult result = diff.Compare();
            return result.Equal;
        }

        /// <summary>
        /// Common method to abstract the use of Compare, to check if the actual is identical to the expected
        /// </summary>
        /// <param name="actualFile">File with output of the map</param>
        /// <param name="expectedFile">File with the expected output</param>
        /// <returns></returns>
        public static bool IsActualIdenticalToExpected(string actualFile, string expectedFile)
        {
            XmlUnit.XmlInput xmlOutput = new XmlUnit.XmlInput(File.ReadAllText(actualFile));
            XmlUnit.XmlInput xmlExpected = new XmlUnit.XmlInput(File.ReadAllText(expectedFile));

            XmlUnit.DiffConfiguration dc = new XmlUnit.DiffConfiguration(System.Xml.WhitespaceHandling.None);
            XmlUnit.XmlDiff diff = new XmlUnit.XmlDiff(xmlOutput, xmlExpected, dc);

            XmlUnit.DiffResult result = diff.Compare();
            return result.Identical;
        }

        /// <summary>
        /// Common method to abstract the use of EvaluateXpath
        /// </summary>
        /// <param name="actualFile">File with output of the map</param>
        /// <param name="xpath">XPath to lookup</param>
        /// <param name="expectedValue">Value to compare with</param>
        /// <returns></returns>
        public static bool IsExpectedXpathValue(string actualFile, string xpath, string expectedValue)
        {
            XmlUnit.XmlInput xmlOutput = new XmlUnit.XmlInput(File.ReadAllText(actualFile));
            XmlUnit.XPath xp = new XmlUnit.XPath(xpath);
            string valueFromOutput = xp.EvaluateXPath(xmlOutput);
            return (valueFromOutput == expectedValue);
        }

        /// <summary>
        /// Common method to get the first match of an xpath value. XmlUnit concatenates all matches in the result, we don't need that
        /// </summary>
        /// <param name="actualFile">File with output of the map</param>
        /// <param name="xpath">XPath to lookup</param>
        /// <returns></returns>
        public static string GetXpathValue(string actualFile, string xpath)
        {
            // First read the XML file
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(actualFile);
            XmlNode xnode = xdoc.SelectSingleNode(xpath);
            if (xnode != null)
            {
                return xnode.InnerXml;
            }
            throw new XmlException("GetXpathValue Exception: Error in Xpath or Xpath has no result");
        }
    }
}
