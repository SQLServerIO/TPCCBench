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

Options (mandatory):

--Clients=1             Number of clients you wish to run max 200

--ConnectionString=""   Connection String in SQL Server format

--Warehouses=2          Number of warehouses populated in test database

--ClientDelay=0         Delay between queries issued by the Client.
                        Any number between 0 and 50 is normal 0 for maximum throughput
                        50 for a standard tpc benchmark Client thinking delay

--StoreProcedures       Use extended stored procedures instead of single issued statements

--StaggerLoad=0         Stagger Client load where {0} is the number of seconds to delay each Client

--Loops=1               Number of fixed loops to execute per Client. This overrides the --Minutes switch

--Minutes=1             Time to run this test

--Heartbeat             Log to Heartbeat table to track replication latency.

Below is the work load mix switches all 5 must be used and must equal 100.
--PercentNewOrder=45 percent new order status

--PercentOrder=43        Percent order status

--PercentPayment=4       Percent Payment

--PercentDelivery=4      Percent delivery

--PercentStockLevel=4    Percent stock level

--help                   help

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