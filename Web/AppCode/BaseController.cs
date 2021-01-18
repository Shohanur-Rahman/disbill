using System;
using Core.Exceptions;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using Services.EmailService;
using System.Web.ModelBinding;
using System.Web;
using System.IO;
using System.Net;
using System.Data.SqlClient;
using System.Data;
using Services.Domain;

namespace Web.AppCode
{
    public class BaseController : Controller
    {
        
        public static string GetSiteSettingAdminEmal() {
            try
            {
                string strsql = "Select top 1 AdminEmailAddress from SiteSettings";
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                string settingsEmailAddress = "test@test.com";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(strsql, con))
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = strsql.Trim();
                        SqlDataReader rea = cmd.ExecuteReader();

                        while (rea.Read())
                        {
                            settingsEmailAddress = rea["AdminEmailAddress"].ToString();
                        }
                        con.Close();
                    }
                }

                return settingsEmailAddress;
            }
            catch (Exception ex) {
                throw ex;
            }
        }


        public static string GetIPAddress()
        {
            string ipAddress = "";

            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipAddress = Convert.ToString(IP);
                }
            }
            return ipAddress;
        }

        public string GetUploadImagePath(HttpPostedFileBase file, string folderName)
        {
            try
            {
                string imagePath = "";
                string fileName = "";
                Guid fileNameInGuid = Guid.NewGuid();
                string savingPath = Server.MapPath("~/uploads/" + folderName + "/");

                if (!Directory.Exists(savingPath))
                {
                    Directory.CreateDirectory(savingPath);
                }

                if (file.ContentLength > 0)
                {
                    //Use Namespace called :  System.IO  
                    fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    //To Get File Extension  
                    string extension = Path.GetExtension(file.FileName);

                    //Add Current Date To Attached File Name  
                    fileName = DateTime.Now.ToString("yyyy-MM-dd") + "-" + fileNameInGuid.ToString() + "-" + fileName.Trim() + extension;

                    savingPath = savingPath + fileName;
                    imagePath = "/uploads/" + folderName + "/" + fileName;
                    file.SaveAs(savingPath);

                }

                return imagePath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetUserActivitionKey()
        {
            Guid key = Guid.NewGuid();
            return key.ToString().ToUpper();
        }

        protected virtual ActionResult DataActionViewService(Action dataAction, Func<ActionResult> dataActionComplete,
            Func<ActionResult> dataActionErrorAction)
        {
            var errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();

            if (ModelState.IsValid)
            {
                try
                {
                    dataAction();

                }
                catch (CoreException ex)
                {

                    Emailer.SendMail("shohanur.rahman57@gmail.com", "Error From Core Exception Block Base Controller", ex.ErrorCode + "<br /> " + ex.Message);

                    ModelState.AddModelError(String.Empty, ExceptionMapper.MapException(ex).ViewMessage);
                    //MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, ex.Message);
                    return dataActionErrorAction();
                }
                catch (Exception ex)
                {

                    Emailer.SendMail("shohanur.rahman57@gmail.com", "Error From Exception Block Base Controller", ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "MessageConstants.ViewServiceDefaultErrorMessage");
                List<System.Web.Mvc.ModelState> errorList = ModelState.Values.ToList();
                string errorMessage = "";
                foreach (System.Web.Mvc.ModelState err in errorList)
                {
                    errorMessage += err.Value;
                }

                if (String.IsNullOrEmpty(errorMessage) == false)
                    Emailer.SendMail("shohanur.rahman57@gmail.com", "Error from IsValid False, Base Controller", errorMessage);
                return dataActionErrorAction();

            }

            return dataActionComplete();
        }

    }
}