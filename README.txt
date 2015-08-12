------------------------------------------------------------------------------------------------
TPCC Benchmark Tools

This is a basic implementation of the TPC-C benchmark tool.

I use it for testing new database servers for throughput.
It also implements a heartbeat table for testing replication.
This doesn't generate certifiable results for TPC-C submission.

tpcc database schema.sql to generate the empty schema.
TPCCDatabaseGenerator.exe to initally populate the TPC-C database.
tpccbench.exe to execute the test.
------------------------------------------------------------------------------------------------
tpccbench.exe command line
------------------------------------------------------------------------------------------------
Commands are in \x:option format.
No spaces between the \x: and the option specified.
Valid command line arguements are:

\c:{1} number of clients you wish to run max 200

\s:{servername} name of the database server to benchmark

\d:{target database} the TPC database you wish to benchmark

\u:{username} login name to connect to server. For sql authentication only
              ignored if \t option is used

\p:{password} login password to connect to server. For sql authentication only
              ignored if \t option is used

\t use trusted connection notice no : or any following commands

\wh:{2} number of warehouses to use during tests.
         Depends on number of warehouses populated in test database.

\m:{1} number of minutes to execute the test minimum 1 no maximum

\cs:{50} maximum number of delay between queries issued by the Client.
         Any number between 0 and 50 is normal 0 for maximum throughput
         50 for a standard tpc benchmark Client thinking delay

\sp used extended stored procedures instead of single issued statements

\stl:{0} stagger Client load where {0} is the number of seconds to delay each Client

\nl:{0} number of fixed loops to execute per Client. This overrides the \m: switch

\hb Log to Heartbeat table to track replication latency.

Below is the work load mix switches all 5 must be used and must equal 100.
\pno:{45} percent new order status
\pos:{43} percent order status
\pp:{4} percent Payment
\pd:{4} percent dilivery
\psl:{4} percent stock level

\? for help

if there is a tpccbench.txt in the same directory it will read the command line switches from the file.
See the included file.

------------------------------------------------------------------------------------------------
execute tpcc database schema.sql to generate the schema
------------------------------------------------------------------------------------------------
TPCCDatabaseGenerator.exe command line
------------------------------------------------------------------------------------------------

Options (mandatory):
--ConnectionString=""   Connection String in SQL Server format
--Tablename=""          Table to load. One of (warehouse, item, district, customer, stock, order, neworder, orderline, history)
--Start=1 start 2 warehouse ID Set this to 1
--End=2  end warehouse ID Set this to the upper limit i.e. 10 for 1 through 10 warehouses.

see gendb.bat example for loading all tables in an empty database.

------------------------------------------------------------------------------------------------
I recommend backing up the database if you plan on running multiple tests. 
The restore will be quicker than regenerating the database every time.