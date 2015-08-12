using NDesk.Options;
using System;
using System.Collections.Generic;
using TPCCDatabaseGenerator;

namespace TpccGen
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string connstring = null;
            string table = null;
            string startwh = "1";
            string endwh = "2";
            List<string> extra;

            bool help = false;

            var p = new OptionSet()
            {
                { "ConnectionString=", v => connstring = v },
                { "Tablename=",        v => table = v },
                { "Start=",            v => startwh = v},
                { "End=",              v => endwh = v},
                { "h|help",           v => help = v != null },
            };

            try
            {
                extra = p.Parse(args);
                if (help)
                {
                    p.WriteOptionDescriptions(Console.Out);
                }

            }
            catch (OptionException oe )
            {
                p.WriteOptionDescriptions(Console.Out);
            }

            Console.WriteLine(connstring);
            Console.WriteLine(table);
            Console.WriteLine(startwh);
            Console.WriteLine(endwh);
            
            var sGenData = new TPCCGenData { Sqlconn = connstring };

            sGenData.NumWh = Convert.ToInt32(startwh);
            sGenData.MaxNumWh = Convert.ToInt32(endwh);
            
            switch (table)
            {
                case "warehouse":
                    sGenData.BuildWarehouseTable();
                    break;
                case "item":
                    sGenData.BuildItemsTable();
                    break;
                case "district":
                    sGenData.BuildDistrictTable();
                    break;
                case "customer":
                    sGenData.BuildCustomerTable();
                    break;
                case "stock":
                    sGenData.BuildStockTable();
                    break;
                case "order":
                    sGenData.BuildOrderTable();
                    break;
                case "neworder":
                    sGenData.BuildNewOrderTable();
                    break;
                case "orderline":
                    sGenData.BuildOrderLineTable();
                    break;
                case "history":
                    sGenData.BuildHistoryTable();
                    break;

                default:
                    Console.WriteLine("Invalid selection.");
                    break;
            }
        }
    }
}