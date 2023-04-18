namespace PDSystem
{
    partial class POSpec
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
            this.pospecs = new System.Windows.Forms.RichTextBox();
            this.lblpono = new System.Windows.Forms.Label();
            this.lblentryno = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblitem = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pospecs
            // 
            this.pospecs.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pospecs.Location = new System.Drawing.Point(22, 46);
            this.pospecs.Name = "pospecs";
            this.pospecs.Size = new System.Drawing.Size(396, 385);
            this.pospecs.TabIndex = 0;
            this.pospecs.Text = "";
            // 
            // lblpono
            // 
            this.lblpono.AutoSize = true;
            this.lblpono.Location = new System.Drawing.Point(19, 4);
            this.lblpono.Name = "lblpono";
            this.lblpono.Size = new System.Drawing.Size(42, 13);
            this.lblpono.TabIndex = 1;
            this.lblpono.Text = "PO No:";
            // 
            // lblentryno
            // 
            this.lblentryno.AutoSize = true;
            this.lblentryno.Location = new System.Drawing.Point(19, 30);
            this.lblentryno.Name = "lblentryno";
            this.lblentryno.Size = new System.Drawing.Size(51, 13);
            this.lblentryno.TabIndex = 3;
            this.lblentryno.Text = "Entry No:";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(314, 437);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(103, 30);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "&Save && Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblitem
            // 
            this.lblitem.AutoSize = true;
            this.lblitem.Location = new System.Drawing.Point(150, 30);
            this.lblitem.Name = "lblitem";
            this.lblitem.Size = new System.Drawing.Size(30, 13);
            this.lblitem.TabIndex = 5;
            this.lblitem.Text = "Item:";
            // 
            // POSpec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 471);
            this.Controls.Add(this.lblitem);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblentryno);
            this.Controls.Add(this.lblpono);
            this.Controls.Add(this.pospecs);
            this.Name = "POSpec";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "POSpec";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox pospecs;
        private System.Windows.Forms.Label lblpono;
        private System.Windows.Forms.Label lblentryno;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblitem;
    }
}