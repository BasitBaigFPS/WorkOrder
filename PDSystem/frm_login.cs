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
using encPassword; 


namespace PDSystem
{
    //Witribe Password  //FpsHo123
    //DBCC CHECKIDENT('Vendor', RESEED, 0)
    //Reset Identity column value to Zero in Sql Server
    public partial class frm_login : Form
    {
        public frm_login()
        {
          InitializeComponent();
        }
        //private bool newlogin;
        Login OBJLogin = new Login();       
        MyFunctions OBJFuncLib = new MyFunctions();
        

        private void frm_login_Load(object sender, EventArgs e)
        {
            //newlogin = false;
            //OBJFuncLib.FillCombo("UsrName","UsrID",cboLogin,"PUser","blockuser=0");

            OBJFuncLib.FillCombo("UsrName", "UsrID", cboLogin, "VPUser", "");
            OBJFuncLib.FillCombo("UsrName", "UsrID", cbochoicelogin, "VPUser", "");

            cbochoicelogin.SelectedValue = -1;

            this.cboLogin.Text = "";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
           //string passw = OBJFuncLib.DLookup("pwd","users","Name='"+cboLogin.Text+"'");
           //ArrayList fieldlist = new ArrayList();
           //int totpara = fieldlist.Count;
            int gpid = 12;
            bool passok;

            string checkpass = encPassword.Md5Util.Md5Enc("3572390");  // Zulfiqar WO Password

            string encpass = encPassword.Md5Util.Md5Enc(txtpassword.Text.Trim());


            //Login.Cuserid
            if (OBJLogin.DLookup("password","VPUser","UsrID="+Login.Cuserid)==encpass)
            {
              passok =true;
              gpid = int.Parse(OBJLogin.DLookup("fkGID", "VPUser", "UsrID=" + Login.Cuserid));
            }
            else
            {
                passok=false;
            }
           
           //if (newlogin == false)
           //{
           //    passok = OBJLogin.ValidatePassword(cboLogin.SelectedValue.ToString(), this.txtpassword.Text);       
           //}

           if (passok == true)
           {
               //if (this.txtconfirmpass.Text != "")
               //{
               //    bool result = OBJLogin.PostUserLogin(int.Parse(cboLogin.SelectedValue.ToString()), false,OBJLogin.Makepassword(txtconfirmpass.Text));
               //}
               //if (newlogin==true)
               //{
                   //MessageBox.Show("Login Success!");  
               //}
               //else
               //{
               //    //MessageBox.Show("Correct Password");  
               //}
               //Login myuser = new Login(int.Parse(cboLogin.SelectedValue.ToString()),cboLogin.Text); 
               
               //Workuserid

               if (cbochoicelogin.SelectedIndex.ToString()!="-1")
               {
                   Login.Cuserid = int.Parse(cbochoicelogin.SelectedValue.ToString());
                   Login.CuserEmail = OBJLogin.DLookup("empEmail", "VPUser", "UsrID=" + Login.Cuserid);
                   Login.Cuser = this.cbochoicelogin.Text;
               }
               else
               {
                   Login.Cuserid = int.Parse(cboLogin.SelectedValue.ToString());
                   Login.Cuser = this.cboLogin.Text;
               }
              

               //string checkadmin;
               //checkadmin = OBJLogin.DLookup("IsAdmin", "puser", "usrID=" + int.Parse(cboLogin.SelectedValue.ToString()));
               //if (bool.Parse(checkadmin) == true)
               //{
               //    frm_UserAdmin usradm;
               //    usradm = new frm_UserAdmin();

               //    DialogResult result1 = MessageBox.Show("Do You Want To Open User Control Panel?", "Admin Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
               //    if (result1.ToString() == "Yes")
               //    {
               //        this.Hide();
               //        usradm.Show();
               //    }
               //    else
               //    {
               //        frm_Main mainform;
               //        mainform = new frm_Main();
               //        if (mainform.Visible == false)
               //        {
               //            //frm_login.ActiveForm.Activate()  
               //            this.Hide();
               //            mainform.Show();
               //        }
               //    }
               //    if (usradm.IsDisposed == true)
               //    {
               //        usradm.Close();
               //        usradm.Dispose();
               //    }
               //}
               //else
               //{
                Login.CuserGP = gpid;

                //frm_UserAdmin usradm;
                //usradm = new frm_UserAdmin();
                //this.Hide();
                //usradm.Show();
              

                frm_Main mainform;
                mainform = new frm_Main();
                if (mainform.Visible == false)
                {
                    //frm_login.ActiveForm.Activate()  
                    this.Hide();
                    mainform.Show();
                }
    
               //}

           }
           else
           {
               MessageBox.Show("InCorrect Password");
               this.txtpassword.Focus();  
           }

        }

        private void txtpassword_TextChanged(object sender, EventArgs e)
        {

            //this.btnLogin.Focus(); 
        }

        private void cboLogin_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CheckLogin
            try
            {
                //Login OBJLogin = new Login();
                string cboval1 = cboLogin.SelectedIndex.ToString();
                if (this.cboLogin.SelectedIndex != 0 && this.cboLogin.SelectedIndex > 0)
                {
                    //this.cboLogin.Text.Trim().CompareTo(string.Empty) != 0
                    //this.cboLogin.SelectedValue.ToString() != "System.Data.DataRowView" ||

                    int cboval = int.Parse(cboLogin.SelectedValue.ToString());

                    if (cboval == 95 || cboval == 113)
                    {
                        this.cbochoicelogin.Visible = true;
                    }
                    else
                    {
                        this.cbochoicelogin.Visible = false;
                    }
                    Login.Cuserid = cboval;
                    Login.CuserEmail = OBJLogin.DLookup("empEmail","VPUser","UsrID="+Login.Cuserid);


                    //MessageBox.Show(cboval.ToString());  
                    //int cboval = int.Parse(cboval1);
                    //if (OBJLogin.CheckLogin(cboval) == true)
                    //{
                    //    MessageBox.Show("Welcome To System!" + "\n\rPlease Enter and Confirm you New Password! \n\rThank You.");
                    //    this.lblconfirmpass.Visible = true;
                    //    this.txtconfirmpass.Visible = true;
                    //    newlogin = true;
                    //    this.txtpassword.Focus();
                    //}
                    //else
                    //{
                    //    this.lblconfirmpass.Visible = false;
                    //    this.txtconfirmpass.Visible = false;
                    //    this.txtpassword.Focus();
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.txtpassword.Focus();
            }
            
        }

        private void txtpassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.KeyChar = Chr(Keys.Enter)
            if (e.KeyChar == (Char)13)
            {

                //string sqlstr = "SELECT UserID, UsrName FROM VPUser WHERE UsrID ='" + txtUserID.Text + "' AND UPassword ='" + encpass + "'";
                
                //if (this.txtconfirmpass.Visible == true)
                //{
                //    this.txtconfirmpass.Focus();
                //}
                //else
                //{
                btnLogin_Click(sender, e);
                //}
            }
        }

        private void txtconfirmpass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnLogin_Click(sender, e);
            
            //this.btnLogin.Focus();
            //if(e.KeyChar == (Char)13)
            //{
            //    this.btnLogin.Focus();
            //}
            
            //Byte[] bytes = ASCIIEncoding.ASCII.GetBytes(e.KeyChar.ToString());
            //foreach (Byte b in bytes)
            //{
            //    if (b == 13)
            //    {
            //        this.btnLogin.Focus();
            //    }
            //}
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();  
            //this.Close();
            //frm_login F1 = new frm_login(); 
            //F1.Show();
           // frm_login.Show();
        }

        private void cbochoicelogin_SelectedIndexChanged(object sender, EventArgs e)
        {

            string cboval1 = cbochoicelogin.SelectedIndex.ToString();
            if (this.cbochoicelogin.SelectedIndex != 0 && this.cbochoicelogin.SelectedIndex > 0)
            {
                int cboval = int.Parse(cbochoicelogin.SelectedValue.ToString());

            }
        }

        private void frm_login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogin_Click(sender, e);
        }
    }
    // Allow the keypress as long as it is a number.
    //if (Char.IsDigit(e.KeyChar))
    //{
    //    newText += e.KeyChar.ToString();
    //    finished = true;
    //    break;
    //}
    //else
    //{
    //    // Invalid entry; exit and don't change the text.
    //    return;
    //}

    // Allow the keypress as long as it is a letter.
    //if (Char.IsLetter(e.KeyChar))
    //{
    //    newText += e.KeyChar.ToString();
    //    finished = true;
    //    break;
    //}
    //else
    //{
    //    // Invalid entry; exit and don't change the text.
    //    return;
    //}


    //private void comboBox1_TextChanged(object sender, EventArgs e){    for(int i=0; i<comboBox1.Items.Count; i++)    {        if (comboBox1.Items[i].ToString().StartsWith(comboBox1.Text))        {            comboBox1.SelectedIndex = i;            return;        }    }}

}
