using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections;

namespace PDSystem
{
    class PUser:MyFunctions 
    {
        #region Class level Objects


        #endregion

        #region Class Constructor

        #endregion

        #region Public/Private Members

        private int _usrid;
        private string _deptid;
        private string _usrname;
        private string _desig;
        private string _email;
        private bool _isadmin;
        private bool _resetlogin;
        private bool _blockuser;
        private bool _payright;

        #endregion

        #region Public/Private Properties

        public int Usrid
        {
            get { return _usrid; }
            set { _usrid = value; }
        }

        public string Usrname
        {
            get { return _usrname; }
            set { _usrname = value; }
        }

        public string Deptid
        {
            get { return _deptid; }
            set { _deptid = value; }
        }

        public string Desig
        {
            get { return _desig; }
            set { _desig = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public bool Isadmin
        {
            get { return _isadmin; }
            set { _isadmin = value; }
        }

        public bool Blockuser
        {
            get { return _blockuser; }
            set { _blockuser = value; }
        }

        public bool Resetlogin
        {
            get { return _resetlogin; }
            set { _resetlogin = value; }
        }

        public bool Payright
        {
            get { return _payright; }
            set { _payright = value; }
        }
        #endregion

        #region Helper Methods


        public bool UserRecord(ArrayList paralist, string token)
        {

            bool result;
            int totpara = paralist.Count;

            totpara++;
            
            SqlParameter[] param = new SqlParameter[totpara];
            param[0] = new SqlParameter("@usrname",   paralist[0]);
            param[1] = new SqlParameter("@deptid",    paralist[1]);
            param[2] = new SqlParameter("@desig",     paralist[2]);
            param[3] = new SqlParameter("@usremail",  paralist[3]);
            param[4] = new SqlParameter("@blockuser", paralist[4]);
            param[5] = new SqlParameter("@isadmin",   paralist[5]);
            param[6] = new SqlParameter("@firstlogin",paralist[6]);
            param[7] = new SqlParameter("@payrights", paralist[7]);
            param[8] = new SqlParameter("@pkusrid",   paralist[8]);
            param[9] = new SqlParameter("@token",     token);

            ConnectionManager obj = new ConnectionManager();
            if (token == "I")
            {
                result = obj.MyInsertUpdate("sp_InsertUpdateUser", "I",param);
            }
            else
            {
                result = obj.MyInsertUpdate("sp_InsertUpdateUser", "U",param);
            }

            return result;
        }

        #endregion

    }
}
