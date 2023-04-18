using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PDSystem
{
    public partial class POSpec : Form
    {
        PurchaseOrder POObj = new PurchaseOrder(); 



        public POSpec()
        {
            InitializeComponent();

            lblpono.Text = lblpono.Text  + PurchaseOrder.Pono2.ToString();
            lblitem.Text = lblitem.Text  + PurchaseOrder.ItemName.ToString();

            if (PurchaseOrder.DetailSpec != "")
            {
                this.pospecs.Text = PurchaseOrder.DetailSpec;
            }

        }


        private void btnClose_Click(object sender, EventArgs e)
        {

            PurchaseOrder.DetailSpec = this.pospecs.Text;  
            
            this.Close();
        }
    }
}
