using System;
using CommonClasses;

namespace CLParser
{
    public static class CLP
    {
        public static void About()
        {
            Console.WriteLine("");
            Console.WriteLine("TPC Benchmarking Tool");
            Console.WriteLine("Copyright (c) 2009 Nitrosphere Inc.");
        }

        public static void Cmdline(string[] args)
        {
            int numargs = args.GetLength(0);
            string arg;

            if (numargs > 0)
            {
                foreach (string cmd in args)
                {
                    //select tpc benchmark type defaults to c 
                    //not implemented yet
                    if (cmd.ToLower().Contains("\\b:"))
                    {
                        arg = cmd.Remove(0, 3);
                        arg = arg.Trim();

                        if (arg.Length == 0)
                        {
                            Globals.Err = 1;
                            Console.WriteLine(cmd + " no valid argument after identifier");
                            Showhelp();
                            break;
                        }

                        Globals.BenchType = arg;
                    }
                        //number of warehouses
                    else if (cmd.ToLower().Contains("\\wh:"))
                    {
                        arg = cmd.Remove(0, 4);
                        arg = arg.Trim();
                        if (arg.Length == 0)
                        {
                            Globals.Err = 1;
                            Console.WriteLine(cmd + " no valid argument after identifier");
                            Showhelp();
                            break;
                        }
                        Globals.WH = Convert.ToInt32(arg);
                    }
                        //number of clients
                    else if (cmd.ToLower().Contains("\\c:"))
                    {
                        arg = cmd.Remove(0, 3);
                        arg = arg.Trim();
                        if (arg.Length == 0)
                        {
                            Globals.Err = 1;
                            Console.WriteLine(cmd + " no valid argument after identifier");
                            Showhelp();
                            break;
                        }
                        Globals.NumClients = Convert.ToInt32(arg);
                    }
                        //Client sleep cycle maximum
                    else if (cmd.ToLower().Contains("\\cs:"))
                    {
                        arg = cmd.Remove(0, 4);
                        arg = arg.Trim();
                        if (arg.Length == 0)
                        {
                            Globals.Err = 1;
                            Console.WriteLine(cmd + " no valid argument after identifier");
                            Showhelp();
                            break;
                        }
                        Globals.ClientSleepSec = Convert.ToInt32(arg)*1000;
                    }

                        //percent new orders
                    else if (cmd.ToLower().Contains("\\pno:"))
                    {
                        arg = cmd.Remove(0, 5);
                        arg = arg.Trim();
                        if (arg.Length == 0)
                        {
                            Globals.Err = 1;
                            Console.WriteLine(cmd + " no valid argument after identifier");
                            Showhelp();
                            break;
                        }
                        Globals.PNO = Convert.ToInt32(arg);
                    }

                        //percent new order status
                    else if (cmd.ToLower().Contains("\\pos:"))
                    {
                        arg = cmd.Remove(0, 5);
                        arg = arg.Trim();
                        if (arg.Length == 0)
                        {
                            Globals.Err = 1;
                            Console.WriteLine(cmd + " no valid argument after identifier");
                            Showhelp();
                            break;
                        }
                        Globals.POS = Convert.ToInt32(arg);
                    }

                        //percent pament
                    else if (cmd.ToLower().Contains("\\pp:"))
                    {
                        arg = cmd.Remove(0, 4);
                        arg = arg.Trim();
                        if (arg.Length == 0)
                        {
                            Globals.Err = 1;
                            Console.WriteLine(cmd + " no valid argument after identifier");
                            Showhelp();
                            break;
                        }
                        Globals.PP = Convert.ToInt32(arg);
                    }

                        //percent dilivery
                    else if (cmd.ToLower().Contains("\\pd:"))
                    {
                        arg = cmd.Remove(0, 4);
                        arg = arg.Trim();
                        if (arg.Length == 0)
                        {
                            Globals.Err = 1;
                            Console.WriteLine(cmd + " no valid argument after identifier");
                            Showhelp();
                            break;
                        }
                        Globals.PD = Convert.ToInt32(arg);
                    }

                        //percent stock lookup
                    else if (cmd.ToLower().Contains("\\psl:"))
                    {
                        arg = cmd.Remove(0, 5);
                        arg = arg.Trim();
                        if (arg.Length == 0)
                        {
                            Globals.Err = 1;
                            Console.WriteLine(cmd + " no valid argument after identifier");
                            Showhelp();
                            break;
                        }
                        Globals.PSL = Convert.ToInt32(arg);
                    }

                        //server name
                    else if (cmd.ToLower().Contains("\\s:"))
                    {
                        arg = cmd.Remove(0, 3);
                        arg = arg.Trim();
                        if (arg.Length == 0)
                        {
                            Globals.Err = 1;
                            Console.WriteLine(cmd + " no valid argument after identifier");
                            Showhelp();
                            break;
                        }

                        Globals.ServerName = arg;
                    }
                        //database name
                    else if (cmd.ToLower().Contains("\\d:"))
                    {
                        arg = cmd.Remove(0, 3);
                        arg = arg.Trim();
                        if (arg.Length == 0)
                        {
                            Globals.Err = 1;
                            Console.WriteLine(cmd + " no valid argument after identifier");
                            Showhelp();
                            break;
                        }

                        Globals.DatabaseName = arg;
                        //Globals.DatabaseName = Globals.DatabaseName.Trim;
                    }
                        //user name
                    else if (cmd.ToLower().Contains("\\u:"))
                    {
                        arg = cmd.Remove(0, 3);
                        arg = arg.Trim();
                        if (arg.Length == 0)
                        {
                            Globals.Err = 1;
                            Console.WriteLine(cmd + " no valid argument after identifier");
                            Showhelp();
                            break;
                        }

                        Globals.UserName = arg;
                        //Globals.UserName = Globals.UserName.Trim;
                    }
                        //fixed number of loops
                    else if (cmd.ToLower().Contains("\\nl:"))
                    {
                        arg = cmd.Remove(0, 4);
                        arg = arg.Trim();
                        if (arg.Length == 0)
                        {
                            Globals.Err = 1;
                            Console.WriteLine(cmd + " no valid argument after identifier");
                            Showhelp();
                            break;
                        }
                        Globals.NumLoops = Convert.ToInt32(arg);
                    }
                        //password
                    else if (cmd.ToLower().Contains("\\p:"))
                    {
                        arg = cmd.Remove(0, 3);
                        arg = arg.Trim();
                        if (arg.Length == 0)
                        {
                            Globals.Err = 1;
                            Console.WriteLine(cmd + " no valid argument after identifier");
                            Showhelp();
                            break;
                        }

                        Globals.Password = arg;
                        //Globals.Password = Globals.Password.Trim;
                    }
                        //logging level debug,info,query,error,warn
                    else if (cmd.ToLower().Contains("\\l:"))
                    {
                        arg = cmd.Remove(0, 3);
                        arg = arg.Trim();
                        if (arg.Length == 0)
                        {
                            Globals.Err = 1;
                            Console.WriteLine(cmd + " no valid argument after identifier");
                            Showhelp();
                            break;
                        }

                        Globals.StrLoggingLevel = arg;
                        //Globals.UserName = Globals.UserName.Trim;
                    }
                        //trusted
                    else if (cmd.ToLower().Contains("\\t"))
                    {
                        Globals.Trusted = "true";
                    }

                        //use stored procedures
                    else if (cmd.ToLower().Contains("\\sp"))
                    {
                        Globals.StoredProc = 1;
                    }
                        //full out writes to a table
                    else if (cmd.ToLower().Contains("\\rw"))
                    {
                        Globals.RawWrite = 1;
                    }
                        //write to Heartbeat table
                    else if (cmd.ToLower().Contains("\\hb"))
                    {
                        Globals.Heartbeat = 1;
                    }

                        //Stagger Client load
                    else if (cmd.ToLower().Contains("\\stl:"))
                    {
                        arg = cmd.Remove(0, 5);
                        arg = arg.Trim();
                        if (arg.Length == 0)
                        {
                            Globals.Err = 1;
                            Console.WriteLine(cmd + " no valid argument after identifier");
                            Showhelp();
                            break;
                        }

                        Globals.StaggeredLoad = Convert.ToInt32(arg)*1000;
                    }

                        //show help
                    else if (cmd.ToLower().Contains("\\?"))
                    {
                        Showhelp();
                        Globals.Err = 2;
                    }
                        //log to console
                        //for debugging only
                    else if (cmd.ToLower().Contains("\\lc"))
                    {
                        Globals.LogToConsole = 1;
                    }

                        //run time
                    else if (cmd.ToLower().Contains("\\m:"))
                    {
                        arg = cmd.Remove(0, 3);
                        arg = arg.Trim();
                        if (arg.Length == 0)
                        {
                            Globals.Err = 1;
                            Console.WriteLine(cmd + " no valid argument after identifier");
                            Showhelp();
                            break;
                        }
                        Globals.MaxRunTimeMin = Convert.ToInt32(arg);
                        //Globals.MaxRunTimeMin = Convert.ToInt32(cmd);
                    }
                    else
                    {
                        Console.WriteLine("invalid arguements specified");
                        Console.WriteLine(cmd + " is incorrect");
                        Showhelp();
                        Globals.Err = 1;
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("No arguements specified.");
                Showhelp();
                Globals.Err = 1;
            }

            if (!((Globals.PD + Globals.PNO + Globals.POS + Globals.PP + Globals.PSL) == 100))
            {
                Console.WriteLine(Globals.PD + Globals.PNO + Globals.POS + Globals.PP + Globals.PSL);
                Console.WriteLine(
                    "You must specifiy all work load variables and they must total 100. Please correct and run again");
                Globals.Err = 1;
            }
        }

        private static void Showhelp()
        {
            Console.WriteLine("");
            Console.WriteLine("Commands are in \\x:option format.");
            Console.WriteLine("No spaces between the \\x: and the option specified.");
            Console.WriteLine("Valid command line arguements are:");
            Console.WriteLine("");
            Console.WriteLine("\\c:{1} number of clients you wish to run max 200");
            Console.WriteLine("");
            Console.WriteLine("\\s:{servername} name of the database server to benchmark");
            Console.WriteLine("");
            Console.WriteLine("\\d:{target database} the TPC database you wish to benchmark");
            Console.WriteLine("");
            Console.WriteLine("\\u:{username} login name to connect to server. For sql authentication only");
            Console.WriteLine("              ignored if \\t option is used");
            Console.WriteLine("");
            Console.WriteLine("\\p:{password} login password to connect to server. For sql authentication only");
            Console.WriteLine("              ignored if \\t option is used");
            Console.WriteLine("");
            Console.WriteLine("\\t use trusted connection notice no : or any following commands");
            Console.WriteLine("");
            Console.WriteLine("\\wh:{2} number of warehouses to use during tests.");
            Console.WriteLine("         Depends on number of warehouses populated in test database.");
            Console.WriteLine("");
            Console.WriteLine("\\m:{1} number of minutes to execute the test minimum 1 no maximum");
            Console.WriteLine("");
            Console.WriteLine("\\cs:{50} maximum number of delay between queries issued by the Client.");
            Console.WriteLine("         Any number between 0 and 50 is normal 0 for maximum throughput");
            Console.WriteLine("         50 for a standard tpc benchmark Client thinking delay");
            Console.WriteLine("");
            Console.WriteLine("\\sp used extended stored procedures instead of single issued statements");
            Console.WriteLine("");
            Console.WriteLine("\\stl:{0} stagger Client load where {0} is the number of seconds to delay each Client");
            Console.WriteLine("");
            Console.WriteLine("\\nl:{0} number of fixed loops to execute per Client. This overrides the \\m: switch");
            Console.WriteLine("");
            Console.WriteLine("\\hb Log to Heartbeat table to track replication latency.");
            Console.WriteLine("");
            Console.WriteLine("Below is the work load mix switches all 5 must be used and must equal 100.");
            Console.WriteLine("\\pno:{45} percent new order status");
            Console.WriteLine("\\pos:{43} percent order status");
            Console.WriteLine("\\pp:{4} percent Payment");
            Console.WriteLine("\\pd:{4} percent dilivery");
            Console.WriteLine("\\psl:{4} percent stock level");
            Console.WriteLine("");
            Console.WriteLine("\\? this help screen");
        }
    }
}