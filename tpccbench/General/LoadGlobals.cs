using System;
using System.IO;

namespace CommonClasses
{
    public static class LoadGlobals
    {
/*
        public Int32 UnixTime(DateTime dt)
        {
            dt = dt.ToUniversalTime();
            TimeSpan ts = (dt - new DateTime(1970, 1, 1, 0, 0, 0));
            var unixTime = (Int32) ts.TotalSeconds;
            return unixTime;
        }
*/

        public static void LoadLogFilePath()
        {
            Globals.StrLogPath = "tpcbench_" + Globals.ServerName.Replace('\\', '_') + ".log"; //FileName;


            if (File.Exists("tpcbench_" + Globals.ServerName.Replace('\\', '_') + ".log"))
            {
                var rnd = new Random();
                try
                {
                    File.Move("tpcbench_" + Globals.ServerName.Replace('\\', '_') + ".log",
                              "tpcbench_" + Globals.ServerName.Replace('\\', '_') + "_" + Convert.ToString(rnd.Next()) +
                              ".log");
                }
                catch
                {
                    Globals.StrLogPath = "tpcbench_" + Globals.ServerName.Replace('\\', '_') + "_2.log"; //FileName;
                }
            }

            Globals.StrLogPathErr = "tpcbench_Err_" + Globals.ServerName.Replace('\\', '_') + ".log"; //FileName;


            if (File.Exists("tpcbench_Err_" + Globals.ServerName.Replace('\\', '_') + ".log"))
            {
                var rnd = new Random();
                try
                {
                    File.Move("tpcbench_Err_" + Globals.ServerName.Replace('\\', '_') + ".log",
                              "tpcbench_Err_" + Globals.ServerName.Replace('\\', '_') + "_" +
                              Convert.ToString(rnd.Next()) + ".log");
                }
                catch
                {
                    Globals.StrLogPathErr = "tpcbench_Err_" + Globals.ServerName.Replace('\\', '_') + "_2.log";
                    //FileName;
                }
            }
        }

/*
        public void LoadGeneralGlobals()
        {
            //string MaxThreads = "200";
            //string ServerName = "PlanetaryDB";
            //string DatabaseName = "TPCCDB";
            //string UserName = "";
            //string Password = "";
            //string Trusted = "true";

            //Globals.MaxThreads = MaxThreads;
            //Globals.ServerName = ServerName;
            //Globals.DatabaseName = DatabaseName;
            //Globals.UserName = UserName;
            //Globals.Password = Password;
            //Globals.Trusted = Trusted;
        }
*/

        public static void LoadMasterConnectionString()
        {
            string strConnect;

            //Port = "1433";
            if (Globals.Trusted == "true")
            {
                strConnect =
                    "Data Source=" + Globals.ServerName + ";" +
                    "Initial Catalog=" + Globals.DatabaseName + ";" +
                    "Integrated Security=SSPI;" +
                    "Enlist=false;" +
                    "connection reset=false;" +
                    "connection lifetime=5;" +
                    "connection timeout=60;" +
                    "min pool size=2;" +
                    "max pool size=2000;";
            }
            else
            {
                strConnect =
                    "Data Source=" + Globals.ServerName + ";" +
                    "Initial Catalog=" + Globals.DatabaseName + ";" +
                    "Uid=" + Globals.UserName + ";" +
                    "Pwd=" + Globals.Password + ";" +
                    "Enlist=false;" +
                    "connection reset=false;" +
                    "connection lifetime=5;" +
                    "connection timeout=60;" +
                    "min pool size=2;" +
                    "max pool size=2000;";
            }
            Globals.StrPublisherConn = strConnect;
        }
    }
}