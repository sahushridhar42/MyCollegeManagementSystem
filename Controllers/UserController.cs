using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CollegeManagementSystemDE;

namespace CollegeManagementSystem.Controllers
{
    //
    public class UserController : Controller
    {

        #region[Connection]

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

        #endregion

        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        #region[User Home]

        public ActionResult UserHome()
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

        #region[GetAll User]

        public JsonResult GetAllUser(string userid)
        {
            List<UserDE> de = new List<UserDE>();
            SqlCommand cmd = new SqlCommand("SELECT UserMaster.UserID, GardianMaster.GardianName, GardianMaster.Contact, GardianMaster.Address, GardianMaster.Profession, UserMaster.FirstName, UserMaster.LastName,  UserMaster.AdmissionYear, UserMaster.CurrentAddress, UserMaster.PermanentAddress, UserMaster.ContactNo, UserMaster.EmailID, UserMaster.Gender, UserMaster.LoginName,  UserMaster.Password, UserMaster.Role, UserMaster.AcadamicYear, BranchMaster.BranchName, UserMaster.BranchID FROM BranchMaster INNER JOIN UserMaster ON BranchMaster.BranchID = UserMaster.BranchID INNER JOIN GardianMaster ON UserMaster.UserID = GardianMaster.UserID WHERE(UserMaster.UserID = @UserID)", con);
            cmd.Parameters.AddWithValue("@UserID", userid);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                de.Add(new UserDE()
                {
                    UserID = dr["UserID"].ToString(),
                    FirstName = dr["FirstName"].ToString(),
                    LastName = dr["LastName"].ToString(),
                    FullName = dr["FirstName"].ToString() + " " + dr["LastName"].ToString(),
                    PermanentAddress = dr["PermanentAddress"].ToString(),
                    ContactNo = dr["ContactNo"].ToString(),
                    EmailID = dr["EmailID"].ToString(),
                    Gender = dr["Gender"].ToString(),
                    LoginName = dr["LoginName"].ToString(),
                    Password = dr["Password"].ToString(),
                    AcadamicYear = dr["AcadamicYear"].ToString(),
                    Branch = dr["BranchName"].ToString(),
                    BranchID=Convert.ToInt32(dr["BranchID"]),
                    GardianName = dr["GardianName"].ToString(),
                    Contact = dr["Contact"].ToString(),
                    Address = dr["Address"].ToString(),
                    Profession = dr["Profession"].ToString()
                }
               );

            }
            dr.Close();
            con.Close();
            return Json(de, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region[GetResult]

        public ActionResult GetResult()
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

        public JsonResult GetMarksTotal(string userid)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select SUM( CONVERT(numeric(18,2), Marks)) from Result where UserID=@UserID", con);
            cmd.Parameters.AddWithValue("@UserID",userid);
            int total =Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return Json(total, JsonRequestBehavior.AllowGet);
            

        }

        public JsonResult GetResultResult(string userid)
        {
            con.Open();
            List<ResultDE> de = new List<ResultDE>();
            SqlCommand cmd = new SqlCommand("SELECT     Result.ResultID, BranchMaster.BranchName, UserMaster.FirstName, UserMaster.LastName, ExaminationMaster.Examination, Result.Subject, Result.Marks FROM Result INNER JOIN BranchMaster ON Result.BranchID = BranchMaster.BranchID INNER JOIN UserMaster ON Result.UserID = UserMaster.UserID INNER JOIN ExaminationMaster ON Result.ExaminationID = ExaminationMaster.ExaminationID where UserMaster.UserID=@UserID", con);
            cmd.Parameters.AddWithValue("@UserID", userid);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    de.Add(new ResultDE()
                    {
                       // UserID = dr["UserID"].ToString(),
                      //  BranchID = Convert.ToInt32(dr["BranchID"]),
                        Examination = dr["Examination"].ToString(),
                        Subject = dr["Subject"].ToString(),
                        Marks = dr["Marks"].ToString(),
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
                return Json("null", JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region[Student Exam Time Table]

        public ActionResult StudentTimeTable()
        {
            return View();
        }

        public JsonResult GetTimeTable1s(string AcadamicYear ,string bach)
        {
            con.Open();
            List<timetable> de = new List<timetable>();
            SqlCommand cmd = new SqlCommand("SELECT ExaminationMaster.Examination, ExaminationDetails.Subject, ExaminationDetails.Time, ExaminationDetails.Date, BranchMaster.BranchName FROM ExaminationDetails INNER JOIN ExaminationMaster ON ExaminationDetails.ExaminationID = ExaminationMaster.ExaminationID INNER JOIN BranchMaster ON ExaminationMaster.BranchID = BranchMaster.BranchID where ExaminationMaster.AcadamicYear=@AcadamicYear  and ExaminationMaster.BranchID=@BranchID", con);
              cmd.Parameters.AddWithValue("@AcadamicYear",AcadamicYear);
            cmd.Parameters.AddWithValue("@BranchID", Convert.ToInt32(bach));
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    de.Add(new timetable()
                    {
                        // AcadamicYear = dr["AcadamicYear"].ToString(),
                        BachName = dr["BranchName"].ToString(),
                        // BranchID = Convert.ToInt32(dr["BranchID"]),
                        Examination = dr["Examination"].ToString(),
                        Date = dr["Date"].ToString(),
                        Subject = dr["Subject"].ToString(),
                        Time = dr["Time"].ToString(),
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
                return Json("null", JsonRequestBehavior.AllowGet);
            }
        }


        #endregion

        #region [Get Time Table]

        public ActionResult GetTimeTable()
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




        public JsonResult GetTimeTable1(string bach)
        {
            con.Open();
            List<timetable> de = new List<timetable>();
            SqlCommand cmd = new SqlCommand("SELECT ExaminationMaster.Examination, ExaminationDetails.Subject, ExaminationDetails.Time, ExaminationDetails.Date, BranchMaster.BranchName FROM ExaminationDetails INNER JOIN ExaminationMaster ON ExaminationDetails.ExaminationID = ExaminationMaster.ExaminationID INNER JOIN BranchMaster ON ExaminationMaster.BranchID = BranchMaster.BranchID where ExaminationMaster.BranchID=@BranchID", con);
          //  cmd.Parameters.AddWithValue("@AcadamicYear",acadamicyear);
            cmd.Parameters.AddWithValue("@BranchID",Convert.ToInt32(bach));
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    de.Add(new timetable()
                    {
                        // AcadamicYear = dr["AcadamicYear"].ToString(),
                        BachName = dr["BranchName"].ToString(),
                        // BranchID = Convert.ToInt32(dr["BranchID"]),
                        Examination = dr["Examination"].ToString(),
                        Date = dr["Date"].ToString(),
                        Subject = dr["Subject"].ToString(),
                        Time = dr["Time"].ToString(),
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
                return Json("null", JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region[Get Salary Details]

        public ActionResult GetSalary()
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

        public JsonResult GetSalaryDetails( string userid)
        {
            con.Open();
            Payroll1 de = new Payroll1();
            string month = (System.DateTime.Now.Month.ToString("d2") + "/" + System.DateTime.Now.Year.ToString());
            SqlCommand cmd = new SqlCommand("SELECT PaymentMaster.PaymentID, UserMaster.FirstName, UserMaster.LastName, PaymentMaster.AcadamicYear, BranchMaster.BranchName, PaymentMaster.Month, PaymentMaster.Amount, PaymentMaster.DearnessAllowance, PaymentMaster.MedicalAllowance, PaymentMaster.HouseRentAllowance, PaymentMaster.Overtime, PaymentMaster.ConveyanceAllowance,PaymentMaster.EmployeeStateInsurance, PaymentMaster.ProvidentFund, PaymentMaster.ProfessionalTax, PaymentMaster.NetAmount, PaymentMaster.TotalDeduction, PaymentMaster.TotalEarn FROM PaymentMaster INNER JOIN BranchMaster ON PaymentMaster.BranchID = BranchMaster.BranchID INNER JOIN UserMaster ON PaymentMaster.UserID = UserMaster.UserID where PaymentMaster.UserID=@UserID and PaymentMaster.Month=@Month", con);
            cmd.Parameters.AddWithValue("@UserID", userid);
            cmd.Parameters.AddWithValue("@Month", month);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                de.PaymentID = Convert.ToInt32(dr["PaymentID"]);
                de.FullName = dr["FirstName"].ToString() + " " + dr["LastName"].ToString();
                de.AcadamicYear = dr["AcadamicYear"].ToString();
                de.Branch = dr["BranchName"].ToString();
                de.Month = dr["Month"].ToString();
                de.Amount =Convert.ToDecimal( dr["Amount"].ToString());
                de.DearnessAllowance = Convert.ToDecimal(dr["DearnessAllowance"].ToString());
                de.MedicalAllowance = Convert.ToDecimal(dr["MedicalAllowance"].ToString());
                de.HouseRentAllowance = Convert.ToDecimal(dr["HouseRentAllowance"].ToString());
                de.Overtime = Convert.ToDecimal(dr["Overtime"].ToString());
                de.ConveyanceAllowance = Convert.ToDecimal(dr["ConveyanceAllowance"].ToString());
                de.EmployeeStateInsurance = Convert.ToDecimal(dr["EmployeeStateInsurance"].ToString());
                de.ProvidentFund = Convert.ToDecimal(dr["ProvidentFund"].ToString());
                de.ProfessionalTax = Convert.ToDecimal(dr["ProfessionalTax"].ToString());
                de.NetAmount = Convert.ToDecimal(dr["NetAmount"].ToString());
                de.TotalDeduction = Convert.ToDecimal(dr["TotalDeduction"].ToString());
                de.TotalEarn = Convert.ToDecimal(dr["TotalEarn"].ToString());
                dr.Close();

                return Json(de, JsonRequestBehavior.AllowGet);
            }
            else
            {
                dr.Close();

                return Json("null", JsonRequestBehavior.AllowGet);
            }
           // con.Close();
        }
        #endregion

        #region[Attendance]

        public ActionResult Attendance()
        {
            return View();
        }

        public JsonResult GetUserForAttendance(string brach, string acadamicyear)
        {
            List<UserforAttendanceDE> de = new List<UserforAttendanceDE>();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT UserMaster.UserID, UserMaster.FirstName, UserMaster.LastName, BranchMaster.BranchName, UserMaster.AcadamicYear FROM UserMaster INNER JOIN BranchMaster ON UserMaster.BranchID = BranchMaster.BranchID where BranchMaster.BranchID=@BranchID and UserMaster.AcadamicYear=@AcadamicYear and UserMaster.Role='Student'", con);
            cmd.Parameters.AddWithValue("@BranchID", Convert.ToInt32(brach));
            cmd.Parameters.AddWithValue("@AcadamicYear", acadamicyear);
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
                return Json(null, JsonRequestBehavior.AllowGet);
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
                    cmd.Parameters.AddWithValue("@Role", "Student");
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
                        updated = attendance + 1;
                        SqlCommand cmd2 = new SqlCommand("UPDATE    AttendanceMaster SET  Month1 =@Month1, Attendance =@Attendance where ( UserID=@UserID)", con);
                        cmd2.Parameters.AddWithValue("@Date", date);
                        cmd2.Parameters.AddWithValue("@Month1", month);
                        cmd2.Parameters.AddWithValue("@Attendance", updated);
                        cmd2.Parameters.AddWithValue("@UserID", userid);
                        cmd2.ExecuteNonQuery();
                        dratt.Close();
                    }
                    return Json("attendance successfully", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("Attendance already set of this student", JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        //yuvraj
         #region [Fee Details]

        public ActionResult FeeDetails()
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

        #region [Notic View]

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

        #endregion

        #region[Get user By id]
        
        /// <summary>
        /// This function get all User details from database by user id
        /// </summary>
        /// <param name="userid">this parameter userd to UserId</param>
        /// <returns></returns>
        /// 

        public JsonResult GetAllUserByID(string userid)
        {
            List<UserDE> de = new List<UserDE>();
            SqlCommand cmd = new SqlCommand("SELECT UserMaster.UserID, GardianMaster.GardianName, GardianMaster.Contact, GardianMaster.Address, GardianMaster.Profession, UserMaster.FirstName, UserMaster.LastName,  UserMaster.AdmissionYear, UserMaster.CurrentAddress, UserMaster.PermanentAddress, UserMaster.ContactNo, UserMaster.EmailID, UserMaster.Gender, UserMaster.LoginName,  UserMaster.Password, UserMaster.Role, UserMaster.AcadamicYear, BranchMaster.BranchName, UserMaster.BranchID FROM BranchMaster INNER JOIN UserMaster ON BranchMaster.BranchID = UserMaster.BranchID INNER JOIN GardianMaster ON UserMaster.UserID = GardianMaster.UserID WHERE(UserMaster.UserID = @UserID)", con);
            cmd.Parameters.AddWithValue("@UserID", userid);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    de.Add(new UserDE()
                    {
                        UserID = dr["UserID"].ToString(),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        FullName = dr["FirstName"].ToString() + " " + dr["LastName"].ToString(),
                        PermanentAddress = dr["PermanentAddress"].ToString(),
                        ContactNo = dr["ContactNo"].ToString(),
                        EmailID = dr["EmailID"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        LoginName = dr["LoginName"].ToString(),
                        Password = dr["Password"].ToString(),
                        AcadamicYear = dr["AcadamicYear"].ToString(),
                        Branch = dr["BranchName"].ToString(),
                        BranchID = Convert.ToInt32(dr["BranchID"]),
                        GardianName = dr["GardianName"].ToString(),
                        Contact = dr["Contact"].ToString(),
                        Address = dr["Address"].ToString(),
                        Profession = dr["Profession"].ToString()
                    }
                   );

                }
                dr.Close();
                con.Close();
                return Json(de, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Null", JsonRequestBehavior.AllowGet);
            }
        }


        #endregion

        #region [Get Notic By Role And Acadmic Year ]

        /// <summary>
        /// This Function is used to get notic from Notice Table Using Role and Acadmic Year 
        /// </summary>
        /// <param name="Role"> it may Be Student or Student </param>
        /// <param name="Acadamicyear"> for acadmic value are= '1st Year','2nd Year','3rd Year','4th Year'</param>
        /// <returns></returns>

        public JsonResult Getnotic(string Role, string Acadamicyear)
        {
           
            List<NoticeDE> de = new List<NoticeDE>();
            SqlCommand cmd = new SqlCommand("select distinct b.BranchName,n.NoticeID,n.AcadamicYear,n.Date,n.Notice,n.Role from BranchMaster b, NoticeDetail n  where b.BranchID=n.BranchID and (n.Role='All' or n.Role=@Role) and n.AcadamicYear=@AcadamicYear ", con);
            con.Open();
            cmd.Parameters.AddWithValue("@Role", Role);
            cmd.Parameters.AddWithValue("@Acadamicyear", Acadamicyear);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
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

                return Json(de, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        //    dr.Close();
        //    con.Close();

        }

        #endregion

        #region [Get Payment Details]

        //

        public JsonResult GetPymentDetailsById(string id)
        {


            List<FeeTransaction> de = new List<FeeTransaction>();
            SqlCommand cmd = new SqlCommand("SELECT FeeTransaction.FeeTransactionID, FeeTransaction.UserID, FeeTransaction.FeeMasterID, FeeMaster.Amount, FeeTransaction.PaidAmount,CASE WHEN CONVERT(numeric(18, 2), FeeMaster.Amount) = CONVERT(numeric(18, 2), FeeTransaction.PaidAmount) THEN 0 WHEN FeeMaster.Amount != FeeTransaction.PaidAmount THEN CONVERT(numeric(18, 2), FeeMaster.Amount) - CONVERT(numeric(18, 2), FeeTransaction.PaidAmount) END AS 'RemAmt' FROM FeeTransaction INNER JOIN FeeMaster ON FeeTransaction.FeeMasterID = FeeMaster.FeeMasterID WHERE(FeeTransaction.UserID = @UserID)", con);
            con.Open();

            cmd.Parameters.AddWithValue("@UserID", id);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                de.Add(new FeeTransaction()
                {
                 //   FeeTransactionID,UserID,FeeMasterID,PaidAmount

                    FeeTransactionID =Convert.ToInt32( dr["FeeTransactionID"].ToString()),
                    UserID =Convert.ToInt32( dr["UserID"].ToString()),
                    FeeMasterID = Convert.ToInt32(dr["FeeMasterID"].ToString()),
                    PaidAmount =Convert.ToDecimal( dr["PaidAmount"].ToString()),
                    Amount = Convert.ToDecimal(dr["Amount"].ToString()),
                    RemAmt = Convert.ToDecimal(dr["RemAmt"].ToString())
                }
               );

            }
            dr.Close();
            con.Close();
            return Json(de, JsonRequestBehavior.AllowGet);

        }

        #endregion

        //

        #region[ClassTimeTable]

        public ActionResult ClassTimeTableView()
        {
            return View();
        }

        public JsonResult GetclassTimeTable1monday(string acadamicyear, string bach)
        {
            con.Open();
            List<classtimetable> de = new List<classtimetable>();
            SqlCommand cmd = new SqlCommand("SELECT   TimeTableMaster.TimetableID, TimeTableTransaction.Subject,TimeTableTransaction.Day, TimeTableTransaction.Time FROM TimeTableMaster INNER JOIN TimeTableTransaction ON TimeTableMaster.TimetableID = TimeTableTransaction.TimeTableID where TimeTableMaster.BranchID=@BranchID and TimeTableMaster.AcadamicYear=@AcadamicYear ", con);
            cmd.Parameters.AddWithValue("@AcadamicYear", acadamicyear);
            cmd.Parameters.AddWithValue("@BranchID", Convert.ToInt32(bach));
            SqlDataReader dr = cmd.ExecuteReader();
            string check="";
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    if ( dr["Day"].ToString() == check)
                    {
                        check = "";
                    }
                    else
                    {
                        check = dr["Day"].ToString();
                    }
                    de.Add(new classtimetable()
                    {
                       
                        Day = check,
                        Subject = dr["Subject"].ToString(),
                        Time = dr["Time"].ToString(),
                    }
                   );

                    check = dr["Day"].ToString();
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

        #endregion

        #region [Update ]
        public JsonResult UpdateAttendance(string userid)
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
                    cmd.Parameters.AddWithValue("@Role", "Student");
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
                        SqlCommand cmd2 = new SqlCommand("UPDATE    AttendanceMaster SET  Month1 =@Month1, Attendance =@Attendance where ( UserID=@UserID)", con);
                        cmd2.Parameters.AddWithValue("@Date", date);
                        cmd2.Parameters.AddWithValue("@Month1", month);
                        cmd2.Parameters.AddWithValue("@Attendance", updated);
                        cmd2.Parameters.AddWithValue("@UserID", userid);
                        cmd2.ExecuteNonQuery();
                        dratt.Close();
                    }
                    return Json("attendance Update successfully", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("Attendance already set of this student", JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}
