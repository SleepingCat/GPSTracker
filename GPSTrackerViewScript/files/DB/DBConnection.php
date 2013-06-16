<?
	$sql_host="192.168.1.8";
	$sql_user="GPSTracker";
	$sql_pass="nanodesu";
	$sql_db="GPSTracker";

	$cserv = @mysql_connect("$sql_host", "$sql_user", "$sql_pass") or die ("Невозможно соединиться с MySQL-сервером.");
	$cbase = @mysql_select_db("$sql_db") or die ("Невозможно соединиться с MySQL-базой.");
	
	
function latlng2distance($lat1, $long1, $lat2, $long2) {
    //радиус Земли
    $R = 6372795;
     
    //перевод коордитат в радианы
    $lat1 *= pi() / 180;
    $lat2 *= pi() / 180;
    $long1 *= pi() / 180;
    $long2 *= pi() / 180;
     
    //вычисление косинусов и синусов широт и разницы долгот
    $cl1 = cos($lat1);
    $cl2 = cos($lat2);
    $sl1 = sin($lat1);
    $sl2 = sin($lat2);
    $delta = $long2 - $long1;
    $cdelta = cos($delta);
    $sdelta = sin($delta);
     
    //вычисления длины большого круга
    $y = sqrt(pow($cl2 * $sdelta, 2) + pow($cl1 * $sl2 - $sl1 * $cl2 * $cdelta, 2));
    $x = $sl1 * $sl2 + $cl1 * $cl2 * $cdelta;
    $ad = atan2($y, $x);
    $dist = $ad * $R; //расстояние между двумя координатами в метрах
 
    return $dist;
}
?>