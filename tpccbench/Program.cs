#region

using System;
using System.IO;
using System.Threading;
using Amib.Threading;
using CLParser;
using CommonClasses;
using DataAccess;
using TPC;
using TPC.C;

#endregion

namespace tpccbench
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            CLP.About();

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
                    CLP.Cmdline(cmdargs);
                }
                else
                {
                    Globals.Err = 1;
                    Console.WriteLine("tpccbench.txt not found, no commands on command line found.");
                }
            }
            else
            {
                CLP.Cmdline(args);
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
                LoadGlobals.LoadMasterConnectionString();
                LoadGlobals.LoadLogFilePath();

                var trd = new Thread(PLOG.WriteLog);
                //trd.IsBackground = true;
                trd.Start();

                PLOG.Write("Running Benchmark on server: " + Globals.ServerName);
                PLOG.Write("Running Benchmark on database: " + Globals.DatabaseName);

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
                    if (Globals.Heartbeat == 1)
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