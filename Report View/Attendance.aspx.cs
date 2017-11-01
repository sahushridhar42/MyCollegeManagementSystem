using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace CollegeManagementSystem.Report_View
{
    public partial class Attendance : System.Web.UI.Page
    {
        #region[Create Connection]

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

        #endregion

        #region[Page Load]

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack == false)
                {
                    GetBranch();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region[Get Branch]

        public void GetBranch()
        {
            SqlCommand cmd = new SqlCommand("select * from BranchMaster", con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ddlbranch.DataSource = ds;
            ddlbranch.DataTextField = "BranchName";
            ddlbranch.DataValueField = "BranchID";
            ddlbranch.DataBind();
            con.Close();

        }

        #endregion

        #region[Display Report student]

        private void DisplayReport()
        {
            try
            {

                ReportViewer1.LocalReport.DataSources.Clear();


                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", Getdetailsa()));


                ReportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~//Reports//RptAttendance.rdlc");
                ReportViewer1.LocalReport.Refresh();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private DataTable Getdetailsa()
        {
            try
            {

                con.Open();

                SqlCommand cmd = new SqlCommand("sp_GetUserAttendance", con);
                cmd.CommandType = CommandType.StoredProcedure;



                CollegeMgmSystemDataSet.sp_GetUserAttendanceDataTable tp = new CollegeMgmSystemDataSet.sp_GetUserAttendanceDataTable();


                SqlDataAdapter SqlDa = new SqlDataAdapter(cmd);
                SqlDa.SelectCommand.Parameters.AddWithValue("@BranchID", Convert.ToInt32(ddlbranch.SelectedValue));
                SqlDa.SelectCommand.Parameters.AddWithValue("@AcadamicYear", ddlacadamicyear.Text).ToString();
                SqlDa.SelectCommand.Parameters.AddWithValue("@Month1", ddlmonth.SelectedValue).ToString();

                SqlDa.Fill(tp);
                SqlDa.SelectCommand.Parameters.Clear();
                con.Close();
                return tp;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        #endregion


        // For Empolyee

        #region[Display Report student]

        private void DisplayReportEmp()
        {
            try
            {

            //    ReportViewer1.LocalReport.DataSources.Clear();


                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", Getdetailsaemp()));


                ReportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~//Reports//RptAttendance.rdlc");
                ReportViewer1.LocalReport.Refresh();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private DataTable Getdetailsaemp()
        {
            try
            {

                con.Open();

                SqlCommand cmd = new SqlCommand("sp_GetEmployee_Attendance", con);
                cmd.CommandType = CommandType.StoredProcedure;



                CollegeMgmSystemDataSet.sp_GetEmployee_AttendanceDataTable tp = new CollegeMgmSystemDataSet.sp_GetEmployee_AttendanceDataTable();


                SqlDataAdapter SqlDa = new SqlDataAdapter(cmd);
                SqlDa.SelectCommand.Parameters.AddWithValue("@BranchID", Convert.ToInt32(ddlbranch.SelectedValue));
                SqlDa.SelectCommand.Parameters.AddWithValue("@AcadamicYear", ddlacadamicyear.Text).ToString();
                SqlDa.SelectCommand.Parameters.AddWithValue("@Month1", ddlmonth.SelectedValue).ToString();

                SqlDa.Fill(tp);
                SqlDa.SelectCommand.Parameters.Clear();
                con.Close();
                return tp;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            DisplayReport();
            DisplayReportEmp();
        }
    }
}