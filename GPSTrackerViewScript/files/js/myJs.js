$(document).ready(function () {
//$("[rel='tooltip']").tooltip();
if($.cookie('auth') == '1'){
	$('#MyAuthForm').remove();
	var myButton ='<p class="navbar-text pull-right"> <span>' + $.cookie('username') + '&nbsp </span><a id="logout" class="btn">Выйти</a></p>' ;
	$('#MyMarker').append(myButton);
	}
$('#logout').click(function() 
{
	$.cookie('auth','0');
	location.reload();
});
});