using System;
using System.Data;
using System.Data.SqlClient;
using SharpNeat.Utility;

namespace TPCCDatabaseGenerator
{
    /// <summary>
    /// TPCCGenData is the container class for the routines that generate and load the actual data into the tables.
    /// </summary>
    public class TPCCGenData
    {
        private static readonly FastRandom Frnd = new FastRandom();
        private const String RandHold = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        private const String ZipRandHold = "1234567890";
        public int MaxNumWh;
        public int NumWh;
        public string Sqlconn;

        /// <summary>
        ///    /************************************************************************************************
        ///    Populate CUSTOMER table
        ///    By Wesley D. Brown
        ///    Date 11/24/2007
        ///    Mod
        ///    **Description**
        ///    Functions:
        ///    **End Discription**
        ///    **Change Log**
        ///    **End Change Log**
        ///    ************************************************************************************************/
        /// </summary>
        /// <returns></returns>
        public void BuildCustomerTable()
        {
            var currdate = new DateTime();
            var dt = new DataTable();

            var sqlConnect = new SqlConnection {ConnectionString = Sqlconn};
            sqlConnect.Open();

            var dc = new SqlCommand("select top 0 * from dbo.CUSTOMER", sqlConnect) {CommandType = CommandType.Text};
            var da = new SqlDataAdapter(dc);
            da.FillSchema(dt, SchemaType.Mapped);

            sqlConnect.Close();
            sqlConnect.Dispose();

            var bulkCopy = new SqlBulkCopy(Sqlconn,SqlBulkCopyOptions.TableLock) {DestinationTableName = "dbo.CUSTOMER", BatchSize = 10000};

            int i = NumWh;
            int tid = (NumWh*10*3000) + 1;


            while (i < MaxNumWh + 1)
            {
                for (int id = 1; id < 11; id++)
                {
                    for (int cid = 1; cid < 3001; cid++)
                    {
                        DataRow dr  = dt.NewRow();
                        dr["C_ID"] = "C_W" + i + "_D" + id + "_" + cid;
                        dr["C_D_ID"] = "D_W" + i + "_" + id;
                        dr["C_W_ID"] = "W_" + i;
                        dr["C_FIRST"] = RandomString(5, 16);
                        dr["C_MIDDLE"] = "oe";
                        dr["C_LAST"] = RandomString(8, 16);
                        dr["C_STREET_1"] = RandomString(10, 20);
                        dr["C_STREET_2"] = RandomString(10, 20);
                        dr["C_CITY"] = RandomString(10, 20);
                        dr["C_STATE"] = RandomString(2, 2);
                        dr["C_ZIP"] = RandZip();
                        dr["C_PHONE"] = RandomString(12, 12);
                        dr["C_SINCE"] = currdate.ToLongTimeString();
                        dr["C_CREDIT"] = "GC";
                        dr["C_CREDIT_LIM"] = 5000;
                        dr["C_DISCOUNT"] = .5;
                        dr["C_BALANCE"] = -10.00;
                        dr["C_YTD_PAYMENT"] = 10.00;
                        dr["C_PAYMENT_CNT"] = 1;
                        dr["C_DELIVERY_CNT"] = 0;
                        dr["C_DATA"] = RandomString(300, 500);
                        dr["SEQ_ID"] = tid;
                        dt.Rows.Add(dr);
                    }
                }
                bulkCopy.WriteToServer(dt);
                dt.Clear();

                i++;
            }
            return;
        }

        /// <summary>
        ///    /************************************************************************************************
        ///    Populate DISTRICT table
        ///    By Wesley D. Brown
        ///    Date 11/24/2007
        ///    Mod
        ///    **Description**
        ///    Functions:
        ///    **End Discription**
        ///    **Change Log**
        ///    **End Change Log**
        ///    ************************************************************************************************/
        /// </summary>
        /// <returns></returns>
        public void BuildDistrictTable()
        {
            var dt = new DataTable();

            var sqlConnect = new SqlConnection {ConnectionString = Sqlconn};
            sqlConnect.Open();

            var dc = new SqlCommand("select top 0 * from dbo.DISTRICT", sqlConnect) {CommandType = CommandType.Text};
            var da = new SqlDataAdapter(dc);
            da.FillSchema(dt, SchemaType.Mapped);

            sqlConnect.Close();
            sqlConnect.Dispose();

            var bulkCopy = new SqlBulkCopy(Sqlconn,SqlBulkCopyOptions.TableLock) {DestinationTableName = "dbo.DISTRICT", BatchSize = 10000};
            //,SqlBulkCopyOptions.TableLock);

            int i = 1;
            int tid = 1;

            while (i < MaxNumWh + 1)
            {
                for (int id = 1; id < 11; id++)
                {
                    DataRow dr  = dt.NewRow();
                    dr["D_ID"] = "D_W" + i + "_" + id;
                    dr["D_W_ID"] = "W_" + i;
                    dr["D_NAME"] = RandomString(6, 10);
                    dr["D_STREET_1"] = RandomString(10, 20);
                    dr["D_STREET_2"] = RandomString(10, 20);
                    dr["D_CITY"] = RandomString(10, 20);
                    dr["D_STATE"] = RandomString(2, 2);
                    dr["D_ZIP"] = RandZip();
                    dr["D_TAX"] = 0.1000;
                    dr["D_YTD"] = 30000;
                    dr["D_NEXT_O_ID"] = "3001";
                    dr["SEQ_ID"] = tid;
                    dt.Rows.Add(dr);
                    tid++;
                }
                bulkCopy.WriteToServer(dt);
                dt.Clear();

                i++;
            }
            return;
        }

        /// <summary>
        ///    /************************************************************************************************
        ///    Populate HISTORY table
        ///    By Wesley D. Brown
        ///    Date 11/24/2007
        ///    Mod
        ///    **Description**
        ///    Functions:
        ///    **End Discription**
        ///    **Change Log**
        ///    **End Change Log**
        ///    ************************************************************************************************/
        /// </summary>
        /// <returns></returns>
        public void BuildHistoryTable()
        {
            var currdate = new DateTime();
            var dt = new DataTable();

            var sqlConnect = new SqlConnection {ConnectionString = Sqlconn};
            sqlConnect.Open();

            var dc = new SqlCommand("select top 0 * from dbo.HISTORY", sqlConnect) {CommandType = CommandType.Text};
            var da = new SqlDataAdapter(dc);
            da.FillSchema(dt, SchemaType.Mapped);

            sqlConnect.Close();
            sqlConnect.Dispose();

            var bulkCopy = new SqlBulkCopy(Sqlconn,SqlBulkCopyOptions.TableLock) {DestinationTableName = "dbo.HISTORY", BatchSize = 10000};

            int i = NumWh;
            int tid = (NumWh*10*3000) + 1;


            while (i < MaxNumWh + 1)
            {
                for (int id = 1; id < 11; id++) //check to see if ID should be 11 and not 10
                {
                    for (int cid = 1; cid < 3001; cid++)
                    {
                        DataRow dr  = dt.NewRow();
                        dr["H_C_ID"] = "C_W" + i + "_D" + id + "_" + cid;
                        dr["H_C_D_ID"] = "D_W" + i + "_" + id;
                        dr["H_C_W_ID"] = "W_" + i;
                        dr["H_D_ID"] = "D_W" + i + "_" + id;
                        dr["H_W_ID"] = "W_" + i;
                        dr["H_DATE"] = currdate.ToLongTimeString();
                        dr["H_AMOUNT"] = 10.00;
                        dr["H_DATA"] = RandomString(12, 24);
                        dr["SEQ_ID"] = tid;
                        dt.Rows.Add(dr);
                        tid++;
                    }
                }
                bulkCopy.WriteToServer(dt);
                dt.Clear();

                i++;
            }
            return;
        }

        /// <summary>
        ///    /************************************************************************************************
        ///    Populate ITEM table
        ///    By Wesley D. Brown
        ///    Date 11/24/2007
        ///    Mod
        ///    **Description**
        ///    Functions:
        ///    **End Discription**
        ///    **Change Log**
        ///    **End Change Log**
        ///    ************************************************************************************************/
        /// </summary>
        /// <returns></returns>
        public void BuildItemsTable()
        {
            var dt = new DataTable();

            var sqlConnect = new SqlConnection {ConnectionString = Sqlconn};
            sqlConnect.Open();

            var dc = new SqlCommand("select top 0 * from dbo.ITEM", sqlConnect) {CommandType = CommandType.Text};
            var da = new SqlDataAdapter(dc);
            da.FillSchema(dt, SchemaType.Mapped);

            sqlConnect.Close();
            sqlConnect.Dispose();

            var bulkCopy = new SqlBulkCopy(Sqlconn,SqlBulkCopyOptions.TableLock) {DestinationTableName = "dbo.ITEM", BatchSize = 10000}; //,SqlBulkCopyOptions.TableLock);


            int i = 1;

            while (i < 100001)
            {
                DataRow dr  = dt.NewRow();
                dr["I_ID"] = i;
                dr["I_IM_ID"] = RandomString(10, 20);
                dr["I_NAME"] = RandomString(14, 24);
                dr["I_PRICE"] = Frnd.Next(1, 100);
                dr["I_DATA"] = RandomString(26, 50);
                i++;
                dt.Rows.Add(dr);
            }
            bulkCopy.WriteToServer(dt);
            return;
        }

        /// <summary>
        ///    /************************************************************************************************
        ///    Populate NEW_ORDER table
        ///    By Wesley D. Brown
        ///    Date 11/24/2007
        ///    Mod
        ///    **Description**
        ///    Functions:
        ///    **End Discription**
        ///    **Change Log**
        ///    **End Change Log**
        ///    ************************************************************************************************/
        /// </summary>
        /// <returns></returns>
        public void BuildNewOrderTable()
        {
            var dt = new DataTable();
            var sqlConnect = new SqlConnection {ConnectionString = Sqlconn};
            sqlConnect.Open();

            var dc = new SqlCommand("select top 0 * from dbo.NEW_ORDER", sqlConnect) {CommandType = CommandType.Text};
            var da = new SqlDataAdapter(dc);
            da.FillSchema(dt, SchemaType.Mapped);

            sqlConnect.Close();
            sqlConnect.Dispose();

            var bulkCopy = new SqlBulkCopy(Sqlconn,SqlBulkCopyOptions.TableLock) {DestinationTableName = "dbo.NEW_ORDER", BatchSize = 10000};

            int i = NumWh;
            int tid;
            int tolid;

            if (NumWh == 1)
            {
                tid = 1;
                tolid = 1;
            }
            else
            {
                tid = (NumWh*10*3000) + 1;
                tolid = (NumWh*10*3000*10) + 1;
            }

            while (i < MaxNumWh + 1)
            {
                for (int id = 1; id < 11; id++)
                {
                    for (int cid = 1; cid < 3001; cid++)
                    {
                        if (cid < 901)
                        {
                            DataRow dr  = dt.NewRow();
                            dr["NO_O_ID"] = tid;
                            dr["NO_D_ID"] = "D_W" + i + "_" + id;
                            dr["NO_W_ID"] = "W_" + i;
                            dr["SEQ_ID"] = tolid;
                            tolid++;
                            dt.Rows.Add(dr);
                        }
                        tid++;
                    }
                }

                bulkCopy.WriteToServer(dt);
                dt.Clear();

                i++;
            }

            return;
        }

        /// <summary>
        ///    /************************************************************************************************
        ///    Populate O_ORDER table
        ///    By Wesley D. Brown
        ///    Date 11/24/2007
        ///    Mod
        ///    **Description**
        ///    Functions:
        ///    **End Discription**
        ///    **Change Log**
        ///    **End Change Log**
        ///    ************************************************************************************************/
        /// </summary>
        /// <returns></returns>
        public void BuildOrderTable()
        {
            var currdate = new DateTime();
            var dt = new DataTable();
            var sqlConnect = new SqlConnection {ConnectionString = Sqlconn};
            sqlConnect.Open();

            var dc = new SqlCommand("select top 0 * from dbo.O_ORDER", sqlConnect) {CommandType = CommandType.Text};
            var da = new SqlDataAdapter(dc);
            da.FillSchema(dt, SchemaType.Mapped);

            sqlConnect.Close();
            sqlConnect.Dispose();

            var bulkCopy = new SqlBulkCopy(Sqlconn,SqlBulkCopyOptions.TableLock) {DestinationTableName = "dbo.O_ORDER", BatchSize = 10000};

            int i = NumWh;
            int tid;

            if (NumWh == 1)
            {
                tid = 1;
            }
            else
            {
                tid = (NumWh*10*3000) + 1;
            }

            while (i < MaxNumWh + 1)
            {
                for (int id = 1; id < 11; id++)
                {
                    for (int cid = 1; cid < 3001; cid++)
                    {
                        DataRow dr  = dt.NewRow();
                        dr["O_ID"] = tid;
                        dr["O_D_ID"] = "D_W" + i + "_" + id;
                        dr["O_W_ID"] = "W_" + i;
                        dr["O_C_ID"] = "C_W" + i + "_D" + id + "_" + cid;
                        dr["O_ENTRY_D"] = currdate.ToLongTimeString();
                        dr["O_CARRIER_ID"] = "";
                        dr["O_OL_CNT"] = Frnd.Next(5, 15);
                        dr["O_ALL_LOCAL"] = "1";
                        dr["SEQ_ID"] = tid;
                        dr["SOURCE_TIME"] = currdate.ToLongTimeString();

                        tid++;
                        dt.Rows.Add(dr);
                    }
                }
                bulkCopy.WriteToServer(dt);
                dt.Clear();

                i++;
            }
            return;
        }

        /// <summary>
        ///    /************************************************************************************************
        ///    Populate ORDER_LINE table
        ///    By Wesley D. Brown
        ///    Date 11/24/2007
        ///    Mod
        ///    **Description**
        ///    Functions:
        ///    **End Discription**
        ///    **Change Log**
        ///    **End Change Log**
        ///    ************************************************************************************************/
        /// </summary>
        /// <returns></returns>
        public void BuildOrderLineTable()
        {
            var dt = new DataTable();
            var sqlConnect = new SqlConnection {ConnectionString = Sqlconn};
            sqlConnect.Open();

            var dc = new SqlCommand("select top 0 * from dbo.ORDER_LINE", sqlConnect) {CommandType = CommandType.Text};
            var da = new SqlDataAdapter(dc);
            da.FillSchema(dt, SchemaType.Mapped);

            sqlConnect.Close();
            sqlConnect.Dispose();

            var bulkCopy = new SqlBulkCopy(Sqlconn,SqlBulkCopyOptions.TableLock) {DestinationTableName = "dbo.ORDER_LINE", BatchSize = 10000};

            int i = NumWh;
            int tid;
            int tolid;

            if (NumWh == 1)
            {
                tid = 1;
                tolid = 1;
            }
            else
            {
                tid = (NumWh*10*3000) + 1;
                tolid = (NumWh*10*3000*10) + 1;
            }

            while (i < MaxNumWh + 1)
            {
                for (int id = 1; id < 11; id++)
                {
                    for (int cid = 1; cid < 3001; cid++)
                    {
                        for (int olid = 1; olid < 11; olid++)
                        {
                            DataRow dr  = dt.NewRow();
                            dr["OL_O_ID"] = tid;
                            dr["OL_D_ID"] = "D_W" + i + "_" + id;
                            dr["OL_W_ID"] = "W_" + i;
                            dr["OL_NUMBER"] = "OL_NUM_" + tolid;
                            dr["OL_I_ID"] = Frnd.Next(1, 100000);
                            dr["OL_SUPPLY_W_ID"] = "W_" + i;
                            dr["OL_DELIVERY_D"] = "01/01/1900 00:00:00.000"; // SqlDateTime.Null;// sqldatenull;
                            dr["OL_QUANTITY"] = 5;
                            dr["OL_AMOUNT"] = Frnd.Next(1, 9999);
                            dr["OL_DIST_INFO"] = RandomString(24, 24);
                            dr["SEQ_ID"] = tolid;

                            tolid++;
                            dt.Rows.Add(dr);
                        }
                        tid++;
                    }
                }
                bulkCopy.WriteToServer(dt);
                dt.Clear();
                i++;
            }
            return;
        }

        /// <summary>
        ///    /************************************************************************************************
        ///    Populate STOCK table
        ///    By Wesley D. Brown
        ///    Date 11/24/2007
        ///    Mod
        ///    **Description**
        ///    Functions:
        ///    **End Discription**
        ///    **Change Log**
        ///    **End Change Log**
        ///    ************************************************************************************************/
        /// </summary>
        /// <returns></returns>
        public void BuildStockTable()
        {
            var dt = new DataTable();
            var sqlConnect = new SqlConnection {ConnectionString = Sqlconn};
            sqlConnect.Open();

            var dc = new SqlCommand("select top 0 * from dbo.STOCK", sqlConnect) {CommandType = CommandType.Text};
            var da = new SqlDataAdapter(dc);
            da.FillSchema(dt, SchemaType.Mapped);

            sqlConnect.Close();
            sqlConnect.Dispose();

            var bulkCopy = new SqlBulkCopy(Sqlconn,SqlBulkCopyOptions.TableLock) {DestinationTableName = "dbo.STOCK", BatchSize = 10000};

            int i = NumWh;

            while (i < MaxNumWh + 1)
            {
                for (int id = 1; id < 100001; id++)
                {
                    DataRow dr  = dt.NewRow();
                    dr["S_I_ID"] = id;
                    dr["S_W_ID"] = "W_" + i;
                    dr["S_QUANTITY"] = Frnd.Next(10, 100);
                    dr["S_DIST_01"] = RandomString(24, 24);
                    dr["S_DIST_02"] = RandomString(24, 24);
                    dr["S_DIST_03"] = RandomString(24, 24);
                    dr["S_DIST_04"] = RandomString(24, 24);
                    dr["S_DIST_05"] = RandomString(24, 24);
                    dr["S_DIST_06"] = RandomString(24, 24);
                    dr["S_DIST_07"] = RandomString(24, 24);
                    dr["S_DIST_08"] = RandomString(24, 24);
                    dr["S_DIST_09"] = RandomString(24, 24);
                    dr["S_DIST_10"] = RandomString(24, 24);
                    dr["S_YTD"] = 0;
                    dr["S_ORDER_CNT"] = 0;
                    dr["S_REMOTE_CNT"] = 0;
                    dr["S_DATA"] = RandomString(26, 50);
                    dr["SEQ_ID"] = id;

                    dt.Rows.Add(dr);
                }
                bulkCopy.WriteToServer(dt);
                dt.Clear();
                i++;
            }
            return;
        }

        /// <summary>
        ///    /************************************************************************************************
        ///    Populate WAREHOUSE table
        ///    By Wesley D. Brown
        ///    Date 11/24/2007
        ///    Mod
        ///    **Description**
        ///    Functions:
        ///    **End Discription**
        ///    **Change Log**
        ///    **End Change Log**
        ///    ************************************************************************************************/
        /// </summary>
        /// <returns></returns>
        public void BuildWarehouseTable()
        {
            var dt = new DataTable();
            var sqlConnect = new SqlConnection {ConnectionString = Sqlconn};
            sqlConnect.Open();

            var dc = new SqlCommand("select top 0 * from dbo.WAREHOUSE", sqlConnect) {CommandType = CommandType.Text};
            var da = new SqlDataAdapter(dc);
            da.FillSchema(dt, SchemaType.Mapped);

            sqlConnect.Close();
            sqlConnect.Dispose();

            var bulkCopy = new SqlBulkCopy(Sqlconn,SqlBulkCopyOptions.TableLock) {DestinationTableName = "dbo.WAREHOUSE", BatchSize = 10000};

            int i = NumWh;


            while (i < MaxNumWh + 1)
            {
                DataRow dr  = dt.NewRow();

                dr["W_ID"] = "W_" + i;
                dr["W_NAME"] = RandomString(6, 10);
                dr["W_STREET_1"] = RandomString(10, 20);
                dr["W_STREET_2"] = RandomString(10, 20);
                dr["W_CITY"] = RandomString(10, 20);
                dr["W_STATE"] = RandomString(2, 2);
                dr["W_ZIP"] = RandZip();
                dr["W_TAX"] = 0.1000;
                dr["W_YTD"] = 3000000.00;
                dr["SEQ_ID"] = i;

                dt.Rows.Add(dr);
                i++;
            }
            bulkCopy.WriteToServer(dt);
            dt.Clear();
            return;
        }

        /// <summary>
        /// Generates a random string with the given length
        /// </summary>
        /// <param name="strMin">
        ///   minimum size of the string
        /// </param>
        /// <param name="strMax"></param>
        /// <returns>Random string</returns>
        private static String RandomString(int strMin, int strMax)
        {
            String randomString = "";
            for (int x = 0; x < Frnd.Next(strMin, strMax); ++x)
            {
                randomString += RandHold.Substring(Frnd.Next(0, 62), 1);
            }
            return randomString;
        }

        /// <summary>
        /// Generates a random zip code string with the given length
        /// </summary>
        /// <returns>Random string</returns>
        private static String RandZip()
        {
            string holdZip = "";
            for (int x = 0; x < 4; ++x)
            {
                holdZip += ZipRandHold.Substring(Frnd.Next(0, 9), 1);
            }

            holdZip += "11111";
            return holdZip;
        }

        //private static Random Frnd = new Random();
    }
}