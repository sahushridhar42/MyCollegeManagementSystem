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
    public partial class StudentGraphFeePaid : System.Web.UI.Page
    {

        #region[Create Connection]

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            string BranchId = (string)(Session["uid"]);
            try
            {
                if (IsPostBack == false)
                {

                    DisplayReport(BranchId);

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        #region[Display Report]

        private void DisplayReport(string p)
        {
            try
            {

                ReportViewer1.LocalReport.DataSources.Clear();


                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", Getdetailsa(p)));


                ReportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~//Reports//ReportFeePaidGraphStudent.rdlc");
                ReportViewer1.LocalReport.Refresh();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private DataTable Getdetailsa(string p)
        {
            try
            {

                con.Open();

                SqlCommand cmd = new SqlCommand("proc_StudentGraphPaidList", con);
                cmd.CommandType = CommandType.StoredProcedure;



                CollegeMgmSystemDataSet.proc_GetPassStudentListGraphDataTable tp = new CollegeMgmSystemDataSet.proc_GetPassStudentListGraphDataTable();


                SqlDataAdapter SqlDa = new SqlDataAdapter(cmd);

               // SqlDa.SelectCommand.Parameters.AddWithValue("@userid", p);
                //SqlDa.SelectCommand.Parameters.AddWithValue("@month", (System.DateTime.Now.Month.ToString("d2") + "/" + System.DateTime.Now.Year.ToString()));


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