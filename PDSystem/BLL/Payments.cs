using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDSystem
{
    class Payments : MyFunctions
    {
        #region Class level Objects

        #endregion

        #region Class Constructor

        #endregion

        #region Public/Private Members

        private int _usrid;
        private int _estno;
        private string _usrname;
        private int _vendid;
        private int _payid;
        private DateTime _entdate;
        private string _desc;


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

        public DateTime Entdate
        {
            get { return _entdate; }
            set { _entdate = value; }
        }

        public int VendID
        {
            get { return _vendid; }
            set { _vendid = value; }
        }


        #endregion

        #region Helper Methods


        #endregion

    }

}
