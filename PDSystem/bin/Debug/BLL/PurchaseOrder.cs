using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace PDSystem
{
    class PurchaseOrder:MyFunctions 
    {
        #region Class level Objects

            static string _DetailSpec;
            static string _itemName;
            static int _itemid2;
            static int _pono2;

        #endregion

        #region Class Constructor

        #endregion

        #region Public/Private Members
        //Ctrl R + E to Make Properties
        //-----------------------------
        private int _usrid;
        private int _pono;
        private int _wrkno;
        private int _estno;
        private string _worktitle;
        private string _dptid;
        private string _usrname;
        private int _vendid;
        private int _payid;
        private string _terms ;
        private DateTime _podate;
        private DateTime _dlvdate;
        private string _contperson;
        private double _discount;
        private double _tax;
        
        //private Nullable _quotid;
        private string  _quotid;
        private DateTime _quotdate;
        private string _shipat;
        //-------------------------------
        private int _entno;

        private int _itmid;
        private string _itmspec;
        private string _unit;
        private double _rate;
        private float _qty;

        


        #endregion

        #region Public/Private Properties

        public static string DetailSpec
        {
            get { return _DetailSpec; }
            set { _DetailSpec = value; }
        }

        public static int Pono2
        {
            get { return _pono2; }
            set { _pono2 = value; }
        }

        public static string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        }

        public static int Itemid2
        {
            get { return _itemid2; }
            set { _itemid2 = value; }
        }

        public int Usrid
        {
            get { return _usrid; }
            set { _usrid = value; }
        }

        public string DptID
        {
            get { return _dptid; }
            set { _dptid = value; }
        }


        public string Usrname
        {
            get { return _usrname; }
            set { _usrname = value; }
        }
        
        public int POno
        {
            get { return _pono; }
            set { _pono = value; }
        }

        public int Estno
        {
            get { return _estno; }
            set { _estno = value; }
        }

        public string Worktitle
        {
            get { return _worktitle; }
            set { _worktitle = value; }
        }


        public int Wrkno
        {
            get { return _wrkno; }
            set { _wrkno = value; }
        }

        public int Payid
        {
            get { return _payid; }
            set { _payid = value; }
        }

        public string ContPerson
        {
            get { return _contperson; }
            set { _contperson = value; }
        }

        public DateTime POdate
        {
            get { return _podate; }
            set { _podate = value; }
        }

        public DateTime Dlvdate
        {
            get { return _dlvdate; }
            set { _dlvdate = value; }
        }

        public int VendID
        {
            get { return _vendid; }
            set { _vendid = value; }
        }

        public string TermsCond
        {
            get { return _terms; }
            set { _terms = value; }
        }


        public double Discount
        {
            get { return _discount; }
            set { _discount = value; }
        }

        public double Tax
        {
            get { return _tax; }
            set { _tax = value; }
        }

        public string Quotid
        {
            get { return _quotid; }
            set { _quotid = value; }
        }

        public DateTime  Quotdate
        {
            get { return _quotdate; }
            set { _quotdate = value; }
        }

        public string Shipat
        {
            get { return _shipat; }
            set { _shipat = value; }
        }
        //-------------------------------------------------------

        public int Entno
        {
            get { return _entno; }
            set { _entno = value; }
        }

        public int Itmid
        {
            get { return _itmid; }
            set { _itmid = value; }
        }

        public string Itmspec
        {
            get { return _itmspec; }
            set { _itmspec = value; }
        }

        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        public double Rate
        {
            get { return _rate; }
            set { _rate = value; }
        }

        public float Qty
        {
            get { return _qty; }
            set { _qty = value; }
        }

        #endregion

        #region Helper Methods

        public bool ItemRecord(ArrayList paralist, string token)
        {

            bool result;
            int totpara = paralist.Count;

            totpara++;

            SqlParameter[] param = new SqlParameter[totpara];

            param[0] = new SqlParameter("@itmname", paralist[0]);
            param[1] = new SqlParameter("@pkitmID", paralist[2]);
            param[2] = new SqlParameter("@token", token);

            ConnectionManager obj = new ConnectionManager();
            if (token == "I")
            {
                result = obj.MyInsertUpdate("sp_InsertUpdateItem", "I", param);
            }
            else
            {
                result = obj.MyInsertUpdate("sp_InsertUpdateItem", "U", param);
            }

            return result;
        }

        public bool MainRecord(ArrayList paralist, string token)
        {

            bool result;
            int totpara = paralist.Count;

            //totpara++;
            //@isaprd as bit, //@iscancl as bit, //@islock as bit,

            //@estno as int,
            //@pono as int,
            //@wrkno as int,   
            //@payid as int,
            //@vdrid as int,
            //@dptid as int,
            //@podate as datetime,
            //@terms as varchar(255), 
            //@cperson as varchar(50),
            //@disco as money,
            //@tax as money,
            //@quotid as varchar(50),  
            //@quotdate as datetime,
            //@shipat as varchar(100),
            //@usrID as int

            SqlParameter[] param = new SqlParameter[totpara];
            param[0] = new SqlParameter("@estno", paralist[0]);
            param[1] = new SqlParameter("@pono", paralist[1]);
            param[2] = new SqlParameter("@wrkno", paralist[2]);
            param[3] = new SqlParameter("@payid", paralist[3]);
            param[4] = new SqlParameter("@vdrid", paralist[4]);
            param[5] = new SqlParameter("@dptid", paralist[5]);
            param[6] = new SqlParameter("@podate", paralist[6]);
            param[7] = new SqlParameter("@terms", paralist[7]);
            param[8] = new SqlParameter("@cperson", paralist[8]);
            param[9] = new SqlParameter("@disco", paralist[9]);
            param[10] = new SqlParameter("@tax", paralist[10]);
            param[11] = new SqlParameter("@quotid", paralist[11]);
            param[12] = new SqlParameter("@quotdate", paralist[12]);
            param[13] = new SqlParameter("@shipat", paralist[13]);
            param[14] = new SqlParameter("@dlvdate", paralist[14]);
            param[15] = new SqlParameter("@usrID", paralist[15]);
            param[16] = new SqlParameter("@token", paralist[16]);

            //param[9] = new SqlParameter("@newido", SqlDbType.Int);
            //param[9].Direction = ParameterDirection.Output;

            ConnectionManager obj = new ConnectionManager();

            result = obj.MyInsertUpdate("sp_InsertPOMain", token, param);

            //int finalid = (int)param[9].Value;
            //Estno = finalid; 
            return result;
        }

        public bool DetailRecord(ArrayList paralist, string token)
        {

            bool result;
            int totpara = paralist.Count;

            //totpara++;

            //@pono as int,
            //@entno as int,
            //@itmid as int, 
            //@itmspec as varchar(100),
            //@unit as varchar(10),
            //@qty as float,
            //@rate as money

            SqlParameter[] param = new SqlParameter[totpara];
            param[0] = new SqlParameter("@pono", paralist[0]);
            param[1] = new SqlParameter("@entno", paralist[1]);
            param[2] = new SqlParameter("@itmid", paralist[2]);
            param[3] = new SqlParameter("@itmspec", paralist[3]);
            param[4] = new SqlParameter("@unit", paralist[4]);
            param[5] = new SqlParameter("@qty", paralist[5]);
            param[6] = new SqlParameter("@rate", paralist[6]);

            ConnectionManager obj = new ConnectionManager();
            result = obj.MyInsertUpdate("sp_InsertPODetail", "I", param);
            return result;
        }


        public bool DetailRecord(DataTable dt, string token)
        {

            bool result;
           









            ConnectionManager obj = new ConnectionManager();

            result = obj.InsertUpdate(dt, "I", "sp_InsertPODetailFull");
            return result;
        }



        //
        public bool InsertExcelRecord(ArrayList paralist, string token)
        {

            bool result;
            int totpara = paralist.Count;
            SqlParameter[] param = new SqlParameter[totpara];
            param[0] = new SqlParameter("@pono", paralist[0]);
            param[1] = new SqlParameter("@entno", paralist[1]);
            param[2] = new SqlParameter("@itmid", paralist[2]);
            param[3] = new SqlParameter("@itmspec", paralist[3]);
            param[4] = new SqlParameter("@unit", paralist[4]);
            param[5] = new SqlParameter("@qty", paralist[5]);
            param[6] = new SqlParameter("@rate", paralist[6]);

            ConnectionManager obj = new ConnectionManager();
            result = obj.MyInsertUpdate("sp_InsertPOData", "I", param);
            return result;
        }


        public void GetExcelRecord(int pono)
        {

            bool result;

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@pono", pono);

            ConnectionManager obj = new ConnectionManager();
            result = obj.MyInsertUpdate("sp_GetExcelData", "U", param);
        }
        #endregion
    }
}
