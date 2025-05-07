
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/01/2025 23:19:44
-- Generated from EDMX file: C:\Users\User\Downloads\finalC#\GestionRentasMunicipal\Diagram\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ProyectoHacienda];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CertificateUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CertificateSet] DROP CONSTRAINT [FK_CertificateUser];
GO
IF OBJECT_ID(N'[dbo].[FK_RolPermissionRol]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RolFormPermissionSet] DROP CONSTRAINT [FK_RolPermissionRol];
GO
IF OBJECT_ID(N'[dbo].[FK_RolPermissionPermission]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RolFormPermissionSet] DROP CONSTRAINT [FK_RolPermissionPermission];
GO
IF OBJECT_ID(N'[dbo].[FK_ModuleFormModule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModuleForms] DROP CONSTRAINT [FK_ModuleFormModule];
GO
IF OBJECT_ID(N'[dbo].[FK_FormModuleForm]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModuleForms] DROP CONSTRAINT [FK_FormModuleForm];
GO
IF OBJECT_ID(N'[dbo].[FK_DebtUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Debts] DROP CONSTRAINT [FK_DebtUser];
GO
IF OBJECT_ID(N'[dbo].[FK_DebtEstablishment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Debts] DROP CONSTRAINT [FK_DebtEstablishment];
GO
IF OBJECT_ID(N'[dbo].[FK_EstablishmentTypeEstablishment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Establishments] DROP CONSTRAINT [FK_EstablishmentTypeEstablishment];
GO
IF OBJECT_ID(N'[dbo].[FK_DebtBillBill]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DebtBills] DROP CONSTRAINT [FK_DebtBillBill];
GO
IF OBJECT_ID(N'[dbo].[FK_DebtBillDebt]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DebtBills] DROP CONSTRAINT [FK_DebtBillDebt];
GO
IF OBJECT_ID(N'[dbo].[FK_DebtPayment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Payments] DROP CONSTRAINT [FK_DebtPayment];
GO
IF OBJECT_ID(N'[dbo].[FK_UserContract]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Contracts] DROP CONSTRAINT [FK_UserContract];
GO
IF OBJECT_ID(N'[dbo].[FK_RentEstablishment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Contracts] DROP CONSTRAINT [FK_RentEstablishment];
GO
IF OBJECT_ID(N'[dbo].[FK_RolUserRol]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RolUsers] DROP CONSTRAINT [FK_RolUserRol];
GO
IF OBJECT_ID(N'[dbo].[FK_RolUserUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RolUsers] DROP CONSTRAINT [FK_RolUserUser];
GO
IF OBJECT_ID(N'[dbo].[FK_RolFormPermissionForm]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RolFormPermissionSet] DROP CONSTRAINT [FK_RolFormPermissionForm];
GO
IF OBJECT_ID(N'[dbo].[FK_StatementDetailStatement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StatementDetailSet] DROP CONSTRAINT [FK_StatementDetailStatement];
GO
IF OBJECT_ID(N'[dbo].[FK_StatementDetailPayment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StatementDetailSet] DROP CONSTRAINT [FK_StatementDetailPayment];
GO
IF OBJECT_ID(N'[dbo].[FK_StatementDetailDebt]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StatementDetailSet] DROP CONSTRAINT [FK_StatementDetailDebt];
GO
IF OBJECT_ID(N'[dbo].[FK_StatementDetailEstablishment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StatementDetailSet] DROP CONSTRAINT [FK_StatementDetailEstablishment];
GO
IF OBJECT_ID(N'[dbo].[FK_StatementDetailRent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StatementDetailSet] DROP CONSTRAINT [FK_StatementDetailRent];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Rols]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rols];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Permissions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Permissions];
GO
IF OBJECT_ID(N'[dbo].[Forms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Forms];
GO
IF OBJECT_ID(N'[dbo].[ModuleForms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ModuleForms];
GO
IF OBJECT_ID(N'[dbo].[Modules]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Modules];
GO
IF OBJECT_ID(N'[dbo].[Logs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Logs];
GO
IF OBJECT_ID(N'[dbo].[CertificateSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CertificateSet];
GO
IF OBJECT_ID(N'[dbo].[Paremeters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Paremeters];
GO
IF OBJECT_ID(N'[dbo].[RolFormPermissionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RolFormPermissionSet];
GO
IF OBJECT_ID(N'[dbo].[Establishments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Establishments];
GO
IF OBJECT_ID(N'[dbo].[TypeEstablishments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TypeEstablishments];
GO
IF OBJECT_ID(N'[dbo].[Debts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Debts];
GO
IF OBJECT_ID(N'[dbo].[Contracts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Contracts];
GO
IF OBJECT_ID(N'[dbo].[Payments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Payments];
GO
IF OBJECT_ID(N'[dbo].[DebtBills]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DebtBills];
GO
IF OBJECT_ID(N'[dbo].[Bills]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bills];
GO
IF OBJECT_ID(N'[dbo].[RolUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RolUsers];
GO
IF OBJECT_ID(N'[dbo].[Statements]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Statements];
GO
IF OBJECT_ID(N'[dbo].[Calculations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Calculations];
GO
IF OBJECT_ID(N'[dbo].[StatementDetailSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StatementDetailSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Rols'
CREATE TABLE [dbo].[Rols] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Identification] int  NOT NULL,
    [Phone] bigint  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'Permissions'
CREATE TABLE [dbo].[Permissions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'Forms'
CREATE TABLE [dbo].[Forms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'ModuleForms'
CREATE TABLE [dbo].[ModuleForms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ModuleId_Id] int  NOT NULL,
    [FormId_Id] int  NOT NULL
);
GO

-- Creating table 'Modules'
CREATE TABLE [dbo].[Modules] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Code] nvarchar(max)  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'Logs'
CREATE TABLE [dbo].[Logs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TableName] nvarchar(max)  NOT NULL,
    [PropietyName] nvarchar(max)  NOT NULL,
    [Action] nvarchar(max)  NOT NULL,
    [TimeStamp] nvarchar(max)  NOT NULL,
    [UserId] nvarchar(max)  NOT NULL,
    [OldValue] nvarchar(max)  NOT NULL,
    [NewValue] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CertificateSet'
CREATE TABLE [dbo].[CertificateSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TypeCertificate] nvarchar(max)  NOT NULL,
    [IssueDate] datetime  NOT NULL,
    [ExpirationDate] datetime  NOT NULL,
    [UserId_Id] int  NOT NULL
);
GO

-- Creating table 'Paremeters'
CREATE TABLE [dbo].[Paremeters] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Value] decimal(15,2)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'RolFormPermissionSet'
CREATE TABLE [dbo].[RolFormPermissionSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RolId_Id] int  NOT NULL,
    [PermissionId_Id] int  NOT NULL,
    [FormId_Id] int  NOT NULL
);
GO

-- Creating table 'Establishments'
CREATE TABLE [dbo].[Establishments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nit] int  NOT NULL,
    [State] bit  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [IsDeleted] nvarchar(max)  NOT NULL,
    [TypeEstablishmentId_Id] int  NOT NULL
);
GO

-- Creating table 'TypeEstablishments'
CREATE TABLE [dbo].[TypeEstablishments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [UVT] decimal(13,2)  NOT NULL,
    [PaymentTime] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Debts'
CREATE TABLE [dbo].[Debts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DebtDate] datetime  NOT NULL,
    [GrossValue] decimal(13,2)  NOT NULL,
    [IvaValue] decimal(13,2)  NOT NULL,
    [DebtTotal] decimal(13,2)  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [UserId_Id] int  NOT NULL,
    [EstablishmentId_Id] int  NOT NULL
);
GO

-- Creating table 'Contracts'
CREATE TABLE [dbo].[Contracts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ContractDate] datetime  NOT NULL,
    [ExpirationDate] datetime  NOT NULL,
    [SocialReason] nvarchar(max)  NOT NULL,
    [State] bit  NOT NULL,
    [UserId_Id] int  NOT NULL,
    [EstablishmentId_Id] int  NOT NULL
);
GO

-- Creating table 'Payments'
CREATE TABLE [dbo].[Payments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PaymentDate] datetime  NOT NULL,
    [AmountPaid] decimal(13,2)  NOT NULL,
    [State] bit  NOT NULL,
    [DebtId_Id] int  NOT NULL
);
GO

-- Creating table 'DebtBills'
CREATE TABLE [dbo].[DebtBills] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BillId_Id] int  NOT NULL,
    [DebtId_Id] int  NOT NULL
);
GO

-- Creating table 'Bills'
CREATE TABLE [dbo].[Bills] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Barcode] bigint  NOT NULL,
    [IssueDate] datetime  NOT NULL,
    [ExpirationDate] datetime  NOT NULL,
    [Amount] decimal(13,2)  NOT NULL,
    [State] bit  NOT NULL
);
GO

-- Creating table 'RolUsers'
CREATE TABLE [dbo].[RolUsers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RolId_Id] int  NOT NULL,
    [UserId_Id] int  NOT NULL
);
GO

-- Creating table 'Statements'
CREATE TABLE [dbo].[Statements] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [GenerationDate] nvarchar(max)  NOT NULL,
    [PeriodStart] nvarchar(max)  NOT NULL,
    [PeriodEnd] nvarchar(max)  NOT NULL,
    [TotalAmount] nvarchar(max)  NOT NULL,
    [IsDeleted] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Calculations'
CREATE TABLE [dbo].[Calculations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Formula] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'StatementDetailSet'
CREATE TABLE [dbo].[StatementDetailSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Catogory] nvarchar(max)  NOT NULL,
    [ItemName] nvarchar(max)  NOT NULL,
    [Amount] nvarchar(max)  NOT NULL,
    [Period] nvarchar(max)  NOT NULL,
    [PreviousPeriodAmount] nvarchar(max)  NOT NULL,
    [TargetAmount] nvarchar(max)  NOT NULL,
    [Variance] nvarchar(max)  NOT NULL,
    [VariancePercentage] nvarchar(max)  NOT NULL,
    [CreatedAt] nvarchar(max)  NOT NULL,
    [StatementId_Id] int  NOT NULL,
    [PaymentId_Id] int  NOT NULL,
    [DebtId_Id] int  NOT NULL,
    [EstablishmentId_Id] int  NOT NULL,
    [RentId_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Rols'
ALTER TABLE [dbo].[Rols]
ADD CONSTRAINT [PK_Rols]
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

-- Creating primary key on [Id] in table 'Forms'
ALTER TABLE [dbo].[Forms]
ADD CONSTRAINT [PK_Forms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ModuleForms'
ALTER TABLE [dbo].[ModuleForms]
ADD CONSTRAINT [PK_ModuleForms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Modules'
ALTER TABLE [dbo].[Modules]
ADD CONSTRAINT [PK_Modules]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Logs'
ALTER TABLE [dbo].[Logs]
ADD CONSTRAINT [PK_Logs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CertificateSet'
ALTER TABLE [dbo].[CertificateSet]
ADD CONSTRAINT [PK_CertificateSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Paremeters'
ALTER TABLE [dbo].[Paremeters]
ADD CONSTRAINT [PK_Paremeters]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RolFormPermissionSet'
ALTER TABLE [dbo].[RolFormPermissionSet]
ADD CONSTRAINT [PK_RolFormPermissionSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Establishments'
ALTER TABLE [dbo].[Establishments]
ADD CONSTRAINT [PK_Establishments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TypeEstablishments'
ALTER TABLE [dbo].[TypeEstablishments]
ADD CONSTRAINT [PK_TypeEstablishments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Debts'
ALTER TABLE [dbo].[Debts]
ADD CONSTRAINT [PK_Debts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Contracts'
ALTER TABLE [dbo].[Contracts]
ADD CONSTRAINT [PK_Contracts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Payments'
ALTER TABLE [dbo].[Payments]
ADD CONSTRAINT [PK_Payments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DebtBills'
ALTER TABLE [dbo].[DebtBills]
ADD CONSTRAINT [PK_DebtBills]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Bills'
ALTER TABLE [dbo].[Bills]
ADD CONSTRAINT [PK_Bills]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RolUsers'
ALTER TABLE [dbo].[RolUsers]
ADD CONSTRAINT [PK_RolUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Statements'
ALTER TABLE [dbo].[Statements]
ADD CONSTRAINT [PK_Statements]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Calculations'
ALTER TABLE [dbo].[Calculations]
ADD CONSTRAINT [PK_Calculations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StatementDetailSet'
ALTER TABLE [dbo].[StatementDetailSet]
ADD CONSTRAINT [PK_StatementDetailSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId_Id] in table 'CertificateSet'
ALTER TABLE [dbo].[CertificateSet]
ADD CONSTRAINT [FK_CertificateUser]
    FOREIGN KEY ([UserId_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CertificateUser'
CREATE INDEX [IX_FK_CertificateUser]
ON [dbo].[CertificateSet]
    ([UserId_Id]);
GO

-- Creating foreign key on [RolId_Id] in table 'RolFormPermissionSet'
ALTER TABLE [dbo].[RolFormPermissionSet]
ADD CONSTRAINT [FK_RolPermissionRol]
    FOREIGN KEY ([RolId_Id])
    REFERENCES [dbo].[Rols]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RolPermissionRol'
CREATE INDEX [IX_FK_RolPermissionRol]
ON [dbo].[RolFormPermissionSet]
    ([RolId_Id]);
GO

-- Creating foreign key on [PermissionId_Id] in table 'RolFormPermissionSet'
ALTER TABLE [dbo].[RolFormPermissionSet]
ADD CONSTRAINT [FK_RolPermissionPermission]
    FOREIGN KEY ([PermissionId_Id])
    REFERENCES [dbo].[Permissions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RolPermissionPermission'
CREATE INDEX [IX_FK_RolPermissionPermission]
ON [dbo].[RolFormPermissionSet]
    ([PermissionId_Id]);
GO

-- Creating foreign key on [ModuleId_Id] in table 'ModuleForms'
ALTER TABLE [dbo].[ModuleForms]
ADD CONSTRAINT [FK_ModuleFormModule]
    FOREIGN KEY ([ModuleId_Id])
    REFERENCES [dbo].[Modules]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModuleFormModule'
CREATE INDEX [IX_FK_ModuleFormModule]
ON [dbo].[ModuleForms]
    ([ModuleId_Id]);
GO

-- Creating foreign key on [FormId_Id] in table 'ModuleForms'
ALTER TABLE [dbo].[ModuleForms]
ADD CONSTRAINT [FK_FormModuleForm]
    FOREIGN KEY ([FormId_Id])
    REFERENCES [dbo].[Forms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FormModuleForm'
CREATE INDEX [IX_FK_FormModuleForm]
ON [dbo].[ModuleForms]
    ([FormId_Id]);
GO

-- Creating foreign key on [UserId_Id] in table 'Debts'
ALTER TABLE [dbo].[Debts]
ADD CONSTRAINT [FK_DebtUser]
    FOREIGN KEY ([UserId_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DebtUser'
CREATE INDEX [IX_FK_DebtUser]
ON [dbo].[Debts]
    ([UserId_Id]);
GO

-- Creating foreign key on [EstablishmentId_Id] in table 'Debts'
ALTER TABLE [dbo].[Debts]
ADD CONSTRAINT [FK_DebtEstablishment]
    FOREIGN KEY ([EstablishmentId_Id])
    REFERENCES [dbo].[Establishments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DebtEstablishment'
CREATE INDEX [IX_FK_DebtEstablishment]
ON [dbo].[Debts]
    ([EstablishmentId_Id]);
GO

-- Creating foreign key on [TypeEstablishmentId_Id] in table 'Establishments'
ALTER TABLE [dbo].[Establishments]
ADD CONSTRAINT [FK_EstablishmentTypeEstablishment]
    FOREIGN KEY ([TypeEstablishmentId_Id])
    REFERENCES [dbo].[TypeEstablishments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EstablishmentTypeEstablishment'
CREATE INDEX [IX_FK_EstablishmentTypeEstablishment]
ON [dbo].[Establishments]
    ([TypeEstablishmentId_Id]);
GO

-- Creating foreign key on [BillId_Id] in table 'DebtBills'
ALTER TABLE [dbo].[DebtBills]
ADD CONSTRAINT [FK_DebtBillBill]
    FOREIGN KEY ([BillId_Id])
    REFERENCES [dbo].[Bills]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DebtBillBill'
CREATE INDEX [IX_FK_DebtBillBill]
ON [dbo].[DebtBills]
    ([BillId_Id]);
GO

-- Creating foreign key on [DebtId_Id] in table 'DebtBills'
ALTER TABLE [dbo].[DebtBills]
ADD CONSTRAINT [FK_DebtBillDebt]
    FOREIGN KEY ([DebtId_Id])
    REFERENCES [dbo].[Debts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DebtBillDebt'
CREATE INDEX [IX_FK_DebtBillDebt]
ON [dbo].[DebtBills]
    ([DebtId_Id]);
GO

-- Creating foreign key on [DebtId_Id] in table 'Payments'
ALTER TABLE [dbo].[Payments]
ADD CONSTRAINT [FK_DebtPayment]
    FOREIGN KEY ([DebtId_Id])
    REFERENCES [dbo].[Debts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DebtPayment'
CREATE INDEX [IX_FK_DebtPayment]
ON [dbo].[Payments]
    ([DebtId_Id]);
GO

-- Creating foreign key on [UserId_Id] in table 'Contracts'
ALTER TABLE [dbo].[Contracts]
ADD CONSTRAINT [FK_UserContract]
    FOREIGN KEY ([UserId_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserContract'
CREATE INDEX [IX_FK_UserContract]
ON [dbo].[Contracts]
    ([UserId_Id]);
GO

-- Creating foreign key on [EstablishmentId_Id] in table 'Contracts'
ALTER TABLE [dbo].[Contracts]
ADD CONSTRAINT [FK_RentEstablishment]
    FOREIGN KEY ([EstablishmentId_Id])
    REFERENCES [dbo].[Establishments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RentEstablishment'
CREATE INDEX [IX_FK_RentEstablishment]
ON [dbo].[Contracts]
    ([EstablishmentId_Id]);
GO

-- Creating foreign key on [RolId_Id] in table 'RolUsers'
ALTER TABLE [dbo].[RolUsers]
ADD CONSTRAINT [FK_RolUserRol]
    FOREIGN KEY ([RolId_Id])
    REFERENCES [dbo].[Rols]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RolUserRol'
CREATE INDEX [IX_FK_RolUserRol]
ON [dbo].[RolUsers]
    ([RolId_Id]);
GO

-- Creating foreign key on [UserId_Id] in table 'RolUsers'
ALTER TABLE [dbo].[RolUsers]
ADD CONSTRAINT [FK_RolUserUser]
    FOREIGN KEY ([UserId_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RolUserUser'
CREATE INDEX [IX_FK_RolUserUser]
ON [dbo].[RolUsers]
    ([UserId_Id]);
GO

-- Creating foreign key on [FormId_Id] in table 'RolFormPermissionSet'
ALTER TABLE [dbo].[RolFormPermissionSet]
ADD CONSTRAINT [FK_RolFormPermissionForm]
    FOREIGN KEY ([FormId_Id])
    REFERENCES [dbo].[Forms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RolFormPermissionForm'
CREATE INDEX [IX_FK_RolFormPermissionForm]
ON [dbo].[RolFormPermissionSet]
    ([FormId_Id]);
GO

-- Creating foreign key on [StatementId_Id] in table 'StatementDetailSet'
ALTER TABLE [dbo].[StatementDetailSet]
ADD CONSTRAINT [FK_StatementDetailStatement]
    FOREIGN KEY ([StatementId_Id])
    REFERENCES [dbo].[Statements]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StatementDetailStatement'
CREATE INDEX [IX_FK_StatementDetailStatement]
ON [dbo].[StatementDetailSet]
    ([StatementId_Id]);
GO

-- Creating foreign key on [PaymentId_Id] in table 'StatementDetailSet'
ALTER TABLE [dbo].[StatementDetailSet]
ADD CONSTRAINT [FK_StatementDetailPayment]
    FOREIGN KEY ([PaymentId_Id])
    REFERENCES [dbo].[Payments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StatementDetailPayment'
CREATE INDEX [IX_FK_StatementDetailPayment]
ON [dbo].[StatementDetailSet]
    ([PaymentId_Id]);
GO

-- Creating foreign key on [DebtId_Id] in table 'StatementDetailSet'
ALTER TABLE [dbo].[StatementDetailSet]
ADD CONSTRAINT [FK_StatementDetailDebt]
    FOREIGN KEY ([DebtId_Id])
    REFERENCES [dbo].[Debts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StatementDetailDebt'
CREATE INDEX [IX_FK_StatementDetailDebt]
ON [dbo].[StatementDetailSet]
    ([DebtId_Id]);
GO

-- Creating foreign key on [EstablishmentId_Id] in table 'StatementDetailSet'
ALTER TABLE [dbo].[StatementDetailSet]
ADD CONSTRAINT [FK_StatementDetailEstablishment]
    FOREIGN KEY ([EstablishmentId_Id])
    REFERENCES [dbo].[Establishments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StatementDetailEstablishment'
CREATE INDEX [IX_FK_StatementDetailEstablishment]
ON [dbo].[StatementDetailSet]
    ([EstablishmentId_Id]);
GO

-- Creating foreign key on [RentId_Id] in table 'StatementDetailSet'
ALTER TABLE [dbo].[StatementDetailSet]
ADD CONSTRAINT [FK_StatementDetailRent]
    FOREIGN KEY ([RentId_Id])
    REFERENCES [dbo].[Contracts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StatementDetailRent'
CREATE INDEX [IX_FK_StatementDetailRent]
ON [dbo].[StatementDetailSet]
    ([RentId_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------