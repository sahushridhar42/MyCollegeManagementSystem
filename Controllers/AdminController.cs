
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CollegeManagementSystemDE;
using System.Net.Mail;
using System.Net;
namespace CollegeManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        #region[Instance Variables Declaration]

        public string query;
        string userid;
        string FirstName;
        string LastName;
        string Role;

        #endregion

        #region[Connection]

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

        #endregion

        #region[Get Current Date]

        public JsonResult GetDate()
        {
            string cdate = System.DateTime.Now.ToShortDateString();
            return Json(cdate, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region[Login]

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Login(string Loginname, string password)
        {
            string date = System.DateTime.Now.Date.ToString();
            string month = System.DateTime.Now.Month.ToString();
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            //try
            //{
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from UserMaster where LoginName=@LoginName and Password=@Password", con);
                cmd.Parameters.AddWithValue("@LoginName", Loginname);
                cmd.Parameters.AddWithValue("@Password", password);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    userid = dr["UserID"].ToString();
                    FirstName = dr["FirstName"].ToString();
                    LastName = dr["LastName"].ToString();
                    Role = dr["Role"].ToString();
                    Session["uid"] = userid;
                    Session["username"] = FirstName + " " + LastName;
                    Session["Role"] = Role;
                    Session["BranchID"] = dr["branchID"].ToString();
                    Session["AcadamicYear"] = dr["AcadamicYear"].ToString();
                    dr.Close();
                    return Json(1, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    dr.Close();
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            //}
            //finally
            //{
              //  con.Close();
          //  }
        }

        public JsonResult VarifyUserName(string loginname)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from UserMaster where LoginName=@LoginName", con);
            cmd.Parameters.AddWithValue("@LoginName", loginname);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                string id = dr["UserID"].ToString();
                
                return Json(id, JsonRequestBehavior.AllowGet);
            }
           
            else
            {
            return Json(0, JsonRequestBehavior.AllowGet);
            }
          //  con.Close();
        }

        public JsonResult GetPassword(string loginname)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select Password,EmailID from UserMaster where LoginName=@LoginName", con);
            cmd.Parameters.AddWithValue("@LoginName", loginname);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                string password = dr["Password"].ToString();
                string email = dr["EmailID"].ToString();
                //mail start 

                string message = "Hi" +
            "<h4>Your UserName is: " + loginname + "</h4><br><h4>Your Password Is:" + password + "</h4>" +
            "<br><h3> Thank You For Joining With Us</h3>";
                if (!string.IsNullOrEmpty(email))
                {
                    MailMessage mm = new MailMessage("sender@gmail.com", email);
                    mm.Subject ="Change Password";
                    mm.Body = message;
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential();
                    NetworkCred.UserName = "pvpp@gmail.com";
                    NetworkCred.Password = "PVPPCOLLEGE";
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    
                    smtp.Send(mm);
                }

                //mail end

                return Json("Password Send to mail please check.............!", JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json("Wrong User Name ..............!", JsonRequestBehavior.AllowGet);
            }
            //con.Close();
        }

        public JsonResult ChangePassword(string password,string userid)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE UserMaster SET Password =@Password where UserID=@UserID", con);
            cmd.Parameters.AddWithValue("@Password", password);
            cmd.Parameters.AddWithValue("@UserID", userid);
            int flg=cmd.ExecuteNonQuery();
            return Json(flg, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region[Send Mail]

        public void sendmail(string email, string password,string loginname)
        {
            string message = "Hi {0},<br /><br />You are Registered Successfully As Member Of Pvppcoe College Of Engineering.<br />" +
            "<h4>Your UserName is: " + loginname + "</h4><br><h4>Your Password Is:" + password + "</h4>" +
            "<br><h3> Thank You For Joining With Us</h3>";
            if (!string.IsNullOrEmpty(email))
            {
                MailMessage mm = new MailMessage("sender@gmail.com", email);
                mm.Subject = "Order Pacement";
                mm.Body = message;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential();
                NetworkCred.UserName = "College@pvpp.com";
                NetworkCred.Password = "pvppcoe";
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                //smtp.Port=465;
                smtp.Send(mm);
            }


        }

        #endregion

        #region[Register]

        public ActionResult Register()
        {
            if (Session["uid"] != null)
            {
                return View();
            }
            else 
            {
                return View("Index");
            }
        }

        public JsonResult RegisterUser(string fname1, string lname1, string branch, string role, string Acadamicyear, string admissionDatee, string caddress, string paddress, string contactno, string email, string gender, string loginname, string password, string gname, string gcontact, string gaddress, string gprofession, string exp, string cast, string fileuplode,string qua)
        {
            string year = (System.DateTime.Now.Year.ToString());
            int uid = Convert.ToInt32(GenerateNumber());
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_UserRegister", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", uid);
            cmd.Parameters.AddWithValue("@FirstName", fname1);
            cmd.Parameters.AddWithValue("@LastName", lname1);
            cmd.Parameters.AddWithValue("@AdmissionYear", admissionDatee);
            cmd.Parameters.AddWithValue("@CurrentAddress", caddress);
            cmd.Parameters.AddWithValue("@PermanentAddress", paddress);
            cmd.Parameters.AddWithValue("@ContactNo", contactno);
            cmd.Parameters.AddWithValue("@EmailID", email);
            cmd.Parameters.AddWithValue("@Gender", gender);
            cmd.Parameters.AddWithValue("@LoginName", loginname);
            cmd.Parameters.AddWithValue("@Password", password);
            cmd.Parameters.AddWithValue("@Role", role);
            cmd.Parameters.AddWithValue("@AcadamicYear", Acadamicyear);
            cmd.Parameters.AddWithValue("@BranchID",Convert.ToInt32(branch));
            cmd.Parameters.AddWithValue("@AddYear",year);
            cmd.Parameters.AddWithValue("@Experiense", exp);
            cmd.Parameters.AddWithValue("@Cast", cast);
            cmd.Parameters.AddWithValue("@fileuplode", fileuplode);
            cmd.Parameters.AddWithValue("@qua", qua);
            int flag = cmd.ExecuteNonQuery();
           
            con.Close();
            if (flag > 0)
            {
                sendmail(email, password, loginname);
                 con.Open();
                 SqlCommand cmd1 = new SqlCommand("INSERT INTO GardianMaster(GardianName, Contact, Address, Profession, UserID)VALUES(@GardianName,@Contact,@Address,@Profession,@UserID)", con);
                cmd1.Parameters.AddWithValue("@GardianName",gname);
                cmd1.Parameters.AddWithValue("@Contact", gcontact);
                cmd1.Parameters.AddWithValue("@Address", gaddress);
                cmd1.Parameters.AddWithValue("@Profession", gprofession);
                cmd1.Parameters.AddWithValue("@UserID",Convert.ToInt32(uid));
                cmd1.ExecuteNonQuery();
                return Json("Record insert Successfully.....................!", JsonRequestBehavior.AllowGet);
             //  con.Close();
            }
            else
            {
                return Json("User Already register.....................!", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region[Generate Unic iD]

        public string GenerateNumber()
        {
            Random ran = new Random();
            string r = "";
            int i;
            for (i = 0; i < 9; i++)
            {
                r += ran.Next(0, 9).ToString();
            }
            return r;
        }

        #endregion

        #region[Admin Home]

        public ActionResult AdminHome()
        {
            if (Session["uid"] != null)
            {
                return View();
            }
            else
            {
                return View("Index");
            }
        }

        #endregion

        #region[Get Branch]

        public JsonResult GetBranch()
        {
            List<Branch> de=new List<Branch>();
            SqlCommand cmd = new SqlCommand("select * from BranchMaster", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                de.Add(new Branch()
                {
                    BranchID = Convert.ToInt32(dr["BranchID"]),
                    BranchName = dr["BranchName"].ToString(),
                    Duration = dr["Duration"].ToString(),
                    Description = dr["Description"].ToString(),
                    Fees =Convert.ToDouble(dr["Fees"]),
                    Criteria = dr["Criteria"].ToString(),      
                }
               );
               
            }
            dr.Close();
            con.Close();
            return Json(de, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region[Get User By Acadamic Year and Branch]

        public JsonResult GetUserByAcadamicYearandBranch(string branch, string AcadamicYear)
        {
            List<UserDE> de = new List<UserDE>();
            SqlCommand cmd = new SqlCommand("select * from UserMaster where BranchID=@BranchID and AcadamicYear=@AcadamicYear", con);
            con.Open();
            cmd.Parameters.AddWithValue("@BranchID", branch);
            cmd.Parameters.AddWithValue("@AcadamicYear", AcadamicYear);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                de.Add(new UserDE()
                {
                    UserID = dr["UserID"].ToString(),
                    FirstName = dr["FirstName"].ToString(),
                    LastName = dr["LastName"].ToString(),
                    FullName = dr["FirstName"].ToString() + " " + dr["LastName"].ToString(),
                    AdmissionYear = dr["AdmissionYear"].ToString(),
                    CurrentAddress = dr["CurrentAddress"].ToString(),
                    PermanentAddress = dr["PermanentAddress"].ToString(),
                    ContactNo = dr["ContactNo"].ToString(),
                    EmailID = dr["EmailID"].ToString(),
                    Gender = dr["Gender"].ToString(),
                    LoginName = dr["LoginName"].ToString(),
                    Password = dr["Password"].ToString(),
                    Role = dr["Role"].ToString(),
                    AcadamicYear = dr["AcadamicYear"].ToString(),
                    BranchID = Convert.ToInt32(dr["BranchID"])

                }
               );

            }
            dr.Close();
            con.Close();
            return Json(de, JsonRequestBehavior.AllowGet);

        }



        #endregion
        #region[Get Examination]

        public JsonResult GetUserByAcadamicYearandBranch1(string branch, string AcadamicYear, string cast)
        {
            List<UserDE> de = new List<UserDE>();
            SqlCommand cmd = new SqlCommand("select * from UserMaster where BranchID=@BranchID and AcadamicYear=@AcadamicYear and Cast=@Cast", con);
            con.Open();
            cmd.Parameters.AddWithValue("@BranchID", Convert.ToInt32(branch));
            cmd.Parameters.AddWithValue("@AcadamicYear", AcadamicYear);
            cmd.Parameters.AddWithValue("@Cast", cast);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                de.Add(new UserDE()
                {
                    UserID = dr["UserID"].ToString(),
                    FirstName = dr["FirstName"].ToString(),
                    LastName = dr["LastName"].ToString(),
                    FullName = dr["FirstName"].ToString() + " " + dr["LastName"].ToString(),
                    AdmissionYear = dr["AdmissionYear"].ToString(),
                    CurrentAddress = dr["CurrentAddress"].ToString(),
                    PermanentAddress = dr["PermanentAddress"].ToString(),
                    ContactNo = dr["ContactNo"].ToString(),
                    EmailID = dr["EmailID"].ToString(),
                    Gender = dr["Gender"].ToString(),
                    LoginName = dr["LoginName"].ToString(),
                    Password = dr["Password"].ToString(),
                    Role = dr["Role"].ToString(),
                    AcadamicYear = dr["AcadamicYear"].ToString(),
                    BranchID = Convert.ToInt32(dr["BranchID"])

                }
               );

            }
            dr.Close();
            con.Close();
            return Json(de, JsonRequestBehavior.AllowGet);

        }

        #endregion


        #region[Get Examination]

        public JsonResult GetExaminationByBranchAndAcadamicYear(string branch, string acadamicyear)
        {
            List<ExaminationDE> de = new List<ExaminationDE>();
            SqlCommand cmd = new SqlCommand("select * from ExaminationMaster where BranchID=@BranchID and AcadamicYear =@AcadamicYear", con);
            cmd.Parameters.AddWithValue("@BranchID", Convert.ToInt32(branch));
            cmd.Parameters.AddWithValue("@AcadamicYear", acadamicyear);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                de.Add(new ExaminationDE()
                {
                    BranchID = Convert.ToInt32(dr["BranchID"]),
                    ExaminationID = Convert.ToInt32(dr["ExaminationID"]),
                    Examination = dr["Examination"].ToString(),
                    AcadamicYear = dr["AcadamicYear"].ToString()
                }
               );

            }
            dr.Close();
            con.Close();
            return Json(de, JsonRequestBehavior.AllowGet);

        }

        #endregion

     
     

        #region[Get Examination]

        public JsonResult GetExamination()
        {
            List<ExaminationDE> de = new List<ExaminationDE>();
            SqlCommand cmd = new SqlCommand("select * from ExaminationMaster", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                de.Add(new ExaminationDE()
                {
                    BranchID = Convert.ToInt32(dr["BranchID"]),
                    ExaminationID = Convert.ToInt32(dr["ExaminationID"]),
                    Examination = dr["Examination"].ToString(),
                    AcadamicYear = dr["AcadamicYear"].ToString()                
                }
               );

            }
            dr.Close();
            con.Close();
            return Json(de, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region[Notice]

        public ActionResult SetNotice()
        {
            if (Session["uid"] != null)
            {
                return View();
            }
            else
            {
                return View("Index");
            }
        }

        public JsonResult InsertNotice(string date, string notice, string role, string branch, string year)
        {
            con.Open();
            if (role == "Both")
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO NoticeDetail(BranchID, AcadamicYear, Date, Notice, Role)VALUES(@BranchID,@AcadamicYear,@Date,@Notice,@Role)", con);
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.Parameters.AddWithValue("@Notice", notice);
                cmd.Parameters.AddWithValue("@Role", "All");
                cmd.Parameters.AddWithValue("@BranchID", Convert.ToInt32(branch));
                cmd.Parameters.AddWithValue("@AcadamicYear", year);
                int flag = cmd.ExecuteNonQuery();
                if (flag > 0)
                {
                    // following code for send notice alert by mail
                    SqlCommand cmdmail = new SqlCommand("select * from dbo.UserMaster where BranchID=@BranchID and AcadamicYear=@AcadamicYear", con);
                    cmdmail.Parameters.AddWithValue("@BranchID", Convert.ToInt32(branch));
                    cmdmail.Parameters.AddWithValue("@AcadamicYear", year);
                   // cmdmail.Parameters.AddWithValue("@Role", role);
                    SqlDataReader dr1 = cmdmail.ExecuteReader();
                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            string email = dr1["EmailID"].ToString();
                            string message = "Hi" +
                        "<h4>Date: " + date + "</h4><br><h4>Your Notice Is: " + notice + "</h4>" +
                        "<br><h3> Thank You </h3>";
                            if (!string.IsNullOrEmpty(email))
                            {
                                MailMessage mm = new MailMessage("sender@gmail.com", email);
                                mm.Subject = "Notice Alert";
                                mm.Body = message;
                                mm.IsBodyHtml = true;
                                SmtpClient smtp = new SmtpClient();
                                smtp.Host = "smtp.gmail.com";
                                smtp.EnableSsl = true;
                                NetworkCredential NetworkCred = new NetworkCredential();
                                NetworkCred.UserName = "dmce.college@gmail.com";
                                NetworkCred.Password = "DMCECOLLEGE";
                                smtp.UseDefaultCredentials = true;
                                smtp.Credentials = NetworkCred;
                                smtp.Port = 587;
                                //smtp.Port=465;
                                smtp.Send(mm);
                            }

                        }

                    }
                    return Json("Notice set Successfully", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Notice set Failed", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO NoticeDetail(BranchID, AcadamicYear, Date, Notice, Role)VALUES(@BranchID,@AcadamicYear,@Date,@Notice,@Role)", con);
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.Parameters.AddWithValue("@Notice", notice);
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.Parameters.AddWithValue("@BranchID", Convert.ToInt32(branch));
                cmd.Parameters.AddWithValue("@AcadamicYear", year);
                int flag = cmd.ExecuteNonQuery();
                if (flag > 0)
                {
                    // following code for send notice alert by mail
                    SqlCommand cmdmail = new SqlCommand("select * from dbo.UserMaster where BranchID=@BranchID and AcadamicYear=@AcadamicYear and Role=@Role", con);
                    cmdmail.Parameters.AddWithValue("@BranchID", Convert.ToInt32(branch));
                    cmdmail.Parameters.AddWithValue("@AcadamicYear", year);
                    cmdmail.Parameters.AddWithValue("@Role", role);
                    SqlDataReader dr1 = cmdmail.ExecuteReader();
                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            string email = dr1["EmailID"].ToString();
                            string message = "Hi" +
                        "<h4>Date: " + date + "</h4><br><h4>Your Notice Is: " + notice + "</h4>" +
                        "<br><h3> Thank You </h3>";
                            if (!string.IsNullOrEmpty(email))
                            {
                                MailMessage mm = new MailMessage("sender@gmail.com", email);
                                mm.Subject = "Notice Alert";
                                mm.Body = message;
                                mm.IsBodyHtml = true;
                                SmtpClient smtp = new SmtpClient();
                                smtp.Host = "smtp.gmail.com";
                                smtp.EnableSsl = true;
                                NetworkCredential NetworkCred = new NetworkCredential();
                                NetworkCred.UserName = "dmce.college@gmail.com";
                                NetworkCred.Password = "DMCECOLLEGE";
                                smtp.UseDefaultCredentials = true;
                                smtp.Credentials = NetworkCred;
                                smtp.Port = 587;
                                //smtp.Port=465;
                                smtp.Send(mm);
                            }

                        }

                    }
                    return Json("Notice set Successfully", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Notice set Faled", JsonRequestBehavior.AllowGet);
                }
            }
            //con.Close();
        }

        #endregion

        #region[Payroll]

        public ActionResult SetPayroll()
        {
            if (Session["uid"] != null)
            {
                return View();
            }
            else
            {
                return View("Index");
            }
        }

        #endregion

        #region[Get Examination Subject by Acadamic Year BranchID ExaminationID]

        public JsonResult GetExaminationSubjectbyAcadamicYearBranchIDExaminationID(string branch, string acadamicyear, string examinationid)
        {
            List<timetable> de = new List<timetable>();
            SqlCommand cmd = new SqlCommand("SELECT ExaminationDetails.ExaminationDetailID, ExaminationMaster.Examination, ExaminationDetails.Subject, ExaminationDetails.Time, ExaminationDetails.Date, ExaminationDetails.Marks,ExaminationMaster.AcadamicYear, ExaminationMaster.BranchID, BranchMaster.BranchName,ExaminationDetails.Marks FROM ExaminationDetails INNER JOIN ExaminationMaster ON ExaminationDetails.ExaminationID = ExaminationMaster.ExaminationID INNER JOIN BranchMaster ON ExaminationMaster.BranchID = BranchMaster.BranchID where  ExaminationDetails.ExaminationID=@ExaminationID and  BranchMaster.BranchID=@BranchID and ExaminationMaster.AcadamicYear=@AcadamicYear", con);
            con.Open();
            cmd.Parameters.AddWithValue("@BranchID", branch);
            cmd.Parameters.AddWithValue("@AcadamicYear", acadamicyear);
            cmd.Parameters.AddWithValue("@ExaminationID", examinationid);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                de.Add(new timetable()
                {
                    // ExaminationID=Convert.ToInt32(dr["ExaminationID"]),
                    AcadamicYear = dr["AcadamicYear"].ToString(),
                    BranchID = Convert.ToInt32(dr["BranchID"]),
                    Examination = dr["Examination"].ToString(),
                    ExaminationDetailID = Convert.ToInt32(dr["ExaminationDetailID"]),
                    Subject = dr["Subject"].ToString(),
                    Time = dr["Time"].ToString(),
                    Date = dr["Date"].ToString(),
                    Marks = dr["Marks"].ToString()
                }
               );

            }
            dr.Close();
            con.Close();
            return Json(de, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region[Set Result]

        public ActionResult SetResult()
        {
            if (Session["uid"] != null)
            {
                return View();
            }
            else
            {
                return View("Index");
            }
        }

        public JsonResult AddResult(string BranchID, string UserID, string ExaminationID, string Subject, string Marks)
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("select COUNT(*) from Result where UserID=@UserID  and Subject=@Subject", con);
            cmd1.Parameters.AddWithValue("@UserID", UserID);
            cmd1.Parameters.AddWithValue("@Subject", Subject);
            int count = Convert.ToInt32(cmd1.ExecuteScalar());
            con.Close();
            if (count == 0)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Result(BranchID, UserID, ExaminationID, Subject, Marks)VALUES(@BranchID,@UserID,@ExaminationID,@Subject,@Marks)", con);
                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@ExaminationID", ExaminationID);
                cmd.Parameters.AddWithValue("@Subject", Subject);
                cmd.Parameters.AddWithValue("@Marks", Marks);
                int flag = cmd.ExecuteNonQuery();
                con.Close();
                if (flag > 0)
                {
                    return Json("Result Set Successfully.................!", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Result Not Set.................!", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("Result Already set .................!", JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult SendResultAlertMailToStudent(string userid)
        {
            con.Open();
            // following code for send notice alert by mail
            SqlCommand cmdmail = new SqlCommand("select * from dbo.UserMaster where UserID=@UserID", con);
            cmdmail.Parameters.AddWithValue("@UserID", userid);
            SqlDataReader dr1 = cmdmail.ExecuteReader();
            if (dr1.HasRows)
            {
                while (dr1.Read())
                {
                    string email = dr1["EmailID"].ToString();
                    string message = "Hi" +
                "<br><h4>Your Result is declared please check on College website by your login:</h4>" +
                "<br><h3> Thank You </h3>";
                    if (!string.IsNullOrEmpty(email))
                    {
                        MailMessage mm = new MailMessage("sender@gmail.com", email);
                        mm.Subject = "Notice Alert";
                        mm.Body = message;
                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential();
                        NetworkCred.UserName = "dmce.college@gmail.com";
                        NetworkCred.Password = "DMCECOLLEGE";
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        //smtp.Port=465;
                        smtp.Send(mm);
                    }

                }

            }
            return Json(1, JsonRequestBehavior.AllowGet);
          //  con.Close();
        }

        #endregion

        #region[Set TimeTable]

        public ActionResult SetTimetable()
        {
            if (Session["uid"] != null)
            {
                return View();
            }
            else
            {
                return View("Index");
            }
        }

        #endregion

        //Employee
        #region[Attendance]

        public ActionResult Attendanceemp()
        {
            return View();
        }

        public JsonResult GetUserForAttendance(string branch)
        {
            List<UserforAttendanceDE> de = new List<UserforAttendanceDE>();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT UserMaster.UserID, UserMaster.FirstName, UserMaster.LastName, BranchMaster.BranchName, UserMaster.AcadamicYear FROM UserMaster INNER JOIN BranchMaster ON UserMaster.BranchID = BranchMaster.BranchID where UserMaster.Role='Employee' and UserMaster.BranchID=@BranchID", con);

            cmd.Parameters.AddWithValue("@BranchID",Convert.ToInt32(branch));
           // cmd.Parameters.AddWithValue("@AcadamicYear",year);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    de.Add(new UserforAttendanceDE()
                    {
                        UserID = dr["UserID"].ToString(),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        FullName = dr["FirstName"].ToString() + " " + dr["LastName"].ToString(),

                        Branch = dr["BranchName"].ToString(),
                        AcadamicYear = dr["AcadamicYear"].ToString(),

                    }
                   );
                }
                dr.Close();
                con.Close();
                return Json(de, JsonRequestBehavior.AllowGet);
            }
            else
            {
                dr.Close();
                con.Close();
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult insertAttendance(string userid)
        {
            string date = System.DateTime.Now.ToShortDateString();
            string month = System.DateTime.Now.Month.ToString();
            con.Open();
            SqlCommand cmd1 = new SqlCommand("select COUNT(*) from dbo.AttendanceMaster where UserID=@UserID and Date=@Date", con);
            cmd1.Parameters.AddWithValue("@UserID", userid);
            cmd1.Parameters.AddWithValue("@Date", date);
            int count = Convert.ToInt32(cmd1.ExecuteScalar());
            if (count == 0)
            {
                SqlCommand cmd3 = new SqlCommand("select COUNT(*)from AttendanceMaster where UserID=@UserID and Month1=@Month1", con);
                cmd3.Parameters.AddWithValue("@UserID", userid);
                cmd3.Parameters.AddWithValue("@Month1", month);
                //  cmd3.Parameters.AddWithValue("@Date", date);
                int count1 = Convert.ToInt32(cmd3.ExecuteScalar());
                if (count1 == 0)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO AttendanceMaster(Date, Month1, UserID, Role, Attendance)VALUES(@Date,@Month1,@UserID,@Role,@Attendance)", con);
                    cmd.Parameters.AddWithValue("@Date", date);
                    cmd.Parameters.AddWithValue("@Month1", month);
                    cmd.Parameters.AddWithValue("@UserID", userid);
                    cmd.Parameters.AddWithValue("@Role", "Employee");
                    cmd.Parameters.AddWithValue("@Attendance", "1");
                    int flag = cmd.ExecuteNonQuery();
                   // con.Close();
                    if (flag > 0)
                    {
                        return Json("attendance successfully", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("attendance Fail", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    int attendance = 0;
                    int updated = 0;
                    SqlCommand cmd4 = new SqlCommand("select Attendance from AttendanceMaster where UserID=@UserID and Month1=@Month1", con);
                    cmd4.Parameters.AddWithValue("@Month1", month);
                    cmd4.Parameters.AddWithValue("@UserID", userid);
                    SqlDataReader dratt = cmd4.ExecuteReader();
                    if (dratt.HasRows)
                    {
                        dratt.Read();
                        attendance = Convert.ToInt32(dratt["Attendance"]);
                        updated = attendance + 1;
                        SqlCommand cmd2 = new SqlCommand("UPDATE  AttendanceMaster SET Date=@Date, Month1 =@Month1, Attendance =@Attendance where ( UserID=@UserID)", con);
                        cmd2.Parameters.AddWithValue("@Month1", month);
                        cmd2.Parameters.AddWithValue("@Attendance", updated);
                        cmd2.Parameters.AddWithValue("@Date", date);
                        cmd2.Parameters.AddWithValue("@UserID", userid);
                        cmd2.ExecuteNonQuery();
                        dratt.Close();
                    }
                    return Json("attendance successfully", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("Attendance already set of this Employee", JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        // Yuvaraj
        #region [Payment ]

        public JsonResult Savepayment(string UserID, string AcadamicYear, string BranchID, string Amount, string DearnessAllowance, string MedicalAllowance, string HouseRentAllowance, string Overtime, string ConveyanceAllowance, string EmployeeStateInsurance, string ProvidentFund, string ProfessionalTax, string NetAmount, string TotalDeduction, string TotalEarn)
        {
            int id = 0;
            AcadamicYear = "0";
            SqlCommand cmd = new SqlCommand("select * from UserMaster where UserID=@UserID", con);
            con.Open();
            cmd.Parameters.AddWithValue("@UserID", UserID);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                SqlCommand cmd2 = new SqlCommand("select COUNT(*) from PaymentMaster where UserID=@UserID and Month=@Month", con);
                cmd2.Parameters.AddWithValue("@UserID", dr["UserID"].ToString());
                cmd2.Parameters.AddWithValue("@Month", (System.DateTime.Now.Month.ToString("d2") + "/" + System.DateTime.Now.Year.ToString()));
                int count = Convert.ToInt32(cmd2.ExecuteScalar());
                if (count > 0)
                {

                }
                else
                {

                    //Amount,DA,HRA,GrossSalary,PF,NetAmount
                    SqlCommand cmd1 = new SqlCommand("INSERT INTO PaymentMaster(UserID, AcadamicYear, BranchID, Month, Amount, DearnessAllowance, MedicalAllowance, HouseRentAllowance, Overtime, ConveyanceAllowance, EmployeeStateInsurance, ProvidentFund, ProfessionalTax, NetAmount, TotalDeduction,TotalEarn)VALUES(@UserID,@AcadamicYear,@BranchID,@Month,@Amount,@DearnessAllowance,@MedicalAllowance,@HouseRentAllowance,@Overtime,@ConveyanceAllowance,@EmployeeStateInsurance,@ProvidentFund,@ProfessionalTax,@NetAmount,@TotalDeduction,@TotalEarn)", con);
                    cmd1.Parameters.AddWithValue("@UserID", dr["UserID"].ToString());
                    cmd1.Parameters.AddWithValue("@AcadamicYear", dr["AcadamicYear"].ToString());
                    cmd1.Parameters.AddWithValue("@BranchID", dr["BranchID"].ToString());
                    cmd1.Parameters.AddWithValue("@Month", (System.DateTime.Now.Month.ToString("d2") + "/" + System.DateTime.Now.Year.ToString()));
                    cmd1.Parameters.AddWithValue("@Amount", Amount);
                    cmd1.Parameters.AddWithValue("@DearnessAllowance", DearnessAllowance);
                    cmd1.Parameters.AddWithValue("@MedicalAllowance", MedicalAllowance);
                    cmd1.Parameters.AddWithValue("@HouseRentAllowance", HouseRentAllowance);
                    cmd1.Parameters.AddWithValue("@Overtime", Overtime);
                    cmd1.Parameters.AddWithValue("@ConveyanceAllowance", ConveyanceAllowance);
                    cmd1.Parameters.AddWithValue("@EmployeeStateInsurance", EmployeeStateInsurance);
                    cmd1.Parameters.AddWithValue("@ProvidentFund", ProvidentFund);
                    cmd1.Parameters.AddWithValue("@ProfessionalTax", ProfessionalTax);
                    cmd1.Parameters.AddWithValue("@TotalDeduction", TotalDeduction);
                    cmd1.Parameters.AddWithValue("@TotalEarn", TotalEarn);
                    cmd1.Parameters.AddWithValue("@NetAmount", NetAmount);
                    id = cmd1.ExecuteNonQuery();
                    if (id > 0)
                    {
                        // following code for send notice alert by mail
                        SqlCommand cmdmail = new SqlCommand("select * from dbo.UserMaster where BranchID=@BranchID and AcadamicYear=@AcadamicYear and Role=@Role", con);
                        cmdmail.Parameters.AddWithValue("@BranchID", Convert.ToInt32(BranchID));
                        cmdmail.Parameters.AddWithValue("@AcadamicYear", AcadamicYear);
                        cmdmail.Parameters.AddWithValue("@Role", "Employee");
                        SqlDataReader dr1 = cmdmail.ExecuteReader();
                        if (dr1.HasRows)
                        {
                            while (dr1.Read())
                            {
                                string email = dr1["EmailID"].ToString();
                                string message = "Hi" +
                            "<h4>Your Payment is Assign For This Month confirm on website:</h4>" +
                            "<br><h3> Thank You </h3>";
                                if (!string.IsNullOrEmpty(email))
                                {
                                    MailMessage mm = new MailMessage("sender@gmail.com", email);
                                    mm.Subject = "Notice Alert";
                                    mm.Body = message;
                                    mm.IsBodyHtml = true;
                                    SmtpClient smtp = new SmtpClient();
                                    smtp.Host = "smtp.gmail.com";
                                    smtp.EnableSsl = true;
                                    NetworkCredential NetworkCred = new NetworkCredential();
                                    NetworkCred.UserName = "pvpp@gmail.com";
                                    NetworkCred.Password = "Pvppcoe";
                                    smtp.UseDefaultCredentials = true;
                                    smtp.Credentials = NetworkCred;
                                    smtp.Port = 587;
                                    //smtp.Port=465;
                                    smtp.Send(mm);
                                }

                            }

                        }
                    }
                }

            }



            con.Close();
            return Json(id, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region[Get Experiance of employee by id]

        public JsonResult GetExperianceByID(string userid)
        {
            string experance = "";

            SqlCommand cmd = new SqlCommand("select Experiense from dbo.UserMaster where UserID=@UserID", con);
            con.Open();
            cmd.Parameters.AddWithValue("@UserID", userid);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                 experance = dr["Experiense"].ToString();
                return Json(experance, JsonRequestBehavior.AllowGet);
            }
            else
            {
                dr.Read();
                return Json(null, JsonRequestBehavior.AllowGet);
 
            }
        //    dr.Close();
         //   con.Close();
        }

        #endregion

        #region[Get Experiance of employee by id]

        public JsonResult GetAttendanceByID(string userid)
        {
            string Attendance = "";

            SqlCommand cmd = new SqlCommand("select Attendance from AttendanceMaster where UserID=@UserID", con);
            con.Open();
            cmd.Parameters.AddWithValue("@UserID", userid);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                Attendance = dr["Attendance"].ToString();
                return Json(Attendance, JsonRequestBehavior.AllowGet);
            }
            else
            {
                dr.Read();
                return Json(null, JsonRequestBehavior.AllowGet);

            }
         //   dr.Close();
           // con.Close();
        }

        #endregion

        #region[get employee br branch and acadamic year]

        public JsonResult GetEmp(string branch)
        {
            List<UserDE> de = new List<UserDE>();
          
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from dbo.UserMaster where BranchID=@BranchID and Role=@Role", con);
            cmd.Parameters.AddWithValue("@BranchID", Convert.ToInt32(branch));
            cmd.Parameters.AddWithValue("@Role","Employee");
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    de.Add(new UserDE()
                    {
                        UserID = dr["UserID"].ToString(),                     
                        FullName = dr["FirstName"].ToString() + " " + dr["LastName"].ToString(),                    
                    }
                   );

                }
                return Json(de, JsonRequestBehavior.AllowGet);
              // dr.Close();
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
             //   dr.Close();
            }

        }

        #endregion

        #region[getsalarybyexperience]

        public JsonResult GetAmount(string branch, string acadamicyear)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from PaymentPerDay where BranchID=@BranchID", con);
            cmd.Parameters.AddWithValue("@BranchID",Convert.ToInt32(branch));
            //cmd.Parameters.AddWithValue("@AcadamicYear", acadamicyear);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                int amt = Convert.ToInt32(dr["AmountPerDay"]);
                

                return Json(amt, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
          //  dr.Close();
          //  con.Close();
        }

        #endregion

        #region [Time table ]

        public JsonResult InsertTimetabbleMaster(string examinection, string BranchID, string AcadmicID)
        {
            string id;
            id = GenerateNumber();
            con.Open();

            SqlCommand cmd = new SqlCommand(" INSERT INTO ExaminationMaster(ExaminationID, Examination, AcadamicYear, BranchID)VALUES(@ExaminationID,@Examination,@AcadamicYear,@BranchID)", con);
            cmd.Parameters.AddWithValue("@ExaminationID", id);
            cmd.Parameters.AddWithValue("@Examination", examinection);
            cmd.Parameters.AddWithValue("@AcadamicYear", AcadmicID);
            cmd.Parameters.AddWithValue("@BranchID", BranchID);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {


            }
            return Json(id, JsonRequestBehavior.AllowGet);
        }


        public JsonResult TimeTableInsert(string subject, string edate, string time, string mark, string id)
        {
            int i;

            con.Open();

            SqlCommand cmd1 = new SqlCommand("INSERT INTO ExaminationDetails(ExaminationID, Subject, Time, Date, Marks)VALUES(@ExaminationID,@Subject,@Time,@Date,@Marks)", con);
            cmd1.Parameters.AddWithValue("@ExaminationID", id);
            cmd1.Parameters.AddWithValue("@Subject", subject);
            cmd1.Parameters.AddWithValue("@Time", time);
            cmd1.Parameters.AddWithValue("@Date", edate);
            cmd1.Parameters.AddWithValue("@Marks", mark);
            i = cmd1.ExecuteNonQuery();
            if (i > 0)
            {
                i = 1;
            }

            return Json(i, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region [ Studunt Master]

        public ActionResult StudentFees()
        {
            if (Session["uid"] != null)
            {
                return View();
            }
            else
            {
                return View("Index");
            }
        }

        public JsonResult GetCourseAmount(string branchID, string acadmicYear)
        {
            FeesMastes fe = new FeesMastes();

            List<FeesMastes> de = new List<FeesMastes>();
            SqlCommand cmd = new SqlCommand("SELECT FeeMaster.Amount, FeeMaster.FeeMasterID FROM BranchMaster INNER JOIN FeeMaster ON BranchMaster.BranchID = FeeMaster.BranchID WHERE     (BranchMaster.BranchID = @BranchID)", con);
            con.Open();
            cmd.Parameters.AddWithValue("@BranchID", branchID);
            cmd.Parameters.AddWithValue("@AcadamicYear", acadmicYear);

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                de.Add(new FeesMastes()
                {
                    Amount = Convert.ToDouble(dr["Amount"]),
                    FeeMasterID = Convert.ToInt32(dr["FeeMasterID"]),
                    // RemainingAmount = Convert.ToDouble(dr["RemainingAmount"]),

                }
               );

            }


            return Json(de, JsonRequestBehavior.AllowGet);

        }


        public JsonResult InsertStudentFees(string UserID, string FeeMasterID, string PaidAmount)
        {

            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO FeeTransaction(UserID, FeeMasterID, PaidAmount)VALUES(@UserID,@FeeMasterID,@PaidAmount)", con);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@FeeMasterID", FeeMasterID);
            cmd.Parameters.AddWithValue("@PaidAmount", PaidAmount);

            int i = cmd.ExecuteNonQuery();
            con.Close();
            return Json(i, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region [ Notic Bord]

        public ActionResult Notice()
        {
            if (Session["uid"] != null)
            {
                return View();
            }
            else
            {
                return View("Index");
            }
        }

        public JsonResult Getnotic(string Role)
        {
            Role = "Employee";
            List<NoticeDE> de = new List<NoticeDE>();
            SqlCommand cmd = new SqlCommand("select distinct b.BranchName,n.NoticeID,n.AcadamicYear,n.Date,n.Notice,n.Role from BranchMaster b, NoticeDetail n  where b.BranchID=n.BranchID and (n.Role='All' or n.Role=@Role) ", con);
            con.Open();
            cmd.Parameters.AddWithValue("@Role", Role);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                de.Add(new NoticeDE()
                {
                    Notice = dr["Notice"].ToString(),
                    BranchName = dr["BranchName"].ToString(),
                    Date = dr["Date"].ToString(),
                    AcadamicYear = dr["AcadamicYear"].ToString()
                }
               );

            }
            dr.Close();
            con.Close();
            return Json(de, JsonRequestBehavior.AllowGet);

        }



        #endregion

        #region[GetBasicSalary]

        public JsonResult GetBasicSalary(string exp, string att, string amtperday)
        {
                decimal Totalbasic=0;
            decimal experiance = Convert.ToDecimal(exp);
            decimal attenadance = Convert.ToDecimal(att);
            decimal perdayamt = Convert.ToDecimal(amtperday);
            decimal tot = perdayamt * attenadance;
            if(experiance==1)
            {
                Totalbasic=(tot*5)/100;
            }
            else if(experiance==2)
            {
                Totalbasic=(tot*10)/100;
            }
            else if(experiance==3)
            {
                Totalbasic=(tot*12)/100;
            }
            else if(experiance==4)
            {
                Totalbasic=(tot*15)/100;
            }
            else if(experiance==5)
            {
                Totalbasic=(tot*18)/100;
            }
            else
            {
                Totalbasic = (tot * 20) / 100;
            }
            decimal basic = tot + Totalbasic;
            return Json(basic, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult Logout()
        {
            Session.Abandon();
            return View("Index");
        }


        #region[Set Class Time Table]

        public ActionResult ClassTimeTable()
        {
            return View();
        }

        public JsonResult InsertClassTimetabbleMaster(string BranchID, string AcadmicID)
        {
            string id;
            id = GenerateNumber();
            con.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO TimeTableMaster (TimetableID,BranchID, AcadamicYear) VALUES (@TimetableID,@BranchID,@AcadamicYear)", con);
            cmd.Parameters.AddWithValue("@TimetableID", id);
            cmd.Parameters.AddWithValue("@AcadamicYear", AcadmicID);
            cmd.Parameters.AddWithValue("@BranchID", BranchID);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {


            }
            return Json(id, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ClassTimeTableInsert(string day, string subject,string time, string id)
        {
            int i;

            con.Open();

            SqlCommand cmd1 = new SqlCommand("INSERT INTO TimeTableTransaction(TimeTableID, Day, Subject, Time)VALUES(@TimeTableID,@Day,@Subject,@Time)", con);
            cmd1.Parameters.AddWithValue("@TimeTableID", id);
            cmd1.Parameters.AddWithValue("@Subject", subject);
            cmd1.Parameters.AddWithValue("@Time", time);
            cmd1.Parameters.AddWithValue("@Day", day); 
            i = cmd1.ExecuteNonQuery();
            if (i > 0)
            {
                i = 1;
            }

            return Json(i, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public ActionResult fileupdmethod(HttpPostedFileBase fileupd)
        {
            String path = "";
            String imagepath;
         
                String Imageurl =
              path = System.IO.Path.Combine(Server.MapPath("~/images"), System.IO.Path.GetFileName(fileupd.FileName));
                fileupd.SaveAs(path);
                string fl = path.Substring(path.LastIndexOf("\\"));
                string[] split = fl.Split('\\');
                string newpath = split[1];
                imagepath = "/Images/" + newpath;
                return Json(imagepath, JsonRequestBehavior.AllowGet);
            
        
        }

        # region [update attendance ]

        public JsonResult UpdateAttendance(string userid)
        {
            string date = System.DateTime.Now.ToShortDateString();
            string month = System.DateTime.Now.Month.ToString();
            con.Open();
            SqlCommand cmd1 = new SqlCommand("select COUNT(*) from dbo.AttendanceMaster where UserID=@UserID and Date=@Date", con);
            cmd1.Parameters.AddWithValue("@UserID", userid);
            cmd1.Parameters.AddWithValue("@Date", date);
            int count = Convert.ToInt32(cmd1.ExecuteScalar());
            if (count > 0)
            {
                SqlCommand cmd3 = new SqlCommand("select COUNT(*)from AttendanceMaster where UserID=@UserID and Month1=@Month1", con);
                cmd3.Parameters.AddWithValue("@UserID", userid);
                cmd3.Parameters.AddWithValue("@Month1", month);
                //  cmd3.Parameters.AddWithValue("@Date", date);
                int count1 = Convert.ToInt32(cmd3.ExecuteScalar());
                if (count1 == 0)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO AttendanceMaster(Date, Month1, UserID, Role, Attendance)VALUES(@Date,@Month1,@UserID,@Role,@Attendance)", con);
                    cmd.Parameters.AddWithValue("@Date", date);
                    cmd.Parameters.AddWithValue("@Month1", month);
                    cmd.Parameters.AddWithValue("@UserID", userid);
                    cmd.Parameters.AddWithValue("@Role", "Employee");
                    cmd.Parameters.AddWithValue("@Attendance", "1");
                    int flag = cmd.ExecuteNonQuery();
                    con.Close();
                    if (flag > 0)
                    {
                        return Json("attendance successfully", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("attendance Fail", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    int attendance = 0;
                    int updated = 0;
                    SqlCommand cmd4 = new SqlCommand("select Attendance from AttendanceMaster where UserID=@UserID and Month1=@Month1", con);
                    cmd4.Parameters.AddWithValue("@Month1", month);
                    cmd4.Parameters.AddWithValue("@UserID", userid);
                    SqlDataReader dratt = cmd4.ExecuteReader();
                    if (dratt.HasRows)
                    {
                        dratt.Read();
                        attendance = Convert.ToInt32(dratt["Attendance"]);
                        if (attendance > 0)
                        {
                            updated = attendance - 1;
                        }
                        SqlCommand cmd2 = new SqlCommand("UPDATE  AttendanceMaster SET Date=@Date, Month1 =@Month1, Attendance =@Attendance where ( UserID=@UserID)", con);
                        cmd2.Parameters.AddWithValue("@Month1", month);
                        cmd2.Parameters.AddWithValue("@Attendance", updated);
                        cmd2.Parameters.AddWithValue("@Date", date);
                        cmd2.Parameters.AddWithValue("@UserID", userid);
                        cmd2.ExecuteNonQuery();
                        dratt.Close();
                    }
                    return Json("attendance successfully", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("Attendance already set of this Employee", JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}
   