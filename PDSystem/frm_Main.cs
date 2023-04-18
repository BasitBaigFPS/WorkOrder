using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace PDSystem
{
    public partial class frm_Main : Form
    {
        private int childFormNumber = 0;
        //MyBaseFunc mybaseobj = new MyBaseFunc(); 

        public frm_Main()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Close();
            Application.Exit();  
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void frm_Main_Load(object sender, EventArgs e)
        {
            
            //this.lblUserName.Text = Login.Cuser;
            toolStripStatusLabel.Text = Login.Cuser;  
            this.statusStrip.Refresh();

            if (Login.CuserGP == 13)
            {
                frm_Payments paymentform;
                paymentform = new frm_Payments();
                paymentform.MdiParent = this;
                paymentform.Show();
            }
            else
            {
                frm_EntryMain entryform;
                entryform = new frm_EntryMain();
                entryform.MdiParent = this;
                //this.Close();
                entryform.Show();
                //entryform.WindowState = FormWindowState.Maximized;    
            }


            //mybaseobj.RemoveMenuItems(Handle);
        }

        

        private void frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //get a variable to hold the users response
            DialogResult result;
            //set the variable to the messagebox results
            result = MessageBox.Show("Are you sure? Do you want to exit this application.", "Exit Application?",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            //check the users response before deciding what to do

            if (result.ToString() == "OK") //user clicked ok
            {
                //close the application
                e.Cancel = false;
                Application.Exit();  
            }
            else //user clicked cancel
            {
                //keep the application open
                e.Cancel = true;
            }

        }

        private void newworkitem_Click(object sender, EventArgs e)
        {
            frm_EntryMain entryform;
            entryform = new frm_EntryMain();
            entryform.MdiParent = this;
            //this.Close();
            entryform.Show(); 

        }

        private void paymentPostingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Payments paymentform;
            paymentform = new frm_Payments();
            paymentform.MdiParent = this;
            paymentform.Show(); 

        }

 
        private void printPaymentRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            frm_ReportViewer Reportform;
            Reportform = new frm_ReportViewer(false);
            Reportform.Show();

        }

        private void printPurchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjEntry.Rptname = "Rpt_POMain.rdlc";
            frm_ReportViewer Reportform;
            Reportform = new frm_ReportViewer(false);
            Reportform.Show(); 
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();  
        }



    }
}
