using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Reporting.WebForms;

namespace CollegeManagementSystem.Report_View
{
    public partial class GraphAttendanceById : System.Web.UI.Page
    {
        #region[Create Connection]

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack == false)
                {
                    GetBranch();
                    BindStudent();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }


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

        #region[Get Branch]

        public void BindStudent()
        {
            SqlCommand cmd = new SqlCommand("select FirstName+ ' '+LastName as Fullnam,* from UserMaster where BranchID=@BranchID and AcadamicYear=@AcadamicYear", con);
            con.Open();
            cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
            cmd.Parameters.AddWithValue("@AcadamicYear", ddlacadamicyear.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ddlStudent.DataSource = ds;
            ddlStudent.DataTextField = "Fullnam";
            ddlStudent.DataValueField = "UserID";
            ddlStudent.DataBind();
            con.Close();

        }

        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            DisplayReport();
        }
        #region[Display Report student]

        private void DisplayReport()
        {
            try
            {

                ReportViewer1.LocalReport.DataSources.Clear();


                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", Getdetailsa()));


                ReportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~//Reports//rptStudentAttendanceGraphReportByID.rdlc");
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

                SqlCommand cmd = new SqlCommand("proGetStudentAttendanceByIdForGraph", con);
                cmd.CommandType = CommandType.StoredProcedure;



                CollegeMgmSystemDataSet.proGetStudentAttendanceByIdForGraphDataTable tp = new CollegeMgmSystemDataSet.proGetStudentAttendanceByIdForGraphDataTable();


                SqlDataAdapter SqlDa = new SqlDataAdapter(cmd);
                SqlDa.SelectCommand.Parameters.AddWithValue("@BranchID", Convert.ToInt32(ddlbranch.SelectedValue));
                SqlDa.SelectCommand.Parameters.AddWithValue("@year", ddlacadamicyear.Text).ToString();
                SqlDa.SelectCommand.Parameters.AddWithValue("@month", ddlmonth.SelectedValue).ToString();
                SqlDa.SelectCommand.Parameters.AddWithValue("@search", ddlStudent.SelectedValue).ToString();


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

        protected void ddlacadamicyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindStudent();
        }
    }
}