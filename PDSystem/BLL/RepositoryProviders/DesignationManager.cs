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
    public class DesignationManager
    {
        #region Class Level Objects

        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();

        #endregion

        #region Class Constructor
        public DesignationManager()
         {
            conn.ConnectionString = SqlHelper.connectionstring;
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
         }
        #endregion

        #region Public/Private Members
      
        //Members of Brand Table
        private int _pkdesigID;

        private string _desigName;


        private string _desigDesc;

        
        private int _CreatedBy;
        private int _ModifiedBy;
        private bool _desigisActive;
        private string _Token;

        #endregion

        #region Public/Private Properties

        public int PkdesigID
        {
            get { return _pkdesigID; }
            set { _pkdesigID = value; }
        }
        public string DesigName
        {
            get { return _desigName; }
            set { _desigName = value; }
        }
        public string DesigDesc
        {
            get { return _desigDesc; }
            set { _desigDesc = value; }
        }
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        public int ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        public bool DesigIsActive
        {
            get { return _desigisActive; }
            set { _desigisActive = value; }
        }
        public string Token
        {
            get { return _Token; }
            set { _Token = value; }
        }

        #endregion

        #region Helper Methods

        public int IUDesig()
        {

            int result = 0;
            SqlParameter[] param = new SqlParameter[7];

            param[0] = new SqlParameter("@pkdesigID", this.PkdesigID);
            param[1] = new SqlParameter("@desigName", this.DesigName);
            param[2] = new SqlParameter("@desigDesc", this.DesigDesc);
            param[3] = new SqlParameter("@CreatedBy", this.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", this.ModifiedBy);
            param[5] = new SqlParameter("@desigIsActive", this.DesigIsActive);
            param[6] = new SqlParameter("@Token", this.Token);

            if (this.Token == "I")
            {
                object o = SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, "sp_InsertUpdateDesignation", param);
                if (o != null)
                    result = int.Parse(o.ToString());
            }
            else
            {
                result = SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "sp_InsertUpdateDesignation", param);
                if (result == -1)
                    result = 1;
                else
                    result = 0;
            }

            return result;

        }

        public DesignationManager SelectTable(string tblName, string whereCond)
        {

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@tblName", tblName);
            param[1] = new SqlParameter("@whereCond", whereCond);

            SqlDataReader Dr = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, "sp_GetTable", param);
            if (Dr.Read())
            {
                this.DesigName= Dr["desigName"].ToString();
                this.DesigDesc = Dr["desigDesc"].ToString();
                this.DesigIsActive = bool.Parse(Dr["desigIsActive"].ToString());
                return this;
            }
            return null;
        }
        
        #endregion
    }
}
