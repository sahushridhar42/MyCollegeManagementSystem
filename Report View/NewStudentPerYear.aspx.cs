#region[Namespace]

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

#endregion

namespace CollegeManagementSystem.Report_View
{

    public partial class NewStudentPerYear : System.Web.UI.Page
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

        #region[Display Report]

        private void DisplayReport()
    {
        try
        {

            ReportViewer1.LocalReport.DataSources.Clear();


            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", Getdetailsa()));


            ReportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~//Reports//RptNewStudentPerYear.rdlc");
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

            SqlCommand cmd = new SqlCommand("sp_GetNewAddmissionList",con);
            cmd.CommandType = CommandType.StoredProcedure;
           


            CollegeMgmSystemDataSet.sp_GetNewAddmissionListDataTable tp = new CollegeMgmSystemDataSet.sp_GetNewAddmissionListDataTable();


            SqlDataAdapter SqlDa = new SqlDataAdapter(cmd);
            SqlDa.SelectCommand.Parameters.AddWithValue("@branchId", Convert.ToInt32(ddlbranch.SelectedValue));
            SqlDa.SelectCommand.Parameters.AddWithValue("@AcadamicYear",ddlacadamicyear.Text).ToString();
            SqlDa.SelectCommand.Parameters.AddWithValue("@AddYear", ddladdyear.Text).ToString();

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

    protected void Button1_Click(object sender, EventArgs e)
    {
        DisplayReport();
    }

        #endregion

    }
}