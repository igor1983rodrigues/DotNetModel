# Teste da Aplicação

## Technologies
* Swagger
* Dapper ORM
* Docker
* MS SQL Server Database
* .Net Core 3
* SimpleInjection as dependence injection

## Setup
* Run the code below:
<code>docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=applications@4pl1c4710n" \
   -p 1433:1433 --name sql1 -h sql1 \
   -d mcr.microsoft.com/mssql/server:2019-latest</code>
* Create database "application", and run the DDL script below:
<code>CREATE TABLE application.dbo.TB_APPL (
	ID_APPL int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	URL_APPL varchar(512) NOT NULL,
	PATH_LOCAL_APPL varchar(144) NOT NULL,
	DBG_MODE_APPL tinyint NOT NULL
);
EXEC application.sys.sp_addextendedproperty 'MS_Description', N'Application', 'schema', N'dbo', 'table', N'TB_APPL';
GO
EXEC application.sys.sp_addextendedproperty 'MS_Description', N'Id', 'schema', N'dbo', 'table', N'TB_APPL', 'column', N'ID_APPL';
GO
EXEC application.sys.sp_addextendedproperty 'MS_Description', N'Uri application', 'schema', N'dbo', 'table', N'TB_APPL', 'column', N'URL_APPL';
GO
EXEC application.sys.sp_addextendedproperty 'MS_Description', N'Local path application', 'schema', N'dbo', 'table', N'TB_APPL', 'column', N'PATH_LOCAL_APPL';
GO
EXEC application.sys.sp_addextendedproperty 'MS_Description', N'debug mode', 'schema', N'dbo', 'table', N'TB_APPL', 'column', N'DBG_MODE_APPL';
GO
</code>
* Run the project


/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "applications@4pl1c4710n"

* Data Source=localhost;User ID=SA;Password=applications@4pl1c4710n;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False