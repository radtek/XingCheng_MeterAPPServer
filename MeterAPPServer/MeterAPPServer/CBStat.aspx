<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CBStat.aspx.cs" Inherits="MeterAPPServer.CBStat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>抄表查询</title>
    <!-- Main CSS file -->
    <link rel="stylesheet" href="style.css" type="text/css" media="screen" />


    <!-- jQuery file -->
    <script src="js/jquery.min.js"></script>
    <!-- Hide Mobiles Browser Navigation Bar -->
    <script type="text/javascript">
        window.addEventListener("load", function () {
            // Set a timeout...
            setTimeout(function () {
                // Hide the address bar!
                window.scrollTo(0, 1);
            }, 0);
        });
    </script>
</head>
<body id="page">
    <form id="form1" runat="server">
        <div id="pagecontainer">
            <div class="content">
                <div class="form">
                    <label>选择分公司：</label>
                   <div class="select_container">
                        <asp:DropDownList class="form_select" id="Area" name="Area"  runat="server" OnSelectedIndexChanged="Area_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                       
                    </div>
                    <label>抄表员：</label>
                    <div class="select_container">
                        <asp:DropDownList class="form_select" id="CBUser" name="CBUser" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CBUser_SelectedIndexChanged">
                        </asp:DropDownList>
                       
                    </div>
                    <label>表本：</label>
                    <div class="select_container">
                        <asp:DropDownList class="form_select" id="ReadingNO" name="ReadingNO"  runat="server"  AutoPostBack="True">
                        </asp:DropDownList>
                       
                    </div>
                    <label style="display: none">输入户号或户名（可以为空）:</label>
                    <input id="SearchKey" name="SearchKey" runat="server" type="text" class="form_input" style="display: none" />
                    <asp:Button ID="Btn_Search" runat="server" class="form_submit" Text="查    询" OnClick="Btn_Search_Click" />

                    <div id="DataView">
                        <ul>
                            <li>总用户数：<%= UTotal%>
                            </li>
                            <li>抄表员数量：<%=UUserNum %>
                            </li>
                            <li>已抄用户数：<%= UOver%>
                            </li>
                            <li>未抄用户数：<%= UNever%>
                            </li>
                            <li>水量为0用户数：<%= U0%>
                            </li>
                            <li>抄表率：<%=Urate %>%</li>
                            <li>已收费水量：<%=Fwater %>
                            </li>
                            <li>已收费用户：<%=FNum %>
                            </li>
                            <li>已收费金额：<%= FFee%>
                            </li>
                        </ul>
                    </div>
                    <div style="height: 35px; line-height: 35px; float: right; font-weight: bold">
                        <label><a href="CBView.aspx">详细查询</a></label></div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
        <div id="footer" class="black_gradient">
            <a href="Main.aspx" class="back_button black_button">返回</a>
            <div class="page_title">抄表查询统计</div>
            <a onclick="jQuery('html, body').animate( { scrollTop: 0 }, 'slow' );" href="javascript:void(0);" id="top" class="black_button">顶部</a>
            <div class="clear"></div>
        </div>
    </form>
</body>
</html>
