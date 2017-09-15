@ECHO OFF

SET CONNECTIONNAME="FasTnT.Database"

..\packages\FluentMigrator.1.6.2\tools\Migrate.exe -a ../FasTnT.Data/bin/Debug/FasTnT.Data.dll -db Postgres -conn "%CONNECTIONNAME%"

pause