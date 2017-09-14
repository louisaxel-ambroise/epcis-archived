@ECHO OFF

..\packages\FluentMigrator.1.6.2\tools\Migrate.exe -a ../FasTnT.Data/bin/Debug/FasTnT.Data.dll -db Postgres -conn "Server=localhost;Port=5432;Database=fastnt;User Id=postgres;Password=p@ssw0rd;CommandTimeout=20;"

pause