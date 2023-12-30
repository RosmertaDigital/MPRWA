using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace BMHSRPv2
{
    /// <summary>
    /// Summary description for FileUploadHandler
    /// </summary>
    public class FileUploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                //Fetch the Uploaded File.

                if (context.Request.Files.Count == 1)
                {

                
                 #region Rc upload 
              
                HttpPostedFile postedFile = context.Request.Files[0];

                //Set the Folder Path.
                string folderPath = ConfigurationManager.AppSettings["RCFilePath"].ToString();

                //Set the File Name.
                string fileName = Path.GetFileName(postedFile.FileName);
                string GetExtention= Path.GetExtension(postedFile.FileName);
                    double sz = postedFile.ContentLength / 1024;
                sz = sz / 1024;
                if (!IsImage(Path.GetExtension(postedFile.FileName)))
                    fileName = "Error! Invalid image file!!";
                else if(sz >1)
                    fileName = "Error! File size can not be max 1.5 MB!!";
                else
                {
                        //Save the File in Folder.
                        // fileName = "Plate"+DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + fileName.Replace(" ","");
                        fileName = "Plate" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + RandomString(4)+ GetExtention;


                    postedFile.SaveAs(@folderPath + fileName);
                }

                

                //Send File details in a JSON Response.
                string json = new JavaScriptSerializer().Serialize(
                    new
                    {
                        name = fileName
                    });
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.ContentType = "text/json";
                context.Response.Write(json);
                context.Response.End();
                    #endregion
                }
                //if(context.Request.Files.Count == 2)
                //{
                //    #region Laser Upload 

                //    HttpPostedFile postedFile = context.Request.Files[0];
                //    HttpPostedFile postedFile2 = context.Request.Files[1];

                //    //Set the Folder Path.
                //    string folderPath = ConfigurationManager.AppSettings["RCFilePath"].ToString();

                //    //Set the File Name.
                //    string fileName = Path.GetFileName(postedFile.FileName);
                //    string GetExtention = Path.GetExtension(postedFile.FileName);
                //    string fileName2= Path.GetFileName(postedFile2.FileName);
                //    string GetExtention2 = Path.GetExtension(postedFile2.FileName);

                //    double sz = postedFile.ContentLength / 1024;
                //    double sz2 = postedFile2.ContentLength / 1024;
                //    sz = sz / 1024;

                //    sz2 = sz2 / 1024;

                //    if (!IsImage(Path.GetExtension(postedFile.FileName)) || !IsImage(Path.GetExtension(postedFile2.FileName)))
                //        fileName = "Error! Invalid image file format, file should be .jpg|.jpeg|.bmp|.png|.pdf!!";
                //    else if (sz > 1 || sz2>1)
                //        fileName = "Error! File size can not be max 1.5 MB!!";
                //    else
                //    {
                //        //Save the File in Folder.
                //        //fileName = "Front" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + fileName.Replace(" ", "");
                //        fileName = "Front" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + RandomString(4)+ GetExtention;
                //        postedFile.SaveAs(@folderPath + fileName);
                //        //fileName2 = "Rear" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + fileName2.Replace(" ", "");
                //        fileName2 = "Rear" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + RandomString(4)+ GetExtention2;
                //        postedFile.SaveAs(@folderPath + fileName);
                //        postedFile.SaveAs(@folderPath + fileName2);
                //    }



                //    //Send File details in a JSON Response.
                //    string json = new JavaScriptSerializer().Serialize(
                //        new
                //        {
                //            name = fileName,name2= fileName2
                //        });
                //    context.Response.StatusCode = (int)HttpStatusCode.OK;
                //    context.Response.ContentType = "text/json";
                //    context.Response.Write(json);
                //    context.Response.End();
                //    #endregion

                //}


                if (context.Request.Files.Count == 4)
                {
                    #region Laser Upload 

                    HttpPostedFile postedFile = context.Request.Files[0];
                    HttpPostedFile postedFile2 = context.Request.Files[1];
                    HttpPostedFile postedFile3 = context.Request.Files[2];
                    HttpPostedFile postedFile4 = context.Request.Files[3];

                    //Set the Folder Path.
                    string folderPath = ConfigurationManager.AppSettings["RCFilePath"].ToString();

                    //Set the File Name.
                    string fileName = Path.GetFileName(postedFile.FileName);
                    string GetExtention = Path.GetExtension(postedFile.FileName);
                    string fileName2 = Path.GetFileName(postedFile2.FileName);
                    string GetExtention2 = Path.GetExtension(postedFile2.FileName);
                    string fileName3 = Path.GetFileName(postedFile3.FileName);
                    string GetExtention3 = Path.GetExtension(postedFile3.FileName);
                    string fileName4 = Path.GetFileName(postedFile4.FileName);
                    string GetExtention4 = Path.GetExtension(postedFile4.FileName);

                    double sz = postedFile.ContentLength / 1024;
                    double sz2 = postedFile2.ContentLength / 1024;
                    double sz3 = postedFile3.ContentLength / 1024;
                    double sz4 = postedFile4.ContentLength / 1024;
                    sz = sz / 1024;
                    sz2 = sz2 / 1024;
                    sz3 = sz3 / 1024;
                    sz4 = sz4 / 1024;

                    if (!IsImage(Path.GetExtension(postedFile.FileName)) || !IsImage(Path.GetExtension(postedFile2.FileName)) || !IsImage(Path.GetExtension(postedFile3.FileName)) || !IsImage(Path.GetExtension(postedFile4.FileName)))
                        fileName = "Error! Invalid image file format, file should be .jpg|.jpeg|.bmp|.png|.pdf!!";
                    else if (sz > 1 || sz2 > 1 || sz3 > 1 || sz4 > 1)
                        fileName = "Error! File size can not be max 1.5 MB!!";
                    else
                    {
                        //Save the File in Folder.
                        //fileName = "Front" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + fileName.Replace(" ", "");
                        fileName = "Front" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + RandomString(4) + GetExtention;
                        postedFile.SaveAs(@folderPath + fileName);
                        //fileName2 = "Rear" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + fileName2.Replace(" ", "");
                        fileName2 = "Rear" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + RandomString(4) + GetExtention2;
                        postedFile2.SaveAs(@folderPath + fileName2);

                        fileName3 = "File1" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + RandomString(4) + GetExtention3;
                        postedFile3.SaveAs(@folderPath + fileName3);

                        fileName4 = "File2" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + RandomString(4) + GetExtention4;
                        postedFile4.SaveAs(@folderPath + fileName4);
                    }



                    //Send File details in a JSON Response.
                    string json = new JavaScriptSerializer().Serialize(
                        new
                        {
                            name = fileName,
                            name2 = fileName2,
                            name3 = fileName3,
                            name4 = fileName4
                        });
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.ContentType = "text/json";
                    context.Response.Write(json);
                    context.Response.End();
                    #endregion

                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public bool IsImage(string Ext)
        {
            foreach (string st in ".jpg|.jpeg|.bmp|.png|.pdf".Split('|'))
            {
                if (Ext.ToLower() == st)
                    return true;
            }
            return false;
        }

        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}