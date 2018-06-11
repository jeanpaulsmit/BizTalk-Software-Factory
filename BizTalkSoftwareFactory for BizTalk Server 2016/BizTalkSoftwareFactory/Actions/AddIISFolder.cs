#region Using Directives

using System;
using Microsoft.Practices.ComponentModel;
using Microsoft.Practices.RecipeFramework;
using System.IO;
using System.Xml;
using System.Diagnostics;
using Microsoft.Practices.RecipeFramework.Library;
using Microsoft.Win32;
using VSLangProj;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using BizTalkSoftwareFactory.OperationalManagement;
using System.Web;
using System.Xml.Linq;

#endregion

namespace BizTalkSoftwareFactory.Actions
{
    /// <summary>
    /// This action add a IISWebsite
    /// </summary>
    class AddIISFolder : Microsoft.Practices.RecipeFramework.Action
    {
        /// <summary>
        /// The name of the generalAssemblyInfoFile (used if generalAssemblyInfoFile not specified)
        /// </summary>
        private string name;
        /// <summary>
        /// Determine create WCF Site
        /// </summary>
        private bool createWCF;
        /// <summary>
        /// Determine create SOAP Site
        /// </summary>
        private bool createSoap;

        #region Input Properties

        /// <summary>
        /// Gets or sets the name of the SiteFolder
        /// </summary>
        [Input(Required = true)]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        /// <summary>
        /// Gets or sets create WCF Site
        /// </summary>
        [Input(Required = true)]
        public bool CreateWCF
        {
            get { return createWCF; }
            set { createWCF = value; }
        }
        /// <summary>
        /// Gets or sets create WCF Site
        /// </summary>
        [Input(Required = true)]
        public bool CreateSoap
        {
            get { return createSoap; }
            set { createSoap = value; }
        }


        #endregion

        #region IAction Members

        /// <summary>
        /// Executes the action.
        /// </summary>
        public override void Execute()
        {
            EnvDTE.DTE dte = this.GetService<EnvDTE.DTE>(true);

            CreateWebsiteFolders(dte);
            UpdateDeploymentProjectFile(dte);
        }

        private void UpdateDeploymentProjectFile(EnvDTE.DTE dte)
        {
            var soln = (EnvDTE80.Solution2)dte.Solution;

            var deploymentProjectFile = FindSolutionItemByFileExtension(soln, ".btdfproj");
            var fileName = deploymentProjectFile?.FileNames[1];
            XDocument xmlFile = XDocument.Load(fileName);

            var ns = xmlFile.Root.Name.Namespace;
            var project = xmlFile.Element(ns + "Project");
            var lastItemGroup = project.Elements(ns + "ItemGroup").LastOrDefault();

  //          <IISAppPool Include="$(ProjectName)WcfAppPool">
		//	<DotNetFrameworkVersion>v4.0</DotNetFrameworkVersion>
		//	<PipelineMode>Integrated</PipelineMode>
		//	<Enable32Bit>false</Enable32Bit>
		//	<DeployAction>CreateOrUpdate</DeployAction>
		//	<UndeployAction>None</UndeployAction>
		//</IISAppPool>
		//<IISApp Include="/Kw1c.BizTalk.ErrorHandling.WebsiteWCF-WebHttp">
		//	<AppPoolName>$(ProjectName)WcfAppPool</AppPoolName>
		//	<SiteName>Default Web Site</SiteName>
		//	<VirtualPath>/$(ProjectName).WCF</VirtualPath>
		//	<PhysicalPath>..\$(ProjectName).WebsiteWCF-WebHttp</PhysicalPath>
		//	<DeployAction>CreateOrUpdate</DeployAction>
		//	<UndeployAction>Delete</UndeployAction>
		//</IISApp>


            //determine IISAppPoolDefinitionGroup is Added
            var iISAppPoolDefinitionGroup = from g in xmlFile.Root.Elements(ns + "ItemDefinitionGroup").Elements(ns + "IISAppPool") select g;

            // add IISAppPoolDefinitionGroup if not added yet
            if (!iISAppPoolDefinitionGroup.Any())
            {
                //create iISAppPool
                var newItem = new XElement(ns + "ItemDefinitionGroup",
                        new XElement(ns + "IISAppPool",
                            new XElement(ns + "IdentityType", "SpecificUser"),
                            new XElement(ns + "UserName", "IISAppPoolUser"),
                            new XElement(ns + "Password", "IISAppPoolPass")
                        )
                    );
                
                lastItemGroup.AddAfterSelf(newItem);
                lastItemGroup = newItem;
            }

            //determine IISAppPoolItemGroup is Added
            var iISAppPoolItemGroup = from g in xmlFile.Root.Elements(ns + "ItemGroup").Elements(ns + "IISAppPool") select g;

            // add IISAppPoolDefinitionGroup if not added yet
            if (!iISAppPoolItemGroup.Any())
            {
                //create IISAppPool
                var newItem = new XElement(ns + "ItemGroup",
                        new XAttribute("Condition", @"Exists('..\$(ProjectName).WebsiteWCF-WebHttp')"),
                        new XElement(ns + "IISAppPool",
                            new XAttribute("Include", "$(ProjectName)WcfAppPool"),
                            new XElement(ns + "DotNetFrameworkVersion", "v4.0"),
                            new XElement(ns + "PipelineMode", "Integrated"),
                            new XElement(ns + "Enable32Bit", "false"),
                            new XElement(ns + "DeployAction", "CreateOrUpdate"),
                            new XElement(ns + "UndeployAction", "None")
                        )
                    );
                
                lastItemGroup.AddAfterSelf(newItem);
                lastItemGroup = newItem;
            }

            //determine IISAppItemGroup is Added
            var iISAppItemGroup = from g in xmlFile.Root.Elements(ns + "ItemGroup").Elements(ns + "IISApp") select g;

            // add IISAppGroup if not added yet
            if (!iISAppItemGroup.Any())
            {
                //create IISApp
                var newItem = new XElement(ns + "ItemGroup",
                        new XAttribute("Condition", @"Exists('..\$(ProjectName).WebsiteWCF-WebHttp')"),
                        new XElement(ns + "IISApp",
                            new XAttribute("Include", "/Kw1c.BizTalk.ErrorHandling.WebsiteWCF-WebHttp"),
                            new XElement(ns + "AppPoolName", "$(ProjectName)WcfAppPool"),
                            new XElement(ns + "SiteName", "Default Web Site"),
                            new XElement(ns + "VirtualPath", "/$(ProjectName).WCF"),
                            new XElement(ns + "PhysicalPath", @"..\$(ProjectName).WebsiteWCF-WebHttp"),
                            new XElement(ns + "DeployAction", "CreateOrUpdate"),
                            new XElement(ns + "UndeployAction", "None")
                        )
                    );
                lastItemGroup.AddAfterSelf(newItem);
                lastItemGroup = newItem;
            }

            xmlFile.Save(fileName);

        }

        private static EnvDTE.ProjectItem FindSolutionItemByFileExtension(EnvDTE80.Solution2 soln, string extension)
        {
            foreach (EnvDTE.Project project in soln.Projects)
            {
                foreach (EnvDTE.ProjectItem item in project.ProjectItems)
                {
                    if (Path.GetExtension(item.Name) == extension)
                    {
                        return item;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Performs an undo of the action.
        /// </summary>
        public override void Undo()
        {

        }
        private void CreateWebsiteFolders(EnvDTE.DTE dte)
        {

            var soln = (EnvDTE80.Solution2)dte.Solution;
            var deploymentFolderFullPath = Path.GetDirectoryName(soln.FullName);
            var wcfWebsiteFolder = Path.Combine(deploymentFolderFullPath, String.Format("{0}.WebsiteWCF", Name));
            var soapWebsiteFolder = Path.Combine(deploymentFolderFullPath, String.Format("{0}.WebsiteSoap", Name));

            var solutionNameParts = Name.Split('.');

            if (createWCF && !Directory.Exists(wcfWebsiteFolder))
            {
                var wcfWebsiteFolderInfo = Directory.CreateDirectory(wcfWebsiteFolder);
                var deploymentprj = soln.AddSolutionFolder("WebsiteWCF");

                //create App_Data Folder
                wcfWebsiteFolderInfo.CreateSubdirectory("App_Data");
                var serializationFileName = String.Format("{0}\\App_Data\\Serialization.xsd", wcfWebsiteFolder);
                using (var serializationFile = File.CreateText(serializationFileName))
                {
                    serializationFile.WriteLine(HttpUtility.HtmlDecode(@"&lt;?xml version=&quot;1.0&quot;?&gt;&lt;xs:schema xmlns:tns=&quot;http://schemas.microsoft.com/2003/10/Serialization/&quot; attributeFormDefault=&quot;qualified&quot; elementFormDefault=&quot;qualified&quot; targetNamespace=&quot;http://schemas.microsoft.com/2003/10/Serialization/&quot; xmlns:xs=&quot;http://www.w3.org/2001/XMLSchema&quot;&gt;&lt;xs:element name=&quot;anyType&quot; nillable=&quot;true&quot; type=&quot;xs:anyType&quot; /&gt;&lt;xs:element name=&quot;anyURI&quot; nillable=&quot;true&quot; type=&quot;xs:anyURI&quot; /&gt;&lt;xs:element name=&quot;base64Binary&quot; nillable=&quot;true&quot; type=&quot;xs:base64Binary&quot; /&gt;&lt;xs:element name=&quot;boolean&quot; nillable=&quot;true&quot; type=&quot;xs:boolean&quot; /&gt;&lt;xs:element name=&quot;byte&quot; nillable=&quot;true&quot; type=&quot;xs:byte&quot; /&gt;&lt;xs:element name=&quot;dateTime&quot; nillable=&quot;true&quot; type=&quot;xs:dateTime&quot; /&gt;&lt;xs:element name=&quot;decimal&quot; nillable=&quot;true&quot; type=&quot;xs:decimal&quot; /&gt;&lt;xs:element name=&quot;double&quot; nillable=&quot;true&quot; type=&quot;xs:double&quot; /&gt;&lt;xs:element name=&quot;float&quot; nillable=&quot;true&quot; type=&quot;xs:float&quot; /&gt;&lt;xs:element name=&quot;int&quot; nillable=&quot;true&quot; type=&quot;xs:int&quot; /&gt;&lt;xs:element name=&quot;long&quot; nillable=&quot;true&quot; type=&quot;xs:long&quot; /&gt;&lt;xs:element name=&quot;QName&quot; nillable=&quot;true&quot; type=&quot;xs:QName&quot; /&gt;&lt;xs:element name=&quot;short&quot; nillable=&quot;true&quot; type=&quot;xs:short&quot; /&gt;&lt;xs:element name=&quot;string&quot; nillable=&quot;true&quot; type=&quot;xs:string&quot; /&gt;&lt;xs:element name=&quot;unsignedByte&quot; nillable=&quot;true&quot; type=&quot;xs:unsignedByte&quot; /&gt;&lt;xs:element name=&quot;unsignedInt&quot; nillable=&quot;true&quot; type=&quot;xs:unsignedInt&quot; /&gt;&lt;xs:element name=&quot;unsignedLong&quot; nillable=&quot;true&quot; type=&quot;xs:unsignedLong&quot; /&gt;&lt;xs:element name=&quot;unsignedShort&quot; nillable=&quot;true&quot; type=&quot;xs:unsignedShort&quot; /&gt;&lt;xs:element name=&quot;char&quot; nillable=&quot;true&quot; type=&quot;tns:char&quot; /&gt;&lt;xs:simpleType name=&quot;char&quot;&gt;&lt;xs:restriction base=&quot;xs:int&quot; /&gt;&lt;/xs:simpleType&gt;&lt;xs:element name=&quot;duration&quot; nillable=&quot;true&quot; type=&quot;tns:duration&quot; /&gt;&lt;xs:simpleType name=&quot;duration&quot;&gt;&lt;xs:restriction base=&quot;xs:duration&quot;&gt;&lt;xs:pattern value=&quot;\-?P(\d*D)?(T(\d*H)?(\d*M)?(\d*(\.\d*)?S)?)?&quot; /&gt;&lt;xs:minInclusive value=&quot;-P10675199DT2H48M5.4775808S&quot; /&gt;&lt;xs:maxInclusive value=&quot;P10675199DT2H48M5.4775807S&quot; /&gt;&lt;/xs:restriction&gt;&lt;/xs:simpleType&gt;&lt;xs:element name=&quot;guid&quot; nillable=&quot;true&quot; type=&quot;tns:guid&quot; /&gt;&lt;xs:simpleType name=&quot;guid&quot;&gt;&lt;xs:restriction base=&quot;xs:string&quot;&gt;&lt;xs:pattern value=&quot;[\da-fA-F]{8}-[\da-fA-F]{4}-[\da-fA-F]{4}-[\da-fA-F]{4}-[\da-fA-F]{12}&quot; /&gt;&lt;/xs:restriction&gt;&lt;/xs:simpleType&gt;&lt;xs:attribute name=&quot;FactoryType&quot; type=&quot;xs:QName&quot; /&gt;&lt;/xs:schema&gt;"));
                    serializationFile.Close();
                }

                //create svc file
                var svcFileName = String.Format("{0}\\{1}.svc", wcfWebsiteFolder, solutionNameParts.Last());
                using (var svcFile = File.CreateText(svcFileName))
                {
                    svcFile.WriteLine("<%@ ServiceHost Language=\"c#\" Factory=\"Microsoft.BizTalk.Adapter.Wcf.Runtime.BasicHttpWebServiceHostFactory, Microsoft.BizTalk.Adapter.Wcf.Runtime, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\" %>");
                    svcFile.Close();
                }

                //create web.config file
                var webconfig = String.Format("{0}\\Web.config", wcfWebsiteFolder);
                using (var webconfigFile = File.CreateText(webconfig))
                {
                    webconfigFile.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    webconfigFile.WriteLine("<!--");
                    webconfigFile.WriteLine("  Note: As an alternative to hand editing this file you can use the");
                    webconfigFile.WriteLine("  web admin tool to configure settings for your application. Use");
                    webconfigFile.WriteLine("  the Website->Asp.Net Configuration option in Visual Studio.");
                    webconfigFile.WriteLine("  A full list of settings and comments can be found in");
                    webconfigFile.WriteLine("  machine.config.comments usually located in");
                    webconfigFile.WriteLine("  \\Windows\\Microsoft.Net\\Framework\\v2.x\\Config");
                    webconfigFile.WriteLine("  -->");
                    webconfigFile.WriteLine("<configuration xmlns=\"http://schemas.microsoft.com/.NetConfiguration/v2.0\">");
                    webconfigFile.WriteLine("<!--");
                    webconfigFile.WriteLine(" The <configSections> section declares handlers for custom configuration sections.");
                    webconfigFile.WriteLine(" -->");
                    webconfigFile.WriteLine("<configSections>");
                    webconfigFile.WriteLine("<section name=\"bizTalkSettings\" type=\"Microsoft.BizTalk.Adapter.Wcf.Runtime.BizTalkConfigurationSection, Microsoft.BizTalk.Adapter.Wcf.Runtime, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\" />");
                    webconfigFile.WriteLine("</configSections>");
                    webconfigFile.WriteLine("<!--");
                    webconfigFile.WriteLine(" The <bizTalkSettings> section specifies BizTalk specific configuration.");
                    webconfigFile.WriteLine(" -->");
                    webconfigFile.WriteLine("<bizTalkSettings>");
                    webconfigFile.WriteLine("<!--");
                    webconfigFile.WriteLine(" mexServiceHostFactory debug: Set to \"true\" to launch debugger when MexServiceHostFactory.CreateServiceHost(...) is called by IIS.");
                    webconfigFile.WriteLine(" Used to debug from initial point of activation by IIS. Default value is \"false\" for normal operation.");
                    webconfigFile.WriteLine(" -->");
                    webconfigFile.WriteLine("<mexServiceHostFactory debug=\"false\">");
                    webconfigFile.WriteLine("<receiveLocationMappings><!--add markupFileName=\"*.svc\" receiveLocationName=\"?\" publicBaseAddress=\"protocol://host[:port]\" /-->");
                    webconfigFile.WriteLine("</receiveLocationMappings>");
                    webconfigFile.WriteLine("</mexServiceHostFactory>");
                    webconfigFile.WriteLine("<!--");
                    webconfigFile.WriteLine(" webServiceHostFactory debug:");
                    webconfigFile.WriteLine(" Set to \"true\" to launch debugger when WebServiceHostFactory.CreateServiceHost(...) is called by IIS.");
                    webconfigFile.WriteLine(" Used to debug from initial point of activation by IIS.");
                    webconfigFile.WriteLine(" Default value is \"false\" for normal operation.");
                    webconfigFile.WriteLine(" -->");
                    webconfigFile.WriteLine("<webServiceHostFactory debug=\"false\" />");
                    webconfigFile.WriteLine("<!--");
                    webconfigFile.WriteLine(" isolatedReceiver disable:");
                    webconfigFile.WriteLine(" Set to \"true\" to skip IBTTransportProxy.RegisterIsolatedReceiver(...) and IBTTransportProxy.TerminateIsolatedReceiver(...) calls.");
                    webconfigFile.WriteLine(" Used for testing metadata exchange without having to setup receive location.");
                    webconfigFile.WriteLine(" Default value is \"false\" for normal operation.");
                    webconfigFile.WriteLine(" -->");
                    webconfigFile.WriteLine("<isolatedReceiver disable=\"false\" />");
                    webconfigFile.WriteLine("<!--");
                    webconfigFile.WriteLine(" btsWsdlExporter disable: Set to \"true\" to skip adding BtsWsdlExporter behavior extension to service endpoint.");
                    webconfigFile.WriteLine(" Used for testing or comparing strongly-typed WSDL customization versus weakly-typed WSDL of generic WCF service.");
                    webconfigFile.WriteLine(" Default value is \"false\" for normal operation.");
                    webconfigFile.WriteLine(" -->");
                    webconfigFile.WriteLine("<btsWsdlExporter disable=\"false\" />");
                    webconfigFile.WriteLine("</bizTalkSettings>");
                    webconfigFile.WriteLine("<system.web>");
                    webconfigFile.WriteLine("<!--");
                    webconfigFile.WriteLine("  Set compilation debug=\"true\" to insert debugging symbols into the compiled page.");
                    webconfigFile.WriteLine(" Because this affects performance, set this value to true only during development.");
                    webconfigFile.WriteLine(" -->");
                    webconfigFile.WriteLine("<compilation defaultLanguage=\"c#\" debug=\"false\">");
                    webconfigFile.WriteLine("<assemblies>");
                    webconfigFile.WriteLine("<add assembly=\"mscorlib, version=2.0.0.0, culture=neutral, publickeytoken=b77a5c561934e089\" />");
                    webconfigFile.WriteLine("<add assembly=\"Microsoft.BizTalk.Adapter.Wcf.Common, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\" />");
                    webconfigFile.WriteLine("<add assembly=\"Microsoft.BizTalk.Adapter.Wcf.Runtime, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\" />");
                    webconfigFile.WriteLine("</assemblies>");
                    webconfigFile.WriteLine("</compilation>");
                    webconfigFile.WriteLine("<!--");
                    webconfigFile.WriteLine(" The <authentication> section enables configuration of the security authentication mode");
                    webconfigFile.WriteLine(" used by ASP.NET to identify an incoming user.");
                    webconfigFile.WriteLine(" -->");
                    webconfigFile.WriteLine("<authentication mode=\"Windows\" />");
                    webconfigFile.WriteLine("<!--");
                    webconfigFile.WriteLine(" The <customErrors> section enables configuration of what to do if/when an unhandled error");
                    webconfigFile.WriteLine(" occurs during the execution of a request. Specifically, it enables developers to configure");
                    webconfigFile.WriteLine(" html error pages to be displayed in place of a error stack trace.");
                    webconfigFile.WriteLine(" -->");
                    webconfigFile.WriteLine("<customErrors mode=\"Off\" />");
                    webconfigFile.WriteLine("</system.web>");
                    webconfigFile.WriteLine("<!-- The <system.serviceModel> section specifies Windows Communication Foundation (WCF) configuration. -->");
                    webconfigFile.WriteLine("<system.serviceModel>");
                    webconfigFile.WriteLine("<behaviors>");
                    webconfigFile.WriteLine("<serviceBehaviors>");
                    webconfigFile.WriteLine("<behavior name=\"ServiceBehaviorConfiguration\">");
                    webconfigFile.WriteLine("<serviceDebug includeExceptionDetailInFaults=\"true\" />");
                    webconfigFile.WriteLine("<serviceMetadata httpGetEnabled=\"true\" /> <!-- Aanpassen naar 'httpsGetEnabled=\"true\"' indien https binding wordt gebruikt -->");
                    webconfigFile.WriteLine("</behavior>");
                    webconfigFile.WriteLine("</serviceBehaviors>");
                    webconfigFile.WriteLine("</behaviors>");
                    webconfigFile.WriteLine("<services>");
                    webconfigFile.WriteLine("<!-- Note: the service name must match the configuration name for the service implementation. -->");
                    webconfigFile.WriteLine("<service name=\"Microsoft.BizTalk.Adapter.Wcf.Runtime.BizTalkServiceInstance\" behaviorConfiguration=\"ServiceBehaviorConfiguration\">");
                    webconfigFile.WriteLine("</service>");
                    webconfigFile.WriteLine("</services>");
                    webconfigFile.WriteLine("</system.serviceModel>");
                    webconfigFile.WriteLine("</configuration>");
                    webconfigFile.Close();
                }

                AddSolutionFoldersAndFiles(wcfWebsiteFolder, deploymentprj);
            }

            if (createSoap && !Directory.Exists(soapWebsiteFolder))
            {
                var soapWebsiteFolderInfo = Directory.CreateDirectory(soapWebsiteFolder);

                var deploymentprj = soln.AddSolutionFolder("WebsiteSoap");

                //create App_Data Folder
                soapWebsiteFolderInfo.CreateSubdirectory("App_Code");
                var dataTypesFileName = String.Format("{0}\\App_Code\\DataTypes.cs", soapWebsiteFolder);
                using (var dataTypesFile = File.CreateText(dataTypesFileName))
                {
                    dataTypesFile.Write("");
                    dataTypesFile.Close();
                }

                //create asmx file
                var asmxFileName = String.Format("{0}\\{1}.asmx", wcfWebsiteFolder, solutionNameParts.Last());
                using (var svcFile = File.CreateText(asmxFileName))
                {
                    svcFile.WriteLine(String.Format("<%@ WebService Language=\"c#\" CodeBehind=\"~/App_Code/{0}.asmx.cs\" Class=\"BizTalkWebService.{0}\" %>", solutionNameParts.Last()));
                    svcFile.Close();
                }

                AddSolutionFoldersAndFiles(soapWebsiteFolder, deploymentprj);
            }
        }
        private static void AddSolutionFoldersAndFiles(string path, EnvDTE.Project solutionFolderProject)
        {

            foreach (var fileInfo in new DirectoryInfo(path).GetFiles())
            {
                solutionFolderProject.ProjectItems.AddFromFile(fileInfo.FullName);
            }

            foreach (var directoryInfo in new DirectoryInfo(path).GetDirectories())
            {
                var deploymentParentFoldername = directoryInfo.Name;
                var deploymentsf = (EnvDTE80.SolutionFolder)solutionFolderProject.Object;
                var solutionFolderProject2 = deploymentsf.AddSolutionFolder(deploymentParentFoldername);

                AddSolutionFoldersAndFiles(directoryInfo.FullName, solutionFolderProject2);
            }

        }


        #endregion
    }
}
