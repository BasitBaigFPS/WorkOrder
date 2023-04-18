using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDSystem.BLL
{
    class BackupCode
    {

       private void button43_Click(object sender, EventArgs e)//to extract frame{string outPath = txtExtBitmap.Text;//path of destination folder where the extracted frames are keptSystem.IO.Directory.CreateDirectory(outPath); if (fg != null){foreach (FrameGrabber.Frame f in fg){using (f){picBoxFrame.Image = (Bitmap)f.Image.Clone();f.Image.Save(System.IO.Path.Combine(outPath, "frame" + f.FrameIndex + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);//save each frame in *.bmp formatApplication.DoEvents();} if (fg == null){return;}}}private void button43_Click(object sender, EventArgs e)//to extract frame
        {
            //string outPath = txtExtBitmap.Text;//path of destination folder where the extracted frames are kept
            //System.IO.Directory.CreateDirectory(outPath);

            //if (fg != null)
            //{
            //foreach (FrameGrabber.Frame f in fg)
            //{
            //using (f)
            //{
            //picBoxFrame.Image = (Bitmap)f.Image.Clone();
            //f.Image.Save(System.IO.Path.Combine(outPath, "frame" + f.FrameIndex + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);//save each frame in *.bmp format
            //Application.DoEvents();
            //}

            //if (fg == null)
            //{
            //return;
            //}
            //}
        }

        
        
        //Update Button Click
        //private void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    DataTable dt = ds.Tables["Orders"];
        //    this.dgv.BindingContext[dt].EndCurrentEdit();
        //    this.da.Update(dt);
        // }

        //private void dgv_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        //{
        //    if (!e.Row.IsNewRow)
        //    {
        //        DialogResult res = MessageBox.Show("Are you sure you want to delete this row?", "Delete confirmation",
        //                 MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //        if (res == DialogResult.No)
        //            e.Cancel = true;
        //    }
        // }


        private void DG_Master_CellFormatting(object sender, System.Windows.Forms.DataGridViewCellFormattingEventArgs e)
        {
            //------------------------------------------------
            //int lastrow = 0;
            //lastrow = DG_Master.RowCount -2;
            //Int32 lastrow = this.DG_Master.Rows.GetLastRow(DataGridViewElementStates.ReadOnly);


            //DataGridViewRow mrow = DG_Master.Rows[lastrow];
            //------------------------------------------------
            //row.Cells[1].Value = "Gross Total";
            //row.Cells[1].Style.Font = new Font(DG_Detail.Font, FontStyle.Bold);
            //int rowNumber = 1;
            //float cst = 0;
            //float pad = 0;
            //foreach (DataGridViewRow row in DG_Master.Rows)
            //{
            //    if (row.IsNewRow) continue;
            //    //row.HeaderCell.Value = "Row " + rowNumber;
            //    //rowNumber = rowNumber + 1;
            //    cst = cst + float.Parse(row.Cells[2].Value.ToString());
            //    pad = pad + float.Parse(row.Cells[3].Value.ToString());
            //}
            //-----------------------------------------------------------------------
            //mrow1.Cells[1].Value = "Gross Total";
            //mrow.Cells[2].Value = cst;
            //mrow.Cells[3].Value = pad;
            //mrow1.Cells[1].Style.Font = new Font(DG_Master.Font, FontStyle.Bold);
            //mrow1.Cells[2].Style.Font = new Font(DG_Master.Font, FontStyle.Bold);
            //mrow1.Cells[3].Style.Font = new Font(DG_Master.Font, FontStyle.Bold);
            //------------------------------------------------------------------------
            //DG_Master.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }

        private void DG_Detail_CellFormatting(object sender, System.Windows.Forms.DataGridViewCellFormattingEventArgs e)
        {
            //mrow2.Cells[0].Value = "Gross Total";
            //mrow2.Cells[0].Style.Font = new Font(DG_Detail.Font, FontStyle.Bold);
            //mrow2.Cells[1].Style.Font = new Font(DG_Detail.Font, FontStyle.Bold);
            //mrow2.Cells[2].Style.Font = new Font(DG_Detail.Font, FontStyle.Bold);
            //mrow2.Cells[3].Style.Font = new Font(DG_Detail.Font, FontStyle.Bold);
            ////------------------------------------------------------------------------
            //DG_Master.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }

        private void SetupLayout()
        {
            ////this.Size = new Size(600, 500);

            //addNewRowButton.Text = "Add Row";
            //addNewRowButton.Location = new Point(10, 10);
            //addNewRowButton.Click += new EventHandler(addNewRowButton_Click);

            //deleteRowButton.Text = "Delete Row";
            //deleteRowButton.Location = new Point(100, 10);
            //deleteRowButton.Click += new EventHandler(deleteRowButton_Click);

            //panel2.Controls.Add(addNewRowButton);
            ////buttonPanel.Controls.Add(addNewRowButton);
            //panel2.Controls.Add(deleteRowButton);
            ////buttonPanel.Height = 50;
            ////buttonPanel.Dock = DockStyle.Bottom;
            ////this.Controls.Add(this.buttonPanel);
        }


        //attach the BindingSource to the DataGridView.
        //this.customersDataGridView1.DataSource = this.customersBindingSource1;
        //gets the total number of the items.
        //int count = this.flagsBindingSource.Count;
        //gets or sets the index of the current item.
        //int pos = this.customersBindingSource1.Position;
        //moves to the last item in the list.
        //MoveLast() OR equivalent
        //this.bindingSource1.Position = this.bindingSource1.Count - 1;

        //    private void txtDummie_Enter(object sender, EventArgs e) 
        //{
        //txtDummie.BackColor=Color.Red;
        //}


                    //DG_Master.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DG_Master.MultiSelect = false;
            
            //---------------------------------------------
            //Setting Up Columns
            //---------------------------------------------
            //this.DG_Master.Columns["EstimateNo"].Visible = false;
            //DataGridViewTextBoxColumn Column0 = new DataGridViewTextBoxColumn();
            //Column0.DataPropertyName = "WorkNo";
            //Column0.HeaderText = "Work_ID";
            //Column0.Width = 80;
            //Column0.ReadOnly = true;
            //Column0.Frozen = true;
            //DG_Master.Columns.Add(Column0);

            //DataGridViewTextBoxColumn Column1 = new DataGridViewTextBoxColumn(); 
            //Column1.DataPropertyName = "WorkDesc";
            //Column1.HeaderText = "Work Description";
            //Column1.Width = 350;
            //DG_Master.Columns.Add(Column1);

            //DataGridViewTextBoxColumn Column2 = new DataGridViewTextBoxColumn();
            //Column2.DataPropertyName = "Cost";
            //Column2.HeaderText = "Approved Amount";
            //Column2.Width = 100;
            //Column2.DefaultCellStyle.Format = "##,0";
            ////= "c"
            //Column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; 
            //DG_Master.Columns.Add(Column2);
            
            //DataGridViewTextBoxColumn Column3 = new DataGridViewTextBoxColumn();
            //Column3.DataPropertyName = "Paid";
            //Column3.HeaderText = "Paid Amount";
            //Column3.Width = 100;
            //Column3.DefaultCellStyle.Format = "##,0";
            //Column3.ReadOnly = true;
            //Column3.DefaultCellStyle.NullValue = String.Empty; 
            //Column3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; 
            //DG_Master.Columns.Add(Column3);

            //DataGridViewTextBoxColumn Column4 = new DataGridViewTextBoxColumn();
            //Column4.DataPropertyName = "Balance";
            //Column4.HeaderText = "Balance";
            //Column4.Width = 100;
            //Column4.DefaultCellStyle.Format = "##,0";
            //Column4.ReadOnly = true;
            //Column4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; 

            //DG_Master.Columns.Add(Column4);

            //DG_Master.SelectionMode = DataGridViewSelectionMode.CellSelect;
            //DG_Master.MultiSelect = false;
            //DG_Master.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 9, FontStyle.Bold);
            ////Font(DG_Master.Font, FontStyle.Bold);
            //DG_Master.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; 
            //DG_Master.ColumnHeadersDefaultCellStyle.BackColor = Color.Aqua;

            ////DG_Master.Dock = DockStyle.Top;
            ////DG_Master.CellFormatting += new DataGridViewCellFormattingEventHandler(DG_Master_CellFormatting);
            //DG_Master.KeyPress += new System.Windows.Forms.KeyPressEventHandler(DG_Master_KeyPress);


        //private void SetupDetailDataGridView2()
        //{
        //    panel5.Controls.Add(this.DG_Detail);
        //    //DataGridView Master Record Setting 
        //    DG_Detail.AutoGenerateColumns = false;
        //    DG_Detail.ReadOnly = true;
        //    DG_Detail.EditMode = DataGridViewEditMode.EditOnEnter;
        //    //DG_Master.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //    //DG_Master.MultiSelect = false;

        //    //---------------------------------------------
        //    //Setting Up Columns
        //    //---------------------------------------------
        //    DataGridViewComboBoxColumn Column0 = new DataGridViewComboBoxColumn();
        //    Column0 = OBJFuncLib.combocolumn("VendorName", "VendorID", "Vendor", "");
        //    Column0.DataPropertyName = "VendorID";
        //    Column0.HeaderText = "Vendor Name";
        //    Column0.Width = 300;
        //    Column0.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        //    DG_Detail.Columns.Add(Column0);
        //    //DG_Detail.Columns.Add(OBJFuncLib.combocolumn("VendorName", "VendorID", "Vendor", ""));

        //    DataGridViewTextBoxColumn Column1 = new DataGridViewTextBoxColumn();
        //    Column1.DataPropertyName = "GrossAmt";
        //    Column1.HeaderText = "Gross Amount";
        //    Column1.Width = 100;
        //    Column1.DefaultCellStyle.Format = "##,0";
        //    Column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    DG_Detail.Columns.Add(Column1);

        //    DataGridViewTextBoxColumn Column2 = new DataGridViewTextBoxColumn();
        //    Column2.DataPropertyName = "PaidAmt";
        //    Column2.HeaderText = "Paid Amount";
        //    Column2.Width = 100;
        //    Column2.ReadOnly = true;
        //    Column2.DefaultCellStyle.Format = "##,0";
        //    Column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    DG_Detail.Columns.Add(Column2);

        //    DataGridViewTextBoxColumn Column3 = new DataGridViewTextBoxColumn();
        //    Column3.DataPropertyName = "BalAmt";
        //    Column3.HeaderText = "Remain Amt";
        //    Column3.Width = 100;
        //    Column3.DefaultCellStyle.Format = "##,0";
        //    Column3.ReadOnly = true;
        //    Column3.DefaultCellStyle.NullValue = String.Empty;
        //    Column3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    DG_Detail.Columns.Add(Column3);

        //    DataGridViewTextBoxColumn Column4 = new DataGridViewTextBoxColumn();
        //    Column4.DataPropertyName = "PayRecmd";
        //    Column4.HeaderText = "Payment Recmd";
        //    Column4.Width = 100;
        //    Column4.DefaultCellStyle.Format = "##,0";
        //    Column4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    DG_Detail.Columns.Add(Column4);

        //    DataGridViewTextBoxColumn Column5 = new DataGridViewTextBoxColumn();
        //    Column5.DataPropertyName = "Discount";
        //    Column5.HeaderText = "Discount";
        //    Column5.Width = 100;
        //    Column5.DefaultCellStyle.Format = "##,0";
        //    Column5.ReadOnly = true;
        //    Column5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    DG_Detail.Columns.Add(Column5);

        //    DataGridViewTextBoxColumn Column6 = new DataGridViewTextBoxColumn();
        //    Column6.DataPropertyName = "PayDetail";
        //    Column6.HeaderText = "Payment Detail";
        //    Column6.Width = 300;
        //    Column6.DefaultCellStyle.Format = "##,0";
        //    Column6.ReadOnly = true;
        //    Column6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        //    DG_Detail.Columns.Add(Column6);

        //    DataGridViewTextBoxColumn Column7 = new DataGridViewTextBoxColumn();
        //    Column7.DataPropertyName = "BillInvoice";
        //    Column7.HeaderText = "Bill_Invoice";
        //    Column7.Width = 100;
        //    Column7.DefaultCellStyle.Format = "##,0";
        //    Column7.ReadOnly = true;
        //    Column7.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        //    DG_Detail.Columns.Add(Column7);


        //    DG_Detail.SelectionMode = DataGridViewSelectionMode.CellSelect;
        //    DG_Detail.MultiSelect = false;
        //    DG_Detail.Dock = DockStyle.Top;

        //    DG_Detail.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 9, FontStyle.Bold);
        //    DG_Detail.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        //    //DG_Detail.CellFormatting += new DataGridViewCellFormattingEventHandler(DG_Detail_CellFormatting);
        //    DG_Detail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(DG_Detail_KeyPress);

        //    //DG_Detail.AutoGenerateColumns = false;
        //    //TblStyle.GridColumnStyles[7].NullText = String.Empty;
        //}


        private void CreateRowsWithItemArray()
        {
            //// Make a DataTable using the function below.
            //DataTable dt = MakeTableWithAutoIncrement();
            //DataRow relation;
            //// Declare the array variable.
            //object[] rowArray = new object[2];
            //// Create 10 new rows and add to DataRowCollection.
            //for (int i = 0; i < 10; i++)
            //{
            //    rowArray[0] = null;
            //    rowArray[1] = "item " + i;
            //    relation = dt.NewRow();
            //    relation.ItemArray = rowArray;
            //    dt.Rows.Add(relation);
            //}
            //PrintTable(dt);
        }

        //private DataTable MakeTableWithAutoIncrement()
        //{
        //    //// Make a table with one AutoIncrement column.
        //    //DataTable table = new DataTable("table");
        //    //DataColumn idColumn = new DataColumn("id",
        //    //    Type.GetType("System.Int32"));
        //    //idColumn.AutoIncrement = true;
        //    //idColumn.AutoIncrementSeed = 10;
        //    //table.Columns.Add(idColumn);

        //    //DataColumn firstNameColumn = new DataColumn("Item",
        //    //    Type.GetType("System.String"));
        //    //table.Columns.Add(firstNameColumn);
        //    //return table;
        //}

        //private void PrintTable(DataTable table)
        //{
        //    foreach (DataRow row in table.Rows)
        //    {
        //        foreach (DataColumn column in table.Columns)
        //        {
        //            Console.WriteLine(row[column]);
        //        }
        //    }
        //}


        //private void DG_Detail_CellLeave(object sender, DataGridViewCellEventArgs e)
        //{

        //    int celno;
        //    celno = int.Parse(e.ColumnIndex.ToString());
        //    //MessageBox.Show(celno.ToString());
        //    //if (celno == 5)
        //    //{
        //    //    showtotal();
        //    //}
        //    //this.DG_Master.CurrentCell.OwningColumn.Name.ToString() != "Paid Amount"
        //    //if (celno != 2 && celno!=6)
        //    //{

        //    if (celno != 13)
        //    {
        //        SendKeys.Send("{UP}");
        //        SendKeys.Send("{RIGHT}");
        //    }
        //    else
        //    {
        //        SendKeys.Send("{HOME}");
        //        SendKeys.Send("{UP}");
        //    }
        //    //}
        //    showtotal();
        //}



        //private void DG_Detail_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        //{


        //    DataGridViewComboBoxCell cell = DG_Detail.CurrentCell as DataGridViewComboBoxCell;
        //    //if (e.ColumnIndex == cell.OwningColumn.DisplayIndex)   
        //    if (payentry1 == true)
        //    {
        //        if (cell != null && !cell.Items.Contains(e.FormattedValue))
        //        {
        //            if (MessageBox.Show("Do You Want to Add New Vendor? " + e.FormattedValue + " ",
        //                       "New Work Entry Alert", MessageBoxButtons.YesNo,
        //                       MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, 0, false)
        //                       == DialogResult.Yes)
        //            {
        //                cell.Items.Add(e.FormattedValue);
        //            }
        //            //    // Insert the new value into position 0
        //            //    // in the item collection of the cell
        //            //    ////cell.Items.Insert(0, e.FormattedValue);
        //            //    // When setting the Value of the cell, the  
        //            //    // string is not shown until it has been
        //            //    // comitted. The code below will make sure 
        //            //    // it is committed directly.
        //            //    if (DG_Detail.IsCurrentCellDirty)
        //            //    {
        //            //        // Ensure the inserted value will 
        //            //        // be shown directly.
        //            //        // First tell the DataGridView to commit 
        //            //        // itself using the Commit context...
        //            //        DG_Detail.CommitEdit (DataGridViewDataErrorContexts.Commit);
        //            //    }
        //            //    // ...then set the Value that needs 
        //            //    // to be committed in order to be displayed directly.
        //            //    cell.Value = cell.Items[0];
        //        }
        //    }
        //}


        //private void DG_Detail_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        //{
        //    if (!e.Row.IsNewRow)
        //    {
        //        DialogResult response = MessageBox.Show("Are you sure?", "Delete Record?",
        //                 MessageBoxButtons.YesNo,
        //                 MessageBoxIcon.Question,
        //                 MessageBoxDefaultButton.Button2);
        //        if (response == DialogResult.No)
        //        {
        //            e.Cancel = true;
        //        }
        //        else
        //        {
        //            //this.DG_Detail.Rows.RemoveAt(this.DG_Detail.SelectedRows[0].Index);
        //            MessageBox.Show("Delete Me...");
        //        }
        //    }
        //}


        //private void DG_Master_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        //{
        //    //TO BE Deleted
        //    // If the key pressed is enter, then call ProceedOpen.
        //    //
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        //SendKeys.Send("{RIGHT}");
        //        //SendKeys.Send("{UP}");
        //        //showtotal();
        //        //ProceedOpen();
        //    }
        //}

        //private void DG_Master_KeyDown(object sender, KeyEventArgs e)
        //{
        //    //TO BE Deleted
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        //SendKeys.Send("{UP}");
        //        //SendKeys.Send("{RIGHT}");
        //        showtotal();
        //    }
        //}

        //private void DG_Master_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    //TO BE Deleted
        //     if ( e.KeyValue == 46 ) // Delete Key
       //     if (e.KeyChar == (char)Keys.Enter)
        //    {
        //        e.Handled = true;
        //        if (this.DG_Master.CurrentCell.OwningColumn.Name.ToString() != "Paid")
        //        {
        //            SendKeys.Send("{UP}");
        //            SendKeys.Send("{RIGHT}");
        //        }
        //        else
        //        {
        //            SendKeys.Send("{UP}");
        //            SendKeys.Send("{HOME}");
        //        }
        //    }
        //    else
        //    {
        //        e.Handled = false;
        //    }
        //}

        //private void DG_Master_CellLeave(object sender, DataGridViewCellEventArgs e)
        //{
        //    int celno;
        //    celno = int.Parse(e.ColumnIndex.ToString());
        //    //if (celno==5) 
        //    //{
        //    //    showtotal(); 
        //    //}
        //    //this.DG_Master.CurrentCell.OwningColumn.Name.ToString() != "Approved Amount"


        //    if (celno == 5)
        //    {
        //        // SendKeys.Send("{HOME}");
        //        // SendKeys.Send("{UP}");
        //    }
        //    else
        //    {
        //        // SendKeys.Send("{UP}");
        //        // SendKeys.Send("{RIGHT}");
        //    }

        //}

        //private void DG_Master_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        //{
        //    //DataGridViewComboBoxEditingControl comboControl = e.Control as DataGridViewComboBoxEditingControl;
        //    DataGridViewTextBoxEditingControl txtbox = e.Control as DataGridViewTextBoxEditingControl;

        //    e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
        //    e.Control.KeyDown += new KeyEventHandler(Control_KeyDown);
        //    //new KeyDownEventHandler(Control_KeyDown);

        //    e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(Control_PreviewKeyDown);

        //    //Convert entered Text in Upper Case
        //    //if (txtbox.Text != null)
        //    //{
        //    //    txtbox.CharacterCasing = CharacterCasing.Upper;   
        //    //}

        //    MessageBox.Show(this.DG_Master.CurrentCell.ColumnIndex.ToString());

        //    if (this.DG_Master.CurrentCell.ColumnIndex == 2)
        //    {

        //    }
        //    else
        //    {
        //        SendKeys.Send("{UP}");
        //        SendKeys.Send("{RIGHT}");
        //    }

        //    //if (comboControl != null)
        //    //{
        //    //    // Set the DropDown style to get an editable ComboBox
        //    //    if (comboControl.DropDownStyle != ComboBoxStyle.DropDown)
        //    //    {
        //    //        comboControl.DropDownStyle = ComboBoxStyle.DropDown;
        //    //    }
        //    //}

        //}

        //void Control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        //{
        //    //throw new NotImplementedException();
        //    //MessageBox.Show(e.KeyValue.ToString());  
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        SendKeys.Send("{UP}");
        //        SendKeys.Send("{RIGHT}");

        //       // showtotal();
        //    }

        //}

        //void Control_KeyDown(object sender, KeyEventArgs e)
        //{
        //    //throw new NotImplementedException();
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        SendKeys.Send("{RIGHT}");
        //        SendKeys.Send("{UP}");
        //       // showtotal();
        //    }
        //}

        //private void Control_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    //Only Numeric Values enter in the text box  
        //    //if (this.DG_Detail.CurrentCell.OwningColumn.Name.ToString() != "VendorID")
        //    //&& this.DG_Detail.CurrentCell.OwningColumn.Name.ToString() != "Payment Detail")
        //    //if (this.DG_Detail.CurrentCell.ColumnIndex != 2 && this.DG_Detail.CurrentCell.ColumnIndex != 13)
        //    //{
        //    //    int ascii = Convert.ToInt16(e.KeyChar);
        //    //    if ((ascii >= 48 && ascii <= 57) || (ascii == 8) || (ascii == 46))
        //    //    {
        //    //        e.Handled = false;
        //    //    }
        //    //    else
        //    //    {
        //    //        e.Handled = true;
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    if (e.KeyChar == (char)Keys.Enter)
        //    //    {
        //    //        e.Handled = true;
        //    //        if (this.DG_Detail.CurrentCell.OwningColumn.Name.ToString() != "Payment Detail")
        //    //        {
        //    //            SendKeys.Send("{UP}");
        //    //            SendKeys.Send("{RIGHT}");
        //    //        }
        //    //        else
        //    //        {
        //    //            SendKeys.Send("{UP}");
        //    //            SendKeys.Send("{HOME}");
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        e.Handled = false;
        //    //    }

        //    //}
        //}


     //       DataTable dt = new DataTable();
     //    dt.Columns.Add("test");
     //    dt.Rows.Add("1");
     //    dt.Rows.Add("2");
     //    dt.Rows.Add("3");
     //    dt.Rows.Add("4");

     //    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
     //    dataGridView1.DataSource = dt;
     //    dataGridView1.MultiSelect = true;

     //     //the following two lines will have no effect.
     //     dataGridView1.Rows[2].Selected = true;
     //     dataGridView1.Rows[4].Selected = true;
     //}

     //The solution is to bind to the DataBindingComplete event.

     //Change the last two code lines like this, will make it work:

     //    dataGridView1.DataBindingComplete += delegate
     //    {
     //        dataGridView1.Rows[2].Selected = true;
     //        dataGridView1.Rows[4].Selected = true;
     //    };


        //private void frm_ReportViewer1_Load(object sender, EventArgs e)
        //{
        //    sqlQuery = "Select * from View_ProjEstimate";
        //    SetDataObjects(); 
        //    connection.Open();



        //    ReportDocument rptd = new ReportDocument();
        //    rptd.Load(Application.StartupPath + "\\CR_ProjEstimate.rpt");
        //    ds = new DataSet();
        //    adapter.Fill(ds);
        //    rptd.SetDataSource(ds.Tables[0]);
        //    this.crystalReportViewer1.ReportSource = rptd;
        //    this.crystalReportViewer1.Show();

        //}


        // Reset all the controls to the user's default Control color. 
        //private void ResetAllControlsBackColor(Control control)
        //{
        //    control.BackColor = SystemColors.Control;
        //    control.ForeColor = SystemColors.ControlText;
        //    if (control.HasChildren)
        //    {
        //        // Recursively call this method for each child control.
        //        foreach (Control childControl in control.Controls)
        //        {
        //            ResetAllControlsBackColor(childControl);
        //        }
        //    }
        //}


        //private void BrowseMultipleButton_Click(object sender, EventArgs e)
        //{

        //    this.openFileDialog1.Filter =

        //    "Images (*.BMP;*.JPG;*.GIF,*.PNG,*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF|" +

        //    "All files (*.*)|*.*";



        //    this.openFileDialog1.Multiselect = true;

        //    this.openFileDialog1.Title = "Select Photos";



        //    DialogResult dr = this.openFileDialog1.ShowDialog();

        //    if (dr == System.Windows.Forms.DialogResult.OK)
        //    {

        //        foreach (String file in openFileDialog1.FileNames)
        //        {

        //            try
        //            {

        //                PictureBox imageControl = new PictureBox();

        //                imageControl.Height = 100;

        //                imageControl.Width = 100;



        //                Image.GetThumbnailImageAbort myCallback =

        //                        new Image.GetThumbnailImageAbort(ThumbnailCallback);

        //                Bitmap myBitmap = new Bitmap(file);

        //                Image myThumbnail = myBitmap.GetThumbnailImage(96, 96,

        //                    myCallback, IntPtr.Zero);

        //                imageControl.Image = myThumbnail;



        //                PhotoGallary.Controls.Add(imageControl);

        //            }

        //            catch (Exception ex)
        //            {

        //                MessageBox.Show("Error: " + ex.Message);

        //            }

        //        }

        //    }

        //}

        //public bool ThumbnailCallback()
        //{

        //    return false;

        //}

















    }
}
