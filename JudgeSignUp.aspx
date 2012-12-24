<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JudgeSignUp.aspx.cs" Inherits="JudgeSignUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="tbAccountName" runat="server"></asp:TextBox>
        <br />
        <asp:TextBox ID="tbPassword" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <asp:Button ID="btnSignUp" runat="server" onclick="btnSignUp_Click" 
            Text="SignUp" />
    
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/JudgeLogin.aspx">返回</asp:HyperLink>
    
        <br />
        <asp:Label ID="lResult" runat="server"></asp:Label>
    
    </div>
    </form>
</body>
</html>
