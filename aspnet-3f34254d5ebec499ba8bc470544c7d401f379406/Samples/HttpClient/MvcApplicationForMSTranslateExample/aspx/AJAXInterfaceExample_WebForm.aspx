﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm_Example.aspx.cs" Inherits="WebApplication.WebForm_Example" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Trans</title>
</head>
<body>
    <h1>Hello - This is the .aspx Page</h1>

    <form id="form1" runat="server">
        <div>
        </div>
    </form>

    <div>
        <p>
            from emdedded page script
        </p>
        <%= MvcApplicationForMSTranslateExample.CodeLibrary.HtmlCommonIncludeGenerator.getHomeHtmlAnchor() %>
    </div>
    <div>
        <p>
            from server include file
        </p>
        <!-- #include file="~/aspx/_home.html" -->
    </div>
</body>
</html>
