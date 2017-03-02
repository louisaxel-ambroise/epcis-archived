SET CONNECTION_STRING="Server=.\SQLEXPRESS;Database=epcis;Integrated Security=true;"
SET LIBRARY=".\Epcis.Data\bin\Debug\Epcis.Data.dll"

.\packages\FluentMigrator.1.6.2\tools\Migrate.exe -db sqlserver2008 -a %LIBRARY% -t migrate -c %CONNECTION_STRING%