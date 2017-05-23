/*
html
<div id="selectbox"><input type="hidden" id="city" name="city" value="" /></div>
js
$('#selectbox').selectbox({name:'city',src:'a.json',data:['1','2','3']},function(){alert('hello')});
*/

(function($){
	$.fn.selectbox = function(s,callback) {
		return this.each(function() {
			var $this = $(this);
			s = $.extend({
				name: 'select',
				src: '',
				inputFinal: false,
				data: []
			}, s || {});
			if (!s.src){ return false; }
			var defaultOption = '<option value="">请选择</option>';
			var html = defaultOption;
			var index = 0;
			var indexNew = 0;
			var dataNew;
			var optionIndex = 0;
			var selects;
			var newSelect = function(index,data){
				selects = $this.find('select');
				$.each(data,function(itemIndex,item){
					html += '<option value="'+ item[0]+'">' + item[1] + '</option>';
				});
				selects.eq(index).append(html).show();
				selects.eq(index).change(function(){
					selects.eq(index).nextAll('select').remove();
					optionIndex = $(this).find('option:selected').val();
					if( optionIndex && optionIndex != '' ){
						if(!s.inputFinal){ $this.find('#'+s.name).val(optionIndex); }
						else{ $this.find('#'+s.name).val(''); }
						$.getJSON(s.src,{value:optionIndex},function(dataNew){
							if(dataNew&&dataNew.length>0){
								indexNew = index + 1;
								$this.append('<select id="' + s.name + indexNew + '"name="' + s.name + indexNew + '" style="width:auto;display:none;"></select>');
								selects.eq(indexNew).show();
								html = defaultOption;
								newSelect(indexNew,dataNew);
							}else{
								if(s.inputFinal){ $this.find('#'+s.name).val(optionIndex); }
							}
							if(jQuery.isFunction(callback)){ callback(); }
						});	
					}else{
						if( index == 0||s.inputFinal ){ $this.find('#'+s.name).val(''); }
						else if( optionIndex == '' ){ 
							var lastVal = $(selects[index-1]).val();
							$this.find('#'+s.name).val(lastVal);
						}
						if(jQuery.isFunction(callback)){ callback(); }
					}
				});
				if(s.data[index]&&s.data[index]!=''){ selects.eq(index).val(s.data[index]).change();s.data[index]=''; }
			}
			$this.find('select').remove();
			$this.find('#'+s.name).val('');
			$this.append('<select id="' + s.name + index + '"name="' + s.name + index + '" style="width:auto;display:none;"></select>');
			$.getJSON(s.src,function(data){
				newSelect(index,data);
			});
		});
	}
})(jQuery);