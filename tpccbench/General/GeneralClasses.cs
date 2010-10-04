using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace CommonClasses
{
    public static class Errhandle
    {
/*
        public void StopProcessing(Exception e)
        {
            string stackTraceMsg = e.StackTrace;
            string errorMessage = e.Message;
            if (Globals.LogToConsole == 1)
            {
                Console.WriteLine("error occured");
                Console.WriteLine(errorMessage);
                Console.WriteLine(stackTraceMsg);
                Console.WriteLine("end error");
            }
            else
            {
                PLOG.Write(errorMessage, 1);
            }
            //throw(e);
        }
*/

        public static void StopProcessing(Exception e, string query)
        {
            string stackTraceMsg = e.StackTrace;
            string errorMessage = e.Message;
            if (Globals.LogToConsole == 1)
            {
                Console.WriteLine("error occured");
                Console.WriteLine(errorMessage);
                Console.WriteLine(stackTraceMsg);
                Console.WriteLine("end error");
            }
            else
            {
                PLOG.Write(query, 1);
                PLOG.Write(errorMessage, 1);
            }
            //throw(e);
        }

/*
        public void WriteToEventLog(Exception objError)
        {
            //*************************************
            //* Purpose:Writing error to the windows event log
            //* Input parameters:
            //*objError----Exception object
            //* Returns : 
            //*nothing
            //* ***************************************************
            var objEventLog = new EventLog {Source = "TPCCBENCH"};
            objEventLog.WriteEntry(objError.Message);

            try
            {
                Console.WriteLine(objError.Message);
            }
            catch
            {
                objEventLog.WriteEntry("Could not write to console");
            }
        }
*/
    }

    internal static class Globals
    {
        public static readonly Object ThisLock = new Object();

        static Globals()
        {
            BenchType = "c";
            SQLVersion = "";
            WH = 25;
            PSL = 4;
            PP = 4;
            PD = 4;
            POS = 43;
            PNO = 45;
            MaxRunTimeMin = 1;
            NumClients = 1;
            MaxThreads = "";
            Trusted = "true";
            Password = "";
            UserName = "";
            DatabaseName = "TPCCDB";
            ServerName = "Planetarydb";
            StrLoggingLevel = "debug";
            StrLogPathErr = "";
            StrLogPath = "";
            StrPublisherConn = "";
            RawWrite = 0;
        Countno = 0;
        TotalCount = 0;
        Countos = 0;
        Countp = 0;
        Countd = 0;
        Countsl = 0;

        }

// ReSharper disable UnusedAutoPropertyAccessor.Global
        public static string BenchType { get; set; }
// ReSharper restore UnusedAutoPropertyAccessor.Global

        public static string StrPublisherConn { get; set; }

        public static string StrLogPath { get; set; }

        public static string StrLogPathErr { get; set; }

// ReSharper disable UnusedAutoPropertyAccessor.Global
        public static string StrLoggingLevel { get; set; }
// ReSharper restore UnusedAutoPropertyAccessor.Global

        public static string ServerName { get; set; }

        public static string DatabaseName { get; set; }

        public static string UserName { get; set; }

        public static string Password { get; set; }

        public static string Trusted { get; set; }

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
        public static string MaxThreads { get; set; }
// ReSharper restore UnusedAutoPropertyAccessor.Global
// ReSharper restore MemberCanBePrivate.Global

        public static int LogToConsole { get; set; }

        public static int NumClients { get; set; }

        public static int MaxRunTimeMin { get; set; }

        public static int Err { get; set; }

        public static int Countno { get; set; }

        public static int TotalCount { get; set; }

        public static int Countos { get; set; }

        public static int Countp { get; set; }

        public static int Countd { get; set; }

        public static int Countsl { get; set; }

        public static int PNO { get; set; }

        public static int POS { get; set; }

        public static int PD { get; set; }

        public static int PP { get; set; }

        public static int PSL { get; set; }

        public static int WH { get; set; }

        public static int ClientSleepSec { get; set; }

        public static int NumLoops { get; set; }

        public static int StoredProc { get; set; }

        public static int StaggeredLoad { get; set; }

        public static int Heartbeat { get; set; }

        public static int RawWrite { get; set; }

        public static int RunFlag { get; set; }

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
        public static string SQLVersion { get; set; }
// ReSharper restore UnusedAutoPropertyAccessor.Global
// ReSharper restore MemberCanBePrivate.Global
    }

    public class StopWatch
    {
        private bool _running;
        private DateTime _startTime;
        private DateTime _stopTime;


        public void Start()
        {
            _startTime = DateTime.Now;
            _running = true;
        }


        public void Stop()
        {
            _stopTime = DateTime.Now;
            _running = false;
        }


        // elaspsed time in milliseconds
/*
        public double GetElapsedTime()
        {
            TimeSpan interval;

            if (_running)
                interval = DateTime.Now - _startTime;
            else
                interval = _stopTime - _startTime;

            return interval.TotalMilliseconds;
        }
*/


        // elaspsed time in seconds
        public double GetElapsedTimeSecs()
        {
            TimeSpan interval;

            if (_running)
                interval = DateTime.Now - _startTime;
            else
                interval = _stopTime - _startTime;

            return interval.TotalSeconds;
        }
    }

    public static class PLOG
    {
        private static readonly ReaderWriterLock Listlock =
            new ReaderWriterLock();

        private static readonly List<string> WriteBuffer = new List<string>(100);
        private static readonly List<string> WriteErrBuffer = new List<string>(100);

        public static void Write(string msg)
        {
            Listlock.AcquireWriterLock(-1);
            WriteBuffer.Add(msg);
            Listlock.ReleaseWriterLock();
        }

        public static void Write(string msg, int lvl)
        {
#if DEBUG
            var frame = new StackFrame(2);
#else
                        StackFrame frame = new StackFrame(1);
#endif


            switch (lvl)
            {
                case 1:
                    Listlock.AcquireWriterLock(-1);
                    WriteErrBuffer.Add("-----------------------");
                    WriteErrBuffer.Add("DEBUG");
                    WriteErrBuffer.Add(DateTime.Now.ToString());
                    try
                    {
                        WriteErrBuffer.Add(frame.GetMethod().Name);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    WriteErrBuffer.Add(msg);
                    Listlock.ReleaseWriterLock();
                    break;
                case 2:
                    Listlock.AcquireWriterLock(-1);
                    WriteErrBuffer.Add("-----------------------");
                    WriteErrBuffer.Add("DEBUG");
                    WriteErrBuffer.Add(DateTime.Now.ToString());
                    try
                    {
                        WriteErrBuffer.Add(frame.GetMethod().Name);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    Listlock.ReleaseWriterLock();
                    break;
                case 3:
                    Listlock.AcquireWriterLock(-1);
                    WriteErrBuffer.Add("-----------------------");
                    WriteErrBuffer.Add("DEBUG");
                    WriteErrBuffer.Add(DateTime.Now.ToString());
                    try
                    {
                        WriteErrBuffer.Add(frame.GetMethod().Name);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    Listlock.ReleaseWriterLock();
                    break;
                default:
                    Listlock.AcquireWriterLock(-1);
                    WriteErrBuffer.Add("-----------------------");
                    WriteErrBuffer.Add("DEBUG");
                    WriteErrBuffer.Add(DateTime.Now.ToString());
                    try
                    {
                        WriteErrBuffer.Add(frame.GetMethod().Name);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    WriteErrBuffer.Add(msg);
                    Listlock.ReleaseWriterLock();
                    break;
            }
        }

        public static void Read()
        {
            Listlock.AcquireReaderLock(-1);
            foreach (string item in WriteBuffer)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            Listlock.ReleaseReaderLock();
        }

        private static void WriteBufferToFile()
        {
            // create a writer and open the file
            TextWriter tw = new StreamWriter(Globals.StrLogPath, true);
            //TextWriter tw = new StreamWriter("testlog.txt",true);

            // write current buffer to logfile
            Listlock.AcquireWriterLock(-1);

            foreach (string item in WriteBuffer)
            {
                //write item
                tw.WriteLine(DateTime.Now + " - " + item);
            }

            WriteBuffer.Clear();
            Listlock.ReleaseWriterLock();

            // close the stream
            tw.Close();
        }

        private static void WriteErrBufferToFile()
        {
            // create a writer and open the file
            TextWriter tw = new StreamWriter(Globals.StrLogPathErr, true);
            //TextWriter tw = new StreamWriter("testlog.txt",true);

            // write current buffer to logfile
            Listlock.AcquireWriterLock(-1);

            foreach (string item in WriteErrBuffer)
            {
                //write item
                tw.WriteLine(DateTime.Now + " - " + item);
            }

            WriteErrBuffer.Clear();
            Listlock.ReleaseWriterLock();

            // close the stream
            tw.Close();
        }

        public static void WriteLog()
        {
            while (true)
            {
                WriteErrBufferToFile();
                WriteBufferToFile();
                if (Globals.RunFlag == 1)
                {
                    break;
                }
                Thread.Sleep(500);
            }
        }
    }

    //    public static class LAB
    //    {
    //        static readonly LogWriter _writer;
    //        static LAB()
    //        {
    //            // The formatter is responsible for the
    //            // look of the message. Notice the tokens:
    //            // {timestamp}, {newline}, {message}, {category}
    //            //TextFormatter formatter = new TextFormatter
    //            //(
    //            //    "Timestamp: {timestamp(local)}{newline}" +
    //            //    "Category: {category}{newline}" +
    //            //    "Priority: {priority}{newline}" +
    //            //    "EventId: {eventid}{newline}" +
    //            //    "Severity: {severity}{newline}" +
    //            //    "Machine: {machine}{newline}" +
    //            //    "Calling Method: {message}{newline}"
    //            //);
    //            TextFormatter formatter = new TextFormatter
    //            (
    //                "Timestamp: {timestamp(local)}" +
    //                "Category: {category} " +
    //                "EventId: {eventid} " +
    //                "Severity: {severity} " +
    //                "Calling Method: {message}"
    //            );
    //            // Log messages to a log file.
    //            // Use the formatter mentioned above
    //            // as well as the header and footer
    //            // specified.

    //            FlatFileTraceListener logFileListener =
    //                new FlatFileTraceListener(Globals.StrLogPath,"", "----------", formatter);

    //            //RollingFlatFileTraceListener logFileListener =
    //            //    new RollingFlatFileTraceListener(
    //            //    Globals.StrLogPath
    //            //    , "---"
    //            //    , ""
    //            //    , formatter
    //            //    , 100000 
    //            //    , "MM dd yyyy HH mm ss"
    //            //    , RollFileExistsBehavior.Overwrite
    //            //    , RollInterval.None);


    //            //event log listener just for failed logging attempts.
    //            EventLogTraceListener eventLogListener = new EventLogTraceListener("Application");
    //            // My collection of TraceListeners.
    //            // I am only using one.  Could add more.
    //            LogSource mainLogSource =
    //                new LogSource("MainLogSource", SourceLevels.All);

    //            LogSource eventLogSource =
    //                new LogSource("eventLogSource", SourceLevels.All);

    //            eventLogSource.Listeners.Add(eventLogListener);

    //            mainLogSource.Listeners.Add(logFileListener);

    //            // Assigning a non-existant LogSource
    //            // for Logging Application Block
    //            // Specials Sources I don't care about.
    //            // Used to say "don't log".
    //            LogSource nonExistantLogSource = new LogSource("Empty");

    //            // I want all messages with a category of
    //            // "Error" or "Debug" to get distributed
    //            // to all TraceListeners in my mainLogSource.
    //            IDictionary<string, LogSource> traceSources = new Dictionary<string, LogSource>();

    //            if (Globals.StrLoggingLevel == "query")
    //            {
    //                traceSources.Add("Error", mainLogSource);
    //                traceSources.Add("Warning", mainLogSource);
    //                traceSources.Add("Info", mainLogSource);
    //                traceSources.Add("Debug", mainLogSource);
    //                traceSources.Add("Query", mainLogSource);
    //            }
    //            else if (Globals.StrLoggingLevel == "debug")
    //            {
    //                traceSources.Add("Error", mainLogSource);
    //                traceSources.Add("Warning", mainLogSource);
    //                traceSources.Add("Info", mainLogSource);
    //                traceSources.Add("Debug", mainLogSource);
    //            }
    //            else if (Globals.StrLoggingLevel == "info")
    //            {
    //                traceSources.Add("Error", mainLogSource);
    //                traceSources.Add("Warning", mainLogSource);
    //                traceSources.Add("Info", mainLogSource);
    //            }
    //            else if (Globals.StrLoggingLevel == "warn")
    //            {
    //                traceSources.Add("Error", mainLogSource);
    //                traceSources.Add("Warning", mainLogSource);
    //            }
    //            else if (Globals.StrLoggingLevel == "error")
    //            {
    //                traceSources.Add("Error", mainLogSource);
    //            }
    //            else
    //            {
    //                traceSources.Add("Error", mainLogSource);
    //            }

    //            // Let's glue it all together.
    //            // No filters at this time.
    //            // I won't log a couple of the Special
    //            // Sources: All Events and Events not
    //            // using "Error" or "Debug" categories.
    //            _writer = new LogWriter(new ILogFilter[0],
    //                            traceSources,
    //                            nonExistantLogSource,
    //                            eventLogSource,
    //                            mainLogSource,
    //                            "Error",
    //                            false,
    //                            true);
    //        }

    //        /// <summary>
    //        /// Writes Debug info to log.
    //        /// </summary>
    //        /// <param name="message">Debug Message</param>
    //        public static void Write(string message)
    //        {
    //            Write(message, "Debug");
    //        }

    //        /// <summary>
    //        /// Writes a message to the log using the specified
    //        /// category.
    //        /// </summary>
    //        /// <param name="message"></param>
    //        /// <param name="category"></param>
    //        public static void Write(string message, string category)
    //        {
    //#if DEBUG
    //            StackFrame frame = new StackFrame(2);
    //#else
    //            StackFrame frame = new StackFrame(1);
    //#endif

    //            LogEntry entry = new LogEntry();
    //            entry.Categories.Add(category);
    //            try
    //            {
    //                entry.Message = frame.GetMethod().Name + "\r\n" + "Message: " + message;
    //            }
    //            catch(Exception e)
    //            {
    //                entry.Message = "\r\n" + "Message: " + message;
    //                Console.WriteLine("Failed to write to log");
    //                Console.WriteLine(e.Message.ToString());
    //            }
    //            _writer.Write(entry);
    //        }
    //    }
}