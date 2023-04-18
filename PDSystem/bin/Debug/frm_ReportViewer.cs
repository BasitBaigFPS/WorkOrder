using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient; 
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Reflection;
using System.Security;
using System.Drawing.Printing;
using System.Drawing.Imaging;   
using System.Security.Permissions;
using System.Security.Policy;     
using System.IO;
using WordAmount; 



namespace PDSystem
{
    public partial class frm_ReportViewer : Form
    {
        ProjEntry rptobj = new ProjEntry();
    
        private DataTable tblEstMaster = null;
        private DataTable tblEstDetail = null;
        private DataTable tblPODetail = null;

        private string sqlQuery;
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataSet ds;

        AmountInWords rupeeword = new AmountInWords();
        private string EstNo;
        private string RptAmt;
        private string MyRptName;
        private double TotalCost;
        ReportViewer reportViewer = new ReportViewer();

        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        private bool allpayment;

        public bool Allpayment
        {
            get { return allpayment; }
            set { allpayment = value; }
        }


        private void SetDataObjects()
        {
            ConnectionManager conobj = new ConnectionManager();
            connection = new SqlConnection(conobj.conn);
            command = new SqlCommand(sqlQuery, connection);
            adapter = new SqlDataAdapter(command);
        }


        private DataTable LoadtblEstMaster()
        {
            sqlQuery = "Select * from View_ProjEstimate";
            SetDataObjects(); 
            connection.Open();
            ds = new DataSet();
            adapter.Fill(ds);
            //return ds.Tables[0];
            DataTable t1 = ds.Tables[0];
            return t1;
        }

        private DataTable ProjectTotalCost()
        {
            
            sqlQuery = "Select ESTIMATENO,COST,PayInProcess from View_ProjEstimateALL";
            SetDataObjects();
            connection.Open();
            ds = new DataSet();
            adapter.Fill(ds);
            
            DataTable t1 = ds.Tables[0];
            return t1;
        }
        

        private DataTable LoadADDtblEstMaster()
        {
            sqlQuery = "Select * from View_ProjEstimateAdditional";
            SetDataObjects();
            connection.Open();
            ds = new DataSet();
            adapter.Fill(ds);
            DataTable t2 = ds.Tables[0];
            ds.Tables.Add();
            return t2;
            //return ds.Tables[1];
        }


        private DataTable LoadtblEstDetail(bool isall)
        {
            if (isall == true)
            {
                sqlQuery = "Select * from View_EstPayRequestAll";
                
            }
            else
            {
                sqlQuery = "Select * from View_EstPayRequest";
            }

            SetDataObjects(); 
            connection.Open();
            ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];
        }

        private DataTable LoadtblPOMain()
        {
            sqlQuery = "Select * from View_POMain";
            SetDataObjects();
            connection.Open();
            ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];
        }

        private DataTable LoadtblPODetail()
        {
            sqlQuery = "Select * from View_PODetail";
            SetDataObjects();
            connection.Open();
            ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];
        }

        public frm_ReportViewer(bool allreport)
        {
            //InitializeComponent();

            try
            {
                Allpayment = allreport;

                this.ClientSize = new System.Drawing.Size(800, 600);
                reportViewer.ProcessingMode = ProcessingMode.Local;
                MyRptName = ProjEntry.Rptname;

                if (MyRptName == null)
                {
                    reportViewer.LocalReport.ReportPath = "Rpt_ProjEstSumm.rdlc";
                }
                else
                {
                    reportViewer.LocalReport.ReportPath = MyRptName;
                }
               
                ReportParameter parameters = new ReportParameter();
                                 
                GetAmountWord();
                ReportParameter p2;
                ReportParameter p3;
                p2 = new ReportParameter("RptAmount", RptAmt);
                p3 = new ReportParameter("TotalCost", TotalCost.ToString());

                switch (MyRptName)
                {
                    case "Rpt_ProjEstSumm.rdlc":
                        reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DS_PWR", LoadtblEstMaster()));
                       // parameters = new ReportParameter("RptAmount", RptAmt);

                        p2 = new ReportParameter("RptAmount", RptAmt);
                        p3 = new ReportParameter("TotalCost", TotalCost.ToString());
                       
                        break;

                    case "Rpt_ProjEstimate.rdlc":
                        reportViewer.LocalReport.DataSources.Add(new ReportDataSource("PWRDataSet", LoadtblEstMaster()));
                        parameters = new ReportParameter("RptCost", RptAmt);
                        break;

                    case "Rpt_ProjEstSummary.rdlc":

                        DataTable t1 = LoadtblEstMaster();

                        reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DS_PWR", t1));
                        reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DS_PWRADD", LoadADDtblEstMaster()));
                        parameters = new ReportParameter("RptAmount", RptAmt);
                        parameters = new ReportParameter("TotalCost", TotalCost.ToString());
                        //parameters = new ReportParameter("EstNo", t1.Rows[0]["EstimateNo"].ToString());
                       
 

                        reportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(Report_SubreportProcessing);

                        //ReportParameter p1 = new ReportParameter("EstNo", t1.Rows[1]["EstimateNo"].ToString());

                        //ReportParameter p2 = new ReportParameter("RptAmount", RptAmt);
                        //ReportParameter p3 = new ReportParameter("TotalCost", TotalCost.ToString());
                        //reportViewer.LocalReport.SetParameters(p2);
                        //reportViewer.LocalReport.SetParameters(p3);

                        break;

                    case "Rpt_POMain.rdlc":
                        reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DS_POMain", LoadtblPOMain()));
                        parameters = new ReportParameter("RptAmount", RptAmt);
                        break;


                    default:
                        break;
                }
                
                
                //reportViewer.LocalReport.SetParameters(parameters);

                reportViewer.LocalReport.SetParameters(p2);

                if (MyRptName != "Rpt_POMain.rdlc")
                {
                    reportViewer.LocalReport.SetParameters(p3);
                }
               reportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(Report_SubreportProcessing);



                reportViewer.Dock = DockStyle.Fill;
                this.Controls.Add(reportViewer);
                reportViewer.RefreshReport();
                
                //Use This Option to Open Printer Dialog and Select Desire Printer
                      //reportViewer.RenderingComplete  +=new Microsoft.Reporting.WinForms.RenderingCompleteEventHandler(reportViewer_RenderingComplete);


                //Use This Option to Print Report Directly To the Printer.
                    //LocalReport report = new LocalReport();
                    //report.ReportPath = @"..\..\"+MyRptName;
                    //report.SetParameters(parameters);
                    //report.DataSources.Add(new ReportDataSource("DS_PWR", LoadtblEstMaster()));
                    //report.SubreportProcessing += new SubreportProcessingEventHandler(report_SubreportProcessing);
                    //Export(report);
                    //Print();


               // CreatePDF(MyRptName); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());  
            }

        }

        void reportViewer_RenderingComplete(object sender, Microsoft.Reporting.WinForms.RenderingCompleteEventArgs e)
        {
            //throw new NotImplementedException();

            reportViewer.PrintDialog();  
        }

        private void GetAmountWord()
        {
            DataTable tbl = new DataTable();
            double amt = 0;

            //if (MyRptName == "Rpt_ProjEstSumm.rdlc" || MyRptName == "Rpt_ProjEstSummary.rdlc")
            //{
            //    tbl = LoadtblEstDetail(allpayment);
            //    for (int i = 0; i < tbl.Rows.Count; i++)
            //    {
            //        amt = amt + float.Parse(tbl.Rows[i]["PayRecmd"].ToString() == null ? "0" : tbl.Rows[i]["PayRecmd"].ToString());
            //    }
            //}

            //if (MyRptName == "Rpt_ProjEstimate.rdlc")
            //{
            //    tbl = LoadtblEstMaster();
            //    for (int i = 0; i < tbl.Rows.Count; i++)
            //    {
            //        amt = amt + float.Parse(tbl.Rows[i]["Cost"].ToString() == null ? "0" : tbl.Rows[i]["Cost"].ToString());
            //    }
            //}

            if (MyRptName == "Rpt_POMain.rdlc")
            {
                tbl = LoadtblPODetail();
                double disc = double.Parse(tbl.Rows[0]["Discount"].ToString() == null ? "0" : tbl.Rows[0]["Discount"].ToString());
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    amt = amt + float.Parse(tbl.Rows[i]["Amount"].ToString() == null ? "0" : tbl.Rows[i]["Amount"].ToString());
                }
                amt = amt - disc;
            }

            if (MyRptName == "Rpt_ProjEstSumm.rdlc" || MyRptName == "Rpt_ProjEstSummary.rdlc" || MyRptName == "Rpt_ProjEstimate.rdlc")
            {

                tbl = ProjectTotalCost();
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    TotalCost = TotalCost + float.Parse(tbl.Rows[i]["Cost"].ToString() == null || tbl.Rows[i]["Cost"].ToString() == "" ? "0" : tbl.Rows[i]["Cost"].ToString());
                    amt = amt + float.Parse(tbl.Rows[i]["PayInProcess"].ToString() == null || tbl.Rows[i]["PayInProcess"].ToString() == "" ? "0" : tbl.Rows[i]["PayInProcess"].ToString());
                }

                TotalCost = amt;

            }

            RptAmt = rupeeword.changeCurrencyToWords(amt);
            
        }

        void Report_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            //throw new NotImplementedException();
            //MessageBox.Show(sender.ToString());

            //var mainSource = ((LocalReport)sender).DataSources["PRequest_DS"];
            //var EstNo = int.Parse(e.Parameters["EstNo"].Values.First());
            //var subSource = ((List<Order>)mainSource.Value).Single(o => o.EstimateNo == EstNo).tblEstDetail;
            //e.DataSources.Add(new ReportDataSource("PRequest_DS", subSource));

            if (MyRptName == "Rpt_POMain.rdlc")
            {
                if (tblPODetail == null)
                    tblPODetail = LoadtblPODetail();
                e.DataSources.Add(new ReportDataSource("DS_PO", tblPODetail));
            }
            if (MyRptName == "Rpt_ProjEstSumm.rdlc" || MyRptName == "Rpt_ProjEstSummary.rdlc")
            {

                if (e.ReportPath == "SubRpt_AddCostSummary")
                {
                    if (tblEstMaster == null)
                        tblEstMaster = LoadADDtblEstMaster(); ;
                    e.DataSources.Add(new ReportDataSource("DS_PWRADD", tblEstMaster));
                }
                else
                {

                    if (tblEstDetail == null)
                        tblEstDetail = LoadtblEstDetail(Allpayment);
                    e.DataSources.Add(new ReportDataSource("PRequest_DS", tblEstDetail));
                }
            }




 
                //for subreport databinding
                //SqlConnection con = new SqlConnection(connectionString);
                //con.Open();

                //SqlCommand cmd = new SqlCommand("PMRTicketConsumablesRpt", con);
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@TicketID", Request.QueryString["TicketID"].Trim());

                //DataTable dtConsumables = new DataTable();

                //SqlDataAdapter da = new SqlDataAdapter(cmd);

                //da.Fill(dtConsumables);

                //// define your report data source in your .rdlc report file and enclosed with open and close parenthesis
                //e.DataSources.Add(new ReportDataSource("dsPMRConsumables", dtConsumables));        



             
        }

        private void CreatePDF(string fileName)
        {
            // Variables 
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            // Setup the report viewer object and get the array of bytes 
            //ReportViewer viewer = new ReportViewer();
            //viewer.ProcessingMode = ProcessingMode.Local;
            //viewer.LocalReport.ReportPath = fileName;

            //byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            byte[] bytes = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            
            string pdffile = fileName.Substring(0,(fileName.Length-5));
            //pdffile = "D:/" + pdffile + ".pdf"; 
            MyFunctions objfunc = new MyFunctions();
            objfunc.CreateFileFolder();  

            pdffile = @"D:\pwr\pdf\PaymentRequest_" + ProjEntry.PWRNumber.ToString();
            pdffile = pdffile + ".pdf";
            
            using (FileStream fs = new FileStream(pdffile, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }

            //ASP Code
            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client. 
            //Response.Buffer = true;
            //Response.Clear();
            //Response.ContentType = mimeType;
            //Response.AddHeader("content-disposition", "attachment; filename=" + fileName + "." + extension);
            //Response.BinaryWrite(bytes); // create the file 
            //Response.Flush(); // send it to the client to download 
        }


        //Here is the Procedure / Method For Direct Printing.
        //void report_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        //{
        //    //throw new NotImplementedException();

        //    if (MyRptName == "Rpt_POMain.rdlc")
        //    {
        //        if (tblPODetail == null)
        //            tblPODetail = LoadtblPODetail();
        //        e.DataSources.Add(new ReportDataSource("DS_POMain", tblPODetail));
        //    }
        //    else
        //    {
        //        if (tblEstDetail == null)
        //            tblEstDetail = LoadtblEstDetail(Allpayment);

        //      //  e.DataSources.Add(new ReportDataSource("DS_PWRADD", tblEstDetail));
        //        e.DataSources.Add(new ReportDataSource("PRequest_DS", tblEstDetail));

        //    }



        //}

        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        private void Export(LocalReport report)
        {
            string deviceInfo =
              @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>8.5in</PageWidth>
                <PageHeight>11in</PageHeight>
                <MarginTop>0.25in</MarginTop>
                <MarginLeft>0.25in</MarginLeft>
                <MarginRight>0.25in</MarginRight>
                <MarginBottom>0.25in</MarginBottom>
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream, out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        private void Print()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
                DisposeMe(); 
            }
        }

        public void DisposeMe()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }



    }
}

//PermissionSet permissions = new PermissionSet(PermissionState.None);
//permissions.AddPermission(new FileIOPermission(PermissionState.Unrestricted)); 
//permissions.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution)); 
//reportViewer.LocalReport.SetBasePermissionsForSandboxAppDomain(permissions); 
//Assembly asm = Assembly.Load("WordAmount, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"); 
//AssemblyName asm_name = asm.GetName();
//reportViewer.LocalReport.AddFullTrustModuleInSandboxAppDomain(new StrongName(new StrongNamePublicKeyBlob(asm_name.GetPublicKeyToken()), asm_name.Name, asm_name.Version));            


//reportViewer.LocalReport.AddFullTrustModuleInSandboxAppDomain("WordAmount", Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//reportViewer.LocalReport.SetBasePermissionsForSandboxAppDomain(new PermissionSet(PermissionState.Unrestricted));
//reportViewer.LocalReport.AddTrustedCodeModuleInCurrentAppDomain("WordAmount, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
//reportViewer.LocalReport.ReportPath = "Rpt_ProjEstSummary.rdlc";
//reportViewer.LocalReport.DataSources.Add(new ReportDataSource("PWRDataSet", LoadtblEstMaster()));

//ReportParameter RptAmount = new ReportParameter();
//RptAmount.Values.Add(RptAmt);
//reportViewer.LocalReport.SetParameters(RptAmt);


//Private Sub LoadRDLCFromStream() 
//Dim asm As Assembly = Assembly.Load("ReportLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null") 
//Dim ioStream As IO.Stream = asm.GetManifestResourceStream("ReportLibrary.MasterSubReportContainer.rdlc") 
//Me.ReportViewer1.LocalReport.LoadReportDefinition(ioStream) 
//End Sub 
//System.IO.FileInfo rpt = new System.IO.FileInfo(Application.StartupPath + @"..\..\..\Rpt_ProjEstSummary.rdlc");


//  <CodeGroup
//class="FirstMatchCodeGroup"
//version="1"
//PermissionSetName="FullTrust"
//Name="WordAmountAssembly"
//Description="A special code group for my custom assembly.">
//    <IMembershipCondition
//    class="UrlMembershipCondition"
//    version="1"
//    Url="D:\PDSystem\PDSystem\bin\Debug\WordAmount.dll"/>
//  </CodeGroup>



