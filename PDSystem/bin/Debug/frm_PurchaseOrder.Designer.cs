namespace PDSystem
{
    partial class frm_PurchaseOrder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblpo = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblworktitle = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnNewItem = new System.Windows.Forms.Button();
            this.txtNewItem = new System.Windows.Forms.TextBox();
            this.txtAmtTotal = new System.Windows.Forms.TextBox();
            this.txtQtyTotal = new System.Windows.Forms.TextBox();
            this.DG_POEntry = new System.Windows.Forms.DataGridView();
            this.txtPO = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtcontact = new System.Windows.Forms.TextBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.txtusrid = new System.Windows.Forms.TextBox();
            this.txtRupees = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnDeletePO = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.txtFndPO = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnNewPO = new System.Windows.Forms.Button();
            this.entdate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.cboDepartment = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtcost = new System.Windows.Forms.TextBox();
            this.txtTerms = new System.Windows.Forms.RichTextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQuotID = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtshipat = new System.Windows.Forms.TextBox();
            this.txtquotdate = new System.Windows.Forms.DateTimePicker();
            this.txtdpt = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtdiscount = new System.Windows.Forms.TextBox();
            this.txtdlvdate = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.txtworktitle = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DG_POEntry)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Controls.Add(this.lblpo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(797, 36);
            this.panel1.TabIndex = 0;
            // 
            // lblpo
            // 
            this.lblpo.AutoSize = true;
            this.lblpo.Font = new System.Drawing.Font("Times New Roman", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpo.Location = new System.Drawing.Point(9, 2);
            this.lblpo.Name = "lblpo";
            this.lblpo.Size = new System.Drawing.Size(196, 31);
            this.lblpo.TabIndex = 0;
            this.lblpo.Text = "Purchase Order";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel2.Controls.Add(this.txtworktitle);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.lblworktitle);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 556);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(797, 38);
            this.panel2.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Navy;
            this.btnSave.Location = new System.Drawing.Point(603, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(64, 31);
            this.btnSave.TabIndex = 28;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.Navy;
            this.btnExit.Location = new System.Drawing.Point(700, 8);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(77, 27);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "&Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblworktitle
            // 
            this.lblworktitle.AutoSize = true;
            this.lblworktitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblworktitle.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblworktitle.ForeColor = System.Drawing.Color.Lavender;
            this.lblworktitle.Location = new System.Drawing.Point(6, 8);
            this.lblworktitle.Name = "lblworktitle";
            this.lblworktitle.Size = new System.Drawing.Size(59, 21);
            this.lblworktitle.TabIndex = 27;
            this.lblworktitle.Text = "label12";
            this.lblworktitle.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnNewItem);
            this.panel3.Controls.Add(this.txtNewItem);
            this.panel3.Controls.Add(this.txtAmtTotal);
            this.panel3.Controls.Add(this.txtQtyTotal);
            this.panel3.Controls.Add(this.DG_POEntry);
            this.panel3.Location = new System.Drawing.Point(0, 244);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(797, 281);
            this.panel3.TabIndex = 2;
            // 
            // btnNewItem
            // 
            this.btnNewItem.Enabled = false;
            this.btnNewItem.Location = new System.Drawing.Point(452, 254);
            this.btnNewItem.Name = "btnNewItem";
            this.btnNewItem.Size = new System.Drawing.Size(78, 23);
            this.btnNewItem.TabIndex = 5;
            this.btnNewItem.Text = "New Item";
            this.btnNewItem.UseVisualStyleBackColor = true;
            this.btnNewItem.Click += new System.EventHandler(this.btnNewItem_Click);
            // 
            // txtNewItem
            // 
            this.txtNewItem.Location = new System.Drawing.Point(3, 255);
            this.txtNewItem.Name = "txtNewItem";
            this.txtNewItem.Size = new System.Drawing.Size(443, 20);
            this.txtNewItem.TabIndex = 4;
            this.txtNewItem.TextChanged += new System.EventHandler(this.txtNewItem_TextChanged);
            // 
            // txtAmtTotal
            // 
            this.txtAmtTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtAmtTotal.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtAmtTotal.Location = new System.Drawing.Point(676, 255);
            this.txtAmtTotal.Name = "txtAmtTotal";
            this.txtAmtTotal.ReadOnly = true;
            this.txtAmtTotal.Size = new System.Drawing.Size(93, 23);
            this.txtAmtTotal.TabIndex = 3;
            this.txtAmtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtQtyTotal
            // 
            this.txtQtyTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtQtyTotal.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtQtyTotal.Location = new System.Drawing.Point(536, 255);
            this.txtQtyTotal.Name = "txtQtyTotal";
            this.txtQtyTotal.ReadOnly = true;
            this.txtQtyTotal.Size = new System.Drawing.Size(74, 23);
            this.txtQtyTotal.TabIndex = 2;
            this.txtQtyTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // DG_POEntry
            // 
            this.DG_POEntry.AllowUserToAddRows = false;
            this.DG_POEntry.AllowUserToOrderColumns = true;
            this.DG_POEntry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DG_POEntry.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DG_POEntry.Location = new System.Drawing.Point(3, 3);
            this.DG_POEntry.Name = "DG_POEntry";
            this.DG_POEntry.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DG_POEntry.Size = new System.Drawing.Size(790, 246);
            this.DG_POEntry.TabIndex = 0;
            this.DG_POEntry.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DG_POEntry_CellClick);
            this.DG_POEntry.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DG_POEntry_CellDoubleClick);
            this.DG_POEntry.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DG_POEntry_RowHeaderMouseClick);
            this.DG_POEntry.Enter += new System.EventHandler(this.DG_POEntry_Enter);
            this.DG_POEntry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DG_POEntry_KeyDown);
            // 
            // txtPO
            // 
            this.txtPO.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPO.Location = new System.Drawing.Point(107, 71);
            this.txtPO.Name = "txtPO";
            this.txtPO.Size = new System.Drawing.Size(69, 26);
            this.txtPO.TabIndex = 5;
            this.txtPO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "P.O. No:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "P.O. Date:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Terms && Conditions:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(504, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Contact Person:";
            // 
            // txtcontact
            // 
            this.txtcontact.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcontact.Location = new System.Drawing.Point(593, 71);
            this.txtcontact.Name = "txtcontact";
            this.txtcontact.Size = new System.Drawing.Size(184, 22);
            this.txtcontact.TabIndex = 11;
            this.txtcontact.Leave += new System.EventHandler(this.txtcontact_Leave);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(603, 215);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(96, 25);
            this.btnImport.TabIndex = 12;
            this.btnImport.Text = "Load Excel Data";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // txtusrid
            // 
            this.txtusrid.Location = new System.Drawing.Point(343, 74);
            this.txtusrid.Name = "txtusrid";
            this.txtusrid.Size = new System.Drawing.Size(72, 20);
            this.txtusrid.TabIndex = 13;
            this.txtusrid.Visible = false;
            // 
            // txtRupees
            // 
            this.txtRupees.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRupees.Location = new System.Drawing.Point(0, 531);
            this.txtRupees.Name = "txtRupees";
            this.txtRupees.ReadOnly = true;
            this.txtRupees.Size = new System.Drawing.Size(793, 22);
            this.txtRupees.TabIndex = 14;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LightGray;
            this.panel4.Controls.Add(this.btnDeletePO);
            this.panel4.Controls.Add(this.btnUpdate);
            this.panel4.Controls.Add(this.btnPrint);
            this.panel4.Controls.Add(this.txtFndPO);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.btnNewPO);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 36);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(797, 32);
            this.panel4.TabIndex = 15;
            // 
            // btnDeletePO
            // 
            this.btnDeletePO.FlatAppearance.BorderSize = 0;
            this.btnDeletePO.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnDeletePO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeletePO.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeletePO.ForeColor = System.Drawing.Color.Navy;
            this.btnDeletePO.Location = new System.Drawing.Point(309, 5);
            this.btnDeletePO.Name = "btnDeletePO";
            this.btnDeletePO.Size = new System.Drawing.Size(106, 23);
            this.btnDeletePO.TabIndex = 38;
            this.btnDeletePO.Text = "Delete P.Order";
            this.btnDeletePO.UseVisualStyleBackColor = true;
            this.btnDeletePO.Click += new System.EventHandler(this.btnDeletePO_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.Navy;
            this.btnUpdate.Location = new System.Drawing.Point(128, 6);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(93, 23);
            this.btnUpdate.TabIndex = 37;
            this.btnUpdate.Text = "Edit P.Order";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.Navy;
            this.btnPrint.Location = new System.Drawing.Point(488, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(101, 23);
            this.btnPrint.TabIndex = 36;
            this.btnPrint.Text = "Print  POrder";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // txtFndPO
            // 
            this.txtFndPO.Location = new System.Drawing.Point(706, 7);
            this.txtFndPO.Name = "txtFndPO";
            this.txtFndPO.Size = new System.Drawing.Size(71, 20);
            this.txtFndPO.TabIndex = 3;
            this.txtFndPO.TextChanged += new System.EventHandler(this.txtFndPO_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(628, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Find P. Order:";
            // 
            // btnNewPO
            // 
            this.btnNewPO.FlatAppearance.BorderSize = 0;
            this.btnNewPO.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnNewPO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewPO.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewPO.ForeColor = System.Drawing.Color.Navy;
            this.btnNewPO.Location = new System.Drawing.Point(6, 6);
            this.btnNewPO.Name = "btnNewPO";
            this.btnNewPO.Size = new System.Drawing.Size(93, 23);
            this.btnNewPO.TabIndex = 0;
            this.btnNewPO.Text = "New P.Order";
            this.btnNewPO.UseVisualStyleBackColor = true;
            this.btnNewPO.Click += new System.EventHandler(this.btnNewPO_Click);
            // 
            // entdate
            // 
            this.entdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.entdate.Location = new System.Drawing.Point(107, 103);
            this.entdate.Name = "entdate";
            this.entdate.Size = new System.Drawing.Size(157, 20);
            this.entdate.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(485, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Selcect Department:";
            // 
            // cboDepartment
            // 
            this.cboDepartment.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDepartment.FormattingEnabled = true;
            this.cboDepartment.Location = new System.Drawing.Point(593, 96);
            this.cboDepartment.Name = "cboDepartment";
            this.cboDepartment.Size = new System.Drawing.Size(183, 22);
            this.cboDepartment.TabIndex = 21;
            this.cboDepartment.SelectedIndexChanged += new System.EventHandler(this.cboDepartment_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(602, 159);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Actual Cost:";
            // 
            // txtcost
            // 
            this.txtcost.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcost.Location = new System.Drawing.Point(667, 149);
            this.txtcost.Name = "txtcost";
            this.txtcost.ReadOnly = true;
            this.txtcost.Size = new System.Drawing.Size(110, 23);
            this.txtcost.TabIndex = 23;
            this.txtcost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTerms
            // 
            this.txtTerms.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTerms.Location = new System.Drawing.Point(107, 178);
            this.txtTerms.Name = "txtTerms";
            this.txtTerms.Size = new System.Drawing.Size(490, 62);
            this.txtTerms.TabIndex = 24;
            this.txtTerms.Text = "";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 217);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(87, 21);
            this.btnAdd.TabIndex = 25;
            this.btnAdd.Text = "Start Entry";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Quotation ID:";
            // 
            // txtQuotID
            // 
            this.txtQuotID.Location = new System.Drawing.Point(107, 128);
            this.txtQuotID.Name = "txtQuotID";
            this.txtQuotID.Size = new System.Drawing.Size(78, 20);
            this.txtQuotID.TabIndex = 27;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(191, 131);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 13);
            this.label9.TabIndex = 28;
            this.label9.Text = "Quotation Date:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 153);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "Deliver At:";
            // 
            // txtshipat
            // 
            this.txtshipat.Location = new System.Drawing.Point(107, 154);
            this.txtshipat.Name = "txtshipat";
            this.txtshipat.Size = new System.Drawing.Size(489, 20);
            this.txtshipat.TabIndex = 31;
            // 
            // txtquotdate
            // 
            this.txtquotdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtquotdate.Location = new System.Drawing.Point(270, 129);
            this.txtquotdate.Name = "txtquotdate";
            this.txtquotdate.Size = new System.Drawing.Size(115, 20);
            this.txtquotdate.TabIndex = 32;
            // 
            // txtdpt
            // 
            this.txtdpt.Location = new System.Drawing.Point(341, 101);
            this.txtdpt.Name = "txtdpt";
            this.txtdpt.Size = new System.Drawing.Size(73, 20);
            this.txtdpt.TabIndex = 33;
            this.txtdpt.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(600, 184);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 13);
            this.label11.TabIndex = 34;
            this.label11.Text = "All Discount:";
            // 
            // txtdiscount
            // 
            this.txtdiscount.Location = new System.Drawing.Point(667, 178);
            this.txtdiscount.Name = "txtdiscount";
            this.txtdiscount.Size = new System.Drawing.Size(108, 20);
            this.txtdiscount.TabIndex = 35;
            // 
            // txtdlvdate
            // 
            this.txtdlvdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtdlvdate.Location = new System.Drawing.Point(651, 128);
            this.txtdlvdate.Name = "txtdlvdate";
            this.txtdlvdate.Size = new System.Drawing.Size(124, 20);
            this.txtdlvdate.TabIndex = 37;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(581, 130);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 13);
            this.label12.TabIndex = 36;
            this.label12.Text = "Delivery Date:";
            // 
            // txtworktitle
            // 
            this.txtworktitle.Location = new System.Drawing.Point(6, 9);
            this.txtworktitle.Name = "txtworktitle";
            this.txtworktitle.Size = new System.Drawing.Size(580, 20);
            this.txtworktitle.TabIndex = 29;
            // 
            // frm_PurchaseOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 594);
            this.Controls.Add(this.txtdlvdate);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtdiscount);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtdpt);
            this.Controls.Add(this.txtquotdate);
            this.Controls.Add(this.txtshipat);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtQuotID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtTerms);
            this.Controls.Add(this.txtcost);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cboDepartment);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.entdate);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.txtRupees);
            this.Controls.Add(this.txtusrid);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.txtcontact);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPO);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frm_PurchaseOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purchase Order";
            this.Load += new System.EventHandler(this.frm_PurchaseOrder_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DG_POEntry)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView DG_POEntry;
        private System.Windows.Forms.TextBox txtPO;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblpo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtcontact;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.TextBox txtusrid;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtRupees;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtFndPO;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnNewPO;
        private System.Windows.Forms.DateTimePicker entdate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboDepartment;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtcost;
        private System.Windows.Forms.RichTextBox txtTerms;
        private System.Windows.Forms.TextBox txtQtyTotal;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQuotID;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtshipat;
        private System.Windows.Forms.DateTimePicker txtquotdate;
        private System.Windows.Forms.TextBox txtdpt;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtdiscount;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label lblworktitle;
        private System.Windows.Forms.TextBox txtAmtTotal;
        private System.Windows.Forms.Button btnNewItem;
        private System.Windows.Forms.TextBox txtNewItem;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDeletePO;
        private System.Windows.Forms.DateTimePicker txtdlvdate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtworktitle;
    }
}