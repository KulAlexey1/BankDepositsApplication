﻿/*
Deployment script for bank_deposits_db

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "bank_deposits_db"
:setvar DefaultFilePrefix "bank_deposits_db"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE SQL_Latin1_General_CP1_CI_AS
GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF),
                MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = OFF,
                DELAYED_DURABILITY = DISABLED 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_PLANS_PER_QUERY = 200, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE = OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET TEMPORAL_HISTORY_RETENTION ON 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Creating [dbo].[account_operation_types]...';


GO
CREATE TABLE [dbo].[account_operation_types] (
    [account_operation_type_id] INT            NOT NULL,
    [account_operation_type]    NVARCHAR (100) NOT NULL,
    CONSTRAINT [pk_account_operation_types] PRIMARY KEY CLUSTERED ([account_operation_type_id] ASC),
    CONSTRAINT [ak_account_operation_type] UNIQUE NONCLUSTERED ([account_operation_type] ASC)
);


GO
PRINT N'Creating [dbo].[account_operations]...';


GO
CREATE TABLE [dbo].[account_operations] (
    [account_operation_id] INT             IDENTITY (1, 1) NOT NULL,
    [account_id]           INT             NOT NULL,
    [type_id]              INT             NOT NULL,
    [date_time]            DATETIME        NOT NULL,
    [amount]               DECIMAL (30, 4) NOT NULL,
    CONSTRAINT [pk_account_operations] PRIMARY KEY CLUSTERED ([account_operation_id] ASC)
);


GO
PRINT N'Creating [dbo].[accounts]...';


GO
CREATE TABLE [dbo].[accounts] (
    [account_id]   INT             IDENTITY (1, 1) NOT NULL,
    [currency_id]  INT             NOT NULL,
    [opening_date] DATE            NOT NULL,
    [closing_date] DATE            NULL,
    [amount]       DECIMAL (30, 4) NOT NULL,
    CONSTRAINT [pk_accounts] PRIMARY KEY CLUSTERED ([account_id] ASC)
);


GO
PRINT N'Creating [dbo].[addresses]...';


GO
CREATE TABLE [dbo].[addresses] (
    [address_id] INT           IDENTITY (1, 1) NOT NULL,
    [street_id]  INT           NOT NULL,
    [house]      INT           NOT NULL,
    [housing]    NVARCHAR (25) NULL,
    [apartment]  INT           NOT NULL,
    CONSTRAINT [pk_addresses] PRIMARY KEY CLUSTERED ([address_id] ASC),
    CONSTRAINT [ak_street_house_housing_apartment] UNIQUE NONCLUSTERED ([street_id] ASC, [house] ASC, [housing] ASC, [apartment] ASC)
);


GO
PRINT N'Creating [dbo].[citizenships]...';


GO
CREATE TABLE [dbo].[citizenships] (
    [citizenship_id] INT            NOT NULL,
    [citizenship]    NVARCHAR (100) NOT NULL,
    CONSTRAINT [pk_citizenships] PRIMARY KEY CLUSTERED ([citizenship_id] ASC),
    CONSTRAINT [ak_citizenship] UNIQUE NONCLUSTERED ([citizenship] ASC)
);


GO
PRINT N'Creating [dbo].[contracts]...';


GO
CREATE TABLE [dbo].[contracts] (
    [contract_id]      INT  IDENTITY (1, 1) NOT NULL,
    [deposit_id]       INT  NOT NULL,
    [depositor_id]     INT  NOT NULL,
    [employee_id]      INT  NOT NULL,
    [account_id]       INT  NOT NULL,
    [conclusion_date]  DATE NOT NULL,
    [termination_date] DATE NULL,
    CONSTRAINT [pk_contracts] PRIMARY KEY CLUSTERED ([contract_id] ASC),
    CONSTRAINT [ak_contracts_account_id] UNIQUE NONCLUSTERED ([account_id] ASC)
);


GO
PRINT N'Creating [dbo].[currencies]...';


GO
CREATE TABLE [dbo].[currencies] (
    [currency_id] INT            NOT NULL,
    [currency]    NVARCHAR (100) NOT NULL,
    CONSTRAINT [pk_currencies] PRIMARY KEY CLUSTERED ([currency_id] ASC),
    CONSTRAINT [ak_currency] UNIQUE NONCLUSTERED ([currency] ASC)
);


GO
PRINT N'Creating [dbo].[currency_conversions]...';


GO
CREATE TABLE [dbo].[currency_conversions] (
    [currency_id]                        INT             NOT NULL,
    [buy]                                BIT             NOT NULL,
    [amount_in_native_currency_per_unit] DECIMAL (10, 5) NOT NULL,
    CONSTRAINT [pk_currency_conversion] PRIMARY KEY CLUSTERED ([currency_id] ASC, [buy] ASC, [amount_in_native_currency_per_unit] ASC)
);


GO
PRINT N'Creating [dbo].[deposit_terms]...';


GO
CREATE TABLE [dbo].[deposit_terms] (
    [deposit_term_id] INT IDENTITY (1, 1) NOT NULL,
    [days_amount]     INT NOT NULL,
    [months_amount]   INT NOT NULL,
    [years_amount]    INT NOT NULL,
    CONSTRAINT [pk_deposit_terms] PRIMARY KEY CLUSTERED ([deposit_term_id] ASC),
    CONSTRAINT [ak_deposit_term_days_months_years] UNIQUE NONCLUSTERED ([days_amount] ASC, [months_amount] ASC, [years_amount] ASC)
);


GO
PRINT N'Creating [dbo].[depositors]...';


GO
CREATE TABLE [dbo].[depositors] (
    [depositor_id]    INT            IDENTITY (1, 1) NOT NULL,
    [passport_id]     INT            NOT NULL,
    [address_id]      INT            NOT NULL,
    [phone_number_id] INT            NOT NULL,
    [email]           NVARCHAR (254) NULL,
    CONSTRAINT [pk_depositors] PRIMARY KEY CLUSTERED ([depositor_id] ASC),
    CONSTRAINT [ak_depositor_passport_id] UNIQUE NONCLUSTERED ([passport_id] ASC)
);


GO
PRINT N'Creating [dbo].[deposits]...';


GO
CREATE TABLE [dbo].[deposits] (
    [deposit_id]            INT            IDENTITY (1, 1) NOT NULL,
    [deposit]               NVARCHAR (255) NOT NULL,
    [payment_period]        INT            NOT NULL,
    [currency_id]           INT            NOT NULL,
    [deposit_term_id]       INT            NOT NULL,
    [rate]                  DECIMAL (4, 2) NOT NULL,
    [account_replenishment] BIT            NOT NULL,
    CONSTRAINT [pk_deposits] PRIMARY KEY CLUSTERED ([deposit_id] ASC),
    CONSTRAINT [ak_deposit_payment_period_currency_id_term_rate] UNIQUE NONCLUSTERED ([deposit] ASC, [payment_period] ASC, [currency_id] ASC, [deposit_term_id] ASC, [rate] ASC, [account_replenishment] ASC)
);


GO
PRINT N'Creating [dbo].[employee_positions]...';


GO
CREATE TABLE [dbo].[employee_positions] (
    [employee_position_id] INT            NOT NULL,
    [employee_position]    NVARCHAR (100) NOT NULL,
    CONSTRAINT [pk_employee_positions] PRIMARY KEY CLUSTERED ([employee_position_id] ASC),
    CONSTRAINT [ak_employee_position] UNIQUE NONCLUSTERED ([employee_position] ASC)
);


GO
PRINT N'Creating [dbo].[employees]...';


GO
CREATE TABLE [dbo].[employees] (
    [employee_id]     INT            IDENTITY (1, 1) NOT NULL,
    [passport_id]     INT            NOT NULL,
    [address_id]      INT            NOT NULL,
    [phone_number_id] INT            NOT NULL,
    [email]           NVARCHAR (254) NULL,
    [position_id]     INT            NOT NULL,
    [start_date]      DATE           NOT NULL,
    [end_date]        DATE           NULL,
    [password]        NVARCHAR (20)  NOT NULL,
    CONSTRAINT [pk_employees] PRIMARY KEY CLUSTERED ([employee_id] ASC),
    CONSTRAINT [ak_employees_passport_id] UNIQUE NONCLUSTERED ([passport_id] ASC)
);


GO
PRINT N'Creating [dbo].[issuing_authorities]...';


GO
CREATE TABLE [dbo].[issuing_authorities] (
    [issuing_authority_id] INT            NOT NULL,
    [issuing_authority]    NVARCHAR (100) NOT NULL,
    CONSTRAINT [pk_issuing_authorities] PRIMARY KEY CLUSTERED ([issuing_authority_id] ASC),
    CONSTRAINT [ak_issuing_authority] UNIQUE NONCLUSTERED ([issuing_authority] ASC)
);


GO
PRINT N'Creating [dbo].[issuing_authorities_localities]...';


GO
CREATE TABLE [dbo].[issuing_authorities_localities] (
    [issuing_authority_id]          INT NOT NULL,
    [issuing_authority_locality_id] INT NOT NULL,
    CONSTRAINT [pk_issuing_authorities_localities] PRIMARY KEY CLUSTERED ([issuing_authority_id] ASC, [issuing_authority_locality_id] ASC)
);


GO
PRINT N'Creating [dbo].[localities]...';


GO
CREATE TABLE [dbo].[localities] (
    [locality_id]      INT            IDENTITY (1, 1) NOT NULL,
    [locality_type_id] INT            NOT NULL,
    [region]           NVARCHAR (255) NOT NULL,
    [locality]         NVARCHAR (255) NOT NULL,
    CONSTRAINT [pk_localities] PRIMARY KEY CLUSTERED ([locality_id] ASC),
    CONSTRAINT [ak_locality_type_region_locality] UNIQUE NONCLUSTERED ([locality_type_id] ASC, [region] ASC, [locality] ASC)
);


GO
PRINT N'Creating [dbo].[locality_types]...';


GO
CREATE TABLE [dbo].[locality_types] (
    [locality_type_id] INT            NOT NULL,
    [locality_type]    NVARCHAR (100) NOT NULL,
    CONSTRAINT [pk_locality_types] PRIMARY KEY CLUSTERED ([locality_type_id] ASC),
    CONSTRAINT [ak_locality_type] UNIQUE NONCLUSTERED ([locality_type] ASC)
);


GO
PRINT N'Creating [dbo].[log_types]...';


GO
CREATE TABLE [dbo].[log_types] (
    [log_type_id] INT            NOT NULL,
    [log_type]    NVARCHAR (100) NOT NULL,
    CONSTRAINT [pk_log_types] PRIMARY KEY CLUSTERED ([log_type_id] ASC)
);


GO
PRINT N'Creating [dbo].[logs]...';


GO
CREATE TABLE [dbo].[logs] (
    [log_id]       INT            IDENTITY (1, 1) NOT NULL,
    [log_type_id]  INT            NOT NULL,
    [log_text]     NVARCHAR (MAX) NOT NULL,
    [log_datetime] DATETIME       NOT NULL,
    CONSTRAINT [pk_logs] PRIMARY KEY CLUSTERED ([log_id] ASC)
);


GO
PRINT N'Creating [dbo].[passports]...';


GO
CREATE TABLE [dbo].[passports] (
    [passport_id]                   INT           IDENTITY (1, 1) NOT NULL,
    [first_name]                    NVARCHAR (50) NOT NULL,
    [middle_name]                   NVARCHAR (50) NOT NULL,
    [last_name]                     NVARCHAR (50) NOT NULL,
    [birth_date]                    DATE          NOT NULL,
    [gender]                        BIT           NOT NULL,
    [citizenship_id]                INT           NOT NULL,
    [number]                        NVARCHAR (20) NOT NULL,
    [identification_number]         NVARCHAR (20) NOT NULL,
    [issuing_authority_id]          INT           NOT NULL,
    [issuing_authority_locality_id] INT           NOT NULL,
    [issue_date]                    DATE          NOT NULL,
    [expiration_date]               DATE          NOT NULL,
    CONSTRAINT [pk_passports] PRIMARY KEY CLUSTERED ([passport_id] ASC),
    CONSTRAINT [ak_first_name_middle_name_last_name_birth_date_gender_issue_date] UNIQUE NONCLUSTERED ([first_name] ASC, [middle_name] ASC, [last_name] ASC, [birth_date] ASC, [gender] ASC, [issue_date] ASC),
    CONSTRAINT [ak_identification_number] UNIQUE NONCLUSTERED ([identification_number] ASC),
    CONSTRAINT [ak_number] UNIQUE NONCLUSTERED ([number] ASC)
);


GO
PRINT N'Creating [dbo].[phone_number_operators]...';


GO
CREATE TABLE [dbo].[phone_number_operators] (
    [phone_number_operator_id] INT            IDENTITY (1, 1) NOT NULL,
    [phone_number_operator]    NVARCHAR (100) NOT NULL,
    CONSTRAINT [pk_phone_number_operators] PRIMARY KEY CLUSTERED ([phone_number_operator_id] ASC),
    CONSTRAINT [ak_phone_number_operator] UNIQUE NONCLUSTERED ([phone_number_operator] ASC)
);


GO
PRINT N'Creating [dbo].[phone_number_operators_codes]...';


GO
CREATE TABLE [dbo].[phone_number_operators_codes] (
    [phone_number_operator_id]   INT NOT NULL,
    [phone_number_operator_code] INT NOT NULL,
    CONSTRAINT [pk_phone_number_operators_codes] PRIMARY KEY CLUSTERED ([phone_number_operator_id] ASC, [phone_number_operator_code] ASC)
);


GO
PRINT N'Creating [dbo].[phone_numbers]...';


GO
CREATE TABLE [dbo].[phone_numbers] (
    [phone_number_id] INT IDENTITY (1, 1) NOT NULL,
    [operator_id]     INT NOT NULL,
    [operator_code]   INT NOT NULL,
    [phone_number]    INT NOT NULL,
    CONSTRAINT [pk_phone_numbers] PRIMARY KEY CLUSTERED ([phone_number_id] ASC),
    CONSTRAINT [ak_operator_operator_code_phone_number] UNIQUE NONCLUSTERED ([operator_id] ASC, [operator_code] ASC, [phone_number] ASC)
);


GO
PRINT N'Creating [dbo].[streets]...';


GO
CREATE TABLE [dbo].[streets] (
    [street_id]   INT            IDENTITY (1, 1) NOT NULL,
    [locality_id] INT            NOT NULL,
    [street]      NVARCHAR (255) NOT NULL,
    [postcode]    INT            NULL,
    CONSTRAINT [pk_streets] PRIMARY KEY CLUSTERED ([street_id] ASC),
    CONSTRAINT [ak_locality_street_postcode] UNIQUE NONCLUSTERED ([locality_id] ASC, [street] ASC, [postcode] ASC)
);


GO
PRINT N'Creating unnamed constraint on [dbo].[deposit_terms]...';


GO
ALTER TABLE [dbo].[deposit_terms]
    ADD DEFAULT 0 FOR [days_amount];


GO
PRINT N'Creating unnamed constraint on [dbo].[deposit_terms]...';


GO
ALTER TABLE [dbo].[deposit_terms]
    ADD DEFAULT 0 FOR [months_amount];


GO
PRINT N'Creating unnamed constraint on [dbo].[deposit_terms]...';


GO
ALTER TABLE [dbo].[deposit_terms]
    ADD DEFAULT 0 FOR [years_amount];


GO
PRINT N'Creating unnamed constraint on [dbo].[deposits]...';


GO
ALTER TABLE [dbo].[deposits]
    ADD DEFAULT 0 FOR [payment_period];


GO
PRINT N'Creating [dbo].[df_logs_log_datetime]...';


GO
ALTER TABLE [dbo].[logs]
    ADD CONSTRAINT [df_logs_log_datetime] DEFAULT getutcdate() FOR [log_datetime];


GO
PRINT N'Creating [dbo].[fk_account_operations_account]...';


GO
ALTER TABLE [dbo].[account_operations]
    ADD CONSTRAINT [fk_account_operations_account] FOREIGN KEY ([account_id]) REFERENCES [dbo].[accounts] ([account_id]);


GO
PRINT N'Creating [dbo].[fk_account_operations_account_operation_types]...';


GO
ALTER TABLE [dbo].[account_operations]
    ADD CONSTRAINT [fk_account_operations_account_operation_types] FOREIGN KEY ([type_id]) REFERENCES [dbo].[account_operation_types] ([account_operation_type_id]);


GO
PRINT N'Creating [dbo].[fk_accounts_currencies]...';


GO
ALTER TABLE [dbo].[accounts]
    ADD CONSTRAINT [fk_accounts_currencies] FOREIGN KEY ([currency_id]) REFERENCES [dbo].[currencies] ([currency_id]);


GO
PRINT N'Creating [dbo].[fk_addresses_streets]...';


GO
ALTER TABLE [dbo].[addresses]
    ADD CONSTRAINT [fk_addresses_streets] FOREIGN KEY ([street_id]) REFERENCES [dbo].[streets] ([street_id]);


GO
PRINT N'Creating [dbo].[fk_contracts_deposits]...';


GO
ALTER TABLE [dbo].[contracts]
    ADD CONSTRAINT [fk_contracts_deposits] FOREIGN KEY ([deposit_id]) REFERENCES [dbo].[deposits] ([deposit_id]);


GO
PRINT N'Creating [dbo].[fk_contracts_depositors]...';


GO
ALTER TABLE [dbo].[contracts]
    ADD CONSTRAINT [fk_contracts_depositors] FOREIGN KEY ([depositor_id]) REFERENCES [dbo].[depositors] ([depositor_id]);


GO
PRINT N'Creating [dbo].[fk_contracts_employees]...';


GO
ALTER TABLE [dbo].[contracts]
    ADD CONSTRAINT [fk_contracts_employees] FOREIGN KEY ([employee_id]) REFERENCES [dbo].[employees] ([employee_id]);


GO
PRINT N'Creating [dbo].[fk_contracts_accounts]...';


GO
ALTER TABLE [dbo].[contracts]
    ADD CONSTRAINT [fk_contracts_accounts] FOREIGN KEY ([account_id]) REFERENCES [dbo].[accounts] ([account_id]);


GO
PRINT N'Creating [dbo].[fk_depositors_passport]...';


GO
ALTER TABLE [dbo].[depositors]
    ADD CONSTRAINT [fk_depositors_passport] FOREIGN KEY ([passport_id]) REFERENCES [dbo].[passports] ([passport_id]);


GO
PRINT N'Creating [dbo].[fk_depositors_addresses]...';


GO
ALTER TABLE [dbo].[depositors]
    ADD CONSTRAINT [fk_depositors_addresses] FOREIGN KEY ([address_id]) REFERENCES [dbo].[addresses] ([address_id]);


GO
PRINT N'Creating [dbo].[fk_depositors_phone_numbers]...';


GO
ALTER TABLE [dbo].[depositors]
    ADD CONSTRAINT [fk_depositors_phone_numbers] FOREIGN KEY ([phone_number_id]) REFERENCES [dbo].[phone_numbers] ([phone_number_id]);


GO
PRINT N'Creating [dbo].[fk_deposits_currencies]...';


GO
ALTER TABLE [dbo].[deposits]
    ADD CONSTRAINT [fk_deposits_currencies] FOREIGN KEY ([currency_id]) REFERENCES [dbo].[currencies] ([currency_id]);


GO
PRINT N'Creating [dbo].[fk_employees_passports]...';


GO
ALTER TABLE [dbo].[employees]
    ADD CONSTRAINT [fk_employees_passports] FOREIGN KEY ([passport_id]) REFERENCES [dbo].[passports] ([passport_id]);


GO
PRINT N'Creating [dbo].[fk_employees_addresses]...';


GO
ALTER TABLE [dbo].[employees]
    ADD CONSTRAINT [fk_employees_addresses] FOREIGN KEY ([address_id]) REFERENCES [dbo].[addresses] ([address_id]);


GO
PRINT N'Creating [dbo].[fk_employees_phone_numbers]...';


GO
ALTER TABLE [dbo].[employees]
    ADD CONSTRAINT [fk_employees_phone_numbers] FOREIGN KEY ([phone_number_id]) REFERENCES [dbo].[phone_numbers] ([phone_number_id]);


GO
PRINT N'Creating [dbo].[fk_employees_employee_positions]...';


GO
ALTER TABLE [dbo].[employees]
    ADD CONSTRAINT [fk_employees_employee_positions] FOREIGN KEY ([position_id]) REFERENCES [dbo].[employee_positions] ([employee_position_id]);


GO
PRINT N'Creating [dbo].[fk_issuing_authorities_localities_issuing_authorities]...';


GO
ALTER TABLE [dbo].[issuing_authorities_localities]
    ADD CONSTRAINT [fk_issuing_authorities_localities_issuing_authorities] FOREIGN KEY ([issuing_authority_id]) REFERENCES [dbo].[issuing_authorities] ([issuing_authority_id]);


GO
PRINT N'Creating [dbo].[fk_issuing_authorities_localities_localities]...';


GO
ALTER TABLE [dbo].[issuing_authorities_localities]
    ADD CONSTRAINT [fk_issuing_authorities_localities_localities] FOREIGN KEY ([issuing_authority_locality_id]) REFERENCES [dbo].[localities] ([locality_id]);


GO
PRINT N'Creating [dbo].[fk_localities_locality_types]...';


GO
ALTER TABLE [dbo].[localities]
    ADD CONSTRAINT [fk_localities_locality_types] FOREIGN KEY ([locality_type_id]) REFERENCES [dbo].[locality_types] ([locality_type_id]);


GO
PRINT N'Creating [dbo].[fk_logs_log_types]...';


GO
ALTER TABLE [dbo].[logs]
    ADD CONSTRAINT [fk_logs_log_types] FOREIGN KEY ([log_type_id]) REFERENCES [dbo].[log_types] ([log_type_id]);


GO
PRINT N'Creating [dbo].[fk_passports_citizenships]...';


GO
ALTER TABLE [dbo].[passports]
    ADD CONSTRAINT [fk_passports_citizenships] FOREIGN KEY ([citizenship_id]) REFERENCES [dbo].[citizenships] ([citizenship_id]);


GO
PRINT N'Creating [dbo].[fk_passports_issuing_authorities_localities]...';


GO
ALTER TABLE [dbo].[passports]
    ADD CONSTRAINT [fk_passports_issuing_authorities_localities] FOREIGN KEY ([issuing_authority_id], [issuing_authority_locality_id]) REFERENCES [dbo].[issuing_authorities_localities] ([issuing_authority_id], [issuing_authority_locality_id]);


GO
PRINT N'Creating [dbo].[fk_phone_number_operators_codes_phone_number_operator]...';


GO
ALTER TABLE [dbo].[phone_number_operators_codes]
    ADD CONSTRAINT [fk_phone_number_operators_codes_phone_number_operator] FOREIGN KEY ([phone_number_operator_id]) REFERENCES [dbo].[phone_number_operators] ([phone_number_operator_id]);


GO
PRINT N'Creating [dbo].[fk_phone_numbers_phone_number_operators]...';


GO
ALTER TABLE [dbo].[phone_numbers]
    ADD CONSTRAINT [fk_phone_numbers_phone_number_operators] FOREIGN KEY ([operator_id]) REFERENCES [dbo].[phone_number_operators] ([phone_number_operator_id]);


GO
PRINT N'Creating [dbo].[fk_streets_locality]...';


GO
ALTER TABLE [dbo].[streets]
    ADD CONSTRAINT [fk_streets_locality] FOREIGN KEY ([locality_id]) REFERENCES [dbo].[localities] ([locality_id]);


GO
PRINT N'Creating [dbo].[check_accounts_opening_date_closing_date]...';


GO
ALTER TABLE [dbo].[accounts]
    ADD CONSTRAINT [check_accounts_opening_date_closing_date] CHECK (closing_date > opening_date);


GO
PRINT N'Creating [dbo].[check_contracts_conclusion_date_termination_date]...';


GO
ALTER TABLE [dbo].[contracts]
    ADD CONSTRAINT [check_contracts_conclusion_date_termination_date] CHECK (termination_date > conclusion_date);


GO
PRINT N'Creating [dbo].[check_deposit_term_days_months_years]...';


GO
ALTER TABLE [dbo].[deposit_terms]
    ADD CONSTRAINT [check_deposit_term_days_months_years] CHECK ((days_amount + months_amount + years_amount != 0) 
        and (days_amount + months_amount + years_amount) in (days_amount, months_amount, years_amount));


GO
PRINT N'Creating [dbo].[check_employees_start_date_end_date]...';


GO
ALTER TABLE [dbo].[employees]
    ADD CONSTRAINT [check_employees_start_date_end_date] CHECK ([end_date] > [start_date]);


GO
PRINT N'Creating [dbo].[check_passport_issue_date_expiration_date]...';


GO
ALTER TABLE [dbo].[passports]
    ADD CONSTRAINT [check_passport_issue_date_expiration_date] CHECK (expiration_date > issue_date);


GO
PRINT N'Creating [dbo].[ufn_convert_to_currency]...';


GO
create function [dbo].[ufn_convert_to_currency]
(
    @convertible_currency_id int,
    @convertible_amount decimal(30, 4),
    @currency_id_to_convert int
)
returns decimal(30, 4)
as
begin
    declare @result decimal(30, 4)

    select @result = cc.amount_in_native_currency_per_unit * @convertible_amount
    from dbo.currency_conversion cc
    where cc.buy = 1 and cc.currency_id = @convertible_currency_id;

    if @currency_id_to_convert = 1
        return @result;
    else if @convertible_currency_id = 1
        set @result = @convertible_amount;

    select @result = @result / cc.amount_in_native_currency_per_unit
    from dbo.currency_conversion cc
    where cc.buy = 0 and cc.currency_id = @currency_id_to_convert;

    return @result;
end
GO
PRINT N'Creating [dbo].[ufn_get_age]...';


GO
create function [dbo].[ufn_get_age](@birth_date date)
returns int
as
begin
    declare @current_date date = getdate();

    declare @birth_year int = year(@birth_date);
    declare @current_year int = year(@current_date); 

    declare @age int = @current_year - @birth_year;

    declare @birth_month int = month(@birth_date);
    declare @current_month int = month(@current_date);
    declare @month_diff int = @current_month - @birth_month; 
    
    if @month_diff < 0
        return @age - 1;
    else if @month_diff > 0
        return @age;
    else if @month_diff = 0 and day(@current_date) - day(@birth_date) < 0    
        return @age - 1;
    return @age;
end
GO
PRINT N'Creating [dbo].[usp_average_sum_of_deposits_for_month]...';


GO
create procedure [dbo].[usp_average_sum_of_deposits_for_month]
    @year int,
    @month int,
    @result decimal(30, 2) output
as
begin    
    select @result = avg(dbo.ufn_convert_to_currency(a.currency_id, ao.amount, 1))
    from dbo.contracts cts
        join dbo.accounts a
            on cts.account_id = a.account_id
        join dbo.account_operations ao
            on a.account_id = ao.account_id
    where year(cts.conclusion_date) = @year and month(cts.conclusion_date) = @month
        and year(ao.date_time) = @year and month(ao.date_time) = @month and ao.[type_id] = 1;
end
GO
PRINT N'Creating [dbo].[usp_depositors_with_account_replenishment_deposits_for_year]...';


GO
create procedure [dbo].[usp_depositors_with_account_replenishment_deposits_for_year]
    @year int
as
begin
    select dtr.depositor_id, p.last_name, p.first_name, p.middle_name
    from dbo.depositors dtr join dbo.passports p on dtr.passport_id = p.passport_id
    where exists (select 1
           from dbo.contracts c join dbo.deposits d on c.deposit_id = d.deposit_id
           where c.depositor_id = dtr.depositor_id
               and year(c.conclusion_date) = @year and d.account_replenishment = 1)    
    order by p.last_name, p.first_name, p.middle_name;
end
GO
PRINT N'Creating [dbo].[usp_depositors_with_fixed_deposits_for_year]...';


GO
create procedure [dbo].[usp_depositors_with_fixed_deposits_for_year]
    @year int
as
begin
    select dtr.depositor_id, p.last_name, p.first_name, p.middle_name
    from dbo.depositors dtr join dbo.passports p on dtr.passport_id = p.passport_id
    where dtr.depositor_id in (select c.depositor_id
           from dbo.contracts c join dbo.deposits d on c.deposit_id = d.deposit_id
               and year(c.conclusion_date) = @year and d.payment_period = 0
           group by c.depositor_id)    
    order by p.last_name, p.first_name, p.middle_name;
end
GO
PRINT N'Creating [dbo].[usp_depositors_with_non_belarus_citizenship]...';


GO
create procedure [dbo].[usp_depositors_with_non_belarus_citizenship]    
as
begin
    select dtr.depositor_id, p.last_name, p.first_name, p.middle_name, p.citizenship_id
    from dbo.depositors dtr join dbo.passports p on dtr.passport_id = p.passport_id
    where p.citizenship_id != 1
    order by p.last_name, p.first_name, p.middle_name;
end
GO
PRINT N'Creating [dbo].[usp_deposits_with_depositors_age_in_range]...';


GO
create procedure [dbo].[usp_deposits_with_depositors_age_in_range]
    @min_age int,
    @max_age int
as
begin
    select dtr.depositor_id, p.last_name, p.first_name, p.middle_name, p.birth_date,
        dt.deposit_id, dt.deposit, dt.currency_id, dt.deposit_term_id, dt.rate,
        dt.payment_period, dt.account_replenishment, c.conclusion_date, c.termination_date, a.amount
    from dbo.deposits dt
        join dbo.contracts c on dt.deposit_id = c.deposit_id
        join dbo.accounts a on c.account_id = a.account_id        
        join dbo.depositors dtr on c.depositor_id = dtr.depositor_id
        join dbo.passports p on dtr.passport_id = p.passport_id
    where dbo.ufn_get_age(p.birth_date) >= @min_age and dbo.ufn_get_age(p.birth_date) <= @max_age
    order by p.last_name, p.first_name, p.middle_name;
end
GO
PRINT N'Creating [dbo].[usp_employees_with_deposit_contract_for_month]...';


GO
create procedure [dbo].[usp_employees_with_deposit_contract_for_month]
    @year int,
    @month int
as
begin
    select e.employee_id, p.last_name, p.first_name, p.middle_name,
    d.deposit_id, d.deposit, c.conclusion_date
    from dbo.contracts c
        right join dbo.employees e on c.employee_id = e.employee_id
        join dbo.passports p on e.passport_id = e.passport_id
        join dbo.deposits d on c.deposit_id = d.deposit_id
    where year(c.conclusion_date) = @year and month(c.conclusion_date) = @month
        and c.employee_id is not null
    order by p.last_name, p.first_name, p.middle_name, c.conclusion_date desc;
end
GO
PRINT N'Creating [dbo].[usp_employees_without_deposit_contract_for_month]...';


GO
create procedure [dbo].[usp_employees_without_deposit_contract_for_month]
    @year int,
    @month int
as
begin
    select e.employee_id, p.last_name, p.first_name, p.middle_name
    from dbo.employees e
        left join dbo.contracts c on e.employee_id = c.employee_id
        join dbo.passports p on e.passport_id = e.passport_id
    where year(c.conclusion_date) = @year and month(c.conclusion_date) = @month
        and c.employee_id is null
    order by p.last_name, p.first_name, p.middle_name;
end
GO
PRINT N'Creating [dbo].[usp_largest_deposit_for_year]...';


GO
create procedure [dbo].[usp_largest_deposit_for_year]
    @year int,
    @result decimal(30, 2) output
as
begin
    select @result = max(dbo.ufn_convert_to_currency(a.currency_id, ao.amount, 1))
    from dbo.contracts cts
        join dbo.accounts a
            on cts.account_id = a.account_id
        join dbo.account_operations ao
            on a.account_id = ao.account_id
    where year(cts.conclusion_date) = @year and year(ao.date_time) = @year and ao.[type_id] = 1;
end
GO
PRINT N'Creating [dbo].[usp_most_popular_deposit_currency_for_year]...';


GO
create procedure [dbo].[usp_most_popular_deposit_currency_for_year]  
    @year int,
    @result int output
as
begin
    select @result = r.currency_id
    from (select top 1 d.currency_id, count(d.currency_id) as deposit_currency_count
          from dbo.contracts cts
              join dbo.deposits d on cts.deposit_id = d.deposit_id
          where year(cts.conclusion_date) = @year
          group by d.currency_id
          order by deposit_currency_count DESC) r;
end
GO
PRINT N'Creating [dbo].[usp_smallest_deposit_for_year]...';


GO
create procedure [dbo].[usp_smallest_deposit_for_year]
    @year int,
    @result decimal(30, 2) output
as
begin
    select @result = min(dbo.ufn_convert_to_currency(a.currency_id, ao.amount, 1))
    from dbo.contracts cts
        join dbo.accounts a
            on cts.account_id = a.account_id
        join dbo.account_operations ao
            on a.account_id = ao.account_id
    where year(cts.conclusion_date) = @year and year(ao.date_time) = @year and ao.[type_id] = 1;
end
GO
PRINT N'Creating [dbo].[usp_sum_of_deposits_for_month]...';


GO
create procedure [dbo].[usp_sum_of_deposits_for_month]
    @year int,
    @month int,    
    @result decimal(30, 2) output
as
begin    
    select @result = sum(dbo.ufn_convert_to_currency(a.currency_id, ao.amount, 1))
    from dbo.contracts cts
        join dbo.accounts a
            on cts.account_id = a.account_id
        join dbo.account_operations ao
            on a.account_id = ao.account_id
    where year(cts.conclusion_date) = @year and month(cts.conclusion_date) = @month
        and year(ao.date_time) = @year and month(ao.date_time) = @month and ao.[type_id] = 1;
end
GO
/*
Post-Deployment Script Template                            
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.        
 Use SQLCMD syntax to include a file in the post-deployment script.            
 Example:      :r .\myfile.sql                                
 Use SQLCMD syntax to reference a variable in the post-deployment script.        
 Example:      :setvar TableName MyTable                            
               SELECT * FROM [$(TableName)]                    
--------------------------------------------------------------------------------------
*/

insert into dbo.employee_positions
values (1, N'Employee'),
       (5, N'Cashier'),
       (10, N'Manager'),
       (15, N'Admin')
GO
insert into dbo.account_operation_types
values (1, N'Credit'),
       (5, N'Debit')
GO
insert into dbo.citizenships
values (1, N'PB'),
       (5, N'BA'),
       (10, N'BI')
GO
insert into dbo.currencies
values (1, N'BYN'),
       (5, N'USD'),
       (10, N'EUR'),
       (15, N'RUB')
GO
insert into dbo.currency_conversions
values (5, 0, 2.1130),
       (5, 1, 2.0770),
       (10, 0, 2.3530),
       (10, 1, 2.3170),
       (15, 0, 0.03236)
GO

insert into dbo.currency_conversions values (15, 1, 0.03174)
GO
insert into dbo.locality_types
values (1, N'Agro-city'),
       (5, N'City'),
       (10, N'Urban village')
GO
insert into dbo.log_types
values (1, N'INSERT'),
       (5, N'UPDATE'),
       (10, N'DELETE')
GO

declare @sql_command nvarchar(max)
declare command_cursor cursor for
select concat(N'create trigger ', t.table_schema, '.tgr_', t.table_name, '_insert ',
    'on ', t.full_table_name, ' after insert as begin ',
    'declare @log_header nvarchar(MAX) = ''Insert into ', t.full_table_name, '.'';',
    'declare @json nvarchar(max) = (select * from inserted i for json path, without_array_wrapper);',
    'insert into dbo.logs(log_type_id, log_text) values (1, concat(@log_header, '' '', @json, ''.'')); end ')
from
(select table_schema, table_name, concat(table_schema, '.', table_name) as full_table_name
from information_schema.tables
where table_name not in ('sysdiagrams', 'logs')) as t;

open command_cursor

fetch next from command_cursor
into @sql_command

while @@FETCH_STATUS = 0
begin
    exec sp_executesql @sql_command

    fetch next from command_cursor
    into @sql_command
end

close command_cursor;  
deallocate command_cursor;
GO
declare @sql_command nvarchar(max)
declare command_cursor cursor for
select concat(N'create trigger ', t.table_schema, '.tgr_', t.table_name, '_update ',
    'on ', t.full_table_name, ' after update as begin ',
    'declare @log_header nvarchar(MAX) = ''Update ', t.full_table_name, '.''; ',
    'select ''false'' as is_new, * into #temp_table from deleted d ',
    'union all select ''true'' as is_new, * from inserted i; ',
    'declare @json nvarchar(max) = (select * from #temp_table for json auto); ',
    'insert into dbo.logs(log_type_id, log_text) values (5, concat(@log_header, '' '', @json, ''.'')); ',
    'drop table #temp_table; end ')
from
(select table_schema, table_name, concat(table_schema, '.', table_name) as full_table_name
from information_schema.tables
where table_name not in ('sysdiagrams', 'logs')) as t;

open command_cursor

fetch next from command_cursor
into @sql_command

while @@FETCH_STATUS = 0
begin
    exec sp_executesql @sql_command

    fetch next from command_cursor
    into @sql_command
end

close command_cursor;  
deallocate command_cursor;
GO
declare @sql_command nvarchar(max)
declare command_cursor cursor for
select concat(N'create trigger ', t.table_schema, '.tgr_', t.table_name, '_delete ',
    'on ', t.full_table_name, ' after delete as begin ',
    'declare @log_header nvarchar(MAX) = ''Delete from ', t.full_table_name, '.'';',
    'declare @json nvarchar(max) = (select * from deleted d for json path, without_array_wrapper);',
    'insert into dbo.logs(log_type_id, log_text) values (10, concat(@log_header, '' '', @json, ''.'')); end ')
from
(select table_schema, table_name, concat(table_schema, '.', table_name) as full_table_name
from information_schema.tables
where table_name not in ('sysdiagrams', 'logs')) as t;

open command_cursor

fetch next from command_cursor
into @sql_command

while @@FETCH_STATUS = 0
begin
    exec sp_executesql @sql_command

    fetch next from command_cursor
    into @sql_command
end

close command_cursor;  
deallocate command_cursor;
GO

GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Update complete.';


GO
