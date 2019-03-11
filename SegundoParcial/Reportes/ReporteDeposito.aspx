﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteDeposito.aspx.cs"
    Inherits="SegundoParcial.Reportes.ReporteDeposito" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>


    <style>
        html, body, form, #div1 {
            height: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="div1">

            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ID="DepositoReportViewer" runat="server" Height="100%" Width="100%"></rsweb:ReportViewer>

        </div>
    </form>
</body>
</html>
