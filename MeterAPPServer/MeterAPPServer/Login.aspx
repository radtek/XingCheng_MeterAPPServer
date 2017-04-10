<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MeterAPPServer.Login" %>

<!DOCTYPE html>
<!--[if lt IE 7 ]> <html lang="en" class="no-js ie6 lt8"> <![endif]-->
<!--[if IE 7 ]>    <html lang="en" class="no-js ie7 lt8"> <![endif]-->
<!--[if IE 8 ]>    <html lang="en" class="no-js ie8 lt8"> <![endif]-->
<!--[if IE 9 ]>    <html lang="en" class="no-js ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <html lang="en" class="no-js"> <!--<![endif]-->
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
</head>
<body>
    <div class="container">
        <header>
        </header>
        <section>
            <div id="container_demo">
                <div id="wrapper">
                    <div id="login" class="animate form">
                        <form action="mysuperscript.php" autocomplete="on" runat="server">
                            <h1>兴城市自来水公司</h1>
                            <p>
                                <label for="username" class="uname" data-icon="u">用户名： </label>
                                <input id="username" name="username" required type="text" placeholder="请输入登录名" />
                            </p>
                            <p>
                                <label for="password" class="youpasswd" data-icon="p">密  码： </label>
                                <input id="password" name="password" required type="password" placeholder="请输入密码" />
                            </p>

                            <p class="login button">
                                <input type="submit" value="登  录" />
                            </p>
                        </form>
                    </div>



                </div>
            </div>
        </section>
    </div>
</body>
</html>
