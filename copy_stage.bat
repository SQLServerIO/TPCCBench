DEL "C:\projects\source\OpenSource\tpc\STAGE\*" /Q
DEL "C:\projects\source\OpenSource\tpc\RELEASE\*" /Q

copy "C:\projects\source\OpenSource\tpc\tpccbench\bin\Release\tpccbench.exe" "C:\projects\source\OpenSource\tpc\STAGE\tpccbench.exe"
copy "C:\projects\source\OpenSource\tpc\tpccbench\Testing\tpccbench.txt" "C:\projects\source\OpenSource\tpc\STAGE\tpccbench.txt"
copy "C:\projects\source\OpenSource\tpc\TPCCDatabaseGenerator\bin\Release\TPCCDatabaseGenerator.exe" "C:\projects\source\OpenSource\tpc\STAGE\TPCCDatabaseGenerator.exe"
copy "C:\projects\source\OpenSource\tpc\TPCCDatabaseGenerator\Testing\gendb.bat" "C:\projects\source\OpenSource\tpc\STAGE\gendb.bat"
copy "C:\projects\source\OpenSource\tpc\TPCCDatabaseGenerator\tpcc database schema.sql" "C:\projects\source\OpenSource\tpc\STAGE\tpcc database schema.sql"
copy "C:\projects\source\OpenSource\tpc\README.txt" "C:\projects\source\OpenSource\tpc\STAGE\README.txt"