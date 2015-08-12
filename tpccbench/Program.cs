#region

using System;
using System.IO;
using System.Threading;
using Amib.Threading;
using CommonClasses;
using DataAccess;
using TPC;
using TPC.C;
using NDesk.Options;
using System.Collections.Generic;

#endregion

namespace tpccbench
{
    internal static class Program
    {
        private static void ParseArgs(string[] args)
        {
            bool help = false;

            var p = new OptionSet()
            {
                { "Clients=", "number of clients you wish to run max 200", v => Globals.NumClients = Convert.ToInt32(v) },
                { "ConnectionString=", "Connection String in SQL Server Format", v => Globals.StrPublisherConn = v },
                { "Warehouses=", "Number of warehouses populated in test database", v => Globals.WH = Convert.ToInt32(v) },
                { "ClientDelay=", "Delay between queries issued by the Client", v => Globals.ClientSleepSec = Convert.ToInt32(v) },
                { "StoreProcedures=", "Use extended stored procedures instead of single issued statements", v => Globals.StoredProc = true },
                { "StaggerLoad=", "stagger Client load where {0} is the number of seconds to delay each Client", v => Globals.StaggeredLoad = Convert.ToInt32(v) },
                { "Loops=", "Number of loops per client. This overhides the --Minutes option", v => Globals.NumLoops = Convert.ToInt32(v) },
                { "Heartbeat=", "Log to hearbeat to track replication latency", v => Globals.Heartbeat = true },

                { "PercentNewOrder=", "Percent new order status", v => Globals.PNO = Convert.ToInt32(v) },
                { "PercentOrder=", "Percent order status", v => Globals.POS = Convert.ToInt32(v) },
                { "PercentPayment=", "Percent Payment", v => Globals.PP = Convert.ToInt32(v) },
                { "PercentDelivery=", "Percent delivery", v => Globals.PD = Convert.ToInt32(v) },
                { "PercentStockLevel=", "Percent stock level", v => Globals.PSL = Convert.ToInt32(v) },

                { "h|Help", v => help = true}
            };
            List<string> extra = p.Parse(args);

            if (help)
            {
                p.WriteOptionDescriptions(Console.Out);
                System.Environment.Exit(1);
            }

            // FIXME
            Globals.StrPublisherConn += ";Enlist=false;" +
                    "connection reset=false;" +
                    "connection lifetime=5;" +
                    "connection timeout=60;" +
                    "min pool size=2;" +
                    "max pool size=2000;";
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("");
            Console.WriteLine("TPC Benchmarking Tool");
            Console.WriteLine("Copyright (c) 2009 Nitrosphere Inc.");

            if (args.Length == 0)
            {
                char[] delimiterChars = {' '};
                if (File.Exists("tpccbench.txt"))
                {
                    //attempt to open file with command line args in it
                    StreamReader re = File.OpenText("tpccbench.txt");
                    string input = re.ReadToEnd();
                    string[] cmdargs = input.Split(delimiterChars);
                    re.Close();
                    //send the command line to the parser
                    ParseArgs(cmdargs);
                }
                else
                {
                    Globals.Err = 1;
                    Console.WriteLine("tpccbench.txt not found, no commands on command line found.");
                }
            }
            else
            {
                ParseArgs(args);
            }

            if (Globals.Err == 1)
            {
                Console.WriteLine("Error detected, execution stopped.");
            }
            else if (Globals.Err == 2)
            {
                Console.WriteLine("Program Exit.");
            }
            else
            {
                //LoadGlobal.LoadGeneralGlobals();
                LoadGlobals.LoadLogFilePath();

                var trd = new Thread(PLOG.WriteLog);
                //trd.IsBackground = true;
                trd.Start();

                PLOG.Write("Running Benchmark on server: " + Globals.StrPublisherConn);

                const string query = "select @@version";
                //Console.WriteLine(query);
                try
                {
                    //Globals.SQLVersion = 
                    ClientDataAccess.RunProc(Globals.StrPublisherConn, query);
                }
                catch
                {
                    Console.WriteLine("Unable to connect to database, correct and retry");
                    PLOG.Write("Unable to connect to database, correct and retry", 1);
                    Thread.Sleep(1000);
                    Globals.Err = 1;
                }

                if (Globals.Err == 1)
                {
                    Console.WriteLine("Error detected, execution stopped.");
                }
                else
                {
                    if (Globals.Heartbeat)
                    {
                        PLOG.Write("Heartbeat enabled.");
                        var heartbeat = new Thread(TpcUtils.Heartbeat) {IsBackground = true};
                        heartbeat.Start();
                    }

                    var stpStartInfo = new STPStartInfo();

                    if (!(Globals.StaggeredLoad == 0))
                    {
                        stpStartInfo.StartSuspended = false;
                        stpStartInfo.MaxWorkerThreads = Globals.NumClients + 1;
                        stpStartInfo.MinWorkerThreads = Globals.NumClients + 1;
                        stpStartInfo.IdleTimeout = 10*1000;
                    }
                    else
                    {
                        stpStartInfo.StartSuspended = true;
                        stpStartInfo.MaxWorkerThreads = Globals.NumClients + 1;
                        stpStartInfo.MinWorkerThreads = Globals.NumClients + 1;
                        stpStartInfo.IdleTimeout = 10*1000;
                    }

                    var smartThreadPool = new SmartThreadPool(stpStartInfo);


                    var s = new StopWatch();

                    int i = 0;

                    var clients = new Tpcc[Globals.NumClients];

                    PLOG.Write("Start TPC benchmark :" + DateTime.Now);
                    s.Start();

                    if (!(Globals.StaggeredLoad == 0))
                    {
                        while (i < Globals.NumClients)
                        {
                            clients[i] = new Tpcc {Clientid = i + 1};
                            smartThreadPool.QueueWorkItem(new WorkItemCallback(clients[i].Client));
                            Console.WriteLine("Client " + i + " Loaded.");
                            Thread.Sleep(Globals.StaggeredLoad);
                            i++;
                        }
                    }
                    else
                    {
                        while (i < Globals.NumClients)
                        {
                            clients[i] = new Tpcc {Clientid = i + 1};
                            smartThreadPool.QueueWorkItem(new WorkItemCallback(clients[i].Client));
                            i++;
                        }
                    }

                    PLOG.Write("Started On: " + DateTime.Now);
                    if (Globals.NumLoops == 0)
                    {
                        PLOG.Write("Target Run Time: " + (Globals.MaxRunTimeMin));
                    }
                    else
                    {
                        PLOG.Write("Number of Loops: " + (Globals.NumLoops));
                    }
                    PLOG.Write("number of clients to load: " + i);
                    PLOG.Write("Work load Mix:");
                    if (Globals.RawWrite == 0)
                    {
                        PLOG.Write("Percent New Orders: " + Globals.PNO);
                        PLOG.Write("Percent Order Status: " + (Globals.POS));
                        PLOG.Write("Percent Stock Level: " + (Globals.PSL));
                        PLOG.Write("Percent Payment: " + (Globals.PP));
                        PLOG.Write("Percent Delivery: " + (Globals.PD));
                        PLOG.Write("Total Percent: " +
                                   ((Globals.PNO) + (Globals.POS) + (Globals.PSL) + (Globals.PD) + (Globals.PP)));
                    }
                    else
                    {
                        PLOG.Write("Raw Inserts: 100");
                    }
                    PLOG.Read();

                    //Console.WriteLine("Threads waiting callback: " + smartThreadPool.WaitingCallbacks);

                    if (Globals.StaggeredLoad == 0)
                    {
                        smartThreadPool.Start();
                    }

                    // Wait for the completion of all work items
                    smartThreadPool.WaitForIdle();

                    s.Stop();

                    smartThreadPool.Shutdown();

                    if (Globals.RawWrite == 1)
                    {
                        PLOG.Write("Total number of operations: " + Globals.TotalCount);
                        PLOG.Write("elapsed time in seconds: " + s.GetElapsedTimeSecs());
                        PLOG.Write("elapsed time in minutes: " + s.GetElapsedTimeSecs()/60);
                        PLOG.Write("Total number of operations per minute: " +
                                   (Globals.TotalCount/(s.GetElapsedTimeSecs()/60)));
                    }
                    else
                    {
                        PLOG.Write("Total number of new orders: " + Globals.Countno);
                        PLOG.Write("Total number of order status: " + Globals.Countos);
                        PLOG.Write("Total number of payments: " + Globals.Countp);
                        PLOG.Write("Total number of new diliveries: " + Globals.Countd);
                        PLOG.Write("Total number of new stock level: " + Globals.Countsl);
                        PLOG.Write("Total number of operations: " +
                                   (Globals.Countno + Globals.Countos + Globals.Countp + Globals.Countd +
                                    Globals.Countsl));
                        PLOG.Write("elapsed time in seconds: " + s.GetElapsedTimeSecs());
                        PLOG.Write("elapsed time in minutes: " + s.GetElapsedTimeSecs()/60);
                        PLOG.Write("Total number of new orders per minute: " +
                                   (Globals.Countno/(s.GetElapsedTimeSecs()/60)));
                    }
                    PLOG.Write("end TPC benchmark :" + DateTime.Now);

                    PLOG.Read();
                    Globals.RunFlag = 1;
                }
                trd.Join(10000);
                //Console.ReadKey();
            }
        }
    }
}