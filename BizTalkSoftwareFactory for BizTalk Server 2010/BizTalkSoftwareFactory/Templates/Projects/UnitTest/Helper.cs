using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BizTalkSoftwareFactory.Templates.Projects.UnitTest
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
    }
}
