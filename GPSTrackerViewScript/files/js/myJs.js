$(document).ready(function () {
//$("[rel='tooltip']").tooltip();
if($.cookie('auth') == '1'){
	$('#MyAuthForm').remove();
	var myButton ='<p class="navbar-text pull-right"> <span>' + $.cookie('username') + '</span><a id="logout" class="btn">Выйти</a></p>' ;
	$('#MyMarker').append(myButton);
	}
$('#logout').click(function() 
{
	$.cookie('auth','0');
	location.reload();
});
});
function latlng2distance(lat1, long1, lat2, long2) {
    //радиус Земли
    var R = 6372795;
     
    //перевод коордитат в радианы
    lat1 *= Math.PI / 180;
    lat2 *= Math.PI / 180;
    long1 *= Math.PI / 180;
    long2 *= Math.PI / 180;
     
    //вычисление косинусов и синусов широт и разницы долгот
    var cl1 = Math.cos(lat1);
    var cl2 = Math.cos(lat2);
    var sl1 = Math.sin(lat1);
    var sl2 = Math.sin(lat2);
    var delta = long2 - long1;
    var cdelta = Math.cos(delta);
    var sdelta = Math.sin(delta);
     
    //вычисления длины большого круга
    var y = Math.sqrt(Math.pow(cl2 * sdelta, 2) + Math.pow(cl1 * sl2 - sl1 * cl2 * cdelta, 2));
    var x = sl1 * sl2 + cl1 * cl2 * cdelta;
    var ad = Math.atan2(y, x);
    var dist = ad * R; //расстояние между двумя координатами в метрах
 
    return dist
}