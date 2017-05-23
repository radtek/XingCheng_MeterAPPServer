<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CBMap.aspx.cs" Inherits="MeterAPPServer.CBMap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>人员定位</title>
      <style type="text/css">
        body, html
        {
            width: 100%;
            height: 100%;
            margin: 0;
            font-family: "微软雅黑";
        }
        #allmap
        {
            width: 100%;
            height: 100%;
        }
        p
        {
            margin-left: 5px;
            font-size: 14px;
        }
    </style>
<link rel="stylesheet" href="style.css"  type="text/css" media="screen" />

<!-- jQuery file -->
<script src="js/jquery.min.js" ></script>
 <script src="/Scripts/FunctionJS.js" type="text/javascript"></script>

<!-- Hide Mobiles Browser Navigation Bar -->

    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=0KTzcYwNtzBqPtA8qOive4zojUAALv41"></script>

</head>
<body>
    
    <div id="allmap">
    </div>
     <div id="footer" class="black_gradient">
            <a href="Main.aspx"  class="back_button black_button">返回</a>
         
            
            <div class="clear"></div>
        </div>
    
<script type="text/javascript">
    // 百度地图API功能
    var map = new BMap.Map("allmap");
    map.centerAndZoom(new BMap.Point(120.73542, 40.615744), 15);

    $(document).ready(function () {
        var parm = '';
        getAjax('Frame/UserGps.ashx', parm, function (rs) {
            if (parseInt(rs) != "") {
                LoadInfo(eval(rs))
                return false;
            } else {
                
                return false;
            }
        });


    });
    function LoadInfo(rs) {
        var data_info =rs;

        for (var i = 0; i < data_info.length; i++) {
            var pt = new BMap.Point(data_info[i][0], data_info[i][1]);
            var myIcon = new BMap.Icon("/images/UserHead/" + data_info[i][4]+".png", new BMap.Size(21, 31));
            //var label = new BMap.Label("抄表员", { offset: new BMap.Size(20, -10) });
            var marker = new BMap.Marker(pt, { icon: myIcon });  // 创建标注

            var content ='电话：'+ data_info[i][6];
            map.addOverlay(marker);               // 将标注添加到地图中
            // marker.setLabel(label);
            addClickHandler(content, data_info[i][3] + '--' + data_info[i][5], marker);
        }
    }
   
    function addClickHandler(content,tag, marker) {
        marker.addEventListener("click", function (e) {
            openInfo(content,tag, e)
        }
		);
    }
    function openInfo(content,tag, e) {
        var p = e.target;
        var point = new BMap.Point(p.getPosition().lng, p.getPosition().lat);
        var opts = {
            width: 100,     // 信息窗口宽度
            height: 30,     // 信息窗口高度
            title: tag, // 信息窗口标题
            enableMessage: true//设置允许信息窗发送短息
        };
        var infoWindow = new BMap.InfoWindow(content, opts);  // 创建信息窗口对象 
        map.openInfoWindow(infoWindow, point); //开启信息窗口
    }


</script>
</body>
</html>

