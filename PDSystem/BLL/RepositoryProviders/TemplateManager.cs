using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;
using System.Configuration;

namespace BLL.RepositoryProvider
{
    public class TemplateManager
    {
        #region Class Constructor

        public TemplateManager()
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

        #region Public/Private Members
      
        public int _ID = 0;
        public int _CreatedBy = 0;
        public int _ModifiedBy = 0;
        public string _Token;
    
        #endregion

        #region Public/Private Properties

        public int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID == value)
                    return;
                _ID = value;
            }
        }
        public int CreatedBy
        {
            get
            {
                return _CreatedBy;
            }
            set
            {
                if (_CreatedBy == value)
                    return;
                _CreatedBy = value;
            }
        }
        public int ModifiedBy
        {
            get
            {
                return _ModifiedBy;
            }
            set
            {
                if (_ModifiedBy == value)
                    return;
                _ModifiedBy = value;
            }
        }
        public string Token
        {
            get
            {
                return _Token;
            }
            set
            {
                if (_Token == value)
                    return;
                _Token = value;
            }
        }
   
        #endregion

        #region Helper Methods

        public int DelAnnouncement()
        {
            int result = 0;

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@TableOrView", "DS_tbl_Announcements");
            param[1] = new SqlParameter("@Columns", "Active ='False'");
            param[2] = new SqlParameter("@WhereClause", "ID=" + this.ID);
            param[3] = new SqlParameter("@SQL", "");

            result = SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "sp_Update", param);
            
            if (result == -1)
                 return 1;
            else
                return 0;
           
        }
               
        public int CRUDBrand()
        {
            int result = 0;
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@ID", this.ID);
            param[1] = new SqlParameter("@CreatedBy", this.CreatedBy);
            param[2] = new SqlParameter("@ModifiedBy", this.ModifiedBy);
            param[3] = new SqlParameter("@Action", this.Token);

            if (this.Token == "ADD")
            {
                object o = SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, "sp_InsertUpdateBrand", param);
                if (o != null)
                    result = int.Parse(o.ToString());
            }
            else
            {
                result = SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "sp_InsertUpdateBrand", param);
                if (result == -1)
                    result = 1;
                else
                    result = 0;
            }

            return result;

        }

        #endregion
    }
}
