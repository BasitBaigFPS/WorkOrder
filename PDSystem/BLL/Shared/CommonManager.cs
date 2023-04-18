using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;


namespace BLL.Shared
{
    public class CommonManager
    {

        #region Class Constructor
        
        public CommonManager()
         {
            conn.ConnectionString = SqlHelper.connectionstring;
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
         } 

        #endregion

        #region Class Level Objects

        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();

        #endregion

        #region Helper Methods

        public SqlDataReader RetrieveRecords(string ConnectionString,string TableOrView, int SelectedPage, int PageSize, string Columns, string OrderByColumn, string OrderByDirection, string WhereClause)
        {
            SqlParameter[] arParams = new SqlParameter[7];

            arParams[0] = new SqlParameter("@TableOrView", TableOrView);
            arParams[1] = new SqlParameter("@SelectedPage", SelectedPage);
            arParams[2] = new SqlParameter("@PageSize", PageSize);
            arParams[3] = new SqlParameter("@Columns", Columns);
            arParams[4] = new SqlParameter("@OrderByColumn", OrderByColumn);
            arParams[5] = new SqlParameter("@OrderByDirection", OrderByDirection);
            arParams[6] = new SqlParameter("@WhereClause", WhereClause);

            
            // SqlDataReader that will hold the returned results		
            SqlDataReader dr = null;
            
            dr = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, "GetList", arParams);

            return dr;

        }

        public SqlDataReader RetrieveRecords(string TableOrView, int SelectedPage, int PageSize, string Columns, string OrderByColumn, string OrderByDirection, string WhereClause)
        {
             SqlParameter[] arParams = new SqlParameter[7];

            arParams[0] = new SqlParameter("@TableOrView", TableOrView);
            arParams[1] = new SqlParameter("@SelectedPage", SelectedPage);
            arParams[2] = new SqlParameter("@PageSize", PageSize);
            arParams[3] = new SqlParameter("@Columns", Columns);
            arParams[4] = new SqlParameter("@OrderByColumn", OrderByColumn);
            arParams[5] = new SqlParameter("@OrderByDirection", OrderByDirection);
            arParams[6] = new SqlParameter("@WhereClause", WhereClause);

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(SqlHelper.connectionstring);
            }
            catch
            {
            }

           // SqlDataReader that will hold the returned results		
			SqlDataReader dr = null;
			
		     dr = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "GetList", arParams);

             return dr;

        }

        #endregion

    }
    
}
