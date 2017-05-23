//validator default
jQuery.extend(jQuery.validator.messages, {
	required: "<font color=red>*必填项</font>",
	remote: "请修正该项",
	requiredChoose: "<font color=red>*必选项</font>",
	email: "<font color=red>*请输入正确格式的电子邮件</font>",
	url: "请输入合法的网址",
	date: "请输入合法的日期",
	dateISO: "日期格式必须为yyyy-mm-dd",
	number: "<font color=red>*请输入合法的数字</font>",
	digits: "只能输入整数",
	creditcard: "请输入合法的信用卡号",
	equalTo: "请再次输入相同的值",
	accept: "请输入拥有合法后缀名的字符串",
	maxlength: jQuery.validator.format("请输入一个长度最多是 {0} 的字符串"),
	minlength: jQuery.validator.format("请输入一个长度最少是 {0} 的字符串"),
	rangelength: jQuery.validator.format("请输入一个长度介于 {0} 和 {1} 之间的字符串"),
	range: jQuery.validator.format("请输入一个介于 {0} 和 {1} 之间的值"),
	max: jQuery.validator.format("请输入一个最大为 {0} 的值"),
	min: jQuery.validator.format("请输入一个最小为 {0} 的值")
});

jQuery(function() {
//dialog default
	$.ui.dialog.defaults.bgiframe = false;
	$.ui.dialog.defaults.autoOpen = false;

//datepicker default
	$.datepicker.setDefaults($.extend($.datepicker.regional['']));
	$('input.datepicker').datepicker({
		showOn: 'button',
		buttonImage: '/themes/default/images/calendar.gif',
		buttonImageOnly: true,
		changeMonth: true,
		changeYear: true,
		dateFormat: 'yy-mm-dd',
		dayNames: ['星期日','星期一','星期二','星期三','星期四','星期五','星期六'],
		dayNamesMin: ['日','一','二','三','四','五','六'],
		dayNamesShort: ['周日','周一','周二','周三','周四','周五','周六'],
		monthNames: ['一月','二月','三月','四月','五月','六月','七月','八月','九月','十月','十一月','十二月'],
		monthNamesShort: ['1','2','3','4','5','6','7','8','9','10','11','12'],
		showMonthAfterYear: true,
		yearRange: '1970:2030',
		onClose: function() { $(this).focus(); }
	},$.datepicker.regional['zh-CN']);

	//字符串
	$.validator.addMethod("isWord",function(value, element) {
		return this.optional(element) || /^\w+$/.test(value);
	},"只能包含数字、字符或下划线");
	//纯汉字
	$.validator.addMethod("chinese",function(value, element) {
		return this.optional(element) || /^[\u4E00-\u9FA5]+$/.test(value);
	},"<font color=red>只能使用汉字</font>");
	//身份证
	$.validator.addMethod("idCard",function(value, element) {
		return this.optional(element) || /(^\d{15}$)|(^\d{17}([0-9]|X|x)$)/.test(value);
	},"请输入15或18位身份证号码，18位末尾可以是X");
	//选择框
	$.validator.addMethod("selectRequired",function(value, element) {
		if(value == '0') { return this.optional(element) }
		else { return true };
	},$.validator.messages.required);
	$.validator.addMethod("userRequired",function(value, element) {
		return this.optional(element) || /^[a-z\A-Z\d\u4E00-\u9FA5-_]+$/.test(value);
	},"<font color=red>只能包含中文、数字、字符或下划线</font>");
	$.validator.addMethod("emailRequired",function(value, element) {
		return this.optional(element) || /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/.test(value);
	},"<font color=red>请输入正确的电子邮箱!</font>");
	//二选一
	$.validator.addMethod("oneRequired",function(value, element, param) {
		var paramVal = $(param).val();
		if(value == '' && paramVal == '') { return false }
		else { return true };
	},$.validator.messages.required);
	//电话
	$.validator.addMethod("telephone",function(value, element) {
		return this.optional(element) || /^\d{3,4}-\d{7,9}$/.test(value);
	},"<font color=red>电话格式为010-12345678</font>");
	//联系电话
	$.validator.addMethod("phone",function(value, element) {
       return this.optional(element) || /^[0-9-]{1,}$/.test(value);
	},"<font color=red>电话格式为0310-12345678</font>");
	//手机     
	$.validator.addMethod("mobile",function(value, element) {
		return this.optional(element) || /^(13|15|18)[0-9]{9}$/.test(value);
	},"<font color=red>请输入正确的手机号</font>");
	//邮编
	$.validator.addMethod("isZipCode", function(value, element) {
		return this.optional(element) || /^[0-9]{6}$/.test(value);
	}, "<font color=red>请输入正确的邮政编码</font>");
	//时间比较
	$.validator.addMethod("dateCompare",function(value, element, param) {
		var paramtime = $(param).val();
		var comp = comptime(paramtime,value);
		if(comp == true) { return true }
		else { return this.optional(element); }
	},"自提时间必须早于当前时间");
	
	//号卡身份证
	$.validator.addMethod("simcardIdCard",function(value, element) {
		return this.optional(element) || /(^\d{15}$)|(^\d{17}([0-9]|X|x)$)/.test(value);
	},"<font color=red>请输入15或18位身份证号码，18位末尾可以是X</font>");
	
	//号卡手机号与座机校验
	$.validator.addMethod("simcardMobileOrTel",function(value, element) {
		if(value.indexOf("-") == -1) {	//手机号
			return this.optional(element) || /^(13|15|18)[0-9]{9}$/.test(value);
		} else {	//座机
			return this.optional(element) || /^\d{3,4}-\d{7,9}$/.test(value);
		}
	}, "<font color=red>号码格式错误</font>");

	$.validator.addClassRules({
		username: {
			isWord:true,
			rangelength:[6,18]
		},
		password: {
			isWord:true,
			rangelength:[6,12]
		},
		realname: {
			chinese:true,
			minlength:2
		}
	});
	
	//常用页码
	if($('.page-number-detail') && $('.page-number-detail').length>0) {
		$('.page-number-detail').each(function(){
			var $this = $(this);
			var spanHtml = '';
			var pageTotal = parseInt($this.find('input[name="pagetotal"]').val());
			var pageCurrent = parseInt($this.find('input[name="pagecurrent"]').val());
			var pageurl = $this.find('input[name="pageurl"]').val();
			var pageparm = $this.find('input[name="pageparm"]').val();
			var pageBegin = 1;
			var pageFinish = 10;
			if(pageTotal<=10){
				pageBegin = 1;
				pageFinish = pageTotal;
			}else if(pageCurrent<=6){
				pageBegin = 1;
				pageFinish = 10;
			}else if(pageCurrent>=pageTotal-4){
				pageFinish = pageTotal;
				pageBegin = pageTotal - 9;
			}else{
				pageBegin = pageCurrent - 5;
				pageFinish = pageCurrent + 4;
			}
			if(pageBegin>1){
				spanHtml += '<em>...</em>';
			}
			for(i = pageBegin;i <= pageFinish;i++){
				if(i == pageCurrent){
					spanHtml += " <a class='current'>" + i + "</a> ";
				}else{
					spanHtml += ' <a href="'+pageurl + i + pageparm +'">' + i + "</a> ";
				}
			}
			if(pageFinish<pageTotal){
				spanHtml += '<em>...</em>';
			}
			$this.find('input[name="topageNo"]').val(pageCurrent);
			$this.find('.page-num').html(spanHtml);
			$this.find('input[name="gotopage"]').click(function(){
				var num = $this.find('input[name="topageNo"]').val();
				window.location = pageurl + num + pageparm;
			});
		});
	}


});

function comptime(beginTime,endTime){
	var today = getNowFormatDate();
	var startDate = new Date(today.replace("-",",")).getTime() ;
	var endDate = new Date(edate.replace("-",",")).getTime() ;
	
	if( startDate > endDate )
	{  
	   return false;
	}
	return true;
}

function getNowFormatDate()
{
   var day = new Date();

   var Year = 0;
   var Month = 0;
   var Day = 0;
   var CurrentDate = "";
   //初始化时间   
   Year       = day.getFullYear();
   Month      = day.getMonth()+1;
   Day        = day.getDate();
  
   CurrentDate += Year + "-";
  
   if (Month >= 10 )
   {
    CurrentDate += Month + "-";
   }
   else
   {
    CurrentDate += "0" + Month + "-";
   }
   if (Day >= 10 )
   {
    CurrentDate += Day ;
   }
   else
   {
    CurrentDate += "0" + Day ;
   }
   return CurrentDate;
} 

jQuery.extend({
	alert:function(txt,callback){
		var htmlAlert = '<div id="systemAlert" title="系统提示">';
		htmlAlert += '<div class="msg">' + txt + '</div></div>';
		$('body').append(htmlAlert);
		
		$('#systemAlert').dialog({
			bgiframe: false,
			autoOpen: true,
			modal: true,
			position: "middle", 
			resizable: false,
			buttons: {
				确定: function() {
					$(this).dialog('close');
					if(jQuery.isFunction(callback)){ callback(); }
				}
			},
			close:function(){
				$('#systemAlert').dialog('destroy').remove();
			}
		});
	},
	confirm:function(txt,callback){
		var htmlConfirm = '<div id="systemConfirm" title="操作确认">';
		htmlConfirm += '<div class="msg">' + txt + '</div></div>';
		$('body').append(htmlConfirm);
		
		$('#systemConfirm').dialog({
			bgiframe: false,
			autoOpen: true,
			position: "middle", 
			modal: true,
			resizable: false,
			buttons: {
				取消: function(){
					$(this).dialog('close');
				},
				确定: function() {
					$(this).dialog('close');
					if(jQuery.isFunction(callback)){ callback(); }
				}
			},
			close:function(){
				$('#systemConfirm').dialog('destroy').remove();
			}
		});
	},
	reconfirm:function(txt,callback){
		var htmlConfirm = '<div id="systemConfirm" title="操作确认">';
		htmlConfirm += '<div class="msg">' + txt + '</div></div>';
		$('body').append(htmlConfirm);
		
		$('#systemConfirm').dialog({
			bgiframe: false,
			autoOpen: true,
			position: "middle", 
			modal: true,
			resizable: false,
			buttons: {
				返回: function(){
					$(this).dialog('close');
				},
				不保存: function() {
					$(this).dialog('close');
					if(jQuery.isFunction(callback)){ callback(); }
				}
			},
			close:function(){
				$('#systemConfirm').dialog('destroy').remove();
			}
		});
	}
}); 