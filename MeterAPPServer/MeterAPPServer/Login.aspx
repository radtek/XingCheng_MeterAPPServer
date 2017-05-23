<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MeterAPPServer.Login" %>

<!DOCTYPE html>
<!--[if lt IE 7 ]> <html lang="en" class="no-js ie6 lt8"> <![endif]-->
<!--[if IE 7 ]>    <html lang="en" class="no-js ie7 lt8"> <![endif]-->
<!--[if IE 8 ]>    <html lang="en" class="no-js ie8 lt8"> <![endif]-->
<!--[if IE 9 ]>    <html lang="en" class="no-js ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!-->
<html lang="en" class="no-js">
<!--<![endif]-->
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户登录--兴城市自来水公司</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="Login and Registration Form with HTML5 and CSS3" />
    <meta name="keywords" content="html5, css3, form, switch, animation, :target, pseudo-class" />
    <meta name="author" content="Codrops" />
    <link rel="stylesheet" type="text/css" href="css/demo.css" />
    <link rel="stylesheet" type="text/css" href="css/style2.css" />
    <link rel="stylesheet" type="text/css" href="css/animate-custom.css" />
    <script src="/scripts/jquery.js" type="text/javascript"></script>
    <script src="/scripts/layer.js" type="text/javascript"></script>
    <script src="/scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="/Scripts/FunctionJS.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Scripts/jquery.cookie.js"></script>
</head>
<body>
    <div class="container">
        <header>
        </header>
        <section>
            <div id="container_demo">
                <div id="wrapper">
                    <div id="login" class="animate form">
                        <form id="loginForm" runat="server" target="nm_iframe">
                            <h1>兴城市自来水公司</h1>
                            <p>
                                <label for="username" class="uname" data-icon="u">用户名： </label>
                                <input id="username" name="username" required type="text" placeholder="请输入登录名" />
                            </p>
                            <p>
                                <label for="password" class="youpasswd" data-icon="p">密  码： </label>
                                <input id="password" name="password" required type="password" placeholder="请输入密码" />
                            </p>
                            <p class="keeplogin">
                                <input type="checkbox" name="loginkeeping" id="loginkeeping" value="loginkeeping" />
                                <label for="loginkeeping">保存账号密码</label>
                            </p>
                            <p class="login button">
                                <input id="btn_Login" type="submit" onclick="return chkLoginSubmit();" value="登  录" />
                            </p>

                        </form>
                    </div>



                </div>
            </div>
        </section>
    </div>
    <iframe id="id_iframe" name="nm_iframe" style="display:none;"></iframe>  
    <script type="text/javascript">
        var xhr;
        function createXHR() {
            if (window.XMLHttpRequest) { // Mozilla, Safari, ...
                xhr = new XMLHttpRequest();
            } else if (window.ActiveXObject) { // IE
                try {
                    xhr = new ActiveXObject("Microsoft.XMLHTTP");
                } catch (e) {
                    try {
                        xhr = new ActiveXObject("Msxml2.XMLHTTP");
                    } catch (e2) {
                        alert("您的浏览器有问题！");
                    }
                }
            }
        }

        function chkLoginSubmit()
        {
            var UName = $("#username").val();
            var UPwd = $("#password").val();
            if (UName == '' || UPwd == '') {
                layer.msg('账号密码不能为空');
                return false;
            }
            else {
                login();
            }
        }

        $(document).ready(function () {
            if ($.cookie("rmbUser") == "true") {
                $("#loginkeeping").attr("checked", true);
                $("#username").val($.cookie("username"));
                //$("#password").remove();
                $("#password").val($.cookie("password"));
            }
            //$("#btn_Login").click(function () {
            //    //chkLoginSubmit();
            //});
        });

        //记住用户名密码 
        function saveCookie(){
            if ($("#loginkeeping").attr('checked')) {
                var username = $("#username").val();
                var password = $("#password").val();
                $.cookie("rmbUser", "true", { expires: 30 }); //存储一个带30天期限的cookie 
                $.cookie("username", username, { expires: 30 });
                $.cookie("password", password, { expires: 30 });
            } else {
                $.cookie("rmbUser", "false", { expire: -1 });
                $.cookie("username", "", { expires: -1 });
                $.cookie("password", "", { expires: -1 });
            }
        }

        function login() {
            var userName = $("#username").val();
            var userPwd = $("#password").val();
            var parm = 'action=login&username=' + escape(userName) + '&password=' + escape(userPwd);
            getAjax('Frame/Frame.ashx', parm, function (rs) {
               if (parseInt(rs) == 4) {
                    $("#username").focus();
                    layer.msg('账户或密码有错误');
                    return false;
                } else if (parseInt(rs) == 3) {
                    layer.msg('登录成功');
                    $("#btn_Login").attr({ "disabled": "disabled" });
                    saveCookie();
                    window.parent.location.href = 'Main.aspx';
                    return false;
                } else {
                    layer.msg('服务器连接不上,联系管理员');
                    return false;
                }
            });

        }

        //登陆加载
        function Load() {
            window.parent.location.href = 'Main.aspx';
            return false;
        }

        //function showOverlay() {
        //    // 遮罩层宽高分别为页面内容的宽高   
        //    $('.overlay').css({ 'height': $(document).height(), 'width': $(document).width() });
        //    $('.overlay').show();
        //}
    </script>
</body>
</html>
