using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;

namespace PDSystem
{
    class UploadDownload
    {
        public bool UploadFile()
        {
            //'Debug.Print FTPUpFile("ftp.fps.edu.pk", "d:\ftpdownload\testmdb.mdb", "testmdb.mdb", "/httpdocs/registration/", "2038027", "fps4547472")
            //Debug.Print FTPUpFile("hs.edu.pk", "D:\HSSCampus\HSSK_G\campusdb.mdb", "campusdb.mdb", "/httpdocs/registration/HSKG", "2057266", "hs4547473")



            string localPath = @"D:\PDSystem\";
            string fileName = "tms.xls";

            FtpWebRequest requestFTPUploader = (FtpWebRequest)WebRequest.Create("ftp://fps.edu.pk/httpdocs/registration/" + fileName);
            requestFTPUploader.Credentials = new NetworkCredential("2038027", "fps4547472");
            requestFTPUploader.Method = WebRequestMethods.Ftp.UploadFile;

            FileInfo fileInfo = new FileInfo(localPath + fileName);
            FileStream fileStream = fileInfo.OpenRead();

            int bufferLength = 2048;
            byte[] buffer = new byte[bufferLength];

            Stream uploadStream = requestFTPUploader.GetRequestStream();
            int contentLength = fileStream.Read(buffer, 0, bufferLength);

            while (contentLength != 0)
            {
                uploadStream.Write(buffer, 0, contentLength);
                contentLength = fileStream.Read(buffer, 0, bufferLength);
            }

            uploadStream.Close();
            fileStream.Close();

            requestFTPUploader = null;



            return true;
        }


        public bool DownloadFile()
        {

            //'Debug.Print FTPGetFile("ftp.fps.edu.pk", "d:\ftpdownload\fps_reg.mdb", "registration.mdb", "/httpdocs/registration/", "2038027", "fps4547472")
            //Debug.Print FTPGetFile("ftp.hs.edu.pk", "d:\ftpdownload\hss_reg.mdb", "registration.mdb", "/httpdocs/registration/", "2057266", "hs4547473")

            string localPath = @"D:\PDSystem\";
            string fileName = "tms.xls";

            FtpWebRequest requestFileDownload = (FtpWebRequest)WebRequest.Create("ftp://fps.edu.pk/httpdocs/registration/" + fileName);
            requestFileDownload.Credentials = new NetworkCredential("2038027", "fps4547472");
            requestFileDownload.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse responseFileDownload = (FtpWebResponse)requestFileDownload.GetResponse();

            Stream responseStream = responseFileDownload.GetResponseStream();
            FileStream writeStream = new FileStream(localPath + fileName, FileMode.Create);

            int Length = 2048;
            Byte[] buffer = new Byte[Length];
            int bytesRead = responseStream.Read(buffer, 0, Length);

            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Length);
            }

            responseStream.Close();
            writeStream.Close();

            requestFileDownload = null;
            responseFileDownload = null;




            return true;
        }


        public bool DeleteFile()
        {
            string fileName = "tms.xls";

            FtpWebRequest requestFileDelete = (FtpWebRequest)WebRequest.Create("ftp://fps.edu.pk/httpdocs/registration/" + fileName);
            requestFileDelete.Credentials = new NetworkCredential("2038027", "fps4547472");
            requestFileDelete.Method = WebRequestMethods.Ftp.DeleteFile;

            FtpWebResponse responseFileDelete = (FtpWebResponse)requestFileDelete.GetResponse();

            return true;
        }


        public void FtpDirectory()
        {

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://localhost/Source");
            request.Credentials = new NetworkCredential("khanrahim", "arkhan22");
            request.Method = WebRequestMethods.Ftp.ListDirectory;

            StreamReader streamReader = new StreamReader(request.GetResponse().GetResponseStream());

            string fileName = streamReader.ReadLine();

            while (fileName != null)
            {
                //Console.Writeline(fileName);
                fileName = streamReader.ReadLine();
            }

            request = null;
            streamReader = null;


        }


        public void LocalDirectory()
        {

            string localPath = @"G:\FTPTrialLocalPath\";

            string[] files = Directory.GetFiles(localPath);

            foreach (string filepath in files)
            {
                string fileName = Path.GetFileName(filepath);
                Console.WriteLine(fileName);
            }


        }





    }
}
