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
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using WordAmount;



namespace PDSystem
{
    public partial class frm_Payments : Form
    {
        MyFunctions OBJFuncLib = new MyFunctions();
        Payments PAYObj = new Payments();
        ProjEntry PEObj = new ProjEntry();

        AmountInWords rupeeword = new AmountInWords();


        private string sqlQuery;
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        //private SqlCommandBuilder builder;
        //private DataSet ds;
        private DataTable MainTable;
        private DataTable userTable;
        private DataTable PaidTable;
        private Double totalPamt;
        private Double totalUamt;
        
        
        public frm_Payments()
        {
            InitializeComponent();

         //   this.BNavPay.BindingSource = this.BSPay;  
           // this.Controls.Add(this.BNavPay);


        }

        private void frm_Payments_Load(object sender, System.EventArgs e)
        {
            SetForm(); 
           
        }

        private void SetDataObjects()
        {
            ConnectionManager conobj = new ConnectionManager();
            connection = new SqlConnection(conobj.conn);
            command = new SqlCommand(sqlQuery, connection);
            adapter = new SqlDataAdapter(command);

        }

        private void SetForm()
        {
            try
            {
                //string sqlpay = "SELECT EstMainID,EstimateID,EstimateNo,BranchID,ProjtypeID,SystemID,Description,EstDate,Acdyear,UsrID,UsrDT FROM EstMain where UsrID=" + Login.Cuserid;
                //sqlQuery = "SELECT * FROM [" + cmbTables.Text.Trim() + "]";
                //sqlQuery = "SELECT EstimateNo,Workno,PayID,EntDate,VendorID,GlCode,PaidAmt,PayRecmd,Taxamt,Amount,DueDate,PayDetail,Hold,ChqReady,PaidStatus,Chqno,ChqDate,BkAccount,IssuedDate,IssuedBy,RecvdBy,Remarks,UsrID,UsrDT from payments";

                MessageBox.Show("Please Enter Project/Work ID to Get the Recommended Payment Record!");
                //if (MessageBox.Show("No Project/Work Found Reference to you User ID!,Do You Want to Create New Project?",
                        //    "New User Entry Alert", MessageBoxButtons.YesNo,
                        //    MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, 0, false)
                        //    == DialogResult.Yes)
                        //{
                        //   MessageBox.Show("No Project/Work Found, Please Enter Correct!");
                        //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.txtFindPwr.Focus();  
                //this.txtUserID.Text = Login.Cuser;
            }

        }

        private void showrecord()
        {

            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (userTable != null)
                {
                    userTable.Clear();
                }
                DG_Paid.DataSource = null;
                DG_Paid.Columns.Clear();
                DG_Paid.Refresh();

                DG_Unpaid.DataSource = null;
                DG_Unpaid.Rows.Clear();
                DG_Unpaid.Refresh();

                SetDataObjects();
                connection.Open();
                userTable = new DataTable();
                adapter.Fill(userTable);

                if (userTable.Rows.Count == 0)
                {
                    totalUamt = 0;
                    ShowPaidRecord();
                }
                else
                {
                    DG_Unpaid.DataSource = userTable.DefaultView;
                    DG_Unpaid.AutoGenerateColumns = false;
                    
                    DG_Unpaid.Columns.Clear();  
                    foreach (DataColumn dc in userTable.Columns)
                    {

                        if (dc.ColumnName == "VendorID" || dc.ColumnName == "PayDetail" || dc.ColumnName == "PayRecmd" || dc.ColumnName == "ChqReady" || dc.ColumnName == "Remarks")
                        {

                            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                            column.DataPropertyName = dc.ColumnName;
                            column.HeaderText = dc.ColumnName;

                            if (dc.ColumnName == "ChqReady")
                            {
                                DataGridViewCheckBoxColumn chkbx_key = new DataGridViewCheckBoxColumn();

                                chkbx_key = new DataGridViewCheckBoxColumn();
                                chkbx_key.Width = 40;
                                chkbx_key.FlatStyle = FlatStyle.Popup;
                                chkbx_key.DisplayIndex = 3;
                                DG_Unpaid.Columns.Insert(3, chkbx_key);

                                chkbx_key.DataPropertyName = dc.ColumnName;
                                chkbx_key.ValueType = dc.DataType;
                                chkbx_key.HeaderText = "Confirm Payment";
                                chkbx_key.Width = 100;
                                //DG_Unpaid.Columns.Add(chkbx_key);
                            }
                            else
                            {

                                if (dc.ColumnName == "VendorID")
                                {
                                    DataGridViewTextBoxColumn VndrName = new DataGridViewTextBoxColumn();
                                    int currentRow ;
                                    if (DG_Unpaid.CurrentCell==null)
                                    {
                                       currentRow = 0;
                                    }
                                    else
                                    {
                                        currentRow = DG_Unpaid.CurrentCell.RowIndex;
                                    }
                                    int vdrid = int.Parse(userTable.Rows[currentRow]["VendorID"].ToString());
  
                                    //int vdrid = int.Parse(DG_Unpaid.Rows[currentRow].Cells["VendorID"].ToString());

                                    VndrName.Width = 300;
                                    VndrName.DisplayIndex = 0;
                                    DG_Unpaid.Columns.Add("VendorName", "Vendor/Supplier Name");
                                    DG_Unpaid.Rows[currentRow].Cells["VendorName"].Value = PEObj.DLookup("VendorName", "Vendor", "VendorID=" + vdrid);
                                    DG_Unpaid.Columns["VendorName"].Width = 300;
                                    DG_Unpaid.Columns["VendorName"].ReadOnly = true;
                                    //DG_Unpaid.Rows[currentRow].Cells["VendorName"].Value = PEObj.DLookup("VendorName", "Vendor", "VendorID=" + vdrid);
                                    //column.Width = 300;
                                    //column.ReadOnly = true;
                                }
                                else
                                {
                                    column.Name = dc.ColumnName;
                                    column.SortMode = DataGridViewColumnSortMode.Automatic;
                                    column.ValueType = dc.DataType;
                                    DG_Unpaid.Columns.Add(column);
                                }
                            }

                            switch (column.DataPropertyName)
                            {
                                case "VendorName":

                                    //dc.Caption = "This is a Vendor Name";
                                    column.Width = 300;
                                    column.ReadOnly = true;
                                    break;

                                case "VendorID":
                                    column.Visible = false;
                                    //column.HeaderText = "Vendor/Supplier Name";
                                    ////dc.Caption = "This is a Vendor Name";
                                    //column.Width = 300;
                                    //column.ReadOnly = true;
                                    break;

                                case "PayDetail":
                                    column.HeaderText = "Payment Details";
                                    column.Width = 300;
                                    break;

                                case "PayRecmd":
                                    column.HeaderText = "Amount";
                                    column.Width = 100;
                                    column.DefaultCellStyle.Format = "##,0.00";
                                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                    break;

                                //case "ChqReady":
                                //    column.HeaderText = "Confirm Payment";
                                //    column.Width = 100;
                                //    column.DefaultCellStyle.Format = "##,0";
                                //    break;

                                case "Remarks":
                                    column.HeaderText = "Remarks";
                                    column.Width = 300;
                                    break;
                                default:
                                    column.Visible = false;
                                    break;
                            }

                        }
                        else
                        {
                          //  column.Visible = false;
                          //  break;
                        }
                    }
                    //------
                    float amt = 0;
                    DataRow row;
                    row = userTable.NewRow();
                    for (int i = 0; i < userTable.Rows.Count; i++)
                    {
                        DG_Unpaid.Rows[i].Cells["VendorName"].Value = PEObj.DLookup("VendorName", "Vendor", "VendorID=" + int.Parse(userTable.Rows[i]["VendorID"].ToString()));
                        amt = amt + float.Parse(userTable.Rows[i]["Payrecmd"].ToString());
                    }
                    row[1] = "Total";
                    row[2] = amt.ToString();
                    userTable.Rows.Add(row);
                    totalUamt = amt;
                    this.txtRupees.Text = "Rupees: " + rupeeword.changeCurrencyToWords(totalUamt);
                    DG_Unpaid.Rows[DG_Unpaid.RowCount - 2].Cells[1].Style.Font = new Font(DG_Unpaid.Font, FontStyle.Bold);
                    DG_Unpaid.Rows[DG_Unpaid.RowCount - 2].Cells[2].Style.Font = new Font(DG_Unpaid.Font, FontStyle.Bold);

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
                //DG_Unpaid.DataSource = userTable.DefaultView;
                DG_Unpaid.AutoGenerateColumns = false;
                DG_Unpaid.AllowUserToResizeColumns = true;
            }

            //MakeColTotal(userTable);

            DG_Unpaid.CurrentCellDirtyStateChanged += new EventHandler(DG_Unpaid_CurrentCellDirtyStateChanged); 
            DG_Unpaid.CellValueChanged += new DataGridViewCellEventHandler(DG_Unpaid_CellValueChanged);
            
            
        }

        private void txtFindPwr_TextChanged(object sender, System.EventArgs e)
        {
            //--
        }

        private void MakeColTotal(DataTable tbl)
        {
            DataRow dr;
            //-----Making Total Row -------
            dr = tbl.NewRow();
            float amt = 0;
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
              amt = amt + float.Parse(tbl.Rows[i]["Payrecmd"].ToString());
            }
            dr[2] = "Total";
            dr[3] = amt.ToString();
            tbl.Rows.Add(dr);
        }


        private void ShowPaidRecord()
        {

            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (PaidTable != null)
                {
                    PaidTable.Clear();
                }
                DG_Paid.DataSource = null;
                DG_Paid.Rows.Clear();
                DG_Paid.Refresh();
                sqlQuery = "SELECT VendorID,PayDetail,PaidAmt,Remarks,EntDate,Chqno,ChqDate,BkAccount from payments where EstimateNo=" + int.Parse(this.txtFindPwr.Text) + " and not PaidAmt is Null and iscancelled is null Order By Workno, PayID";
                SetDataObjects();
                connection.Open();
                PaidTable = new DataTable();
                adapter.Fill(PaidTable);
                
                if (PaidTable.Rows.Count == 0)
                {
                    totalPamt = 0;
                    MessageBox.Show("No Payment has been Done as yet for this Project/Work ID! / Or This Work Order might be Cancelled!");
                    //return true;
                    //MessageBox.Show("No Project/Work Found, Please Enter Correct!");
                }
                else
                {

                    DG_Paid.DataSource = PaidTable.DefaultView;
                    DG_Paid.AutoGenerateColumns = false;
                    
                    //ShowProjectMainInfo();
                    DG_Paid.Columns.Clear();
                    foreach (DataColumn dc in PaidTable.Columns)
                    {

                        if (dc.ColumnName == "VendorID" || dc.ColumnName == "PayDetail" || dc.ColumnName == "PaidAmt" || dc.ColumnName == "Remarks")
                        {

                            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                            column.DataPropertyName = dc.ColumnName;
                            column.HeaderText = dc.ColumnName;

                            if (dc.ColumnName == "VendorID")
                            {
                                    DataGridViewTextBoxColumn VndrName = new DataGridViewTextBoxColumn();
                                    int currentRow;
                                    if (DG_Paid.CurrentCell == null)
                                    {
                                        currentRow = 0;
                                    }
                                    else
                                    {
                                        currentRow = DG_Paid.CurrentCell.RowIndex;
                                    }
                                    int vdrid = int.Parse(PaidTable.Rows[currentRow]["VendorID"].ToString());
                                    VndrName.Width = 300;
                                    VndrName.DisplayIndex = 0;
                                    DG_Paid.Columns.Add("VendorName", "Vendor/Supplier Name");
                                    DG_Paid.Rows[currentRow].Cells["VendorName"].Value = PEObj.DLookup("VendorName", "Vendor", "VendorID=" + vdrid);
                                    DG_Paid.Columns["VendorName"].Width = 300;
                                    DG_Paid.Columns["VendorName"].ReadOnly = true;
                            }
                            column.Name = dc.ColumnName;
                            column.SortMode = DataGridViewColumnSortMode.Automatic;
                            column.ValueType = dc.DataType;
                            DG_Paid.Columns.Add(column);

                            switch (column.DataPropertyName)
                            {
                                case "VendorName":

                                    //dc.Caption = "This is a Vendor Name";
                                    column.Width = 300;
                                    column.ReadOnly = true;
                                    break;

                                case "VendorID":
                                    column.Visible = false;
                                    //column.HeaderText = "Vendor/Supplier Name";
                                    ////dc.Caption = "This is a Vendor Name";
                                    //column.Width = 300;
                                    //column.ReadOnly = true;
                                    break;

                                case "PayDetail":
                                    column.HeaderText = "Payment Details";
                                    column.Width = 300;
                                    break;

                                case "PaidAmt":
                                    column.HeaderText = "Amount";
                                    column.Width = 100;
                                    column.DefaultCellStyle.Format = "##,0";
                                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                    break;

                                case "Remarks":
                                    column.HeaderText = "Remarks";
                                    column.Width = 300;
                                    break;
                                default:
                                    column.Visible = false;
                                    break;
                            }

                        }
                        else
                        {
                            //  column.Visible = false;
                            //  break;
                        }
                    }
                    float amt = 0;
                    DataRow row;
                    row = PaidTable.NewRow();
                    for (int i = 0; i < PaidTable.Rows.Count; i++)
                    {
                        DG_Paid.Rows[i].Cells["VendorName"].Value = PEObj.DLookup("VendorName", "Vendor", "VendorID=" + int.Parse(PaidTable.Rows[i]["VendorID"].ToString()));
                        amt = amt + float.Parse(PaidTable.Rows[i]["PaidAmt"].ToString());
                    }
                    row[1] = "Total";
                    row[2] = amt.ToString();
                    PaidTable.Rows.Add(row);
                    totalPamt = amt;
                    this.txtPaid.Text = "Rupees: " + rupeeword.changeCurrencyToWords(totalPamt);
                    DG_Paid.Rows[DG_Paid.RowCount - 2].Cells[3].Style.Font = new Font(DG_Paid.Font, FontStyle.Bold);
                    DG_Paid.Rows[DG_Paid.RowCount - 2].Cells[4].Style.Font = new Font(DG_Paid.Font, FontStyle.Bold);

                    //return false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //return false;
            }
            finally
            {
                
                connection.Close();
                this.Cursor = Cursors.Default;
                DG_Paid.AutoGenerateColumns = false;
                DG_Paid.AllowUserToResizeColumns = true;
                //return false;
            }

        }



        void DG_Unpaid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (DG_Unpaid.IsCurrentCellDirty)
                DG_Unpaid.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        
        private void DG_Unpaid_CellValueChanged(object obj, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex==3)
            //{ 
            //}
            //if (e.ColumnIndex == DG_Unpaid.Columns[3].Index) //compare to checkBox column index
            {
                foreach (DataGridViewRow row in DG_Unpaid.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        DataGridViewCheckBoxCell checkBox = DG_Unpaid[3, row.Index] as DataGridViewCheckBoxCell;
                        if (checkBox != null)
                        {
 

                            if (checkBox.Value.ToString()=="1")
                                DG_Unpaid.Rows[row.Index].Selected = true;
                            else
                                DG_Unpaid.Rows[row.Index].Selected = false;

                            //MessageBox.Show(checkBox.Value.ToString());
                        }
                    }
                }
            }
        }



        private void ShowProjectMainInfo()
        {
            try
            {
                sqlQuery = "SELECT EstimateNo,Description,UsrID from EstMain where EstimateNo=" + int.Parse(this.txtFindPwr.Text);

                SetDataObjects();
                connection.Open();
                MainTable = new DataTable();
                adapter.Fill(MainTable);

                txtEstno.Text = MainTable.Rows[0]["EstimateNo"].ToString();
                txtProjTitle.Text = MainTable.Rows[0]["Description"].ToString();
                txtusrid.Text = MainTable.Rows[0]["UsrID"].ToString();
                txtReqBy.Text = OBJFuncLib.DLookup("Usrname", "puser", "usrID=" + int.Parse(txtusrid.Text)); 

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


        private void btnUpdatePayment_Click(object sender, EventArgs e)
        {
            try
            {
                //connection.Open();
                //adapter.Update(userTable);
                //DG_Unpaid.ReadOnly = true;

                DialogResult DResult = MessageBox.Show("Are You Sure to Confirm These Payments?","Payment Posting Alert", MessageBoxButtons.YesNo,
             MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, 0, false);
                
                if (DResult == DialogResult.Yes)
                {
                    int estid = 0;
                    int wrkid = 0;
                    int payid = 0;
                    string chqr;
                    string remark;
                    double payrc = 0;
                    bool result = false;
                    DateTime paydate = DateTime.Now;

                    ArrayList paymentlist = new ArrayList();

                    foreach (DataRow row in userTable.Rows)
                    { 
                        chqr  = row["ChqReady"].ToString();
                        if (chqr == "True")
                        {
                            estid = int.Parse(row["EstimateNo"].ToString());
                            wrkid = int.Parse(row["WorkNo"].ToString());
                            payid = int.Parse(row["PayID"].ToString());
                            remark = row["Remarks"].ToString();
                            payrc = double.Parse(row["PayRecmd"].ToString());
                            paydate = DateTime.Now;

                            paymentlist.Add(wrkid);
                            paymentlist.Add(payid);
                            paymentlist.Add(payrc);

                            ArrayList fieldlist = new ArrayList();

                            //Passwing property values to arraylist
                            fieldlist.Add(estid);
                            fieldlist.Add(wrkid);
                            fieldlist.Add(payid);
                            fieldlist.Add(0);
                            fieldlist.Add(payrc);
                            fieldlist.Add(paydate);
                            fieldlist.Add(remark);
                            fieldlist.Add(Login.Cuserid);
                            fieldlist.Add("U");
                            result = PEObj.PostPaymentRecord(fieldlist, "U");
                     
                        }
                    }
                    if (result == true)
                    {
                        MessageBox.Show("Payment Successfully Posted!!!!");
                        ShowProjectMainInfo();
                        sqlQuery = "SELECT VendorID,PayDetail,PayRecmd,ChqReady,Remarks,EstimateNo,Workno,PayID,EntDate,GlCode,PaidAmt,Taxamt,Amount,DueDate,Hold,PaidStatus,Chqno,ChqDate,BkAccount,IssuedDate,IssuedBy,RecvdBy,UsrID,UsrDT from payments where EstimateNo=" + int.Parse(this.txtFindPwr.Text) + " and not PayRecmd is Null Order By Workno, PayID";
                        showrecord();
                        this.txtRupees.Text = "Rupees: " + rupeeword.changeCurrencyToWords(totalUamt);
                        ShowPaidRecord();
                        this.txtPaid.Text = "Rupees: " + rupeeword.changeCurrencyToWords(totalPamt);
                        this.txtFindPwr.Focus();

                        SendEmail(paymentlist);
                        //SendEmail();
                    }
                    else
                    {
                        MessageBox.Show("Payment Successfully Posted!!!!");
                    }

                }
                else
                {

                }
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                btnUpdatePayment.Enabled = true;
            }
            finally
            {
                connection.Close();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            frm_login loginform;
            loginform = new frm_login();
            this.Close();
            loginform.Show(); 

        }

        private void frm_Payments_Activated(object sender, EventArgs e)
        {
            this.txtFindPwr.Focus();  
        }

        private void SendEmail(ArrayList paralist)
        {
            //SmtpClient client = new SmtpClient("mail.fps.edu.pk");
            //MailAddress from = new MailAddress("it@fps.edu.pk", "FPS/HSS Work Order System", System.Text.Encoding.UTF8);

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("erpemail@fps.edu.pk", "K@r@chi123*");

            MailAddress from = new MailAddress(Login.CuserEmail, "FPS/HSS Work Order System", System.Text.Encoding.UTF8);

            string musrid = OBJFuncLib.DLookup("usrid", "EstMain", "EstimateNo=" + int.Parse(this.txtFindPwr.Text));
            string usremail = OBJFuncLib.DLookup("empEmail", "VPUser", "UsrID=" + int.Parse(musrid));
            MailAddress to = new MailAddress(usremail);


            //paralist[3]

            string mbrid = OBJFuncLib.DLookup("BranchID", "EstMain", "EstimateNo=" + int.Parse(this.txtFindPwr.Text));
            string msysid = OBJFuncLib.DLookup("SystemID", "EstMain", "EstimateNo=" + int.Parse(this.txtFindPwr.Text));
            string mDesc = OBJFuncLib.DLookup("Description", "EstMain", "EstimateNo=" + int.Parse(this.txtFindPwr.Text));


            //string paidamt = OBJFuncLib.DLookup("PaidAmt", "Payments", "EstimateNo=" + int.Parse(this.txtFindPwr.Text) + " AND WorkNo=" + workid.ToString() + " AND PayID=" + payid);

            //string paidamt = OBJFuncLib.DLookup("PayRecmd", "Payments", "EstimateNo=" + int.Parse(this.txtFindPwr.Text) + " AND WorkNo=" + workid.ToString() + " AND PayID=" + payid);



            //String.Format("{0:n}", 1234); //Output: 1,234.00

            //string.Format("{0:n0}", 9876); // no digits after the decimal point. Output: 9,876

            // Specify the message content.
            MailMessage message = new MailMessage(from, to);

            message.IsBodyHtml = true;            
            message.Body = "Your Recommended Payment Request No :" + "PWR/" + mbrid + "-" + this.txtFindPwr.Text;
            message.Body += Environment.NewLine + "<br /><br /> Description :<b><u>" + mDesc + "</u><b>";
            message.Body += Environment.NewLine + "<br /><br /> The Cheque is Ready to Deliver To : <br /><br />";

            for (int p = 0; p < paralist.Count; p++)
            {
                    //string value = paralist[i] as string;
                    string workid = paralist[p].ToString();
                    p++;
                    string payid = paralist[p].ToString();
                    p++;
                    string paidamt = paralist[p].ToString();

                    string vendid = OBJFuncLib.DLookup("VendorID", "Payments", "EstimateNo=" + int.Parse(this.txtFindPwr.Text) + " AND WorkNo=" + workid + " AND PayID=" + payid);

                    string vendorname = OBJFuncLib.DLookup("VendorName", "Vendor", "VendorID=" + int.Parse(vendid));

                    message.Body += Environment.NewLine + "<b>" + vendorname + "</b>" + ": <b><u> Rs." + String.Format("{0:#,##0}", double.Parse(paidamt)) + "/=</u></b><br />";
            }

            message.Body += Environment.NewLine + "<br /><br />Please Contact the Concern Vendor(s) or Proceed Accordingly!"; message.Body += Environment.NewLine + " ";
            message.Body += Environment.NewLine + "<br /><br />Thank You.";
            message.Body += Environment.NewLine + "<br />FPS/HSS Finance Department.";
            // Include some non-ASCII characters in body and subject.
            string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
            message.Body += Environment.NewLine + someArrows;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = "Cheque Is Ready For The Payment Request - " + "PWR/" + msysid + "-" + this.txtFindPwr.Text;
            message.To.Add("basitbaig@fps.edu.pk");
            client.Send(message);
            message.Dispose();
        }

        private void SendEmail()
        {
            SmtpClient client = new SmtpClient("mail.fps.edu.pk");
            MailAddress from = new MailAddress("erpemail@fps.edu.pk", "FPS/HSS Work Order System", System.Text.Encoding.UTF8);

            string musrid = OBJFuncLib.DLookup("usrid", "EstMain", "EstimateNo=" + int.Parse(this.txtFindPwr.Text));
            string usremail = OBJFuncLib.DLookup("empEmail", "VPUser", "UsrID=" + int.Parse(musrid));
            MailAddress to = new MailAddress(usremail);

            string mbrid = OBJFuncLib.DLookup("BranchID", "EstMain", "EstimateNo=" + int.Parse(this.txtFindPwr.Text));
            string msysid = OBJFuncLib.DLookup("SystemID", "EstMain", "EstimateNo=" + int.Parse(this.txtFindPwr.Text));
            string mDesc = OBJFuncLib.DLookup("Description", "EstMain", "EstimateNo=" + int.Parse(this.txtFindPwr.Text));

            string vendid = OBJFuncLib.DLookup("VendorID", "Payments", "EstimateNo=" + int.Parse(this.txtFindPwr.Text));

            string vendorname = OBJFuncLib.DLookup("VendorName", "Vendor", "VendorID=" + int.Parse(vendid));

            string paidamt = OBJFuncLib.DLookup("paidamt", "Payments", "EstimateNo=" + int.Parse(this.txtFindPwr.Text));


            // Specify the message content.
            MailMessage message = new MailMessage(from, to);
            message.Body = "Your Recommended Payment Request No :" + "PWR/" + mbrid + "-" + this.txtFindPwr.Text;
            message.Body += Environment.NewLine + "Description :" + mDesc;
            message.Body += Environment.NewLine + "The Cheque is Ready to Deliver, Please Contact the Concern Vendor or Proceed Accordingly!";
            message.Body += Environment.NewLine + " ";
            message.Body += Environment.NewLine + "Thank You.";
            message.Body += Environment.NewLine + "FPS/HSS Finance Department.";

            // Include some non-ASCII characters in body and subject.
            string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
            message.Body += Environment.NewLine + someArrows;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = "Cheque Is Ready For The Payment Request - " + "PWR/" + msysid + "-" + this.txtFindPwr.Text;
            message.To.Add("basitbaig@fps.edu.pk");
            client.Send(message);
            message.Dispose();
        }

        private void txtFindPwr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    if (txtFindPwr.Text != "")
                    {
                        ShowProjectMainInfo();
                        sqlQuery = "SELECT VendorID,PayDetail,PayRecmd,ChqReady,Remarks,EstimateNo,Workno,PayID,EntDate,GlCode,PaidAmt,Taxamt,Amount,DueDate,Hold,PaidStatus,Chqno,ChqDate,BkAccount,IssuedDate,IssuedBy,RecvdBy,UsrID,UsrDT from payments where EstimateNo=" + int.Parse(this.txtFindPwr.Text) + " and not PayRecmd is Null AND IsCancelled IS Null Order By Workno, PayID";
                        showrecord();
                        this.txtRupees.Text = "Rupees: " + rupeeword.changeCurrencyToWords(totalUamt);
                        ShowPaidRecord();
                        this.txtPaid.Text = "Rupees: " + rupeeword.changeCurrencyToWords(totalPamt);
                        this.txtFindPwr.Focus();
                    }
                    else
                    {
                        if (userTable != null)
                        {
                            userTable.Clear();
                        }
                        DG_Unpaid.DataSource = null;
                        DG_Unpaid.Rows.Clear();
                        DG_Unpaid.Refresh();
                    }
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
        }


    }
}
