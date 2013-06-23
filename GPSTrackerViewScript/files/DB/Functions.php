<?
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
function getUsernameByID($UserID)
{
	$query = @mysql_query("SELECT UserName FROM `Users` WHERE UserID = '".$UserID."'") or die (mysql_error());
	$Name = mysql_fetch_row($query);
	if (isset($Name[0])){return $Name[0];}
	else {die("Пользователь с id $UserID не найден");}
}
function getUserIDByName($Name)
{
	$query = @mysql_query("SELECT UserID FROM `Users` WHERE UserName = '".$Name."'") or die (mysql_error());
	$id = mysql_fetch_row($query);
	if (isset($id[0])){return $id[0];}
	else {die("Пользователь с именем $Name не найден");}
}
// функция удаляет пустые элементы массива
function ArrayTrim(&$a)
{
	foreach($a as $k => $v)
	if ($v=='') unset($a[$k]);
	return $a;
}

// получаем друзей пользователя, друзья в данном случае - те пользователи чьи маршруты можно просматривать и для каждого из них получаем координаты перемещения
$query = @mysql_query("SELECT Friends FROM `Users` WHERE UserName = '".$username."'") or die (mysql_error());
$friendsStr = @mysql_fetch_array($query);
$friendsID = ArrayTrim(explode(";",$friendsStr[0]));
array_unshift($friendsID,getUserIDByName($username));
for($i=0;$i<count($friendsID);$i++)
{
	$friends[] = getUsernameByID($friendsID[$i]);
	if (isset($_COOKIE[$friends[$i].'_limit'])) {$limit = $_COOKIE[$friends[$i].'_limit'];} else {$limit=10;}
	$query = @mysql_query("SELECT Latitude, Longitude, Time, Speed FROM `Coordinates` where UserID = $friendsID[$i] ORDER BY `Time` asc LIMIT $limit") or die (mysql_error());
	//Select `Users`.`UserID`, `UserName`, `Longitude`, `Latitude`, `Speed`, `Time` from `Coordinates`,`Users` where `Users`.`UserID` = `Coordinates`.`UserID`
	while($result = @mysql_fetch_array($query))
	{ 				
		//print_r($result);
		$coords[$friends[$i]][] = $result;
	}
}
/*
echo "<pre>";
print_r($coords);
echo "</pre>";
*/
?>