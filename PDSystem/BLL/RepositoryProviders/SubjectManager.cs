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
    public class SubjectManager
    {
        #region Class Level Objects

        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();

        #endregion

        #region Class Constructor
        public SubjectManager()
         {
            conn.ConnectionString = SqlHelper.connectionstring;
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
         }
        #endregion

        #region Public/Private Members
      
        //Members of Subject Table
        private int _pksubjID;
        private string _subName;
        private string _subLevel;
        private int _CreatedBy;
        private int _ModifiedBy;
        private bool _subisActive;
        private string _Token;

        #endregion

        #region Public/Private Properties
        public int PksubjID
        {
            get { return _pksubjID; }
            set { _pksubjID = value; }
        }
        public string SubName
        {
            get { return _subName; }
            set { _subName = value; }
        }
        public string SubLevel
        {
            get { return _subLevel; }
            set { _subLevel = value; }
        }
        public bool SubisActive
        {
            get { return _subisActive; }
            set { _subisActive = value; }
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

            param[0] = new SqlParameter("@pksubID", this.PksubjID);
            param[1] = new SqlParameter("@subName", this.SubName);
            param[2] = new SqlParameter("@subLevel", this.SubLevel);
            param[3] = new SqlParameter("@CreatedBy", this.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", this.ModifiedBy);
            param[5] = new SqlParameter("@subIsActive", this.SubisActive);
            param[6] = new SqlParameter("@Token", this.Token);

            if (this.Token == "I")
            {
                object o = SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, "sp_InsertUpdateSubject", param);
                if (o != null)
                    result = int.Parse(o.ToString());
            }
            else
            {
                result = SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "sp_InsertUpdateSubject", param);
                if (result == -1)
                    result = 1;
                else
                    result = 0;
            }

            return result;

        }

        public SubjectManager SelectTable(string tblName, string whereCond)
        {

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@tblName", tblName);
            param[1] = new SqlParameter("@whereCond", whereCond);

            SqlDataReader Dr = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, "sp_GetTable", param);
            if (Dr.Read())
            {
                this.SubName= Dr["subName"].ToString();
                this.SubLevel = Dr["subLevel"].ToString();
                this.SubisActive = bool.Parse(Dr["subIsActive"].ToString());
                return this;
            }
            return null;
        }
        
        #endregion
    }
}
