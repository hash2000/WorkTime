
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/02/2014 02:00:53
-- Generated from EDMX file: D:\projects\Web\WorkTime\WorkTime\Models\WorkTime.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Contributors];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Post_Staff]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Staffs] DROP CONSTRAINT [FK_Post_Staff];
GO
IF OBJECT_ID(N'[dbo].[FK_Post_TypeStaff]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Staffs] DROP CONSTRAINT [FK_Post_TypeStaff];
GO
IF OBJECT_ID(N'[dbo].[FK_Reg_NormStaff]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Staffs] DROP CONSTRAINT [FK_Reg_NormStaff];
GO
IF OBJECT_ID(N'[dbo].[FK_Staff_Vacation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Vacations] DROP CONSTRAINT [FK_Staff_Vacation];
GO
IF OBJECT_ID(N'[dbo].[FK_TimeNormType_TimeNormOfMonth]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TimeNormOfMonths] DROP CONSTRAINT [FK_TimeNormType_TimeNormOfMonth];
GO
IF OBJECT_ID(N'[dbo].[FK_Vacation_TypeVacation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Vacations] DROP CONSTRAINT [FK_Vacation_TypeVacation];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Holidays]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Holidays];
GO
IF OBJECT_ID(N'[dbo].[Posts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Posts];
GO
IF OBJECT_ID(N'[dbo].[PostTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PostTypes];
GO
IF OBJECT_ID(N'[dbo].[RegNorms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RegNorms];
GO
IF OBJECT_ID(N'[dbo].[Staffs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Staffs];
GO
IF OBJECT_ID(N'[dbo].[TimeNormOfMonths]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TimeNormOfMonths];
GO
IF OBJECT_ID(N'[dbo].[TimeNormTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TimeNormTypes];
GO
IF OBJECT_ID(N'[dbo].[Vacations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Vacations];
GO
IF OBJECT_ID(N'[dbo].[VacationTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VacationTypes];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Holidays'
CREATE TABLE [dbo].[Holidays] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(128)  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NULL,
    [IsShiftOfYear] bit  NOT NULL,
    [IsHoliday] bit  NOT NULL,
    [Description] nvarchar(max)  NULL
);
GO

-- Creating table 'Posts'
CREATE TABLE [dbo].[Posts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'PostTypes'
CREATE TABLE [dbo].[PostTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'RegNorms'
CREATE TABLE [dbo].[RegNorms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(250)  NOT NULL,
    [Norm] float  NOT NULL
);
GO

-- Creating table 'Staffs'
CREATE TABLE [dbo].[Staffs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Surname] char(128)  NOT NULL,
    [Name] nvarchar(128)  NOT NULL,
    [PatronymicName] nvarchar(128)  NOT NULL,
    [TabelNumber] smallint  NOT NULL,
    [PostId] int  NOT NULL,
    [PostTypeId] int  NOT NULL,
    [StartDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [Gender] smallint  NOT NULL,
    [WorkgraphsId] int  NOT NULL,
    [RegNormsId] int  NULL,
    [RegNormId] int  NOT NULL
);
GO

-- Creating table 'TimeNormOfMonths'
CREATE TABLE [dbo].[TimeNormOfMonths] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Norm] float  NOT NULL,
    [TimeNormTypeId] int  NOT NULL
);
GO

-- Creating table 'TimeNormTypes'
CREATE TABLE [dbo].[TimeNormTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'Vacations'
CREATE TABLE [dbo].[Vacations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StaffId] int  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [VacationTypeId] int  NOT NULL
);
GO

-- Creating table 'VacationTypes'
CREATE TABLE [dbo].[VacationTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(128)  NOT NULL,
    [Label] nvarchar(4)  NOT NULL,
    [StaffId] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'Permissions'
CREATE TABLE [dbo].[Permissions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Holidays'
ALTER TABLE [dbo].[Holidays]
ADD CONSTRAINT [PK_Holidays]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [PK_Posts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PostTypes'
ALTER TABLE [dbo].[PostTypes]
ADD CONSTRAINT [PK_PostTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RegNorms'
ALTER TABLE [dbo].[RegNorms]
ADD CONSTRAINT [PK_RegNorms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Staffs'
ALTER TABLE [dbo].[Staffs]
ADD CONSTRAINT [PK_Staffs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TimeNormOfMonths'
ALTER TABLE [dbo].[TimeNormOfMonths]
ADD CONSTRAINT [PK_TimeNormOfMonths]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TimeNormTypes'
ALTER TABLE [dbo].[TimeNormTypes]
ADD CONSTRAINT [PK_TimeNormTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Vacations'
ALTER TABLE [dbo].[Vacations]
ADD CONSTRAINT [PK_Vacations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VacationTypes'
ALTER TABLE [dbo].[VacationTypes]
ADD CONSTRAINT [PK_VacationTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Permissions'
ALTER TABLE [dbo].[Permissions]
ADD CONSTRAINT [PK_Permissions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PostId] in table 'Staffs'
ALTER TABLE [dbo].[Staffs]
ADD CONSTRAINT [FK_Post_Staff]
    FOREIGN KEY ([PostId])
    REFERENCES [dbo].[Posts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Post_Staff'
CREATE INDEX [IX_FK_Post_Staff]
ON [dbo].[Staffs]
    ([PostId]);
GO

-- Creating foreign key on [PostTypeId] in table 'Staffs'
ALTER TABLE [dbo].[Staffs]
ADD CONSTRAINT [FK_Post_TypeStaff]
    FOREIGN KEY ([PostTypeId])
    REFERENCES [dbo].[PostTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Post_TypeStaff'
CREATE INDEX [IX_FK_Post_TypeStaff]
ON [dbo].[Staffs]
    ([PostTypeId]);
GO

-- Creating foreign key on [RegNormId] in table 'Staffs'
ALTER TABLE [dbo].[Staffs]
ADD CONSTRAINT [FK_Reg_NormStaff]
    FOREIGN KEY ([RegNormId])
    REFERENCES [dbo].[RegNorms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Reg_NormStaff'
CREATE INDEX [IX_FK_Reg_NormStaff]
ON [dbo].[Staffs]
    ([RegNormId]);
GO

-- Creating foreign key on [StaffId] in table 'Vacations'
ALTER TABLE [dbo].[Vacations]
ADD CONSTRAINT [FK_Staff_Vacation]
    FOREIGN KEY ([StaffId])
    REFERENCES [dbo].[Staffs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Staff_Vacation'
CREATE INDEX [IX_FK_Staff_Vacation]
ON [dbo].[Vacations]
    ([StaffId]);
GO

-- Creating foreign key on [TimeNormTypeId] in table 'TimeNormOfMonths'
ALTER TABLE [dbo].[TimeNormOfMonths]
ADD CONSTRAINT [FK_TimeNormType_TimeNormOfMonth]
    FOREIGN KEY ([TimeNormTypeId])
    REFERENCES [dbo].[TimeNormTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TimeNormType_TimeNormOfMonth'
CREATE INDEX [IX_FK_TimeNormType_TimeNormOfMonth]
ON [dbo].[TimeNormOfMonths]
    ([TimeNormTypeId]);
GO

-- Creating foreign key on [VacationTypeId] in table 'Vacations'
ALTER TABLE [dbo].[Vacations]
ADD CONSTRAINT [FK_Vacation_TypeVacation]
    FOREIGN KEY ([VacationTypeId])
    REFERENCES [dbo].[VacationTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Vacation_TypeVacation'
CREATE INDEX [IX_FK_Vacation_TypeVacation]
ON [dbo].[Vacations]
    ([VacationTypeId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------