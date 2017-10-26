using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.BizTalk.Component.Utilities;

namespace BizTalkSoftwareFactory.CustomWizardPages.PipelineComponentWizard
{
    /// <summary>
    /// used to strongly cast the string selection choosen by the user
    /// as designer variables types. primarily used to facilitate strong
    /// casting within the code creating the pipeline component using CodeDOM
    /// </summary>
    internal class DesignerVariableType
    {
        /// <summary>
        /// represents the string primitive type
        /// </summary>
        private const string dvtString = "string";
        /// <summary>
        /// represents the bool primitive type
        /// </summary>
        private const string dvtBoolean = "bool";
        /// <summary>
        /// represents the int primitive type
        /// </summary>
        private const string dvtInt = "int";
        /// <summary>
        /// represents the long primitive type
        /// </summary>
        private const string dvtLong = "long";
        /// <summary>
        /// represents the short primitive type
        /// </summary>
        private const string dvtShort = "short";
        /// <summary>
        /// represents the Microsoft.BizTalk.Component.Utilities.SchemaList type
        /// </summary>
        private const string dvtSchemaList = "SchemaList";
        /// <summary>
        /// represents the Microsoft.BizTalk.Component.Utilities.SchemaWithNone type
        /// </summary>
        private const string dvtSchemaWithNone = "SchemaWithNone";
        /// <summary>
        /// determines whether a reference to Microsoft.BizTalk.Component.Utilities
        /// is needed within the generated project
        /// </summary>
        private static bool _schemaListUsed = false;

        /// <summary>
        /// determines whether the user added one or more Designer properties of the
        /// SchemaList type
        /// </summary>
        public static bool SchemaListUsed
        {
            get { return _schemaListUsed; }
        }

        /// <summary>
        /// checks whether the inbound string matches the regular expressions
        /// set up to catch the supported types and returns a typeof of the
        /// associated type.
        /// </summary>
        /// <param name="dataType">the representation of the type</param>
        /// <returns>the actual type, if supported. returns typeof(object) if no
        /// match can be found</returns>
        public static Type getType(string dataType)
        {
            if (Regex.IsMatch(dataType, dvtBoolean, RegexOptions.IgnoreCase))
            {
                return typeof(bool);
            }
            else if (Regex.IsMatch(dataType, dvtInt, RegexOptions.IgnoreCase))
            {
                return typeof(int);
            }
            else if (Regex.IsMatch(dataType, dvtLong, RegexOptions.IgnoreCase))
            {
                return typeof(long);
            }
            else if (Regex.IsMatch(dataType, dvtSchemaList, RegexOptions.IgnoreCase))
            {
                // Microsoft.BizTalk.Component.Utilities needs to be referenced
                _schemaListUsed = true;

                return typeof(SchemaList);
            }
            else if (Regex.IsMatch(dataType, dvtSchemaWithNone, RegexOptions.IgnoreCase))
            {
                // Microsoft.BizTalk.Component.Utilities needs to be referenced
                _schemaListUsed = true;

                return typeof(SchemaWithNone);
            }
            else if (Regex.IsMatch(dataType, dvtShort, RegexOptions.IgnoreCase))
            {
                return typeof(short);
            }
            else if (Regex.IsMatch(dataType, dvtString, RegexOptions.IgnoreCase))
            {
                return typeof(string);
            }
            else
            {
                return typeof(object);
            }
        }

        /// <summary>
        /// returns a string array for all supported variable types.
        /// used to create the dropdownlist in which the user can
        /// choose what type designer property should encompass
        /// </summary>
        /// <returns></returns>
        public static string[] ToArray()
        {
            ArrayList retVal = new ArrayList();

            retVal.Add(dvtString);
            retVal.Add(dvtBoolean);
            retVal.Add(dvtInt);
            retVal.Add(dvtLong);
            retVal.Add(dvtShort);
            retVal.Add(dvtSchemaList);
            retVal.Add(dvtSchemaWithNone);

            return (string[])retVal.ToArray(typeof(string));
        }
    }
}
