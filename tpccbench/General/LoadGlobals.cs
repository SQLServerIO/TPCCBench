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
            Globals.StrLogPath = "tpcbench.log"; //FileName;


            if (File.Exists(Globals.StrLogPath))
            {
                var rnd = new Random();
                try
                {
                    File.Move(Globals.StrLogPath,
                              "tpcbench_" + Convert.ToString(rnd.Next()) + ".log");
                }
                catch
                {
                    Globals.StrLogPath = "tpcbench_2.log"; //FileName;
                }
            }

            Globals.StrLogPathErr = "tpcbench_Err.log"; //FileName;


            if (File.Exists(Globals.StrLogPathErr))
            {
                var rnd = new Random();
                try
                {
                    File.Move(Globals.StrLogPathErr,
                              "tpcbench_Err_" + Convert.ToString(rnd.Next()) + ".log");
                }
                catch
                {
                    Globals.StrLogPathErr = "tpcbench_Err_2.log";
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

    }
}