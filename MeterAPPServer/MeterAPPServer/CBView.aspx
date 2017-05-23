<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CBView.aspx.cs" Inherits="MeterAPPServer.CBView" %>

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
    <script src="Mobiscroll/jquery-1.11.0.min.js"></script>
    <script src="Mobiscroll/mobiscroll.core.js"></script>
    <script src="Mobiscroll/mobiscroll.scroller.js" type="text/javascript"></script>
    <script src="Mobiscroll/mobiscroll.datetime.js" type="text/javascript"></script>
    <script src="/scripts/layer.js" type="text/javascript"></script>
    <link href="Mobiscroll/mobiscroll.scroller.css" rel="stylesheet" type="text/css" />
    <link href="Mobiscroll/mobiscroll.animation.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            var curr = new Date().getFullYear();
            var opt = {
                'date': {
                    preset: 'date',
                    invalid: { daysOfWeek: [0, 6], daysOfMonth: ['5/1', '12/24', '12/25'] }
                },
                'datetime': {
                    preset: 'datetime',
                    minDate: new Date(2012, 3, 10, 9, 22),
                    maxDate: new Date(2014, 7, 30, 15, 44),
                    stepMinute: 5
                },
                'time': {
                    preset: 'time'
                },
                'credit': {
                    preset: 'date',
                    dateOrder: 'mmyy',
                    dateFormat: 'mm/yy',
                    startYear: curr,
                    endYear: curr + 10,
                    width: 100
                },
                'select': {
                    preset: 'select'
                },
                'select-opt': {
                    preset: 'select',
                    group: true,
                    width: 50
                }
            }



            $('.demo-test-date').scroller('destroy').scroller($.extend(opt['date'], {
                theme: 'default',
                mode: 'scroller',
                lang: '',
                display: 'modal',
                animate: 'slidevertical'
            }));

        });
    </script>


</head>
<body id="page">
    <form id="form1" runat="server">
        <div id="pagecontainer">
            <div class="content">
                <div class="form">
                    <div style="height: 30px; line-height: 30px;">
                        <label style="float: left;">抄表月份：</label>
                        <div style="float: left; width: 35%;">
                            <input name="DateBegin" id="DateBegin" required class="demo-test-date form_input" runat="server" value=""/>
                        </div>
                        <div style="float: left; width: 5%; text-align: center">
                            至
                        </div>
                        <div style="float: left; width: 35%;">
                            <input name="DateEnd" id="DateEnd" required class="demo-test-date form_input" runat="server" value="" />
                        </div>
                        <div style="clear: both"></div>
                    </div>
                    <div style="clear: both"></div>

                    <label style="float: left">输入户号或户名:</label>
                    <input id="SearchKey" name="SearchKey" required runat="server" type="text" class="form_input" value=""/>
                    <asp:Button ID="Btn_Search" runat="server" class="form_submit" Text="查    询" OnClientClick="return CheckInput();" OnClick="Btn_Search_Click" />
                    <div id="DataView">
                        <%=ItemList %>
                        
                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
        <div id="footer" class="black_gradient">
            <a href="Main.aspx" class="back_button black_button">返回</a>
            <div class="page_title">抄表详细查询</div>
            <a onclick="jQuery('html, body').animate( { scrollTop: 0 }, 'slow' );" href="javascript:void(0);" id="top" class="black_button">顶部</a>
            <div class="clear"></div>
        </div>
    </form>
    <script>
        function CheckInput() {
            if (($("#DateBegin").val() == '' || $("#DateEnd").val() == '' || $("#SearchKey").val() == '')) {
                layer.msg('查询条件不能为空');
              return false
            }
        }

    </script>
</body>
</html>
