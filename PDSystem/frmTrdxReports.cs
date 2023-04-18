using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Mail;
using System.IO;
using Telerik.Reporting.Processing;
using Telerik.WinControls;
using Telerik.Reporting;
using Telerik.Reporting.Drawing;
using Telerik.ReportViewer.WinForms;
using Telerik.Reporting.Xml;
using System.Drawing.Printing;
using System.Configuration;
using System.Globalization;



namespace PDSystem
{


    public partial class frmTrdxReports : Form
    {
        MyFunctions OBJFuncLib = new MyFunctions();
        ProjEntry PEObj = new ProjEntry();          

        ConnectionManager conobj = new ConnectionManager();

        private string sqlQuery;
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataSet ds;

        

        

        public frmTrdxReports()
        {
            InitializeComponent();
        }


        public frmTrdxReports(string reportname)
        {
    
             InitializeComponent();

            AddWorkOrderRpt(reportname);


            //Telerik.Reporting.Report report = new Telerik.Reporting.Report();

            //report.DocumentName = reportname;
            ////report.DataSource = ods;

            //Telerik.Reporting.InstanceReportSource reportSource = new Telerik.Reporting.InstanceReportSource();
            //reportSource.ReportDocument = report;

            ////reportSource.Parameters.Add(new Telerik.Reporting.Parameter("EstimateNo", estno));

            //reportViewer1.ReportSource = reportSource;
            //reportViewer1.RefreshReport();



        }

        private void frmTrdxReports_Load(object sender, EventArgs e)
        {
            
        }

        private void SetDataObjects()
        {
            ConnectionManager conobj = new ConnectionManager();
            connection = new SqlConnection(conobj.conn);
            command = new SqlCommand(sqlQuery, connection);
            adapter = new SqlDataAdapter(command);
        }


        public void AddWorkOrderRpt(string reportname)
        {
            SetDataObjects();
            connection = new SqlConnection(conobj.conn);

            //var connectionString = "Data Source=.\\SQLEXPRESS;AttachDbFilename=" + System.IO.Path.GetFullPath("BRSMS.mdf") + ";Connect Timeout=30;User Instance=True; Integrated Security=SSPI";

            var connectionStringHandler = new ReportConnectionStringManager(connection.ConnectionString);

            var sourceReportSource = new UriReportSource { Uri = reportname };

            var reportSource = connectionStringHandler.UpdateReportSource(sourceReportSource);

            //reportSource.Parameters.Add(new Telerik.Reporting.Parameter("EstimateNo", estno));

            this.reportViewer1.ReportSource = reportSource;
            this.reportViewer1.RefreshReport();

            //========================================================================================


            //var ods = new Telerik.Reporting.ObjectDataSource();
            //ods.DataSource = "GIN_DS";
            //ods.DataMember = "View_GoodsIssueNote"; //table to bind to  

            //Telerik.Reporting.Report report = new Telerik.Reporting.Report();

            //report.DocumentName = reportname;
            ////report.DataSource = ods;

            //Telerik.Reporting.InstanceReportSource reportSource = new Telerik.Reporting.InstanceReportSource();
            //reportSource.ReportDocument = report;

            ////reportSource.Parameters.Add(new Telerik.Reporting.Parameter("EstimateNo", estno));

            //reportViewer1.ReportSource = reportSource;
            //reportViewer1.RefreshReport();






        }


    }
}
