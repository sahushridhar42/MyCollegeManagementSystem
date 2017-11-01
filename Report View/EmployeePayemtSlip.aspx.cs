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
    public partial class EmployeePayemtSlip : System.Web.UI.Page
    {
        #region[Create Connection]

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

        #endregion

        public string BranchId; 

        protected void Page_Load(object sender, EventArgs e)
        {
            BranchId = (string)(Session["uid"]);
            try
            {
                if (IsPostBack == false)
                {
                  
                  //  GetBranch();
                  //  BindAllEmployee();
                    //DisplayReport(BranchId );

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


                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("PaymentSlipDataSet1", Getdetailsa(p)));


                ReportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~//Reports//ReportSalary.rdlc");
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

                SqlCommand cmd = new SqlCommand("GetPaymentSlip", con);
                cmd.CommandType = CommandType.StoredProcedure;



                CollegeMgmSystemDataSet.GetPaymentSlipDataTable tp = new CollegeMgmSystemDataSet.GetPaymentSlipDataTable();


                SqlDataAdapter SqlDa = new SqlDataAdapter(cmd);

                SqlDa.SelectCommand.Parameters.AddWithValue("@userid", p);
                if (RadioButton1.Checked == true)
                {
                    SqlDa.SelectCommand.Parameters.AddWithValue("@month", (System.DateTime.Now.Month.ToString("d2") + "/" + System.DateTime.Now.Year.ToString()));
                }
                else
                {
                    string d,m, y;
                    m = ddlmonth.SelectedValue.ToString();
                    y = ddlyer.SelectedValue.ToString();
                    d = m + "/" + y;
                    SqlDa.SelectCommand.Parameters.AddWithValue("@month", d.ToString());
             
                }

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

        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
         
        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton1.Checked == true)
            {
                lblmonth.Visible = false;
                ddlmonth.Visible = false;
                lblyear.Visible = false;
                ddlyer.Visible = false;
            }
            else
            {
                lblmonth.Visible = true;
                lblyear.Visible = true;
                ddlmonth.Visible = true;
                ddlyer.Visible = true;
            }
        }

        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton2.Checked == true)
            {
                lblmonth.Visible = true;
                lblyear.Visible = true;
                ddlmonth.Visible = true;
                ddlyer.Visible = true;
            }
            else
            {
                lblmonth.Visible = false;
                ddlmonth.Visible = false;
                lblyear.Visible = false;
                ddlyer.Visible = false;
            }
        }

        protected void btnshow_Click(object sender, EventArgs e)
        {
            DisplayReport(BranchId);
        }

      
    }
}