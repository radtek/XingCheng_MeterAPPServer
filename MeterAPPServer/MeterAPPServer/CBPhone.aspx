<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CBPhone.aspx.cs" Inherits="MeterAPPServer.CBPhone" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>通讯录</title>
    <link rel="stylesheet" href="style.css" type="text/css" media="screen" />
    <style type="text/css">
        .content h2 {
            background: url("images/phone.png") no-repeat left;
            float: left;
            clear: both;
            width: 96%;
            display: block;
            text-align: center;
            font-family: Open Sans;
            font-size: 18px;
            color: #000000;
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            background-color: #ececec;
            border: 1px #d7d7d7 solid;
            padding: 2%;
        }
    </style>


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
                <div class="search">
                    <input type="text" id="SearchKey" name="SearchKey" class="search_input" value="搜索" onclick="this.value = ''" />
                    <input type="image" src="images/search.gif" class="search_submit" />
                    <div class="clear"></div>
                </div>
                <%=sbList %>
                <div class="clear"></div>
            </div>
            <div id="footer" class="black_gradient">
                <a href="Main.aspx" class="back_button black_button">返回</a>
                <div class="page_title">抄表员通讯录</div>
                <a onclick="jQuery('html, body').animate( { scrollTop: 0 }, 'slow' );" href="javascript:void(0);" id="top" class="black_button">顶部</a>
                <div class="clear"></div>
            </div>
        </div>
    </form>
    
</body>
</html>
