<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Brain.aspx.cs" Inherits="Judge_Brain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/Styles/Style.css" rel="Stylesheet" type="text/css" />
    <title>果壳魅影2.02版上帝之脑 version0.85</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div class="wholepage">
            <div id="divResult" class="header">
                <asp:TextBox ID="tbResult" runat="server" Height="180px" TextMode="MultiLine" 
                    Width="100%"></asp:TextBox>
            </div>
            <div id="divMain" class="content">
                <div id="divLeft" class="leftcolumn">
                    <div id="divPlayerList">
                    <div>
                        <asp:Label ID="lHintPlayerList" runat="server" Text="请输入玩家列表，以分号(;)分隔"></asp:Label></div>
                        <asp:TextBox ID="tbPlayerList" runat="server" Height="400px" 
                            TextMode="MultiLine" Width="90%"></asp:TextBox>
                    </div>
                    <div id="divNewGame">
                        <div style="font-size: small">
                            Werewolf Number:
                            <asp:TextBox ID="tbWerewolfNum" runat="server" MaxLength="1" Width="25px" Wrap="False">2</asp:TextBox>
                            Hunter Number:
                            <asp:TextBox ID="tbHunterNum" runat="server" MaxLength="1" Width="25px" Wrap="False">2</asp:TextBox>
                        </div>
                        <div style="font-size: small">
                            <asp:Label ID="lGameNo" runat="server" Text=""></asp:Label>
                            <asp:Button ID="btnStart" runat="server" onclick="btnStart_Click" Text="New Game" />
                            <asp:Label ID="lDayNo" runat="server" Text="0" Visible="False"></asp:Label>
                        </div>
                    </div>
                    <div id="divLoadGame">
                    </div>
                </div>
                <div id="divOperation" class="rightcolumn">
                    <div id="divAddAction">
                        <div>
                            <asp:DropDownList ID="ddlSource" runat="server" Width="150px" Enabled="False" 
                                AutoPostBack="True" onselectedindexchanged="ddlSource_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlAction" runat="server" Width="80px" Enabled="False" 
                                AutoPostBack="True" onselectedindexchanged="ddlAction_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlTarget" runat="server" Width="150px" Enabled="False">
                            </asp:DropDownList>
                        </div>
                        <div class="alignright">
                            <asp:Button ID="btnAddAction" runat="server" Text="Add Action" 
                                onclick="btnAddAction_Click" Enabled="False" />
                        </div>
                        <div id="divActions">
                            <asp:ListBox ID="lbActions" runat="server" Width="300px" Height="270px"></asp:ListBox>
                            <asp:Button ID="btnDeleteAction" runat="server" Text="Delete" 
                                onclick="btnDeleteAction_Click" />
                            </div>
                        <div class="alignright">
                            <asp:Button ID="btnCalculate" runat="server" Text="Calculate" Enabled="False" 
                                onclick="btnCalculate_Click" /></div>
                        <div>城主动作区<br />
                            <asp:DropDownList ID="ddlJudgeAction" runat="server" Width="80px" 
                                AutoPostBack="True" 
                                onselectedindexchanged="ddlJudgeAction_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlJudgeTarget" runat="server" Width="150px" 
                                AutoPostBack="True" 
                                onselectedindexchanged="ddlJudgeTarget_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlInheritanceFrom" runat="server" Visible="False" 
                                Width="150px">
                            </asp:DropDownList>
                        </div>
                        <div class="alignright">
                            <asp:Button ID="btnJudgeAction" runat="server" Text="Judge Go!" 
                                onclick="btnJudgeAction_Click"/></div>
                    </div>
                </div>
            </div>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
