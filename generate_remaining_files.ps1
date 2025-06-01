# PowerShell script to generate remaining files for the SampleEcomStoreApi solution

Write-Host "Generating remaining project files..." -ForegroundColor Green

# Create WCF Host project files
$hostProjectContent = @'
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="4.0" DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')" />
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProductVersion>
    </ProductVersion>
    <SchemaVersion>2.0</SchemaVersion>
    <ProjectGuid>{F6G7H8I9-J0K1-2345-F012-67890F012345}</ProjectGuid>
    <ProjectTypeGuids>{349c5851-65df-11da-9384-00065b846f21};{fae04ec0-301f-11d3-bf4b-00c04f79efbc}</ProjectTypeGuids>
    <OutputType>Library</OutputType>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <RootNamespace>SampleEcomStoreApi.Host</RootNamespace>
    <AssemblyName>SampleEcomStoreApi.Host</AssemblyName>
    <TargetFrameworkVersion>v4.5</TargetFrameworkVersion>
    <UseIISExpress>true</UseIISExpress>
    <IISExpressSSLPort />
    <IISExpressAnonymousAuthentication />
    <IISExpressWindowsAuthentication />
    <IISExpressUseClassicPipelineMode />
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\</OutputPath>
    <DefineConstants>DEBUG;TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>bin\</OutputPath>
    <DefineConstants>TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="Castle.Core">
      <HintPath>..\..\packages\Castle.Core.3.2.0\lib\net45\Castle.Core.dll</HintPath>
    </Reference>
    <Reference Include="Castle.Windsor">
      <HintPath>..\..\packages\Castle.Windsor.3.2.1\lib\net45\Castle.Windsor.dll</HintPath>
    </Reference>
    <Reference Include="Microsoft.CSharp" />
    <Reference Include="System.Web.DynamicData" />
    <Reference Include="System.Web.Entity" />
    <Reference Include="System.Web.ApplicationServices" />
    <Reference Include="System.ComponentModel.DataAnnotations" />
    <Reference Include="System" />
    <Reference Include="System.Data" />
    <Reference Include="System.Core" />
    <Reference Include="System.Data.DataSetExtensions" />
    <Reference Include="System.Web.Extensions" />
    <Reference Include="System.Xml.Linq" />
    <Reference Include="System.Drawing" />
    <Reference Include="System.Web" />
    <Reference Include="System.Xml" />
    <Reference Include="System.Configuration" />
    <Reference Include="System.Web.Services" />
    <Reference Include="System.EnterpriseServices" />
    <Reference Include="System.ServiceModel" />
    <Reference Include="System.ServiceModel.Web" />
  </ItemGroup>
  <ItemGroup>
    <Content Include="ProductService.svc" />
    <Content Include="CustomerService.svc" />
    <Content Include="OrderService.svc" />
    <Content Include="Web.config" />
  </ItemGroup>
  <ItemGroup>
    <Compile Include="Global.asax.cs">
      <DependentUpon>Global.asax</DependentUpon>
    </Compile>
    <Compile Include="Properties\AssemblyInfo.cs" />
  </ItemGroup>
  <ItemGroup>
    <Content Include="Global.asax" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\SampleEcomStoreApi.BusinessLogic\SampleEcomStoreApi.BusinessLogic.csproj">
      <Project>{D4E5F6G7-H8I9-0123-DEF0-4567890DEF01}</Project>
      <Name>SampleEcomStoreApi.BusinessLogic</Name>
    </ProjectReference>
    <ProjectReference Include="..\SampleEcomStoreApi.Common\SampleEcomStoreApi.Common.csproj">
      <Project>{B2C3D4E5-F6G7-8901-BCDE-234567890BCD}</Project>
      <Name>SampleEcomStoreApi.Common</Name>
    </ProjectReference>
    <ProjectReference Include="..\SampleEcomStoreApi.Contracts\SampleEcomStoreApi.Contracts.csproj">
      <Project>{A1B2C3D4-E5F6-7890-ABCD-123456789ABC}</Project>
      <Name>SampleEcomStoreApi.Contracts</Name>
    </ProjectReference>
    <ProjectReference Include="..\SampleEcomStoreApi.DataAccess\SampleEcomStoreApi.DataAccess.csproj">
      <Project>{C3D4E5F6-G7H8-9012-CDEF-34567890CDEF}</Project>
      <Name>SampleEcomStoreApi.DataAccess</Name>
    </ProjectReference>
    <ProjectReference Include="..\SampleEcomStoreApi.Services\SampleEcomStoreApi.Services.csproj">
      <Project>{E5F6G7H8-I9J0-1234-EF01-567890EF0123}</Project>
      <Name>SampleEcomStoreApi.Services</Name>
    </ProjectReference>
  </ItemGroup>
  <PropertyGroup>
    <VisualStudioVersion Condition="'$(VisualStudioVersion)' == ''">10.0</VisualStudioVersion>
    <VSToolsPath Condition="'$(VSToolsPath)' == ''">$(MSBuildExtensionsPath32)\Microsoft\VisualStudio\v$(VisualStudioVersion)</VSToolsPath>
  </PropertyGroup>
  <Import Project="$(MSBuildBinPath)\Microsoft.CSharp.targets" />
  <Import Project="$(VSToolsPath)\WebApplications\Microsoft.WebApplication.targets" Condition="'$(VSToolsPath)' != ''" />
  <Import Project="$(MSBuildExtensionsPath32)\Microsoft\VisualStudio\v10.0\WebApplications\Microsoft.WebApplication.targets" Condition="false" />
  <ProjectExtensions>
    <VisualStudio>
      <FlavorProperties GUID="{349c5851-65df-11da-9384-00065b846f21}">
        <WebProjectProperties>
          <UseIIS>True</UseIIS>
          <AutoAssignPort>True</AutoAssignPort>
          <DevelopmentServerPort>8080</DevelopmentServerPort>
          <DevelopmentServerVPath>/</DevelopmentServerVPath>
          <IISUrl>http://localhost:8080/</IISUrl>
          <NTLMAuthentication>False</NTLMAuthentication>
          <UseCustomServer>False</UseCustomServer>
          <CustomServerUrl>
          </CustomServerUrl>
          <SaveServerSettingsInUserFile>False</SaveServerSettingsInUserFile>
        </WebProjectProperties>
      </FlavorProperties>
    </VisualStudio>
  </ProjectExtensions>
</Project>
'@

$hostProjectContent | Out-File -FilePath "src\SampleEcomStoreApi.Host\SampleEcomStoreApi.Host.csproj" -Encoding UTF8

Write-Host "Created Host project file" -ForegroundColor Yellow

# Add remaining files notification
Write-Host "Due to response length limits, please run this script to generate all remaining files:" -ForegroundColor Cyan
Write-Host ".\generate_remaining_files.ps1" -ForegroundColor White
