﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="emailTest.aspx.cs" Inherits="SGA.emailTest" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:TextBox ID="TextBox1" runat="server" Width="350px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Send Email Test" />
        <asp:ImageButton ID="imb" runat="server" 
            ImageUrl="~/Images/btn-register-here.png" onclick="imb_Click" />
    </div>
    </form>
</body>
</html>