using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Configuration; 
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PDSystem
{
    
    class ConnectionManager
    {
        #region Class Level Objects
        SqlCommand scmd = new SqlCommand();
        SqlConnection scon = new SqlConnection();

        #endregion

        #region Class Constructor

        public ConnectionManager()
        {
            //<add name="PDSystem.Properties.Settings.PWRDBConn" connectionString="Data Source=MITPC\sqlexpress;Initial Catalog=SQLPWRDB;Persist Security Info=True;User ID=sqluser;Password=fps" providerName="System.Data.SqlClient"/>
            //SetDataObjects();
            conn = ConfigurationManager.ConnectionStrings["pwrdbconn"].ConnectionString; 
            scon.ConnectionString = conn;
            if (scon.State == ConnectionState.Closed)
            {
                scon.Open();
            }
            else
            {
                scon.Close();
            }
        }

        #endregion

        #region Public/Private Members
          public string conn;

        #endregion

        #region Public/Private Properties

        #endregion

        #region Helper Methods

            public SqlDataAdapter MyDataAdapter(string spstring, params SqlParameter[] commandParameters)
            {
                SqlDataAdapter da = new SqlDataAdapter(MyCommand(spstring, commandParameters));
                //scon.Close();
                return da;
            }

            public SqlCommand MyCommand(string sqlsp, params SqlParameter[] commandParameters)
            {
                try
                {
                    scmd.CommandType = CommandType.StoredProcedure;
                    scmd.CommandText = sqlsp;
                    scmd.Parameters.Clear();
                    scmd.Parameters.AddRange(commandParameters);
                    scmd.Connection = scon;
                    //scmd.CommandType = CommandType.Text;
                    //scmd.CommandText = @sqlsp;
                    //scmd.Connection = scon;
                    //scon.Close();
                    return scmd;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return scmd;
                }
                finally
                {
                   // scon.Close();
                }
            }

            public SqlDataReader MyReader(string sql)
            {
                SqlCommand scmd = MyCommand(sql);
                SqlDataReader srdr = scmd.ExecuteReader();
                srdr.Close();
                return srdr;
            }

            public SqlDataReader MyReader(string sqlsp, params SqlParameter[] commandParameters)
             {
                 //ExecuteNonQuery = Reutrn only 1 or 0;
                 //ExecuteScaler = Return IDentity field value
                 SqlDataReader srdr = null;
                try
                 {
                      
                     scmd.CommandType = CommandType.StoredProcedure;
                     scmd.CommandText = sqlsp;
                     scmd.Parameters.Clear();
                     scmd.Parameters.AddRange(commandParameters);
                     scmd.Connection = scon;
                     srdr = scmd.ExecuteReader();
                     return srdr;
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.ToString());
                     return srdr;
                 }
                 finally
                 {
                     //scon.Close();
                     //return srdr;
                 }
   
             }

            public bool MyInsertUpdate(string sqlsp, string token,params SqlParameter[] commandParameters)
            {
                SqlCommand scmd = new SqlCommand();
                scmd.CommandType = CommandType.StoredProcedure;
                scmd.CommandText = sqlsp;
                scmd.Parameters.AddRange(commandParameters);
                scmd.Connection = scon;
                Int32 result = 0;
                try
                {
                    if (token == "I")
                    {
                        result = (Int32)scmd.ExecuteScalar();
                        //result = (Int32)scmd.ExecuteNonQuery();
                    }
                    else
                    {
                        result = (Int32)scmd.ExecuteNonQuery();
                        //scmd.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally 
                {
                    scmd.Dispose();
                }
                // (int)newProdID;
                if (result != 0)
                {
                    return true;
                }
                else
                { 
                    return false;
                }

                
            }

            public bool DeleteRecord(params SqlParameter[] commandParameters)
            {
                SqlCommand delcmd = new SqlCommand();
                delcmd.CommandType = CommandType.StoredProcedure;
                delcmd.CommandText = "sp_DeleRecord";
                delcmd.Parameters.AddRange(commandParameters);
                delcmd.Connection = scon;
                int result = 0;
                try
                {
                  result = (Int32)scmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    delcmd.Dispose();
                }
                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public bool InsertUpdate(DataTable dt, string token,string sqlsp)
            {
                SqlCommand scmd = new SqlCommand();
                scmd.CommandType = CommandType.StoredProcedure;
                scmd.CommandText = sqlsp;

             //scmd.Parameters.AddWithValue("@dt", dt);

                

               scmd.Parameters.Add(new SqlParameter("@dt", dt));

                if (scon.State == ConnectionState.Closed)
                {
                    scon.Open();
                }
                else
                {
                    scon.Close();
                }

                scon.Open();

                scmd.Connection = scon;

                Int32 result = 0;
                try
                {

                    result = (Int32)scmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                finally
                {
                    scmd.Dispose();
                    scon.Close();
                }
                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }


        #endregion

        
    }
}