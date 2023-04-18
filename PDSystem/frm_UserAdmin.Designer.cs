namespace PDSystem
{
    partial class frm_UserAdmin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.cboUser = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chk_adminuser = new System.Windows.Forms.CheckBox();
            this.chk_newuser = new System.Windows.Forms.CheckBox();
            this.chk_reset = new System.Windows.Forms.CheckBox();
            this.chk_blockuser = new System.Windows.Forms.CheckBox();
            this.chk_payrights = new System.Windows.Forms.CheckBox();
            this.lblusrname = new System.Windows.Forms.Label();
            this.lbldesig = new System.Windows.Forms.Label();
            this.lblemail = new System.Windows.Forms.Label();
            this.lbldept = new System.Windows.Forms.Label();
            this.txtuser = new System.Windows.Forms.TextBox();
            this.txtdesig = new System.Windows.Forms.TextBox();
            this.cbodepart = new System.Windows.Forms.ComboBox();
            this.txtemail = new System.Windows.Forms.TextBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.label4);
            this.panel2.ForeColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(0, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(555, 36);
            this.panel2.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(171, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(172, 26);
            this.label4.TabIndex = 11;
            this.label4.Text = "User Control Panel";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(58, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(459, 24);
            this.label3.TabIndex = 0;
            this.label3.Text = "Foundation Pubilc School / Head Start School System";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(545, 46);
            this.panel1.TabIndex = 10;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.AliceBlue;
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Controls.Add(this.btnExit);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 459);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(545, 36);
            this.panel3.TabIndex = 12;
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(9, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.Navy;
            this.btnExit.Location = new System.Drawing.Point(467, 0);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(85, 33);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "&Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // cboUser
            // 
            this.cboUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboUser.FormattingEnabled = true;
            this.cboUser.Location = new System.Drawing.Point(94, 88);
            this.cboUser.Name = "cboUser";
            this.cboUser.Size = new System.Drawing.Size(269, 21);
            this.cboUser.TabIndex = 13;
            this.cboUser.SelectedIndexChanged += new System.EventHandler(this.cboUser_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "User Name:";
            // 
            // chk_adminuser
            // 
            this.chk_adminuser.AutoSize = true;
            this.chk_adminuser.Location = new System.Drawing.Point(11, 124);
            this.chk_adminuser.Name = "chk_adminuser";
            this.chk_adminuser.Size = new System.Drawing.Size(80, 17);
            this.chk_adminuser.TabIndex = 15;
            this.chk_adminuser.Text = "Admin User";
            this.chk_adminuser.UseVisualStyleBackColor = true;
            this.chk_adminuser.CheckedChanged += new System.EventHandler(this.chk_adminuser_CheckedChanged);
            // 
            // chk_newuser
            // 
            this.chk_newuser.AutoSize = true;
            this.chk_newuser.Location = new System.Drawing.Point(11, 167);
            this.chk_newuser.Name = "chk_newuser";
            this.chk_newuser.Size = new System.Drawing.Size(73, 17);
            this.chk_newuser.TabIndex = 16;
            this.chk_newuser.Text = "New User";
            this.chk_newuser.UseVisualStyleBackColor = true;
            this.chk_newuser.CheckedChanged += new System.EventHandler(this.chk_newuser_CheckedChanged);
            // 
            // chk_reset
            // 
            this.chk_reset.AutoSize = true;
            this.chk_reset.Location = new System.Drawing.Point(137, 124);
            this.chk_reset.Name = "chk_reset";
            this.chk_reset.Size = new System.Drawing.Size(103, 17);
            this.chk_reset.TabIndex = 17;
            this.chk_reset.Text = "Reset Password";
            this.chk_reset.UseVisualStyleBackColor = true;
            this.chk_reset.CheckedChanged += new System.EventHandler(this.chk_reset_CheckedChanged);
            // 
            // chk_blockuser
            // 
            this.chk_blockuser.AutoSize = true;
            this.chk_blockuser.Location = new System.Drawing.Point(310, 124);
            this.chk_blockuser.Name = "chk_blockuser";
            this.chk_blockuser.Size = new System.Drawing.Size(78, 17);
            this.chk_blockuser.TabIndex = 18;
            this.chk_blockuser.Text = "Block User";
            this.chk_blockuser.UseVisualStyleBackColor = true;
            this.chk_blockuser.CheckedChanged += new System.EventHandler(this.chk_blockuser_CheckedChanged);
            // 
            // chk_payrights
            // 
            this.chk_payrights.AutoSize = true;
            this.chk_payrights.Location = new System.Drawing.Point(453, 124);
            this.chk_payrights.Name = "chk_payrights";
            this.chk_payrights.Size = new System.Drawing.Size(77, 17);
            this.chk_payrights.TabIndex = 19;
            this.chk_payrights.Text = "Pay Rights";
            this.chk_payrights.UseVisualStyleBackColor = true;
            this.chk_payrights.CheckedChanged += new System.EventHandler(this.chk_payrights_CheckedChanged);
            // 
            // lblusrname
            // 
            this.lblusrname.AutoSize = true;
            this.lblusrname.Location = new System.Drawing.Point(11, 203);
            this.lblusrname.Name = "lblusrname";
            this.lblusrname.Size = new System.Drawing.Size(88, 13);
            this.lblusrname.TabIndex = 20;
            this.lblusrname.Text = "New User Name:";
            // 
            // lbldesig
            // 
            this.lbldesig.AutoSize = true;
            this.lbldesig.Location = new System.Drawing.Point(297, 203);
            this.lbldesig.Name = "lbldesig";
            this.lbldesig.Size = new System.Drawing.Size(66, 13);
            this.lbldesig.TabIndex = 21;
            this.lbldesig.Text = "Designation:";
            // 
            // lblemail
            // 
            this.lblemail.AutoSize = true;
            this.lblemail.Location = new System.Drawing.Point(297, 229);
            this.lblemail.Name = "lblemail";
            this.lblemail.Size = new System.Drawing.Size(49, 13);
            this.lblemail.TabIndex = 22;
            this.lblemail.Text = "Email ID:";
            // 
            // lbldept
            // 
            this.lbldept.AutoSize = true;
            this.lbldept.Location = new System.Drawing.Point(12, 229);
            this.lbldept.Name = "lbldept";
            this.lbldept.Size = new System.Drawing.Size(65, 13);
            this.lbldept.TabIndex = 23;
            this.lbldept.Text = "Department:";
            // 
            // txtuser
            // 
            this.txtuser.Location = new System.Drawing.Point(94, 196);
            this.txtuser.Name = "txtuser";
            this.txtuser.Size = new System.Drawing.Size(197, 20);
            this.txtuser.TabIndex = 24;
            this.txtuser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtuser_KeyPress);
            // 
            // txtdesig
            // 
            this.txtdesig.Location = new System.Drawing.Point(361, 195);
            this.txtdesig.Name = "txtdesig";
            this.txtdesig.Size = new System.Drawing.Size(156, 20);
            this.txtdesig.TabIndex = 25;
            this.txtdesig.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtdesig_KeyPress);
            // 
            // cbodepart
            // 
            this.cbodepart.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbodepart.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbodepart.FormattingEnabled = true;
            this.cbodepart.Location = new System.Drawing.Point(94, 222);
            this.cbodepart.Name = "cbodepart";
            this.cbodepart.Size = new System.Drawing.Size(197, 21);
            this.cbodepart.TabIndex = 26;
            this.cbodepart.SelectedIndexChanged += new System.EventHandler(this.cbodepart_SelectedIndexChanged);
            this.cbodepart.Enter += new System.EventHandler(this.cbodepart_Enter);
            this.cbodepart.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbodepart_KeyPress);
            // 
            // txtemail
            // 
            this.txtemail.Location = new System.Drawing.Point(361, 222);
            this.txtemail.Name = "txtemail";
            this.txtemail.Size = new System.Drawing.Size(156, 20);
            this.txtemail.TabIndex = 27;
            this.txtemail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtemail_KeyPress);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(329, 161);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(93, 23);
            this.btnUpload.TabIndex = 28;
            this.btnUpload.Text = "UploadFile";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(436, 159);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(93, 24);
            this.btnDownload.TabIndex = 29;
            this.btnDownload.Text = "Download File";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // frm_UserAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 495);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.txtemail);
            this.Controls.Add(this.cbodepart);
            this.Controls.Add(this.txtdesig);
            this.Controls.Add(this.txtuser);
            this.Controls.Add(this.lbldept);
            this.Controls.Add(this.lblemail);
            this.Controls.Add(this.lbldesig);
            this.Controls.Add(this.lblusrname);
            this.Controls.Add(this.chk_payrights);
            this.Controls.Add(this.chk_blockuser);
            this.Controls.Add(this.chk_reset);
            this.Controls.Add(this.chk_newuser);
            this.Controls.Add(this.chk_adminuser);
            this.Controls.Add(this.cboUser);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frm_UserAdmin";
            this.Text = "frm_UserAdmin";
            this.Load += new System.EventHandler(this.frm_UserAdmin_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ComboBox cboUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chk_adminuser;
        private System.Windows.Forms.CheckBox chk_newuser;
        private System.Windows.Forms.CheckBox chk_reset;
        private System.Windows.Forms.CheckBox chk_blockuser;
        private System.Windows.Forms.CheckBox chk_payrights;
        private System.Windows.Forms.Label lblusrname;
        private System.Windows.Forms.Label lbldesig;
        private System.Windows.Forms.Label lblemail;
        private System.Windows.Forms.Label lbldept;
        private System.Windows.Forms.TextBox txtuser;
        private System.Windows.Forms.TextBox txtdesig;
        private System.Windows.Forms.ComboBox cbodepart;
        private System.Windows.Forms.TextBox txtemail;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnDownload;
    }
}