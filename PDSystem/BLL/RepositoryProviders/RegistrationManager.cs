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
    public class RegistrationManager
    {
        #region Class Constructor

        public RegistrationManager()
         {
            conn.ConnectionString = SqlHelper.connectionstring;
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
         }

        #endregion

        #region Class Level Objects

        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();

        #endregion

        #region Public/Private Members
      
        //Members of Brand Table

        private int _pkregID;
        private int _fkbrhID;
        private int _fkgrdID;
        private int _CreatedBy;
        private int _ModifiedBy;
        private string _regCode;
        private DateTime _regDateTime;
        private string _regFirstName;
        private string _regLastName;
        private string _regGrade;
        private string _regGender;
        private string _regNationality;
        private DateTime _regDOB;
        private string _regPOB;
        private string _regAddress;
        private string _regState;
        private string _regZip;
        private string _regCity;
        private string _regCountry;
        private string _regPhone;
        private string _regCell;
        private string _regEmail;
        private string _regPWD;
        private string _regFatherFirstName;
        private string _regFatherLastName;
        private string _regFatherCompany;
        private string _regFatherDesignation;
        private string _regFatherBusinessType;
        private string _regFatherContact;
        private string _regFatherEmail;
        private string _regMotherFirtName;
        private string _regMotherLastName;
        private string _regMotherComapny;
        private string _regMotherDesignation;
        private string _regMotherBusinessType;
        private string _regMotherContact;
        private string _regMotherEmail;
        private bool _regParentsInKarachi;
        private bool _regParentDivorced;
        private bool _regIsOrphan;
        private string _regLastSchool;
        private string _regType;
        private string _regImage;
        private string _Token;

    
        #endregion

        #region Public/Private Properties

        public int PkregID
        {
            get { return _pkregID; }
            set { _pkregID = value; }
        }
        public int FkbrhID
        {
            get { return _fkbrhID; }
            set { _fkbrhID = value; }
        }
        public int FkgrdID
        {
            get { return _fkgrdID; }
            set { _fkgrdID = value; }
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
        public string RegCode
        {
            get { return _regCode; }
            set { _regCode = value; }
        }
        public DateTime RegDateTime
        {
            get { return _regDateTime; }
            set { _regDateTime = value; }
        }
        public string RegFirstName
        {
            get { return _regFirstName; }
            set { _regFirstName = value; }
        }
        public string RegLastName
        {
            get { return _regLastName; }
            set { _regLastName = value; }
        }
        public string RegGrade
        {
            get { return _regGrade; }
            set { _regGrade = value; }
        }
        public string RegGender
        {
            get { return _regGender; }
            set { _regGender = value; }
        }
        public string RegNationality
        {
            get { return _regNationality; }
            set { _regNationality = value; }
        }
        public DateTime RegDOB
        {
            get { return _regDOB; }
            set { _regDOB = value; }
        }
        public string RegPOB
        {
            get { return _regPOB; }
            set { _regPOB = value; }
        }
        public string RegAddress
        {
            get { return _regAddress; }
            set { _regAddress = value; }
        }
        public string RegState
        {
            get { return _regState; }
            set { _regState = value; }
        }
        public string RegZip
        {
            get { return _regZip; }
            set { _regZip = value; }
        }
        public string RegCity
        {
            get { return _regCity; }
            set { _regCity = value; }
        }
        public string RegCountry
        {
            get { return _regCountry; }
            set { _regCountry = value; }
        }
        public string RegPhone
        {
            get { return _regPhone; }
            set { _regPhone = value; }
        }
        public string RegCell
        {
            get { return _regCell; }
            set { _regCell = value; }
        }
        public string RegEmail
        {
            get { return _regEmail; }
            set { _regEmail = value; }
        }
        public string RegPWD
        {
            get { return _regPWD; }
            set { _regPWD = value; }
        }
        public string RegFatherFirstName
        {
            get { return _regFatherFirstName; }
            set { _regFatherFirstName = value; }
        }
        public string RegFatherLastName
        {
            get { return _regFatherLastName; }
            set { _regFatherLastName = value; }
        }
        public string RegFatherCompany
        {
            get { return _regFatherCompany; }
            set { _regFatherCompany = value; }
        }
        public string RegFatherDesignation
        {
            get { return _regFatherDesignation; }
            set { _regFatherDesignation = value; }
        }
        public string RegFatherBusinessType
        {
            get { return _regFatherBusinessType; }
            set { _regFatherBusinessType = value; }
        }
        public string RegFatherContact
        {
            get { return _regFatherContact; }
            set { _regFatherContact = value; }
        }
        public string RegFatherEmail
        {
            get { return _regFatherEmail; }
            set { _regFatherEmail = value; }
        }
        public string RegMotherFirtName
        {
            get { return _regMotherFirtName; }
            set { _regMotherFirtName = value; }
        }
        public string RegMotherLastName
        {
            get { return _regMotherLastName; }
            set { _regMotherLastName = value; }
        }
        public string RegMotherComapny
        {
            get { return _regMotherComapny; }
            set { _regMotherComapny = value; }
        }
        public string RegMotherDesignation
        {
            get { return _regMotherDesignation; }
            set { _regMotherDesignation = value; }
        }
        public string RegMotherBusinessType
        {
            get { return _regMotherBusinessType; }
            set { _regMotherBusinessType = value; }
        }
        public string RegMotherContact
        {
            get { return _regMotherContact; }
            set { _regMotherContact = value; }
        }
        public string RegMotherEmail
        {
            get { return _regMotherEmail; }
            set { _regMotherEmail = value; }
        }
        public bool RegParentsInKarachi
        {
            get { return _regParentsInKarachi; }
            set { _regParentsInKarachi = value; }
        }
        public bool RegParentDivorced
        {
            get { return _regParentDivorced; }
            set { _regParentDivorced = value; }
        }
        public bool RegIsOrphan
        {
            get { return _regIsOrphan; }
            set { _regIsOrphan = value; }
        }
        public string RegLastSchool
        {
            get { return _regLastSchool; }
            set { _regLastSchool = value; }
        }
        public string RegType
        {
            get { return _regType; }
            set { _regType = value; }
        }
        public string RegImage
        {
            get { return _regImage; }
            set { _regImage = value; }
        }
        public string Token
        {
            get { return _Token; }
            set { _Token = value; }
        }

        #endregion

        #region Helper Methods

        public int InsertReg()
        {

            int result = 0;
            SqlParameter[] param = new SqlParameter[44];
            param[0] = new SqlParameter("@pkregID", this.PkregID);
            param[1] = new SqlParameter("@fkbrhID", this.FkbrhID);
            param[2] = new SqlParameter("@fkgrdID", this.FkgrdID);
            param[3] = new SqlParameter("@CreatedBy", this.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", this.ModifiedBy);
	        param[5] = new SqlParameter("@regCode", this.RegCode);
	        param[6] = new SqlParameter("@regDateTime", this.RegDateTime);
	        param[7] = new SqlParameter("@regFirstName", this.RegFirstName);
	        param[8] = new SqlParameter("@regLastName", this.RegLastName);
	        param[9] = new SqlParameter("@regGrade", this.RegGrade);
	        param[10] = new SqlParameter("@regGender", this.RegGender);
	        param[11] = new SqlParameter("@regNationality", this.RegNationality);
	        param[12] = new SqlParameter("@regDOB", this.RegDOB);
	        param[13] = new SqlParameter("@regPOB", this.RegPOB);
	        param[14] = new SqlParameter("@regAddress", this.RegAddress);
	        param[15] = new SqlParameter("@regState", this.RegState);
	        param[16] = new SqlParameter("@regZip", this.RegZip);
	        param[17] = new SqlParameter("@regCity", this.RegCity);
	        param[18] = new SqlParameter("@regCountry", this.RegCountry);
	        param[19] = new SqlParameter("@regPhone", this.RegPhone);
	        param[20] = new SqlParameter("@regCell", this.RegCell);
	        param[21] = new SqlParameter("@regEmail", this.RegEmail);
	        param[22] = new SqlParameter("@regPWD", this.RegPWD);
	        param[23] = new SqlParameter("@regFatherFirstName", this.RegFatherFirstName);
	        param[24] = new SqlParameter("@regFatherLastName", this.RegFatherLastName);
	        param[25] = new SqlParameter("@regFatherCompany", this.RegFatherCompany);
	        param[26] = new SqlParameter("@regFatherDesignation", this.RegFatherDesignation);
	        param[27] = new SqlParameter("@regFatherBusinessType", this.RegFatherBusinessType);
	        param[28] = new SqlParameter("@regFatherContact", this.RegFatherContact);
	        param[29] = new SqlParameter("@regFatherEmail", this.RegFatherEmail);
	        param[30] = new SqlParameter("@regMotherFirtName", this.RegMotherFirtName);
	        param[31] = new SqlParameter("@regMotherLastName", this.RegMotherLastName);
	        param[32] = new SqlParameter("@regMotherComapny", this.RegMotherComapny);
	        param[33] = new SqlParameter("@regMotherDesignation", this.RegMotherDesignation);
	        param[34] = new SqlParameter("@regMotherBusinessType", this.RegMotherBusinessType);
	        param[35] = new SqlParameter("@regMotherContact", this.RegMotherContact);
	        param[36] = new SqlParameter("@regMotherEmail", this.RegMotherEmail);
	        param[37] = new SqlParameter("@regParentsInKarachi", this.RegParentsInKarachi);
	        param[38] = new SqlParameter("@regParentDivorced", this.RegParentDivorced);
	        param[39] = new SqlParameter("@regIsOrphan", this.RegIsOrphan);
	        param[40] = new SqlParameter("@regLastSchool", this.RegLastSchool);
	        param[41] = new SqlParameter("@regType", this.RegType);
	        param[42] = new SqlParameter("@regImage", this.RegImage);
	        param[43] = new SqlParameter("@Action", this.Token);

            if (this.Token == "ADD")
            {
                object o = SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, "sp_InsertUpdateRegistration", param);
                if (o != null)
                    result = int.Parse(o.ToString());
            }
            else
            {
                result = SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "sp_InsertUpdateRegistration", param);
                if (result == -1)
                    result = 1;
                else
                    result = 0;
            }

            return result;

        }

        public int UpdateReg()
        {
            int result = 0;

            //SqlParameter[] param = new SqlParameter[4];
            //param[0] = new SqlParameter("@TableOrView", "DS_tbl_Announcements");
            //param[1] = new SqlParameter("@Columns", "Active ='False'");
            //param[2] = new SqlParameter("@WhereClause", "ID=" + this.PkregID);
            //param[3] = new SqlParameter("@SQL", "");

            //result = SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "sp_Update", param);
            
            if (result == -1)
                 return 1;
            else
                return 0;
           
        }

        #endregion
    }
}
