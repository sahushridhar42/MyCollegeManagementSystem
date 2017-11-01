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
    public partial class GraphOfStudentPassByDepartmentName : System.Web.UI.Page
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

                   DisplayReport();


                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }




        #region[Display Report]

        private void DisplayReport()
        {
            try
            {

                ReportViewer1.LocalReport.DataSources.Clear();


                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", Getdetailsa()));


                ReportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~//Reports//ReportGetStudentPassResultGraph.rdlc");
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

                SqlCommand cmd = new SqlCommand("proc_GetPassStudentListGraph", con);
                cmd.CommandType = CommandType.StoredProcedure;



                CollegeMgmSystemDataSet.pro_GetEmployeeAttandanceGraphDataTable tp = new CollegeMgmSystemDataSet.pro_GetEmployeeAttandanceGraphDataTable();


                SqlDataAdapter SqlDa = new SqlDataAdapter(cmd);

              //  SqlDa.SelectCommand.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
               // SqlDa.SelectCommand.Parameters.AddWithValue("@year", ddlacadamicyear.SelectedValue);
                //SqlDa.SelectCommand.Parameters.AddWithValue("@month", ddlmonth.SelectedValue);


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

    }
}