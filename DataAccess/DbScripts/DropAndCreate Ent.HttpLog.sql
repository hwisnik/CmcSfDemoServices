USE [EnterpriseLogging]
GO

/****** Object: Table [Ent].[HttpLog] Script Date: 4/23/2018 11:36:50 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [Ent].[HttpLog];


GO
CREATE TABLE [Ent].[HttpLog] (
    [Id]              INT              IDENTITY (1, 1) NOT NULL,
    [CallerIdentity]  VARCHAR (50)     NULL,
    [CorrelationId]   UNIQUEIDENTIFIER NULL,
    [RequestUri]      VARCHAR (MAX)    NOT NULL,
    [RequestBody]     VARCHAR (MAX)    NOT NULL,
    [RequestHeaders]  VARCHAR (MAX)    NOT NULL,
    [RequestLength]   BIGINT           NOT NULL,
	[RequestTimestamp] DATETIME2		NOT NULL,
    [Response]        VARCHAR (MAX)    NOT NULL,
    [ResponseHeaders] VARCHAR (MAX)    NOT NULL,
    [ResponseLength]  BIGINT           NOT NULL,
	[ResponseDurationInSeconds] FLOAT  NULL,
    [Verb]            VARCHAR (6)      NOT NULL,
    [StatusCode]      INT              NOT NULL,
    [ReasonPhrase]    VARCHAR (100)    NOT NULL
);


