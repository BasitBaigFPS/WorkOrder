using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using Telerik.Reporting.Processing;
using Telerik.WinControls;
using Telerik.Reporting;
using Telerik.Reporting.Drawing;
using Telerik.ReportViewer.WinForms;
using Telerik.Reporting.Xml;
//using System.ComponentModel;

 


namespace PDSystem
{
    public partial class frm_EntryMain : Form
    {
        
        MyFunctions OBJFuncLib = new MyFunctions();
        ProjEntry PEObj = new ProjEntry();

        private Button addNewRowButton = new Button();
        private Button deleteRowButton = new Button();

        private SqlConnection connection;
        private SqlCommand commandMain;
        private SqlCommand commandMast;
        private SqlCommand commandDetail;
        private SqlCommand command;

        private SqlDataAdapter adapterMain;
        private SqlDataAdapter adapterMast;
        private SqlDataAdapter adapterDetail;

        private SqlCommandBuilder builderMain;
        private SqlCommandBuilder builderMast;
        private SqlCommandBuilder builderDetail;

        private BindingSource EstMastBS = null;
        private BindingSource EstDetailBS = null;
        
        private string sqlmast = null;
        private string sqldetail = null;
        private string sqlMastPara = null;

        private DataSet ds = new DataSet(); 

        //private DataSet tempDataSet;
        private DataTable EstMain;
        private DataTable EstMaster;
        private DataTable EstDetail;

        private DataRow row;
        
        private MyDomain SystemID;
        private int Domainid;
        private MyDomain type;

        private bool newproj;
        private bool saveonce;
        private bool workentry1;
        private bool payentry1;
        private bool editstart;
        private bool fDeleteRow;
        private bool justclick;
        private bool revertprocess;

        static bool mailSent = false;

        private int newwork = 0;
        private int payid = 0;
        private int brid = 0;
        private int cmbid = 0;
        double workgrossamt;
        private bool newvendor;
        private string vndrname;
        

        public frm_EntryMain()
        {
            InitializeComponent();
            // Set up the BindingSource component.
            this.BindNav.BindingSource = this.BindSource;
            this.BindNav.Dock = DockStyle.Top;
               
            this.Controls.Add(this.BindNav);
            // Set up the form.
            //this.Size = new Size(800, 200);
            //this.Load += new EventHandler(Form1_Load);

        }

        public enum MyDomain
        {
            FPS,
            HSS,
            UTP
        }

        private void frm_EntryMain_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'SQLPWRDBDataSet.View_ProjEstimate' table. You can move, or remove it, as needed.
            //this.View_ProjEstimateTableAdapter.Fill(this.SQLPWRDBDataSet.View_ProjEstimate);
            newproj = false;
            workentry1 = false;
            newvendor = false;

          

            SetForm();
            //this.txtDesc.Focus();
            this.txtFindPwr.Focus();  

        }

        private void frm_EntryMain_Activated(object sender, EventArgs e)
        {
            txtFindPwr.Focus();
        }

        private void SetDataObjects(string spstring, string tblnam, params SqlParameter[] commandParameters)
        {
            ConnectionManager conobj = new ConnectionManager(); 
            connection = new SqlConnection(conobj.conn);

            if (tblnam=="EstMaster")
            {
                commandMast = new SqlCommand();
                if (spstring.Substring(0, 2) == "sp")
                {
                    commandMast.CommandType = CommandType.StoredProcedure;
                }
                else 
                {
                    commandMast.CommandType = CommandType.Text; 
                }

                commandMast.CommandText = spstring;
                commandMast.Parameters.Clear();
                commandMast.Parameters.AddRange(commandParameters);
                commandMast.Connection = connection;
                adapterMast = new SqlDataAdapter(sqlmast,connection);
                //adapterMast = new SqlDataAdapter(commandMast);
                builderMast = new SqlCommandBuilder(adapterMast);    
                
            }
            else
            {
                commandDetail = new SqlCommand();
                if (spstring.Substring(0, 2) == "sp")
                {
                    commandDetail.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    commandDetail.CommandType = CommandType.Text;
                }
                commandDetail.CommandText = spstring;
                commandDetail.Parameters.Clear();
                commandDetail.Parameters.AddRange(commandParameters);
                commandDetail.Connection = connection;
                adapterDetail = new SqlDataAdapter(commandDetail);
                builderDetail = new SqlCommandBuilder(adapterDetail);
            }

            //tempDataSet = new DataSet("TempDataSet");
        }

        private void SetForm()
        {
            try
            {
                if (Login.Cuserid == 95 || Login.Cuserid == 113)
                {
                    OBJFuncLib.FillCombo("UsrName", "UsrID", cmbAsgnUser, "VPUser", "");
                }
                else
                {
                    cmbAsgnUser.Visible = false;
                    txtAssignProjID.Visible = false;
                }
  
                
                OBJFuncLib.FillCombo("ProjTitle", "ProjtypeID", cboProjType, "ProjType", "");
                OBJFuncLib.FillCombo("Branch", "BrnID", cbobranch, "Branches", "");

                this.cboWorkDomain.DataSource = System.Enum.GetValues(typeof(MyDomain));
                int Domainid = (int)(cboWorkDomain.SelectedValue);

                //---------------------------------------
                string sqlmain = "SELECT EstMainID,EstimateID,EstimateNo,BranchID,ProjtypeID,SystemID,Description,EstDate,Acdyear,UsrID,UsrDT FROM EstMain where UsrID=" + Login.Cuserid + " AND IsCancelled IS Null";
                
                ConnectionManager conobj = new ConnectionManager(); 
                connection = new SqlConnection(conobj.conn);

                commandMain = new SqlCommand();
                commandMain.CommandType = CommandType.Text;
                commandMain.CommandText = sqlmain;
                commandMain.Connection = connection;
                //adapterMain = new SqlDataAdapter(sqlmain,connection);
                adapterMain = new SqlDataAdapter(commandMain);
                builderMain = new SqlCommandBuilder(adapterMain);    
                

                EstMain = new DataTable();
                adapterMain.Fill(EstMain);

                
                this.BindSource.DataSource = EstMain;  

                //-------------------------
                //this.BindSource = OBJFuncLib.MyBindSource("EstimateNo", "EstMain", BindSource, "UsrID=" + Login.Cuserid);
                if (this.BindSource.Count != 0)
                {
                    //this.BindSource.MoveLast();

                    //PopulateDataGridView();
                    //SetupMasterDataGridView();
                    //SetupDetailDataGridView();
                    //ds = new DataSet();
                    //setproperties();
                    
                    this.BindSource.MovePrevious();
                    this.BindSource.MoveLast();
                    this.BindNav.BindingSource = this.BindSource;

                    this.txtProjtypeID.Visible = true;  
                    this.txtbranchID.Visible = true;



                    this.txtProjtypeID.DataBindings.Add(new System.Windows.Forms.Binding("TEXT", this.BindSource, "ProjtypeID", true));
                    this.txtbranchID.DataBindings.Add(new System.Windows.Forms.Binding("TEXT", this.BindSource, "BranchID", true));
                    this.txtDesc.DataBindings.Add(new System.Windows.Forms.Binding("TEXT", this.BindSource, "Description", true));
                    this.txtProjID.DataBindings.Add(new System.Windows.Forms.Binding("TEXT", this.BindSource, "EstimateNo", true));
                    this.cboWorkDomain.DataBindings.Add(new System.Windows.Forms.Binding("TEXT", this.BindSource, "SystemID", true));
                    this.entdate.DataBindings.Add(new System.Windows.Forms.Binding("TEXT", this.BindSource, "EstDate", true));
                    this.txtacdyear.DataBindings.Add(new System.Windows.Forms.Binding("TEXT", this.BindSource, "Acdyear", true));

                    PEObj.Estno = 0;
                    PEObj.Estno = int.Parse(this.txtProjID.Text);

                    int cboval = int.Parse(txtProjtypeID.Text);
                    cboProjType.SelectedValue = cboval;

                    PEObj.Branchid = this.txtbranchID.Text;  
                    brid = int.Parse(PEObj.DLookup("BrnID", "Branches", "BranchID='" + PEObj.Branchid + "'").ToString()); 
                    cbobranch.SelectedValue = brid;

                    newvendor = false;

                    showrecord();
                    //showtotal(); 
                    
                }
                else
                {
                    //if (MessageBox.Show("No Project/Work Found Reference to you User ID!,Do You Want to Create New Project?",
                    //     "New User Entry Alert", MessageBoxButtons.YesNo,
                    //     MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, 0, false)
                    //     == DialogResult.Yes)
                    //{
                    //   // MessageBox.Show("User want to Create New Work Order!"); 

                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); 
            }
            finally
            {
                //SetupLayout();
                // this.BindNav.AddNewItem.Visible = false;
                //this.BindNav.DeleteItem.Visible = false;
                //this.cboUser.Text = "";            

                this.txtUserID.Text = Login.Cuser;
                txtFindPwr.Focus();  


            }
        
        }

        private void showrecord(Boolean fromfind)
        {
            this.txtbranchID.Visible = false;
            this.txtProjtypeID.Visible = false;

            if (this.txtProjID.Text == "")
            {
                PEObj.Estno = 0;
            }
            else
            {
                if (newproj == true)
                {
                    PEObj.Estno = 0;
                }
                else
                {
                    PEObj.Estno = int.Parse(this.txtProjID.Text);
                }
                if (this.txtProjtypeID.Text != "")
                {
                    PEObj.Projtypeid = int.Parse(this.txtProjtypeID.Text);
                }
                PEObj.Branchid = this.txtbranchID.Text;
                brid = int.Parse(PEObj.DLookup("BrnID", "Branches", "BranchID='" + PEObj.Branchid + "'").ToString());
                this.cbobranch.SelectedValue = brid;
                this.cboProjType.SelectedValue = PEObj.Projtypeid;

            }
            if (OBJFuncLib.DLookup("WorkNo", "EstMaster", "EstimateNo=" + PEObj.Estno) == null)
            {
                PEObj.Wrkno = 0;
            }
            else
            {
                PEObj.Wrkno = int.Parse(OBJFuncLib.DLookup("WorkNo", "EstMaster", "EstimateNo=" + PEObj.Estno));
            }
            SetupMasterDataGridView("EstimateNo", "EstMaster", "sp_GetPWRRecord", DG_Master, "EstimateNo=" + PEObj.Estno);
            SetupDetailDataGridView("EstimateNo", "EstDetail", "sp_GetPWRRecord", DG_Detail, "EstimateNo=" + PEObj.Estno + " and WorkNo=" + PEObj.Wrkno);
            DG_Master.AllowUserToAddRows = false;
            showtotal();
            txtFindPwr.Focus(); 

        }


        private void showrecord()
        {
           this.txtbranchID.Visible = false;
           this.txtProjtypeID.Visible = false;

            if (this.txtProjID.Text == "")
            {
                PEObj.Estno = 0;
            }
            else
            {
                if (newproj == true)
                {
                    PEObj.Estno = 0;
                }
                else
                {
                    PEObj.Estno = int.Parse(this.txtProjID.Text);
                }
                if (this.txtProjtypeID.Text != "")
                {
                    PEObj.Projtypeid = int.Parse(this.txtProjtypeID.Text);
                }
                PEObj.Branchid = this.txtbranchID.Text;
                //this.cbobranch.Text = PEObj.Branchid;
                brid = int.Parse(PEObj.DLookup("BrnID", "Branches", "BranchID='" + PEObj.Branchid + "'").ToString()); 
                this.cbobranch.SelectedValue = brid;
                this.cboProjType.SelectedValue = PEObj.Projtypeid;


            }
            if (OBJFuncLib.DLookup("WorkNo", "EstMaster", "EstimateNo=" + PEObj.Estno) == null)
            {
                PEObj.Wrkno = 0;
            }
            else
            {
                PEObj.Wrkno = int.Parse(OBJFuncLib.DLookup("WorkNo", "EstMaster", "EstimateNo=" + PEObj.Estno));
            }
            SetupMasterDataGridView("EstimateNo", "EstMaster", "sp_GetPWRRecord", DG_Master, "EstimateNo=" + PEObj.Estno);


            SetupDetailDataGridView("EstimateNo", "EstDetail", "sp_GetPWRRecord", DG_Detail, "EstimateNo=" + PEObj.Estno + " and WorkNo=" + PEObj.Wrkno);

            //MessageBox.Show(ds.Tables.Count.ToString());   
            //OBJFuncLib.MyDataGrid("EstimateNo", "EstMaster", "sp_GetPWRRecord", DG_Master, "EstimateNo=" + PEObj.Estno);
            //OBJFuncLib.MyDataGrid("EstimateNo", "EstDetail", "sp_GetPWRRecord", DG_Detail, "EstimateNo=" + PEObj.Estno + " and WorkNo=" + PEObj.Wrkno);

            //DG_Master.Rows[DG_Master.Rows.Count - 1].Selected = true;
            //DG_Master.CurrentCell = DG_Master.Rows[DG_Master.RowCount-1].Cells[3];
            //DG_Master.CurrentCell.Value = ShowTotal();
            DG_Master.AllowUserToAddRows = false;
            showtotal(); 


            //DG_Master.Rows[DG_Master.RowCount -2].Cells[2].Style.Font = new Font(DG_Master.Font, FontStyle.Bold);
            //DG_Master.Rows[DG_Master.RowCount -2].Cells[3].Style.Font = new Font(DG_Master.Font, FontStyle.Bold);
            //DG_Master.Rows[DG_Master.RowCount - 2].Cells[4].Style.Font = new Font(DG_Master.Font, FontStyle.Bold);
            //DG_Detail.Rows[DG_Detail.RowCount - 2].Cells[4].Style.Font = new Font(DG_Detail.Font, FontStyle.Bold);
            //DG_Detail.Rows[DG_Detail.RowCount - 2].Cells[5].Style.Font = new Font(DG_Detail.Font, FontStyle.Bold);
            //DG_Detail.Rows[DG_Detail.RowCount - 2].Cells[6].Style.Font = new Font(DG_Detail.Font, FontStyle.Bold);
            //DG_Detail.Rows[DG_Detail.RowCount - 2].Cells[7].Style.Font = new Font(DG_Detail.Font, FontStyle.Bold);

            //this.textBox1.Text = sum.ToString();
            //DataTable dt = new DataTable();
            
            //DataRow CountRow = dt.NewRow(); 
            //CountRow["Cost"] = sum.ToString();
            //this.DG_Master.Rows.Add(CountRow);
            //dt.Rows.Add(CountRow);
        }

        private void showtotal()
        {
            txtTotalCost.ReadOnly = true;
            txtTotalPaid.ReadOnly = true;
            txtTotalBal.ReadOnly = true;
            txtTotalCost.BackColor = Color.Aquamarine;
            txtTotalPaid.BackColor = Color.Aquamarine;
            txtTotalBal.BackColor = Color.Aquamarine;

           

            txtTotalCost.Text = String.Format("{0:#,##0}", Convert.ToDecimal(OBJFuncLib.MakeGridTotal(DG_Master, 7).ToString()));
            txtTotalPaid.Text = String.Format("{0:#,##0}", Convert.ToDecimal(OBJFuncLib.MakeGridTotal(DG_Master, 8).ToString()));
            txtTotalBal.Text = String.Format("{0:#,##0}", Convert.ToDecimal(OBJFuncLib.MakeGridTotal(DG_Master, 9).ToString()));

            txtGrossAmt.ReadOnly = true;
            txtGrossPaid.ReadOnly = true;
            txtremain.ReadOnly = true;
            txtTotalRecmd.ReadOnly = true;
            txtGrossAmt.BackColor = Color.Aquamarine;
            txtGrossPaid.BackColor = Color.Aquamarine;
            txtremain.BackColor = Color.Aquamarine;
            txtTotalRecmd.BackColor = Color.Aquamarine; 

            txtGrossAmt.Text = String.Format("{0:#,##0}", Convert.ToDecimal(OBJFuncLib.MakeGridTotal(DG_Detail, 1).ToString()));
            txtGrossPaid.Text = String.Format("{0:#,##0}", Convert.ToDecimal(OBJFuncLib.MakeGridTotal(DG_Detail, 2).ToString()));
            txtremain.Text = String.Format("{0:#,##0}", Convert.ToDecimal(OBJFuncLib.MakeGridTotal(DG_Detail, 3).ToString()));
            txtTotalRecmd.Text = String.Format("{0:#,##0}", Convert.ToDecimal(OBJFuncLib.MakeGridTotal(DG_Detail, 5).ToString()));
        }

        private void cboProjType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cboProjType.SelectedIndex = cboProjType.SelectedIndex + 1;
            if (this.cboProjType.SelectedIndex > 0)
            {
                //int cboval = int.Parse(txtProjtypeID.Text);
                cboProjType.SelectedValue = int.Parse(cboProjType.SelectedValue.ToString());
                this.txtProjtypeID.Text = PEObj.DLookup("ProjtypeID", "ProjType", "ProjtypeID=" + int.Parse(cboProjType.SelectedValue.ToString()));

                if (newproj == true)
                {
                    this.cboWorkDomain.DroppedDown = true;
                    this.cboWorkDomain.Focus();
                }
            }
        }
        
        private void SetupMasterDataGridView(string fldName, string tblName, string StProc, DataGridView mygrid, string whereCond)
        {
            try
            {
                if (EstMaster != null)
                {
                    EstMaster.Clear();
                }
                DG_Master.DataSource = null;
                DG_Master.Rows.Clear();
                DG_Master.Refresh();

                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@tblName", tblName);
                param[1] = new SqlParameter("@fldName", fldName);
                param[2] = new SqlParameter("@whereCond", whereCond);

                sqlmast = "Select EstMastID,EstimateNo,EstDate,WorkNo,WorkDesc,Rate,Quantity,Cost,Paid,(isnull([Cost],0)- isnull([Paid],0)) as Balance,IsAdditionalApproval as AddApprov from " + tblName + " where  " + whereCond + " Order by WorkNo";
                //+fldName;
                SetDataObjects(sqlmast, "EstMaster", param);
                EstMaster = new DataTable();
                adapterMast.Fill(EstMaster);
                
                //adapter.Fill(ds, "EstMaster");
                //EstMasterTable = ds.Tables["EstMaster"];

                EstMastBS = new BindingSource();
                EstMastBS.DataSource = EstMaster; 
                

                DG_Master.Columns.Clear(); 
                foreach (System.Data.DataColumn dc in EstMaster.Columns)
                {
                    DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                    column.DataPropertyName = dc.ColumnName;
                    //column.HeaderText = dc.ColumnName;
                    column.Name = dc.ColumnName;
                    column.SortMode = DataGridViewColumnSortMode.Automatic;
                    column.ValueType = dc.DataType;

                    if (dc.ColumnName != "AddApprov")
                    {
                        DG_Master.Columns.Add(column);
                    }


                    switch (column.DataPropertyName)
                    { 
                        case "WorkNo":
                              column.HeaderText = "Work_ID";
                              column.Width = 60;
                              column.ReadOnly = true;
                              column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; 
                              //column.Frozen = true;
                             break;

                        case "WorkDesc":
                             column.HeaderText = "Work Description";
                             column.Width = 350;
                             break;


                        case "Rate":
                             column.HeaderText = "Rate";
                             column.Width = 60;
                             column.DefaultCellStyle.Format = "##,0.00";
                             column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                             break;

                        case "Quantity":
                             column.HeaderText = "Quantity";
                             column.Width = 75;
                             column.DefaultCellStyle.Format = "##,0";
                             column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                             break;

                        case "Cost":
                             column.HeaderText = "Amount";
                             column.Width = 75;
                             column.DefaultCellStyle.Format = "##,0";
                             column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; 
                             break;

                        case "Paid":
                             column.HeaderText = "Paid Amount";
                             column.Width = 100;
                             column.DefaultCellStyle.Format = "##,0";
                             column.ReadOnly = true;
                             column.DefaultCellStyle.NullValue = String.Empty; 
                             column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; 
                             break;

                        case "Balance":
                             column.HeaderText = "Balance";
                             column.Width = 75;
                             column.DefaultCellStyle.Format = "##,0";
                             column.ReadOnly = true;
                             column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; 
                             break;
                     
                        case "AddApprov":
                              
                              DataGridViewCheckBoxColumn chkbx_key = new DataGridViewCheckBoxColumn();

                              chkbx_key = new DataGridViewCheckBoxColumn();
                              chkbx_key.Width = 40;
                              chkbx_key.FlatStyle = FlatStyle.Popup;
                              chkbx_key.DisplayIndex = 10;
                              DG_Master.Columns.Insert(10, chkbx_key);
                              chkbx_key.DataPropertyName = dc.ColumnName;
                              chkbx_key.ValueType = dc.DataType;
                              chkbx_key.HeaderText = "AddApprov";
                              chkbx_key.Width = 100;
                              break;


                        default: 
                            column.Visible=false;
                            break;
                    }
                }

                panel4.Controls.Add(this.DG_Master);

                DG_Master.DataSource = EstMastBS;
                //DG_Master.DataSource = EstMasterTable.DefaultView;

                //MakeColTotal(EstMasterTable);
                DG_Master.Columns["WorkDesc"].DefaultCellStyle.Format.ToUpper();
                DG_Master.AutoGenerateColumns = false;
                //DG_Master.ReadOnly = true;
                DG_Master.EditMode = DataGridViewEditMode.EditOnEnter;
                DG_Master.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Calibri", 9, FontStyle.Bold);
                //Font(DG_Master.Font, FontStyle.Bold);
                DG_Master.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DG_Master.ColumnHeadersDefaultCellStyle.BackColor = Color.Aqua;
                DG_Master.AllowUserToDeleteRows = true;
                DG_Master.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                DG_Master.RowsRemoved += new DataGridViewRowsRemovedEventHandler(DG_Master_RowsRemoved);


                //Master Grid Event Handlers
                //DG_Master.CellFormatting += new DataGridViewCellFormattingEventHandler(DG_Master_CellFormatting);
                //DG_Master.KeyPress += new System.Windows.Forms.KeyPressEventHandler(DG_Master_KeyPress);
                //DG_Master.KeyDown +=new KeyEventHandler(DG_Master_KeyDown);
                //DG_Master.PreviewKeyDown  +=new PreviewKeyDownEventHandler(DG_Master_PreviewKeyDown);
                //DG_Master.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(DG_Master_EditingControlShowing);   
                //DG_Master.CellValidated += new DataGridViewCellEventHandler(DG_Master_CellValidated);
                
                //DG_Master.SelectionChanged += new EventHandler(DG_Master_SelectionChanged);
                //DG_Master.UserAddedRow += new DataGridViewRowEventHandler(DG_Master_UserAddedRow);
                //DG_Master.UserDeletingRow += new DataGridViewRowCancelEventHandler(DG_Master_UserDeletingRow);
                //DG_Master.CellValidating += new DataGridViewCellValidatingEventHandler(DG_Master_CellValidating);

                //DG_Master.CurrentCellDirtyStateChanged += new EventHandler(DG_Master_CurrentCellDirtyStateChanged);
                DG_Master.CellValueChanged += new DataGridViewCellEventHandler(DG_Master_CellValueChanged);
                


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                connection.Close();
                this.Cursor = Cursors.Default;
            }

        }

        private void SetupDetailDataGridView(string fldName, string tblName, string StProc, DataGridView mygrid, string whereCond)
        {
            try
            {
                if (EstDetail != null)
                {
                   EstDetail.Clear();
                }

                DG_Detail.DataSource = null;
                DG_Detail.Rows.Clear();
                DG_Detail.Refresh();

                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@tblName", tblName);
                param[1] = new SqlParameter("@fldName", fldName);
                param[2] = new SqlParameter("@whereCond", whereCond);

                //SetDataObjects(StProc, "EstDetail", param);
                sqldetail = "SELECT EstDetailID,EstimateNo,VendorID,WorkNo,PayID,EntDate,GrossAmt,PaidAmt,(isnull([GrossAmt],0)- isnull([PaidAmt],0)) as BalAmt, Discount,PayRecmd,DueDate,BillInvoice,PayDetail,LockDele,PaidStatus,NewTrans,ShowonRpt FROM " + tblName + " where  " + whereCond + " Order by WorkNo, PayID";
                //+fldName;
                //sqldetail = "SELECT VendorID,WorkNo,PayID,GrossAmt,PaidAmt,(isnull([GrossAmt],0)- isnull([PaidAmt],0)) as BalAmt, Discount,PayRecmd,DueDate,BillInvoice,PayDetail,LockDele,PaidStatus,NewTrans FROM " + tblName + " where  " + whereCond + " Order by " + fldName;

                SetDataObjects(sqldetail, "EstDetail", param);
                
                //adapterDetail.Fill(ds, "EstDetail");
                //EstDetailTable = ds.Tables["EstDetail"];

                EstDetail = new DataTable();
                adapterDetail.Fill(EstDetail);
                EstDetailBS = new BindingSource();
                EstDetailBS.DataSource = EstDetail;

                panel5.Controls.Add(this.DG_Detail);
                
                DG_Detail.DataSource = EstDetail;
                DG_Detail.Columns.Clear();
                
                    //MessageBox.Show("Current Work No:" + PEObj.Wrkno.ToString());
                    foreach (System.Data.DataColumn dc in EstDetail.Columns)
                    {
                        if (dc.ColumnName != "EstDetailID" && dc.ColumnName != "EstimateNo" && dc.ColumnName != "WorkNo" && dc.ColumnName != "PayID" && dc.ColumnName != "EntDate" && dc.ColumnName != "DueDate" && dc.ColumnName != "LockDele" && dc.ColumnName != "PaidStatus" && dc.ColumnName != "NewTrans")
                        {
                            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                            column.DataPropertyName = dc.ColumnName;

                            if (dc.ColumnName == "VendorID")
                            {
                                DataGridViewComboBoxColumn columnv = new DataGridViewComboBoxColumn();
                                columnv = OBJFuncLib.combocolumn("VendorName", "VendorID", "Vendor", "");
                                columnv.DataPropertyName = dc.ColumnName;
                                columnv.Name = "VendorID";
                                columnv.SortMode = DataGridViewColumnSortMode.Automatic;
                                columnv.ValueType = dc.DataType;
                                DG_Detail.Columns.Add(columnv);

                                columnv.HeaderText = "Vendor Name";
                                columnv.Width = 320;
                                columnv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                                columnv.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                                columnv.ReadOnly = true;
                            }
                            else
                            {

                                if (dc.ColumnName != "ShowonRpt")
                                {
                                    column.Name = dc.ColumnName;
                                    column.SortMode = DataGridViewColumnSortMode.Automatic;
                                    column.ValueType = dc.DataType;
                                    DG_Detail.Columns.Add(column);
                                }

                            }

                          


                            switch (column.DataPropertyName)
                            {
                                //case "VendorID":


                                case "GrossAmt":
                                    column.HeaderText = "Gross Amount";
                                    column.Width = 70;
                                    column.DefaultCellStyle.Format = "##,0";
                                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                    break;

                                case "PaidAmt":
                                    column.HeaderText = "Paid Amount";
                                    column.Width = 70;
                                    column.ReadOnly = true;
                                    column.DefaultCellStyle.Format = "##,0";
                                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                    break;

                                case "BalAmt":
                                    column.HeaderText = "Remain Amt";
                                    column.Width = 80;
                                    column.DefaultCellStyle.Format = "##,0";
                                    column.ReadOnly = true;
                                    column.DefaultCellStyle.NullValue = String.Empty;
                                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                    break;

                                case "PayRecmd":
                                    column.HeaderText = "Pay Recmd";
                                    column.Width = 80;
                                    column.DefaultCellStyle.Format = "##,0";
                                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                    break;

                                case "Discount":
                                    column.HeaderText = "Discount";
                                    column.Width = 50;
                                    column.DefaultCellStyle.Format = "##,0";
                                    column.ReadOnly = true;
                                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                    break;

                                case "PayDetail":
                                    column.HeaderText = "Payment Detail";
                                    column.Width = 470;
                                    column.DefaultCellStyle.Format = "##,0";
                                    column.ReadOnly = true;
                                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                                    break;

                                case "BillInvoice":
                                    column.HeaderText = "Invoice";
                                    column.Width = 50;
                                    column.DefaultCellStyle.Format = "##,0";
                                    column.ReadOnly = true;
                                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                                    break;

                                case "ShowonRpt":
 
                                    DataGridViewCheckBoxColumn chkbx_key = new DataGridViewCheckBoxColumn();

                                    chkbx_key = new DataGridViewCheckBoxColumn();
                                    chkbx_key.Width = 40;
                                    chkbx_key.FlatStyle = FlatStyle.Popup;
                                    chkbx_key.DisplayIndex = 8;
                                    DG_Detail.Columns.Insert(8, chkbx_key);
                                    chkbx_key.DataPropertyName = dc.ColumnName;
                                    chkbx_key.ValueType = dc.DataType;
                                    chkbx_key.HeaderText = "Hold Recmd";
                                    chkbx_key.Width = 50;
                                    break;


                                default:
                                    column.Visible = false;
                                    break;

                            }
                        }
                    }

                    //DG_Detail.DataSource = EstDetailTable.DefaultView;

                    //----------- Add Total Row ---------
                    //MakeColTotal(EstDetailTable);
                    //-----------------------------------
                    //DG_Detail.DefaultCellStyle.NullValue = "NA";

                    DG_Detail.Columns["PayDetail"].DefaultCellStyle.Format.ToUpper();

                    DG_Detail.AutoGenerateColumns = false;
                    DG_Detail.ReadOnly = true;
                    DG_Detail.EditMode = DataGridViewEditMode.EditOnEnter;
                    DG_Detail.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Calibri", 9, FontStyle.Bold);
                    DG_Detail.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    DG_Detail.ColumnHeadersDefaultCellStyle.BackColor = Color.Aqua;
                    DG_Detail.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    DG_Detail.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    DG_Detail.MultiSelect = false;
                    DG_Detail.Dock = DockStyle.Top;
                    DG_Detail.AllowUserToDeleteRows = true;
                    DG_Detail.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;

                //}
                //Detail Grid Event Handler
                //DG_Detail.CellFormatting += new DataGridViewCellFormattingEventHandler(DG_Detail_CellFormatting);
                //DG_Detail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(DG_Detail_KeyPress);
                //DG_Detail.CellValidated += new DataGridViewCellEventHandler(DG_Detail_CellValidated);
                //DG_Detail.RowsRemoved += new DataGridViewRowsRemovedEventHandler(DG_Detail_RowsRemoved);
                //DG_Detail.SelectionChanged += new EventHandler(DG_Detail_SelectionChanged);
                //DG_Detail.UserAddedRow += new DataGridViewRowEventHandler(DG_Detail_UserAddedRow);
                DG_Detail.CellValidating += new DataGridViewCellValidatingEventHandler(DG_Detail_CellValidating);
                DG_Detail.CellEndEdit += new DataGridViewCellEventHandler(DG_Detail_CellEndEdit); 
                //DG_Detail.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(DG_Detail_EditingControlShowing);   
                    
                //DG_Detail.KeyDown += new KeyEventHandler(DG_Detail_KeyDown); 
                //DG_Detail.PreviewKeyDown += new PreviewKeyDownEventHandler(DG_Detail_PreviewKeyDown); 
                //DG_Detail.CellLeave += new DataGridViewCellEventHandler(DG_Detail_CellLeave); 

                //DG_Detail.UserDeletedRow += new DataGridViewRowEventHandler(DG_Detail_UserDeletedRow);   
                //DG_Detail.UserDeletingRow+=new DataGridViewRowCancelEventHandler(DG_Detail_UserDeletingRow); 
                DG_Detail.CellValueChanged += new DataGridViewCellEventHandler(DG_Detail_CellValueChanged);
                DG_Detail.RowsRemoved +=new DataGridViewRowsRemovedEventHandler(DG_Detail_RowsRemoved);

                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                connection.Close();
                this.Cursor = Cursors.Default;
            }

        }


        void DG_Detail_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
           // throw new NotImplementedException();
        }


        //private void MakeColTotal(DataTable tbl)
        //{
        //    DataRow dr;
        //    if (tbl.TableName == "EstMaster")
        //    {
        //        //-----Making Total Row -------
        //        dr = tbl.NewRow();
        //        float cst = 0;
        //        float pad = 0;
        //        for (int i = 0; i < tbl.Rows.Count; i++)
        //        {
        //            cst = cst + float.Parse(tbl.Rows[i]["Cost"].ToString());
        //            pad = pad + float.Parse(tbl.Rows[i]["Paid"].ToString());
        //        }
        //        dr[2] = "Total";
        //        dr[3] = cst.ToString();
        //        dr[4] = pad.ToString();
        //        tbl.Rows.Add(dr);
        //        //----------------------------------------------
        //    }
        //    else
        //    {
        //        //-----Making Total Row -------
        //        dr = tbl.NewRow();
        //        float gros = 0;
        //        float pad = 0;
        //        float rem = 0;
        //        float rcd = 0;
        //        for (int i = 0; i < tbl.Rows.Count; i++)
        //        {
        //            gros = gros + float.Parse(tbl.Rows[i]["GrossAmt"].ToString());
        //            pad = pad + float.Parse(tbl.Rows[i]["PaidAmt"].ToString());
        //            rem = rem + float.Parse(tbl.Rows[i]["BalAmt"].ToString());
        //            rcd = rcd + float.Parse(tbl.Rows[i]["PayRecmd"].ToString());
        //        }

        //        // dr[1] = "Total";
        //        dr[4] = gros.ToString();
        //        dr[5] = pad.ToString();
        //        dr[6] = rem.ToString();
        //        dr[7] = rcd.ToString();
        //        tbl.Rows.Add(dr);
        //        //----------------------------------------------
        //    }
        //}
        

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            showrecord();
        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            showrecord();
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            showrecord();
        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            showrecord();
        }

        private void btnNewWork_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Want to Start New Project?",
                         "New Project Entry Alert", MessageBoxButtons.YesNo,
                         MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, 0, false)
                         == DialogResult.No)
            {
                //this.cboProjType.DroppedDown = true;
                //this.cboProjType.Focus();
                //this.txtDesc.Focus();
                return;
            }


            saveonce = false;   
            newproj = true; 
            EnableControls();
            this.BindSource.AddNew();

            ////this.txtProjtypeID.DataBindings.Add(new Binding("TEXT", this.BindSource, "ProjtypeID", true));
            //this.txtbranchID.DataBindings.Add(new Binding("TEXT", this.BindSource, "BranchID", true));
            //this.txtDesc.DataBindings.Add(new Binding("TEXT", this.BindSource, "Description", true));
            //this.txtProjID.DataBindings.Add(new Binding("TEXT", this.BindSource, "EstimateNo", true));
            //this.cboWorkDomain.DataBindings.Add(new Binding("TEXT", this.BindSource, "SystemID", true));
            //this.entdate.DataBindings.Add(new Binding("TEXT", this.BindSource, "EstDate", true));
            //this.txtacdyear.DataBindings.Add(new Binding("TEXT", this.BindSource, "Acdyear", true));
            //PEObj.Estno = int.Parse(OBJFuncLib.DLookup("LastEstimateNo", "MastPara", ""));
            //PEObj.Estno = PEObj.Estno + 1;
            this.txtProjID.Text = "NEW";
            this.txtProjtypeID.Text = "6";
            this.txtbranchID.Text = "HO";
            this.cboWorkDomain.SelectedIndex = 0;
            this.txtacdyear.Text = DateTime.Now.ToString("yyyy") + "-" + DateTime.Now.AddYears(1).ToString("yyyy");
            newwork = 0;
            showrecord();
            this.txtDesc.Text = "";
            //this.txtDesc.Focus();
            this.cboProjType.DroppedDown = true;
            this.cboProjType.Focus();

            

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            justclick = true;
            this.btnUpdate.BackColor = SystemColors.Control;
            this.btnUpdate.Refresh();
            DG_Master.Enabled = true; 
            
            if (newproj == false && btnSave.Text == "Save")
            {
                btnSave.Text = "Update";
            }
            else
            {
                btnSave.Text = "Save";
            }
            editstart = true;
            if (MessageBox.Show("Do You Want to Add New Entry?",
                         "New Work Entry Alert", MessageBoxButtons.YesNo,
                         MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, 0, false)
                         == DialogResult.Yes)
            {
                row = EstMaster.NewRow();
                EstMaster.Rows.Add(row);
                DataRow currentRow = EstMaster.Rows[DG_Master.CurrentCell.RowIndex];
                row["EstimateNo"] = PEObj.Estno;
               // row["ProjtypeID"] = Int32.Parse(this.cboProjType.SelectedIndex.ToString());
                row["EstDate"] = DateTime.Now.ToString();
                
                //if (OBJFuncLib.DLookup("WorkNo", "EstMaster", "EstimateNo=" + PEObj.Estno) == null)
                //{
                //}
                //else
                //{
                //    newwork = int.Parse(OBJFuncLib.DLookup("WorkNo", "EstMaster", "EstimateNo=" + PEObj.Estno));
                //}
                //MessageBox.Show(EstMaster.Rows[0]["WorkNo"].ToString()=="");
                DG_Master.AllowUserToAddRows = true;
 
                if (EstMaster.Rows.Count != 0 && EstMaster.Rows[0]["WorkNo"].ToString()!="")
                {
                    foreach (DataRow mrow in EstMaster.Rows)
                    {
                        if (mrow["WorkNo"].ToString() != "")
                        {
                            newwork = int.Parse(mrow["WorkNo"].ToString());
                        }
                    }
                }
                newwork++;
                row["WorkNo"] = newwork;
                if (newwork == 1)
                {
                    workentry1 = true; 
                }

                EnableControls();
                row.BeginEdit();
                this.DG_Master.Focus();
                DG_Master.Rows[0].Cells["WorkDesc"].Selected = true;
  
            }
            else
            {
                workentry1 = false;
                txtDesc.Enabled = true;
                //DG_Master.FirstDisplayedScrollingRowIndex;          
                EnableControls();
               // row = EstMaster.Rows[0];
                //row.BeginEdit(); 
                this.DG_Master.Focus();
            }
  
        }

        void EnableControls()
        {
            this.cboProjType.Enabled = true;
            this.cbobranch.Enabled = true;
            this.cboWorkDomain.Enabled = true;
            this.txtDesc.Enabled = true;
            this.entdate.Enabled = true;
            this.DG_Master.ReadOnly = false;
            this.DG_Detail.ReadOnly = false;
            //this.DG_Detail.Columns["PaidAmt"].ReadOnly = true;
            //this.DG_Detail.Columns["BalAmt"].ReadOnly = true;
        }

        private void txtFindPwr_TextChanged(object sender, EventArgs e)
        {
            int recno = 0;
            try
            {
                if (txtFindPwr.Text != "")
                {

                    recno = this.BindSource.Find("EstimateNo", txtFindPwr.Text);
                    if (recno != -1)
                    {
                        this.BindSource.Position = recno;
                        showrecord(true);
                        lblcancel.Visible = false;
                        // SetForm();
                    }
                    else
                    {
                        lblcancel.Visible = true;
                    }
                }
                else
                {
                    lblcancel.Visible = false; 
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.BindSource.MoveLast();
                showrecord(true);
            }
            finally
            {
                this.BindSource.Position = recno;
                showrecord(true);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            frm_login loginform;
            loginform = new frm_login();
            this.Close();
            loginform.Show(); 
        }

        private void btnAUPayment_Click(object sender, EventArgs e)
        {
            justclick = true;
            DG_Detail.AllowUserToDeleteRows = true;
            editstart = true;
            if (newproj==false && btnSave.Text == "Save")
            {
                btnSave.Text = "Update";
            }
            else
            {
                btnSave.Text = "Save";
            }

            DG_Detail.Enabled = true;

           // DataRow currentRow = EstDetail.Rows[DG_Detail.CurrentCell.RowIndex];

            if (MessageBox.Show("Do You Want to Add New Entry?",
                         "New Work Entry Alert", MessageBoxButtons.YesNo,
                         MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, 0, false)
                         == DialogResult.Yes)
            {

                MessageBox.Show("If You Have New Vendor and You Sure, Please Use New Vendor Option Below to Add New Vendor First!");  
                newvendor = false;
                 
                foreach (DataRow mrow in EstDetail.Rows)
                {
                    payid = int.Parse(mrow["PayID"].ToString());
                }
                if (this.DG_Detail.Rows.Count.ToString()=="0")
                {
                    payid = 0;
                }
                payid++;
                row = EstDetail.NewRow();
                EstDetail.Rows.Add(row);
                row["EstimateNo"] = PEObj.Estno;
                row["WorkNo"] = PEObj.Wrkno;
                row["EntDate"] = DateTime.Now.ToString();
                row["DueDate"] = DateTime.Now.ToString();                  
                row["PayID"] = payid;
                
                if (payid == 1)
                {
                    payentry1 = true;
                }
                
            }
            // MessageBox.Show("Current Work No:" + PEObj.Wrkno.ToString());
            else
            {
                payentry1 = false;
                if (EstDetail.Rows.Count != 0)
                {
                    row = EstDetail.Rows[0];
                    row["EstimateNo"] = PEObj.Estno;
                    row["EntDate"] = DateTime.Now.ToString();
                    row["DueDate"] = DateTime.Now.ToString();
                    row["Workno"] = PEObj.Wrkno;
                }
                //EstDetail.Rows.Add(row);
            }
            EnableControls();
            //row.BeginEdit(); 
            this.DG_Detail.Focus();  
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            
            if (editstart == false && saveonce==true)
            {
                this.btnRptPayRequest.Enabled = true;
                this.btnEstimateReport.Enabled = true;
                this.btnToglRecmd.Enabled = true;

                MessageBox.Show("Record Has Been Successfully Saved!");
                newproj = false;
                editstart = true;
                return;
            }

            if (newproj==true)
            {
                sqlMastPara = "Update MastPara Set PrintEstno=" + PEObj.Estno;
                command = new SqlCommand(sqlMastPara, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                
                try
                {
                    
                    this.Validate();
                    this.BindSource.EndEdit();
                    this.BindNav.Update();

                    PostMainTable("I");
                    PostMasterTable();
                    PostDetailTable();
                    PostPaymentTable();

                    MessageBox.Show("Record Has Been Successfully Saved!");
                    newproj = false;
                    editstart = true;
                }
                catch(Exception ex) 
                {
                    MessageBox.Show(ex.ToString());  
                }
            }
            else
            {
                try
                {
                    this.Validate();
                    this.BindSource.EndEdit();
                    this.BindNav.Update();

                    PostMainTable("U");

                    //adapterMain.Update(EstMain);  
                    //CHeck if New Entry of Work is Made on Existing Work Order, Then Insert Process will Run
                    if (workentry1 == true)
                    {
                        
                        PostMasterTable();
                    }
                    else
                    {
                        //row.EndEdit();
                        //Update Master Table
                         
                        adapterMast.UpdateCommand = builderMast.GetUpdateCommand();
                                 //adapterMast.InsertCommand.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord;  

                        adapterMast.AcceptChangesDuringUpdate = true;

                        adapterMast.AcceptChangesDuringFill = true;

                        adapterMast.Update(EstMaster);

                                //DataTable dataChanges = EstMaster.GetChanges();
                        //adapterMast.RowUpdated += new SqlRowUpdatedEventHandler(OnRowUpdated);
                      
                        //adapterMast.Update(dataChanges);

                        //EstMaster.Merge(dataChanges);
                        //EstMaster.AcceptChanges();
                          

                    }
                    //CHeck if New Entry of Payment is Made on Existing Work Order, Then Insert Process will Run
                    if (payentry1 == true)
                    {
                        PostDetailTable();
                    }
                    else
                    {
                        //Update Detail Table
                        adapterDetail.UpdateCommand = builderDetail.GetUpdateCommand();
                        adapterDetail.Update(EstDetail);
                    }

                    DateTime wodate;
                    wodate = DateTime.Parse(this.entdate.Text);

                    PEObj.Branchid = this.txtbranchID.Text;
                    PEObj.Desc = this.txtDesc.Text;
                    PEObj.Entdate = wodate.ToString("MM/dd/yyyy");
                    PEObj.Projtypeid = int.Parse(this.cboProjType.SelectedValue.ToString());
                    PEObj.Sysid = cboWorkDomain.SelectedValue.ToString();
                    PEObj.Acdyear = this.txtacdyear.Text;
                    PEObj.Usrid = Login.Cuserid;

                    MessageBox.Show("Record Has Been Successfully Updated!");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());  
                }
            }
            this.btnRptPayRequest.Enabled = true;
            this.btnEstimateReport.Enabled = true;
            this.btnToglRecmd.Enabled = true;
        }

        protected static void OnRowUpdated(object sender, SqlRowUpdatedEventArgs e)
        {
            // If this is an insert, then skip this row.
            if (e.StatementType == StatementType.Insert)
            {
                e.Status = UpdateStatus.SkipCurrentRow;
            }
        }


        //private void RefreshMasterDetail(string tblname)
        //{
        //    if (tblname == "EstMaster")
        //    {
        //        sqlMastPara = "Delete From " + tblname + " where EstimateNo=" + PEObj.Estno;
        //    }
        //    else
        //    {
        //        sqlMastPara = "Delete From " + tblname + " where EstimateNo=" + PEObj.Estno + " and ;
        //    }

        //    command = new SqlCommand(sqlMastPara, connection);
        //    connection.Open();
        //    command.ExecuteNonQuery();
        //    connection.Close();
        
        //}


        private void PostMainTable(string token)
        {
            
            bool result;

            ArrayList fieldlist = new ArrayList();

            setMainproperties();

            //Passwing property values to arraylist
            fieldlist.Add(PEObj.Usrid);
            fieldlist.Add(PEObj.Estid);
            fieldlist.Add(PEObj.Estno);
            fieldlist.Add(PEObj.Branchid);
            fieldlist.Add(PEObj.Projtypeid);
            fieldlist.Add(PEObj.Sysid);
            fieldlist.Add(PEObj.Desc);
            fieldlist.Add(PEObj.Entdate);
            fieldlist.Add(PEObj.Acdyear);

            result = PEObj.MainRecord(fieldlist, token);

            if (result == true)
            {
               //MessageBox.Show("Record Has Been Successfully Added!");
            }
            else
            {
                //MessageBox.Show("Record Doest Not Saved!!!!!");
                //this.btnSave.Text = "&Save";
            }
        }

        private void setMainproperties()
        {
            //PEObj.Estno = int.Parse(this.txtProjID.Text);

            //

            string lastestno;
            if (this.txtProjID.Text == "NEW")
            {
                lastestno = PEObj.DLookup("MAX(EstimateNo)", "EstMain", "");

                if (lastestno == "")
                {
                    lastestno = "0";
                }

                PEObj.Estno = int.Parse(lastestno) + 1;
                this.txtProjID.Text = PEObj.Estno.ToString();
                string eststring;
                int estlen;
                estlen = this.txtProjID.TextLength;
                switch (estlen)
                {
                    case 1:
                        eststring = "00000000";
                        break;
                    case 2:
                        eststring = "0000000";
                        break;
                    case 3:
                        eststring = "000000";
                        break;
                    case 4:
                        eststring = "00000";
                        break;
                    case 5:
                        eststring = "0000";
                        break;
                    case 6:
                        eststring = "000";
                        break;
                    case 7:
                        eststring = "00";
                        break;
                    case 8:
                        eststring = "0";
                        break;
                    default:
                        eststring = this.txtProjID.Text;
                        break;
                }

                if (eststring == this.txtProjID.Text)
                {
                }
                else
                {
                    eststring = eststring + this.txtProjID.Text;
                }
                eststring = eststring + "-" + DateTime.Parse(entdate.Text).ToString("MMM").ToUpper() + DateTime.Parse(entdate.Text).ToString("yy").Trim();
                PEObj.Estid = eststring;
            }
            else
            {
                PEObj.Estid = this.txtProjID.Text;
            }

            DateTime wodate = DateTime.Parse(entdate.Text); 
            
            //string bid = PEObj.DLookup("BranchID", "Branches", "BrnID=" + );
            PEObj.Branchid = this.txtbranchID.Text;
            PEObj.Desc = this.txtDesc.Text;
            PEObj.Entdate = wodate.ToString("MM/dd/yyyy"); 
            PEObj.Projtypeid = int.Parse(this.cboProjType.SelectedValue.ToString());
            //PEObj.Sysid = cboWorkDomain.SelectedValue.ToString();
            PEObj.Sysid = cboWorkDomain.Text;  
            PEObj.Acdyear = this.txtacdyear.Text;
            PEObj.Usrid = Login.Cuserid;
            //ProjEntry.PWRNumber = PEObj.Estno; 

        }

        private void makeamount(int rowindex)
        {
            //DG_Master.Columns["WorkDesc"].
            string rat = DG_Master.Rows[rowindex].Cells["Rate"].Value.ToString() == "" ? "0" : DG_Master.Rows[rowindex].Cells["Rate"].Value.ToString();
            string qtt = DG_Master.Rows[rowindex].Cells["Quantity"].Value.ToString() == "" ? "0" : DG_Master.Rows[rowindex].Cells["Quantity"].Value.ToString();
           
            
            double amt = float.Parse(rat) * float.Parse(qtt);

                

            DG_Master.Rows[rowindex].Cells["Cost"].Value = amt.ToString(); 

        }


        private void PostMasterTable()
        {
            int wrkno = 0;
            string wdesc ="";
            float wrate = 0;
            float wqtty = 0;
            double wcost=0;
            //int rowloop = 0;
            
            foreach (DataRow row in EstMaster.Rows)
            {
                    wrkno = int.Parse(row["WorkNo"].ToString());
                    wdesc = row["WorkDesc"].ToString();
                    wrate = float.Parse(row["Rate"].ToString());
                    wqtty = float.Parse(row["Quantity"].ToString());

                    wcost = float.Parse(row["Rate"].ToString()) * float.Parse(row["Quantity"].ToString());
                    //wcost = double.Parse(row["Cost"].ToString());
                    
                    bool result;
                    ArrayList fieldlist = new ArrayList();
                    
                    fieldlist.Add(PEObj.Estno);
                    fieldlist.Add(wrkno);
                    fieldlist.Add(wdesc);
                    fieldlist.Add(wrate);
                    fieldlist.Add(wqtty);
                    fieldlist.Add(wcost);
                    fieldlist.Add(this.entdate.Text);

                    result = PEObj.MasterRecord(fieldlist, "I");


                    if (result == true)
                    {
                        //MessageBox.Show("Record Has Been Successfully Added!");
                    }
                    else
                    {
                        //MessageBox.Show("Record Doest Not Saved!!!!!");
                        //this.btnSave.Text = "&Save";
                    }
                //}
                //break; 
            }
        }

        private void PostDetailTable()
        {
            int payid = 0;
            int vdrid = 0;
            double gross = 0;
            float disc = 0;
            double payrc = 0;
            DateTime dudat = DateTime.Now;
            string billinvo = "";
            string paydetl = "";

            if (EstDetail.Rows.Count==0) 
            {
              return;
            }

            DataRow last = EstDetail.Rows[EstDetail.Rows.Count - 1];

            if (int.Parse(EstDetail.Rows.Count.ToString()) > 1)
            {
                DataRow Prvlast = EstDetail.Rows[EstDetail.Rows.Count - 2];
                int lastpayid = int.Parse(Prvlast["PayID"].ToString());

                if (int.Parse(last["PayID"].ToString()) == lastpayid)
                {
                    return;
                }
            }


            foreach (DataRow row in EstDetail.Rows)
            {
                if (row==last)
                {
                       
                        payid = int.Parse(last["PayID"].ToString());
                        vdrid = int.Parse(last["VendorID"].ToString());
                        gross = (last["GrossAmt"] is DBNull) ? 0 : double.Parse(last["GrossAmt"].ToString()); 

                        if (last["Discount"].ToString() == null || last["Discount"].ToString() == string.Empty)
                        {
                            disc = 0;
                        }
                        else
                        {
                            disc = float.Parse(last["Discount"].ToString());
                        }

                        payrc = (last["PayRecmd"] is DBNull) ? 0 : double.Parse(last["PayRecmd"].ToString());

                        //dudat =    last["DueDate"].ToString();
                        billinvo = last["BillInvoice"].ToString();
                        paydetl = last["PayDetail"].ToString();

                        bool result;
                        ArrayList fieldlist = new ArrayList();

                        //Passwing property values to arraylist
                        fieldlist.Add(PEObj.Estno);
                        fieldlist.Add(vdrid);
                        fieldlist.Add(PEObj.Wrkno);
                        fieldlist.Add(payid);
                        fieldlist.Add(gross);
                        fieldlist.Add(disc);
                        fieldlist.Add(payrc);
                        fieldlist.Add(dudat);
                        fieldlist.Add(billinvo);
                        fieldlist.Add(paydetl);
                        fieldlist.Add(1);

                        result = PEObj.DetailRecord(fieldlist, "I");
                        if (result == true)
                        {
                            //MessageBox.Show("Record Has Been Successfully Added!");
                        }
                        else
                        {
                            //MessageBox.Show("Record Doest Not Saved!!!!!");
                           // this.btnSave.Text = "&Save";
                        }

                }
            }
        }
        
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.BindSource.EndEdit();
                this.BindNav.Update();
                connection.Open();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //MessageBox.Show("Update failed");
            }
            finally
            {
                connection.Close(); 
            }
        }

        //-----------------------------------------
        //Key & Click Events for Master Data Grid
        //-----------------------------------------

        private void DG_Master_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //DataGridViewTextBoxEditingControl dText = (DataGridViewTextBoxEditingControl)e.Control;
            //dText.Select(0, 0);

            if (e.Control.GetType() == typeof(DataGridViewTextBoxEditingControl)) // "System.Windows.Forms.DataGridViewTextBoxEditingControl")
            {
                DataGridViewTextBoxEditingControl c = (DataGridViewTextBoxEditingControl)e.Control;
                //c.TabIndex = 2; 
                //c.DeselectAll();
                if (c.Controls.Count > 0)
                {
                    c.SelectionStart = 0;
                }
            }

        } 


        private void DG_Master_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int celno;
            int yCoord = DG_Master.CurrentCellAddress.Y; // You can get X if you need it.
            celno = int.Parse(e.ColumnIndex.ToString());

            if (celno == 7 || celno == 8)
            {
                DG_Master.Rows[yCoord].Cells[celno].ReadOnly = true;
                DG_Master.Rows[yCoord].Cells[celno + 1].ReadOnly = true;
            }

            makeamount(yCoord);
        }

        private void DG_Master_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(e.ColumnIndex.ToString());
            int celno;
            try
            {
                int yCoord = DG_Master.CurrentCellAddress.Y; // You can get X if you need it.
                celno = int.Parse(e.ColumnIndex.ToString());

                //if (celno == 6)
                //{
                //    DG_Master.Rows[yCoord].Cells[celno].ReadOnly = true;
                //    DG_Master.Rows[yCoord].Cells[celno+1].ReadOnly = true;    
                //}

                makeamount(yCoord);

                if (celno != 3)
                {
                    celno = 3;
                }


                PEObj.Wrkno = (int)DG_Master.Rows[yCoord].Cells[celno].Value;


               // MessageBox.Show(DG_Master.Rows[yCoord].Cells[celno + 2].Value.ToString());

                workgrossamt = double.Parse(DG_Master.Rows[yCoord].Cells[celno + 4].Value.ToString() == "" ? "0" : DG_Master.Rows[yCoord].Cells[celno + 4].Value.ToString());
                //MessageBox.Show(workgrossamt.ToString());    
                ProjEntry.WorkID = PEObj.Wrkno; 

                //if (workentry1 == false)
                //{

                    //OBJFuncLib.MyDataGrid("EstimateNo", "EstDetail", "sp_GetPWRRecord", DG_Detail, "EstimateNo=" + PEObj.Estno + " and WorkNo=" + PEObj.Wrkno);

                    SetupDetailDataGridView("EstimateNo", "EstDetail", "sp_GetPWRRecord", DG_Detail, "EstimateNo=" + PEObj.Estno + " and WorkNo=" + PEObj.Wrkno);
                    //MessageBox.Show(PEObj.Wrkno.ToString());     
                    //DG_Detail.Rows[DG_Detail.RowCount - 2].Cells[4].Style.Font = new Font(DG_Detail.Font, FontStyle.Bold);
                    //DG_Detail.Rows[DG_Detail.RowCount - 2].Cells[5].Style.Font = new Font(DG_Detail.Font, FontStyle.Bold);
                    //DG_Detail.Rows[DG_Detail.RowCount - 2].Cells[6].Style.Font = new Font(DG_Detail.Font, FontStyle.Bold);
                    //DG_Detail.Rows[DG_Detail.RowCount - 2].Cells[7].Style.Font = new Font(DG_Detail.Font, FontStyle.Bold);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MessageBox.Show("Please Select Only At Work Number To get Desire Payment Information", "Information",
                //MessageBox.Show("You may get the list sql servers if user name and password are not correct.", "Information",
                //MessageBoxButtons.OK, MessageBoxIcon.Information,
                //MessageBoxDefaultButton.Button1, 0, false);

                // PEObj.Wrkno = int.Parse(e.RowIndex.ToString());  
            }
            finally
            {
                showtotal();
             
                //PEObj.Wrkno = 1;
                //celno = int.Parse(e.ColumnIndex.ToString());
                //int yCoord = DG_Master.CurrentCellAddress.Y; // You can get X if you need it.
                //celno = 0;
                //PEObj.Wrkno = (int)DG_Master.Rows[yCoord].Cells[celno].Value;
                //OBJFuncLib.MyDataGrid("EstimateNo", "EstDetail", "sp_GetPWRRecord", DG_Detail, "EstimateNo=" + PEObj.Estno + " and WorkNo=" + PEObj.Wrkno);
            }

        }

        private void DG_Master_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            //MessageBox.Show("Hello! I AM Master Table");
        }

        private void DG_Master_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Update the balance column whenever the value of any cell changes.

            if (this.DG_Master.CurrentCell != null)
            {

                if (this.DG_Master.CurrentCell.OwningColumn.Name.ToString() == "Cost")
                {
                    showtotal();
                }

                if (e.ColumnIndex == 10)
                {
                    foreach (DataGridViewRow row in DG_Master.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            DataGridViewCheckBoxCell checkBox = DG_Master[10, row.Index] as DataGridViewCheckBoxCell;
                            if (checkBox != null)
                            {
                                if (checkBox.Value.ToString() == "1")
                                    DG_Master.Rows[row.Index].Selected = true;
                                else
                                    DG_Master.Rows[row.Index].Selected = false;
                            }
                        }
                    }
                }
            }
            justclick = false;
        }

        private void DG_Master_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            // Update the balance column whenever rows are deleted.
           // showtotal();
        }

        private void DG_Master_SelectionChanged(object sender, EventArgs e)
        {
            // Update the labels to reflect changes to the selection.
            //UpdateLabelText();
        }

        private void DG_Master_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            // Update the labels to reflect changes to the number of entries.
            //UpdateLabelText();
              
        }

        private void DG_Master_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //int testint;

            if (e.ColumnIndex == 5)
            {

                //if (e.FormattedValue.ToString().Length != 0)
                //{
                //    // Try to convert the cell value to an int.
                //    testint = int.Parse(e.FormattedValue);
                
   
                //    if (!int.TryParse(e.FormattedValue.ToString(), out testint))
                //    {
                //        DG_Master.Rows[e.RowIndex].ErrorText = "Error: Invalid entry";
                //        e.Cancel = true;
                //    }
                //}
            }
        }

        private void DG_Master_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            // Clear any error messages that may have been set in cell validation.
            //DG_Master.Rows[e.RowIndex].ErrorText = null;
             
        }

        private void DG_Master_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DG_Master.UserDeletingRow += new DataGridViewRowCancelEventHandler(DG_Master_UserDeletingRow);
        }
        
        private void DG_Master_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (DG_Detail.Rows.Count > 0)
            {

                DataGridViewRow startingBalanceRow = DG_Detail.Rows[0];
                // Check if the Starting Balance row is included in the selected rows
                //MessageBox.Show(DG_Detail.Rows.Count.ToString());
                int recordrow;
                recordrow = DG_Detail.Rows.Count;
                //DG_Detail.Rows.Contains(startingBalanceRow)
                if (recordrow > 1)
                {
                    // Do not allow the user to delete the Record Having Payment Transactions.
                    MessageBox.Show("This Record Having Payment Entries, First Remove the Payment Enteries and then Delete This Record!", "Payment Record Information Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Cancel the deletion if the Record having Payment Transaction.
                    e.Cancel = true;
                }
                else
                {
                    if (MessageBox.Show("Are You Sure to Delete This Work Entry?",
                                             "Work Entry Deletion Alert", MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, 0, false)
                                             == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        
        //-----------------------------------------

        //-----------------------------------------
        //Key & Click Events for Detail Data Grid
        //-----------------------------------------     

        private bool AddNewVendor(string vendorname)
        {
            if (vendorname == "System.Data.DataRowView")
            {
                return false;
            }

            bool result;
            newvendor = true;
            ArrayList fieldlist = new ArrayList();

            //Passwing property values to arraylist
            fieldlist.Add(vendorname);
            fieldlist.Add("Mr.");
            fieldlist.Add(0);
            result = PEObj.VendorRecord(fieldlist, "I");
            if (result == true)
            {

                SetupDetailDataGridView("EstimateNo", "EstDetail", "sp_GetPWRRecord", DG_Detail, "EstimateNo=" + PEObj.Estno + " and WorkNo=" + PEObj.Wrkno);
                foreach (DataRow mrow in EstDetail.Rows)
                {
                    payid = int.Parse(mrow["PayID"].ToString());
                }

                payid++;
                row = EstDetail.NewRow();
                EstDetail.Rows.Add(row);
                row["PayID"] = payid;
                if (payid == 1)
                {
                    payentry1 = true;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private void DG_Detail_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            ////if ((e.RowIndex != -1) && (e.ColumnIndex == -1))
            //if ((e.ColumnIndex == -1) || (e.ColumnIndex == 0))
            //{
            //    ToolTip tooltip1 = new ToolTip();
            //    //Rectangle rec = DG_Detail.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);        
            //    //Point p = rec.Location;        // add offset to move tip up        
            //    //p.Offset(0, -(50));        // changed dataGridView1 to panel1, use offset cell position, and timeout        
            //    tooltip1.Show("Double Click On Row Header To Open Purchase Order Form.", panel1, 10, 380, 2000); // show tool tip
            //}
        }

        private void DG_Detail_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int celno;
            int yCoord = DG_Detail.CurrentCellAddress.Y; // You can get X if you need it.
            celno = int.Parse(e.ColumnIndex.ToString());
            //MessageBox.Show("Cell No:" + e.ColumnIndex.ToString());  

            //PaidAmt
            //string checkpaid = DG_Detail.Rows[yCoord].Cells[7].Value.ToString();

            //if (float.Parse(checkpaid) > 0)
            //{
            //    DG_Detail.Rows[yCoord].Cells[0].ReadOnly = true;
            //    DG_Detail.Rows[yCoord].Cells[1].ReadOnly = true;
            //    DG_Detail.Rows[yCoord].Cells[2].ReadOnly = true;
            //    DG_Detail.Rows[yCoord].Cells[3].ReadOnly = true;
            //    DG_Detail.Rows[yCoord].Cells[4].ReadOnly = true;
            //    DG_Detail.Rows[yCoord].Cells[5].ReadOnly = true;
            //    DG_Detail.Rows[yCoord].Cells[6].ReadOnly = true;
            //    DG_Detail.Rows[yCoord].Cells[7].ReadOnly = true;
            //    DG_Detail.Rows[yCoord].Cells[8].ReadOnly = true;

            //    MessageBox.Show("This Record Having Paid Entries,Changes are Not Allowed!", "Payment Record Information Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    //return;
            //}

            if (celno == 2)
            {
                DG_Detail.Rows[yCoord].Cells[celno].ReadOnly = true;
                DG_Detail.Rows[yCoord].Cells[celno + 1].ReadOnly = true;
            }

            
        }

        void DG_Detail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

 
            int celno;
            try
            {
               
                celno = int.Parse(e.ColumnIndex.ToString());
                int yCoord = DG_Detail.CurrentCellAddress.Y; // You can get X if you need it.

                //PaidAmt
                string checkpaid = DG_Detail.Rows[yCoord].Cells["PaidAmt"].Value.ToString() == "" ? "0" : DG_Detail.Rows[yCoord].Cells["PaidAmt"].Value.ToString();

                if (float.Parse(checkpaid) > 0 && celno!=8)
                {
                    DG_Detail.Rows[yCoord].Cells[0].ReadOnly = true;
                    DG_Detail.Rows[yCoord].Cells[1].ReadOnly = true;
                    DG_Detail.Rows[yCoord].Cells[2].ReadOnly = true;
                    DG_Detail.Rows[yCoord].Cells[3].ReadOnly = true;
                    DG_Detail.Rows[yCoord].Cells[4].ReadOnly = true;
                    DG_Detail.Rows[yCoord].Cells[5].ReadOnly = true;
                    DG_Detail.Rows[yCoord].Cells[6].ReadOnly = true;
                    DG_Detail.Rows[yCoord].Cells[7].ReadOnly = true;
                  

                    MessageBox.Show("This Record Having Paid Entries,Changes are Not Allowed, Only Show On Report Check is Enable.", "Payment Record Information Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);

                   return;
                }

                DG_Detail.Rows[yCoord].Cells[8].ReadOnly = false;
                //ProjEntry.PWRNumber = int.Parse(txtProjID.Text);

                if (EstDetail.Rows.Count > 0)
                {
                    ProjEntry.PayNo = int.Parse(EstDetail.Rows[yCoord]["PayID"].ToString() == "" ? "0" : EstDetail.Rows[yCoord]["PayID"].ToString());
                }
 
                revertprocess = true;
                if (celno == 0 && newvendor==false)
                {
                    //if (DG_Detail.Rows[yCoord].Cells["VendorID"].GetType() == typeof(DataGridViewComboBoxCell))
                    //{
                    //    if (newvendor == true)
                    //    {
                    //        DG_Detail.Columns.RemoveAt(0);
                    //        DataGridViewComboBoxColumn columnv = new DataGridViewComboBoxColumn();
                    //        columnv = OBJFuncLib.combocolumn("VendorName", "VendorID", "Vendor", "");
                    //        DG_Detail.Columns.Add(columnv);
                    //        columnv.HeaderText = "Vendor Name";
                    //        columnv.Width = 320;
                    //        columnv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    //        columnv.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                    //        columnv.ReadOnly = true;
                    //        columnv.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                    //    }
                    //    newvendor = false;
                    //}
                    //------------------------------------------------------
                    if (newproj == false && DG_Detail.Rows[yCoord].Cells[celno].Value.ToString() != "")
                    {

                    PEObj.VendID = (int)DG_Detail.Rows[yCoord].Cells[celno].Value;

                    }
                    //------------------------------------------------------------------
                    //ProjEntry.PoVendor = PEObj.VendID;
                    //ProjEntry.WorkCost = double.Parse(DG_Detail.Rows[yCoord].Cells[1].Value.ToString()); 
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

            }

        }

        void DG_Detail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //
            // Do something on double click, except when on the header.
            //
            if (e.RowIndex == -1)
            {
                return;
            }
            //MessageBox.Show("Helloooooo");
            //ProceedOpen();  Purchase ORder Opening Form.
            int celno;
            try
            {
                //celno = int.Parse(e.ColumnIndex.ToString());
                celno = 0;

                int yCoord = DG_Detail.CurrentCellAddress.Y; // You can get X if you need it.


                string checkpaid = DG_Detail.Rows[yCoord].Cells["PaidAmt"].Value.ToString();

                if (float.Parse(checkpaid) > 0)
                {
                    DG_Detail.Rows[yCoord].Cells[0].ReadOnly = true;
                    DG_Detail.Rows[yCoord].Cells[1].ReadOnly = true;
                    DG_Detail.Rows[yCoord].Cells[2].ReadOnly = true;
                    DG_Detail.Rows[yCoord].Cells[3].ReadOnly = true;
                    DG_Detail.Rows[yCoord].Cells[4].ReadOnly = true;
                    DG_Detail.Rows[yCoord].Cells[5].ReadOnly = true;
                    DG_Detail.Rows[yCoord].Cells[6].ReadOnly = true;
                    DG_Detail.Rows[yCoord].Cells[7].ReadOnly = true;
                    DG_Detail.Rows[yCoord].Cells[8].ReadOnly = true;

                    MessageBox.Show("This Record Having Paid Entries,Changes are Not Allowed!", "Payment Record Information Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }

                ProjEntry.PWRNumber = int.Parse(txtProjID.Text);
                PEObj.VendID = (int)DG_Detail.Rows[yCoord].Cells[celno].Value;
                ProjEntry.POVendor = PEObj.VendID;
                ProjEntry.WorkCost = double.Parse(DG_Detail.Rows[yCoord].Cells[1].Value.ToString());

                ProjEntry.PayNo = int.Parse(EstDetail.Rows[yCoord]["PayID"].ToString());
                ProjEntry.WorkID = PEObj.Wrkno;
 

                frm_PurchaseOrder frmPOrder;
                frmPOrder = new frm_PurchaseOrder();
                //this.Close(); 
                frmPOrder.ShowDialog(); 
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.ToString());
            }
        }

        private void DG_Detail_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //int yCoord = DG_Detail.CurrentCellAddress.Y;
           // MessageBox.Show("Selected Value: " + DG_Detail.CurrentCell.Value.ToString());
            //MessageBox.Show("Selected Value: " + DG_Detail.Rows[yCoord].Cells[1].Value.ToString());
            ComboBox editingComboBox = e.Control as ComboBox;

            if (vndrname != null)
            {
                //editingComboBox.SelectedText = vndrname;  
                //editingComboBox.SelectedIndex = 11;
                return;
            }

            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                ((ComboBox)e.Control).AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;

            }
            try
            {
                if (editingComboBox != null)
                {
                    //wrongvendor = editingComboBox.Text;
                    this.DG_Detail.AllowUserToAddRows = false;
                    editingComboBox.SelectedIndexChanged += new System.EventHandler(this.editingComboBox_SelectedIndexChanged);
                    editingComboBox.PreviewKeyDown += new PreviewKeyDownEventHandler(editingComboBox_PreviewKeyDown);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void editingComboBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //throw new NotImplementedException();
            
            if (e.KeyCode == Keys.Enter)
            {
                if (DG_Detail.CurrentCell.ColumnIndex == 0)
                {
                    DG_Detail[1, DG_Detail.CurrentRow.Index].Selected = true;
                    DG_Detail.CurrentCell = DG_Detail[1, DG_Detail.CurrentRow.Index];
                }
               return;
            }
        }

        private void editingComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            
            ComboBox comboBox1 = (ComboBox)sender;
            string oldvendor = comboBox1.Text;
            if (oldvendor == vndrname)
            {
                return;
            }
            vndrname = oldvendor; 
            

            if (PEObj.DLookup("VendorName", "Vendor", "VendorName='" + oldvendor + "'") == oldvendor)
            {
                cmbid = int.Parse(PEObj.DLookup("VendorID", "Vendor", "VendorName='" + oldvendor + "'"));
                //comboBox1.SelectedText = vndrname;  
                 
                newvendor = false;
            }
            else
            {
                newvendor = true;
                MessageBox.Show("Vendor " + comboBox1.Text + " Not Found.....");
                if (MessageBox.Show("Do You Want to ADD New Vendor?", "New Vendor Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, 0, false) == DialogResult.Yes)
                {
                    if (AddNewVendor(comboBox1.Text) == true)
                    {
                        //comboBox1 = OBJFuncLib.FillCombo("VendorName", "VendorID", comboBox1, "Vendor", ""); 

                        MessageBox.Show("New Vendor Successfully Added....");
                    }
                }
            }
            comboBox1.SelectedValue = cmbid; 
            //comboBox1.SelectedIndex = 11; 
    

        }

        private void DG_Detail_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Update the balance column whenever the value of any cell changes.
            //MessageBox.Show(e.ColumnIndex.ToString());     
            try
            {
                if (DG_Detail.Rows.Count != 0)
                {
                    if (editstart == true)
                    {

                        this.DG_Detail.AllowUserToAddRows = false;
                        //if (this.DG_Detail.Columns[e.ColumnIndex].Name == "GrossAmt" || this.DG_Detail.Columns[e.ColumnIndex].Name == "PayRecmd" || this.DG_Detail.Columns[e.ColumnIndex].Name == "Discount")
                        if (this.DG_Detail.Columns[e.ColumnIndex].Name == "PayRecmd")

                        //if (this.DG_Detail.CurrentCell.OwningColumn.Name.ToString() == "GrossAmt" || this.DG_Detail.CurrentCell.OwningColumn.Name.ToString() == "PayRecmd" || this.DG_Detail.CurrentCell.OwningColumn.Name.ToString() == "Discount")
                        {
                            showtotal();
                        
                        }
                        if (this.DG_Detail.Columns[e.ColumnIndex].Name == "PayDetail")
                        {
                            //SendKeys.Send("{UP}");
                            //SendKeys.Send("{HOME}");
                            this.DG_Detail.AllowUserToAddRows = false;
                            
                        }
                        //int colid = int.Parse(e.ColumnIndex.ToString());
                        //if (colid == 6)
                        //{
                        //    // this.DG_Detail.Columns[10].Selected = true;
                        //    //this.DG_Detail.Columns[2].Frozen = true;
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);  
            }
        }

        private void DG_Detail_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            // Update the balance column whenever rows are deleted.
            //showtotal();
        }

        private void DG_Detail_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //throw new System.NotImplementedException();

            DG_Detail.UserDeletingRow += new DataGridViewRowCancelEventHandler(DG_Detail_UserDeletingRow); 
        }

        private void DG_Detail_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
           //throw new NotImplementedException();
            //e.Cancel = false;

            if (DG_Detail.Rows.Count > 0)
            {
                string pwork = null;
                pwork = PEObj.DLookup("Payid", "Payments", "Estimateno=" + PEObj.Estno + " and Workno=" + PEObj.Wrkno + " and payid=" + ProjEntry.PayNo);

                if (pwork == null)
                {
                    pwork = "0";
                }

                if (int.Parse(pwork) > 0)
                {
                    // Do not allow the user to delete the Record Having Payment Transactions.
                    MessageBox.Show("This Record Having Payment Entries at Finance, Deletion of This Record Is Not Allowed!", "Payment Record Information Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // Cancel the deletion if the Record having Payment Transaction.
                    e.Cancel = true;
                }
                else
                {
                    if (MessageBox.Show("Are You Sure to Delete This Payment Entry?",
                                             "Payment Entry Deletion Alert", MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, 0, false)
                                             == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        e.Cancel = false; 
                    }
                }
            }



        }


        private void DG_Detail_SelectionChanged(object sender, EventArgs e)
        {
            // Update the labels to reflect changes to the selection.
            //UpdateLabelText();
        }

        private void DG_Detail_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            // Update the labels to reflect changes to the number of entries.
            //UpdateLabelText();

            if (fDeleteRow)
            {
                try
                {
                    adapterDetail.Update(EstDetail); 
                    //sql_DA_EQUPS.Update(dsEQUPS, dsEQUPS.EQU_PS.TableName);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                fDeleteRow = false;
            }
            justclick = false; 
        }

        private void DG_Detail_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText = DG_Detail.Columns[e.ColumnIndex].HeaderText;
            if (!headerText.Equals("Gross Amount") && !headerText.Equals("PayRecmd"))
            {
                return;
            }
 
            try
            {
                
                double grsamt = 0;
                double recdamt = 0;
                grsamt = double.Parse(txtGrossAmt.Text);

                if (headerText.Equals("Gross Amount"))
                {
                    //int.Parse(e.FormattedValue.ToString())
                    grsamt = double.Parse(e.FormattedValue.ToString());

                   

                    if (grsamt > workgrossamt)
                    {
                        DG_Detail.Rows[e.RowIndex].Cells["GrossAmt"].Value = workgrossamt;
                        e.Cancel = true;
 
                            MessageBox.Show("Gross Amount Should Not Be Greater Than Approved Cost of Work!", "Wrong Amount Entered", MessageBoxButtons.OK, MessageBoxIcon.Information);
 
                        return;
                    }
                }
                else
                {
                    recdamt = double.Parse(e.FormattedValue.ToString());
                    if (recdamt > grsamt)
                    {
                        DG_Detail.Rows[e.RowIndex].Cells["PayRecmd"].Value = "0";
                        e.Cancel = true;
                   
 
                        MessageBox.Show("Recommended Payment Should be Less Than Or Equanl To Gross Amount!", "Wrong Recommendation Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
 
                        return;
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }



            //int testint;

            //if (e.ColumnIndex != 0)
            //{
            //    if (e.FormattedValue.ToString().Length != 0)
            //    {
            //        // Try to convert the cell value to an int.
            //        if (!int.TryParse(e.FormattedValue.ToString(), out testint))
            //        {
            //            DG_Detail.Rows[e.RowIndex].ErrorText = "Error: Invalid entry";
            //            e.Cancel = true;
            //        }
            //    }
            //}
        }

        private void DG_Detail_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            // Clear any error messages that may have been set in cell validation.
            //DG_Detail.Rows[e.RowIndex].ErrorText = null;
            int celno;
            try
            {
                celno = int.Parse(e.ColumnIndex.ToString());
                int yCoord = DG_Detail.CurrentCellAddress.Y; // You can get X if you need it.

                //ProjEntry.PWRNumber = int.Parse(txtProjID.Text);

                if (celno == 0)
                {
                    PEObj.VendID = (int)DG_Detail.Rows[yCoord].Cells[celno].Value;
                    //ProjEntry.PoVendor = PEObj.VendID;
                    //ProjEntry.WorkCost = double.Parse(DG_Detail.Rows[yCoord].Cells[1].Value.ToString()); 
                }

            }
            catch (Exception ex)
            {
              MessageBox.Show(ex.ToString());
            }
            finally
            {

            }

        }

       
        //void DG_Detail_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        //{
        //    //throw new NotImplementedException();
        //}

        //private void DG_Detail_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        //{
        //    if (MessageBox.Show("Are You Sure to Delete This Work Entry?",
        //                 "Payment Entry Deletion Alert", MessageBoxButtons.YesNo,
        //                 MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, 0, false)
        //                 == DialogResult.No)
        //    {
        //        e.Cancel = true;
        //        if (DG_Detail.RowCount != 0)
        //        {
        //            int totrow = int.Parse(DG_Detail.RowCount.ToString()) - 1;
        //            DG_Detail.Rows[totrow].Selected = true;    
    
        //        }
        //    }
        //    else
        //    {
        //        e.Cancel = false;
        //    }
        //    return;
        //}



        private void btnRptPayRequest_Click(object sender, EventArgs e)
        {

            try
            {
                //SendEmail();
                PostPaymentTable();
                //Update Current Estimate No into the Mastpara Table to Link the Report.
                sqlMastPara = "Update MastPara Set PrintEstno=" + PEObj.Estno;
                command = new SqlCommand(sqlMastPara, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                ProjEntry.PWRNumber = PEObj.Estno;
                frm_ReportViewer Reportform;
                


                if (MessageBox.Show("Do You Want Vendor Group Wise Report?",
                             "Vendor Group Report Alert", MessageBoxButtons.YesNo,
                             MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, 0, false)
                             == DialogResult.Yes)
                {

                    
                    ProjEntry.Rptname = "Rpt_ProjEstSummary.rdlc";

                }
                else
                {
                 
                    
                    ProjEntry.Rptname = "Rpt_ProjEstSumm.rdlc";
     
               }

                
                
                if (MessageBox.Show("Do You Want Full Detail Payment Report?", "Full Payment Report Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, 0, false) == DialogResult.Yes)
                {
                    Reportform = new frm_ReportViewer(true);
                }
                else
                {
                    Reportform = new frm_ReportViewer(false);
                }   


                Reportform.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                //connection.Close(); 
            }


        }

        private void SendEmail()
        {
          //  return;

            // Command line argument must the the SMTP host.
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("erpemail@fps.edu.pk", "K@r@chi123*");
             

                //DeliveryMethod = SmtpDeliveryMethod.Network,
                //Credentials = new System.Net.NetworkCredential(pGmailEmail, pGmailPassword),
                //Timeout = 300000,

            // Specify the e-mail sender.
            // Create a mailing address that includes a UTF8 character
            // in the display name.
            //MailAddress from = new MailAddress("workorder@fps.edu.pk", "FPS/HSS " + (char)0xD8 + "Work Order System", System.Text.Encoding.UTF8);
            MailAddress from = new MailAddress(Login.CuserEmail, "FPS/HSS Work Order System", System.Text.Encoding.UTF8);
            
            // Set destinations for the e-mail message.
            MailAddress to = new MailAddress("salmanshafi@fps.edu.pk");
            //MailAddress to = new MailAddress("basitbaig@fps.edu.pk");
            
            // Specify the message content.
            MailMessage message = new MailMessage(from, to);
            message.Body = "A New Payment Request No :" + "PWR/" + PEObj.Branchid + "-" + PEObj.Estno.ToString();
            message.Body += Environment.NewLine + "Description :" + PEObj.Desc;
            message.Body += Environment.NewLine + "From :" + Login.Cuser;
            message.Body += Environment.NewLine + "Have Been Posted,Please Acknowledge!";
            message.Body += Environment.NewLine + " ";
            message.Body += Environment.NewLine + "Thank You.";
           
            // Include some non-ASCII characters in body and subject.
            string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
            message.Body += Environment.NewLine + someArrows;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = "Work Order/Payment Request - " + "PWR/" + PEObj.Sysid + "-" + PEObj.Estno.ToString();
            message.To.Add("basitbaig@fps.edu.pk");
            //message.To.Add("zameer@fps.edu.pk");

            //----------------- File Attachment -------------------
            //string file = "D:/tms.xls";

            //// Create  the file attachment for this e-mail message.
            ////Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);

            //Attachment data = new Attachment(file);
            //// Add time stamp information for the file.
            //ContentDisposition disposition = data.ContentDisposition;
            //disposition.CreationDate = System.IO.File.GetCreationTime(file);
            //disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
            //disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
            //// Add the file attachment to this e-mail message.
            //message.Attachments.Add(data);

            //-----------------------------------------------------

            //message.SubjectEncoding = System.Text.Encoding.UTF8;
            // Set the method that is called back when the send operation ends.
            //client.SendCompleted += new SendCompletedEventHandler(client_SendCompleted);
            
            //new SendCompletedEventHandler(SendCompletedCallback);
            // The userState can be any object that allows your callback 
            // method to identify this send operation.
            // For this example, the userToken is a string constant.
            //string userState = "test message1";
            client.Send(message);
            //client.SendAsync(message, userState);
            //Console.WriteLine("Sending message... press c to cancel mail. Press any other key to exit.");
            //string answer = Console.ReadLine();
            // If the user canceled the send, and mail hasn't been sent yet,
            // then cancel the pending operation.
            //if (answer.StartsWith("c") && mailSent == false)
            //{
            //    client.SendAsyncCancel();
            //}
            // Clean up.
            message.Dispose();
            //Console.WriteLine("Goodbye.");

        
        }

        private void PostPaymentTable()
        {
            if (MessageBox.Show("Do You Want to Post Recommended Payments to Finance Department?",
                         "Payment Posting Alert", MessageBoxButtons.YesNo,
                         MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, 0, false)
                         == DialogResult.Yes)
            {
                int payid = 0;
                int vdrid = 0;
                double payrc = 0;
                DateTime dudat = DateTime.Now;
                string billinvo = "";
                string paydetl = "";
                bool result = false;

                string commandString = "Select * from EstDetail where " + "EstimateNo=" + PEObj.Estno + " and PayRecmd!=0"; 

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter = new SqlDataAdapter(commandString, connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                DataTable t = dataSet.Tables[0];
                //MessageBox.Show(t.Rows.Count.ToString());
                //MessageBox.Show(EstDetail.Rows.Count.ToString());   
                foreach (DataRow row in t.Rows)
                {
                    payid = int.Parse(row["PayID"].ToString());
                    vdrid = int.Parse(row["VendorID"].ToString());
                    payrc = double.Parse(row["PayRecmd"].ToString());
                    dudat = DateTime.Parse(row["DueDate"].ToString());
                    billinvo = row["BillInvoice"].ToString();
                    paydetl = row["PayDetail"].ToString();
                    PEObj.Wrkno = int.Parse(row["WorkNo"].ToString());

                    
                    ArrayList fieldlist = new ArrayList();

                        //@estno as int,
                        //@wrkno as int,
                        //@payid as int,
                        //@entdt as datetime,
                        //@vdrid as int,
                        //@pyrcmd as money,
                        //@duedate as datetime,
                        //@paydetail as varchar(255),
                        //@userid as int
                    

                    //Passwing property values to arraylist
                    fieldlist.Add(PEObj.Estno);
                    fieldlist.Add(PEObj.Wrkno);
                    fieldlist.Add(payid);
                    fieldlist.Add(vdrid);
                    fieldlist.Add(payrc);
                    fieldlist.Add(dudat);
                    fieldlist.Add(paydetl);
                    fieldlist.Add(Login.Cuserid);

                    PEObj.PayID = payid;

                    string matchval = PEObj.DLookup("PayRecmd", "Payments", "EstimateNo=" + PEObj.Estno + " and WorkNo=" + PEObj.Wrkno + " and PayID=" + PEObj.PayID + " and PayRecmd=" + payrc);
                    //string matchval = PEObj.DLookup("PayRecmd", "Payments", "EstimateNo=" + PEObj.Estno + " and WorkNo=" + PEObj.Wrkno + " and PayID=" + PEObj.PayID);
                    //double payr = double.Parse(matchval);
                    //double payr = double.Parse(PEObj.DLookup("PayRecmd", "Payments", "EstimateNo=" + PEObj.Estno + " and WorkNo=" + PEObj.Wrkno + " and PayID=" + payid));

                    
                    if (matchval!=null)
                    {
                        DialogResult MsgResult = MessageBox.Show("This Work & Payment No:" + PEObj.Wrkno.ToString() + " - " + PEObj.PayID.ToString() + " Already Posted to Finance!, If You Want to Add New Payment Press YES, To Update Existing Payment Press NO and To Bypass This Payment Entry Press Cancel.",
                        "Payment Posting Alert", MessageBoxButtons.YesNoCancel, 
                        MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, 0, false);

                        if (MsgResult == DialogResult.Yes)
                        {
                            fieldlist.Add("I");
                            result = PEObj.PaymentRecord(fieldlist, "I");
                        }
                        if (MsgResult == DialogResult.No)
                        {
                            fieldlist.Add("U");
                            result = PEObj.PaymentRecord(fieldlist, "U");
                        }
                        if (MsgResult == DialogResult.Cancel)
                        {
                            result = false;
                        }

                    }
                    else
                    {
                        fieldlist.Add("I");
                        result = PEObj.PaymentRecord(fieldlist, "I");

                 //       DialogResult MsgResult = MessageBox.Show("This Work Payment Already Posted to Finance!, If You Want to Add New Payment Press YES, To Update Existing Payment Press NO!",
                 //"Payment Posting Alert", MessageBoxButtons.YesNoCancel,
                 //MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, 0, false);

                 //       if (MsgResult == DialogResult.Yes)
                 //       {
                 //           fieldlist.Add("I");
                 //           result = PEObj.PaymentRecord(fieldlist, "I");
                 //       }
                 //       if (MsgResult == DialogResult.No)
                 //       {
                 //           fieldlist.Add("U");
                 //           result = PEObj.PaymentRecord(fieldlist, "U");
                 //       }
                 //       if (MsgResult == DialogResult.Cancel)
                 //       {
                 //           result = false;
                 //       }

                    }
                    
                }
                if (result == true)
                {
                    MessageBox.Show("Recommended Payment Successfully Posted to Finance for Payments");
                    SendEmail();
                }
                else
                {
                    MessageBox.Show("Recommended Payment Posting is Failed,Please Check and Try to Post Payment Again");
                }
                
            }
            else
            {

            }
            return;
    
        }

        private void btnEstimateReport_Click(object sender, EventArgs e)
        {
            //Reportform.MdiParent = this.MdiParent; 
            //this.Close();
            //Update Current Estimate No into the Mastpara Table to Link the Report.
            try
            {
                sqlMastPara = "Update MastPara Set PrintEstno=" + PEObj.Estno;
                command = new SqlCommand(sqlMastPara, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close(); 
                ProjEntry.Rptname = "Rpt_ProjEstimate.rdlc";
                frm_ReportViewer Reportform;
                Reportform = new frm_ReportViewer(false);
                Reportform.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                //connection.Close(); 
            }
        }

        private void cbobranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbobranch.SelectedIndex > -1)
            {
                string cbobrid = cbobranch.SelectedValue.ToString();
               // brid = int.Parse(cbobranch.SelectedIndex.ToString());
                if (cbobrid != "System.Data.DataRowView")
                {
                    cbobranch.SelectedValue = int.Parse(cbobrid.ToString());
                    this.txtbranchID.Text = PEObj.DLookup("BranchID", "Branches", "BrnID=" + int.Parse(cbobrid.ToString()));
                    PEObj.Branchid = this.txtbranchID.Text;
                }
            }
            this.txtDesc.Focus();  

        }

        private void btnPOform_Click(object sender, EventArgs e)
        {


        }

        void DG_Detail_KeyDown(object sender, KeyEventArgs e)
        {
                 //PayDetail

        }

        void DG_Detail_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //throw new NotImplementedException();
            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (DG_Detail.CurrentCell.ColumnIndex == 0)
            //    {
            //        DG_Detail[1, DG_Detail.CurrentRow.Index].Selected = true;
            //        DG_Detail.CurrentCell = DG_Detail[1, DG_Detail.CurrentRow.Index];
            //    }
            //    if (DG_Detail.CurrentCell.ColumnIndex == 1)
            //    {
            //        DG_Detail[4, DG_Detail.CurrentRow.Index].Selected = true;
            //        DG_Detail.CurrentCell = DG_Detail[4, DG_Detail.CurrentRow.Index];
            //    }
            //    if (DG_Detail.CurrentCell.ColumnIndex == 4)
            //    {
            //        DG_Detail[5, DG_Detail.CurrentRow.Index].Selected = true;
            //        DG_Detail.CurrentCell = DG_Detail[5, DG_Detail.CurrentRow.Index];
            //    }
            //}
        }

        private void btnAddVendor_Click(object sender, EventArgs e)
        {
            //To Add New Vendor In the Vendor Table and Refresh the Combo.

            if (this.txtNewVendor.Text.Trim() != "")
            {

                if (MessageBox.Show("ARE YOU SURE TO ADD NEW VENDOR?", "NEW VENDOR Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, 0, false) == DialogResult.Yes)
                {

                    string sqlNewVendor = "Insert Into Vendor (VendorName) Values ('" + this.txtNewVendor.Text.Trim().ToString().Replace("'", "''") + "')";
                    command = new SqlCommand(sqlNewVendor, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Vendor Successfully Add Into The Database", "New Item");
                    this.txtNewVendor.Enabled = false;
                    newvendor = true;
                    this.txtNewVendor.Text = "";
                }
            }
            else 
            {
                
                this.txtNewVendor.Enabled = true;
            
            }

        }

        private void txtNewVendor_TextChanged(object sender, EventArgs e)
        {
            this.btnAddVendor.Enabled = true;  
        }

        private void DG_Master_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == -1)
            {
                return;
            }

            if (this.DG_Master.Columns[e.ColumnIndex].Name == "Paid")
            {

                if (e.Value != null)
                {
                    // Check for the string "pink" in the cell.
                    //string stringValue = (string)e.Value;
                    //stringValue = stringValue.ToLower();
                    //if ((stringValue.IndexOf("pink") > -1))
                    //{
                    e.CellStyle.BackColor = Color.Green;
                    e.CellStyle.ForeColor = Color.White;

                    //}

                }
            }
        }

        private void DG_Detail_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
 
            //PayDetail
            if (e.ColumnIndex != -1)
            {

                if (this.DG_Detail.Columns[e.ColumnIndex].Name == "PaidAmt")
                {
                    if (e.Value != null)
                    {
                        // Check for the string "pink" in the cell.
                        //string stringValue = (string)e.Value;
                        //stringValue = stringValue.ToLower();
                        //if ((stringValue.IndexOf("pink") > -1))
                        //{
                        e.CellStyle.BackColor = Color.Green;
                        e.CellStyle.ForeColor = Color.White;
                        //}
                    }
                }
                if (this.DG_Detail.Columns[e.ColumnIndex].Name == "BalAmt")
                {
                    if (e.Value != null)
                    {
                        // Check for the string "pink" in the cell.
                        //string stringValue = (string)e.Value;
                        //stringValue = stringValue.ToLower();
                        //if ((stringValue.IndexOf("pink") > -1))
                        //{
                        e.CellStyle.BackColor = Color.Red;
                        e.CellStyle.ForeColor = Color.White;
                        //}
                    }
                }
            }

        }

        private void DG_Detail_Leave(object sender, EventArgs e)
        {
            if (editstart == true && justclick==false)
            {
                btnSave_Click(sender, e);
                // MessageBox.Show("Please Click Save/Update Button To Save This Record!", "Record Saving Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);   
                return;
            }
            
           return;

            //if (MessageBox.Show("Do You Want Save This Record?", "Record Save Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, 0, false) == DialogResult.Yes)
            //{
                //editstart = false;

                //if(newproj == true)
                //{
                //    sqlMastPara = "Update MastPara Set PrintEstno=" + PEObj.Estno;
                //    command = new SqlCommand(sqlMastPara, connection);
                //    connection.Open();
                //    command.ExecuteNonQuery();
                //    connection.Close();

                //    try
                //    {

                //        this.Validate();
                //        this.BindSource.EndEdit();
                //        this.BindNav.Update();

                //        PostMainTable("I");
                //        PostMasterTable();
                //        PostDetailTable();
                //        //PostPaymentTable();

                //        //MessageBox.Show("Record Has Been Successfully Saved!");
                //        newproj = false;
                //        editstart = true;
                //        saveonce = true;
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show(ex.ToString());
                //    }
                //}
                //else
                //{
                //    try
                //    {
                //        this.Validate();
                //        this.BindSource.EndEdit();
                //        this.BindNav.Update();
                //        //adapterMain.Update(EstMain);

                //        //CHeck if New Entry of Work is Made on Existing Work Order, Then Insert Process will Run
                //        if (workentry1 == true)
                //        {
                //            PostMasterTable();
                //        }
                //        else
                //        {
                //            //row.EndEdit();
                //            //Update Master Table
                //            adapterMast.UpdateCommand = builderMast.GetUpdateCommand();
                //           // adapterMast.Update(EstMaster);
                //        }
                //        //CHeck if New Entry of Payment is Made on Existing Work Order, Then Insert Process will Run
                //        if (payentry1 == true)
                //        {
                //           PostDetailTable();
                //        }
                //        else
                //        {
                //            //Update Detail Table
                //            adapterDetail.UpdateCommand = builderDetail.GetUpdateCommand();
                //            adapterDetail.Update(EstDetail);
                //        }

                //        DateTime wodate = DateTime.Parse(entdate.Text); 

                //        PEObj.Branchid = this.txtbranchID.Text;
                //        PEObj.Desc = this.txtDesc.Text;
                //        PEObj.Entdate = wodate.ToString("MM/dd/yyyy");
                //        PEObj.Projtypeid = int.Parse(this.cboProjType.SelectedValue.ToString());
                //        PEObj.Sysid = cboWorkDomain.SelectedValue.ToString();
                //        PEObj.Acdyear = this.txtacdyear.Text;
                //        PEObj.Usrid = Login.Cuserid;

                //        //MessageBox.Show("Record Has Been Successfully Updated!");
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show(ex.ToString());
                //    }
                //}
                //this.btnRptPayRequest.Enabled = true;
                //this.btnEstimateReport.Enabled = true;
                //workentry1=false;
            //}

        }

        private void cboWorkDomain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (newproj == true)
            {
                if (cboWorkDomain.Text == "FPS")
                {
                    lblTitle.Text = "Foundation Public School";
                    lblTitle.Refresh();
                }
                else if (cboWorkDomain.Text == "HSS")
                {
                    lblTitle.Text = "Head Start School System";
                    lblTitle.Refresh();
                }
                else
                {
                    lblTitle.Text = "Untitled Project";
                    lblTitle.Refresh();
                }
                this.cbobranch.DroppedDown = true;
                this.cbobranch.Focus();
            }
            else
            {

                if (cboWorkDomain.Text == "FPS")
                {
                    lblTitle.Text = "Foundation Public School";
                    lblTitle.Refresh();
                }
                else if (cboWorkDomain.Text == "HSS")
                {
                    lblTitle.Text = "Head Start School System";
                    lblTitle.Refresh();
                }
                else
                {
                    lblTitle.Text = "Untitled Project";
                    lblTitle.Refresh();
                }

            }
        }

        private void txtDesc_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnUpdate.Focus();
                this.btnUpdate.BackColor = Color.Green;   
            }
 
        }

        private void DG_Detail_Enter(object sender, EventArgs e)
        {
            if (DG_Detail.Rows.Count == 0)
            {
                return;
            }
            int celno;
            int yCoord = DG_Detail.CurrentCellAddress.Y; // You can get X if you need it.

            if (yCoord==-1 )
            {
                return;
            }

            celno = int.Parse(DG_Detail.CurrentCell.ColumnIndex.ToString());   
            
            if (celno == 0 && newvendor == true)
            {

                if (DG_Detail.Rows[yCoord].Cells["VendorID"].GetType() == typeof(DataGridViewComboBoxCell))
                {
                    DG_Detail.Columns.Remove("VendorID");
                    DataGridViewComboBoxColumn columnv = new DataGridViewComboBoxColumn();
                    columnv = OBJFuncLib.combocolumn("VendorName", "VendorID", "Vendor", "");
                    //DG_Detail.Columns.Add(columnv);
                    DG_Detail.Columns.Insert(0, columnv);     
                    columnv.Name = "VendorID";
                    columnv.HeaderText = "Vendor Name";
                    columnv.Width = 320;
                    columnv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    columnv.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                    columnv.ReadOnly = false;
                    columnv.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                }
                newvendor = true; 
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure To Cancel Work Order No:" + PEObj.Estno.ToString() + " of " + txtDesc.Text.TrimEnd()  + "?", "Work Order Cancellation Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, 0, false) == DialogResult.Yes)
            {
                string sqlstring;
                    sqlstring = "Update EstMain Set IsCancelled=1 Where EstimateNo=" + PEObj.Estno;
                    command = new SqlCommand(sqlstring, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    sqlstring = "Update EstMaster Set IsCancelled=1 Where EstimateNo=" + PEObj.Estno;
                    command = new SqlCommand(sqlstring, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    sqlstring = "Update EstDetail Set IsCancelled=1 Where EstimateNo=" + PEObj.Estno;
                    command = new SqlCommand(sqlstring, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    sqlstring = "Update Payments Set IsCancelled=1 Where EstimateNo=" + PEObj.Estno;
                    command = new SqlCommand(sqlstring, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();


                    MessageBox.Show("Work/Project Order Has Been Successfully Cancelled!!", "WorkOrder Cancellation");

            }
        }

        private void txtNewVendor_DoubleClick(object sender, EventArgs e)
        {
            txtNewVendor.Enabled = true; 
        }

        void DG_Detail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //throw new NotImplementedException();

            try
            {

                if (e.RowIndex!=0)
                {

                DG_Detail.Rows[e.RowIndex].ErrorText = string.Empty;

                float apcost = float.Parse(DG_Master.Rows[DG_Master.CurrentCell.RowIndex].Cells["Cost"].Value.ToString());

                float grospmt = float.Parse(DG_Detail.Rows[DG_Detail.CurrentCell.RowIndex].Cells["GrossAmt"].Value.ToString() == "" ? "0" : DG_Detail.Rows[DG_Detail.CurrentCell.RowIndex].Cells["GrossAmt"].Value.ToString());

                if (grospmt > apcost && newvendor == true)
                {
                    ((DataGridView)sender).Rows[e.RowIndex].Selected = true;
                    ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value = apcost;
                    MessageBox.Show("Gross Amount Should Not Be Greater Than Approved Cost/Amount!", "Wrong Amount Entered");
                }

                }
                //int grosamt = int.Parse(DG_Detail.Rows[DG_Detail.CurrentCell.RowIndex].Cells["GrossAmt"].Value.ToString() == null ? "0" : DG_Detail.Rows[DG_Detail.CurrentCell.RowIndex].Cells["GrossAmt"].Value.ToString());
                //int amtrecmd = int.Parse(DG_Detail.Rows[DG_Detail.CurrentCell.RowIndex].Cells["amtrecmd"].Value.ToString());

                //if (amtrecmd > grosamt)
                //{
                //    MessageBox.Show("Recommended Amount Should Not Be Greater Than Gross Amount!", "Wrong Amount Entered");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }      
        
        
        
        private void DG_Detail_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
         
            //if (int.Parse(DG_Detail.Rows[DG_Detail.CurrentCell.RowIndex].Cells["GrossAmt"] ==null ? "0" : DG_Detail.Rows[DG_Detail.CurrentCell.RowIndex].Cells["GrossAmt"].Value.ToString())!=0)
            //{
            
            //    int grosamt = int.Parse(DG_Detail.Rows[DG_Detail.CurrentCell.RowIndex].Cells["GrossAmt"].Value.ToString()==null ? "0" : DG_Detail.Rows[DG_Detail.CurrentCell.RowIndex].Cells["GrossAmt"].Value.ToString());
            //    int amtrecmd = int.Parse(DG_Detail.Rows[DG_Detail.CurrentCell.RowIndex].Cells["amtrecmd"].Value.ToString());

            //    if (amtrecmd > grosamt)
            //    {
            //        MessageBox.Show("Recommended Amount Should Not Be Greater Than Gross Amount!", "Wrong Amount Entered");
            //    }
            //}
        }

        private void DG_Detail_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (DG_Detail[e.ColumnIndex, e.RowIndex].ColumnIndex == 5)
            {
                DG_Detail.EndEdit();
                try
                {
                    string grosamt = DG_Detail.Rows[DG_Detail.CurrentCell.RowIndex].Cells["GrossAmt"].Value.ToString();
                    string amtrecmd = DG_Detail.Rows[DG_Detail.CurrentCell.RowIndex].Cells["payrecmd"].Value.ToString();

                    if (int.Parse(amtrecmd) > int.Parse(grosamt, NumberStyles.AllowDecimalPoint))
                    {
                        MessageBox.Show("Recommended Amount Should Not Be Greater Than Gross Amount!", "Wrong Amount Entered");

                        DG_Detail[DG_Detail.CurrentCell.ColumnIndex, DG_Detail.CurrentRow.Index].Value = grosamt;
                    }
                }
                catch
                {
                    return; 
                }

                //if (DG_Detail[e.ColumnIndex, e.RowIndex].Value == null)//incase it is a blanked cell
                //{
                //    MessageBox.Show("Cell changed:" + e.ColumnIndex.ToString() + ":" + e.RowIndex.ToString() + " New Value:" + " null");

                //}
                //else
                //{
                //    MessageBox.Show("Cell changed:" + e.ColumnIndex.ToString() + ":" + e.RowIndex.ToString() + " New Value:" + DG_Detail[e.ColumnIndex, e.RowIndex].Value.ToString());
                //}

            }
        }

        private void btnAddPayment_Click(object sender, EventArgs e)
        {

            try
            {
                //SendEmail();
                PostPaymentTable();
                //Update Current Estimate No into the Mastpara Table to Link the Report.
                sqlMastPara = "Update MastPara Set PrintEstno=" + PEObj.Estno;
                command = new SqlCommand(sqlMastPara, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                ProjEntry.PWRNumber = PEObj.Estno;
                frmTrdxReports Reportform;


                ProjEntry.Rptname = System.IO.Path.GetFullPath("TRDX\\ProjectEstimate.trdx");
                //,txtProjID.Text
                Reportform = new frmTrdxReports(ProjEntry.Rptname);

                Reportform.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                //connection.Close(); 
            }




        }

        private void btnToglRecmd_Click(object sender, EventArgs e)
        {

            try
            {
                if (btnToglRecmd.Text == "Hide Recommendations")
                {
                    sqlMastPara = "Update EstDetail Set ShowonRpt=0 where EstimateNo=" + PEObj.Estno;
                    command = new SqlCommand(sqlMastPara, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    btnToglRecmd.Text = "Show Recommendations";
                }
                else
                {
                    sqlMastPara = "Update EstDetail Set ShowonRpt=1 where EstimateNo=" + PEObj.Estno + " AND PayRecmd > 0";
                    command = new SqlCommand(sqlMastPara, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    btnToglRecmd.Text = "Hide Recommendations";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                //connection.Close(); 
            }


        }

        private void cmbAsgnUser_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbAsgnUser.SelectedIndex <= 0)
            {
                return;
            }

            try
            {
                if (Login.Cuserid == 95 || Login.Cuserid == 113)
                {
                  
                    
                    
                    int cboval = int.Parse(cmbAsgnUser.SelectedValue.ToString());

                    if (MessageBox.Show("Do You Want To Assign This Project/Work To " + cmbAsgnUser.Text + " ?",
                         "Project/Work ReAssign Alert", MessageBoxButtons.YesNo,
                         MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, 0, false)
                         == DialogResult.Yes)
                    {
                        ///Update Query
                        //txtAssignProjID
                        //EstMain  ---  UsrID  -- EstimateNo
                        //EstMaster -- EstimateNo
                        //EstDetail  -- EstimateNo
                        //Payments -- UsrID  -- EstimateNo

                        if (txtAssignProjID.ToString() == "")
                        {
                            MessageBox.Show("Please Enter Project / Work No To Be Re-Assigned!");
                        }
                        else
                        {

                            string sqlMastPara1 = "Update EstMain Set UsrID='" + cboval.ToString() + "' where EstimateNo='" + txtAssignProjID.ToString() + "'";
                            //string sqlMastPara2 = "Update EstMaster Set ShowonRpt=0 where EstimateNo=" + PEObj.Estno;
                            //string sqlMastPara3 = "Update EstDetail Set ShowonRpt=0 where EstimateNo=" + PEObj.Estno;
                            string sqlMastPara2 = "Update Payments Set UsrID='" + cboval.ToString() + "' where EstimateNo='" + txtAssignProjID.ToString() + "'";

                            connection.Open();
                            command = new SqlCommand(sqlMastPara1, connection);
                            command.ExecuteNonQuery();
                            command = new SqlCommand(sqlMastPara2, connection);
                            command.ExecuteNonQuery();
                            connection.Close();


                            MessageBox.Show("Project/Work Successfully Assign to " + cmbAsgnUser.Text + "!");
                        }

                    }
                    else
                    {
                        txtAssignProjID.Text = "";
                        cmbAsgnUser.SelectedIndex = 0;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
    


        }

        private void DG_Master_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {

         //   DG_Master.Rows[e.RowIndex].ErrorText = "Concisely describe the error and how to fix it";
           // e.Cancel = true;
        }

 


        //------------------------------------------
    }


    public class MyDataGridView : DataGridView
    {
        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ProcessTabKey(e.KeyData);
                return true;
            }
            return base.ProcessDataGridViewKey(e);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                this.ProcessTabKey(keyData);
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
    }

}
