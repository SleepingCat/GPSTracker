$(document).ready(function () {
$("[rel='tooltip']").tooltip();
if($.cookie('auth') == '1'){
	$('#MyAuthForm').remove();
	var myButton ='<p class="navbar-text pull-right"> Desu &nbsp <a id="logout" class="btn pull-right">Выйти</a></p>' ;
	$('#MyMarker').append(myButton);
	}
$('#logout').click(function() 
{
	$.cookie('auth','0');
	location.reload();
});
});