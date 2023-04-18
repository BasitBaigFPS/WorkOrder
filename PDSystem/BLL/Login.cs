using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
 
namespace PDSystem
{
    class Login:MyFunctions
    {
        #region Class level Objects

           static int _workuserid;
           static int _cuserid;
           static string _cuser;

           static int _cuserGP;
           static string _cuseremail;
        #endregion

        #region Class Constructor

        #endregion

        #region Public/Private Members

        #endregion

        #region Public/Private Properties

        public static int Workuserid
        {
            get { return Login._workuserid; }
            set { Login._workuserid = value; }
        }
        
        
        public static int Cuserid
        {
            get { return _cuserid; }
            set { _cuserid = value; }
        }


        public static string Cuser
        {
            get { return _cuser; }
            set { _cuser = value; }
        }

        public static int CuserGP
        {
            get { return Login._cuserGP; }
            set { Login._cuserGP = value; }
        }

        public static string CuserEmail
        {
            get { return Login._cuseremail; }
            set { Login._cuseremail = value; }
        }



        #endregion
        
        #region Helper Methods

        public bool CheckLogin(int PKID)
        {
            string newusr;
            //int myid = int.Parse(PKID); 
            newusr = DLookup("firstlogin", "puser", "usrID=" + PKID);
            bool nusr = bool.Parse(newusr);
            return nusr;  
        
        }

        public bool PostUserLogin(int pkid,bool newlogin, string newpass)
        {
            
            bool result;
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@pkusrID", pkid);
            param[1] = new SqlParameter("@firstlogin", newlogin);
            param[2] = new SqlParameter("@newpass", newpass);
            
            ConnectionManager obj = new ConnectionManager();
            result = obj.MyInsertUpdate("sp_PostLogin","U", param);

            //if (token == "I")
            //{
            //    result = obj.MyInsertUpdate("sp_InsertUpdateUser", "I",param);
            //}
            //else
            //{
            //    result = obj.MyInsertUpdate("sp_InsertUpdateUser", "U",param);
            //}

            return result;
        }

          
        public bool ValidatePassword(string PKID, string passw)
            {
                string vpw;
                string pwc = Makepassword(passw);
                int myid = int.Parse(PKID); 

                vpw = DLookup("password", "puser", "usrID=" + myid);

                if (vpw == pwc)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        public string Makepassword(string mypass)
            {
                string pwc;
                Byte[] bytes = ASCIIEncoding.ASCII.GetBytes(mypass.Trim());
                int k = 1;
                pwc = "";
                char c;
                int a;
                foreach (Byte b in bytes)
                {
                    a = b;
                    c = Convert.ToChar(a * 2 + k);
                    k++;
                    pwc = pwc + c;
                }
                return pwc;
            }
    
        #endregion
    
    }
}
