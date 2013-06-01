<?
session_start();
function ArrayTrim(&$a)
{
	foreach($a as $k => $v)
	if ($v=='') unset($a[$k]);
	return $a;
}
if(!(isset($_SESSION['username']) && ($_COOKIE['auth'] == "1"))){session_destroy(); Die("Авторизуйтесь!");}
require_once("./files/DB/DBConnection.php");
$username = $_SESSION['username'];

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
//$_SESSION['coords'] = $coords;
//echo "<pre>";
//print_r($coords);
//echo "</pre>";
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
	
	<script src="http://code.jquery.com/jquery.js"></script>
	<script src="./files/js/jquery.cookie.js"></script>
	<script src="./files/js/myJs.js"></script>
	
    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false"></script>
    <script>
      var map;
	  var color = '#FFFF00';
      function initialize() {
		<?php
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
	  //function d2(var rowName){}
    </script>
	
	<style>
		.top 
		{
			height: 50px;
			background: #ccc; /* Для старых браузров */
			background: -moz-linear-gradient(top, #fff, #aaa); /* Firefox 3.6+ */
			/* Chrome 1-9, Safari 4-5 */
			background: -webkit-gradient(linear, left top, left bottom, 
						color-stop(0%,#fff), color-stop(100%,#aaa));
			/* Chrome 10+, Safari 5.1+ */
			background: -webkit-linear-gradient(top, #fff, #aaa);
			background: -o-linear-gradient(top, #fff, #aaa); /* Opera 11.10+ */
			background: -ms-linear-gradient(top, #fff, #aaa); /* IE10 */
			background: linear-gradient(top, #fff, #aaa); /* CSS3 */ 
			border-bottom: 1px solid #333;

		}
		.column
		{
			position:absolute;
			bottom:51px;
			top:51px;
		}
		.map 
		{
			min-width:600px;
			left:0;
			right:380px;
		}
		.navbar 
		{
			width:380px;
			min-width:380px;
			right:0;
			background: #eee;
		}
		.foot 
		{
			position:absolute;
			height: 50px;
			bottom:0;
			width:100%;
			background:#eee;
			border-top:1px solid #333;
		}
		.navbar-container
		{
			margin:10px;
			padding:5px;
			border:1px solid #ddd;
			border-radius:5px;
		}
		#map-canvas
		{
			min-width:300px;
			min-height:300px;
			width:100%;
			height:100%;
			border-right:1px solid #333;
		}
		#MyMarker
		{
			position:relative;
			Color:#333;
			padding:12px 12px;
			height:auto;
		}
		#MyMarker span
		{
			position:relative;
			top:4px;
		}
		#MyMarker a
		{
			margin:0;
			position:relative;
			border:1px solid #888;
			padding:3px;
			background: #fff; /* Для старых браузров */
			background: -moz-linear-gradient(top, #fff, #ccc); /* Firefox 3.6+ */
			/* Chrome 1-9, Safari 4-5 */
			background: -webkit-gradient(linear, left top, left bottom, 
						color-stop(0%,#fff), color-stop(100%,#ccc));
			/* Chrome 10+, Safari 5.1+ */
			background: -webkit-linear-gradient(top, #fff, #ccc);
			background: -o-linear-gradient(top, #fff, #ccc); /* Opera 11.10+ */
			background: -ms-linear-gradient(top, #fff, #ccc); /* IE10 */
			background: linear-gradient(top, #fff, #ccc); /* CSS3 */ 

			border-radius:5px;
			display:block;
			float:right;
			box-shadow: 2px 3px 0px rgba(0,0,0,0.5); /* Параметры тени */
		}
			#MyMarker a:hover
		{
			left:1px;
			top:1px;
			box-shadow: 1px 2px 0px rgba(0,0,0,0.5); /* Параметры тени */
			cursor:pointer;
		}
		.navbar-container table
		{
			width:100%;
			border-collapse: separate;
			border-spacing:2px 2px;
			border:1px solid #ddd;
		}
		.white-background
		{
			background:#FFF;
		}
		.users tr
		{
			border:1px solid white;
		}
		.users td,.users th
		{
			border:1px solid #ddd;
			background:#eee;
			text-align:center;
		}
	</style>
</head>
<body onload="initialize()">
	<div class="main" align="center">
		<div class="top" align = "right">
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
		<div class="foot">
		</div>
	</div>
</body>
</html>
