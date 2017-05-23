<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="MeterAPPServer.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>抄表监控</title>

    <!-- Main CSS file -->
    <link rel="stylesheet" href="style.css" type="text/css" media="screen" />
    <!-- jQuery file -->
    <script src="js/jquery.min.js"></script>
    <!-- FlexSlider -->
    <script src="js/jquery.flexslider.js"></script>
    <script type="text/javascript">
        var $ = jQuery.noConflict();
        $(window).load(function () {
            $('.icons_nav').flexslider({
                animation: "slide",
                directionNav: true,
                animationLoop: false,
                controlNav: false,
                slideshow: false,
                animationDuration: 300
            });
            $('.panels_slider').flexslider({
                animation: "slide",
                directionNav: false,
                controlNav: true,
                animationLoop: false,
                slideToStart: 1,
                animationDuration: 300,
                slideshow: false
            });
        });
    </script>
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
    <!-- Hide on iphone top browser element | only on home page -->
    <script type="text/javascript">
        if ((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i))) {
            $(window).load(function () {
                $("body").removeClass("home");

                // Check to see if the window is running in app mode.
                // If it is not, then it is running in full screen mode
                if (("standalone" in window.navigator) && !window.navigator.standalone) {
                    $("body").addClass("homeiphone_app");
                } else {
                    $("body").addClass("homeiphone_full");
                }
            });
        }
    </script>
</head>
<body class="home" style="overflow: hidden;">
    <form id="form1" runat="server">
        <div id="container">

            <div class="content">
                <a class="call_button">兴城市自来水公司</a>
                <div class="post">
                    <span class="post_detail category"><a href="#"><%=TMonth %>月</a></span>
                </div>
                <div id="DataView">
                    <ul>
                        <li>&nbsp;</li>
                        <li>总用户数：<%= UTotal%>
                        </li>
                        <li>抄表员数量：<%=UUserNum %>
                        </li>
                        <li>已抄用户数：<%= UOver%>
                        </li>
                        <li>未抄用户数：<%= UNever%>
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
            </div>
        </div>
        <div id="bottom_nav">
            <div class="icons_nav">
                <div class="paginated">
                    <ul class="slides">
                        <li>
                            <a href="Main.aspx" class="icon">
                                <img src="images/icons/icon_contact.png" alt="" title="" border="0" /><span>首页</span></a>
                            <a href="CBStat.aspx" class="icon">
                                <img src="images/icons/icon_services.png" alt="" title="" border="0" /><span>抄表查询</span></a>
                            <a href="CBMap.aspx" class="icon">
                                <img src="images/icons/icon_photos.png" alt="" title="" border="0" /><span>实时定位</span></a>
                            <a href="CBPhone.aspx" class="icon">
                                <img src="images/icons/icon_clients.png" alt="" title="" border="0" /><span>通讯录</span></a>
                        </li>

                    </ul>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
