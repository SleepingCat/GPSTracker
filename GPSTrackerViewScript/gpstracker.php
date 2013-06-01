<?session_start();?>
<!DOCTYPE html5>

<?php
if ($_GET["username"] && $_GET["psw"])
{
	$username = htmlspecialchars($_GET["username"]);

	$sql_host="192.168.1.8";
	$sql_user="GPSTracker";
	$sql_pass="nanodesu";
	$sql_db="GPSTracker";

	$cserv = @mysql_connect("$sql_host", "$sql_user", "$sql_pass") or die ("Невозможно соединиться с MySQL-сервером.");
	$cbase = @mysql_select_db("$sql_db") or die ("Невозможно соединиться с MySQL-базой.");
	
	$auth = mysql_fetch_row(mysql_query("SELECT Count(UserName) FROM `Users` WHERE `UserName` = '$username' and `Password` = '".MD5($_GET["psw"])."'"));

	if ($auth[0] != 1){ die("Auth fail");} else {$_SESSION['username'] = $username;}
	
	$query = @mysql_query("SELECT Latitude, Longitude FROM `$username` ORDER BY `Time` desc LIMIT 10") or die (mysql_error());

	while($result = @mysql_fetch_array($query))
	{ 				
		$coords[] = $result;
	}
echo' 	
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Карта.</title>
	<meta name="viewport" content="initial-scale=1.0, user-scalable=no">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="/maps/documentation/javascript/examples/default.css" rel="stylesheet">
    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false"></script>
    <script src="http://api-maps.yandex.ru/2.0/?load=package.full&lang=ru-RU" type="text/javascript"></script>
    <script type="text/javascript">

        ymaps.ready(init);

        function init () {
            var myMap = new ymaps.Map(\'map\', {
                    center:['.$coords[0][Latitude].','.$coords[0][Longitude].'],
                    zoom: 16
                });

            myMap.controls
                // Кнопка изменения масштаба
                .add(\'zoomControl\')
                // Список типов карты
                .add(\'typeSelector\')
                // Кнопка изменения масштаба - компактный вариант
                // Расположим её справа
                .add(\'smallZoomControl\', { right: 5, top: 75 })
                // Стандартный набор кнопок
                .add(\'mapTools\');

            // Также в метод add можно передать экземпляр класса, реализующего определенный элемент управления.
            // Например, линейка масштаба (\'scaleLine\')
            myMap.controls
                .add(new ymaps.control.ScaleLine())
                // В конструкторе элемента управления можно задавать расширенные
                // параметры, например, тип карты в обзорной карте
                .add(new ymaps.control.MiniMap({
                    type: \'yandex#publicMap\'
                }));
				
				// Ломаная 
			';
			echo "var geometry = [";
			for ($i = 0; $i <= count($coords) - 2; $i++)
			{
				echo "[".$coords[$i][Latitude].",".$coords[$i][Longitude]."],";
			}
			echo "[".$coords[$i][Latitude].",".$coords[$i][Longitude]."]";
			echo "],";
			echo'
			properties = {
				hintContent: "'.$username.'"
			},
			
			options = {
				draggable: true,
				strokeColor: \'#00ff00\',
				strokeWidth: 2
			},
			
			polyline = new ymaps.Polyline(geometry, properties, options);
			myMap.geoObjects.add(polyline);	
			
        }

        function initialize() 
		{
			var myLatLng = new google.maps.LatLng('.$coords[0][Latitude].','.$coords[0][Longitude].');
			var mapOptions = {
			  zoom: 18,
			  center: myLatLng,
			  mapTypeId: google.maps.MapTypeId.HYBRID
			};

			var map = new google.maps.Map(document.getElementById(\'map-canvas\'), mapOptions);

			';
			echo 'var flightPlanCoordinates = [';
			for ($i = 0; $i <= count($coords) - 2; $i++)
			{
				echo "new google.maps.LatLng(".$coords[$i][Latitude].",".$coords[$i][Longitude]."),";
			}
			echo "new google.maps.LatLng(".$coords[$i][Latitude].",".$coords[$i][Longitude].")";

			echo '];
			
			var flightPath = new google.maps.Polyline({
			  path: flightPlanCoordinates,
			  strokeColor: \'#FF0000\',
			  strokeOpacity: 1.0,
			  strokeWeight: 2
			});

			flightPath.setMap(map);
      }
    </script>
	<style>
	.sb {
		cursor: pointer;
		border: 1px solid #cecece;
		background: #f6f6f6;
		box-shadow: inset 0px 20px 20px #ffffff;
		border-radius: 8px;
		padding: 8px 14px;
		width: 120px;
	}
	.sb:hover {
		box-shadow: inset 0px -20px 20px #ffffff;
	}
	.sb:active {
		margin-top: 1px;
		margin-bottom: -1px;
		zoom: 1;
	}
	</style>
</head>

<body onload="initialize()">
	<div id="map" style="width:600px; height:400px"></div>
	<div id="map-canvas" style="width: 600px; height:400px"></div>
</body>

</html>';
}
else
{
echo
'<html>
<head>
	<style>
	.sb {
		cursor: pointer;
		border: 1px solid #cecece;
		background: #f6f6f6;
		box-shadow: inset 0px 20px 20px #ffffff;
		border-radius: 8px;
		padding: 5px 10px;
		width: 80px;
		height: 30px;
	}
	.sb:hover {
		box-shadow: inset 0px -20px 20px #ffffff;
	}
	.sb:active {
		margin-top: 1px;
		margin-bottom: -1px;
		zoom: 1;
	}
	</style>
</head>
<body>
<div style="border:1px solid #ccc; border-radius:6px; padding:10px 10px 0 10px; top:30%; left:45%; position:absolute; " align = "center"  >
	<form action="./gpstracker.php" method="get">
	<table><tr>
	<td align=center>Login</td><td align=right><input type="text" maxlength="30" name="username" style="margin:5px"></td></tr>
	<tr><td align=center>Password</td><td align=right><input type="password" maxlength="30" name="psw" style="margin:5px"></td></tr>
	</table>
	<input type="submit" value="OK" class="sb">
	</form>
</div>';
}
?>

</body>

</html>
