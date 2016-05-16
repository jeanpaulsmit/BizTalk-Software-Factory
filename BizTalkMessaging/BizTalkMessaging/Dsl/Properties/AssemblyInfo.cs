#region Using directives

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.ConstrainedExecution;

#endregion

//
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
[assembly: AssemblyTitle(@"")]
[assembly: AssemblyDescription(@"")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(@"")]
[assembly: AssemblyProduct(@"BizTalkMessaging")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: System.Resources.NeutralResourcesLanguage("en")]

//
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:

[assembly: AssemblyVersion(@"1.0.0.0")]
[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
[assembly: ReliabilityContract(Consistency.MayCorruptProcess, Cer.None)]

//
// Make the Dsl project internally visible to the DslPackage assembly
//
[assembly: InternalsVisibleTo(@"BizTalkMessaging.DslPackage, PublicKey=0024000004800000940000000602000000240000525341310004000001000100F734E2C14FA381F509E99CE6C953C612E223C64A189F6D2EBB8B8627CDA5AD90FD91C170C9D76CE0DE81EB5058A3740A4933ECCDA499CD53B09573221D5C8F98F9D65096B45C76B8294B266CAF9B2F2B124DD3E374CD5B082CAC2EF86ADFC9BE84B6B912699B825D2497901F999FB9E31F4151208241F61D4E733A4AC1CCECAA")]