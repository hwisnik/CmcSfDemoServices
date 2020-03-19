USE [master]
GO
/****** Object:  Database [CMC-SFDC_TEST]    Script Date: 11/8/2019 4:04:50 PM ******/
CREATE DATABASE [CMC-SFDC_TEST]
 CONTAINMENT = NONE
GO
ALTER DATABASE [CMC-SFDC_TEST] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CMC-SFDC_TEST].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CMC-SFDC_TEST] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET ARITHABORT OFF 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET RECOVERY FULL 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET  MULTI_USER 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CMC-SFDC_TEST] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CMC-SFDC_TEST] SET QUERY_STORE = OFF
GO
USE [CMC-SFDC_TEST]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [CMC-SFDC_TEST]
GO
/****** Object:  User [MAIN\SalesforceSvcDev]    Script Date: 11/8/2019 4:04:50 PM ******/
CREATE USER [MAIN\SalesforceSvcDev] FOR LOGIN [MAIN\SALESFORCESvcDev] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [MAIN\SalesforceExtSvcDev3]    Script Date: 11/8/2019 4:04:50 PM ******/
CREATE USER [MAIN\SalesforceExtSvcDev3] FOR LOGIN [MAIN\SalesforceExtSvcDev3] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [MAIN\cmcutility]    Script Date: 11/8/2019 4:04:50 PM ******/
CREATE USER [MAIN\cmcutility] FOR LOGIN [MAIN\cmcutility] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [CometDService]    Script Date: 11/8/2019 4:04:50 PM ******/
CREATE USER [CometDService] FOR LOGIN [CometDService] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [CMCSFWebAccess]    Script Date: 11/8/2019 4:04:50 PM ******/
CREATE USER [CMCSFWebAccess] FOR LOGIN [CMCSFWebAccess] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [MAIN\SalesforceSvcDev]
GO
ALTER ROLE [db_owner] ADD MEMBER [MAIN\SalesforceExtSvcDev3]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [MAIN\SalesforceExtSvcDev3]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [MAIN\SalesforceExtSvcDev3]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [MAIN\SalesforceExtSvcDev3]
GO
ALTER ROLE [db_datareader] ADD MEMBER [MAIN\SalesforceExtSvcDev3]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [MAIN\SalesforceExtSvcDev3]
GO
ALTER ROLE [db_denydatareader] ADD MEMBER [MAIN\SalesforceExtSvcDev3]
GO
ALTER ROLE [db_denydatawriter] ADD MEMBER [MAIN\SalesforceExtSvcDev3]
GO
ALTER ROLE [db_owner] ADD MEMBER [CometDService]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [CometDService]
GO
ALTER ROLE [db_datareader] ADD MEMBER [CometDService]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [CometDService]
GO
ALTER ROLE [db_owner] ADD MEMBER [CMCSFWebAccess]
GO
/****** Object:  Schema [Address]    Script Date: 11/8/2019 4:04:50 PM ******/
CREATE SCHEMA [Address]
GO
/****** Object:  Schema [CDCQueue]    Script Date: 11/8/2019 4:04:50 PM ******/
CREATE SCHEMA [CDCQueue]
GO
/****** Object:  Schema [Client]    Script Date: 11/8/2019 4:04:50 PM ******/
CREATE SCHEMA [Client]
GO
/****** Object:  Schema [Contact]    Script Date: 11/8/2019 4:04:50 PM ******/
CREATE SCHEMA [Contact]
GO
/****** Object:  Schema [Customer]    Script Date: 11/8/2019 4:04:50 PM ******/
CREATE SCHEMA [Customer]
GO
/****** Object:  Schema [CustomerProgram]    Script Date: 11/8/2019 4:04:50 PM ******/
CREATE SCHEMA [CustomerProgram]
GO
/****** Object:  Schema [Import]    Script Date: 11/8/2019 4:04:50 PM ******/
CREATE SCHEMA [Import]
GO
/****** Object:  Schema [Program]    Script Date: 11/8/2019 4:04:50 PM ******/
CREATE SCHEMA [Program]
GO
/****** Object:  Schema [Staging]    Script Date: 11/8/2019 4:04:50 PM ******/
CREATE SCHEMA [Staging]
GO
/****** Object:  Schema [Technician]    Script Date: 11/8/2019 4:04:50 PM ******/
CREATE SCHEMA [Technician]
GO
/****** Object:  Schema [Usage]    Script Date: 11/8/2019 4:04:50 PM ******/
CREATE SCHEMA [Usage]
GO
/****** Object:  Schema [UsageRaw]    Script Date: 11/8/2019 4:04:50 PM ******/
CREATE SCHEMA [UsageRaw]
GO
/****** Object:  Table [CDCQueue].[CDCQueue]    Script Date: 11/8/2019 4:04:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CDCQueue].[CDCQueue](
	[QueueGuid] [uniqueidentifier] NOT NULL,
	[ChannelObject] [varchar](100) NOT NULL,
	[EventTypeName] [varchar](100) NOT NULL,
	[ReplayID] [int] NOT NULL,
	[Payload] [varchar](max) NULL,
	[ChannelURL] [varchar](200) NOT NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [QueueGuid] PRIMARY KEY CLUSTERED 
(
	[QueueGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [CDCQueue].[CDCQueueArchive]    Script Date: 11/8/2019 4:04:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CDCQueue].[CDCQueueArchive](
	[CDCQueueArchiveGuid] [uniqueidentifier] NOT NULL,
	[ChannelObject] [varchar](100) NOT NULL,
	[EventTypeName] [varchar](100) NOT NULL,
	[ReplayID] [int] NOT NULL,
	[Payload] [varchar](max) NULL,
	[ChannelURL] [varchar](200) NOT NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [CDCQueueArchiveGuid] PRIMARY KEY CLUSTERED 
(
	[CDCQueueArchiveGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [CDCQueue].[Channel]    Script Date: 11/8/2019 4:04:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CDCQueue].[Channel](
	[ChannelGuid] [uniqueidentifier] NOT NULL,
	[Channel] [varchar](55) NOT NULL,
	[ChannelDescription] [varchar](500) NULL,
 CONSTRAINT [ChannelGuid] PRIMARY KEY CLUSTERED 
(
	[ChannelGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [CDCQueue].[EventType]    Script Date: 11/8/2019 4:04:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CDCQueue].[EventType](
	[EventTypeGuid] [uniqueidentifier] NOT NULL,
	[EventTypeName] [varchar](50) NOT NULL,
	[EventTypeDescription] [varchar](500) NULL,
 CONSTRAINT [EventTypeGuid] PRIMARY KEY CLUSTERED 
(
	[EventTypeGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [CDCQueue].[LoggingLastChannelQueue]    Script Date: 11/8/2019 4:04:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CDCQueue].[LoggingLastChannelQueue](
	[LLCQGuid] [uniqueidentifier] NOT NULL,
	[ReplayID] [int] NOT NULL,
	[ChannelObject] [varchar](200) NOT NULL,
	[ChannelURL] [varchar](200) NOT NULL,
 CONSTRAINT [LLCQGuid] PRIMARY KEY CLUSTERED 
(
	[LLCQGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Contact].[Contact]    Script Date: 11/8/2019 4:04:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contact].[Contact](
	[ContactGuid] [uniqueidentifier] NOT NULL,
	[CustomerGuid] [uniqueidentifier] NOT NULL,
	[SFContactId] [varchar](20) NULL,
	[ContactTypeGuid] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](150) NULL,
	[FullName] [varchar](1000) NULL,
	[InformalName] [nvarchar](50) NULL,
	[Salutation] [nvarchar](10) NULL,
	[Suffix] [nvarchar](50) NULL,
	[IsPrimaryContact] [bit] NULL,
	[IsAnyContactAllowed] [bit] NULL,
	[IsVoiceContactAllowed] [bit] NULL,
	[IsSmsContactAllowed] [bit] NULL,
	[IsAutoCallAllowed] [bit] NULL,
	[IsEmailContactAllowed] [bit] NULL,
	[TitleDescription] [varchar](500) NULL,
	[Title] [varchar](50) NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[ContactGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Contact].[Email]    Script Date: 11/8/2019 4:04:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contact].[Email](
	[EmailGuid] [uniqueidentifier] NOT NULL,
	[ContactGuid] [uniqueidentifier] NOT NULL,
	[Email_Address] [varchar](500) NULL,
	[Alternate_Email_1__c] [varchar](500) NULL,
	[Alternate_Email_2__c] [varchar](500) NULL,
	[Alternate_Email_3__c] [varchar](500) NULL,
	[Additional_Email_Data] [varchar](max) NULL,
 CONSTRAINT [PK_Email] PRIMARY KEY CLUSTERED 
(
	[EmailGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Contact].[LkpContactType]    Script Date: 11/8/2019 4:04:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contact].[LkpContactType](
	[ContactTypeGuid] [uniqueidentifier] NOT NULL,
	[ContactTypeName] [varchar](500) NULL,
	[ContactTypeDescription] [varchar](max) NULL,
 CONSTRAINT [PK_LkpContactType] PRIMARY KEY CLUSTERED 
(
	[ContactTypeGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Contact].[Phone]    Script Date: 11/8/2019 4:04:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contact].[Phone](
	[PhoneNumberGuid] [uniqueidentifier] NOT NULL,
	[ContactGuid] [uniqueidentifier] NOT NULL,
	[Phone_Number] [varchar](100) NULL,
	[OtherPhone] [varchar](100) NULL,
	[Other_Phone_1__c] [varchar](100) NULL,
	[Other_Phone_2__c] [varchar](100) NULL,
	[Other_Phone_3__c] [varchar](100) NULL,
	[AssistantPhone] [varchar](100) NULL,
	[MobilePhone] [varchar](100) NULL,
	[HomePhone] [varchar](100) NULL,
	[Fax] [varchar](100) NULL,
	[Additional_Phone_Data] [varchar](max) NULL,
 CONSTRAINT [PK_PhoneNumber] PRIMARY KEY CLUSTERED 
(
	[PhoneNumberGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Customer].[Account]    Script Date: 11/8/2019 4:04:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Customer].[Account](
	[AccountGuid] [uniqueidentifier] NOT NULL,
	[CustomerGuid] [uniqueidentifier] NOT NULL,
	[SFAccountID] [varchar](50) NULL,
	[AccountStatusGuid] [uniqueidentifier] NOT NULL,
	[JobId] [varchar](20) NULL,
 CONSTRAINT [PK_Customer_Account] PRIMARY KEY CLUSTERED 
(
	[AccountGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Customer].[Address]    Script Date: 11/8/2019 4:04:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Customer].[Address](
	[AddressGuid] [uniqueidentifier] NOT NULL,
	[PremiseGuid] [uniqueidentifier] NOT NULL,
	[AddressTypeGuid] [uniqueidentifier] NOT NULL,
	[StreetAddress1] [nvarchar](100) NOT NULL,
	[StreetAddress2] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[City] [nvarchar](50) NOT NULL,
	[State] [nvarchar](50) NOT NULL,
	[Zip] [nvarchar](10) NULL,
	[CountyGuid] [uniqueidentifier] NULL,
	[Latitude] [decimal](12, 6) NULL,
	[Longitude] [decimal](12, 6) NULL,
	[LastServiceDate] [datetime2](7) NULL,
	[SFAddressRecordID] [varchar](100) NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[AddressGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Customer].[Customer]    Script Date: 11/8/2019 4:04:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Customer].[Customer](
	[CustomerGuid] [uniqueidentifier] NOT NULL,
	[CMCCustomerID] [int] IDENTITY(1,1) NOT NULL,
	[ProgramGuid] [uniqueidentifier] NOT NULL,
	[SubProgramGuid] [uniqueidentifier] NOT NULL,
	[BillingAccountNumber] [nvarchar](50) NOT NULL,
	[CustomerAccountTypeGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[CustomerGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Customer].[Demographic]    Script Date: 11/8/2019 4:04:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Customer].[Demographic](
	[DemographicGuid] [uniqueidentifier] NOT NULL,
	[CustomerGuid] [uniqueidentifier] NOT NULL,
	[Age] [int] NULL,
	[Gender] [nvarchar](50) NULL,
 CONSTRAINT [PK_Demographic] PRIMARY KEY CLUSTERED 
(
	[DemographicGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Customer].[Lead]    Script Date: 11/8/2019 4:04:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Customer].[Lead](
	[LeadGuid] [uniqueidentifier] NOT NULL,
	[CustomerGuid] [uniqueidentifier] NOT NULL,
	[SFLeadID] [varchar](50) NULL,
	[LeadStatusGuid] [uniqueidentifier] NOT NULL,
	[LeadSource] [nvarchar](50) NULL,
	[QualifiedAuditTypeGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Lead] PRIMARY KEY CLUSTERED 
(
	[LeadGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Customer].[LkpAccountStatus]    Script Date: 11/8/2019 4:04:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Customer].[LkpAccountStatus](
	[AccountStatusGuid] [uniqueidentifier] NOT NULL,
	[AccountStatusName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Client_Account_Status] PRIMARY KEY CLUSTERED 
(
	[AccountStatusGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Customer].[LkpAccountType]    Script Date: 11/8/2019 4:04:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Customer].[LkpAccountType](
	[AccountTypeGuid] [uniqueidentifier] NOT NULL,
	[AccountTypeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_LkpAccountType] PRIMARY KEY CLUSTERED 
(
	[AccountTypeGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Customer].[LkpAddressType]    Script Date: 11/8/2019 4:04:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Customer].[LkpAddressType](
	[AddressTypeGuid] [uniqueidentifier] NOT NULL,
	[AddressTypeName] [nvarchar](100) NULL,
 CONSTRAINT [PK_LkpAddressType] PRIMARY KEY CLUSTERED 
(
	[AddressTypeGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Customer].[LkpAuditType]    Script Date: 11/8/2019 4:04:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Customer].[LkpAuditType](
	[AuditTypeGuid] [uniqueidentifier] NOT NULL,
	[ProgramGuid] [uniqueidentifier] NOT NULL,
	[AuditTypeName] [varchar](500) NOT NULL,
 CONSTRAINT [PK_LkpAuditType] PRIMARY KEY CLUSTERED 
(
	[AuditTypeGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Customer].[LkpCounties]    Script Date: 11/8/2019 4:04:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Customer].[LkpCounties](
	[CountyGuid] [uniqueidentifier] NOT NULL,
	[CountyTypeGuid] [uniqueidentifier] NOT NULL,
	[CountyCode] [varchar](50) NOT NULL,
	[CountyDescription] [varchar](100) NOT NULL,
	[ReportingCountyCode] [varchar](10) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_LkpCounties] PRIMARY KEY CLUSTERED 
(
	[CountyGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Customer].[LkpCountyType]    Script Date: 11/8/2019 4:04:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Customer].[LkpCountyType](
	[CountyTypeGuid] [uniqueidentifier] NOT NULL,
	[CountyTypeName] [varchar](50) NOT NULL,
	[CountyTypeDescription] [varchar](500) NULL,
 CONSTRAINT [PK_CountyTypeGuid] PRIMARY KEY CLUSTERED 
(
	[CountyTypeGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Customer].[LkpLeadStatus]    Script Date: 11/8/2019 4:04:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Customer].[LkpLeadStatus](
	[LeadStatusGuid] [uniqueidentifier] NOT NULL,
	[LeadStatusName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_LeadStatus] PRIMARY KEY CLUSTERED 
(
	[LeadStatusGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Customer].[LkpOwnerType]    Script Date: 11/8/2019 4:04:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Customer].[LkpOwnerType](
	[OwnerTypeGuid] [uniqueidentifier] NOT NULL,
	[OwnerName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_OwnerType] PRIMARY KEY CLUSTERED 
(
	[OwnerTypeGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Customer].[LkpPremiseType]    Script Date: 11/8/2019 4:04:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Customer].[LkpPremiseType](
	[PremiseTypeGuid] [uniqueidentifier] NOT NULL,
	[PremiseTypeName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_LkpPremiseType] PRIMARY KEY CLUSTERED 
(
	[PremiseTypeGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Customer].[Premise]    Script Date: 11/8/2019 4:04:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Customer].[Premise](
	[PremiseGuid] [uniqueidentifier] NOT NULL,
	[PremiseTypeGuid] [uniqueidentifier] NOT NULL,
	[IsOwnedByPrimary] [bit] NOT NULL,
	[SFPremiseId] [varchar](25) NULL,
	[LandLordConsentObtained] [bit] NULL,
	[CustomerGuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Premise] PRIMARY KEY CLUSTERED 
(
	[PremiseGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Customer].[Usage]    Script Date: 11/8/2019 4:04:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Customer].[Usage](
	[UsageGuid] [uniqueidentifier] NOT NULL,
	[CustomerGuid] [uniqueidentifier] NOT NULL,
	[Rate] [nvarchar](3) NOT NULL,
	[AverageUsage] [decimal](10, 2) NULL,
	[UsageDays] [int] NULL,
	[HeatingAverageUsage] [decimal](10, 2) NULL,
	[HeatingUsageDays] [int] NULL,
 CONSTRAINT [PK_Usage_AccountGuid] PRIMARY KEY CLUSTERED 
(
	[UsageGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [CustomerProgram].[LkpAuditQualification]    Script Date: 11/8/2019 4:04:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CustomerProgram].[LkpAuditQualification](
	[QualifyLeadCriteriaID] [int] NOT NULL,
	[EvaluateSequence] [int] NOT NULL,
	[PrimaryBaseRate] [varchar](20) NOT NULL,
	[SecondaryBaseRate] [varchar](20) NULL,
	[QualifyUsageTypeId] [int] NOT NULL,
	[QualifyingCategory] [varchar](50) NOT NULL,
	[CAP] [bit] NOT NULL,
	[MinimumMonthlyAvgUsage] [int] NOT NULL,
	[MinimumDaysUsage] [int] NOT NULL,
	[MaximumDaysUsage] [int] NOT NULL,
	[BaseloadPeriodDays] [int] NULL,
	[HeatingPeriodStart] [datetime2](0) NULL,
	[HeatingPeriodEnd] [datetime2](0) NULL,
 CONSTRAINT [PK_tlkpQualifyLeadCriteriaGuid] PRIMARY KEY NONCLUSTERED 
(
	[QualifyLeadCriteriaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [CustomerProgram].[PecoCustomer]    Script Date: 11/8/2019 4:04:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CustomerProgram].[PecoCustomer](
	[CustomerGuid] [uniqueidentifier] NOT NULL,
	[PecoCustomerId] [nvarchar](9) NOT NULL,
	[SwitchA] [char](1) NOT NULL,
	[SwitchB] [char](1) NOT NULL,
	[CapStatusAtEnrollment] [bit] NOT NULL,
	[IncomeLevel] [int] NOT NULL,
	[SyncWithPeco] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 11/8/2019 4:04:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[AddressGuid] [uniqueidentifier] NOT NULL,
	[StreetAddress] [nvarchar](50) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[State] [nvarchar](50) NOT NULL,
	[Zip] [nvarchar](10) NULL,
	[Latitude] [float] NULL,
	[Longitude] [float] NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[AddressGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Import].[ExternalFileImportLog]    Script Date: 11/8/2019 4:04:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Import].[ExternalFileImportLog](
	[ProgramId] [uniqueidentifier] NOT NULL,
	[SourceName] [varchar](255) NOT NULL,
	[FileName] [varchar](255) NOT NULL,
	[ImportedDate] [datetime] NOT NULL,
	[ImportedBy] [varchar](255) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Import].[PecoImportLog]    Script Date: 11/8/2019 4:04:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Import].[PecoImportLog](
	[FolderName] [nvarchar](50) NOT NULL,
	[ImportedDate] [datetime] NOT NULL,
	[ImportedBy] [nvarchar](255) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Program].[Program]    Script Date: 11/8/2019 4:04:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Program].[Program](
	[ProgramGuid] [uniqueidentifier] NOT NULL,
	[ProgramName] [nvarchar](250) NOT NULL,
	[SfProgramId] [nvarchar](20) NULL,
 CONSTRAINT [PK_Program] PRIMARY KEY CLUSTERED 
(
	[ProgramGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Program].[Subprogram]    Script Date: 11/8/2019 4:04:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Program].[Subprogram](
	[SubProgramGuid] [uniqueidentifier] NOT NULL,
	[ProgramGuid] [uniqueidentifier] NOT NULL,
	[SubProgramName] [nvarchar](100) NULL,
	[SfSubProgramId] [nvarchar](20) NULL,
 CONSTRAINT [PK_Utility] PRIMARY KEY CLUSTERED 
(
	[SubProgramGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Staging].[AcmfCompData]    Script Date: 11/8/2019 4:04:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Staging].[AcmfCompData](
	[BillAccount] [varchar](255) NULL,
	[CustomerNumber] [varchar](255) NULL,
	[SwitchA] [varchar](255) NULL,
	[SwitchB] [varchar](255) NULL,
	[Blank] [varchar](255) NULL,
	[Premise] [varchar](255) NULL,
	[ShutOffNonPay] [varchar](255) NULL,
	[Year] [varchar](255) NULL,
	[MeterLocation1] [varchar](255) NULL,
	[MeterLocation2] [varchar](255) NULL,
	[MeterLocation3] [varchar](255) NULL,
	[MeterLocation4] [varchar](255) NULL,
	[MeterLocation5] [varchar](255) NULL,
	[MeterLocation6] [varchar](255) NULL,
	[MeterLocation7] [varchar](255) NULL,
	[MeterLocation8] [varchar](255) NULL,
	[MeterLocation9] [varchar](255) NULL,
	[MeterLocation10] [varchar](255) NULL,
	[MeterStatus1] [varchar](255) NULL,
	[MeterStatus2] [varchar](255) NULL,
	[MeterStatus3] [varchar](255) NULL,
	[MeterStatus4] [varchar](255) NULL,
	[MeterStatus5] [varchar](255) NULL,
	[MeterStatus6] [varchar](255) NULL,
	[MeterStatus7] [varchar](255) NULL,
	[MeterStatus8] [varchar](255) NULL,
	[MeterStatus9] [varchar](255) NULL,
	[MeterStatus10] [varchar](255) NULL,
	[Status] [varchar](255) NULL,
	[CriticalCode] [varchar](255) NULL,
	[TurnOnDate] [varchar](255) NULL,
	[Arrears30] [varchar](255) NULL,
	[Arrears60] [varchar](255) NULL,
	[Arrears90] [varchar](255) NULL,
	[FormerBalance] [varchar](255) NULL,
	[SaOrigDate] [varchar](255) NULL,
	[SaUnbilled] [varchar](255) NULL,
	[LastBudget] [varchar](255) NULL,
	[HighBillCode] [varchar](255) NULL,
	[OldBillAccountNumber] [varchar](255) NULL,
	[SwitchAAgain] [varchar](255) NULL,
	[GeographicWorkArea] [varchar](255) NULL,
	[BillGroup] [varchar](255) NULL,
	[Rate1] [varchar](255) NULL,
	[Load1] [varchar](255) NULL,
	[Rate2] [varchar](255) NULL,
	[Load2] [varchar](255) NULL,
	[Rate3] [varchar](255) NULL,
	[Load3] [varchar](255) NULL,
	[Rate4] [varchar](255) NULL,
	[Load4] [varchar](255) NULL,
	[Rate5] [varchar](255) NULL,
	[Load5] [varchar](255) NULL,
	[Rate6] [varchar](255) NULL,
	[Load6] [varchar](255) NULL,
	[Rate7] [varchar](255) NULL,
	[Load7] [varchar](255) NULL,
	[Rate8] [varchar](255) NULL,
	[Load8] [varchar](255) NULL,
	[Rate9] [varchar](255) NULL,
	[Load9] [varchar](255) NULL,
	[Rate10] [varchar](255) NULL,
	[Load10] [varchar](255) NULL,
	[BillDate1R1] [varchar](255) NULL,
	[Usage1R1] [varchar](255) NULL,
	[Revenue1R1] [varchar](255) NULL,
	[BillCode1R1] [varchar](255) NULL,
	[BillType1R1] [varchar](255) NULL,
	[BillDays1R1] [varchar](255) NULL,
	[BillDate2R1] [varchar](255) NULL,
	[Usage2R1] [varchar](255) NULL,
	[Revenue2R1] [varchar](255) NULL,
	[BillCode2R1] [varchar](255) NULL,
	[BillType2R1] [varchar](255) NULL,
	[BillDays2R1] [varchar](255) NULL,
	[BillDate3R1] [varchar](255) NULL,
	[Usage3R1] [varchar](255) NULL,
	[Revenue3R1] [varchar](255) NULL,
	[BillCode3R1] [varchar](255) NULL,
	[BillType3R1] [varchar](255) NULL,
	[BillDays3R1] [varchar](255) NULL,
	[BillDate4R1] [varchar](255) NULL,
	[Usage4R1] [varchar](255) NULL,
	[Revenue4R1] [varchar](255) NULL,
	[BillCode4R1] [varchar](255) NULL,
	[BillType4R1] [varchar](255) NULL,
	[BillDays4R1] [varchar](255) NULL,
	[BillDate5R1] [varchar](255) NULL,
	[Usage5R1] [varchar](255) NULL,
	[Revenue5R1] [varchar](255) NULL,
	[BillCode5R1] [varchar](255) NULL,
	[BillType5R1] [varchar](255) NULL,
	[BillDays5R1] [varchar](255) NULL,
	[BillDate6R1] [varchar](255) NULL,
	[Usage6R1] [varchar](255) NULL,
	[Revenue6R1] [varchar](255) NULL,
	[BillCode6R1] [varchar](255) NULL,
	[BillType6R1] [varchar](255) NULL,
	[BillDays6R1] [varchar](255) NULL,
	[BillDate7R1] [varchar](255) NULL,
	[Usage7R1] [varchar](255) NULL,
	[Revenue7R1] [varchar](255) NULL,
	[BillCode7R1] [varchar](255) NULL,
	[BillType7R1] [varchar](255) NULL,
	[BillDays7R1] [varchar](255) NULL,
	[BillDate8R1] [varchar](255) NULL,
	[Usage8R1] [varchar](255) NULL,
	[Revenue8R1] [varchar](255) NULL,
	[BillCode8R1] [varchar](255) NULL,
	[BillType8R1] [varchar](255) NULL,
	[BillDays8R1] [varchar](255) NULL,
	[BillDate9R1] [varchar](255) NULL,
	[Usage9R1] [varchar](255) NULL,
	[Revenue9R1] [varchar](255) NULL,
	[BillCode9R1] [varchar](255) NULL,
	[BillType9R1] [varchar](255) NULL,
	[BillDays9R1] [varchar](255) NULL,
	[BillDate10R1] [varchar](255) NULL,
	[Usage10R1] [varchar](255) NULL,
	[Revenue10R1] [varchar](255) NULL,
	[BillCode10R1] [varchar](255) NULL,
	[BillType10R1] [varchar](255) NULL,
	[BillDays10R1] [varchar](255) NULL,
	[BillDate11R1] [varchar](255) NULL,
	[Usage11R1] [varchar](255) NULL,
	[Revenue11R1] [varchar](255) NULL,
	[BillCode11R1] [varchar](255) NULL,
	[BillType11R1] [varchar](255) NULL,
	[BillDays11R1] [varchar](255) NULL,
	[BillDate12R1] [varchar](255) NULL,
	[Usage12R1] [varchar](255) NULL,
	[Revenue12R1] [varchar](255) NULL,
	[BillCode12R1] [varchar](255) NULL,
	[BillType12R1] [varchar](255) NULL,
	[BillDays12R1] [varchar](255) NULL,
	[BillDate13R1] [varchar](255) NULL,
	[Usage13R1] [varchar](255) NULL,
	[Revenue13R1] [varchar](255) NULL,
	[BillCode13R1] [varchar](255) NULL,
	[BillType13R1] [varchar](255) NULL,
	[BillDays13R1] [varchar](255) NULL,
	[BillDate14R1] [varchar](255) NULL,
	[Usage14R1] [varchar](255) NULL,
	[Revenue14R1] [varchar](255) NULL,
	[BillCode14R1] [varchar](255) NULL,
	[BillType14R1] [varchar](255) NULL,
	[BillDays14R1] [varchar](255) NULL,
	[BillDate1R2] [varchar](255) NULL,
	[Usage1R2] [varchar](255) NULL,
	[Revenue1R2] [varchar](255) NULL,
	[BillCode1R2] [varchar](255) NULL,
	[BillType1R2] [varchar](255) NULL,
	[BillDays1R2] [varchar](255) NULL,
	[BillDate2R2] [varchar](255) NULL,
	[Usage2R2] [varchar](255) NULL,
	[Revenue2R2] [varchar](255) NULL,
	[BillCode2R2] [varchar](255) NULL,
	[BillType2R2] [varchar](255) NULL,
	[BillDays2R2] [varchar](255) NULL,
	[BillDate3R2] [varchar](255) NULL,
	[Usage3R2] [varchar](255) NULL,
	[Revenue3R2] [varchar](255) NULL,
	[BillCode3R2] [varchar](255) NULL,
	[BillType3R2] [varchar](255) NULL,
	[BillDays3R2] [varchar](255) NULL,
	[BillDate4R2] [varchar](255) NULL,
	[Usage4R2] [varchar](255) NULL,
	[Revenue4R2] [varchar](255) NULL,
	[BillCode4R2] [varchar](255) NULL,
	[BillType4R2] [varchar](255) NULL,
	[BillDays4R2] [varchar](255) NULL,
	[BillDate5R2] [varchar](255) NULL,
	[Usage5R2] [varchar](255) NULL,
	[Revenue5R2] [varchar](255) NULL,
	[BillCode5R2] [varchar](255) NULL,
	[BillType5R2] [varchar](255) NULL,
	[BillDays5R2] [varchar](255) NULL,
	[BillDate6R2] [varchar](255) NULL,
	[Usage6R2] [varchar](255) NULL,
	[Revenue6R2] [varchar](255) NULL,
	[BillCode6R2] [varchar](255) NULL,
	[BillType6R2] [varchar](255) NULL,
	[BillDays6R2] [varchar](255) NULL,
	[BillDate7R2] [varchar](255) NULL,
	[Usage7R2] [varchar](255) NULL,
	[Revenue7R2] [varchar](255) NULL,
	[BillCode7R2] [varchar](255) NULL,
	[BillType7R2] [varchar](255) NULL,
	[BillDays7R2] [varchar](255) NULL,
	[BillDate8R2] [varchar](255) NULL,
	[Usage8R2] [varchar](255) NULL,
	[Revenue8R2] [varchar](255) NULL,
	[BillCode8R2] [varchar](255) NULL,
	[BillType8R2] [varchar](255) NULL,
	[BillDays8R2] [varchar](255) NULL,
	[BillDate9R2] [varchar](255) NULL,
	[Usage9R2] [varchar](255) NULL,
	[Revenue9R2] [varchar](255) NULL,
	[BillCode9R2] [varchar](255) NULL,
	[BillType9R2] [varchar](255) NULL,
	[BillDays9R2] [varchar](255) NULL,
	[BillDate10R2] [varchar](255) NULL,
	[Usage10R2] [varchar](255) NULL,
	[Revenue10R2] [varchar](255) NULL,
	[BillCode10R2] [varchar](255) NULL,
	[BillType10R2] [varchar](255) NULL,
	[BillDays10R2] [varchar](255) NULL,
	[BillDate11R2] [varchar](255) NULL,
	[Usage11R2] [varchar](255) NULL,
	[Revenue11R2] [varchar](255) NULL,
	[BillCode11R2] [varchar](255) NULL,
	[BillType11R2] [varchar](255) NULL,
	[BillDays11R2] [varchar](255) NULL,
	[BillDate12R2] [varchar](255) NULL,
	[Usage12R2] [varchar](255) NULL,
	[Revenue12R2] [varchar](255) NULL,
	[BillCode12R2] [varchar](255) NULL,
	[BillType12R2] [varchar](255) NULL,
	[BillDays12R2] [varchar](255) NULL,
	[BillDate13R2] [varchar](255) NULL,
	[Usage13R2] [varchar](255) NULL,
	[Revenue13R2] [varchar](255) NULL,
	[BillCode13R2] [varchar](255) NULL,
	[BillType13R2] [varchar](255) NULL,
	[BillDays13R2] [varchar](255) NULL,
	[BillDate14R2] [varchar](255) NULL,
	[Usage14R2] [varchar](255) NULL,
	[Revenue14R2] [varchar](255) NULL,
	[BillCode14R2] [varchar](255) NULL,
	[BillType14R2] [varchar](255) NULL,
	[BillDays14R2] [varchar](255) NULL,
	[BillDate1R3] [varchar](255) NULL,
	[Usage1R3] [varchar](255) NULL,
	[Revenue1R3] [varchar](255) NULL,
	[BillCode1R3] [varchar](255) NULL,
	[BillType1R3] [varchar](255) NULL,
	[BillDays1R3] [varchar](255) NULL,
	[BillDate2R3] [varchar](255) NULL,
	[Usage2R3] [varchar](255) NULL,
	[Revenue2R3] [varchar](255) NULL,
	[BillCode2R3] [varchar](255) NULL,
	[BillType2R3] [varchar](255) NULL,
	[BillDays2R3] [varchar](255) NULL,
	[BillDate3R3] [varchar](255) NULL,
	[Usage3R3] [varchar](255) NULL,
	[Revenue3R3] [varchar](255) NULL,
	[BillCode3R3] [varchar](255) NULL,
	[BillType3R3] [varchar](255) NULL,
	[BillDays3R3] [varchar](255) NULL,
	[BillDate4R3] [varchar](255) NULL,
	[Usage4R3] [varchar](255) NULL,
	[Revenue4R3] [varchar](255) NULL,
	[BillCode4R3] [varchar](255) NULL,
	[BillType4R3] [varchar](255) NULL,
	[BillDays4R3] [varchar](255) NULL,
	[BillDate5R3] [varchar](255) NULL,
	[Usage5R3] [varchar](255) NULL,
	[Revenue5R3] [varchar](255) NULL,
	[BillCode5R3] [varchar](255) NULL,
	[BillType5R3] [varchar](255) NULL,
	[BillDays5R3] [varchar](255) NULL,
	[BillDate6R3] [varchar](255) NULL,
	[Usage6R3] [varchar](255) NULL,
	[Revenue6R3] [varchar](255) NULL,
	[BillCode6R3] [varchar](255) NULL,
	[BillType6R3] [varchar](255) NULL,
	[BillDays6R3] [varchar](255) NULL,
	[BillDate7R3] [varchar](255) NULL,
	[Usage7R3] [varchar](255) NULL,
	[Revenue7R3] [varchar](255) NULL,
	[BillCode7R3] [varchar](255) NULL,
	[BillType7R3] [varchar](255) NULL,
	[BillDays7R3] [varchar](255) NULL,
	[BillDate8R3] [varchar](255) NULL,
	[Usage8R3] [varchar](255) NULL,
	[Revenue8R3] [varchar](255) NULL,
	[BillCode8R3] [varchar](255) NULL,
	[BillType8R3] [varchar](255) NULL,
	[BillDays8R3] [varchar](255) NULL,
	[BillDate9R3] [varchar](255) NULL,
	[Usage9R3] [varchar](255) NULL,
	[Revenue9R3] [varchar](255) NULL,
	[BillCode9R3] [varchar](255) NULL,
	[BillType9R3] [varchar](255) NULL,
	[BillDays9R3] [varchar](255) NULL,
	[BillDate10R3] [varchar](255) NULL,
	[Usage10R3] [varchar](255) NULL,
	[Revenue10R3] [varchar](255) NULL,
	[BillCode10R3] [varchar](255) NULL,
	[BillType10R3] [varchar](255) NULL,
	[BillDays10R3] [varchar](255) NULL,
	[BillDate11R3] [varchar](255) NULL,
	[Usage11R3] [varchar](255) NULL,
	[Revenue11R3] [varchar](255) NULL,
	[BillCode11R3] [varchar](255) NULL,
	[BillType11R3] [varchar](255) NULL,
	[BillDays11R3] [varchar](255) NULL,
	[BillDate12R3] [varchar](255) NULL,
	[Usage12R3] [varchar](255) NULL,
	[Revenue12R3] [varchar](255) NULL,
	[BillCode12R3] [varchar](255) NULL,
	[BillType12R3] [varchar](255) NULL,
	[BillDays12R3] [varchar](255) NULL,
	[BillDate13R3] [varchar](255) NULL,
	[Usage13R3] [varchar](255) NULL,
	[Revenue13R3] [varchar](255) NULL,
	[BillCode13R3] [varchar](255) NULL,
	[BillType13R3] [varchar](255) NULL,
	[BillDays13R3] [varchar](255) NULL,
	[BillDate14R3] [varchar](255) NULL,
	[Usage14R3] [varchar](255) NULL,
	[Revenue14R3] [varchar](255) NULL,
	[BillCode14R3] [varchar](255) NULL,
	[BillType14R3] [varchar](255) NULL,
	[BillDays14R3] [varchar](255) NULL,
	[BillDate1R4] [varchar](255) NULL,
	[Usage1R4] [varchar](255) NULL,
	[Revenue1R4] [varchar](255) NULL,
	[BillCode1R4] [varchar](255) NULL,
	[BillType1R4] [varchar](255) NULL,
	[BillDays1R4] [varchar](255) NULL,
	[BillDate2R4] [varchar](255) NULL,
	[Usage2R4] [varchar](255) NULL,
	[Revenue2R4] [varchar](255) NULL,
	[BillCode2R4] [varchar](255) NULL,
	[BillType2R4] [varchar](255) NULL,
	[BillDays2R4] [varchar](255) NULL,
	[BillDate3R4] [varchar](255) NULL,
	[Usage3R4] [varchar](255) NULL,
	[Revenue3R4] [varchar](255) NULL,
	[BillCode3R4] [varchar](255) NULL,
	[BillType3R4] [varchar](255) NULL,
	[BillDays3R4] [varchar](255) NULL,
	[BillDate4R4] [varchar](255) NULL,
	[Usage4R4] [varchar](255) NULL,
	[Revenue4R4] [varchar](255) NULL,
	[BillCode4R4] [varchar](255) NULL,
	[BillType4R4] [varchar](255) NULL,
	[BillDays4R4] [varchar](255) NULL,
	[BillDate5R4] [varchar](255) NULL,
	[Usage5R4] [varchar](255) NULL,
	[Revenue5R4] [varchar](255) NULL,
	[BillCode5R4] [varchar](255) NULL,
	[BillType5R4] [varchar](255) NULL,
	[BillDays5R4] [varchar](255) NULL,
	[BillDate6R4] [varchar](255) NULL,
	[Usage6R4] [varchar](255) NULL,
	[Revenue6R4] [varchar](255) NULL,
	[BillCode6R4] [varchar](255) NULL,
	[BillType6R4] [varchar](255) NULL,
	[BillDays6R4] [varchar](255) NULL,
	[BillDate7R4] [varchar](255) NULL,
	[Usage7R4] [varchar](255) NULL,
	[Revenue7R4] [varchar](255) NULL,
	[BillCode7R4] [varchar](255) NULL,
	[BillType7R4] [varchar](255) NULL,
	[BillDays7R4] [varchar](255) NULL,
	[BillDate8R4] [varchar](255) NULL,
	[Usage8R4] [varchar](255) NULL,
	[Revenue8R4] [varchar](255) NULL,
	[BillCode8R4] [varchar](255) NULL,
	[BillType8R4] [varchar](255) NULL,
	[BillDays8R4] [varchar](255) NULL,
	[BillDate9R4] [varchar](255) NULL,
	[Usage9R4] [varchar](255) NULL,
	[Revenue9R4] [varchar](255) NULL,
	[BillCode9R4] [varchar](255) NULL,
	[BillType9R4] [varchar](255) NULL,
	[BillDays9R4] [varchar](255) NULL,
	[BillDate10R4] [varchar](255) NULL,
	[Usage10R4] [varchar](255) NULL,
	[Revenue10R4] [varchar](255) NULL,
	[BillCode10R4] [varchar](255) NULL,
	[BillType10R4] [varchar](255) NULL,
	[BillDays10R4] [varchar](255) NULL,
	[BillDate11R4] [varchar](255) NULL,
	[Usage11R4] [varchar](255) NULL,
	[Revenue11R4] [varchar](255) NULL,
	[BillCode11R4] [varchar](255) NULL,
	[BillType11R4] [varchar](255) NULL,
	[BillDays11R4] [varchar](255) NULL,
	[BillDate12R4] [varchar](255) NULL,
	[Usage12R4] [varchar](255) NULL,
	[Revenue12R4] [varchar](255) NULL,
	[BillCode12R4] [varchar](255) NULL,
	[BillType12R4] [varchar](255) NULL,
	[BillDays12R4] [varchar](255) NULL,
	[BillDate13R4] [varchar](255) NULL,
	[Usage13R4] [varchar](255) NULL,
	[Revenue13R4] [varchar](255) NULL,
	[BillCode13R4] [varchar](255) NULL,
	[BillType13R4] [varchar](255) NULL,
	[BillDays13R4] [varchar](255) NULL,
	[BillDate14R4] [varchar](255) NULL,
	[Usage14R4] [varchar](255) NULL,
	[Revenue14R4] [varchar](255) NULL,
	[BillCode14R4] [varchar](255) NULL,
	[BillType14R4] [varchar](255) NULL,
	[BillDays14R4] [varchar](255) NULL,
	[BillDate1R5] [varchar](255) NULL,
	[Usage1R5] [varchar](255) NULL,
	[Revenue1R5] [varchar](255) NULL,
	[BillCode1R5] [varchar](255) NULL,
	[BillType1R5] [varchar](255) NULL,
	[BillDays1R5] [varchar](255) NULL,
	[BillDate2R5] [varchar](255) NULL,
	[Usage2R5] [varchar](255) NULL,
	[Revenue2R5] [varchar](255) NULL,
	[BillCode2R5] [varchar](255) NULL,
	[BillType2R5] [varchar](255) NULL,
	[BillDays2R5] [varchar](255) NULL,
	[BillDate3R5] [varchar](255) NULL,
	[Usage3R5] [varchar](255) NULL,
	[Revenue3R5] [varchar](255) NULL,
	[BillCode3R5] [varchar](255) NULL,
	[BillType3R5] [varchar](255) NULL,
	[BillDays3R5] [varchar](255) NULL,
	[BillDate4R5] [varchar](255) NULL,
	[Usage4R5] [varchar](255) NULL,
	[Revenue4R5] [varchar](255) NULL,
	[BillCode4R5] [varchar](255) NULL,
	[BillType4R5] [varchar](255) NULL,
	[BillDays4R5] [varchar](255) NULL,
	[BillDate5R5] [varchar](255) NULL,
	[Usage5R5] [varchar](255) NULL,
	[Revenue5R5] [varchar](255) NULL,
	[BillCode5R5] [varchar](255) NULL,
	[BillType5R5] [varchar](255) NULL,
	[BillDays5R5] [varchar](255) NULL,
	[BillDate6R5] [varchar](255) NULL,
	[Usage6R5] [varchar](255) NULL,
	[Revenue6R5] [varchar](255) NULL,
	[BillCode6R5] [varchar](255) NULL,
	[BillType6R5] [varchar](255) NULL,
	[BillDays6R5] [varchar](255) NULL,
	[BillDate7R5] [varchar](255) NULL,
	[Usage7R5] [varchar](255) NULL,
	[Revenue7R5] [varchar](255) NULL,
	[BillCode7R5] [varchar](255) NULL,
	[BillType7R5] [varchar](255) NULL,
	[BillDays7R5] [varchar](255) NULL,
	[BillDate8R5] [varchar](255) NULL,
	[Usage8R5] [varchar](255) NULL,
	[Revenue8R5] [varchar](255) NULL,
	[BillCode8R5] [varchar](255) NULL,
	[BillType8R5] [varchar](255) NULL,
	[BillDays8R5] [varchar](255) NULL,
	[BillDate9R5] [varchar](255) NULL,
	[Usage9R5] [varchar](255) NULL,
	[Revenue9R5] [varchar](255) NULL,
	[BillCode9R5] [varchar](255) NULL,
	[BillType9R5] [varchar](255) NULL,
	[BillDays9R5] [varchar](255) NULL,
	[BillDate10R5] [varchar](255) NULL,
	[Usage10R5] [varchar](255) NULL,
	[Revenue10R5] [varchar](255) NULL,
	[BillCode10R5] [varchar](255) NULL,
	[BillType10R5] [varchar](255) NULL,
	[BillDays10R5] [varchar](255) NULL,
	[BillDate11R5] [varchar](255) NULL,
	[Usage11R5] [varchar](255) NULL,
	[Revenue11R5] [varchar](255) NULL,
	[BillCode11R5] [varchar](255) NULL,
	[BillType11R5] [varchar](255) NULL,
	[BillDays11R5] [varchar](255) NULL,
	[BillDate12R5] [varchar](255) NULL,
	[Usage12R5] [varchar](255) NULL,
	[Revenue12R5] [varchar](255) NULL,
	[BillCode12R5] [varchar](255) NULL,
	[BillType12R5] [varchar](255) NULL,
	[BillDays12R5] [varchar](255) NULL,
	[BillDate13R5] [varchar](255) NULL,
	[Usage13R5] [varchar](255) NULL,
	[Revenue13R5] [varchar](255) NULL,
	[BillCode13R5] [varchar](255) NULL,
	[BillType13R5] [varchar](255) NULL,
	[BillDays13R5] [varchar](255) NULL,
	[BillDate14R5] [varchar](255) NULL,
	[Usage14R5] [varchar](255) NULL,
	[Revenue14R5] [varchar](255) NULL,
	[BillCode14R5] [varchar](255) NULL,
	[BillType14R5] [varchar](255) NULL,
	[BillDays14R5] [varchar](255) NULL,
	[BillDate1R6] [varchar](255) NULL,
	[Usage1R6] [varchar](255) NULL,
	[Revenue1R6] [varchar](255) NULL,
	[BillCode1R6] [varchar](255) NULL,
	[BillType1R6] [varchar](255) NULL,
	[BillDays1R6] [varchar](255) NULL,
	[BillDate2R6] [varchar](255) NULL,
	[Usage2R6] [varchar](255) NULL,
	[Revenue2R6] [varchar](255) NULL,
	[BillCode2R6] [varchar](255) NULL,
	[BillType2R6] [varchar](255) NULL,
	[BillDays2R6] [varchar](255) NULL,
	[BillDate3R6] [varchar](255) NULL,
	[Usage3R6] [varchar](255) NULL,
	[Revenue3R6] [varchar](255) NULL,
	[BillCode3R6] [varchar](255) NULL,
	[BillType3R6] [varchar](255) NULL,
	[BillDays3R6] [varchar](255) NULL,
	[BillDate4R6] [varchar](255) NULL,
	[Usage4R6] [varchar](255) NULL,
	[Revenue4R6] [varchar](255) NULL,
	[BillCode4R6] [varchar](255) NULL,
	[BillType4R6] [varchar](255) NULL,
	[BillDays4R6] [varchar](255) NULL,
	[BillDate5R6] [varchar](255) NULL,
	[Usage5R6] [varchar](255) NULL,
	[Revenue5R6] [varchar](255) NULL,
	[BillCode5R6] [varchar](255) NULL,
	[BillType5R6] [varchar](255) NULL,
	[BillDays5R6] [varchar](255) NULL,
	[BillDate6R6] [varchar](255) NULL,
	[Usage6R6] [varchar](255) NULL,
	[Revenue6R6] [varchar](255) NULL,
	[BillCode6R6] [varchar](255) NULL,
	[BillType6R6] [varchar](255) NULL,
	[BillDays6R6] [varchar](255) NULL,
	[BillDate7R6] [varchar](255) NULL,
	[Usage7R6] [varchar](255) NULL,
	[Revenue7R6] [varchar](255) NULL,
	[BillCode7R6] [varchar](255) NULL,
	[BillType7R6] [varchar](255) NULL,
	[BillDays7R6] [varchar](255) NULL,
	[BillDate8R6] [varchar](255) NULL,
	[Usage8R6] [varchar](255) NULL,
	[Revenue8R6] [varchar](255) NULL,
	[BillCode8R6] [varchar](255) NULL,
	[BillType8R6] [varchar](255) NULL,
	[BillDays8R6] [varchar](255) NULL,
	[BillDate9R6] [varchar](255) NULL,
	[Usage9R6] [varchar](255) NULL,
	[Revenue9R6] [varchar](255) NULL,
	[BillCode9R6] [varchar](255) NULL,
	[BillType9R6] [varchar](255) NULL,
	[BillDays9R6] [varchar](255) NULL,
	[BillDate10R6] [varchar](255) NULL,
	[Usage10R6] [varchar](255) NULL,
	[Revenue10R6] [varchar](255) NULL,
	[BillCode10R6] [varchar](255) NULL,
	[BillType10R6] [varchar](255) NULL,
	[BillDays10R6] [varchar](255) NULL,
	[BillDate11R6] [varchar](255) NULL,
	[Usage11R6] [varchar](255) NULL,
	[Revenue11R6] [varchar](255) NULL,
	[BillCode11R6] [varchar](255) NULL,
	[BillType11R6] [varchar](255) NULL,
	[BillDays11R6] [varchar](255) NULL,
	[BillDate12R6] [varchar](255) NULL,
	[Usage12R6] [varchar](255) NULL,
	[Revenue12R6] [varchar](255) NULL,
	[BillCode12R6] [varchar](255) NULL,
	[BillType12R6] [varchar](255) NULL,
	[BillDays12R6] [varchar](255) NULL,
	[BillDate13R6] [varchar](255) NULL,
	[Usage13R6] [varchar](255) NULL,
	[Revenue13R6] [varchar](255) NULL,
	[BillCode13R6] [varchar](255) NULL,
	[BillType13R6] [varchar](255) NULL,
	[BillDays13R6] [varchar](255) NULL,
	[BillDate14R6] [varchar](255) NULL,
	[Usage14R6] [varchar](255) NULL,
	[Revenue14R6] [varchar](255) NULL,
	[BillCode14R6] [varchar](255) NULL,
	[BillType14R6] [varchar](255) NULL,
	[BillDays14R6] [varchar](255) NULL,
	[BillDate1R7] [varchar](255) NULL,
	[Usage1R7] [varchar](255) NULL,
	[Revenue1R7] [varchar](255) NULL,
	[BillCode1R7] [varchar](255) NULL,
	[BillType1R7] [varchar](255) NULL,
	[BillDays1R7] [varchar](255) NULL,
	[BillDate2R7] [varchar](255) NULL,
	[Usage2R7] [varchar](255) NULL,
	[Revenue2R7] [varchar](255) NULL,
	[BillCode2R7] [varchar](255) NULL,
	[BillType2R7] [varchar](255) NULL,
	[BillDays2R7] [varchar](255) NULL,
	[BillDate3R7] [varchar](255) NULL,
	[Usage3R7] [varchar](255) NULL,
	[Revenue3R7] [varchar](255) NULL,
	[BillCode3R7] [varchar](255) NULL,
	[BillType3R7] [varchar](255) NULL,
	[BillDays3R7] [varchar](255) NULL,
	[BillDate4R7] [varchar](255) NULL,
	[Usage4R7] [varchar](255) NULL,
	[Revenue4R7] [varchar](255) NULL,
	[BillCode4R7] [varchar](255) NULL,
	[BillType4R7] [varchar](255) NULL,
	[BillDays4R7] [varchar](255) NULL,
	[BillDate5R7] [varchar](255) NULL,
	[Usage5R7] [varchar](255) NULL,
	[Revenue5R7] [varchar](255) NULL,
	[BillCode5R7] [varchar](255) NULL,
	[BillType5R7] [varchar](255) NULL,
	[BillDays5R7] [varchar](255) NULL,
	[BillDate6R7] [varchar](255) NULL,
	[Usage6R7] [varchar](255) NULL,
	[Revenue6R7] [varchar](255) NULL,
	[BillCode6R7] [varchar](255) NULL,
	[BillType6R7] [varchar](255) NULL,
	[BillDays6R7] [varchar](255) NULL,
	[BillDate7R7] [varchar](255) NULL,
	[Usage7R7] [varchar](255) NULL,
	[Revenue7R7] [varchar](255) NULL,
	[BillCode7R7] [varchar](255) NULL,
	[BillType7R7] [varchar](255) NULL,
	[BillDays7R7] [varchar](255) NULL,
	[BillDate8R7] [varchar](255) NULL,
	[Usage8R7] [varchar](255) NULL,
	[Revenue8R7] [varchar](255) NULL,
	[BillCode8R7] [varchar](255) NULL,
	[BillType8R7] [varchar](255) NULL,
	[BillDays8R7] [varchar](255) NULL,
	[BillDate9R7] [varchar](255) NULL,
	[Usage9R7] [varchar](255) NULL,
	[Revenue9R7] [varchar](255) NULL,
	[BillCode9R7] [varchar](255) NULL,
	[BillType9R7] [varchar](255) NULL,
	[BillDays9R7] [varchar](255) NULL,
	[BillDate10R7] [varchar](255) NULL,
	[Usage10R7] [varchar](255) NULL,
	[Revenue10R7] [varchar](255) NULL,
	[BillCode10R7] [varchar](255) NULL,
	[BillType10R7] [varchar](255) NULL,
	[BillDays10R7] [varchar](255) NULL,
	[BillDate11R7] [varchar](255) NULL,
	[Usage11R7] [varchar](255) NULL,
	[Revenue11R7] [varchar](255) NULL,
	[BillCode11R7] [varchar](255) NULL,
	[BillType11R7] [varchar](255) NULL,
	[BillDays11R7] [varchar](255) NULL,
	[BillDate12R7] [varchar](255) NULL,
	[Usage12R7] [varchar](255) NULL,
	[Revenue12R7] [varchar](255) NULL,
	[BillCode12R7] [varchar](255) NULL,
	[BillType12R7] [varchar](255) NULL,
	[BillDays12R7] [varchar](255) NULL,
	[BillDate13R7] [varchar](255) NULL,
	[Usage13R7] [varchar](255) NULL,
	[Revenue13R7] [varchar](255) NULL,
	[BillCode13R7] [varchar](255) NULL,
	[BillType13R7] [varchar](255) NULL,
	[BillDays13R7] [varchar](255) NULL,
	[BillDate14R7] [varchar](255) NULL,
	[Usage14R7] [varchar](255) NULL,
	[Revenue14R7] [varchar](255) NULL,
	[BillCode14R7] [varchar](255) NULL,
	[BillType14R7] [varchar](255) NULL,
	[BillDays14R7] [varchar](255) NULL,
	[BillDate1R8] [varchar](255) NULL,
	[Usage1R8] [varchar](255) NULL,
	[Revenue1R8] [varchar](255) NULL,
	[BillCode1R8] [varchar](255) NULL,
	[BillType1R8] [varchar](255) NULL,
	[BillDays1R8] [varchar](255) NULL,
	[BillDate2R8] [varchar](255) NULL,
	[Usage2R8] [varchar](255) NULL,
	[Revenue2R8] [varchar](255) NULL,
	[BillCode2R8] [varchar](255) NULL,
	[BillType2R8] [varchar](255) NULL,
	[BillDays2R8] [varchar](255) NULL,
	[BillDate3R8] [varchar](255) NULL,
	[Usage3R8] [varchar](255) NULL,
	[Revenue3R8] [varchar](255) NULL,
	[BillCode3R8] [varchar](255) NULL,
	[BillType3R8] [varchar](255) NULL,
	[BillDays3R8] [varchar](255) NULL,
	[BillDate4R8] [varchar](255) NULL,
	[Usage4R8] [varchar](255) NULL,
	[Revenue4R8] [varchar](255) NULL,
	[BillCode4R8] [varchar](255) NULL,
	[BillType4R8] [varchar](255) NULL,
	[BillDays4R8] [varchar](255) NULL,
	[BillDate5R8] [varchar](255) NULL,
	[Usage5R8] [varchar](255) NULL,
	[Revenue5R8] [varchar](255) NULL,
	[BillCode5R8] [varchar](255) NULL,
	[BillType5R8] [varchar](255) NULL,
	[BillDays5R8] [varchar](255) NULL,
	[BillDate6R8] [varchar](255) NULL,
	[Usage6R8] [varchar](255) NULL,
	[Revenue6R8] [varchar](255) NULL,
	[BillCode6R8] [varchar](255) NULL,
	[BillType6R8] [varchar](255) NULL,
	[BillDays6R8] [varchar](255) NULL,
	[BillDate7R8] [varchar](255) NULL,
	[Usage7R8] [varchar](255) NULL,
	[Revenue7R8] [varchar](255) NULL,
	[BillCode7R8] [varchar](255) NULL,
	[BillType7R8] [varchar](255) NULL,
	[BillDays7R8] [varchar](255) NULL,
	[BillDate8R8] [varchar](255) NULL,
	[Usage8R8] [varchar](255) NULL,
	[Revenue8R8] [varchar](255) NULL,
	[BillCode8R8] [varchar](255) NULL,
	[BillType8R8] [varchar](255) NULL,
	[BillDays8R8] [varchar](255) NULL,
	[BillDate9R8] [varchar](255) NULL,
	[Usage9R8] [varchar](255) NULL,
	[Revenue9R8] [varchar](255) NULL,
	[BillCode9R8] [varchar](255) NULL,
	[BillType9R8] [varchar](255) NULL,
	[BillDays9R8] [varchar](255) NULL,
	[BillDate10R8] [varchar](255) NULL,
	[Usage10R8] [varchar](255) NULL,
	[Revenue10R8] [varchar](255) NULL,
	[BillCode10R8] [varchar](255) NULL,
	[BillType10R8] [varchar](255) NULL,
	[BillDays10R8] [varchar](255) NULL,
	[BillDate11R8] [varchar](255) NULL,
	[Usage11R8] [varchar](255) NULL,
	[Revenue11R8] [varchar](255) NULL,
	[BillCode11R8] [varchar](255) NULL,
	[BillType11R8] [varchar](255) NULL,
	[BillDays11R8] [varchar](255) NULL,
	[BillDate12R8] [varchar](255) NULL,
	[Usage12R8] [varchar](255) NULL,
	[Revenue12R8] [varchar](255) NULL,
	[BillCode12R8] [varchar](255) NULL,
	[BillType12R8] [varchar](255) NULL,
	[BillDays12R8] [varchar](255) NULL,
	[BillDate13R8] [varchar](255) NULL,
	[Usage13R8] [varchar](255) NULL,
	[Revenue13R8] [varchar](255) NULL,
	[BillCode13R8] [varchar](255) NULL,
	[BillType13R8] [varchar](255) NULL,
	[BillDays13R8] [varchar](255) NULL,
	[BillDate14R8] [varchar](255) NULL,
	[Usage14R8] [varchar](255) NULL,
	[Revenue14R8] [varchar](255) NULL,
	[BillCode14R8] [varchar](255) NULL,
	[BillType14R8] [varchar](255) NULL,
	[BillDays14R8] [varchar](255) NULL,
	[BillDate1R9] [varchar](255) NULL,
	[Usage1R9] [varchar](255) NULL,
	[Revenue1R9] [varchar](255) NULL,
	[BillCode1R9] [varchar](255) NULL,
	[BillType1R9] [varchar](255) NULL,
	[BillDays1R9] [varchar](255) NULL,
	[BillDate2R9] [varchar](255) NULL,
	[Usage2R9] [varchar](255) NULL,
	[Revenue2R9] [varchar](255) NULL,
	[BillCode2R9] [varchar](255) NULL,
	[BillType2R9] [varchar](255) NULL,
	[BillDays2R9] [varchar](255) NULL,
	[BillDate3R9] [varchar](255) NULL,
	[Usage3R9] [varchar](255) NULL,
	[Revenue3R9] [varchar](255) NULL,
	[BillCode3R9] [varchar](255) NULL,
	[BillType3R9] [varchar](255) NULL,
	[BillDays3R9] [varchar](255) NULL,
	[BillDate4R9] [varchar](255) NULL,
	[Usage4R9] [varchar](255) NULL,
	[Revenue4R9] [varchar](255) NULL,
	[BillCode4R9] [varchar](255) NULL,
	[BillType4R9] [varchar](255) NULL,
	[BillDays4R9] [varchar](255) NULL,
	[BillDate5R9] [varchar](255) NULL,
	[Usage5R9] [varchar](255) NULL,
	[Revenue5R9] [varchar](255) NULL,
	[BillCode5R9] [varchar](255) NULL,
	[BillType5R9] [varchar](255) NULL,
	[BillDays5R9] [varchar](255) NULL,
	[BillDate6R9] [varchar](255) NULL,
	[Usage6R9] [varchar](255) NULL,
	[Revenue6R9] [varchar](255) NULL,
	[BillCode6R9] [varchar](255) NULL,
	[BillType6R9] [varchar](255) NULL,
	[BillDays6R9] [varchar](255) NULL,
	[BillDate7R9] [varchar](255) NULL,
	[Usage7R9] [varchar](255) NULL,
	[Revenue7R9] [varchar](255) NULL,
	[BillCode7R9] [varchar](255) NULL,
	[BillType7R9] [varchar](255) NULL,
	[BillDays7R9] [varchar](255) NULL,
	[BillDate8R9] [varchar](255) NULL,
	[Usage8R9] [varchar](255) NULL,
	[Revenue8R9] [varchar](255) NULL,
	[BillCode8R9] [varchar](255) NULL,
	[BillType8R9] [varchar](255) NULL,
	[BillDays8R9] [varchar](255) NULL,
	[BillDate9R9] [varchar](255) NULL,
	[Usage9R9] [varchar](255) NULL,
	[Revenue9R9] [varchar](255) NULL,
	[BillCode9R9] [varchar](255) NULL,
	[BillType9R9] [varchar](255) NULL,
	[BillDays9R9] [varchar](255) NULL,
	[BillDate10R9] [varchar](255) NULL,
	[Usage10R9] [varchar](255) NULL,
	[Revenue10R9] [varchar](255) NULL,
	[BillCode10R9] [varchar](255) NULL,
	[BillType10R9] [varchar](255) NULL,
	[BillDays10R9] [varchar](255) NULL,
	[BillDate11R9] [varchar](255) NULL,
	[Usage11R9] [varchar](255) NULL,
	[Revenue11R9] [varchar](255) NULL,
	[BillCode11R9] [varchar](255) NULL,
	[BillType11R9] [varchar](255) NULL,
	[BillDays11R9] [varchar](255) NULL,
	[BillDate12R9] [varchar](255) NULL,
	[Usage12R9] [varchar](255) NULL,
	[Revenue12R9] [varchar](255) NULL,
	[BillCode12R9] [varchar](255) NULL,
	[BillType12R9] [varchar](255) NULL,
	[BillDays12R9] [varchar](255) NULL,
	[BillDate13R9] [varchar](255) NULL,
	[Usage13R9] [varchar](255) NULL,
	[Revenue13R9] [varchar](255) NULL,
	[BillCode13R9] [varchar](255) NULL,
	[BillType13R9] [varchar](255) NULL,
	[BillDays13R9] [varchar](255) NULL,
	[BillDate14R9] [varchar](255) NULL,
	[Usage14R9] [varchar](255) NULL,
	[Revenue14R9] [varchar](255) NULL,
	[BillCode14R9] [varchar](255) NULL,
	[BillType14R9] [varchar](255) NULL,
	[BillDays14R9] [varchar](255) NULL,
	[BillDate1R10] [varchar](255) NULL,
	[Usage1R10] [varchar](255) NULL,
	[Revenue1R10] [varchar](255) NULL,
	[BillCode1R10] [varchar](255) NULL,
	[BillType1R10] [varchar](255) NULL,
	[BillDays1R10] [varchar](255) NULL,
	[BillDate2R10] [varchar](255) NULL,
	[Usage2R10] [varchar](255) NULL,
	[Revenue2R10] [varchar](255) NULL,
	[BillCode2R10] [varchar](255) NULL,
	[BillType2R10] [varchar](255) NULL,
	[BillDays2R10] [varchar](255) NULL,
	[BillDate3R10] [varchar](255) NULL,
	[Usage3R10] [varchar](255) NULL,
	[Revenue3R10] [varchar](255) NULL,
	[BillCode3R10] [varchar](255) NULL,
	[BillType3R10] [varchar](255) NULL,
	[BillDays3R10] [varchar](255) NULL,
	[BillDate4R10] [varchar](255) NULL,
	[Usage4R10] [varchar](255) NULL,
	[Revenue4R10] [varchar](255) NULL,
	[BillCode4R10] [varchar](255) NULL,
	[BillType4R10] [varchar](255) NULL,
	[BillDays4R10] [varchar](255) NULL,
	[BillDate5R10] [varchar](255) NULL,
	[Usage5R10] [varchar](255) NULL,
	[Revenue5R10] [varchar](255) NULL,
	[BillCode5R10] [varchar](255) NULL,
	[BillType5R10] [varchar](255) NULL,
	[BillDays5R10] [varchar](255) NULL,
	[BillDate6R10] [varchar](255) NULL,
	[Usage6R10] [varchar](255) NULL,
	[Revenue6R10] [varchar](255) NULL,
	[BillCode6R10] [varchar](255) NULL,
	[BillType6R10] [varchar](255) NULL,
	[BillDays6R10] [varchar](255) NULL,
	[BillDate7R10] [varchar](255) NULL,
	[Usage7R10] [varchar](255) NULL,
	[Revenue7R10] [varchar](255) NULL,
	[BillCode7R10] [varchar](255) NULL,
	[BillType7R10] [varchar](255) NULL,
	[BillDays7R10] [varchar](255) NULL,
	[BillDate8R10] [varchar](255) NULL,
	[Usage8R10] [varchar](255) NULL,
	[Revenue8R10] [varchar](255) NULL,
	[BillCode8R10] [varchar](255) NULL,
	[BillType8R10] [varchar](255) NULL,
	[BillDays8R10] [varchar](255) NULL,
	[BillDate9R10] [varchar](255) NULL,
	[Usage9R10] [varchar](255) NULL,
	[Revenue9R10] [varchar](255) NULL,
	[BillCode9R10] [varchar](255) NULL,
	[BillType9R10] [varchar](255) NULL,
	[BillDays9R10] [varchar](255) NULL,
	[BillDate10R10] [varchar](255) NULL,
	[Usage10R10] [varchar](255) NULL,
	[Revenue10R10] [varchar](255) NULL,
	[BillCode10R10] [varchar](255) NULL,
	[BillType10R10] [varchar](255) NULL,
	[BillDays10R10] [varchar](255) NULL,
	[BillDate11R10] [varchar](255) NULL,
	[Usage11R10] [varchar](255) NULL,
	[Revenue11R10] [varchar](255) NULL,
	[BillCode11R10] [varchar](255) NULL,
	[BillType11R10] [varchar](255) NULL,
	[BillDays11R10] [varchar](255) NULL,
	[BillDate12R10] [varchar](255) NULL,
	[Usage12R10] [varchar](255) NULL,
	[Revenue12R10] [varchar](255) NULL,
	[BillCode12R10] [varchar](255) NULL,
	[BillType12R10] [varchar](255) NULL,
	[BillDays12R10] [varchar](255) NULL,
	[BillDate13R10] [varchar](255) NULL,
	[Usage13R10] [varchar](255) NULL,
	[Revenue13R10] [varchar](255) NULL,
	[BillCode13R10] [varchar](255) NULL,
	[BillType13R10] [varchar](255) NULL,
	[BillDays13R10] [varchar](255) NULL,
	[BillDate14R10] [varchar](255) NULL,
	[Usage14R10] [varchar](255) NULL,
	[Revenue14R10] [varchar](255) NULL,
	[BillCode14R10] [varchar](255) NULL,
	[BillType14R10] [varchar](255) NULL,
	[BillDays14R10] [varchar](255) NULL,
	[ReturnCode] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Staging].[DegDayData]    Script Date: 11/8/2019 4:04:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Staging].[DegDayData](
	[Year] [varchar](255) NULL,
	[NumberOfDays] [varchar](255) NULL,
	[DailyAverageTemperature1] [varchar](255) NULL,
	[DailyAverageTemperature2] [varchar](255) NULL,
	[DailyAverageTemperature3] [varchar](255) NULL,
	[DailyAverageTemperature4] [varchar](255) NULL,
	[DailyAverageTemperature5] [varchar](255) NULL,
	[DailyAverageTemperature6] [varchar](255) NULL,
	[DailyAverageTemperature7] [varchar](255) NULL,
	[DailyAverageTemperature8] [varchar](255) NULL,
	[DailyAverageTemperature9] [varchar](255) NULL,
	[DailyAverageTemperature10] [varchar](255) NULL,
	[DailyAverageTemperature11] [varchar](255) NULL,
	[DailyAverageTemperature12] [varchar](255) NULL,
	[DailyAverageTemperature13] [varchar](255) NULL,
	[DailyAverageTemperature14] [varchar](255) NULL,
	[DailyAverageTemperature15] [varchar](255) NULL,
	[DailyAverageTemperature16] [varchar](255) NULL,
	[DailyAverageTemperature17] [varchar](255) NULL,
	[DailyAverageTemperature18] [varchar](255) NULL,
	[DailyAverageTemperature19] [varchar](255) NULL,
	[DailyAverageTemperature20] [varchar](255) NULL,
	[DailyAverageTemperature21] [varchar](255) NULL,
	[DailyAverageTemperature22] [varchar](255) NULL,
	[DailyAverageTemperature23] [varchar](255) NULL,
	[DailyAverageTemperature24] [varchar](255) NULL,
	[DailyAverageTemperature25] [varchar](255) NULL,
	[DailyAverageTemperature26] [varchar](255) NULL,
	[DailyAverageTemperature27] [varchar](255) NULL,
	[DailyAverageTemperature28] [varchar](255) NULL,
	[DailyAverageTemperature29] [varchar](255) NULL,
	[DailyAverageTemperature30] [varchar](255) NULL,
	[DailyAverageTemperature31] [varchar](255) NULL,
	[DailyAverageTemperature32] [varchar](255) NULL,
	[DailyAverageTemperature33] [varchar](255) NULL,
	[DailyAverageTemperature34] [varchar](255) NULL,
	[DailyAverageTemperature35] [varchar](255) NULL,
	[DailyAverageTemperature36] [varchar](255) NULL,
	[DailyAverageTemperature37] [varchar](255) NULL,
	[DailyAverageTemperature38] [varchar](255) NULL,
	[DailyAverageTemperature39] [varchar](255) NULL,
	[DailyAverageTemperature40] [varchar](255) NULL,
	[DailyAverageTemperature41] [varchar](255) NULL,
	[DailyAverageTemperature42] [varchar](255) NULL,
	[DailyAverageTemperature43] [varchar](255) NULL,
	[DailyAverageTemperature44] [varchar](255) NULL,
	[DailyAverageTemperature45] [varchar](255) NULL,
	[DailyAverageTemperature46] [varchar](255) NULL,
	[DailyAverageTemperature47] [varchar](255) NULL,
	[DailyAverageTemperature48] [varchar](255) NULL,
	[DailyAverageTemperature49] [varchar](255) NULL,
	[DailyAverageTemperature50] [varchar](255) NULL,
	[DailyAverageTemperature51] [varchar](255) NULL,
	[DailyAverageTemperature52] [varchar](255) NULL,
	[DailyAverageTemperature53] [varchar](255) NULL,
	[DailyAverageTemperature54] [varchar](255) NULL,
	[DailyAverageTemperature55] [varchar](255) NULL,
	[DailyAverageTemperature56] [varchar](255) NULL,
	[DailyAverageTemperature57] [varchar](255) NULL,
	[DailyAverageTemperature58] [varchar](255) NULL,
	[DailyAverageTemperature59] [varchar](255) NULL,
	[DailyAverageTemperature60] [varchar](255) NULL,
	[DailyAverageTemperature61] [varchar](255) NULL,
	[DailyAverageTemperature62] [varchar](255) NULL,
	[DailyAverageTemperature63] [varchar](255) NULL,
	[DailyAverageTemperature64] [varchar](255) NULL,
	[DailyAverageTemperature65] [varchar](255) NULL,
	[DailyAverageTemperature66] [varchar](255) NULL,
	[DailyAverageTemperature67] [varchar](255) NULL,
	[DailyAverageTemperature68] [varchar](255) NULL,
	[DailyAverageTemperature69] [varchar](255) NULL,
	[DailyAverageTemperature70] [varchar](255) NULL,
	[DailyAverageTemperature71] [varchar](255) NULL,
	[DailyAverageTemperature72] [varchar](255) NULL,
	[DailyAverageTemperature73] [varchar](255) NULL,
	[DailyAverageTemperature74] [varchar](255) NULL,
	[DailyAverageTemperature75] [varchar](255) NULL,
	[DailyAverageTemperature76] [varchar](255) NULL,
	[DailyAverageTemperature77] [varchar](255) NULL,
	[DailyAverageTemperature78] [varchar](255) NULL,
	[DailyAverageTemperature79] [varchar](255) NULL,
	[DailyAverageTemperature80] [varchar](255) NULL,
	[DailyAverageTemperature81] [varchar](255) NULL,
	[DailyAverageTemperature82] [varchar](255) NULL,
	[DailyAverageTemperature83] [varchar](255) NULL,
	[DailyAverageTemperature84] [varchar](255) NULL,
	[DailyAverageTemperature85] [varchar](255) NULL,
	[DailyAverageTemperature86] [varchar](255) NULL,
	[DailyAverageTemperature87] [varchar](255) NULL,
	[DailyAverageTemperature88] [varchar](255) NULL,
	[DailyAverageTemperature89] [varchar](255) NULL,
	[DailyAverageTemperature90] [varchar](255) NULL,
	[DailyAverageTemperature91] [varchar](255) NULL,
	[DailyAverageTemperature92] [varchar](255) NULL,
	[DailyAverageTemperature93] [varchar](255) NULL,
	[DailyAverageTemperature94] [varchar](255) NULL,
	[DailyAverageTemperature95] [varchar](255) NULL,
	[DailyAverageTemperature96] [varchar](255) NULL,
	[DailyAverageTemperature97] [varchar](255) NULL,
	[DailyAverageTemperature98] [varchar](255) NULL,
	[DailyAverageTemperature99] [varchar](255) NULL,
	[DailyAverageTemperature100] [varchar](255) NULL,
	[DailyAverageTemperature101] [varchar](255) NULL,
	[DailyAverageTemperature102] [varchar](255) NULL,
	[DailyAverageTemperature103] [varchar](255) NULL,
	[DailyAverageTemperature104] [varchar](255) NULL,
	[DailyAverageTemperature105] [varchar](255) NULL,
	[DailyAverageTemperature106] [varchar](255) NULL,
	[DailyAverageTemperature107] [varchar](255) NULL,
	[DailyAverageTemperature108] [varchar](255) NULL,
	[DailyAverageTemperature109] [varchar](255) NULL,
	[DailyAverageTemperature110] [varchar](255) NULL,
	[DailyAverageTemperature111] [varchar](255) NULL,
	[DailyAverageTemperature112] [varchar](255) NULL,
	[DailyAverageTemperature113] [varchar](255) NULL,
	[DailyAverageTemperature114] [varchar](255) NULL,
	[DailyAverageTemperature115] [varchar](255) NULL,
	[DailyAverageTemperature116] [varchar](255) NULL,
	[DailyAverageTemperature117] [varchar](255) NULL,
	[DailyAverageTemperature118] [varchar](255) NULL,
	[DailyAverageTemperature119] [varchar](255) NULL,
	[DailyAverageTemperature120] [varchar](255) NULL,
	[DailyAverageTemperature121] [varchar](255) NULL,
	[DailyAverageTemperature122] [varchar](255) NULL,
	[DailyAverageTemperature123] [varchar](255) NULL,
	[DailyAverageTemperature124] [varchar](255) NULL,
	[DailyAverageTemperature125] [varchar](255) NULL,
	[DailyAverageTemperature126] [varchar](255) NULL,
	[DailyAverageTemperature127] [varchar](255) NULL,
	[DailyAverageTemperature128] [varchar](255) NULL,
	[DailyAverageTemperature129] [varchar](255) NULL,
	[DailyAverageTemperature130] [varchar](255) NULL,
	[DailyAverageTemperature131] [varchar](255) NULL,
	[DailyAverageTemperature132] [varchar](255) NULL,
	[DailyAverageTemperature133] [varchar](255) NULL,
	[DailyAverageTemperature134] [varchar](255) NULL,
	[DailyAverageTemperature135] [varchar](255) NULL,
	[DailyAverageTemperature136] [varchar](255) NULL,
	[DailyAverageTemperature137] [varchar](255) NULL,
	[DailyAverageTemperature138] [varchar](255) NULL,
	[DailyAverageTemperature139] [varchar](255) NULL,
	[DailyAverageTemperature140] [varchar](255) NULL,
	[DailyAverageTemperature141] [varchar](255) NULL,
	[DailyAverageTemperature142] [varchar](255) NULL,
	[DailyAverageTemperature143] [varchar](255) NULL,
	[DailyAverageTemperature144] [varchar](255) NULL,
	[DailyAverageTemperature145] [varchar](255) NULL,
	[DailyAverageTemperature146] [varchar](255) NULL,
	[DailyAverageTemperature147] [varchar](255) NULL,
	[DailyAverageTemperature148] [varchar](255) NULL,
	[DailyAverageTemperature149] [varchar](255) NULL,
	[DailyAverageTemperature150] [varchar](255) NULL,
	[DailyAverageTemperature151] [varchar](255) NULL,
	[DailyAverageTemperature152] [varchar](255) NULL,
	[DailyAverageTemperature153] [varchar](255) NULL,
	[DailyAverageTemperature154] [varchar](255) NULL,
	[DailyAverageTemperature155] [varchar](255) NULL,
	[DailyAverageTemperature156] [varchar](255) NULL,
	[DailyAverageTemperature157] [varchar](255) NULL,
	[DailyAverageTemperature158] [varchar](255) NULL,
	[DailyAverageTemperature159] [varchar](255) NULL,
	[DailyAverageTemperature160] [varchar](255) NULL,
	[DailyAverageTemperature161] [varchar](255) NULL,
	[DailyAverageTemperature162] [varchar](255) NULL,
	[DailyAverageTemperature163] [varchar](255) NULL,
	[DailyAverageTemperature164] [varchar](255) NULL,
	[DailyAverageTemperature165] [varchar](255) NULL,
	[DailyAverageTemperature166] [varchar](255) NULL,
	[DailyAverageTemperature167] [varchar](255) NULL,
	[DailyAverageTemperature168] [varchar](255) NULL,
	[DailyAverageTemperature169] [varchar](255) NULL,
	[DailyAverageTemperature170] [varchar](255) NULL,
	[DailyAverageTemperature171] [varchar](255) NULL,
	[DailyAverageTemperature172] [varchar](255) NULL,
	[DailyAverageTemperature173] [varchar](255) NULL,
	[DailyAverageTemperature174] [varchar](255) NULL,
	[DailyAverageTemperature175] [varchar](255) NULL,
	[DailyAverageTemperature176] [varchar](255) NULL,
	[DailyAverageTemperature177] [varchar](255) NULL,
	[DailyAverageTemperature178] [varchar](255) NULL,
	[DailyAverageTemperature179] [varchar](255) NULL,
	[DailyAverageTemperature180] [varchar](255) NULL,
	[DailyAverageTemperature181] [varchar](255) NULL,
	[DailyAverageTemperature182] [varchar](255) NULL,
	[DailyAverageTemperature183] [varchar](255) NULL,
	[DailyAverageTemperature184] [varchar](255) NULL,
	[DailyAverageTemperature185] [varchar](255) NULL,
	[DailyAverageTemperature186] [varchar](255) NULL,
	[DailyAverageTemperature187] [varchar](255) NULL,
	[DailyAverageTemperature188] [varchar](255) NULL,
	[DailyAverageTemperature189] [varchar](255) NULL,
	[DailyAverageTemperature190] [varchar](255) NULL,
	[DailyAverageTemperature191] [varchar](255) NULL,
	[DailyAverageTemperature192] [varchar](255) NULL,
	[DailyAverageTemperature193] [varchar](255) NULL,
	[DailyAverageTemperature194] [varchar](255) NULL,
	[DailyAverageTemperature195] [varchar](255) NULL,
	[DailyAverageTemperature196] [varchar](255) NULL,
	[DailyAverageTemperature197] [varchar](255) NULL,
	[DailyAverageTemperature198] [varchar](255) NULL,
	[DailyAverageTemperature199] [varchar](255) NULL,
	[DailyAverageTemperature200] [varchar](255) NULL,
	[DailyAverageTemperature201] [varchar](255) NULL,
	[DailyAverageTemperature202] [varchar](255) NULL,
	[DailyAverageTemperature203] [varchar](255) NULL,
	[DailyAverageTemperature204] [varchar](255) NULL,
	[DailyAverageTemperature205] [varchar](255) NULL,
	[DailyAverageTemperature206] [varchar](255) NULL,
	[DailyAverageTemperature207] [varchar](255) NULL,
	[DailyAverageTemperature208] [varchar](255) NULL,
	[DailyAverageTemperature209] [varchar](255) NULL,
	[DailyAverageTemperature210] [varchar](255) NULL,
	[DailyAverageTemperature211] [varchar](255) NULL,
	[DailyAverageTemperature212] [varchar](255) NULL,
	[DailyAverageTemperature213] [varchar](255) NULL,
	[DailyAverageTemperature214] [varchar](255) NULL,
	[DailyAverageTemperature215] [varchar](255) NULL,
	[DailyAverageTemperature216] [varchar](255) NULL,
	[DailyAverageTemperature217] [varchar](255) NULL,
	[DailyAverageTemperature218] [varchar](255) NULL,
	[DailyAverageTemperature219] [varchar](255) NULL,
	[DailyAverageTemperature220] [varchar](255) NULL,
	[DailyAverageTemperature221] [varchar](255) NULL,
	[DailyAverageTemperature222] [varchar](255) NULL,
	[DailyAverageTemperature223] [varchar](255) NULL,
	[DailyAverageTemperature224] [varchar](255) NULL,
	[DailyAverageTemperature225] [varchar](255) NULL,
	[DailyAverageTemperature226] [varchar](255) NULL,
	[DailyAverageTemperature227] [varchar](255) NULL,
	[DailyAverageTemperature228] [varchar](255) NULL,
	[DailyAverageTemperature229] [varchar](255) NULL,
	[DailyAverageTemperature230] [varchar](255) NULL,
	[DailyAverageTemperature231] [varchar](255) NULL,
	[DailyAverageTemperature232] [varchar](255) NULL,
	[DailyAverageTemperature233] [varchar](255) NULL,
	[DailyAverageTemperature234] [varchar](255) NULL,
	[DailyAverageTemperature235] [varchar](255) NULL,
	[DailyAverageTemperature236] [varchar](255) NULL,
	[DailyAverageTemperature237] [varchar](255) NULL,
	[DailyAverageTemperature238] [varchar](255) NULL,
	[DailyAverageTemperature239] [varchar](255) NULL,
	[DailyAverageTemperature240] [varchar](255) NULL,
	[DailyAverageTemperature241] [varchar](255) NULL,
	[DailyAverageTemperature242] [varchar](255) NULL,
	[DailyAverageTemperature243] [varchar](255) NULL,
	[DailyAverageTemperature244] [varchar](255) NULL,
	[DailyAverageTemperature245] [varchar](255) NULL,
	[DailyAverageTemperature246] [varchar](255) NULL,
	[DailyAverageTemperature247] [varchar](255) NULL,
	[DailyAverageTemperature248] [varchar](255) NULL,
	[DailyAverageTemperature249] [varchar](255) NULL,
	[DailyAverageTemperature250] [varchar](255) NULL,
	[DailyAverageTemperature251] [varchar](255) NULL,
	[DailyAverageTemperature252] [varchar](255) NULL,
	[DailyAverageTemperature253] [varchar](255) NULL,
	[DailyAverageTemperature254] [varchar](255) NULL,
	[DailyAverageTemperature255] [varchar](255) NULL,
	[DailyAverageTemperature256] [varchar](255) NULL,
	[DailyAverageTemperature257] [varchar](255) NULL,
	[DailyAverageTemperature258] [varchar](255) NULL,
	[DailyAverageTemperature259] [varchar](255) NULL,
	[DailyAverageTemperature260] [varchar](255) NULL,
	[DailyAverageTemperature261] [varchar](255) NULL,
	[DailyAverageTemperature262] [varchar](255) NULL,
	[DailyAverageTemperature263] [varchar](255) NULL,
	[DailyAverageTemperature264] [varchar](255) NULL,
	[DailyAverageTemperature265] [varchar](255) NULL,
	[DailyAverageTemperature266] [varchar](255) NULL,
	[DailyAverageTemperature267] [varchar](255) NULL,
	[DailyAverageTemperature268] [varchar](255) NULL,
	[DailyAverageTemperature269] [varchar](255) NULL,
	[DailyAverageTemperature270] [varchar](255) NULL,
	[DailyAverageTemperature271] [varchar](255) NULL,
	[DailyAverageTemperature272] [varchar](255) NULL,
	[DailyAverageTemperature273] [varchar](255) NULL,
	[DailyAverageTemperature274] [varchar](255) NULL,
	[DailyAverageTemperature275] [varchar](255) NULL,
	[DailyAverageTemperature276] [varchar](255) NULL,
	[DailyAverageTemperature277] [varchar](255) NULL,
	[DailyAverageTemperature278] [varchar](255) NULL,
	[DailyAverageTemperature279] [varchar](255) NULL,
	[DailyAverageTemperature280] [varchar](255) NULL,
	[DailyAverageTemperature281] [varchar](255) NULL,
	[DailyAverageTemperature282] [varchar](255) NULL,
	[DailyAverageTemperature283] [varchar](255) NULL,
	[DailyAverageTemperature284] [varchar](255) NULL,
	[DailyAverageTemperature285] [varchar](255) NULL,
	[DailyAverageTemperature286] [varchar](255) NULL,
	[DailyAverageTemperature287] [varchar](255) NULL,
	[DailyAverageTemperature288] [varchar](255) NULL,
	[DailyAverageTemperature289] [varchar](255) NULL,
	[DailyAverageTemperature290] [varchar](255) NULL,
	[DailyAverageTemperature291] [varchar](255) NULL,
	[DailyAverageTemperature292] [varchar](255) NULL,
	[DailyAverageTemperature293] [varchar](255) NULL,
	[DailyAverageTemperature294] [varchar](255) NULL,
	[DailyAverageTemperature295] [varchar](255) NULL,
	[DailyAverageTemperature296] [varchar](255) NULL,
	[DailyAverageTemperature297] [varchar](255) NULL,
	[DailyAverageTemperature298] [varchar](255) NULL,
	[DailyAverageTemperature299] [varchar](255) NULL,
	[DailyAverageTemperature300] [varchar](255) NULL,
	[DailyAverageTemperature301] [varchar](255) NULL,
	[DailyAverageTemperature302] [varchar](255) NULL,
	[DailyAverageTemperature303] [varchar](255) NULL,
	[DailyAverageTemperature304] [varchar](255) NULL,
	[DailyAverageTemperature305] [varchar](255) NULL,
	[DailyAverageTemperature306] [varchar](255) NULL,
	[DailyAverageTemperature307] [varchar](255) NULL,
	[DailyAverageTemperature308] [varchar](255) NULL,
	[DailyAverageTemperature309] [varchar](255) NULL,
	[DailyAverageTemperature310] [varchar](255) NULL,
	[DailyAverageTemperature311] [varchar](255) NULL,
	[DailyAverageTemperature312] [varchar](255) NULL,
	[DailyAverageTemperature313] [varchar](255) NULL,
	[DailyAverageTemperature314] [varchar](255) NULL,
	[DailyAverageTemperature315] [varchar](255) NULL,
	[DailyAverageTemperature316] [varchar](255) NULL,
	[DailyAverageTemperature317] [varchar](255) NULL,
	[DailyAverageTemperature318] [varchar](255) NULL,
	[DailyAverageTemperature319] [varchar](255) NULL,
	[DailyAverageTemperature320] [varchar](255) NULL,
	[DailyAverageTemperature321] [varchar](255) NULL,
	[DailyAverageTemperature322] [varchar](255) NULL,
	[DailyAverageTemperature323] [varchar](255) NULL,
	[DailyAverageTemperature324] [varchar](255) NULL,
	[DailyAverageTemperature325] [varchar](255) NULL,
	[DailyAverageTemperature326] [varchar](255) NULL,
	[DailyAverageTemperature327] [varchar](255) NULL,
	[DailyAverageTemperature328] [varchar](255) NULL,
	[DailyAverageTemperature329] [varchar](255) NULL,
	[DailyAverageTemperature330] [varchar](255) NULL,
	[DailyAverageTemperature331] [varchar](255) NULL,
	[DailyAverageTemperature332] [varchar](255) NULL,
	[DailyAverageTemperature333] [varchar](255) NULL,
	[DailyAverageTemperature334] [varchar](255) NULL,
	[DailyAverageTemperature335] [varchar](255) NULL,
	[DailyAverageTemperature336] [varchar](255) NULL,
	[DailyAverageTemperature337] [varchar](255) NULL,
	[DailyAverageTemperature338] [varchar](255) NULL,
	[DailyAverageTemperature339] [varchar](255) NULL,
	[DailyAverageTemperature340] [varchar](255) NULL,
	[DailyAverageTemperature341] [varchar](255) NULL,
	[DailyAverageTemperature342] [varchar](255) NULL,
	[DailyAverageTemperature343] [varchar](255) NULL,
	[DailyAverageTemperature344] [varchar](255) NULL,
	[DailyAverageTemperature345] [varchar](255) NULL,
	[DailyAverageTemperature346] [varchar](255) NULL,
	[DailyAverageTemperature347] [varchar](255) NULL,
	[DailyAverageTemperature348] [varchar](255) NULL,
	[DailyAverageTemperature349] [varchar](255) NULL,
	[DailyAverageTemperature350] [varchar](255) NULL,
	[DailyAverageTemperature351] [varchar](255) NULL,
	[DailyAverageTemperature352] [varchar](255) NULL,
	[DailyAverageTemperature353] [varchar](255) NULL,
	[DailyAverageTemperature354] [varchar](255) NULL,
	[DailyAverageTemperature355] [varchar](255) NULL,
	[DailyAverageTemperature356] [varchar](255) NULL,
	[DailyAverageTemperature357] [varchar](255) NULL,
	[DailyAverageTemperature358] [varchar](255) NULL,
	[DailyAverageTemperature359] [varchar](255) NULL,
	[DailyAverageTemperature360] [varchar](255) NULL,
	[DailyAverageTemperature361] [varchar](255) NULL,
	[DailyAverageTemperature362] [varchar](255) NULL,
	[DailyAverageTemperature363] [varchar](255) NULL,
	[DailyAverageTemperature364] [varchar](255) NULL,
	[DailyAverageTemperature365] [varchar](255) NULL,
	[DailyAverageTemperature366] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Staging].[FinaCompData]    Script Date: 11/8/2019 4:04:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Staging].[FinaCompData](
	[BillAccount] [varchar](255) NULL,
	[CustomerNumber] [varchar](255) NULL,
	[RunBalance] [varchar](255) NULL,
	[RunCreditBalance] [varchar](255) NULL,
	[CustomerBalance] [varchar](255) NULL,
	[CustomerCreditBalance] [varchar](255) NULL,
	[SeqNbr] [varchar](255) NULL,
	[TransactionCode1] [varchar](255) NULL,
	[TransactionSubTypeCode1] [varchar](255) NULL,
	[TransactionDate1] [varchar](255) NULL,
	[TransactionAmount1] [varchar](255) NULL,
	[TransactionCode2] [varchar](255) NULL,
	[TransactionSubTypeCode2] [varchar](255) NULL,
	[TransactionDate2] [varchar](255) NULL,
	[TransactionAmount2] [varchar](255) NULL,
	[TransactionCode3] [varchar](255) NULL,
	[TransactionSubTypeCode3] [varchar](255) NULL,
	[TransactionDate3] [varchar](255) NULL,
	[TransactionAmount3] [varchar](255) NULL,
	[TransactionCode4] [varchar](255) NULL,
	[TransactionSubTypeCode4] [varchar](255) NULL,
	[TransactionDate4] [varchar](255) NULL,
	[TransactionAmount4] [varchar](255) NULL,
	[TransactionCode5] [varchar](255) NULL,
	[TransactionSubTypeCode5] [varchar](255) NULL,
	[TransactionDate5] [varchar](255) NULL,
	[TransactionAmount5] [varchar](255) NULL,
	[TransactionCode6] [varchar](255) NULL,
	[TransactionSubTypeCode6] [varchar](255) NULL,
	[TransactionDate6] [varchar](255) NULL,
	[TransactionAmount6] [varchar](255) NULL,
	[TransactionCode7] [varchar](255) NULL,
	[TransactionSubTypeCode7] [varchar](255) NULL,
	[TransactionDate7] [varchar](255) NULL,
	[TransactionAmount7] [varchar](255) NULL,
	[TransactionCode8] [varchar](255) NULL,
	[TransactionSubTypeCode8] [varchar](255) NULL,
	[TransactionDate8] [varchar](255) NULL,
	[TransactionAmount8] [varchar](255) NULL,
	[TransactionCode9] [varchar](255) NULL,
	[TransactionSubTypeCode9] [varchar](255) NULL,
	[TransactionDate9] [varchar](255) NULL,
	[TransactionAmount9] [varchar](255) NULL,
	[TransactionCode10] [varchar](255) NULL,
	[TransactionSubTypeCode10] [varchar](255) NULL,
	[TransactionDate10] [varchar](255) NULL,
	[TransactionAmount10] [varchar](255) NULL,
	[TransactionCode11] [varchar](255) NULL,
	[TransactionSubTypeCode11] [varchar](255) NULL,
	[TransactionDate11] [varchar](255) NULL,
	[TransactionAmount11] [varchar](255) NULL,
	[TransactionCode12] [varchar](255) NULL,
	[TransactionSubTypeCode12] [varchar](255) NULL,
	[TransactionDate12] [varchar](255) NULL,
	[TransactionAmount12] [varchar](255) NULL,
	[TransactionCode13] [varchar](255) NULL,
	[TransactionSubTypeCode13] [varchar](255) NULL,
	[TransactionDate13] [varchar](255) NULL,
	[TransactionAmount13] [varchar](255) NULL,
	[TransactionCode14] [varchar](255) NULL,
	[TransactionSubTypeCode14] [varchar](255) NULL,
	[TransactionDate14] [varchar](255) NULL,
	[TransactionAmount14] [varchar](255) NULL,
	[TransactionCode15] [varchar](255) NULL,
	[TransactionSubTypeCode15] [varchar](255) NULL,
	[TransactionDate15] [varchar](255) NULL,
	[TransactionAmount15] [varchar](255) NULL,
	[TransactionCode16] [varchar](255) NULL,
	[TransactionSubTypeCode16] [varchar](255) NULL,
	[TransactionDate16] [varchar](255) NULL,
	[TransactionAmount16] [varchar](255) NULL,
	[TransactionCode17] [varchar](255) NULL,
	[TransactionSubTypeCode17] [varchar](255) NULL,
	[TransactionDate17] [varchar](255) NULL,
	[TransactionAmount17] [varchar](255) NULL,
	[TransactionCode18] [varchar](255) NULL,
	[TransactionSubTypeCode18] [varchar](255) NULL,
	[TransactionDate18] [varchar](255) NULL,
	[TransactionAmount18] [varchar](255) NULL,
	[TransactionCode19] [varchar](255) NULL,
	[TransactionSubTypeCode19] [varchar](255) NULL,
	[TransactionDate19] [varchar](255) NULL,
	[TransactionAmount19] [varchar](255) NULL,
	[TransactionCode20] [varchar](255) NULL,
	[TransactionSubTypeCode20] [varchar](255) NULL,
	[TransactionDate20] [varchar](255) NULL,
	[TransactionAmount20] [varchar](255) NULL,
	[TransactionCode21] [varchar](255) NULL,
	[TransactionSubTypeCode21] [varchar](255) NULL,
	[TransactionDate21] [varchar](255) NULL,
	[TransactionAmount21] [varchar](255) NULL,
	[TransactionCode22] [varchar](255) NULL,
	[TransactionSubTypeCode22] [varchar](255) NULL,
	[TransactionDate22] [varchar](255) NULL,
	[TransactionAmount22] [varchar](255) NULL,
	[TransactionCode23] [varchar](255) NULL,
	[TransactionSubTypeCode23] [varchar](255) NULL,
	[TransactionDate23] [varchar](255) NULL,
	[TransactionAmount23] [varchar](255) NULL,
	[TransactionCode24] [varchar](255) NULL,
	[TransactionSubTypeCode24] [varchar](255) NULL,
	[TransactionDate24] [varchar](255) NULL,
	[TransactionAmount24] [varchar](255) NULL,
	[TransactionCode25] [varchar](255) NULL,
	[TransactionSubTypeCode25] [varchar](255) NULL,
	[TransactionDate25] [varchar](255) NULL,
	[TransactionAmount25] [varchar](255) NULL,
	[TransactionCode26] [varchar](255) NULL,
	[TransactionSubTypeCode26] [varchar](255) NULL,
	[TransactionDate26] [varchar](255) NULL,
	[TransactionAmount26] [varchar](255) NULL,
	[TransactionCode27] [varchar](255) NULL,
	[TransactionSubTypeCode27] [varchar](255) NULL,
	[TransactionDate27] [varchar](255) NULL,
	[TransactionAmount27] [varchar](255) NULL,
	[TransactionCode28] [varchar](255) NULL,
	[TransactionSubTypeCode28] [varchar](255) NULL,
	[TransactionDate28] [varchar](255) NULL,
	[TransactionAmount28] [varchar](255) NULL,
	[TransactionCode29] [varchar](255) NULL,
	[TransactionSubTypeCode29] [varchar](255) NULL,
	[TransactionDate29] [varchar](255) NULL,
	[TransactionAmount29] [varchar](255) NULL,
	[TransactionCode30] [varchar](255) NULL,
	[TransactionSubTypeCode30] [varchar](255) NULL,
	[TransactionDate30] [varchar](255) NULL,
	[TransactionAmount30] [varchar](255) NULL,
	[TransactionCode31] [varchar](255) NULL,
	[TransactionSubTypeCode31] [varchar](255) NULL,
	[TransactionDate31] [varchar](255) NULL,
	[TransactionAmount31] [varchar](255) NULL,
	[TransactionCode32] [varchar](255) NULL,
	[TransactionSubTypeCode32] [varchar](255) NULL,
	[TransactionDate32] [varchar](255) NULL,
	[TransactionAmount32] [varchar](255) NULL,
	[TransactionCode33] [varchar](255) NULL,
	[TransactionSubTypeCode33] [varchar](255) NULL,
	[TransactionDate33] [varchar](255) NULL,
	[TransactionAmount33] [varchar](255) NULL,
	[TransactionCode34] [varchar](255) NULL,
	[TransactionSubTypeCode34] [varchar](255) NULL,
	[TransactionDate34] [varchar](255) NULL,
	[TransactionAmount34] [varchar](255) NULL,
	[TransactionCode35] [varchar](255) NULL,
	[TransactionSubTypeCode35] [varchar](255) NULL,
	[TransactionDate35] [varchar](255) NULL,
	[TransactionAmount35] [varchar](255) NULL,
	[TransactionCode36] [varchar](255) NULL,
	[TransactionSubTypeCode36] [varchar](255) NULL,
	[TransactionDate36] [varchar](255) NULL,
	[TransactionAmount36] [varchar](255) NULL,
	[TransactionCode37] [varchar](255) NULL,
	[TransactionSubTypeCode37] [varchar](255) NULL,
	[TransactionDate37] [varchar](255) NULL,
	[TransactionAmount37] [varchar](255) NULL,
	[TransactionCode38] [varchar](255) NULL,
	[TransactionSubTypeCode38] [varchar](255) NULL,
	[TransactionDate38] [varchar](255) NULL,
	[TransactionAmount38] [varchar](255) NULL,
	[TransactionCode39] [varchar](255) NULL,
	[TransactionSubTypeCode39] [varchar](255) NULL,
	[TransactionDate39] [varchar](255) NULL,
	[TransactionAmount39] [varchar](255) NULL,
	[TransactionCode40] [varchar](255) NULL,
	[TransactionSubTypeCode40] [varchar](255) NULL,
	[TransactionDate40] [varchar](255) NULL,
	[TransactionAmount40] [varchar](255) NULL,
	[TransactionCode41] [varchar](255) NULL,
	[TransactionSubTypeCode41] [varchar](255) NULL,
	[TransactionDate41] [varchar](255) NULL,
	[TransactionAmount41] [varchar](255) NULL,
	[TransactionCode42] [varchar](255) NULL,
	[TransactionSubTypeCode42] [varchar](255) NULL,
	[TransactionDate42] [varchar](255) NULL,
	[TransactionAmount42] [varchar](255) NULL,
	[TransactionCode43] [varchar](255) NULL,
	[TransactionSubTypeCode43] [varchar](255) NULL,
	[TransactionDate43] [varchar](255) NULL,
	[TransactionAmount43] [varchar](255) NULL,
	[TransactionCode44] [varchar](255) NULL,
	[TransactionSubTypeCode44] [varchar](255) NULL,
	[TransactionDate44] [varchar](255) NULL,
	[TransactionAmount44] [varchar](255) NULL,
	[TransactionCode45] [varchar](255) NULL,
	[TransactionSubTypeCode45] [varchar](255) NULL,
	[TransactionDate45] [varchar](255) NULL,
	[TransactionAmount45] [varchar](255) NULL,
	[TransactionCode46] [varchar](255) NULL,
	[TransactionSubTypeCode46] [varchar](255) NULL,
	[TransactionDate46] [varchar](255) NULL,
	[TransactionAmount46] [varchar](255) NULL,
	[TransactionCode47] [varchar](255) NULL,
	[TransactionSubTypeCode47] [varchar](255) NULL,
	[TransactionDate47] [varchar](255) NULL,
	[TransactionAmount47] [varchar](255) NULL,
	[TransactionCode48] [varchar](255) NULL,
	[TransactionSubTypeCode48] [varchar](255) NULL,
	[TransactionDate48] [varchar](255) NULL,
	[TransactionAmount48] [varchar](255) NULL,
	[TransactionCode49] [varchar](255) NULL,
	[TransactionSubTypeCode49] [varchar](255) NULL,
	[TransactionDate49] [varchar](255) NULL,
	[TransactionAmount49] [varchar](255) NULL,
	[TransactionCode50] [varchar](255) NULL,
	[TransactionSubTypeCode50] [varchar](255) NULL,
	[TransactionDate50] [varchar](255) NULL,
	[TransactionAmount50] [varchar](255) NULL,
	[Blank] [varchar](255) NULL,
	[ReturnCode] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Staging].[InclvlPecoData]    Script Date: 11/8/2019 4:04:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Staging].[InclvlPecoData](
	[BillAccount] [varchar](255) NULL,
	[CapTierCode] [varchar](255) NULL,
	[Filler] [varchar](255) NULL,
	[IncomeLevel] [varchar](255) NULL,
	[CustomerNumber] [varchar](255) NULL,
	[Name] [varchar](255) NULL,
	[Address] [varchar](255) NULL,
	[Phone] [varchar](255) NULL,
	[LIHEAPRcvd] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Staging].[RateAcmfPecoData]    Script Date: 11/8/2019 4:04:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Staging].[RateAcmfPecoData](
	[BillAccount] [varchar](255) NULL,
	[CustomerNumber] [varchar](255) NULL,
	[ReadDate] [varchar](255) NULL,
	[Usage] [varchar](255) NULL,
	[ReadingType] [varchar](255) NULL,
	[Rate] [varchar](255) NULL,
	[RateStatus] [varchar](255) NULL,
	[Revenue] [varchar](255) NULL,
	[NumberOfDays] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Staging].[ServiceAcmfPecoData]    Script Date: 11/8/2019 4:04:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Staging].[ServiceAcmfPecoData](
	[BillAccount] [varchar](255) NULL,
	[CustomerNumber] [varchar](255) NULL,
	[AccountStatus] [varchar](255) NULL,
	[Premise] [varchar](255) NULL,
	[Name] [varchar](255) NULL,
	[Address] [varchar](255) NULL,
	[AddressOverflow] [varchar](255) NULL,
	[AddressModifier] [varchar](255) NULL,
	[PostOffice] [varchar](255) NULL,
	[ZipCode] [varchar](255) NULL,
	[Phone] [varchar](255) NULL,
	[DistrictPoliticalSub] [varchar](255) NULL,
	[Rate1] [varchar](255) NULL,
	[RateStatus1] [varchar](255) NULL,
	[Rate2] [varchar](255) NULL,
	[RateStatus2] [varchar](255) NULL,
	[Rate3] [varchar](255) NULL,
	[RateStatus3] [varchar](255) NULL,
	[Rate4] [varchar](255) NULL,
	[RateStatus4] [varchar](255) NULL,
	[Rate5] [varchar](255) NULL,
	[RateStatus5] [varchar](255) NULL,
	[Return] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Technician].[LkpSlotType]    Script Date: 11/8/2019 4:04:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Technician].[LkpSlotType](
	[SlotTypeGuid] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_LkpSlotType] PRIMARY KEY CLUSTERED 
(
	[SlotTypeGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Technician].[ScheduleDay]    Script Date: 11/8/2019 4:04:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Technician].[ScheduleDay](
	[ScheduleDayGuid] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[TechnicianGuid] [uniqueidentifier] NOT NULL,
	[Slot0Guid] [uniqueidentifier] NULL,
	[Slot1Guid] [uniqueidentifier] NULL,
	[Slot2Guid] [uniqueidentifier] NULL,
	[Slot3Guid] [uniqueidentifier] NULL,
	[Slot4Guid] [uniqueidentifier] NULL,
	[Slot5Guid] [uniqueidentifier] NULL,
	[Slot6Guid] [uniqueidentifier] NULL,
	[Slot7Guid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_DaySlot] PRIMARY KEY CLUSTERED 
(
	[ScheduleDayGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Technician].[Slot]    Script Date: 11/8/2019 4:04:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Technician].[Slot](
	[SlotGuid] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NULL,
	[SlotPosition] [int] NOT NULL,
	[SlotTypeId] [uniqueidentifier] NOT NULL,
	[Filled] [bit] NOT NULL,
	[LeadGuid] [uniqueidentifier] NULL,
	[TechnicianGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Slot] PRIMARY KEY CLUSTERED 
(
	[SlotGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Technician].[Technician]    Script Date: 11/8/2019 4:04:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Technician].[Technician](
	[TechnicianGuid] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[StartAddressGuid] [uniqueidentifier] NULL,
	[EndAddressGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Technician] PRIMARY KEY CLUSTERED 
(
	[TechnicianGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UsageRaw].[LkpRateCodes]    Script Date: 11/8/2019 4:04:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UsageRaw].[LkpRateCodes](
	[ProgramGuid] [uniqueidentifier] NOT NULL,
	[RateCode] [varchar](50) NOT NULL,
	[RateDescription] [varchar](100) NOT NULL,
	[BaseRate] [varchar](5) NOT NULL,
	[CAPTier] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [UsageRaw].[UsageRawPeco]    Script Date: 11/8/2019 4:04:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UsageRaw].[UsageRawPeco](
	[CustomerGuid] [uniqueidentifier] NOT NULL,
	[ReadDate] [datetime2](7) NULL,
	[Usage] [decimal](13, 2) NULL,
	[ReadingType] [nchar](1) NULL,
	[Rate] [nchar](3) NULL,
	[RateStatus] [nvarchar](5) NULL,
	[Revenue] [decimal](13, 2) NULL,
	[NumberOfDays] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Index [IX_Address_StreetAddressCityStateZipLatLong]    Script Date: 11/8/2019 4:04:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Address_StreetAddressCityStateZipLatLong] ON [dbo].[Address]
(
	[AddressGuid] ASC
)
INCLUDE([StreetAddress],[City],[State],[Zip],[Latitude],[Longitude]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [CDCQueue].[CDCQueue] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [CDCQueue].[CDCQueueArchive] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [Contact].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_ContactType] FOREIGN KEY([ContactTypeGuid])
REFERENCES [Contact].[LkpContactType] ([ContactTypeGuid])
GO
ALTER TABLE [Contact].[Contact] CHECK CONSTRAINT [FK_Contact_ContactType]
GO
ALTER TABLE [Contact].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_Customer] FOREIGN KEY([CustomerGuid])
REFERENCES [Customer].[Customer] ([CustomerGuid])
GO
ALTER TABLE [Contact].[Contact] CHECK CONSTRAINT [FK_Contact_Customer]
GO
ALTER TABLE [Contact].[Email]  WITH CHECK ADD  CONSTRAINT [FK_Email_Contact] FOREIGN KEY([ContactGuid])
REFERENCES [Contact].[Contact] ([ContactGuid])
GO
ALTER TABLE [Contact].[Email] CHECK CONSTRAINT [FK_Email_Contact]
GO
ALTER TABLE [Contact].[Phone]  WITH CHECK ADD  CONSTRAINT [FK_Phone_Contact] FOREIGN KEY([ContactGuid])
REFERENCES [Contact].[Contact] ([ContactGuid])
GO
ALTER TABLE [Contact].[Phone] CHECK CONSTRAINT [FK_Phone_Contact]
GO
ALTER TABLE [Customer].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_AccountStatus] FOREIGN KEY([AccountStatusGuid])
REFERENCES [Customer].[LkpAccountStatus] ([AccountStatusGuid])
GO
ALTER TABLE [Customer].[Account] CHECK CONSTRAINT [FK_Account_AccountStatus]
GO
ALTER TABLE [Customer].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Customer] FOREIGN KEY([CustomerGuid])
REFERENCES [Customer].[Customer] ([CustomerGuid])
GO
ALTER TABLE [Customer].[Account] CHECK CONSTRAINT [FK_Account_Customer]
GO
ALTER TABLE [Customer].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_AddressType] FOREIGN KEY([AddressTypeGuid])
REFERENCES [Customer].[LkpAddressType] ([AddressTypeGuid])
GO
ALTER TABLE [Customer].[Address] CHECK CONSTRAINT [FK_Address_AddressType]
GO
ALTER TABLE [Customer].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_Counties] FOREIGN KEY([CountyGuid])
REFERENCES [Customer].[LkpCounties] ([CountyGuid])
GO
ALTER TABLE [Customer].[Address] CHECK CONSTRAINT [FK_Address_Counties]
GO
ALTER TABLE [Customer].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_Premise] FOREIGN KEY([PremiseGuid])
REFERENCES [Customer].[Premise] ([PremiseGuid])
GO
ALTER TABLE [Customer].[Address] CHECK CONSTRAINT [FK_Address_Premise]
GO
ALTER TABLE [Customer].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_AccountType] FOREIGN KEY([CustomerAccountTypeGuid])
REFERENCES [Customer].[LkpAccountType] ([AccountTypeGuid])
GO
ALTER TABLE [Customer].[Customer] CHECK CONSTRAINT [FK_Customer_AccountType]
GO
ALTER TABLE [Customer].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Program] FOREIGN KEY([ProgramGuid])
REFERENCES [Program].[Program] ([ProgramGuid])
GO
ALTER TABLE [Customer].[Customer] CHECK CONSTRAINT [FK_Customer_Program]
GO
ALTER TABLE [Customer].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_SubProgram] FOREIGN KEY([SubProgramGuid])
REFERENCES [Program].[Subprogram] ([SubProgramGuid])
GO
ALTER TABLE [Customer].[Customer] CHECK CONSTRAINT [FK_Customer_SubProgram]
GO
ALTER TABLE [Customer].[Demographic]  WITH CHECK ADD  CONSTRAINT [FK_Demographic_Customer] FOREIGN KEY([CustomerGuid])
REFERENCES [Customer].[Customer] ([CustomerGuid])
GO
ALTER TABLE [Customer].[Demographic] CHECK CONSTRAINT [FK_Demographic_Customer]
GO
ALTER TABLE [Customer].[Lead]  WITH CHECK ADD  CONSTRAINT [FK_Lead_AuditType] FOREIGN KEY([QualifiedAuditTypeGuid])
REFERENCES [Customer].[LkpAuditType] ([AuditTypeGuid])
GO
ALTER TABLE [Customer].[Lead] CHECK CONSTRAINT [FK_Lead_AuditType]
GO
ALTER TABLE [Customer].[Lead]  WITH CHECK ADD  CONSTRAINT [FK_Lead_LeadStatus] FOREIGN KEY([LeadStatusGuid])
REFERENCES [Customer].[LkpLeadStatus] ([LeadStatusGuid])
GO
ALTER TABLE [Customer].[Lead] CHECK CONSTRAINT [FK_Lead_LeadStatus]
GO
ALTER TABLE [Customer].[LkpCounties]  WITH CHECK ADD  CONSTRAINT [FK_Counties_CountyType] FOREIGN KEY([CountyTypeGuid])
REFERENCES [Customer].[LkpCountyType] ([CountyTypeGuid])
GO
ALTER TABLE [Customer].[LkpCounties] CHECK CONSTRAINT [FK_Counties_CountyType]
GO
ALTER TABLE [Customer].[Premise]  WITH CHECK ADD  CONSTRAINT [FK_Premise_Customer] FOREIGN KEY([CustomerGuid])
REFERENCES [Customer].[Customer] ([CustomerGuid])
GO
ALTER TABLE [Customer].[Premise] CHECK CONSTRAINT [FK_Premise_Customer]
GO
ALTER TABLE [Customer].[Premise]  WITH CHECK ADD  CONSTRAINT [FK_Premise_PremiseType] FOREIGN KEY([PremiseTypeGuid])
REFERENCES [Customer].[LkpPremiseType] ([PremiseTypeGuid])
GO
ALTER TABLE [Customer].[Premise] CHECK CONSTRAINT [FK_Premise_PremiseType]
GO
ALTER TABLE [Customer].[Usage]  WITH CHECK ADD  CONSTRAINT [FK_Usage_Customer] FOREIGN KEY([CustomerGuid])
REFERENCES [Customer].[Customer] ([CustomerGuid])
GO
ALTER TABLE [Customer].[Usage] CHECK CONSTRAINT [FK_Usage_Customer]
GO
ALTER TABLE [Program].[Subprogram]  WITH CHECK ADD  CONSTRAINT [FK_SubProgram_Program] FOREIGN KEY([ProgramGuid])
REFERENCES [Program].[Program] ([ProgramGuid])
GO
ALTER TABLE [Program].[Subprogram] CHECK CONSTRAINT [FK_SubProgram_Program]
GO
ALTER TABLE [Technician].[ScheduleDay]  WITH CHECK ADD  CONSTRAINT [FK_DaySlot_Slot0Guid] FOREIGN KEY([Slot0Guid])
REFERENCES [Technician].[Slot] ([SlotGuid])
GO
ALTER TABLE [Technician].[ScheduleDay] CHECK CONSTRAINT [FK_DaySlot_Slot0Guid]
GO
ALTER TABLE [Technician].[ScheduleDay]  WITH CHECK ADD  CONSTRAINT [FK_DaySlot_Slot1Guid] FOREIGN KEY([Slot1Guid])
REFERENCES [Technician].[Slot] ([SlotGuid])
GO
ALTER TABLE [Technician].[ScheduleDay] CHECK CONSTRAINT [FK_DaySlot_Slot1Guid]
GO
ALTER TABLE [Technician].[ScheduleDay]  WITH CHECK ADD  CONSTRAINT [FK_DaySlot_Slot2Guid] FOREIGN KEY([Slot2Guid])
REFERENCES [Technician].[Slot] ([SlotGuid])
GO
ALTER TABLE [Technician].[ScheduleDay] CHECK CONSTRAINT [FK_DaySlot_Slot2Guid]
GO
ALTER TABLE [Technician].[ScheduleDay]  WITH CHECK ADD  CONSTRAINT [FK_DaySlot_Slot3Guid] FOREIGN KEY([Slot3Guid])
REFERENCES [Technician].[Slot] ([SlotGuid])
GO
ALTER TABLE [Technician].[ScheduleDay] CHECK CONSTRAINT [FK_DaySlot_Slot3Guid]
GO
ALTER TABLE [Technician].[ScheduleDay]  WITH CHECK ADD  CONSTRAINT [FK_DaySlot_Slot4Guid] FOREIGN KEY([Slot4Guid])
REFERENCES [Technician].[Slot] ([SlotGuid])
GO
ALTER TABLE [Technician].[ScheduleDay] CHECK CONSTRAINT [FK_DaySlot_Slot4Guid]
GO
ALTER TABLE [Technician].[ScheduleDay]  WITH CHECK ADD  CONSTRAINT [FK_DaySlot_Slot5Guid] FOREIGN KEY([Slot5Guid])
REFERENCES [Technician].[Slot] ([SlotGuid])
GO
ALTER TABLE [Technician].[ScheduleDay] CHECK CONSTRAINT [FK_DaySlot_Slot5Guid]
GO
ALTER TABLE [Technician].[ScheduleDay]  WITH CHECK ADD  CONSTRAINT [FK_DaySlot_Slot6Guid] FOREIGN KEY([Slot6Guid])
REFERENCES [Technician].[Slot] ([SlotGuid])
GO
ALTER TABLE [Technician].[ScheduleDay] CHECK CONSTRAINT [FK_DaySlot_Slot6Guid]
GO
ALTER TABLE [Technician].[ScheduleDay]  WITH CHECK ADD  CONSTRAINT [FK_DaySlot_Slot7Guid] FOREIGN KEY([Slot7Guid])
REFERENCES [Technician].[Slot] ([SlotGuid])
GO
ALTER TABLE [Technician].[ScheduleDay] CHECK CONSTRAINT [FK_DaySlot_Slot7Guid]
GO
ALTER TABLE [Technician].[Slot]  WITH CHECK ADD  CONSTRAINT [FK_Slot_LkpSlotType] FOREIGN KEY([SlotTypeId])
REFERENCES [Technician].[LkpSlotType] ([SlotTypeGuid])
GO
ALTER TABLE [Technician].[Slot] CHECK CONSTRAINT [FK_Slot_LkpSlotType]
GO
ALTER TABLE [Technician].[Slot]  WITH CHECK ADD  CONSTRAINT [FK_Slot_Tech] FOREIGN KEY([TechnicianGuid])
REFERENCES [Technician].[Technician] ([TechnicianGuid])
GO
ALTER TABLE [Technician].[Slot] CHECK CONSTRAINT [FK_Slot_Tech]
GO
ALTER TABLE [Technician].[Technician]  WITH CHECK ADD  CONSTRAINT [FK_Technician_EndLocation] FOREIGN KEY([EndAddressGuid])
REFERENCES [dbo].[Address] ([AddressGuid])
GO
ALTER TABLE [Technician].[Technician] CHECK CONSTRAINT [FK_Technician_EndLocation]
GO
ALTER TABLE [Technician].[Technician]  WITH CHECK ADD  CONSTRAINT [FK_Technician_StartLocation] FOREIGN KEY([StartAddressGuid])
REFERENCES [dbo].[Address] ([AddressGuid])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Technician].[Technician] CHECK CONSTRAINT [FK_Technician_StartLocation]
GO
ALTER TABLE [UsageRaw].[UsageRawPeco]  WITH CHECK ADD  CONSTRAINT [FK_UsageRawPeco_Account] FOREIGN KEY([CustomerGuid])
REFERENCES [Customer].[Customer] ([CustomerGuid])
GO
ALTER TABLE [UsageRaw].[UsageRawPeco] CHECK CONSTRAINT [FK_UsageRawPeco_Account]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date of last day billed' , @level0type=N'SCHEMA',@level0name=N'UsageRaw', @level1type=N'TABLE',@level1name=N'UsageRawPeco', @level2type=N'COLUMN',@level2name=N'ReadDate'
GO
USE [master]
GO
ALTER DATABASE [CMC-SFDC_TEST] SET  READ_WRITE 
GO
