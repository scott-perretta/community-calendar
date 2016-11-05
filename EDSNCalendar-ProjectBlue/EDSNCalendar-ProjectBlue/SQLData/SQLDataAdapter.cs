﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace EDSNCalendar_ProjectBlue.SQLData
{
    public class SQLDataAdapter
    {
        private MySqlConnection Connect()
        {
            string MyConnectionString = "Server=127.0.0.1;Database=edsncalendar;Uid=root;Pwd=pass;";
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            return connection;
        }

        private void Disconnect(MySqlConnection connection)
        {
            connection.Close();
        }

        private DataSet getDataSet(string sQuery)
        {
            MySqlConnection con = null;
            DataSet ds = new DataSet();
            try
            {
                con = Connect();         
                con.Open();
                MySqlCommand cmd;
                cmd = con.CreateCommand();
                cmd.CommandText = sQuery;
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                adp.Fill(ds);
            }
            catch(MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return ds;
        }

        //Executes a SQL Query and returns the number of rows affected
        private int ExecuteQuery(string sQuery)
        {
            int iRowsAffected = 0;
            MySqlConnection con = null;
            try
            {
                con = Connect();
                con.Open();
                MySqlCommand cmd;
                cmd = con.CreateCommand();
                cmd.CommandText = sQuery;
                iRowsAffected = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return iRowsAffected;
        }

        public void QueryExecute(string sQuery)
        {
            int iRowsAffected = ExecuteQuery(sQuery);
        }
        public string Query4String(string sQuery)
        {
            string sResult = string.Empty;
            DataSet ds = getDataSet(sQuery);
            sResult = ds.Tables[0].Rows[0][0].ToString();
            return sResult;
        }
        public int Query4Int(string sQuery)
        {
            int iResult;
            DataSet ds = getDataSet(sQuery);
            iResult = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            return iResult;
        }
        public DataTable Query4DataTable(string sQuery)
        {
            DataTable dtResults = new DataTable();
            DataSet ds = getDataSet(sQuery);
            dtResults = ds.Tables[0];
            return dtResults;
        }
        public DataSet Query4DataSet(string sQuery)
        {
            DataSet dsResult = new DataSet();
            dsResult = getDataSet(sQuery);
            return dsResult;
        }
    }
}