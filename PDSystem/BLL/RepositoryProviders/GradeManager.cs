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
    public class GradeManager
    {
        #region Class Level Objects

        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();

        #endregion

        #region Class Constructor
        public GradeManager()
         {
            conn.ConnectionString = SqlHelper.connectionstring;
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
         }
        #endregion

        #region Public/Private Members
      
        //Members of Brand Table
        private int _pkgrdID;
        private int _pkbrhID;
        private string _grdName;
        private string _grdDesc;
        private int _CreatedBy;
        private int _ModifiedBy;
        private bool _grdisActive;
        private string _Token;

        #endregion

        #region Public/Private Properties

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
        public string GrdName
        {
            get { return _grdName; }
            set { _grdName = value; }
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
        public bool GrdIsActive
        {
            get { return _grdisActive; }
            set { _grdisActive = value; }
        }
        public string GrdDesc
        {
            get { return _grdDesc; }
            set { _grdDesc = value; }
        }
        public string Token
        {
            get { return _Token; }
            set { _Token = value; }
        }

        #endregion

        #region Helper Methods

        public int IUGrade()
        {

            int result = 0;
            SqlParameter[] param = new SqlParameter[8];

            param[0] = new SqlParameter("@pkbrhID", this.PkbrhID);
            param[1] = new SqlParameter("@pkgrdID", this.PkgrdID);
            param[2] = new SqlParameter("@grdName", this.GrdName);
            param[3] = new SqlParameter("@grdDesc", this.GrdDesc);
            param[4] = new SqlParameter("@CreatedBy", this.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", this.ModifiedBy);
            param[6] = new SqlParameter("@grdIsActive", this.GrdIsActive);
            param[7] = new SqlParameter("@Token", this.Token);

            if (this.Token == "I")
            {
                object o = SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, "sp_InsertUpdateGrade", param);
                if (o != null)
                    result = int.Parse(o.ToString());
            }
            else
            {
                result = SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "sp_InsertUpdateGrade", param);
                if (result == -1)
                    result = 1;
                else
                    result = 0;
            }

            return result;

        }

        public GradeManager Selectdata(int pkbrhID,int pkgrdID )
        {
           
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@pkbrhID", pkbrhID);
            param[1] = new SqlParameter("@pkgrdID", pkgrdID);
            SqlDataReader Dr = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, "sp_selectgrades", param);
            if (Dr.Read())
            {
                this.GrdName = Dr["grdname"].ToString();
                this.GrdDesc = Dr["grdDesc"].ToString();
                this.GrdIsActive= bool.Parse(Dr["grdIsActive"].ToString());
                return this;
            }
            return null;
        }
        public GradeManager SelectTable(string brhName)
        {

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@tblName", brhName);
            
            SqlDataReader Dr = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, "sp_GetTable", param);
            if (Dr.Read())
            {
                this.GrdName = Dr["grdname"].ToString();
                this.GrdDesc = Dr["grdDesc"].ToString();
                this.GrdIsActive = bool.Parse(Dr["grdIsActive"].ToString());
                return this;
            }
            return null;
        }
        #endregion
    }
}
