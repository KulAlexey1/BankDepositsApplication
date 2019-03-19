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
    [account_operation_type_id] INT            IDENTITY (1, 1) NOT NULL,
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
    [amount]               DECIMAL (30, 2) NOT NULL,
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
    [amount]       DECIMAL (30, 2) NOT NULL,
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
    [citizenship_id]   INT            IDENTITY (1, 1) NOT NULL,
    [citizenship_name] NVARCHAR (100) NOT NULL,
    CONSTRAINT [pk_citizenships] PRIMARY KEY CLUSTERED ([citizenship_id] ASC),
    CONSTRAINT [ak_citizenship_name] UNIQUE NONCLUSTERED ([citizenship_name] ASC)
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
    CONSTRAINT [pk_contracts] PRIMARY KEY CLUSTERED ([contract_id] ASC)
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
    [deposit_id]     INT             IDENTITY (1, 1) NOT NULL,
    [deposit_name]   NVARCHAR (255)  NOT NULL,
    [payment_period] DECIMAL (5, 2)  NOT NULL,
    [currency_id]    INT             NOT NULL,
    [term]           DECIMAL (5, 2)  NOT NULL,
    [rate]           DECIMAL (10, 7) NOT NULL,
    CONSTRAINT [pk_deposits] PRIMARY KEY CLUSTERED ([deposit_id] ASC),
    CONSTRAINT [ak_deposit_name_payment_period_currency_id_term_rate] UNIQUE NONCLUSTERED ([deposit_name] ASC, [payment_period] ASC, [currency_id] ASC, [term] ASC, [rate] ASC)
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
    [postcode]         INT            NULL,
    CONSTRAINT [pk_localities] PRIMARY KEY CLUSTERED ([locality_id] ASC),
    CONSTRAINT [ak_locality_type_region_locality_postcode] UNIQUE NONCLUSTERED ([locality_type_id] ASC, [region] ASC, [locality] ASC, [postcode] ASC)
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
PRINT N'Creating [dbo].[payment_periods]...';


GO
CREATE TABLE [dbo].[payment_periods] (
    [payment_period] DECIMAL (5, 2) NOT NULL,
    CONSTRAINT [pk_payment_periods] PRIMARY KEY CLUSTERED ([payment_period] ASC)
);


GO
PRINT N'Creating [dbo].[phone_number_operator_codes]...';


GO
CREATE TABLE [dbo].[phone_number_operator_codes] (
    [phone_number_operator_code] INT NOT NULL,
    CONSTRAINT [pk_phone_number_operator_codes] PRIMARY KEY CLUSTERED ([phone_number_operator_code] ASC)
);


GO
PRINT N'Creating [dbo].[phone_number_operators]...';


GO
CREATE TABLE [dbo].[phone_number_operators] (
    [phone_number_operator_id] INT            NOT NULL,
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
PRINT N'Creating [dbo].[rates]...';


GO
CREATE TABLE [dbo].[rates] (
    [rate] DECIMAL (10, 7) NOT NULL,
    CONSTRAINT [pk_rates] PRIMARY KEY CLUSTERED ([rate] ASC)
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
PRINT N'Creating [dbo].[terms]...';


GO
CREATE TABLE [dbo].[terms] (
    [term] DECIMAL (5, 2) NOT NULL,
    CONSTRAINT [pk_terms] PRIMARY KEY CLUSTERED ([term] ASC)
);


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
PRINT N'Creating [dbo].[fk_deposits_payment_periods]...';


GO
ALTER TABLE [dbo].[deposits]
    ADD CONSTRAINT [fk_deposits_payment_periods] FOREIGN KEY ([payment_period]) REFERENCES [dbo].[payment_periods] ([payment_period]);


GO
PRINT N'Creating [dbo].[fk_deposits_currencies]...';


GO
ALTER TABLE [dbo].[deposits]
    ADD CONSTRAINT [fk_deposits_currencies] FOREIGN KEY ([currency_id]) REFERENCES [dbo].[currencies] ([currency_id]);


GO
PRINT N'Creating [dbo].[fk_deposits_terms]...';


GO
ALTER TABLE [dbo].[deposits]
    ADD CONSTRAINT [fk_deposits_terms] FOREIGN KEY ([term]) REFERENCES [dbo].[terms] ([term]);


GO
PRINT N'Creating [dbo].[fk_deposits_rates]...';


GO
ALTER TABLE [dbo].[deposits]
    ADD CONSTRAINT [fk_deposits_rates] FOREIGN KEY ([rate]) REFERENCES [dbo].[rates] ([rate]);


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
PRINT N'Creating [dbo].[fk_localities_locality_types]...';


GO
ALTER TABLE [dbo].[localities]
    ADD CONSTRAINT [fk_localities_locality_types] FOREIGN KEY ([locality_type_id]) REFERENCES [dbo].[locality_types] ([locality_type_id]);


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
PRINT N'Creating [dbo].[fk_phone_number_operators_codes_phone_number_operator_code]...';


GO
ALTER TABLE [dbo].[phone_number_operators_codes]
    ADD CONSTRAINT [fk_phone_number_operators_codes_phone_number_operator_code] FOREIGN KEY ([phone_number_operator_code]) REFERENCES [dbo].[phone_number_operator_codes] ([phone_number_operator_code]);


GO
PRINT N'Creating [dbo].[fk_phone_numbers_phone_number_operators]...';


GO
ALTER TABLE [dbo].[phone_numbers]
    ADD CONSTRAINT [fk_phone_numbers_phone_number_operators] FOREIGN KEY ([operator_id]) REFERENCES [dbo].[phone_number_operators] ([phone_number_operator_id]);


GO
PRINT N'Creating [dbo].[fk_phone_numbers_phone_number_operator_codes]...';


GO
ALTER TABLE [dbo].[phone_numbers]
    ADD CONSTRAINT [fk_phone_numbers_phone_number_operator_codes] FOREIGN KEY ([operator_code]) REFERENCES [dbo].[phone_number_operator_codes] ([phone_number_operator_code]);


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
alter table dbo.account_operation_types
add constraint ak_account_operation_type unique (account_operation_type)
GO
alter table dbo.account_operations
add constraint fk_account_operations_account
    foreign key (account_id) references dbo.accounts(account_id)
GO

alter table dbo.account_operations
add constraint fk_account_operations_account_operation_types
    foreign key ([type_id]) references dbo.account_operation_types(account_operation_type_id)
GO
alter table dbo.accounts
add constraint fk_accounts_currencies
    foreign key (currency_id) references dbo.currencies(currency_id)
GO

alter table dbo.accounts
add constraint check_accounts_opening_date_closing_date
    check (closing_date > opening_date)
GO
alter table dbo.addresses
add constraint fk_addresses_streets
    foreign key (street_id) references dbo.streets(street_id)
GO

alter table dbo.addresses
add constraint ak_street_house_housing_apartment
    unique (street_id, house, housing, apartment)
GO
alter table dbo.citizenships
add constraint ak_citizenship_name unique (citizenship_name)
GO
alter table dbo.contracts
add constraint fk_contracts_deposits
    foreign key (deposit_id) references dbo.deposits(deposit_id)
GO

alter table dbo.contracts
add constraint fk_contracts_depositors
    foreign key (depositor_id) references dbo.depositors(depositor_id)
GO

alter table dbo.contracts
add constraint fk_contracts_employees
    foreign key (employee_id) references dbo.employees(employee_id)
GO

alter table dbo.contracts
add constraint fk_contracts_accounts
    foreign key (account_id) references dbo.accounts(account_id)
GO

alter table dbo.contracts
add constraint check_contracts_conclusion_date_termination_date
    check (termination_date > conclusion_date)
GO
alter table dbo.currencies
add constraint ak_currency unique (currency)
GO
alter table dbo.depositors
add constraint fk_depositors_passport
    foreign key (passport_id) references dbo.passports(passport_id)
GO

alter table dbo.depositors
add constraint fk_depositors_addresses
    foreign key (address_id) references dbo.addresses(address_id)
GO

alter table dbo.depositors
add constraint fk_depositors_phone_numbers
    foreign key (phone_number_id) references dbo.phone_numbers(phone_number_id)
GO

alter table dbo.depositors
add constraint ak_depositor_passport_id unique (passport_id)
GO
alter table dbo.deposits
add constraint fk_deposits_payment_periods
    foreign key (payment_period) references dbo.payment_periods(payment_period)
GO

alter table dbo.deposits
add constraint fk_deposits_currencies
    foreign key (currency_id) references dbo.currencies(currency_id)
GO

alter table dbo.deposits
add constraint fk_deposits_terms
    foreign key (term) references dbo.terms(term)
GO

alter table dbo.deposits
add constraint fk_deposits_rates
    foreign key (rate) references dbo.rates(rate)
GO

alter table dbo.deposits
add constraint ak_deposit_name_payment_period_currency_id_term_rate
    unique (deposit_name, payment_period, currency_id, term, rate)
GO
alter table dbo.employee_positions
add constraint ak_employee_position unique (employee_position)
GO
alter table dbo.employees
add constraint fk_employees_passports
    foreign key (passport_id) references dbo.passports(passport_id)
GO

alter table dbo.employees
add constraint fk_employees_addresses
    foreign key (address_id) references dbo.addresses(address_id)
GO

alter table dbo.employees
add constraint fk_employees_phone_numbers
    foreign key (phone_number_id) references dbo.phone_numbers(phone_number_id)
GO

alter table dbo.employees
add constraint fk_employees_employee_positions
    foreign key (position_id) references dbo.employee_positions(employee_position_id)
GO

alter table dbo.employees
add constraint ak_employees_passport_id unique (passport_id)
GO

alter table dbo.employees
add constraint check_employees_start_date_end_date check ([end_date] > [start_date])
GO
alter table dbo.issuing_authorities
add constraint ak_issuing_authority unique (issuing_authority)
GO
alter table dbo.localities
add constraint fk_localities_locality_types
    foreign key (locality_type_id) references dbo.locality_types(locality_type_id)
GO

alter table dbo.localities
add constraint ak_locality_type_region_locality_postcode
    unique (locality_type_id, region, locality, postcode)
GO
alter table dbo.locality_types
add constraint ak_locality_type unique (locality_type)
GO
alter table dbo.passports
add constraint fk_passports_citizenships
    foreign key (citizenship_id) references dbo.citizenships(citizenship_id)
GO

alter table dbo.passports
add constraint fk_passports_issuing_authorities_localities
    foreign key (issuing_authority_id, issuing_authority_locality_id)
    references dbo.issuing_authorities_localities(issuing_authority_id, issuing_authority_locality_id)
GO

alter table dbo.passports
add constraint ak_first_name_middle_name_last_name_birth_date_gender_issue_date
    unique (first_name, middle_name, last_name, birth_date, gender, issue_date)
GO

alter table dbo.passports
add constraint ak_number unique (number)
GO

alter table dbo.passports
add constraint ak_identification_number unique (identification_number)
GO

alter table dbo.passports
add constraint check_passport_issue_date_expiration_date
    check (expiration_date > issue_date)
GO
alter table dbo.phone_number_operators
add constraint ak_phone_number_operator unique (phone_number_operator)
GO
alter table dbo.phone_number_operators_codes
add constraint fk_phone_number_operators_codes_phone_number_operator
    foreign key (phone_number_operator_id) references dbo.phone_number_operators(phone_number_operator_id)
GO

alter table dbo.phone_number_operators_codes
add constraint fk_phone_number_operators_codes_phone_number_operator_code
    foreign key (phone_number_operator_code) references dbo.phone_number_operator_codes(phone_number_operator_code)
GO
alter table dbo.phone_numbers
add constraint fk_phone_numbers_phone_number_operators
    foreign key (operator_id) references dbo.phone_number_operators(phone_number_operator_id)
GO

alter table dbo.phone_numbers
add constraint fk_phone_numbers_phone_number_operator_codes
    foreign key (operator_code) references dbo.phone_number_operator_codes(phone_number_operator_code)
GO

alter table dbo.phone_numbers
add constraint ak_operator_operator_code_phone_number unique (operator_id, operator_code, phone_number)
GO
alter table dbo.streets
add constraint fk_streets_locality
    foreign key (locality_id) references dbo.localities(locality_id)
GO

alter table dbo.streets
add constraint ak_locality_street_postcode unique (locality_id, street, postcode)
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
