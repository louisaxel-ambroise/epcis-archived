@ECHO OFF

SET CONNECTIONNAME="FasTnT.Database"


:PROMPT
SET /P AREYOUSURE=All your tables and data will be deleted. Are you sure (Y/[N])?
IF /I "%AREYOUSURE%" NEQ "Y" GOTO END

..\..\packages\FluentMigrator.1.6.2\tools\Migrate.exe -a ../../src/FasTnT.Data/bin/Debug/FasTnT.Data.dll -db Postgres -conn "%CONNECTIONNAME%" -t rollback:all

:END
pause