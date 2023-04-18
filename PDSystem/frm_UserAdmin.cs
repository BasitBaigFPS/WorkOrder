using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace PDSystem
{
    public partial class frm_UserAdmin : Form
    {
        private bool _newuser = false;
        PUser OBJPUser = new PUser();
        MyFunctions OBJFuncLib = new MyFunctions();

        UploadDownload objupload = new UploadDownload();
 

        public frm_UserAdmin()
        {
            InitializeComponent();
        }
        

        private void frm_UserAdmin_Load(object sender, EventArgs e)
        {
            OBJFuncLib.FillCombo("UsrName", "UsrID", cboUser, "PUser", "blockuser=0");
            OBJFuncLib.FillCombo("DeptName", "DeptID", cbodepart, "Department", "");
            this.cboUser.Text = "";           
        }

        private void chk_newuser_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_newuser.Checked)
            {
                _newuser = true;
                OBJPUser.Isadmin = false;
                OBJPUser.Resetlogin = true;
                OBJPUser.Blockuser = false;
                OBJPUser.Payright = false;
                this.txtuser.Text = "";
                this.txtdesig.Text = "";
                this.cbodepart.Text = "";
                this.txtemail.Text = ""; 
                this.txtuser.Focus();
            }
            else
            {
                _newuser = false;
            }
        }

        private void chk_adminuser_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_adminuser.Checked)
            {
                OBJPUser.Isadmin = true;
            }
            else
            {
                OBJPUser.Isadmin = false;
            }
        }

        private void chk_reset_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_reset.Checked)
            {
                OBJPUser.Resetlogin = true;
            }
            else
            {
                OBJPUser.Resetlogin = false;
            }
        }

        private void chk_blockuser_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_blockuser.Checked)
            {
                OBJPUser.Blockuser = true;
                
            }
            else
            { 
                OBJPUser.Blockuser = false; 
            }
        }

        private void chk_payrights_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_payrights.Checked)
            {
                OBJPUser.Payright = true;
            }
            else
            { 
                OBJPUser.Payright  = false; 
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool result;

            ArrayList fieldlist = new ArrayList();
            
            if (_newuser == true)
            {
                //Calling Bind Property Mothod
                BindMyProperty();

                //Passwing property values to arraylist
                fieldlist.Add(OBJPUser.Usrname);
                fieldlist.Add(OBJPUser.Deptid);
                fieldlist.Add(OBJPUser.Desig);
                fieldlist.Add(OBJPUser.Email);
                fieldlist.Add(OBJPUser.Blockuser);
                fieldlist.Add(OBJPUser.Isadmin);
                fieldlist.Add(OBJPUser.Resetlogin);
                fieldlist.Add(OBJPUser.Payright);
                fieldlist.Add(0);
                result = OBJPUser.UserRecord(fieldlist, "I");
            }
            else
            {
                //Calling Bind Property Mothod
                BindMyProperty();

                //Passwing property values to arraylist
                fieldlist.Add(OBJPUser.Usrname);
                fieldlist.Add(OBJPUser.Deptid);
                fieldlist.Add(OBJPUser.Desig);
                fieldlist.Add(OBJPUser.Email);
                fieldlist.Add(OBJPUser.Blockuser);
                fieldlist.Add(OBJPUser.Isadmin);
                fieldlist.Add(OBJPUser.Resetlogin);
                fieldlist.Add(OBJPUser.Payright);
                fieldlist.Add(OBJPUser.Usrid);
                result = OBJPUser.UserRecord(fieldlist, "U");

            }
            if (result == true)
            {
                if (btnSave.Text == "Save")
                {
                    MessageBox.Show("Record Has Been Successfully Added!");
                }
                else
                {
                    MessageBox.Show("Record Has Been Successfully Updated!");
                    this.btnSave.Text = "&Save"; 
                }
            }
            else
            {
                MessageBox.Show("Record Doest Not Saved!!!!!");
                this.btnSave.Text = "&Save"; 
            }


        }

        private void BindMyProperty()
        {
            //Passing Control/Object values into properties
            //OBJPUser.Isadmin = this._isadmin;
            //OBJPUser.Blockuser = this._blockuser;
            //OBJPUser.Resetlogin = this._reset;
            //OBJPUser.Payright = this._payrights;
            OBJPUser.Usrname = this.txtuser.Text;
            OBJPUser.Desig = this.txtdesig.Text;
            OBJPUser.Email = this.txtemail.Text;
            OBJPUser.Deptid = Convert.ToString(this.cbodepart.SelectedValue);
            OBJPUser.Usrid = int.Parse(this.cboUser.SelectedValue.ToString());
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            frm_login loginform;
            loginform = new frm_login();
            this.Close();
            loginform.Show(); 

        }

        private void txtuser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)13)
            {
                this.cbodepart.Focus(); 
            }
        }

        private void cbodepart_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)13)
            {
                //MessageBox.Show(this.cbodepart.SelectedValue.ToString()); 
                this.txtdesig.Focus();
            }
        }

        private void txtemail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)13)
            {
                this.btnSave.Focus(); 
            }

        }

        private void cboUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            //MessageBox.Show(this.cboUser.SelectedValue.ToString());
            if (this.cboUser.SelectedValue.ToString()!="System.Data.DataRowView")
            {
                //Initilize properties from the table values
                int cboval = int.Parse(this.cboUser.SelectedValue.ToString());
                //MessageBox.Show(cboval.ToString());
                OBJPUser.Usrid = cboval;
                OBJPUser.Usrname = cboUser.Text;
                OBJPUser.Deptid = OBJFuncLib.DLookup("DeptID", "PUser", "UsrID=" + OBJPUser.Usrid);
                OBJPUser.Desig = OBJFuncLib.DLookup("JobTitle", "PUser", "UsrID=" + OBJPUser.Usrid);
                OBJPUser.Email = OBJFuncLib.DLookup("Email", "PUser", "UsrID=" + OBJPUser.Usrid);
                OBJPUser.Isadmin = bool.Parse(OBJFuncLib.DLookup("IsAdmin", "PUser", "UsrID=" + OBJPUser.Usrid));
                OBJPUser.Resetlogin = bool.Parse(OBJFuncLib.DLookup("FirstLogin", "PUser", "UsrID=" + OBJPUser.Usrid));
                OBJPUser.Blockuser = bool.Parse(OBJFuncLib.DLookup("BlockUser", "PUser", "UsrID=" + OBJPUser.Usrid));
                OBJPUser.Payright = bool.Parse(OBJFuncLib.DLookup("PayRights", "PUser", "UsrID=" + OBJPUser.Usrid));

                //Initilize form object with Property Value
                
                 

                this.chk_adminuser.Checked = OBJPUser.Isadmin;
                this.chk_blockuser.Checked = OBJPUser.Blockuser;
                this.chk_reset.Checked = OBJPUser.Resetlogin;
                this.chk_payrights.Checked = OBJPUser.Payright;
                this.txtuser.Text = OBJPUser.Usrname;
                this.txtdesig.Text = OBJPUser.Desig;
                this.txtemail.Text = OBJPUser.Email;
                this.cbodepart.SelectedValue = OBJPUser.Deptid;

                this.btnSave.Text = "&Update"; 
            }

            
        }

        private void cboUser_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void cbodepart_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtdesig.Focus();
        }

        private void cbodepart_Enter(object sender, EventArgs e)
        {
         
        }

        private void txtdesig_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.txtemail.Focus(); 
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            DeleteWebFile();

            bool result;
            result = objupload.UploadFile();
            MessageBox.Show("File Successfully Uploaded....");  
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            bool result;
            result = objupload.DownloadFile(); 
            MessageBox.Show("File Successfully Downloaded....");  
        }


        private void DeleteWebFile()
        {

            bool result;
            result = objupload.DeleteFile();
            MessageBox.Show("File Successfully Deleted....");  
        
        }

    }
}
