namespace PDSystem
{
    partial class frm_Payments
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.DG_Unpaid = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtPaid = new System.Windows.Forms.TextBox();
            this.DG_Paid = new System.Windows.Forms.DataGridView();
            this.txtFindPwr = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEstno = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProjTitle = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtReqBy = new System.Windows.Forms.TextBox();
            this.btnUpdatePayment = new System.Windows.Forms.Button();
            this.txtusrid = new System.Windows.Forms.TextBox();
            this.txtRupees = new System.Windows.Forms.TextBox();
            this.lblcancel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DG_Unpaid)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DG_Paid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(872, 36);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(223, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(296, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Payment Posting Update";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 510);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(872, 38);
            this.panel2.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.Navy;
            this.btnExit.Location = new System.Drawing.Point(782, 8);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(77, 27);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "&Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.DG_Unpaid);
            this.panel3.Location = new System.Drawing.Point(0, 117);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(872, 208);
            this.panel3.TabIndex = 2;
            // 
            // DG_Unpaid
            // 
            this.DG_Unpaid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DG_Unpaid.Location = new System.Drawing.Point(12, 13);
            this.DG_Unpaid.Name = "DG_Unpaid";
            this.DG_Unpaid.Size = new System.Drawing.Size(848, 192);
            this.DG_Unpaid.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtPaid);
            this.panel4.Controls.Add(this.DG_Paid);
            this.panel4.Location = new System.Drawing.Point(0, 356);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(872, 148);
            this.panel4.TabIndex = 3;
            // 
            // txtPaid
            // 
            this.txtPaid.Location = new System.Drawing.Point(13, 125);
            this.txtPaid.Name = "txtPaid";
            this.txtPaid.Size = new System.Drawing.Size(713, 20);
            this.txtPaid.TabIndex = 1;
            // 
            // DG_Paid
            // 
            this.DG_Paid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DG_Paid.Location = new System.Drawing.Point(3, 3);
            this.DG_Paid.Name = "DG_Paid";
            this.DG_Paid.Size = new System.Drawing.Size(857, 119);
            this.DG_Paid.TabIndex = 0;
            // 
            // txtFindPwr
            // 
            this.txtFindPwr.Location = new System.Drawing.Point(94, 47);
            this.txtFindPwr.Name = "txtFindPwr";
            this.txtFindPwr.Size = new System.Drawing.Size(100, 20);
            this.txtFindPwr.TabIndex = 15;
            this.txtFindPwr.TextChanged += new System.EventHandler(this.txtFindPwr_TextChanged);
            this.txtFindPwr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFindPwr_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Find Work Order:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Project / Work Order No:";
            // 
            // txtEstno
            // 
            this.txtEstno.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstno.Location = new System.Drawing.Point(15, 90);
            this.txtEstno.Name = "txtEstno";
            this.txtEstno.Size = new System.Drawing.Size(115, 20);
            this.txtEstno.TabIndex = 7;
            this.txtEstno.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(144, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Description/Title :";
            // 
            // txtProjTitle
            // 
            this.txtProjTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProjTitle.Location = new System.Drawing.Point(147, 90);
            this.txtProjTitle.Name = "txtProjTitle";
            this.txtProjTitle.Size = new System.Drawing.Size(508, 20);
            this.txtProjTitle.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(672, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Payment Request By:";
            // 
            // txtReqBy
            // 
            this.txtReqBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReqBy.Location = new System.Drawing.Point(675, 88);
            this.txtReqBy.Name = "txtReqBy";
            this.txtReqBy.Size = new System.Drawing.Size(184, 20);
            this.txtReqBy.TabIndex = 11;
            // 
            // btnUpdatePayment
            // 
            this.btnUpdatePayment.Location = new System.Drawing.Point(763, 331);
            this.btnUpdatePayment.Name = "btnUpdatePayment";
            this.btnUpdatePayment.Size = new System.Drawing.Size(96, 25);
            this.btnUpdatePayment.TabIndex = 12;
            this.btnUpdatePayment.Text = "Update Payment";
            this.btnUpdatePayment.UseVisualStyleBackColor = true;
            this.btnUpdatePayment.Click += new System.EventHandler(this.btnUpdatePayment_Click);
            // 
            // txtusrid
            // 
            this.txtusrid.Location = new System.Drawing.Point(677, 47);
            this.txtusrid.Name = "txtusrid";
            this.txtusrid.Size = new System.Drawing.Size(72, 20);
            this.txtusrid.TabIndex = 13;
            this.txtusrid.Visible = false;
            // 
            // txtRupees
            // 
            this.txtRupees.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRupees.Location = new System.Drawing.Point(15, 330);
            this.txtRupees.Name = "txtRupees";
            this.txtRupees.Size = new System.Drawing.Size(712, 22);
            this.txtRupees.TabIndex = 14;
            // 
            // lblcancel
            // 
            this.lblcancel.AutoSize = true;
            this.lblcancel.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcancel.ForeColor = System.Drawing.Color.Red;
            this.lblcancel.Location = new System.Drawing.Point(224, 40);
            this.lblcancel.Name = "lblcancel";
            this.lblcancel.Size = new System.Drawing.Size(418, 25);
            this.lblcancel.TabIndex = 16;
            this.lblcancel.Text = "Not Found / Cancelled Work Order";
            this.lblcancel.Visible = false;
            // 
            // frm_Payments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 548);
            this.Controls.Add(this.lblcancel);
            this.Controls.Add(this.txtRupees);
            this.Controls.Add(this.txtusrid);
            this.Controls.Add(this.btnUpdatePayment);
            this.Controls.Add(this.txtReqBy);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtProjTitle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtEstno);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFindPwr);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frm_Payments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Work / Project Payments Form";
            this.Activated += new System.EventHandler(this.frm_Payments_Activated);
            this.Load += new System.EventHandler(this.frm_Payments_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DG_Unpaid)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DG_Paid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView DG_Unpaid;
        private System.Windows.Forms.DataGridView DG_Paid;
        private System.Windows.Forms.TextBox txtFindPwr;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEstno;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProjTitle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtReqBy;
        private System.Windows.Forms.Button btnUpdatePayment;
        private System.Windows.Forms.TextBox txtusrid;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtRupees;
        private System.Windows.Forms.TextBox txtPaid;
        private System.Windows.Forms.Label lblcancel;
    }
}