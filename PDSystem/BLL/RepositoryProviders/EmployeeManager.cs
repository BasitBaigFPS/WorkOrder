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
    public class EmployeeManager
    {
        #region Class Level Objects

        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();

        #endregion

        #region Class Constructor
        public EmployeeManager()
         {
            conn.ConnectionString = SqlHelper.connectionstring;
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
         }
        #endregion

        #region Public/Private Members
      
        //Members of Brand Table

        private int _pkEmpID;
        private int _pkdesigID;
        private string _empName;
        private string _empAddress;
        private string _empEmail;
        private bool _empIsActive;
        private int _CreatedBy;
        private int _ModifiedBy;
        private string _Token;

        #endregion

        #region Public/Private Properties
        public int PkEmpID
        {
            get { return _pkEmpID; }
            set { _pkEmpID = value; }
        }
        public int PkdesigID
        {
            get { return _pkdesigID; }
            set { _pkdesigID = value; }
        }
        public string EmpName
        {
            get { return _empName; }
            set { _empName = value; }
        }
        public string EmpAddress
        {
            get { return _empAddress; }
            set { _empAddress = value; }
        }
        public string EmpEmail
        {
            get { return _empEmail; }
            set { _empEmail = value; }
        }
        public bool EmpIsActive
        {
            get { return _empIsActive; }
            set { _empIsActive = value; }
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

        public int IUEmployee()
        {

            int result = 0;
            SqlParameter[] param = new SqlParameter[9];

            param[0] = new SqlParameter("@pkEmpID", this.PkEmpID);
            param[1] = new SqlParameter("@pkdesigID", this.PkdesigID);
            param[2] = new SqlParameter("@empName", this.EmpName);
            param[3] = new SqlParameter("@empAddress", this.EmpAddress);
            param[4] = new SqlParameter("@empEmail", this.EmpEmail);
            param[5] = new SqlParameter("@empIsActive", this.EmpIsActive);
            param[6] = new SqlParameter("@CreatedBy", this.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", this.ModifiedBy);
            param[8] = new SqlParameter("@Token", this.Token);

            if (this.Token == "I")
            {
                object o = SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, "sp_InsertUpdateEmployee", param);
                if (o != null)
                    result = int.Parse(o.ToString());
            }
            else
            {
                result = SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "sp_InsertUpdateEmployee", param);
                if (result == -1)
                    result = 1;
                else
                    result = 0;
            }

            return result;

        }


        public EmployeeManager SelectTable(string tblName, string whereCond)
        {

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@tblName", tblName);
            param[1] = new SqlParameter("@whereCond", whereCond);

            SqlDataReader Dr = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, "sp_GetTable", param);
            if (Dr.Read())
            {
                this.EmpName = Dr["empname"].ToString();
                this.PkEmpID = int.Parse(Dr["pkempId"].ToString());
                this.EmpAddress = Dr["empAddress"].ToString();
                this.PkdesigID = int.Parse(Dr["pkdesigID"].ToString());
                this.EmpEmail = Dr["empEmail"].ToString();
                this.EmpIsActive = bool.Parse(Dr["empIsActive"].ToString());
                return this;
            }
            return null;
        }
        
        #endregion
    }
}
