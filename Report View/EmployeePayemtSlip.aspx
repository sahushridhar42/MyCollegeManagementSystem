<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeePayemtSlip.aspx.cs" Inherits="CollegeManagementSystem.Report_View.EmployeePayemtSlip" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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


        <center>
    <div style="width: 806px">
        <br />
        <asp:RadioButton ID="RadioButton1" text="Current Month Salary" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="RadioButton1_CheckedChanged"></asp:RadioButton> &nbsp;&nbsp;
&nbsp; &nbsp;       <asp:RadioButton ID="RadioButton2" runat="server" Text="Previous Month Salary" AutoPostBack="True" GroupName="a" OnCheckedChanged="RadioButton2_CheckedChanged"></asp:RadioButton> <br /><asp:Label ID="lblmonth" runat="server" Text="Select Month :"></asp:Label> &nbsp;&nbsp; <asp:DropDownList ID="ddlmonth" hieght="200px" width="300px" runat="server">
            <asp:ListItem Value="01">January</asp:ListItem>
            <asp:ListItem Value="02">February</asp:ListItem>
            <asp:ListItem Value="03">March</asp:ListItem>
            <asp:ListItem Value="04">April</asp:ListItem>
            <asp:ListItem Value="05">May</asp:ListItem>
            <asp:ListItem Value="06">June</asp:ListItem>
            <asp:ListItem Value="07">July</asp:ListItem>
            <asp:ListItem Value="08">August</asp:ListItem>
            <asp:ListItem Value="09">September</asp:ListItem>
            <asp:ListItem Value="10">October</asp:ListItem>
            <asp:ListItem Value="11">November</asp:ListItem>
            <asp:ListItem Value="12">December</asp:ListItem>
        </asp:DropDownList><br />
         <br /> <asp:Label ID="lblyear" runat="server" Text="Select Year :"></asp:Label> &nbsp;&nbsp; <asp:DropDownList ID="ddlyer" hieght="200px" width="300px" runat="server">
            <asp:ListItem>2010</asp:ListItem>
            <asp:ListItem>2011</asp:ListItem>
            <asp:ListItem>2012</asp:ListItem>
            <asp:ListItem>2013</asp:ListItem>
            <asp:ListItem>2014</asp:ListItem>
            <asp:ListItem>2015</asp:ListItem>
            <asp:ListItem>2016</asp:ListItem>
        </asp:DropDownList>
        <br />
<asp:Button ID="btnshow" runat="server" Text="Show" OnClick="btnshow_Click"></asp:Button>
        <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <br />
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="499px" Width="792px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
            <LocalReport ReportEmbeddedResource="CollegeManagementSystem.Reports.ReportSalary.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="PaymentSlipDataSet1" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="CollegeMgmSystemDataSetTableAdapters.GetPaymentSlipTableAdapter"></asp:ObjectDataSource>
    </div>
            </center>
    </form>
</body>
</html>
