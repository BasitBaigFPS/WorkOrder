using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data; 
using System.Data.SqlClient;   
using System.Collections;

namespace PDSystem
{
    class ProjEntry:MyFunctions
    {
        #region Class level Objects
           static string _rptname;
           static int _pwrno;
           static int _pov;
           static double _wkcost;
           static int _workid;
           static int _payno;

        #endregion

        #region Class Constructor

        #endregion

        #region Public/Private Members

        private int _usrid;
        private int _estno;
        private int _wrkno;
        private string _estid;
        private string _usrname;
        private string _acdyear;
        private int _projtypeid;
        private string _sysid;
        private string _branchid;
        private string _entdate;
        private string _desc;
        private int _vendid;
        private int _payid;
        
        #endregion

        #region Public/Private Properties

        public static string Rptname
        {
            get { return _rptname; }
            set { _rptname = value; }
        }

        public static int PWRNumber
        {
            get { return _pwrno; }
            set { _pwrno = value; }
        }

        public static int POVendor
        {
            get { return _pov; }
            set { _pov = value; }
        }

        public static double WorkCost
        {
            get { return _wkcost; }
            set { _wkcost = value; }
        }

        public static int WorkID
        {
            get { return _workid; }
            set { _workid = value; }
        }

        public static int PayNo
        {
            get { return _payno; }
            set { _payno = value; }
        }

        public int Usrid
        {
            get { return _usrid; }
            set { _usrid = value; }
        }

        public int Wrkno
        {
            get { return _wrkno; }
            set { _wrkno = value; }
        }

        public string Estid
        {
            get { return _estid; }
            set { _estid = value; }
        }

        public string Usrname
        {
            get { return _usrname; }
            set { _usrname = value; }
        }

        public string Acdyear
        {
            get { return _acdyear; }
            set { _acdyear = value; }
        }

        public int Projtypeid
        {
            get { return _projtypeid; }
            set { _projtypeid = value; }
        }

        public int Estno
        {
            get { return _estno; }
            set { _estno = value; }
        }

        public string Desc
        {
            get { return _desc; }
            set { _desc = value; }
        }

        public string Sysid
        {
            get { return _sysid; }
            set { _sysid = value; }
        }

        public string Branchid
        {
            get { return _branchid; }
            set { _branchid = value; }
        }

        public string Entdate
        {
            get { return _entdate; }
            set { _entdate = value; }
        }

        public int VendID
        {
            get { return _vendid; }
            set { _vendid = value; }
        }

        public int PayID
        {
            get { return _payid; }
            set { _payid = value; }
        }

        
        
        
        #endregion

        #region Helper Methods

        public bool MainRecord(ArrayList paralist, string token)
        {

            bool result;
            int totpara = paralist.Count+1;

            //totpara++;

            //@usrID as int,
            //@estid as varchar(15),
            //@estno as int,
            //@brid as varchar(3),
            //@ptypid as int,
            //@sysid as varchar(3),   
            //@desc as varchar(255), 
            //@estdat as datetime,  
            //@acdyear as varchar(9)




            SqlParameter[] param = new SqlParameter[totpara];
            param[0] = new SqlParameter("@usrID", paralist[0]);
            param[1] = new SqlParameter("@estid", paralist[1]);
            param[2] = new SqlParameter("@estno", paralist[2]);
            param[3] = new SqlParameter("@brid", paralist[3]);
            param[4] = new SqlParameter("@ptypid", paralist[4]);
            param[5] = new SqlParameter("@sysid", paralist[5]);
            param[6] = new SqlParameter("@desc", paralist[6]);
            param[7] = new SqlParameter("@estdat", paralist[7]);
            param[8] = new SqlParameter("@acdyear", paralist[8]);
            param[9] = new SqlParameter("@token", token);

            //param[9] = new SqlParameter("@newido", SqlDbType.Int);
            //param[9].Direction = ParameterDirection.Output;

            ConnectionManager obj = new ConnectionManager();
            result = obj.MyInsertUpdate("sp_InsertMain", token, param);
            //int finalid = (int)param[9].Value;
            //Estno = finalid; 
            return result;
        }

        public bool MasterRecord(ArrayList paralist, string token)
        {

            bool result;
            int totpara = paralist.Count;

            //totpara++;

            //@estno as int,
            //@wrkno as int,
            //@wrkdesc as varchar(255),
            //@cost as money

            SqlParameter[] param = new SqlParameter[totpara];
            param[0] = new SqlParameter("@estno", paralist[0]);
            param[1] = new SqlParameter("@wrkno", paralist[1]);
            param[2] = new SqlParameter("@wrkdesc", paralist[2]);
            param[3] = new SqlParameter("@rate", paralist[3]);
            param[4] = new SqlParameter("@quantity", paralist[4]);
            param[5] = new SqlParameter("@cost", paralist[5]);
            param[6] = new SqlParameter("@estdate", paralist[6]);
            

            ConnectionManager obj = new ConnectionManager();
            result = obj.MyInsertUpdate("sp_InsertMasterNew", token, param);
           // result = obj.MyInsertUpdate("sp_InsertMaster", token, param);
            return result;
        }

        public bool DetailRecord(ArrayList paralist, string token)
        {

            bool result;
            int totpara = paralist.Count;

            //@estno as int,
            //@vdrid as int,
            //@wrkno as int,
            //@payid as int,
            //@grsamt as money,
            //@dscamt as money,
            //@pyrcmd as money,
            //@duedate as smalldatetime,
            //@billinv as varchar(25),
            //@paydetail as varchar(255),
            //@newtran as bit

            //fieldlist.Add(PEObj.Estno);
            //fieldlist.Add(vdrid);
            //fieldlist.Add(PEObj.Wrkno);
            //fieldlist.Add(payid);
            //fieldlist.Add(gross);
            //fieldlist.Add(disc);
            //fieldlist.Add(payrc);
            //fieldlist.Add(dudat);
            //fieldlist.Add(billinvo);
            //fieldlist.Add(paydetl);
            //fieldlist.Add("true");



            SqlParameter[] param = new SqlParameter[totpara];
            param[0] = new SqlParameter("@estno", paralist[0]);
            param[1] = new SqlParameter("@vdrid", paralist[1]);
            param[2] = new SqlParameter("@wrkno", paralist[2]);
            param[3] = new SqlParameter("@payid", paralist[3]);
            param[4] = new SqlParameter("@grsamt", paralist[4]);
            param[5] = new SqlParameter("@dscamt", paralist[5]);
            param[6] = new SqlParameter("@pyrcmd", paralist[6]);
            param[7] = new SqlParameter("@duedate", paralist[7]);
            param[8] = new SqlParameter("@billinv", paralist[8]);
            param[9] = new SqlParameter("@paydetail", paralist[9]);
            param[10] = new SqlParameter("@newtran", paralist[10]);

            ConnectionManager obj = new ConnectionManager();
            result = obj.MyInsertUpdate("sp_InsertDetail", "I", param);
            return result;
        }

        public bool PaymentRecord(ArrayList paralist, string token)
        {

            bool result;
            int totpara = paralist.Count;

           // totpara++;

            SqlParameter[] param = new SqlParameter[totpara];
            param[0] = new SqlParameter("@estno", paralist[0]);
            param[1] = new SqlParameter("@wrkno", paralist[1]);
            param[2] = new SqlParameter("@payid", paralist[2]);
            param[3] = new SqlParameter("@vdrid", paralist[3]);
            param[4] = new SqlParameter("@pyrcmd", paralist[4]);
            param[5] = new SqlParameter("@duedate", paralist[5]);
            param[6] = new SqlParameter("@paydetail", paralist[6]);
            param[7] = new SqlParameter("@userid", paralist[7]);
            param[8] = new SqlParameter("@token", paralist[8]);

            ConnectionManager obj = new ConnectionManager();
            result = obj.MyInsertUpdate("sp_InsertPayments", token, param);
            return result;
        }

        public bool PostPaymentRecord(ArrayList paralist, string token)
        {

            bool result;
            int totpara = paralist.Count;

            // totpara++;

            SqlParameter[] param = new SqlParameter[totpara];
            param[0] = new SqlParameter("@estno", paralist[0]);
            param[1] = new SqlParameter("@wrkno", paralist[1]);
            param[2] = new SqlParameter("@payid", paralist[2]);
            param[3] = new SqlParameter("@vdrid", paralist[3]);
            param[4] = new SqlParameter("@pyrcmd", paralist[4]);
            param[5] = new SqlParameter("@duedate", paralist[5]);
            param[6] = new SqlParameter("@paydetail", paralist[6]);
            param[7] = new SqlParameter("@userid", paralist[7]);
            param[8] = new SqlParameter("@token", paralist[8]);

            ConnectionManager obj = new ConnectionManager();
            result = obj.MyInsertUpdate("sp_UpdatePayments", token, param);

            return result;
        }

        public bool VendorRecord(ArrayList paralist, string token)
        {

            bool result;
            int totpara = paralist.Count;

            totpara++;

            SqlParameter[] param = new SqlParameter[totpara];
            
            param[0] = new SqlParameter("@vdrname", paralist[0]);
            param[1] = new SqlParameter("@contperson", paralist[1]);
            param[2] = new SqlParameter("@pkvdrID", paralist[2]);
            param[3] = new SqlParameter("@token", token);

            ConnectionManager obj = new ConnectionManager();
            if (token == "I")
            {
                result = obj.MyInsertUpdate("sp_InsertUpdateVendor", "I", param);
            }
            else
            {
                result = obj.MyInsertUpdate("sp_InsertUpdateVendor", "U", param);
            }

            return result;
        }


        #endregion

    }
}
