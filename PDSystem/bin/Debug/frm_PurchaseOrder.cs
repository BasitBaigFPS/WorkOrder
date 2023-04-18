using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;  
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using WordAmount;



namespace PDSystem
{
    public partial class frm_PurchaseOrder : Form
    {
        
        PurchaseOrder POObj = new PurchaseOrder(); 
        AmountInWords rupeeword = new AmountInWords();

        HOIP.IP howebref = new HOIP.IP();
        ODManager.GRWService ODMObj = new ODManager.GRWService();


        private string sqlMain;
        private string sqlDetail;
        private string sqlMastPara;
        private SqlConnection connection;
        private SqlCommand command;
        
        //private SqlDataAdapter adapter;

        private SqlDataAdapter adapterMain;
        private SqlDataAdapter adapterDetail;

        private SqlCommandBuilder builderMain;
        private SqlCommandBuilder builderDetail;


        private DataTable tblMain;
        private DataTable tblDetail;
        private DataTable ExcelTable;


        private DataRow row;

        private Double totalPamt;
        private Double totalUamt;

        private int entno;
        private int itmid = 0;
        private int dpid;

        private bool newpo;
        private bool newitem;
        private bool entry1;
        private bool fDeleteRow;
        private bool editstart;
        private bool IsExcelData;
        
        public frm_PurchaseOrder()
        {
            InitializeComponent();

        }

        private void frm_PurchaseOrder_Load(object sender, System.EventArgs e)
        {
            SetForm();
            fDeleteRow = false;
        }

        private void SetDataObjects(string mysql)
        {
            ConnectionManager conobj = new ConnectionManager();
            connection = new SqlConnection(conobj.conn);
            command = new SqlCommand(mysql, connection);
        }

        private void SetForm()
        {
            try
            {
                //MessageBox.Show("Please Enter Project/Work ID to Get the Recommended Payment Record!");
                IsExcelData = false; 
                 
                lblpo.Text = POObj.DLookup("VendorName", "Vendor", "VendorID=" + ProjEntry.POVendor);
                txtcost.ReadOnly = true;
                txtcost.BackColor = Color.Aquamarine;
                txtcost.Text = String.Format("{0:#,##0}", Convert.ToDecimal(ProjEntry.WorkCost.ToString()));
                //lblworktitle.Text = POObj.DLookup("WorkDesc", "EstMaster", "EstimateNo=" + ProjEntry.PWRNumber + " and WorkNo=" + ProjEntry.WorkID);
                lblworktitle.Text = POObj.DLookup("WorkTitle", "POrder", "EstimateNo=" + ProjEntry.PWRNumber + " and WorkNo=" + ProjEntry.WorkID);
                POObj.FillCombo("DeptName", "DepID", cboDepartment, "Department", "");

                txtworktitle.Text = lblworktitle.Text;
                showrecord();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.txtPO.Focus();
                newpo = false;
                //this.txtUserID.Text = Login.Cuser;
            }

        }

        private void showrecord()
        {

            this.txtPO.Text = POObj.DLookup("PONo", "POrder", "Estimateno=" + ProjEntry.PWRNumber + " and WorkNo=" + ProjEntry.WorkID + " and PayID=" + ProjEntry.PayNo);  

            if (this.txtPO.Text!= "")
            {
                if (newpo == true)
                {
                    POObj.POno = 0;
                }
                else
                {
                    POObj.POno = int.Parse(this.txtPO.Text);
                }

                POObj.Estno = ProjEntry.PWRNumber;
                POObj.Wrkno = ProjEntry.WorkID;
                POObj.Payid = ProjEntry.PayNo;

            }

            ShowPOMainInfo("PONo", "POrder", "PONo=" + POObj.POno);

            SetupPODataGridView("PONo", "PODetail", "sp_GetPORecord", DG_POEntry, "PONo=" + POObj.POno);

            showtotal();


        }

        private void ShowPOMainInfo(string fldName, string tblName, string whereCond)
        {
            try
            {
                sqlMain = "SELECT EstimateNo,PONo,WorkNo,PayID,VendorID,PoDate,IssueDate,TermsCondition,ContactPerson,DeptID,AllDiscount,TaxAmt,QuotID,QuotDate,DeliverTo,Deliverydate,IsApproved,IsCancelled,IsLocked,CreatedBy,CreatedDateTime,ModifiedBy,ModifiedDateTime FROM " + tblName + " where  " + whereCond + " Order by " + fldName;
                SetDataObjects(sqlMain);
                adapterMain = new SqlDataAdapter(command);
                builderMain = new SqlCommandBuilder(adapterMain);  

                connection.Open();
                tblMain = new DataTable();
                adapterMain.Fill(tblMain);

                if (tblMain.Rows.Count != 0)
                {
                    this.txtPO.Text = tblMain.Rows[0]["PONo"].ToString();
                    //this.txtcontact.Text = POObj.DLookup("ContactPerson", "Vendor", "VendorID=" + ProjEntry.POVendor);
                    this.txtcontact.Text = tblMain.Rows[0]["ContactPerson"].ToString();
                    POObj.DptID = tblMain.Rows[0]["DeptID"].ToString();
                    this.txtQuotID.Text = tblMain.Rows[0]["QuotID"].ToString();
                    this.txtquotdate.Text = tblMain.Rows[0]["QuotDate"].ToString();
                    this.txtshipat.Text = tblMain.Rows[0]["DeliverTo"].ToString();
                    this.entdate.Text = tblMain.Rows[0]["PoDate"].ToString();
                    this.txtTerms.Text = tblMain.Rows[0]["TermsCondition"].ToString();
                    this.txtdiscount.Text = tblMain.Rows[0]["AllDiscount"].ToString();
                    this.txtdlvdate.Text = tblMain.Rows[0]["DeliveryDate"].ToString();
                    ProjEntry.PWRNumber = int.Parse(tblMain.Rows[0]["EstimateNo"].ToString());
                    ProjEntry.WorkID = int.Parse(tblMain.Rows[0]["WorkNo"].ToString());
                    ProjEntry.PayNo = int.Parse(tblMain.Rows[0]["PayID"].ToString());

                    if (POObj.DptID != null && POObj.DptID!="")
                    {
                        dpid = int.Parse(POObj.DLookup("DepID", "Department", "DeptID='" + POObj.DptID + "'").ToString());
                        cboDepartment.SelectedValue = dpid;
                    }

                    this.DG_POEntry.ReadOnly = false;

                }
                else
                { 
                  //MessageBox.Show("Purchase Order Not Found","Invalid Purchase Order Number",MessageBoxButtons.OK,MessageBoxIcon.Error);    
                }


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

        void EnableControls()
        {
            this.txtPO.Enabled = true;
            this.txtcontact.Enabled = true;
            this.cboDepartment.Enabled = true;
            this.txtTerms.Enabled = true;
            this.entdate.Enabled = true;
            this.DG_POEntry.ReadOnly = false;
        }
        
        private void SetupPODataGridView(string fldName, string tblName, string StProc, DataGridView mygrid, string whereCond)
        {
            try
            {
                if (tblDetail != null)
                {
                    tblDetail.Clear();
                }

                DG_POEntry.DataSource = null;
                DG_POEntry.Rows.Clear();
                DG_POEntry.Refresh();

                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@tblName", tblName);
                param[1] = new SqlParameter("@fldName", fldName);
                param[2] = new SqlParameter("@whereCond", whereCond);
                
                //SetDataObjects(StProc, "EstDetail", param);

                if (tblName == "POData")
                {
                    //sqlDetail = "SELECT EntryNo,PONo,ItemID,Specification,Unit,Qty,Rate,(Qty*Rate) as Amount FROM " + tblName;

                    //SetDataObjects(sqlDetail);
                    //adapterDetail = new SqlDataAdapter(command);
                    //builderDetail = new SqlCommandBuilder(adapterDetail);
                    tblDetail = ExcelTable; 
                    adapterDetail.Fill(tblDetail);
                }
                else
                {
                    sqlDetail = "SELECT POTransID,PONo,EntryNo,ItemID,Specification,Unit,Qty,Rate,(Qty*Rate) as Amount FROM " + tblName + " where  " + whereCond + " Order by " + fldName;
                    SetDataObjects(sqlDetail);
                    adapterDetail = new SqlDataAdapter(command);
                    builderDetail = new SqlCommandBuilder(adapterDetail);
                    tblDetail = new DataTable();
                    adapterDetail.Fill(tblDetail);


                }


                DG_POEntry.DataSource = tblDetail;
                DG_POEntry.Columns.Clear();

                bool entrystart = false;
                //MessageBox.Show("Total Record " + tblDetail.Rows.Count.ToString());  

                //MessageBox.Show("Current Work No:" + PEObj.Wrkno.ToString());
                foreach (DataColumn dc in tblDetail.Columns)
                {
                    if (dc.ColumnName == "ItemID" ||  dc.ColumnName== "Specification"  ||dc.ColumnName == "Unit" || dc.ColumnName == "Rate" || dc.ColumnName == "Qty" || dc.ColumnName == "Amount")
                    {
                        DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                        column.DataPropertyName = dc.ColumnName;

                        if (tblName == "POData")
                        {
                            if (dc.ColumnName != "ItemID")
                            {
                                column.Name = dc.ColumnName;
                                column.SortMode = DataGridViewColumnSortMode.Automatic;
                                column.ValueType = dc.DataType;
                                DG_POEntry.Columns.Add(column);
                                if (dc.ColumnName == "Specification")
                                {
                                    column.HeaderText = "Item Name";
                                    column.DefaultCellStyle.Font = new Font("Calibri", 8, FontStyle.Regular);
                                    column.Width = 405;                                    
                                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                                }

                            }
                        }
                        else
                        {
                            if (dc.ColumnName == "ItemID" || dc.ColumnName == "Specification" && entrystart == false)
                            {
                                
                                    //celno = int.Parse(e.ColumnIndex.ToString());
                                    //int yCoord = DG_Detail.CurrentCellAddress.Y; // You can get X if you need it.

                                    //PEObj.VendID = (int)DG_Detail.Rows[yCoord].Cells[celno].Value;
                                entrystart = true;
                                int tblitem = 0;

                                if (tblDetail.Rows.Count != 0)
                                {

                                    tblitem = int.Parse(tblDetail.Rows[0]["ItemID"].ToString()!=null ? tblDetail.Rows[0]["ItemID"].ToString() : "0");                                    
                                    //tblitem = int.Parse(tblDetail.Rows[0]["ItemID"].ToString());
                                }

                                if (tblitem == 648)
                                {
                                    if (dc.ColumnName == "ItemID")
                                    {

                                        column.Name = dc.ColumnName;
                                        column.SortMode = DataGridViewColumnSortMode.Automatic;
                                        column.ValueType = dc.DataType;
                                        DG_POEntry.Columns.Add(column);
                                        column.Visible = false;
                                    }
                                    
                                    
                                    if (dc.ColumnName == "Specification")
                                    {
                                        
                                        column.Name = dc.ColumnName;
                                        column.SortMode = DataGridViewColumnSortMode.Automatic;
                                        column.ValueType = dc.DataType;
                                        DG_POEntry.Columns.Add(column);
                                        column.HeaderText = "Item Name";
                                        column.DefaultCellStyle.Font = new Font("Calibri", 8, FontStyle.Regular);
                                        column.Width = 405;
                                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                                    }

                                    entrystart = false;
                                }
                                else
                                {
                                    DataGridViewComboBoxColumn Itemcol = new DataGridViewComboBoxColumn();
                                    Itemcol = POObj.combocolumn("ItemName", "ItemID", "Items", "");
                                    Itemcol.DataPropertyName = dc.ColumnName;
                                    Itemcol.Name = "ItemID";
                                    Itemcol.SortMode = DataGridViewColumnSortMode.Automatic;
                                    Itemcol.ValueType = dc.DataType;
                                    DG_POEntry.Columns.Add(Itemcol);
                                    Itemcol.HeaderText = "Item Name";
                                    Itemcol.DefaultCellStyle.Font = new Font("Calibri", 8, FontStyle.Regular);
                                    Itemcol.Width = 405;
                                    Itemcol.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                                    Itemcol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                                    Itemcol.ReadOnly = true;
                                }
                            }
                            else
                            {
                                if (dc.ColumnName != "Specification" && dc.ColumnName != "ItemID")
                                {
                                    column.Name = dc.ColumnName;
                                    column.SortMode = DataGridViewColumnSortMode.Automatic;
                                    column.ValueType = dc.DataType;
                                    DG_POEntry.Columns.Add(column);
                                }
                            }

                        }



                        switch (column.DataPropertyName)
                        {

                            case "Specification":
                                column.HeaderText = "Item Name";
                                column.Width = 405;
                                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                                break;
                            
                            case "Unit":
                                column.HeaderText = "Unit";
                                column.Width = 80;
                                column.DefaultCellStyle.Format = "##,0";
                                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                break;

                            case "Qty":
                                column.HeaderText = "Quantity";
                                column.Width = 80;
                                column.DefaultCellStyle.Format = "##,0.000";
                                column.ReadOnly = true;
                                column.DefaultCellStyle.NullValue = String.Empty;
                                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                break;
                            
                            case "Rate":
                                column.HeaderText = "Rate";
                                column.Width = 80;
                                column.ReadOnly = true;
                                column.DefaultCellStyle.Format = "##,0.000";
                                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                break;

                            case "Amount":

                                column.HeaderText = "Amount";
                                column.Width = 80;
                                column.DefaultCellStyle.Format = "##,0";
                                column.ReadOnly = true;
                                column.DefaultCellStyle.NullValue = String.Empty;
                                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                break;


                            default:
                                column.Visible = false;
                                break;
                        }

                        //if (column.DataPropertyName == "Rate")
                        //{
                        //    DataGridViewTextBoxColumn columnA = new DataGridViewTextBoxColumn();
                        //    columnA.Name = "Amount";
                        //    columnA.SortMode = DataGridViewColumnSortMode.Automatic;
                        //    columnA.ValueType = dc.DataType;
                        //    DG_POEntry.Columns.Add(columnA);

                        //    columnA.HeaderText = "Amount";
                        //    columnA.Width = 80;
                        //    columnA.DefaultCellStyle.Format = "##,0";
                        //    columnA.ReadOnly = true;
                        //    columnA.DefaultCellStyle.NullValue = String.Empty;
                        //    columnA.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        //}
                    }
                    
                }
                //DG_POEntry.DataSource = EstDetailTable.DefaultView;
                //DG_POEntry.DefaultCellStyle.NullValue = "NA";

                DG_POEntry.Refresh(); 
                DG_POEntry.AutoGenerateColumns = false;
                DG_POEntry.ReadOnly = true;
                DG_POEntry.EditMode = DataGridViewEditMode.EditOnEnter;
                DG_POEntry.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 9, FontStyle.Bold);
                DG_POEntry.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DG_POEntry.ColumnHeadersDefaultCellStyle.BackColor = Color.Aqua;
                DG_POEntry.SelectionMode = DataGridViewSelectionMode.CellSelect;
                DG_POEntry.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                DG_POEntry.MultiSelect = false;
                DG_POEntry.Dock = DockStyle.Top;
                DG_POEntry.AllowUserToDeleteRows = true;
                DG_POEntry.AllowUserToAddRows = false;  
                DG_POEntry.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;

                //DG_POEntry.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(DG_POEntry_EditingControlShowing);
                DG_POEntry.CellValueChanged += new DataGridViewCellEventHandler(DG_POEntry_CellValueChanged);

                DG_POEntry.RowsRemoved += new DataGridViewRowsRemovedEventHandler(DG_POEntry_RowsRemoved);
                

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


        private void showtotal()
        {
            txtQtyTotal.ReadOnly = true;
            txtAmtTotal.ReadOnly = true;

            txtQtyTotal.BackColor = Color.Aquamarine;
            txtAmtTotal.BackColor = Color.Aquamarine;

            txtQtyTotal.Text = String.Format("{0:#,##0}", Convert.ToDecimal(POObj.MakeGridTotal(DG_POEntry, 2).ToString()));
            txtAmtTotal.Text = String.Format("{0:#,##0}", Convert.ToDecimal(POObj.MakeGridTotal(DG_POEntry, 4).ToString()));

            txtRupees.Text = "Rupees:" + rupeeword.changeCurrencyToWords(double.Parse(txtAmtTotal.Text));   

        }


        private bool AddNewItem(string itemname)
        {
            if (itemname == "System.Data.DataRowView")
            {
                return false;
            }

            bool result;
            newitem = true;
            ArrayList fieldlist = new ArrayList();

            //Passwing property values to arraylist
            fieldlist.Add(itemname);
            fieldlist.Add(0);
            result = POObj.ItemRecord(fieldlist, "I");
            if (result == true)
            {

                SetupPODataGridView("PONo", "PODetail", "sp_GetPORecord", DG_POEntry, "PONo=" + POObj.POno);
                foreach (DataRow mrow in tblDetail.Rows)
                {
                    entno = int.Parse(mrow["EntryNo"].ToString());
                }

                entno++;
                row = tblDetail.NewRow();
                tblDetail.Rows.Add(row);
                row["EntryNo"] = entno;
                if (entno == 1)
                {
                    entry1 = true;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        void DG_POEntry_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            int celno;
            try
            {
                celno = int.Parse(e.ColumnIndex.ToString());
                int yCoord = DG_POEntry.CurrentCellAddress.Y; // You can get X if you need it.

                if (celno == 0)
                {
                   // POObj.Itmid = int.Parse(DG_POEntry.Rows[yCoord].Cells[celno].Value.ToString());
                    //ProjEntry.PoVendor = PEObj.VendID;
                    //ProjEntry.WorkCost = double.Parse(DG_POEntry.Rows[yCoord].Cells[1].Value.ToString()); 
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

        void DG_POEntry_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            int celno;
            try
            {
                celno = 1;
                int yCoord = DG_POEntry.CurrentCellAddress.Y; // You can get X if you need it.

                //MessageBox.Show(DG_POEntry.Rows.Count.ToString());

                PurchaseOrder.Itemid2 = int.Parse(DG_POEntry.Rows[e.RowIndex].Cells["ItemID"].Value.ToString());
                PurchaseOrder.ItemName = POObj.DLookup("ItemName", "Items", "ItemID=" + PurchaseOrder.Itemid2); 
                PurchaseOrder.DetailSpec = POObj.DLookup("Specification", "PODetail", "ItemID=" + PurchaseOrder.Itemid2 + " and PONO=" + txtPO.Text); 
                

                POObj.POno = int.Parse(txtPO.Text);

                //PurchaseOrder.Itemid2 = int.Parse(DG_POEntry.SelectedRows[e.RowIndex].Cells[2].Value.ToString());
                //PurchaseOrder.Pono2 = POObj.POno;
                //PurchaseOrder.ItemName = DG_POEntry.SelectedRows[e.RowIndex].Cells[celno].Value.ToString();        
                //gvOnline.SelectedRows[0].Cells[0].Value.ToString()
                // POObj.Itmspec = DG_POEntry.Rows[yCoord].Cells[celno].ToString();
                // POObj.Itmid = int.Parse(DG_POEntry.Rows[yCoord].Cells[1].ToString());

                POSpec posp;
                posp = new POSpec();
                posp.ShowDialog(); 
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.ToString());
            }

        }

        private void DG_POEntry_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox editingComboBox = e.Control as ComboBox;
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
                    //this.DG_POEntry.AllowUserToAddRows = false;
                    editingComboBox.SelectedIndexChanged += new System.EventHandler(this.editingComboBox_SelectedIndexChanged);
                    editingComboBox.PreviewKeyDown += new PreviewKeyDownEventHandler(editingComboBox_PreviewKeyDown);
                    

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void DG_POEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (DG_POEntry.CurrentCell.ColumnIndex == 1)
                {
                    DG_POEntry[2, DG_POEntry.CurrentRow.Index].Selected = true;
                    DG_POEntry.CurrentCell = DG_POEntry[2, DG_POEntry.CurrentRow.Index];
                    //e.Handled = true;
                }
                if (DG_POEntry.CurrentCell.ColumnIndex == 2)
                {
                    DG_POEntry[3, DG_POEntry.CurrentRow.Index].Selected = true;
                    DG_POEntry.CurrentCell = DG_POEntry[3, DG_POEntry.CurrentRow.Index];
                    //e.Handled = true;
                }
                if (DG_POEntry.CurrentCell.ColumnIndex == 3)
                {
                    DG_POEntry[0, DG_POEntry.CurrentRow.Index].Selected = true;
                    DG_POEntry.CurrentCell = DG_POEntry[0, DG_POEntry.CurrentRow.Index];
                    //e.Handled = true;
                }

            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            foreach (Control con in this.Controls)
            {
                if (con is TextBox && con.Focused != true)
                {
                    return false;
                }
            }
                    if (keyData == Keys.Tab && DG_POEntry.CurrentCell.ColumnIndex == 1)
                    {
                        DG_POEntry.CurrentCell = DG_POEntry.Rows[this.DG_POEntry.CurrentCell.RowIndex].Cells[2];
                        return true;
                    }
                    else if (keyData == Keys.Tab && DG_POEntry.CurrentCell.ColumnIndex == 2 && DG_POEntry.CurrentCell.RowIndex < DG_POEntry.Rows.Count - 1)
                    {
                        DG_POEntry.CurrentCell = DG_POEntry.Rows[DG_POEntry.CurrentCell.RowIndex + 1].Cells[3];
                        return true;
                    }
                    else
                        return base.ProcessCmdKey(ref msg, keyData);
        }

        void editingComboBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //throw new NotImplementedException();

            if (e.KeyCode == Keys.Enter)
            {
                //SendKeys.Send("{TAB}");
                //return;

                if (DG_POEntry.CurrentCell.ColumnIndex == 0)
                {
                    DG_POEntry[1, DG_POEntry.CurrentRow.Index].Selected = true;
                    DG_POEntry.CurrentCell = DG_POEntry[1, DG_POEntry.CurrentRow.Index];

                    this.DG_POEntry.AllowUserToAddRows = false;
                    
                    //e.Handled = true;
                }
            }
        }

        private void editingComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ComboBox comboBox1 = (ComboBox)sender;
            string olditem = comboBox1.Text;

            if (POObj.DLookup("ItemName", "Items", "ItemName='" + olditem + "'") == olditem)
            {
                newitem = false;
            }
            else
            {
                newitem = true;
                MessageBox.Show("Item " + comboBox1.Text + " Not Found.....");
                if (MessageBox.Show("Do You Want to ADD New Item?", "New Item Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, 0, false) == DialogResult.Yes)
                {
                    if (AddNewItem(comboBox1.Text) == true)
                    {
                        //comboBox1 = OBJFuncLib.FillCombo("VendorName", "VendorID", comboBox1, "Vendor", ""); 

                        MessageBox.Show("New Item Successfully Added....");
                    }
                }
            }
        }

        private void DG_POEntry_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Update the balance column whenever the value of any cell changes.
            //MessageBox.Show(e.ColumnIndex.ToString());     
            try
            {
                if (DG_POEntry.Rows.Count != 0)
                {
                    if (editstart == true)
                    {
                        double Tot;
                        if (this.DG_POEntry.CurrentCell.OwningColumn.Name.ToString() == "Qty" || this.DG_POEntry.CurrentCell.OwningColumn.Name.ToString() == "Rate")
                        {
                            showtotal();
                        }
                        if (this.DG_POEntry.CurrentCell.OwningColumn.Name.ToString() == "Rate" || this.DG_POEntry.CurrentCell.OwningColumn.Name.ToString() == "Qty")
                        {
                            //this.DG_POEntry.AllowUserToAddRows = false;

                            int Qty = 0;
                            double Rate = 0;

                            if (DG_POEntry.Rows[e.RowIndex].Cells[2].Value != null)
                            {

                                if (DG_POEntry.Rows[e.RowIndex].Cells[2].Value.ToString().Length != 0)
                                {
                                    Qty = int.Parse(DG_POEntry.Rows[e.RowIndex].Cells[2].Value.ToString()) ;
                                }

                            }
                            if (DG_POEntry.Rows[e.RowIndex].Cells[3].Value != null)
                            {
                                if (DG_POEntry.Rows[e.RowIndex].Cells[3].Value.ToString().Length != 0)
                                {
                                    Rate = double.Parse(DG_POEntry.Rows[e.RowIndex].Cells[3].Value.ToString()) ;
                                }

                            }

                            Tot = Qty * Rate;
                            DG_POEntry.Rows[e.RowIndex].Cells[4].Value = Tot;

                            if (e.ColumnIndex==3)
                            {
                               DG_POEntry[0, DG_POEntry.CurrentRow.Index].Selected = true;
                               DG_POEntry.CurrentCell = DG_POEntry[0, DG_POEntry.CurrentRow.Index];
                               //e.Handled = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DG_POEntry_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            // Update the balance column whenever rows are deleted.
            //showtotal();
            //fDeleteRow = false;
        }

        private void DG_POEntry_SelectionChanged(object sender, EventArgs e)
        {
            // Update the labels to reflect changes to the selection.
            //UpdateLabelText();
        }

        private void DG_POEntry_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            // Update the labels to reflect changes to the number of entries.
            //UpdateLabelText();

            if (fDeleteRow)
            {
                try
                {
                    adapterDetail.Update(tblDetail);
                    //sql_DA_EQUPS.Update(dsEQUPS, dsEQUPS.EQU_PS.TableName);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                fDeleteRow = false;
            }

        }

        private void DG_POEntry_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            int testint;

            if (e.ColumnIndex != 0)
            {
                if (e.FormattedValue.ToString().Length != 0)
                {
                    // Try to convert the cell value to an int.
                    if (!int.TryParse(e.FormattedValue.ToString(), out testint))
                    {
                        DG_POEntry.Rows[e.RowIndex].ErrorText = "Error: Invalid entry";
                        e.Cancel = true;
                    }
                }
            }
        }

        private void DG_POEntry_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            // Clear any error messages that may have been set in cell validation.
            DG_POEntry.Rows[e.RowIndex].ErrorText = null;
            double Tot;

            try
            {
                for (int i = 0; i < DG_POEntry.Rows.Count - 1; i++)
                {
                    int Qty = 0;

                    double Rate = 0;

                    if (DG_POEntry.Rows[i].Cells[3].Value != null)
                    {

                        if (DG_POEntry.Rows[i].Cells[3].Value.ToString().Length != 0)
                        {
                            Qty = (int)DG_POEntry.Rows[i].Cells[3].Value;
                        }

                    }
                    if (DG_POEntry.Rows[i].Cells[4].Value != null)
                    {
                        if (DG_POEntry.Rows[i].Cells[4].Value.ToString().Length != 0)
                        {
                            Rate = (double)DG_POEntry.Rows[i].Cells[4].Value;
                        }

                    }

                    Tot = Qty * Rate;
                    DG_POEntry.Rows[i].Cells[5].Value = Tot;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);  
            }

        }

        //void DG_POEntry_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        //{
        //    //throw new NotImplementedException();
        //}

        private void DG_POEntry_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (fDeleteRow==false)
            {
                if (MessageBox.Show("Are You Sure to Delete This Entry?",
                             "Entry Deletion Alert", MessageBoxButtons.YesNo,
                             MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, 0, false)
                             == DialogResult.No)
                {
                    e.Cancel = true;
                    if (DG_POEntry.RowCount != 0)
                    {
                        int totrow = int.Parse(DG_POEntry.RowCount.ToString()) - 1;
                        DG_POEntry.Rows[totrow].Selected = true;

                    }
                }
                else
                {
                    e.Cancel = false;
                    fDeleteRow = true;
                }
            }
            else
            {
                return;
            }
        }

        private void DG_POEntry_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            fDeleteRow = false;
            DG_POEntry.UserDeletingRow += new DataGridViewRowCancelEventHandler(DG_POEntry_UserDeletingRow);
        }


        private void txtFindPO_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (txtPO.Text != "")
                {
                    //ShowProjectMainInfo();
                    //sqlQuery = "SELECT VendorID,PayDetail,PayRecmd,ChqReady,Remarks,EstimateNo,Workno,PayID,EntDate,GlCode,PaidAmt,Taxamt,Amount,DueDate,Hold,PaidStatus,Chqno,ChqDate,BkAccount,IssuedDate,IssuedBy,RecvdBy,UsrID,UsrDT from payments where EstimateNo=" + int.Parse(this.txtPO.Text) + " and not PayRecmd is Null Order By Workno, PayID";
                    this.txtRupees.Text = "Rupees: " + rupeeword.changeCurrencyToWords(totalUamt);
                    this.txtPO.Focus();  
                }
                else
                {
                    if (tblMain != null)
                    {
                        tblMain.Clear();
                    }
                    DG_POEntry.DataSource = null;
                    DG_POEntry.Rows.Clear();
                    DG_POEntry.Refresh();
                }
                showrecord();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //this.BSPay.MoveLast();
                //showrecord();
            }
            finally
            {
                //this.BSPay.Position = recno;
                //showrecord();
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //frm_login loginform;
            ExcelTable = null;
            //loginform = new frm_login();
            this.Close();
            //loginform.Show(); 

        }

        private void frm_PurchaseOrder_Activated(object sender, EventArgs e)
        {
            this.txtPO.Focus();  
        }

        private void btnNewPO_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Want to Add Purchase Order?",
                         "New Purcahse Order Alert", MessageBoxButtons.YesNo,
                         MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, 0, false)
                         == DialogResult.No)
            {
                this.txtFndPO.Focus();
                return;
            }

            newpo = true;
            EnableControls();

            this.txtPO.Text = "NEW";
            //this.cboDepartment.SelectedIndex = 0;
            //showrecord();
            this.txtdpt.Text = cboDepartment.SelectedValue.ToString(); 

            this.txtTerms.Text = "";
            this.txtdiscount.Text = "0"; 
            this.entdate.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            editstart = false;

            if (newpo == true)
            {

                try
                {
                    setMainproperties();
                    sqlMastPara = "Update MastPara Set PrintPONo=" + POObj.POno;
                    SetDataObjects(sqlMastPara);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();


                    
                    PostMainTable();
                    PostDetailTable();
                    newpo = false;
                    MessageBox.Show("Record Has Been Successfully Saved!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                try
                {
                    this.Validate();
                    adapterMain.Update(tblMain);
                    if (this.btnSave.Text == "Update")
                    {
                        setMainproperties();
                        PostMainTable();
                    }

                    //CHeck if New Entry of Work is Made on Existing Work Order, Then Insert Process will Run
                    if (entry1 == true)
                    {
                        PostMainTable();
                        PostDetailTable();
                    }
                    else
                    {
                        foreach (DataRow rw in tblDetail.Rows)
                        {
                            if (rw.RowState != DataRowState.Deleted)
                            {
                                rw["PONo"] = POObj.POno;
                                if (PurchaseOrder.DetailSpec != "")
                                {
                                    int itemid = int.Parse(rw["ItemID"].ToString() == "" ? "648" : rw["ItemID"].ToString());

                                    if (itemid == PurchaseOrder.Itemid2)
                                    {
                                        rw["Specification"] = PurchaseOrder.DetailSpec;
                                        PurchaseOrder.DetailSpec = "";
                                    }
                                }
                            }
                        }
                        
                       adapterDetail.UpdateCommand = builderDetail.GetUpdateCommand();
                       adapterDetail.Update(tblDetail);
                    }
                    MessageBox.Show("Record Has Been Successfully Updated!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void setMainproperties()
        {
            //PEObj.Estno = int.Parse(this.txtProjID.Text);
            string lastpono;
            if (this.txtPO.Text == "NEW")
            {
                lastpono = POObj.DLookup("MAX(PONo)", "POrder", "");

                if (lastpono == "")
                {
                    lastpono = "0";
                }

                txtdpt.Text = POObj.DLookup("DeptID", "Department", "DepID=" + int.Parse(cboDepartment.SelectedValue.ToString()));     

                POObj.POno = int.Parse(lastpono) + 1;
                this.txtPO.Text = POObj.POno.ToString();
                POObj.POdate = Convert.ToDateTime(this.entdate.Text);
                POObj.Payid = ProjEntry.PayNo;
                POObj.Estno = ProjEntry.PWRNumber;
                POObj.ContPerson = this.txtcontact.Text;
                POObj.TermsCond = this.txtTerms.Text;
                POObj.DptID = this.txtdpt.Text;
                POObj.VendID = ProjEntry.POVendor;
                POObj.Wrkno = ProjEntry.WorkID;
                POObj.Discount = double.Parse(this.txtdiscount.Text);
                POObj.Quotid = txtQuotID.Text;
                POObj.Quotdate = Convert.ToDateTime(this.txtquotdate.Text);
                POObj.Shipat = txtshipat.Text;
                POObj.Dlvdate = Convert.ToDateTime(this.txtdlvdate.Text);
                POObj.Usrid = Login.Cuserid;
            }
            else
            {
                POObj.POdate = Convert.ToDateTime(this.entdate.Text);
                POObj.Payid = ProjEntry.PayNo;
                POObj.Estno = ProjEntry.PWRNumber;
                POObj.ContPerson = this.txtcontact.Text;
                POObj.TermsCond = this.txtTerms.Text;
                POObj.DptID = this.txtdpt.Text;
                POObj.VendID = ProjEntry.POVendor;
                POObj.Wrkno = ProjEntry.WorkID;
                POObj.Discount = double.Parse(this.txtdiscount.Text);
                POObj.Quotid = txtQuotID.Text;
                POObj.Quotdate = Convert.ToDateTime(this.txtquotdate.Text);
                POObj.Shipat = txtshipat.Text;
                POObj.Dlvdate = Convert.ToDateTime(this.txtdlvdate.Text);
                POObj.Usrid = Login.Cuserid;
            }

        }

        private void PostMainTable()
        {
            bool result;

            POObj.Worktitle = txtworktitle.Text;

            ArrayList fieldlist = new ArrayList();

            
                //@isaprd as bit,//@iscancl as bit,//@islock as bit,
            
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

            //Passwing property values to arraylist
            fieldlist.Add(POObj.Estno);
           // fieldlist.Add(POObj.Worktitle);
            fieldlist.Add(POObj.POno);
            fieldlist.Add(POObj.Wrkno);
            fieldlist.Add(POObj.Payid);
            fieldlist.Add(POObj.VendID);
            fieldlist.Add(POObj.DptID);
            fieldlist.Add(POObj.POdate);
            fieldlist.Add(POObj.TermsCond);
            fieldlist.Add(POObj.ContPerson);
            fieldlist.Add(POObj.Discount);
            fieldlist.Add(POObj.Tax);
            fieldlist.Add(POObj.Quotid);
            fieldlist.Add(POObj.Quotdate);
            fieldlist.Add(POObj.Shipat);
            fieldlist.Add(POObj.Dlvdate);
            fieldlist.Add(POObj.Usrid);
            if (this.btnSave.Text == "Update")
            {
                fieldlist.Add("U");
                result = POObj.MainRecord(fieldlist, "U");
            }
            else
            {
                fieldlist.Add("I");
                result = POObj.MainRecord(fieldlist, "I");
            }



            
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

        private void PostDetailTable()
        {
            bool result;

            int ent = 0;
            foreach (DataRow row in tblDetail.Rows)
            {

                tblDetail.Rows[ent]["PONO"] = POObj.POno.ToString();
                tblDetail.Rows[ent]["EntryNo"] = ent+1;
                tblDetail.Rows[ent]["ItemID"] = 648;
                tblDetail.Rows[ent]["Unit"] = "PCS";
                ent++;
            }

            int coln = 0;
            foreach (DataColumn col in tblDetail.Columns)
            {
                string checkcolumn = tblDetail.Columns[coln].ToString();

                //if (checkcolumn == "Amount")
                //{
                //    tblDetail.Columns.Remove("Amount");
                //}

                //if (checkcolumn == "POTransID")
                //{
                //    tblDetail.Columns.Remove("POTransID");
                //}

                if (checkcolumn == "F9")
                {
                    tblDetail.Columns.Remove("F9");
                }

                coln++;

                if (coln >= tblDetail.Columns.Count)
                {
                    break;
                }

            }

           // tblDetail.Columns.Remove("Amount");

            tblDetail.AcceptChanges();
            
        //    result = POObj.DetailRecord(tblDetail, "I");



            ent = 1;
            foreach (DataRow row in tblDetail.Rows)
            {
                if (IsExcelData == false)
                {
                    POObj.Itmid = int.Parse(row["ItemID"].ToString());

                    if (POObj.Itmspec == "" || POObj.Itmspec == null)
                    {

                        POObj.Itmspec = string.Empty;
                    }
                }
                else
                {
                    POObj.Itmspec = row["Specification"].ToString();
                    POObj.Itmid = 648;
                }

                if (float.Parse(row["Qty"].ToString()) != 0)
                {

                    POObj.Entno = ent;


                    POObj.Unit = row["Unit"].ToString();
                    POObj.Qty = float.Parse(row["Qty"].ToString().Trim());
                    POObj.Rate = double.Parse(row["Rate"].ToString().Trim());


                    ArrayList fieldlist = new ArrayList();

                    //@pono as int,
                    //@entno as int,
                    //@itmid as int, 
                    //@itmspec as varchar(100),
                    //@unit as varchar(10),
                    //@qty as float,
                    //@rate as money

                    //Passwing property values to arraylist
                    fieldlist.Add(POObj.POno);
                    fieldlist.Add(POObj.Entno);
                    fieldlist.Add(POObj.Itmid);
                    fieldlist.Add(POObj.Itmspec);
                    fieldlist.Add(POObj.Unit);
                    fieldlist.Add(POObj.Qty);
                    fieldlist.Add(POObj.Rate);

                    result = POObj.DetailRecord(fieldlist, "I");



                    if (result == true)
                    {
                        MessageBox.Show("Record Has Been Successfully Added!");
                    }
                    else
                    {
                        MessageBox.Show("Record Doest Not Saved!!!!!");
                        this.btnSave.Text = "&Save";
                    }
                    ent++;
                }
                //break; 
            }
        }

        private void SaveTable(DataTable dt)
        {
            //DataTable ipdt = howebref.GetIP();

            //string ipstring = ipdt.Rows[0]["ipname"].ToString().Trim();

            //string defip = "http://" + ipstring + "/ppwebservices/GRWService.asmx";

            //ODMObj.Url = defip;

            //ConnectionManager conn = new ConnectionManager();

            //bool recordadd = ODMObj.InsertUpdate(conn.conn, "sp_InsertPODetailFull", "I", dt);


            MessageBox.Show("GIN has been uploaded successfully.", "IMS 1.0", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            editstart = true;
            //if (btnSave.Text == "Save")
            //{
            //    btnSave.Text = "Update";
            //}
            //else
            //{
            //    btnSave.Text = "Save";
            //}
            if (MessageBox.Show("Do You Want to Add New Entry?",
                         "New Entry Alert", MessageBoxButtons.YesNo,
                         MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, 0, false)
                         == DialogResult.Yes)
            {
                MessageBox.Show("If You Have New Item and You Sure, Please Use New Item Option Below to Add New Item First!");
                DG_POEntry.AllowUserToAddRows = true;
                if (tblDetail.Rows.Count == 0)
                {
                    newpo = true;
                }
                else
                {
                    POObj.POno = int.Parse(txtPO.Text);
                }
            }
            else
            {
                row = tblDetail.Rows[0];
            }
            //row["EstimateNo"] = PEObj.Estno;
            //row["EntDate"] = DateTime.Now.ToString();
            //row["DueDate"] = DateTime.Now.ToString();
            //row["Workno"] = PEObj.Wrkno;

            EnableControls();
            //row.BeginEdit(); 
            this.DG_POEntry.Focus();  

 
        }

        private void cboDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboDepartment.SelectedIndex > 0)
            {
                POObj.DptID = cboDepartment.SelectedValue.ToString();
                
                if (POObj.DptID == null || POObj.DptID=="")
                {
                    POObj.DptID = "HOA";  
                }

                txtdpt.Text = POObj.DLookup("DeptID","Department","DepID=" + int.Parse(cboDepartment.SelectedValue.ToString()));     
            }

        }

        private void txtFndPO_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtFndPO.Text != "")
                {

                    POObj.POno = int.Parse(txtFndPO.Text);  
                    ShowPOMainInfo("PONo", "POrder", "PONo=" + POObj.POno);
                    SetupPODataGridView("PONo", "PODetail", "sp_GetPORecord", DG_POEntry, "PONo=" + POObj.POno);

                    showtotal();                    
                    
                    this.txtFndPO.Focus();
                }
                else
                {
                    if (tblMain != null)
                    {
                        tblMain.Clear();
                    }
                    DG_POEntry.DataSource = null;
                    DG_POEntry.Rows.Clear();
                    DG_POEntry.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                //this.BSPay.Position = recno;
                //showrecord();
            }

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //Update Current Purchase Order No into the Mastpara Table to Link the Report.
            sqlMastPara = "Update MastPara Set PrintPONo=" + POObj.POno; 
            SetDataObjects(sqlMastPara);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close(); 
            ProjEntry.Rptname = "Rpt_POMain.rdlc";
            frm_ReportViewer Reportform;
            Reportform = new frm_ReportViewer(false);
            Reportform.Show(); 
        }

        private void btnNewItem_Click(object sender, EventArgs e)
        {
            //To Add New Item Into Item Table and Refresh the Item Combo
            if (MessageBox.Show("ARE YOU SURE TO ADD NEW ITEM?", "NEW ITEM Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, 0, false) == DialogResult.Yes)
            {
                if (this.txtNewItem.Text.Trim() != "")
                {
                    string sqlNewItem = "Insert Into Items (ItemName) Values ('" + this.txtNewItem.Text.Trim().ToString().Replace("'", "''") + "')";
                    command = new SqlCommand(sqlNewItem, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Item Successfully Add Into The Database", "New Item");
                    this.btnNewItem.Enabled = false;
                }
            }



        }

        private void txtNewItem_TextChanged(object sender, EventArgs e)
        {
            this.btnNewItem.Enabled = true;

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("ARE YOU SURE TO ADD EXCEL SHEET DATA INTO GRID?", "GET EXCEL DATA Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, 0, false) == DialogResult.Yes)
            {
                LoadExcelData();
                //POObj.GetExcelRecord(POObj.POno);   
                //SetupPODataGridView("PONo", "POData", "sp_GetPORecord", DG_POEntry, "PONo=" + POObj.POno);
                //showtotal();
                //IsExcelData = true;
                //entry1= true;
            }
        }

        private string ExcelFile()
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Browse Excel Files";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "Excel files (*.xls)|*.xls";
            //openFileDialog1.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.ShowReadOnly = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //textBox1.Text = openFileDialog1.FileName;
                return openFileDialog1.FileName;
            }
            else
            { 
              return "Nofile";
            }
        }


        private void LoadExcelData()
        {
            string filename = ExcelFile();
            filename = filename.Replace("\\",@"\");
            char last = filename[filename.Length - 3];                         //value[value.Length - 1];

            if (filename != "Nofile" && last=='x')   
            {
                System.Data.OleDb.OleDbConnection MyConnection;
                System.Data.DataSet DtSet;
                System.Data.OleDb.OleDbDataAdapter MyCommand;
                MyConnection = new System.Data.OleDb.OleDbConnection(@"provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + filename + "';Extended Properties=Excel 8.0;");
                //MyConnection = new System.Data.OleDb.OleDbConnection(@"provider=Microsoft.Jet.OLEDB.4.0;Data Source='D:\PWR\POSheet.xls';Extended Properties=Excel 8.0;");
                MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [Requisition$]", MyConnection);
                MyCommand.TableMappings.Add("Table", "Requisition");
                DtSet = new System.Data.DataSet();
                MyCommand.Fill(DtSet);
                DG_POEntry.DataSource = DtSet.Tables[0];
                MyConnection.Close();

                //int totrec = DtSet.Tables[0].Rows.Count;   

                ExcelTable = DtSet.Tables[0];
                if (DtSet.Tables[0].Rows.Count!=ExcelTable.Rows.Count)
                {
                    ExcelTable.Clear();
                    ExcelTable = DtSet.Tables[0];
                }

                //PostExcelTable(DtSet.Tables[0]);

                SetupPODataGridView("PONo", "POData", "sp_GetPORecord", DG_POEntry, "PONo=" + POObj.POno);
                showtotal();
                IsExcelData = true;
                entry1 = true;
            }

        }


        //private void PostExcelTable(DataTable POTable)
        //{
        //    int ent = 1;
        //    foreach (DataRow row in POTable.Rows)
        //    {
        //        POObj.Itmspec = row["Specification"].ToString();
        //        POObj.Itmid = 648;
        //        if (POObj.Itmspec != "" || POObj.Itmspec != null)
        //        {
        //            bool result;
        //            //if (float.Parse(row["Qty"].ToString() != string.Empty ? row["Qty"].ToString() : "0") != 0)
        //            //{
        //                POObj.Entno = ent;
        //                POObj.Unit = row["Unit"].ToString();
        //                POObj.Qty = float.Parse(row["Qty"].ToString() != string.Empty ? row["Qty"].ToString() : "0");
        //                POObj.Rate = double.Parse(row["Rate"].ToString() != string.Empty ? row["Rate"].ToString() : "0");

        //                ArrayList fieldlist = new ArrayList();
        //                //Passwing property values to arraylist
        //                fieldlist.Add(POObj.POno);
        //                fieldlist.Add(POObj.Entno);
        //                fieldlist.Add(POObj.Itmid);
        //                fieldlist.Add(POObj.Itmspec);
        //                fieldlist.Add(POObj.Unit);
        //                fieldlist.Add(POObj.Qty);
        //                fieldlist.Add(POObj.Rate);

        //                result = POObj.InsertExcelRecord(fieldlist, "I");

        //                if (result == true)
        //                {
        //                    //MessageBox.Show("Record Has Been Successfully Added!");
        //                }
        //                else
        //                {
        //                    //MessageBox.Show("Record Doest Not Saved!!!!!");
        //                    //this.btnSave.Text = "&Save";
        //                }
        //                ent++;
        //            //}
        //        }
        //    }
        //}


        private void DG_POEntry_Enter(object sender, EventArgs e)
        {
            int celno;
            int yCoord = DG_POEntry.CurrentCellAddress.Y; // You can get X if you need it.

            if (DG_POEntry.Rows.Count > 1)
            {
                DG_POEntry.Rows[0].Cells[1].Selected = true;

                celno = int.Parse(DG_POEntry.CurrentCell.ColumnIndex.ToString());

                // && newvendor == true
                if (celno == 0)
                {
                    if (tblDetail.Rows[0]["ItemID"].ToString() == "")
                    {
                        //MessageBox.Show(tblDetail.Rows[0]["ItemID"].ToString());
                        return; 
                    }

                    int tblitem;
                    tblitem = int.Parse(tblDetail.Rows[0]["ItemID"].ToString());
                    if (tblitem != 648)
                    {

                        if (DG_POEntry.Rows[yCoord].Cells["ItemID"].GetType() == typeof(DataGridViewComboBoxCell))
                        {
                            DG_POEntry.Columns.Remove("ItemID");
                            DataGridViewComboBoxColumn Itemcol = new DataGridViewComboBoxColumn();
                            Itemcol = POObj.combocolumn("ItemName", "ItemID", "Items", "");
                            Itemcol.Name = "ItemID";
                            Itemcol.SortMode = DataGridViewColumnSortMode.Automatic;
                            DG_POEntry.Columns.Insert(0, Itemcol);
                            Itemcol.HeaderText = "Item Name";
                            Itemcol.DefaultCellStyle.Font = new Font("Calibri", 8, FontStyle.Regular);
                            Itemcol.Width = 405;
                            //Itemcol.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                            Itemcol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            Itemcol.ReadOnly = false;
                            Itemcol.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;

                        }
                    }
                }
            }


        }

        private void txtcontact_Leave(object sender, EventArgs e)
        {
            cboDepartment.DroppedDown=true;
            cboDepartment.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            this.txtcontact.Focus();
            this.btnSave.Text = "Update";
            newpo = false;
        }

        private void btnDeletePO_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ARE YOU SURE TO DELETE THIS PURCHASE ORDER ENTRIES?", "DELETE PO ENTRIES Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, 0, false) == DialogResult.Yes)
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@tblname", "PODetail");
                param[1] = new SqlParameter("@wherecond", "Pono=" + POObj.POno);

                ConnectionManager obj = new ConnectionManager();
                bool result;
                result = obj.DeleteRecord(param);
                if (result == true)
                {
                    MessageBox.Show("Record Has Been Successfully Delete");
                    //   MessageBoxIcon.Information
                }






            }
        }



    }
}
