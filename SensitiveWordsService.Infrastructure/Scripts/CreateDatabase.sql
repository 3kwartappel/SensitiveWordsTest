-- Create the database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'SensitiveWordsDB')
BEGIN
    CREATE DATABASE SensitiveWordsDB;
END

USE SensitiveWordsDB;
GO

-- Create the SensitiveWords table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SensitiveWords]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[SensitiveWords](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [Word] [nvarchar](100) NOT NULL,
        [CreatedAt] [datetime2] NOT NULL,
        [UpdatedAt] [datetime2] NULL,
        [IsActive] [bit] NOT NULL DEFAULT(1),
        CONSTRAINT [PK_SensitiveWords] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [UQ_SensitiveWords_Word] UNIQUE NONCLUSTERED ([Word] ASC)
    );
END

-- Insert initial data
IF NOT EXISTS (SELECT * FROM [dbo].[SensitiveWords])
BEGIN
    INSERT INTO [dbo].[SensitiveWords] ([Word], [CreatedAt], [IsActive])
    VALUES 
        ('ACTION', GETUTCDATE(), 1),
        ('ADD', GETUTCDATE(), 1),
        ('ALL', GETUTCDATE(), 1),
        ('ALLOCATE', GETUTCDATE(), 1),
        ('ALTER', GETUTCDATE(), 1),
        ('ANY', GETUTCDATE(), 1),
        ('APPLICATION', GETUTCDATE(), 1),
        ('ARE', GETUTCDATE(), 1),
        ('AREA', GETUTCDATE(), 1),
        ('ASC', GETUTCDATE(), 1),
        ('ASSERTION', GETUTCDATE(), 1),
        ('ATOMIC', GETUTCDATE(), 1),
        ('AUTHORIZATION', GETUTCDATE(), 1),
        ('AVG', GETUTCDATE(), 1),
        ('BEGIN', GETUTCDATE(), 1),
        ('BY', GETUTCDATE(), 1),
        ('CALL', GETUTCDATE(), 1),
        ('CASCADE', GETUTCDATE(), 1),
        ('CASCADED', GETUTCDATE(), 1),
        ('CATALOG', GETUTCDATE(), 1),
        ('CHECK', GETUTCDATE(), 1),
        ('CLOSE', GETUTCDATE(), 1),
        ('COLUMN', GETUTCDATE(), 1),
        ('COMMIT', GETUTCDATE(), 1),
        ('COMPRESS', GETUTCDATE(), 1),
        ('CONNECT', GETUTCDATE(), 1),
        ('CONNECTION', GETUTCDATE(), 1),
        ('CONSTRAINT', GETUTCDATE(), 1),
        ('CONSTRAINTS', GETUTCDATE(), 1),
        ('CONTINUE', GETUTCDATE(), 1),
        ('CONVERT', GETUTCDATE(), 1),
        ('CORRESPONDING', GETUTCDATE(), 1),
        ('CREATE', GETUTCDATE(), 1),
        ('CROSS', GETUTCDATE(), 1),
        ('CURRENT', GETUTCDATE(), 1),
        ('CURRENT_PATH', GETUTCDATE(), 1),
        ('CURRENT_SCHEMA', GETUTCDATE(), 1),
        ('CURRENT_SCHEMAID', GETUTCDATE(), 1),
        ('CURRENT_USER', GETUTCDATE(), 1),
        ('CURRENT_USERID', GETUTCDATE(), 1),
        ('CURSOR', GETUTCDATE(), 1),
        ('DATA', GETUTCDATE(), 1),
        ('DEALLOCATE', GETUTCDATE(), 1),
        ('DECLARE', GETUTCDATE(), 1),
        ('DEFAULT', GETUTCDATE(), 1),
        ('DEFERRABLE', GETUTCDATE(), 1),
        ('DEFERRED', GETUTCDATE(), 1),
        ('DELETE', GETUTCDATE(), 1),
        ('DESC', GETUTCDATE(), 1),
        ('DESCRIBE', GETUTCDATE(), 1),
        ('DESCRIPTOR', GETUTCDATE(), 1),
        ('DETERMINISTIC', GETUTCDATE(), 1),
        ('DIAGNOSTICS', GETUTCDATE(), 1),
        ('DIRECTORY', GETUTCDATE(), 1),
        ('DISCONNECT', GETUTCDATE(), 1),
        ('DISTINCT', GETUTCDATE(), 1),
        ('DO', GETUTCDATE(), 1),
        ('DOMAIN', GETUTCDATE(), 1),
        ('DOUBLEATTRIBUTE', GETUTCDATE(), 1),
        ('DROP', GETUTCDATE(), 1),
        ('EACH', GETUTCDATE(), 1),
        ('EXCEPT', GETUTCDATE(), 1),
        ('EXCEPTION', GETUTCDATE(), 1),
        ('EXEC', GETUTCDATE(), 1),
        ('EXECUTE', GETUTCDATE(), 1),
        ('EXTERNAL', GETUTCDATE(), 1),
        ('FETCH', GETUTCDATE(), 1),
        ('FLOAT', GETUTCDATE(), 1),
        ('FOREIGN', GETUTCDATE(), 1),
        ('FOUND', GETUTCDATE(), 1),
        ('FULL', GETUTCDATE(), 1),
        ('FUNCTION', GETUTCDATE(), 1),
        ('GET', GETUTCDATE(), 1),
        ('GLOBAL', GETUTCDATE(), 1),
        ('GO', GETUTCDATE(), 1),
        ('GOTO', GETUTCDATE(), 1),
        ('GRANT', GETUTCDATE(), 1),
        ('GROUP', GETUTCDATE(), 1),
        ('HANDLER', GETUTCDATE(), 1),
        ('HAVING', GETUTCDATE(), 1),
        ('IDENTITY', GETUTCDATE(), 1),
        ('IMMEDIATE', GETUTCDATE(), 1),
        ('INDEX', GETUTCDATE(), 1),
        ('INDEXED', GETUTCDATE(), 1),
        ('INDICATOR', GETUTCDATE(), 1),
        ('INITIALLY', GETUTCDATE(), 1),
        ('INNER', GETUTCDATE(), 1),
        ('INOUT', GETUTCDATE(), 1),
        ('INPUT', GETUTCDATE(), 1),
        ('INSENSITIVE', GETUTCDATE(), 1),
        ('INSERT', GETUTCDATE(), 1),
        ('INTERSECT', GETUTCDATE(), 1),
        ('INTO', GETUTCDATE(), 1),
        ('ISOLATION', GETUTCDATE(), 1),
        ('JOIN', GETUTCDATE(), 1),
        ('KEY', GETUTCDATE(), 1),
        ('LANGUAGE', GETUTCDATE(), 1),
        ('LAST', GETUTCDATE(), 1),
        ('LEAVE', GETUTCDATE(), 1),
        ('LEVEL', GETUTCDATE(), 1),
        ('LOCAL', GETUTCDATE(), 1),
        ('LONGATTRIBUTE', GETUTCDATE(), 1),
        ('LOOP', GETUTCDATE(), 1),
        ('MODIFIES', GETUTCDATE(), 1),
        ('MODULE', GETUTCDATE(), 1),
        ('NAMES', GETUTCDATE(), 1),
        ('NATIONAL', GETUTCDATE(), 1),
        ('NATURAL', GETUTCDATE(), 1),
        ('NEXT', GETUTCDATE(), 1),
        ('NULLIF', GETUTCDATE(), 1),
        ('ON', GETUTCDATE(), 1),
        ('ONLY', GETUTCDATE(), 1),
        ('OPEN', GETUTCDATE(), 1),
        ('OPTION', GETUTCDATE(), 1),
        ('ORDER', GETUTCDATE(), 1),
        ('OUT', GETUTCDATE(), 1),
        ('OUTER', GETUTCDATE(), 1),
        ('OUTPUT', GETUTCDATE(), 1),
        ('OVERLAPS', GETUTCDATE(), 1),
        ('OWNER', GETUTCDATE(), 1),
        ('PARTIAL', GETUTCDATE(), 1),
        ('PATH', GETUTCDATE(), 1),
        ('PRECISION', GETUTCDATE(), 1),
        ('PREPARE', GETUTCDATE(), 1),
        ('PRESERVE', GETUTCDATE(), 1),
        ('PRIMARY', GETUTCDATE(), 1),
        ('PRIOR', GETUTCDATE(), 1),
        ('PRIVILEGES', GETUTCDATE(), 1),
        ('PROCEDURE', GETUTCDATE(), 1),
        ('PUBLIC', GETUTCDATE(), 1),
        ('READ', GETUTCDATE(), 1),
        ('READS', GETUTCDATE(), 1),
        ('REFERENCES', GETUTCDATE(), 1),
        ('RELATIVE', GETUTCDATE(), 1),
        ('REPEAT', GETUTCDATE(), 1),
        ('RESIGNAL', GETUTCDATE(), 1),
        ('RESTRICT', GETUTCDATE(), 1),
        ('RETURN', GETUTCDATE(), 1),
        ('RETURNS', GETUTCDATE(), 1),
        ('REVOKE', GETUTCDATE(), 1),
        ('ROLLBACK', GETUTCDATE(), 1),
        ('ROUTINE', GETUTCDATE(), 1),
        ('ROW', GETUTCDATE(), 1),
        ('ROWS', GETUTCDATE(), 1),
        ('SCHEMA', GETUTCDATE(), 1),
        ('SCROLL', GETUTCDATE(), 1),
        ('SECTION', GETUTCDATE(), 1),
        ('SELECT', GETUTCDATE(), 1),
        ('SEQ', GETUTCDATE(), 1),
        ('SEQUENCE', GETUTCDATE(), 1),
        ('SESSION', GETUTCDATE(), 1),
        ('SESSION_USER', GETUTCDATE(), 1),
        ('SESSION_USERID', GETUTCDATE(), 1),
        ('SET', GETUTCDATE(), 1),
        ('SIGNAL', GETUTCDATE(), 1),
        ('SOME', GETUTCDATE(), 1),
        ('SPACE', GETUTCDATE(), 1),
        ('SPECIFIC', GETUTCDATE(), 1),
        ('SQL', GETUTCDATE(), 1),
        ('SQLCODE', GETUTCDATE(), 1),
        ('SQLERROR', GETUTCDATE(), 1),
        ('SQLEXCEPTION', GETUTCDATE(), 1),
        ('SQLSTATE', GETUTCDATE(), 1),
        ('SQLWARNING', GETUTCDATE(), 1),
        ('STATEMENT', GETUTCDATE(), 1),
        ('STRINGATTRIBUTE', GETUTCDATE(), 1),
        ('SUM', GETUTCDATE(), 1),
        ('SYSACC', GETUTCDATE(), 1),
        ('SYSHGH', GETUTCDATE(), 1),
        ('SYSLNK', GETUTCDATE(), 1),
        ('SYSNIX', GETUTCDATE(), 1),
        ('SYSTBLDEF', GETUTCDATE(), 1),
        ('SYSTBLDSC', GETUTCDATE(), 1),
        ('SYSTBT', GETUTCDATE(), 1),
        ('SYSTBTATT', GETUTCDATE(), 1),
        ('SYSTBTDEF', GETUTCDATE(), 1),
        ('SYSUSR', GETUTCDATE(), 1),
        ('SYSTEM_USER', GETUTCDATE(), 1),
        ('SYSVIW', GETUTCDATE(), 1),
        ('SYSVIWCOL', GETUTCDATE(), 1),
        ('TABLE', GETUTCDATE(), 1),
        ('TABLETYPE', GETUTCDATE(), 1),
        ('TEMPORARY', GETUTCDATE(), 1),
        ('TRANSACTION', GETUTCDATE(), 1),
        ('TRANSLATE', GETUTCDATE(), 1),
        ('TRANSLATION', GETUTCDATE(), 1),
        ('TRIGGER', GETUTCDATE(), 1),
        ('UNDO', GETUTCDATE(), 1),
        ('UNION', GETUTCDATE(), 1),
        ('UNIQUE', GETUTCDATE(), 1),
        ('UNTIL', GETUTCDATE(), 1),
        ('UPDATE', GETUTCDATE(), 1),
        ('USAGE', GETUTCDATE(), 1),
        ('USER', GETUTCDATE(), 1),
        ('USING', GETUTCDATE(), 1),
        ('VALUE', GETUTCDATE(), 1),
        ('VALUES', GETUTCDATE(), 1),
        ('VIEW', GETUTCDATE(), 1),
        ('WHERE', GETUTCDATE(), 1),
        ('WHILE', GETUTCDATE(), 1),
        ('WITH', GETUTCDATE(), 1),
        ('WORK', GETUTCDATE(), 1),
        ('WRITE', GETUTCDATE(), 1),
        ('ALLSCHEMAS', GETUTCDATE(), 1),
        ('ALLTABLES', GETUTCDATE(), 1),
        ('ALLVIEWS', GETUTCDATE(), 1),
        ('ALLVIEWTEXTS', GETUTCDATE(), 1),
        ('ALLCOLUMNS', GETUTCDATE(), 1),
        ('ALLINDEXES', GETUTCDATE(), 1),
        ('ALLINDEXCOLS', GETUTCDATE(), 1),
        ('ALLUSERS', GETUTCDATE(), 1),
        ('ALLTBTS', GETUTCDATE(), 1),
        ('TABLEPRIVILEGES', GETUTCDATE(), 1),
        ('TBTPRIVILEGES', GETUTCDATE(), 1),
        ('MYSCHEMAS', GETUTCDATE(), 1),
        ('MYTABLES', GETUTCDATE(), 1),
        ('MYTBTS', GETUTCDATE(), 1),
        ('MYVIEWS', GETUTCDATE(), 1),
        ('SCHEMAVIEWS', GETUTCDATE(), 1),
        ('DUAL', GETUTCDATE(), 1),
        ('SCHEMAPRIVILEGES', GETUTCDATE(), 1),
        ('SCHEMATABLES', GETUTCDATE(), 1),
        ('STATISTICS', GETUTCDATE(), 1),
        ('USRTBL', GETUTCDATE(), 1),
        ('STRINGTABLE', GETUTCDATE(), 1),
        ('LONGTABLE', GETUTCDATE(), 1),
        ('DOUBLETABLE', GETUTCDATE(), 1),
        ('SELECT * FROM', GETUTCDATE(), 1);
END 