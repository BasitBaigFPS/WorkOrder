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
    public class BranchManager
    {
        #region Class Level Objects

        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();

        #endregion

        #region Class Level Objects
        public BranchManager()
         {
            conn.ConnectionString = SqlHelper.connectionstring;
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
         }
        #endregion

        #region Public/Private Members

        //Members of Brand Table
        
        private int _pkbrhID;
        private string _brhName;
        private string _brhAddress;
        private int  _brhCity;
        private string _brhCountry;
        private string _brhPhone;
        private string _brhFax;
        private string _brhEmail;
        private string _brhURL;
        private string _brhVP;
        private string _brhPrincipal;
        private bool _brhIsActive;
        private int _CreatedBy;
        private int _ModifiedBy;
        private string _Token;

        #endregion

        #region Public/Private Properties

        
        public int PkbrhID
        {
            get { return _pkbrhID; }
            set { _pkbrhID = value; }
        }
        public string BrhName
        {
            get { return _brhName; }
            set { _brhName = value; }
        }
        public string BrhAddress
        {
            get { return _brhAddress; }
            set { _brhAddress = value; }
        }
        public int  BrhCity
        {
            get { return _brhCity; }
            set { _brhCity = value; }
        }
        public string BrhCountry
        {
            get { return _brhCountry; }
            set { _brhCountry = value; }
        }
        public string BrhPhone
        {
            get { return _brhPhone; }
            set { _brhPhone = value; }
        }
        public string BrhFax
        {
            get { return _brhFax; }
            set { _brhFax = value; }
        }
        public string BrhEmail
        {
            get { return _brhEmail; }
            set { _brhEmail = value; }
        }
        public string BrhURL
        {
            get { return _brhURL; }
            set { _brhURL = value; }
        }
        public string BrhVP
        {
            get { return _brhVP; }
            set { _brhVP = value; }
        }
        public string BrhPrincipal
        {
            get { return _brhPrincipal; }
            set { _brhPrincipal = value; }
        }
        public bool BrhIsActive
        {
            get { return _brhIsActive; }
            set { _brhIsActive = value; }
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

        public int IUBranch()
        {

            int result = 0;
            SqlParameter[] param = new SqlParameter[15];

            param[0] = new SqlParameter("@pkbrhID", this.PkbrhID);
            param[1] = new SqlParameter("@brhName", this.BrhName);
            param[2] = new SqlParameter("@brhAddress", this.BrhAddress);
            param[3] = new SqlParameter("@brhCity", this.BrhCity);
            param[4] = new SqlParameter("@brhCountry", this.BrhCountry);
            param[5] = new SqlParameter("@brhPhone", this.BrhPhone);
            param[6] = new SqlParameter("@brhFax", this.BrhFax);
            param[7] = new SqlParameter("@brhEmail", this.BrhEmail);
            param[8] = new SqlParameter("@brhURL", this.BrhURL);
            param[9] = new SqlParameter("@brhVP", this.BrhVP);
            param[10] = new SqlParameter("@brhPrincipal", this.BrhPrincipal);
            param[11] = new SqlParameter("@brhIsActive", this.BrhIsActive);
            param[12] = new SqlParameter("@CreatedBy", this.CreatedBy);
            param[13] = new SqlParameter("@ModifiedBy", this.ModifiedBy);
            param[14] = new SqlParameter("@Token", this.Token);

            if (this.Token == "I")
            {
                object o = SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, "sp_InsertUpdateBranches", param);
                if (o != null)
                    result = int.Parse(o.ToString());
            }
            else
            {
                result = SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "sp_InsertUpdateBranches", param);
                if (result == -1)
                    result = 1;
                else
                    result = 0;
            }

            return result;

        }

        
        public BranchManager SelectTable(string tblName,string whereCond)
        {

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@tblName", tblName);
            param[1] = new SqlParameter("@whereCond", whereCond);

            SqlDataReader Dr = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, "sp_GetTable", param);
            if (Dr.Read())
            {
                this.BrhName = Dr["brhname"].ToString();
                this.BrhAddress = Dr["brhAddress"].ToString();
                this.BrhCity = int.Parse(Dr["brhCity"].ToString());
                this.BrhCountry = Dr["brhCountry"].ToString();
                this.BrhPhone = Dr["brhPhone"].ToString();
                this.BrhFax = Dr["brhFax"].ToString();
                this.BrhEmail = Dr["brhEmail"].ToString();
                this.BrhURL = Dr["brhURL"].ToString();
                this.BrhVP = Dr["brhVP"].ToString();
                this.BrhPrincipal = Dr["brhPrincipal"].ToString();
                this.BrhIsActive = bool.Parse(Dr["brhIsActive"].ToString());
                return this;
            }
            return null;
        }
        #endregion
    }
}
