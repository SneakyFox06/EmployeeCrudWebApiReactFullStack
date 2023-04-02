CREATE TABLE [dbo].[Department] (
    [DId]            INT           IDENTITY (1, 1) NOT NULL,
    [DepartmentName] NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([DId] ASC)
);

CREATE TABLE [dbo].[Employee] (
    [EId]          INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]    NVARCHAR (50)  NULL,
    [LastName]     NVARCHAR (50)  NULL,
    [Email]        NVARCHAR (100) NULL,
    [DOB]          DATE           NULL,
    [Age]          AS             ((CONVERT([int],CONVERT([char](8),getdate(),(112)))-CONVERT([int],CONVERT([char](8),[dob],(112))))/(10000)),
    [DepartmentId] INT            NULL,
    [Salary]       INT            NULL,
    PRIMARY KEY CLUSTERED ([EId] ASC),
    FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Department] ([DId])
);

