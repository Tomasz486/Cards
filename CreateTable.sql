CREATE TABLE [dbo].[Cards] (
    [Id]            VARCHAR (32) NOT NULL,
    [Pin]           VARCHAR (10) NULL,
    [SerialNumber]  VARCHAR (10) NULL,
    [AccountNumber] VARCHAR (10) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

