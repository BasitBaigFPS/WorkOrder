using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace PDSystem
{
    class MyFunctions
    {
        #region Class level Objects
        ConnectionManager OBJConMgr = new ConnectionManager();

        #endregion

           //public MyFunctions()
           //{
               
           //}
        
        #region Helper Methods

            


        public BindingSource MyBindSource(string fldName, string tblName, BindingSource mybsource, string whereCond)
           {
               DataSet ds = new DataSet();
               try
               {
                   SqlParameter[] param = new SqlParameter[3];
                   param[0] = new SqlParameter("@tblName", tblName);
                   param[1] = new SqlParameter("@fldName", fldName);
                   param[2] = new SqlParameter("@whereCond", whereCond);
                   SqlDataAdapter da = OBJConMgr.MyDataAdapter("sp_GetTable", param);
                   da.Fill(ds);
                   mybsource.DataSource = ds.Tables[0];
                   int rowcount = mybsource.Count;
                   return mybsource;
               }
               catch (Exception ex)
               {
                   MessageBox.Show(ex.ToString());
                   return mybsource;
               }

               finally
               {
                  // ds.Clear(); 
               }

           }

        public DataGridView MyDataGrid(string fldName, string tblName, string StProc, DataGridView mygrid, string whereCond)
            {
                //DataSet ds = new DataSet();
                DataTable tbl = new DataTable(); 
                DataRow dr;
               try
                {
                    SqlParameter[] param = new SqlParameter[3];
                    param[0] = new SqlParameter("@tblName", tblName);
                    param[1] = new SqlParameter("@fldName", fldName);
                    param[2] = new SqlParameter("@whereCond", whereCond);
                    
                    SqlDataAdapter da = OBJConMgr.MyDataAdapter(StProc, param);
                    //da.Fill(ds); 
                    da.Fill(tbl);
                    //mygrid.DataSource = ds.Tables[0];
                    mygrid.DataSource = tbl;

                    //dr = ds.Tables[0].NewRow(); // Table[0] of ds is the data I want to display
                    dr = tbl.NewRow(); 
                    // add row to dataset now
                    //DataTable dt = new DataTable();
                    //dt = ds.Tables[0];


                    if (tblName == "EstMaster")
                    {
                        float cst = 0;
                        float pad = 0;
                        for (int i = 0; i < tbl.Rows.Count; i++)
                        {
                            cst = cst + float.Parse(tbl.Rows[i]["Cost"].ToString());
                            pad = pad + float.Parse(tbl.Rows[i]["Paid"].ToString());
                        }

                        dr[2] = "Total";
                        dr[3] = cst.ToString();
                        dr[4] = pad.ToString();
                    }

                    if (tblName == "EstDetail")
                    {
                        float gros = 0;
                        float pad = 0;
                        float rem = 0;
                        float rcd = 0;
                        for (int i = 0; i < tbl.Rows.Count; i++)
                        {
                            gros = gros + float.Parse(tbl.Rows[i]["GrossAmt"].ToString());
                            pad = pad + float.Parse(tbl.Rows[i]["PaidAmt"].ToString());
                            rem = rem + float.Parse(tbl.Rows[i]["BalAmt"].ToString());
                            rcd = rcd + float.Parse(tbl.Rows[i]["PayRecmd"].ToString());
                        }

                       // dr[1] = "Total";
                        dr[4] = gros.ToString();
                        dr[5] = pad.ToString();
                        dr[6] = rem.ToString();
                        dr[7] = rcd.ToString();

                    }

                    //ds.Tables[0].Rows.Add(dr);
                    tbl.Rows.Add(dr); 
                    return mygrid;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return mygrid;
                }
                finally 
                {
                   //ds.Clear();
                }
            }

        public ComboBox FillCombo(string fldName, string fldID, ComboBox mycombo, string tblName, string whereCond)
            {

                DataSet ds = new DataSet();
                try
                {
                    SqlParameter[] param = new SqlParameter[3];
                    param[0] = new SqlParameter("@tblName", tblName);
                    param[1] = new SqlParameter("@fldName", fldName);
                    param[2] = new SqlParameter("@whereCond", whereCond);

                    SqlDataAdapter da = OBJConMgr.MyDataAdapter("sp_GetTable", param);
                    da.Fill(ds);
                    mycombo.DataSource = ds.Tables[0];
                    
                    //Create a XML File Through DataSet
                    //ds.WriteXml("UserList.xml"); 

                    mycombo.DisplayMember = fldName;
                    mycombo.ValueMember = fldID;
                    return mycombo;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return mycombo;
                }
                finally
                {
                   // ds.Clear();
                }

                //string sql;
                //sql = "select " + fldnam + " from " + tblnam + " where blockuser=" + allrec + " order by " + fldnam;
                //SqlDataReader mydr;
                //mydr = OBJConMgr.MyReader(sql);  
                //while (mydr.Read())
                //{
                //    objcmb.Items.Add(mydr[0]);
                //}
            }

        public  DataGridViewComboBoxColumn combocolumn(string fldName, string fldID, string tblName, string whereCond)
        {
            DataSet ds = new DataSet();
            // Create a new Combo Box Column
            DataGridViewComboBoxColumn cboColName = new DataGridViewComboBoxColumn();
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@tblName", tblName);
                param[1] = new SqlParameter("@fldName", fldName);
                param[2] = new SqlParameter("@whereCond", whereCond);
                SqlDataAdapter da = OBJConMgr.MyDataAdapter("sp_GetTable", param);
                da.Fill(ds);
                cboColName.DataSource = ds.Tables[0];
                cboColName.DisplayMember = fldName;
                cboColName.ValueMember = fldID;
                return cboColName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return cboColName;
            }
            finally
            {
               // ds.Clear();
            }
        }
        
        public string DLookup(string fldName, string tblName, string whereCond)
        {
             string commandString;
            if (whereCond == "")
            {
                commandString = "Select " + fldName + " from " + tblName;
            }
            else
            {
                commandString = "Select " + fldName + " from " + tblName + " where " + whereCond;
            }
            
            
            SqlDataAdapter dataAdapter = new SqlDataAdapter(); 
            dataAdapter = new SqlDataAdapter(commandString, new SqlConnection(OBJConMgr.conn));
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            //DataTable t = dataSet.Tables[0];
            if (dataSet.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0].Rows[0][0].ToString();
            }
            //if (t.Rows.Count != 0)
            //{
            //    return t.Rows[0][fldName].ToString();
            //}
            //else
            //{
            //    return null;
            //}
            
            
            // SqlParameter[] param = new SqlParameter[3];
               // param[0] = new SqlParameter("@fldName", fldName);
               // param[1] = new SqlParameter("@tblName", tblName);
               // param[2] = new SqlParameter("@whereCond", whereCond);
            
               // SqlDataReader Dr = OBJConMgr.MyReader("sp_GetField", param);

               // if (Dr.Read())
               // {
               //     string fldvalue = Dr[0].ToString();
               //     Dr.Close(); 
               //     return fldvalue;
               // }
               // else
               // {
               //     Dr.Close(); 
               //     return null;
               // }
               //// Dr.Close(); 
        }

        public double MakeGridTotal(DataGridView GridName, int col)
        {
            double mytotal = 0;
            //MessageBox.Show(GridName.RowCount.ToString());
            try
            {
                for (int i = 0; i < GridName.RowCount; i++)
                {
                    if (Convert.IsDBNull(GridName.Rows[i].Cells[col].Value) == true)
                    {
                        //totcost += Convert.ToInt32(Convert.IsDBNull(DG_Master.Rows[i].Cells[2].Value));
                        mytotal += 0;
                    }
                    else
                    {
                        mytotal += Convert.ToDouble(GridName.Rows[i].Cells[col].Value);
                    }
                }
                return mytotal;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return mytotal;
            }
        }

        public void CreateFileFolder()
        {
                // Specify a "currently active folder"
                string activeDir = @"D:\";

                //Create a new subfolder under the current active folder
                string newPath = System.IO.Path.Combine(activeDir, "PWR");

                // Create a new file name. This example generates
                // a random string.
                //string newFileName = System.IO.Path.GetRandomFileName();

                // Combine the new file name with the path
                //newPath = System.IO.Path.Combine(newPath, newFileName);

                // Create the file and write to it.
                // DANGER: System.IO.File.Create will overwrite the file
                // if it already exists. This can occur even with
                // random file names.
                //if (!System.IO.File.Exists(newPath))
                if (!System.IO.Directory.Exists(newPath))
                {
                    // Create the subfolder
                    System.IO.Directory.CreateDirectory(newPath);
                    activeDir = @"D:\PWR";
                    newPath = System.IO.Path.Combine(activeDir, "PDF");
                    System.IO.Directory.CreateDirectory(newPath);
                }
                else 
                {
                    activeDir = @"D:\PWR";
                    newPath = System.IO.Path.Combine(activeDir, "PDF");
                    if (!System.IO.Directory.Exists(newPath))
                    {
                        System.IO.Directory.CreateDirectory(newPath);
                    }
                }
        }

        #endregion
    }

  }
