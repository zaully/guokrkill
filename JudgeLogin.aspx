<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JudgeLogin.aspx.cs" Inherits="JudgeLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="账号："></asp:Label>
        <asp:TextBox ID="tbAccountName" runat="server" MaxLength="50"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="密码："></asp:Label>
        <asp:TextBox ID="tbPassword" runat="server" MaxLength="8" TextMode="Password"></asp:TextBox>
        <br />
        <asp:Label ID="lResult" runat="server"></asp:Label>
        <br />
        <asp:Button ID="btnLogin" runat="server" Text="Log In" Width="200px" 
            onclick="btnLogin_Click" />
        <br />
        <br />
        <asp:Button ID="btnSignup" runat="server" PostBackUrl="~/JudgeSignUp.aspx" 
            Text="Sign Up" Width="200px" />
        <br />
        <br />
        <asp:Button ID="btnResetPassword" runat="server" Text="Forgot my password" 
            Width="200px" />
    
    </div>
    </form>
</body>
</html>
