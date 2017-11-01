<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GraphAttendanceById.aspx.cs" Inherits="CollegeManagementSystem.Report_View.GraphAttendanceById" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 796px;
        }
    </style>


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
    <form id="form1" runat="server">
    
            <div class="header_bg">
        <div class="container">
            <div class="row header">
                <div class="logo navbar-left">
                    <h1><a href="index.html">COLLEGE ERP SOLUTION </a></h1>
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
                    <asp:DropDownList ID="ddlbranch" runat="server" Height="16px" Width="204px">
                    </asp:DropDownList>
                </td>
                <td class="auto-style4"></td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style5">
                    &nbsp;</td>
                <td class="auto-style4">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">Acadamic Year :&nbsp;&nbsp;&nbsp;&nbsp; </td>
                <td class="auto-style5">
                    <asp:DropDownList ID="ddlacadamicyear" runat="server" Height="18px" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlacadamicyear_SelectedIndexChanged">
                        <asp:ListItem>1st Year</asp:ListItem>
                        <asp:ListItem>2nd Year</asp:ListItem>
                        <asp:ListItem>3rd Year</asp:ListItem>
                        <asp:ListItem>4th Year</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style4"></td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style5">
                    &nbsp;</td>
                <td class="auto-style4">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;&nbsp; Select&nbsp; Month :&nbsp;&nbsp; &nbsp;</td>
                <td class="auto-style5">
                    <asp:DropDownList ID="ddlmonth" runat="server" Height="18px" Width="200px">
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style4">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style5">
                    &nbsp;</td>
                <td class="auto-style4">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">Select Student :</td>
                <td class="auto-style5">
                    <asp:DropDownList ID="ddlStudent" runat="server" Height="16px" Width="196px">
                    </asp:DropDownList>
                </td>
                <td class="auto-style4">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style5">
                    <asp:Button ID="Button1" runat="server" Height="37px" Text="Show Report" Width="146px" OnClick="Button1_Click" />
                </td>
                <td class="auto-style4">&nbsp;</td>
            </tr>
        </table>

        </div>
        <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="767px"></rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
