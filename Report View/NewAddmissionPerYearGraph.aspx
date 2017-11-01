<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewAddmissionPerYearGraph.aspx.cs" Inherits="CollegeManagementSystem.Report_View.NewAddmissionPerYearGraph" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 944px;
        }
    </style>
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
                    <h3>New Admissions per year</h3>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>

    <div>
    <table class="auto-style1">
            <tr>
                <td class="auto-style3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Branch :&nbsp;&nbsp;&nbsp;&nbsp; </td>
                <td class="auto-style5">
                    <asp:DropDownList ID="ddlbranch" runat="server" Height="25px" Width="204px">
                    </asp:DropDownList>
                </td>
                <td class="auto-style2"></td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style5">
                    &nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">Acadamic Year :&nbsp;&nbsp;&nbsp;&nbsp; </td>
                <td class="auto-style5">
                    <asp:DropDownList ID="ddlacadamicyear" runat="server" Height="25px" Width="200px">
                        <asp:ListItem>1st Year</asp:ListItem>
                        <asp:ListItem>2nd Year</asp:ListItem>
                        <asp:ListItem>3rd Year</asp:ListItem>
                        <asp:ListItem>4th Year</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style2"></td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style5">
                    &nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style5">
                    <asp:Button ID="Button1" runat="server" Height="37px" OnClick="Button1_Click" Text="Show Report" Width="146px" class="btn btn-info"/>
                </td>
                <td class="auto-style2">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style5">
                    &nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
            </tr>
        </table>
    </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="587px" Width="967px"></rsweb:ReportViewer>
    </form>
        </center>
</body>
</html>
