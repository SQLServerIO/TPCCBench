using System;
using System.Threading;
using CommonClasses;
using DataAccess;

namespace TPC
{
    public static class TpcUtils
    {
        public static void Heartbeat()
        {
            int i = 1;
            while (true)
            {
                string query = "insert into HEARTBEAT (ID) VALUES(" + i + ")";
                try
                {
                    ClientDataAccess.RunProc(Globals.StrPublisherConn, query);
                }
                catch (Exception e)
                {
                    Errhandle.StopProcessing(e, query);
                }
                i++;
                Thread.Sleep(1000);
            }
// ReSharper disable FunctionNeverReturns
        }
// ReSharper restore FunctionNeverReturns
    }
}