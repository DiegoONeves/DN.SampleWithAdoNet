USE master

GO

CREATE DATABASE SampleWithAdoNetDB 

GO 

USE SampleWithAdoNetDB

GO

CREATE TABLE [dbo].[USER]
(
   [ID]			UNIQUEIDENTIFIER	NOT NULL,
   [NAME]		VARCHAR(255)		NOT NULL,
   [EMAIL]		VARCHAR(255)		NOT NULL,
   [PASSWORD]	VARCHAR(255)		NOT NULL
)
