<?xml version="1.0" encoding="utf-8"?>
<Dsl dslVersion="1.0.0.0" Id="4d0412b7-4396-4ceb-a2cf-263a5cbeb971" Description="Description for BizTalkMessaging.BizTalkMessaging" Name="BizTalkMessaging" DisplayName="BizTalkMessaging" Namespace="BizTalkMessaging" ProductName="BizTalkMessaging" PackageGuid="9204b2f6-ddc7-4163-9609-02e626156d2d" PackageNamespace="BizTalkMessaging" xmlns="http://schemas.microsoft.com/VisualStudio/2005/DslTools/DslDefinitionModel">
  <Classes>
    <DomainClass Id="5a57abbb-ca01-4fe0-84c8-6e594bc3314a" Description="The root in which all other elements are embedded. Appears as a diagram." Name="BizTalkMessagingModel" DisplayName="Biz Talk Messaging Model" Namespace="BizTalkMessaging">
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Notes>Creates an embedding link when an element is dropped onto a model. </Notes>
          <Index>
            <DomainClassMoniker Name="Application" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>BizTalkMessagingModelHasApplications.Applications</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="Broker" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>BizTalkMessagingModelHasBroker.Broker</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="d51113bf-1407-4c1f-b86b-a1572f2e261c" Description="Elements embedded in the model. Appear as boxes on the diagram." Name="Application" DisplayName="Application" Namespace="BizTalkMessaging">
      <Properties>
        <DomainProperty Id="a5ee1259-d9a4-4d23-91dc-15630a3f9382" Description="Description for BizTalkMessaging.Application.Name" Name="Name" DisplayName="Name" DefaultValue="" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="34613c4f-c5a8-4f81-9a1e-31deafccaf8e" Description="Description for BizTalkMessaging.OutPort" Name="OutPort" DisplayName="Out Port" Namespace="BizTalkMessaging" />
    <DomainClass Id="bddfa505-4aff-480a-baa8-1158fb0bab99" Description="Description for BizTalkMessaging.InPort" Name="InPort" DisplayName="In Port" Namespace="BizTalkMessaging">
      <Properties>
        <DomainProperty Id="1ebcc362-a35b-415d-ae91-06192edf99da" Description="Description for BizTalkMessaging.InPort.Message" Name="Message" DisplayName="Message">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="9fa17717-816f-49f0-a011-09b6d97fb296" Description="Description for BizTalkMessaging.InPort.Protocol" Name="Protocol" DisplayName="Protocol">
          <Type>
            <DomainEnumerationMoniker Name="Protocol" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="12e243b5-d163-4c4e-ba64-2b735359c505" Description="Description for BizTalkMessaging.Broker" Name="Broker" DisplayName="Broker" Namespace="BizTalkMessaging">
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="OutPort" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>BrokerHasOutPorts.OutPorts</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="InPort" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>BrokerHasInPorts.InPorts</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
  </Classes>
  <Relationships>
    <DomainRelationship Id="8546cb57-1d7c-4054-988f-523bb89a0ae0" Description="Embedding relationship between the Model and Elements" Name="BizTalkMessagingModelHasApplications" DisplayName="Biz Talk Messaging Model Has Applications" Namespace="BizTalkMessaging" IsEmbedding="true">
      <Source>
        <DomainRole Id="00cc3acf-20d4-4b2b-af1b-bf1109e03859" Description="" Name="BizTalkMessagingModel" DisplayName="Biz Talk Messaging Model" PropertyName="Applications" PropertyDisplayName="Applications">
          <RolePlayer>
            <DomainClassMoniker Name="BizTalkMessagingModel" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="abdd80f8-e853-49cf-8dc5-0f874efd5bac" Description="" Name="Element" DisplayName="Element" PropertyName="BizTalkMessagingModel" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Biz Talk Messaging Model">
          <RolePlayer>
            <DomainClassMoniker Name="Application" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="da28b31a-5326-472e-a895-a8cc35b200cc" Description="Description for BizTalkMessaging.BizTalkMessagingModelHasBroker" Name="BizTalkMessagingModelHasBroker" DisplayName="Biz Talk Messaging Model Has Broker" Namespace="BizTalkMessaging" IsEmbedding="true">
      <Source>
        <DomainRole Id="b0a74542-1d40-45de-8218-2930eae783a7" Description="Description for BizTalkMessaging.BizTalkMessagingModelHasBroker.BizTalkMessagingModel" Name="BizTalkMessagingModel" DisplayName="Biz Talk Messaging Model" PropertyName="Broker" Multiplicity="One" PropertyDisplayName="Broker">
          <RolePlayer>
            <DomainClassMoniker Name="BizTalkMessagingModel" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="c0bfa2c6-1199-4fb6-8585-0218a6739014" Description="Description for BizTalkMessaging.BizTalkMessagingModelHasBroker.Broker" Name="Broker" DisplayName="Broker" PropertyName="BizTalkMessagingModel" Multiplicity="One" PropagatesDelete="true" PropagatesCopy="true" PropertyDisplayName="Biz Talk Messaging Model">
          <RolePlayer>
            <DomainClassMoniker Name="Broker" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="237c052b-ae8c-49be-b688-136b723eafe7" Description="Description for BizTalkMessaging.BrokerHasOutPorts" Name="BrokerHasOutPorts" DisplayName="Broker Has Out Ports" Namespace="BizTalkMessaging" IsEmbedding="true">
      <Source>
        <DomainRole Id="cd293d25-d2e3-4a21-8392-e89ef78c27e7" Description="Description for BizTalkMessaging.BrokerHasOutPorts.Broker" Name="Broker" DisplayName="Broker" PropertyName="OutPorts" PropertyDisplayName="Out Ports">
          <RolePlayer>
            <DomainClassMoniker Name="Broker" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="ee6cf026-d0a9-4074-9dce-94c7658bc5ae" Description="Description for BizTalkMessaging.BrokerHasOutPorts.OutPort" Name="OutPort" DisplayName="Out Port" PropertyName="Broker" Multiplicity="One" PropagatesDelete="true" PropagatesCopy="true" PropertyDisplayName="Broker">
          <RolePlayer>
            <DomainClassMoniker Name="OutPort" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="c262802d-4e7a-4ac9-a8d4-0ba309f81e52" Description="Description for BizTalkMessaging.BrokerHasInPorts" Name="BrokerHasInPorts" DisplayName="Broker Has In Ports" Namespace="BizTalkMessaging" IsEmbedding="true">
      <Source>
        <DomainRole Id="eb575488-03d6-4085-8df7-a8ca8e82eb47" Description="Description for BizTalkMessaging.BrokerHasInPorts.Broker" Name="Broker" DisplayName="Broker" PropertyName="InPorts" PropertyDisplayName="In Ports">
          <RolePlayer>
            <DomainClassMoniker Name="Broker" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="ba751a07-5d37-4daa-a3d0-a2df6d6a9903" Description="Description for BizTalkMessaging.BrokerHasInPorts.InPort" Name="InPort" DisplayName="In Port" PropertyName="Broker" Multiplicity="One" PropagatesDelete="true" PropagatesCopy="true" PropertyDisplayName="Broker">
          <RolePlayer>
            <DomainClassMoniker Name="InPort" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="64fd4b60-ba48-48e8-a50d-1a748680102e" Description="Description for BizTalkMessaging.ApplicationSendsToInPort" Name="ApplicationSendsToInPort" DisplayName="Application Sends To In Port" Namespace="BizTalkMessaging">
      <Source>
        <DomainRole Id="8b2b64c8-b2f7-4d0d-991e-67fc5e602dd8" Description="Description for BizTalkMessaging.ApplicationSendsToInPort.Application" Name="Application" DisplayName="Application" PropertyName="InPort" Multiplicity="ZeroOne" PropertyDisplayName="In Port">
          <RolePlayer>
            <DomainClassMoniker Name="Application" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="cc71bb38-3dcf-42fe-937b-1b43730b1f85" Description="Description for BizTalkMessaging.ApplicationSendsToInPort.InPort" Name="InPort" DisplayName="In Port" PropertyName="Application" Multiplicity="ZeroOne" PropertyDisplayName="Application">
          <RolePlayer>
            <DomainClassMoniker Name="InPort" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="a46621df-0465-4def-bdc3-49a8cd936a9a" Description="Description for BizTalkMessaging.OutPortSendsToApplication" Name="OutPortSendsToApplication" DisplayName="Out Port Sends To Application" Namespace="BizTalkMessaging">
      <Source>
        <DomainRole Id="ea29f1e7-0027-4295-81ad-0347a7b56146" Description="Description for BizTalkMessaging.OutPortSendsToApplication.OutPort" Name="OutPort" DisplayName="Out Port" PropertyName="Applications" Multiplicity="OneMany" PropertyDisplayName="Applications">
          <RolePlayer>
            <DomainClassMoniker Name="OutPort" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="23d9d657-d719-4ed3-ae7a-9196a5bd4f24" Description="Description for BizTalkMessaging.OutPortSendsToApplication.Application" Name="Application" DisplayName="Application" PropertyName="OutPorts" Multiplicity="OneMany" PropertyDisplayName="Out Ports">
          <RolePlayer>
            <DomainClassMoniker Name="Application" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
  </Relationships>
  <Types>
    <ExternalType Name="DateTime" Namespace="System" />
    <ExternalType Name="String" Namespace="System" />
    <ExternalType Name="Int16" Namespace="System" />
    <ExternalType Name="Int32" Namespace="System" />
    <ExternalType Name="Int64" Namespace="System" />
    <ExternalType Name="UInt16" Namespace="System" />
    <ExternalType Name="UInt32" Namespace="System" />
    <ExternalType Name="UInt64" Namespace="System" />
    <ExternalType Name="SByte" Namespace="System" />
    <ExternalType Name="Byte" Namespace="System" />
    <ExternalType Name="Double" Namespace="System" />
    <ExternalType Name="Single" Namespace="System" />
    <ExternalType Name="Guid" Namespace="System" />
    <ExternalType Name="Boolean" Namespace="System" />
    <ExternalType Name="Char" Namespace="System" />
    <DomainEnumeration Name="Protocol" Namespace="BizTalkMessaging" Description="Description for BizTalkMessaging.Protocol">
      <Literals>
        <EnumerationLiteral Description="Description for BizTalkMessaging.Protocol.FILE" Name="FILE" Value="" />
        <EnumerationLiteral Description="Description for BizTalkMessaging.Protocol.HTTP" Name="HTTP" Value="" />
      </Literals>
    </DomainEnumeration>
  </Types>
  <Shapes>
    <GeometryShape Id="9cb66ebd-0fee-446b-96bb-6bd1137d1ed6" Description="Shape used to represent Application on a Diagram." Name="ApplicationShape" DisplayName="Application" Namespace="BizTalkMessaging" FixedTooltipText="Application" FillColor="242, 239, 229" OutlineColor="113, 111, 110" InitialHeight="0.75" OutlineThickness="0.01" Geometry="Rectangle">
      <Notes>The shape has a text decorator used to display the Name property of the mapped Application</Notes>
      <ShapeHasDecorators Position="Center" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="NameDecorator" DisplayName="Name Decorator" DefaultText="NameDecorator" />
      </ShapeHasDecorators>
    </GeometryShape>
    <Port Id="e92ae884-3946-4bed-8aa5-07858b9d6d15" Description="Description for BizTalkMessaging.OutPortShape" Name="OutPortShape" DisplayName="Out Port Shape" Namespace="BizTalkMessaging" FixedTooltipText="Out Port Shape" InitialWidth="0.2" InitialHeight="0.2" OutlineThickness="0.01" Geometry="Rectangle">
      <ShapeHasDecorators Position="Center" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="Image" DisplayName="Image" DefaultIcon="Resources\OutPortImage.bmp" />
      </ShapeHasDecorators>
    </Port>
    <Port Id="5dfb2fe5-c017-4758-a580-c4b9c5ff8c5f" Description="Description for BizTalkMessaging.InPortShape" Name="InPortShape" DisplayName="In Port Shape" Namespace="BizTalkMessaging" FixedTooltipText="In Port Shape" InitialWidth="0.2" InitialHeight="0.2" OutlineThickness="0.01" Geometry="Rectangle">
      <ShapeHasDecorators Position="Center" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="Image" DisplayName="Image" DefaultIcon="Resources\InPortImage.bmp" />
      </ShapeHasDecorators>
    </Port>
    <GeometryShape Id="0b514da7-baa8-4b4b-9f7d-6bdd6ba6e192" Description="Description for BizTalkMessaging.BrokerShape" Name="BrokerShape" DisplayName="Broker Shape" Namespace="BizTalkMessaging" FixedTooltipText="Broker Shape" InitialWidth="2" InitialHeight="2" OutlineThickness="0.01" FillGradientMode="None" Geometry="Rectangle">
      <ShapeHasDecorators Position="Center" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="NameDecorator" DisplayName="Name Decorator" DefaultText="BizTalk Server" />
      </ShapeHasDecorators>
    </GeometryShape>
  </Shapes>
  <Connectors>
    <Connector Id="b7e3da0a-67db-4aa7-b26d-62c453b70cd4" Description="Description for BizTalkMessaging.ConnectionApplication_InPort" Name="ConnectionApplication_InPort" DisplayName="Connection Application_ In Port" Namespace="BizTalkMessaging" FixedTooltipText="Connection Application_ In Port" Color="Blue" TargetEndStyle="FilledArrow" Thickness="0.01" />
    <Connector Id="c944d4fa-ddae-4f4c-943c-444d4bd79519" Description="Description for BizTalkMessaging.ConnectionOutPort_Application" Name="ConnectionOutPort_Application" DisplayName="Connection Out Port_ Application" Namespace="BizTalkMessaging" FixedTooltipText="Connection Out Port_ Application" Color="Red" TargetEndStyle="FilledArrow" Thickness="0.01" />
  </Connectors>
  <XmlSerializationBehavior Name="BizTalkMessagingSerializationBehavior" Namespace="BizTalkMessaging">
    <ClassData>
      <XmlClassData TypeName="BizTalkMessagingModel" MonikerAttributeName="" SerializeId="true" MonikerElementName="bizTalkMessagingModelMoniker" ElementName="bizTalkMessagingModel" MonikerTypeName="BizTalkMessagingModelMoniker">
        <DomainClassMoniker Name="BizTalkMessagingModel" />
        <ElementData>
          <XmlRelationshipData RoleElementName="applications">
            <DomainRelationshipMoniker Name="BizTalkMessagingModelHasApplications" />
          </XmlRelationshipData>
          <XmlRelationshipData RoleElementName="broker">
            <DomainRelationshipMoniker Name="BizTalkMessagingModelHasBroker" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="Application" MonikerAttributeName="name" MonikerElementName="applicationMoniker" ElementName="application" MonikerTypeName="ApplicationMoniker">
        <DomainClassMoniker Name="Application" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="Application/Name" />
          </XmlPropertyData>
          <XmlRelationshipData RoleElementName="inPort">
            <DomainRelationshipMoniker Name="ApplicationSendsToInPort" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="BizTalkMessagingModelHasApplications" MonikerAttributeName="" MonikerElementName="bizTalkMessagingModelHasApplicationsMoniker" ElementName="bizTalkMessagingModelHasApplications" MonikerTypeName="BizTalkMessagingModelHasApplicationsMoniker">
        <DomainRelationshipMoniker Name="BizTalkMessagingModelHasApplications" />
      </XmlClassData>
      <XmlClassData TypeName="ApplicationShape" MonikerAttributeName="" MonikerElementName="applicationShapeMoniker" ElementName="applicationShape" MonikerTypeName="ApplicationShapeMoniker">
        <GeometryShapeMoniker Name="ApplicationShape" />
      </XmlClassData>
      <XmlClassData TypeName="BizTalkMessagingDiagram" MonikerAttributeName="" MonikerElementName="minimalLanguageDiagramMoniker" ElementName="minimalLanguageDiagram" MonikerTypeName="BizTalkMessagingDiagramMoniker">
        <DiagramMoniker Name="BizTalkMessagingDiagram" />
      </XmlClassData>
      <XmlClassData TypeName="OutPort" MonikerAttributeName="" SerializeId="true" MonikerElementName="outPortMoniker" ElementName="outPort" MonikerTypeName="OutPortMoniker">
        <DomainClassMoniker Name="OutPort" />
        <ElementData>
          <XmlRelationshipData RoleElementName="applications">
            <DomainRelationshipMoniker Name="OutPortSendsToApplication" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="InPort" MonikerAttributeName="" SerializeId="true" MonikerElementName="inPortMoniker" ElementName="inPort" MonikerTypeName="InPortMoniker">
        <DomainClassMoniker Name="InPort" />
        <ElementData>
          <XmlPropertyData XmlName="message">
            <DomainPropertyMoniker Name="InPort/Message" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="protocol">
            <DomainPropertyMoniker Name="InPort/Protocol" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="OutPortShape" MonikerAttributeName="" MonikerElementName="outPortShapeMoniker" ElementName="outPortShape" MonikerTypeName="OutPortShapeMoniker">
        <PortMoniker Name="OutPortShape" />
      </XmlClassData>
      <XmlClassData TypeName="Broker" MonikerAttributeName="" SerializeId="true" MonikerElementName="brokerMoniker" ElementName="broker" MonikerTypeName="BrokerMoniker">
        <DomainClassMoniker Name="Broker" />
        <ElementData>
          <XmlRelationshipData RoleElementName="outPorts">
            <DomainRelationshipMoniker Name="BrokerHasOutPorts" />
          </XmlRelationshipData>
          <XmlRelationshipData RoleElementName="inPorts">
            <DomainRelationshipMoniker Name="BrokerHasInPorts" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="InPortShape" MonikerAttributeName="" MonikerElementName="inPortShapeMoniker" ElementName="inPortShape" MonikerTypeName="InPortShapeMoniker">
        <PortMoniker Name="InPortShape" />
      </XmlClassData>
      <XmlClassData TypeName="BrokerShape" MonikerAttributeName="" MonikerElementName="brokerShapeMoniker" ElementName="brokerShape" MonikerTypeName="BrokerShapeMoniker">
        <GeometryShapeMoniker Name="BrokerShape" />
      </XmlClassData>
      <XmlClassData TypeName="ConnectionApplication_InPort" MonikerAttributeName="" MonikerElementName="connectionApplication_InPortMoniker" ElementName="connectionApplication_InPort" MonikerTypeName="ConnectionApplication_InPortMoniker">
        <ConnectorMoniker Name="ConnectionApplication_InPort" />
      </XmlClassData>
      <XmlClassData TypeName="BizTalkMessagingModelHasBroker" MonikerAttributeName="" MonikerElementName="bizTalkMessagingModelHasBrokerMoniker" ElementName="bizTalkMessagingModelHasBroker" MonikerTypeName="BizTalkMessagingModelHasBrokerMoniker">
        <DomainRelationshipMoniker Name="BizTalkMessagingModelHasBroker" />
      </XmlClassData>
      <XmlClassData TypeName="BrokerHasOutPorts" MonikerAttributeName="" MonikerElementName="brokerHasOutPortsMoniker" ElementName="brokerHasOutPorts" MonikerTypeName="BrokerHasOutPortsMoniker">
        <DomainRelationshipMoniker Name="BrokerHasOutPorts" />
      </XmlClassData>
      <XmlClassData TypeName="BrokerHasInPorts" MonikerAttributeName="" MonikerElementName="brokerHasInPortsMoniker" ElementName="brokerHasInPorts" MonikerTypeName="BrokerHasInPortsMoniker">
        <DomainRelationshipMoniker Name="BrokerHasInPorts" />
      </XmlClassData>
      <XmlClassData TypeName="ApplicationSendsToInPort" MonikerAttributeName="" MonikerElementName="applicationSendsToInPortMoniker" ElementName="applicationSendsToInPort" MonikerTypeName="ApplicationSendsToInPortMoniker">
        <DomainRelationshipMoniker Name="ApplicationSendsToInPort" />
      </XmlClassData>
      <XmlClassData TypeName="OutPortSendsToApplication" MonikerAttributeName="" MonikerElementName="outPortSendsToApplicationMoniker" ElementName="outPortSendsToApplication" MonikerTypeName="OutPortSendsToApplicationMoniker">
        <DomainRelationshipMoniker Name="OutPortSendsToApplication" />
      </XmlClassData>
      <XmlClassData TypeName="ConnectionOutPort_Application" MonikerAttributeName="" MonikerElementName="connectionOutPort_ApplicationMoniker" ElementName="connectionOutPort_Application" MonikerTypeName="ConnectionOutPort_ApplicationMoniker">
        <ConnectorMoniker Name="ConnectionOutPort_Application" />
      </XmlClassData>
    </ClassData>
  </XmlSerializationBehavior>
  <ExplorerBehavior Name="BizTalkMessagingExplorer" />
  <ConnectionBuilders>
    <ConnectionBuilder Name="ApplicationSendsToInPortBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="ApplicationSendsToInPort" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="Application" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="InPort" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="OutPortSendsToApplicationBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="OutPortSendsToApplication" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="OutPort" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="Application" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
  </ConnectionBuilders>
  <Diagram Id="454020ff-a44a-4a71-9787-eeb7611639c0" Description="Description for BizTalkMessaging.BizTalkMessagingDiagram" Name="BizTalkMessagingDiagram" DisplayName="Minimal Language Diagram" Namespace="BizTalkMessaging">
    <Class>
      <DomainClassMoniker Name="BizTalkMessagingModel" />
    </Class>
    <ShapeMaps>
      <ShapeMap>
        <DomainClassMoniker Name="Application" />
        <ParentElementPath>
          <DomainPath>BizTalkMessagingModelHasApplications.BizTalkMessagingModel/!BizTalkMessagingModel</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="ApplicationShape/NameDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="Application/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <GeometryShapeMoniker Name="ApplicationShape" />
      </ShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="Broker" />
        <ParentElementPath>
          <DomainPath>BizTalkMessagingModelHasBroker.BizTalkMessagingModel/!BizTalkMessagingModel</DomainPath>
        </ParentElementPath>
        <GeometryShapeMoniker Name="BrokerShape" />
      </ShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="InPort" />
        <ParentElementPath>
          <DomainPath>BrokerHasInPorts.Broker/!Broker</DomainPath>
        </ParentElementPath>
        <PortMoniker Name="InPortShape" />
      </ShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="OutPort" />
        <ParentElementPath>
          <DomainPath>BrokerHasOutPorts.Broker/!Broker</DomainPath>
        </ParentElementPath>
        <PortMoniker Name="OutPortShape" />
      </ShapeMap>
    </ShapeMaps>
    <ConnectorMaps>
      <ConnectorMap>
        <ConnectorMoniker Name="ConnectionApplication_InPort" />
        <DomainRelationshipMoniker Name="ApplicationSendsToInPort" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="ConnectionOutPort_Application" />
        <DomainRelationshipMoniker Name="OutPortSendsToApplication" />
      </ConnectorMap>
    </ConnectorMaps>
  </Diagram>
  <Designer FileExtension="btsmsgdsl" EditorGuid="85b44b4b-4011-447b-a0e6-56a673c497e2">
    <RootClass>
      <DomainClassMoniker Name="BizTalkMessagingModel" />
    </RootClass>
    <XmlSerializationDefinition CustomPostLoad="false">
      <XmlSerializationBehaviorMoniker Name="BizTalkMessagingSerializationBehavior" />
    </XmlSerializationDefinition>
    <ToolboxTab TabText="BizTalkMessaging">
      <ElementTool Name="Broker" ToolboxIcon="Resources\Broker.bmp" Caption="Broker" Tooltip="Broker" HelpKeyword="Broker">
        <DomainClassMoniker Name="Broker" />
      </ElementTool>
      <ElementTool Name="InPort" ToolboxIcon="Resources\InPortTool.bmp" Caption="InPort" Tooltip="In Port" HelpKeyword="InPort">
        <DomainClassMoniker Name="InPort" />
      </ElementTool>
      <ElementTool Name="Outport" ToolboxIcon="Resources\OutPortTool.bmp" Caption="Outport" Tooltip="Outport" HelpKeyword="Outport">
        <DomainClassMoniker Name="OutPort" />
      </ElementTool>
      <ElementTool Name="Application" ToolboxIcon="Resources\Application.bmp" Caption="Application" Tooltip="Create an ExampleElement" HelpKeyword="CreateExampleClassF1Keyword">
        <DomainClassMoniker Name="Application" />
      </ElementTool>
      <ConnectionTool Name="Application_To_Broker_Connector" ToolboxIcon="Resources\ConnectionToolBlue.bmp" Caption="ApplicationToBrokerConnector" Tooltip="Application_ To_ Broker_ Connector" HelpKeyword="ApplicationToBrokerConnector">
        <ConnectionBuilderMoniker Name="BizTalkMessaging/ApplicationSendsToInPortBuilder" />
      </ConnectionTool>
      <ConnectionTool Name="BrokerToApplicationConnector" ToolboxIcon="Resources\ConnectionToolRed.bmp" Caption="BrokerToApplicationConnector" Tooltip="Broker To Application Connector" HelpKeyword="BrokerToApplicationConnector">
        <ConnectionBuilderMoniker Name="BizTalkMessaging/OutPortSendsToApplicationBuilder" />
      </ConnectionTool>
    </ToolboxTab>
    <Validation UsesMenu="false" UsesOpen="false" UsesSave="false" UsesLoad="false" />
    <DiagramMoniker Name="BizTalkMessagingDiagram" />
  </Designer>
  <Explorer ExplorerGuid="4141f16f-2bd8-41ea-86de-0991c2ed0354" Title="BizTalkMessaging Explorer">
    <ExplorerBehaviorMoniker Name="BizTalkMessaging/BizTalkMessagingExplorer" />
  </Explorer>
</Dsl>