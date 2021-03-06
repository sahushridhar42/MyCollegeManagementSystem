﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Reporting.WebForms;

namespace CollegeManagementSystem.Reports
{
    public partial class StudentAttendanceGraph : System.Web.UI.Page
    {
        #region[Create Connection]

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
           // string BranchId = (string)(Session["BranchID"]);
            //try
            //{
                if (IsPostBack == false)
                {

                    GetBranch();
                    //  GetBranch();
                    //  BindAllEmployee();
                    

                }
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
        }



        #region[Display Report]

        private void DisplayReport()
        {
            try
            {

                ReportViewer1.LocalReport.DataSources.Clear();


                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", Getdetailsa()));


                ReportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~//Reports//rptStudentAttandanceGraph.rdlc");
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

                SqlCommand cmd = new SqlCommand("pro_getAttendanceGraph", con);
                cmd.CommandType = CommandType.StoredProcedure;



                CollegeMgmSystemDataSet.pro_getAttendanceGraphDataTable tp = new CollegeMgmSystemDataSet.pro_getAttendanceGraphDataTable();


                SqlDataAdapter SqlDa = new SqlDataAdapter(cmd);

                SqlDa.SelectCommand.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
                SqlDa.SelectCommand.Parameters.AddWithValue("@year",ddlacadamicyear.SelectedValue);
                SqlDa.SelectCommand.Parameters.AddWithValue("@month", ddlmonth.SelectedValue);


                tp.Constraints.Clear();
                SqlDa.Fill(tp);
                SqlDa.SelectCommand.Parameters.Clear();
                return tp;

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

        protected void Button1_Click(object sender, EventArgs e)
        {
            DisplayReport();
        }


    }
}