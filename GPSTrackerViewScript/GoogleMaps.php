<?
// запускаем сессию
session_start();
// проверяем залогинился ли пользователь
if(!(isset($_SESSION['username']) && ($_COOKIE['auth'] == "1"))){session_destroy(); Die("Авторизуйтесь!");}
$username = $_SESSION['username'];
require_once("./files/DB/DBConnection.php");
require_once("./files/DB/Functions.php");
?>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Карта.</title>
	<meta name="viewport" content="initial-scale=1.0, width=device-width, user-scalable=no">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="description" content="">
    <meta name="author" content="">
	
	<link rel="stylesheet" type="text/css" href="./files/css/reset-min.css">
	<link rel="stylesheet" type="text/css" href="./files/css/myMaps.css">
	
	<script src="http://code.jquery.com/jquery.js"></script>
	<script src="./files/js/jquery.cookie.js"></script>
	<script src="./files/js/myJs.js"></script>

    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false"></script>
	
	    <!-- Fav and touch icons -->
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="./files/img/iconmonstr-map-6-icon.png">
      <link rel="apple-touch-icon-precomposed" sizes="72x72" href="./files/img/iconmonstr-map-6-icon.png">
                    <link rel="apple-touch-icon-precomposed" href="./files/img/iconmonstr-map-6-icon.png">
                                   <link rel="shortcut icon" href="./files/img/iconmonstr-map-6-icon.png">
    <script>
      var map;
	  //var color = '#FFFF00';
      function initialize() {
		<?php
		// генерируем карту и линию по координатам
        echo "\n\t\tvar myLatLng = new google.maps.LatLng(".$coords[key($coords)][0]['Latitude'].",".$coords[key($coords)][0]['Longitude'].")\n";
        echo "\t\t var mapOptions = {
          zoom: 17,
          center: myLatLng,
          mapTypeId: google.maps.MapTypeId.HYBRID
        };
		map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
		\n";

		foreach($coords as $key => $val)
		{
			echo "var ".$key."PathColor='".$_COOKIE[$key."_color"]."';\n";
			echo "var ".$key."flightPlanCoordinates = [\n";
			echo "\t\tnew google.maps.LatLng(".$val[0]['Latitude'].",".$val[0]['Longitude'].")";
			$ArraySize = sizeof($val)-1;
			$TimeLimit = 60 * 5;
			$time1 = strtotime($val[0][2]);
			$PathLength = 0;
			$Point1['Latitude'] = $val[0]['Latitude'];
			$Point1['Longitude'] = $val[0]['Longitude'];


			for($i=0; $i<$ArraySize; $i++)
			{

				$time2 = strtotime($val[$i][2]);
				$PathLength += latlng2distance($Point1['Latitude'],$Point1['Longitude'],$val[$i]['Latitude'],$val[$i]['Longitude']);
				if (($time2 - $time1) > $TimeLimit)
				{

					echo "];\n";
					echo "\t\tvar ".$key."flightPath = new google.maps.Polyline({
					path: ".$key."flightPlanCoordinates,
					strokeColor: '".$_COOKIE[$key."_color"]."',
					strokeOpacity: 1.0,
					strokeWeight: 2
					});
					".$key."flightPath.setMap(map);\n";
					echo "\t\tvar ".$key."flightPlanCoordinates = [";
					echo "new google.maps.LatLng(".$val[$i]['Latitude'].",".$val[$i]['Longitude'].")";
				}
				else {
				echo ",\n\t\tnew google.maps.LatLng(".$val[$i]['Latitude'].",".$val[$i]['Longitude'].")";}
				$time1 = $time2;
			}
			echo ",\n\t\tnew google.maps.LatLng(".$val[$ArraySize]['Latitude'].",".$val[$ArraySize]['Longitude'].")];\n";
			echo "\t\tvar ".$key."flightPath = new google.maps.Polyline({
			  path: ".$key."flightPlanCoordinates,
			  strokeColor: '".$_COOKIE[$key."_color"]."',
			  strokeOpacity: 1.0,
			  strokeWeight: 2
			});
			
			".$key."flightPath.setMap(map);\n";
			$CurrentPosition[$key] = end($coords[$key]);
			echo "var ".$key."_LastPosition = new google.maps.LatLng(".$CurrentPosition[$key]['Latitude'].",".$CurrentPosition[$key]['Longitude'].");\n";
			echo "var ".$key."_marker = new google.maps.Marker({
			position: ".$key."_LastPosition,
			map: map,
			title: '".$key.'\r\n'."Проделанный путь: ".round($PathLength,3)." метров".'\r\n'."Скорость: ".Round($CurrentPosition[$key]['Speed']/1.852,3).' км/час\r\n'."Долгота:".$CurrentPosition[$key]['Latitude'].'\r\n'."Широта:".$CurrentPosition[$key]['Longitude'].'\r\n'."Дата:".$CurrentPosition[$key]['Time']."'
			});";
		}
		?>
      }
	$(function(){
		$('.Lim').change(function(e)
		{
			$.cookie(e.target.name,e.target.value);
		});
		$('.Clr').change(function(e){
			$.cookie(e.target.name,ColorConvert(e.target.value));
			alert(ColorConvert(e.target.value));
		});
	});

	function desu(x,y)
	{
	map.setCenter(new google.maps.LatLng(x,y));
	}

	function ColorConvert(color)
	{
		var hexcolor;
		switch (color){
		case 'red':
		hexcolor = '#FF0000';
		break;
		case 'green':
		hexcolor = '#00FF00';
		break;
		case 'blue':
		hexcolor = '#0000FF';
		break;
		default: 
		hexcolor = '#000000';
		}
		return hexcolor;
	}
    </script>
</head>

<body onload="initialize()">
	<div class="main" align="center">
		<div class="top" align = "right">
			<div id="MyLogo"><a href="./index.html">GPSTracker</a></div>
			<div id="MyMarker">
				<div id="MyAuthForm"></div>
			</div>
			
		</div>
		<div class="map column">
			<div id="map-canvas"></div>
		</div>
		<div class="navbar column">
			<div class="Client">
			<form class="navbar-container" id="OptonForm" >
				<table class="users white-background">
				<tr><th>Пользователь</th><th>Точки</th><th>Цвет</th><!--<th>Удалить</th>--></tr>
				<?php  
				// тут генерируется табличка с пользователями
				// $key - Имя пользователя
				// $CurrentPossition - последняя координата (текущее положение пользователя)
				foreach($coords as $key => $val)
				{
					echo "<tr id=".$key.">\n
						<td class=\"usrname\" onclick=\"desu(".$CurrentPosition[$key]['Latitude'].",".$CurrentPosition[$key]['Longitude'].")\">".$key."</td>\n
						<td class=\"points\">\n
							<select name=".$key."_limit class=\"Lim\">\n
								<option "; if($_COOKIE[$key."_limit"] == 5) {echo 'selected';} echo ">5</option>\n
								<option "; if($_COOKIE[$key."_limit"] == 10) {echo 'selected';} echo ">10</option>\n
								<option "; if($_COOKIE[$key."_limit"] == 25) {echo 'selected';} echo ">25</option>\n
								<option "; if($_COOKIE[$key."_limit"] == 50) {echo 'selected';} echo ">50</option>\n
								<option "; if($_COOKIE[$key."_limit"] == 100) {echo 'selected';} echo ">100</option>\n
								<option "; if($_COOKIE[$key."_limit"] == 250) {echo 'selected';} echo ">250</option>\n
								<option "; if($_COOKIE[$key."_limit"] == 500) {echo 'selected';} echo ">500</option>\n
								<option "; if($_COOKIE[$key."_limit"] == 1000) {echo 'selected';} echo ">1000</option>\n
							</select>\n
						</td>\n
						<td class=\"color\">\n
							<select name=".$key."_color class=\"Clr\">\n
								<option>red</option>\n
								<option>green</option>\n
								<option>blue</option>\n
							</select>\n
						</td>\n
						<!--
						<td class=\"enable\">\n
							<input type=\"checkbox\" name=".$key."['enable'] class=\"removeCB\" onclick=\"d2(".$key.")\">\n
						</td>\n
						-->
					</tr>\n";
				}
				?>
				<tr ><td colspan=2></td><td><input type="submit" value="Применить"></td></tr>
				</table>
			</form>
			</div>
		</div>
		<div class="foot" align="left"><div style="position:relative;"> <p class="MyCopyright">&copy Masalin A. 2013</p></div></div>
	</div>
</body>
</html>