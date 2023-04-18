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
    public class SectionManager
    {
        #region Class Level Objects

        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();

        #endregion

        #region Class Constructor
        public SectionManager()
         {
            conn.ConnectionString = SqlHelper.connectionstring;
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
         }
        #endregion

        #region Public/Private Members
      
        //Members of Brand Table
        private int _pksecID;

        
        private int _pkgrdID;
        private int _pkbrhID;
        private string _secName;
        private string _secTeacherName;
        private int _CreatedBy;
        private int _ModifiedBy;
        private bool _secisActive;
        private string _Token;

        #endregion

        #region Public/Private Properties
        public int PksecID
        {
            get { return _pksecID; }
            set { _pksecID = value; }
        }
        public int PkgrdID
        {
            get { return _pkgrdID; }
            set { _pkgrdID = value; }
        }
        public int PkbrhID
        {
            get { return _pkbrhID; }
            set { _pkbrhID = value; }
        }
        public string SecName
        {
            get { return _secName; }
            set { _secName = value; }
        }
        public string SecTeacherName
        {
            get { return _secTeacherName; }
            set { _secTeacherName = value; }
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
        public bool SecIsActive
        {
            get { return _secisActive; }
            set { _secisActive = value; }
        }
        public string Token
        {
            get { return _Token; }
            set { _Token = value; }
        }

        #endregion

        #region Helper Methods

        public int IUSec()
        {

            int result = 0;
            SqlParameter[] param = new SqlParameter[9];

            param[0] = new SqlParameter("@pkbrhID", this.PkbrhID);
            param[1] = new SqlParameter("@pkgrdID", this.PkgrdID);
            param[2] = new SqlParameter("@pksecID", this.PksecID);
            param[3] = new SqlParameter("@secName", this.SecName);
            param[4] = new SqlParameter("@SecTeacherName", this.SecTeacherName);
            param[5] = new SqlParameter("@CreatedBy", this.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", this.ModifiedBy);
            param[7] = new SqlParameter("@SecIsActive", this.SecIsActive);
            param[8] = new SqlParameter("@Token", this.Token);

            if (this.Token == "I")
            {
                object o = SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, "sp_InsertUpdateSection", param);
                if (o != null)
                    result = int.Parse(o.ToString());
            }
            else
            {
                result = SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "sp_InsertUpdateSection", param);
                if (result == -1)
                    result = 1;
                else
                    result = 0;
            }

            return result;

        }


        public SectionManager SelectTable(string tblName, string whereCond)
        {

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@tblName", tblName);
            param[1] = new SqlParameter("@whereCond", whereCond);

            SqlDataReader Dr = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, "sp_GetTable", param);
            if (Dr.Read())
            {
                this.SecName = Dr["secname"].ToString();
                this.SecTeacherName = Dr["secTeacherName"].ToString();
                this.SecIsActive = bool.Parse(Dr["secIsActive"].ToString());
                return this;
            }
            return null;
        }
        #endregion
    }
}
