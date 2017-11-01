<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GraphOfStudentPassByDepartmentName.aspx.cs" Inherits="CollegeManagementSystem.Report_View.GraphOfStudentPassByDepartmentName" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <a href="../Main%20Script/css/"></a>
    <script src="../Main%20Script/js/bootstrap.min.js"></script>
    <script src="../Main%20Script/js/bootstrap.js"></script>
    <link href="../Main%20Script/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Main%20Script/css/bootstrap.css" rel="stylesheet" />
    
 <link href="../Main%20Script/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Main%20Script/css/bootstrap.css" rel="stylesheet" />
    <script type="text/javascript" src="/TemplateScript/js/jquery.min.js"></script>
    <script type="text/javascript" src="/TemplateScript/js/bootstrap.js"></script>
    <script type="text/javascript" src="/TemplateScript/js/bootstrap.min.js"></script>
    <link href="/TemplateScript/css/bootstrap.min.css" rel='stylesheet' type='text/css' />
    <link href="/TemplateScript/css/bootstrap.css" rel='stylesheet' type='text/css' />
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
    <link href="/TemplateScript/css/style.css" rel="stylesheet" type="text/css" media="all" />

    <link href="/TemplateScript/css/slider.css" rel="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" src="~/TemplateScript/js/modernizr.custom.28468.js"></script>
    <script type="text/javascript" src="~/TemplateScript/js/jquery.cslider.js"></script>

</head>
<body>
    <center>
    <form id="form1" runat="server">
            <div class="header_bg">
        <div class="container">
            <div class="row header">
                <div class="logo navbar-left">
                    <h1><a href="index.html">COLLEGE ERP SOLUTION </a></h1>
                    <h3>Students Passed By Department</h3>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>



    <div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="610px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="751px">
            <LocalReport ReportEmbeddedResource="CollegeManagementSystem.Reports.ReportGetStudentPassResultGraph.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="CollegeMgmSystemDataSetTableAdapters.proc_GetPassStudentListGraphTableAdapter"></asp:ObjectDataSource>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    </div>
    </form>
        </center>
</body>
</html>
