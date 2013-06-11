<?
// запускаем сессию
session_start();
// функция удаляет пустые элементы массива
function ArrayTrim(&$a)
{
	foreach($a as $k => $v)
	if ($v=='') unset($a[$k]);
	return $a;
}

// проверяем залогинился ли пользователь
if(!(isset($_SESSION['username']) && ($_COOKIE['auth'] == "1"))){session_destroy(); Die("Авторизуйтесь!");}
require_once("./files/DB/DBConnection.php");
$username = $_SESSION['username'];

// получаем друзей пользователя, друзья в данном случае - те пользователи чьи маршруты можно просматривать и для каждого из них получаем координаты перемещения
$query = @mysql_query("SELECT Friends FROM `Users` WHERE UserName = '".$username."'") or die (mysql_error());
$friendsStr = @mysql_fetch_array($query);
$friends = ArrayTrim(explode(";",$friendsStr[0]));
array_unshift($friends, $username);
for($i=0;$i<count($friends);$i++)
{
	if (isset($_COOKIE[$friends[$i]+'_limit'])) {$limit = $_COOKIE[$friends[$i]+'_limit'];} else {$limit=10;}
	$query = @mysql_query("SELECT Latitude, Longitude FROM `$friends[$i]` ORDER BY `Time` desc LIMIT $limit") or die (mysql_error());

	while($result = @mysql_fetch_array($query))
	{ 				
		//print_r($result);
		$coords[$friends[$i]][] = $result;
	}
}
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
	  var color = '#FFFF00';
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
		echo "var ".$key."flightPlanCoordinates = [\n";
		$ArraySize = sizeof($val)-1;
		for($i=0; $i<$ArraySize; $i++)
		{
			echo "\t\tnew google.maps.LatLng(".$val[$i]['Latitude'].",".$val[$i]['Longitude']."),\n";
		}
		echo "\t\tnew google.maps.LatLng(".$val[$ArraySize]['Latitude'].",".$val[$ArraySize]['Longitude'].")];\n";
        echo "\t\tvar ".$key."flightPath = new google.maps.Polyline({
          path: ".$key."flightPlanCoordinates,
          strokeColor: color,
          strokeOpacity: 1.0,
          strokeWeight: 2
        });
		
        ".$key."flightPath.setMap(map);";
		}
		?>
		
		
      }
	  $(function(){
		$('.enable').click(function(e){ $(e.target).children().click();});
		$('.size').styleddropdown();
	  });
	  function desu()
	  {
	   map.setCenter(new google.maps.LatLng(37.4569, 22.1569));
	  }
	   function d2(rowName)
	   {
			 $('.removeCB')
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
			<div class="addUser">
				<form class="navbar-container" action="test.php" method="POST">
					<table>
						<tr><th>Добавить пользователя</th></tr>
						<tr>
							<td class="usrmname">
								<input type="text" name="usr">
							</td>
							<td class="points">
								<select name="points">
									<option>10</option>
									<option>20</option>
									<option>30</option>
								</select>
							</td>
							<td class="color">
								<select name="color">
									<option>red</option>
									<option>green</option>
									<option>blue</option>
								</select>
							</td>
							<td class="enable">
								<input type="submit" value="Добавить">
							</td>
						</tr>
					</table>
				</form>
			</div>
			<div class="Client">
			<form class="navbar-container">
				<table class="users white-background">
				<tr><th>user</th><th>points</th><th>Цвет</th><th>Удалить</th></tr>
				<?php  
				// тут генерируется табличка с пользователями
				foreach($coords as $key => $val)
				{
					echo "<tr id=".$key.">\n
						<td class=\"usrname\" onclick=\"desu()\">".$key."</td>\n
						<td class=\"points\">\n
							<select name=".$key."['points']>\n
								<option>10</option>\n
								<option>20</option>\n
								<option>30</option>\n
							</select>\n
						</td>\n
						<td class=\"color\">\n
							<select name=".$key."['color']>\n
								<option>red</option>\n
								<option>green</option>\n
								<option>blue</option>\n
							</select>\n
						</td>\n
						<td class=\"enable\">\n
							<input type=\"checkbox\" name=".$key."['enable'] class=\"removeCB\">\n
						</td>\n
					</tr>\n";
					}
					?>
				</table>
			</form>
			</div>
		</div>
		<div class="foot" align="left"><div style="position:relative;"> <p class="MyCopyright">&copy Masalin A. 2013</p></div></div>
	</div>
</body>
</html>
