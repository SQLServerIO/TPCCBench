using System;
using System.Data;
using System.Data.SqlClient;
using CommonClasses;

namespace DataAccess
{
    public static class ClientDataAccess
    {
        //private  
        //**************************************
        //* Purpose: Accessing SQL database 
        //*Methods:
        //*GetDataSet
        //*RunProc
        //*GetDataReader
        //*GetDataView
        //* ************************************* 
/*
        public DataSet GetDataSet(string strConnect, string[] ProcName, string[] DataTable)
        {
            //********************************
            //* Purpose: Returns Dataset for one or multi datatables 
            //* Input parameters:
            //*ProcName() ---StoredProcedures name in array
            //*DataTable()---DataTable name in array
            //* Returns :
            //*DataSet Object contains data
            //**************************************************
            SqlDataAdapter dadEorder;
            DataSet dstEorder = new DataSet();
            SqlConnection conn = new SqlConnection(strConnect);
            try
            {
                int intCnt = ProcName.GetUpperBound(0);
                // if one datatable and SP
                if (intCnt == 0)
                {
                    dadEorder = new SqlDataAdapter(ProcName[0], conn);
                    dadEorder.Fill(dstEorder, DataTable[0]);
                }
                    // more than one datatable and one SP
                else
                {
                    conn.Open();
                    //add first data table and first SP
                    dadEorder = new SqlDataAdapter(ProcName[0], conn);
                    dadEorder.Fill(dstEorder, DataTable[0]);
                    // add second datatable and second SP onwards
                    for (int i = 1; i < (intCnt + 1); i++)
                    {
                        dadEorder.SelectCommand = new SqlCommand(ProcName[i], conn);
                        dadEorder.Fill(dstEorder, DataTable[i]);
                    }
                        conn.Close();
                        conn.Dispose();
                }
                return dstEorder;
            }
            catch (Exception objError)
            {
                //write error to the windows event log
                Errhandle.StopProcessing(objError, ProcName[0]);

                //                Console.WriteLine(objError.Message.ToString());
                //Console.WriteLine("Query -----------\r\n" + strCommandText);
                throw;
            }
            finally
            {
                    conn.Close();
                    conn.Dispose();
            }
        }
*/

        public static void RunProc(string strConnect, string procName)
        {
            //****************************************
            //* Purpose: Executing Stored Procedures where UPDATE, INSERT
            //*and DELETE statements are expected but does not 
            //*work for select statement is expected. 
            //* Input parameters: 
            //*procName ---StoredProcedures name 
            //* Returns : 
            //*nothing 
            //* ***************************************
            string strCommandText = procName;
            //create a new Connection object using the connection string
            var objConnect = new SqlConnection(strConnect);
            //create a new Command using the CommandText and Connection object
            var objCommand = new SqlCommand(strCommandText, objConnect) {CommandTimeout = 3600};
            //set the timeout in seconds

            try
            {
                objConnect.Open();
                objCommand.ExecuteNonQuery();
            }
            catch (Exception objError)
            {
                //write error to the windows event log
                Errhandle.StopProcessing(objError, strCommandText);

                // Console.WriteLine(objError.Message.ToString());
                //Console.WriteLine("Query -----------\r\n" + strCommandText);
                throw;
            }
            finally
            {
                    objConnect.Close();
                    objConnect.Dispose();
            }
        }

        public static SqlDataReader GetDataReader(string strConnect, string procName)
        {
            //**************************************
            //* Purpose: Getting DataReader for the given Procedure
            //* Input parameters:
            //*procName ---StoredProcedures name
            //* Returns :
            //*DataReader contains data
            //* ************************************
            string strCommandText = procName;
            SqlDataReader objDataReader;
            //create a new Connection object using the connection string
            var objConnect = new SqlConnection(strConnect);
            //create a new Command using the CommandText and Connection object
            var objCommand = new SqlCommand(strCommandText, objConnect) {CommandTimeout = 3600};
            //set the timeout in seconds

            try
            {
                //open the connection and execute the command
                objConnect.Open();
                //objDataAdapter.SelectCommand = objCommand
                objDataReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception objError)
            {
                    objConnect.Close();
                    objConnect.Dispose();

                //write error to the log
                Errhandle.StopProcessing(objError, strCommandText);
                //Console.WriteLine(objError.Message.ToString());
                //Console.WriteLine("Query -----------\r\n" + strCommandText);
                throw;
            }
            return objDataReader;
        }

/*
        public DataView GetDataView(string strConnect, string ProcName, string dataSetTable)
        {
            //*****************************************
            //* Purpose: Getting DataReader for the given Procedure
            //* Input parameters:
            //* ProcName ---StoredProcedures name
            //* dataSetTable--dataSetTable name sting
            //* Returns :
            //* DataView contains data
            //* ****************************************
            string strCommandText = ProcName;
            DataView objDataView;
            //create a new Connection object using the connection string
            var objConnect = new SqlConnection(strConnect);
            //create a new Command using the CommandText and Connection object
            var objCommand = new SqlCommand(strCommandText, objConnect);
            //declare a variable to hold a DataAdaptor object
            var objDataAdapter = new SqlDataAdapter();
            DataSet objDataSet;
            objDataSet = new DataSet(dataSetTable);

            //set the timeout in seconds
            objCommand.CommandTimeout = 3600;

            try
            {
                //open the connection and execute the command
                objConnect.Open();
                objDataAdapter.SelectCommand = objCommand;
                objDataAdapter.Fill(objDataSet);

                objDataView = new DataView(objDataSet.Tables[0]);

                //objDataReader = objCommand.ExecuteReader()
            }
            catch (Exception objError)
            {
                //write error to the log
                Errhandle.StopProcessing(objError, strCommandText);

                // Console.WriteLine(objError.Message.ToString());
                //Console.WriteLine("Query -----------\r\n" + strCommandText);
                throw;
            }
            finally
            {
                    objConnect.Close();
                    objConnect.Dispose();
            }
            return objDataView;
        }
*/
    }
}