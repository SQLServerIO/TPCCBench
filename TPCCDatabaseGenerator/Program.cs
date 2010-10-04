using System;

namespace TpccGen
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string dbsrv = "";
            string dbname = "";
            string table = "";
            string startwh = "";
            string endwh = "";

            for (int ar = 0; ar < args.Length; ar++)
            {
                if (args[ar].Contains("\\SR"))
                {
                    dbsrv = args[ar].Replace("\\SR", "");
                }
                if (args[ar].Contains("\\D"))
                {
                    dbname = args[ar].Replace("\\D", "");
                }
                if (args[ar].Contains("\\T"))
                {
                }
                if (args[ar].Contains("\\U"))
                {
                    args[ar].Replace("\\U", "");
                }
                if (args[ar].Contains("\\P"))
                {
                    args[ar].Replace("\\P", "");
                }
                if (args[ar].Contains("\\TN"))
                {
                    table = args[ar].Replace("\\TN", "");
                }
                if (args[ar].Contains("\\SW"))
                {
                    startwh = args[ar].Replace("\\SW", "");
                }
                if (args[ar].Contains("\\EW"))
                {
                    endwh = args[ar].Replace("\\EW", "");
                }


                Console.WriteLine(args[ar]);
            }
            Console.WriteLine(dbsrv);
            Console.WriteLine(dbname);
            Console.WriteLine(table);
            Console.WriteLine(startwh);
            Console.WriteLine(endwh);
            int numWhStart = Convert.ToInt32(startwh);
            //number of warehouses you wish to load             
            int numWhLoad = Convert.ToInt32(endwh);

            var sGenData = new TPCCGenData
                               {
                                   Sqlconn = "Server=" + dbsrv + ";Database=" + dbname + ";Trusted_Connection=True;"
                               };
            Console.WriteLine(sGenData.Sqlconn);
            sGenData.NumWh = numWhStart;
            sGenData.MaxNumWh = numWhLoad;

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
                    Console.WriteLine("Invalid selection. Please select 1, 2, or 3.");
                    break;
            }
        }
    }
}