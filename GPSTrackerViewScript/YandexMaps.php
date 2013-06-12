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
	
	    <!-- Fav and touch icons -->
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="./files/img/iconmonstr-map-6-icon.png">
      <link rel="apple-touch-icon-precomposed" sizes="72x72" href="./files/img/iconmonstr-map-6-icon.png">
                    <link rel="apple-touch-icon-precomposed" href="./files/img/iconmonstr-map-6-icon.png">
                                   <link rel="shortcut icon" href="./files/img/iconmonstr-map-6-icon.png">
								   
	
    <script src="http://api-maps.yandex.ru/2.0/?load=package.full&lang=ru-RU"
            type="text/javascript"></script>
    <script type="text/javascript">
        // Как только будет загружен API и готов DOM, выполняем инициализацию
        ymaps.ready(init);

        function init () {
            var myMap = new ymaps.Map('map', {
                    center: [<? echo $coords[key($coords)][0]['Latitude'].",".$coords[key($coords)][0]['Longitude'];?>],
                    zoom: 17
                });

            // Для добавления элемента управления на карту
            // используется поле controls, ссылающееся на
            // коллекцию элементов управления картой.
            // Добавление элемента в коллекцию производится
            // с помощью метода add().

            // В метод add можно передать строковый идентификатор
            // элемента управления и его параметры.
            myMap.controls
                // Кнопка изменения масштаба
                .add('zoomControl')
                // Список типов карты
                .add('typeSelector')
                // Кнопка изменения масштаба - компактный вариант
                // Расположим её справа
                .add('smallZoomControl', { right: 5, top: 75 })
                // Стандартный набор кнопок
                .add('mapTools');

            // Также в метод add можно передать экземпляр класса, реализующего определенный элемент управления.
            // Например, линейка масштаба ('scaleLine')
            myMap.controls
                .add(new ymaps.control.ScaleLine())
                // В конструкторе элемента управления можно задавать расширенные
                // параметры, например, тип карты в обзорной карте
                .add(new ymaps.control.MiniMap({
                    type: 'yandex#publicMap'
                }));

			<?
			foreach($coords as $key => $val)
			{
				echo "				

				var geometry = [";
				$ArraySize = sizeof($val)-1;
				for($i=0; $i<$ArraySize; $i++)
				{
					echo "[".$val[$i]['Latitude'].", ".$val[$i]['Longitude']."],";
				
				//[55.80, 37.30],[55.80, 37.40],[55.70, 37.30],[55.70, 37.40],[55.74, 37.60],[55.77, 37.50]
				}
				echo "[".$val[$ArraySize]['Latitude'].", ".$val[$ArraySize]['Longitude']."]],
	 
				properties = {
					hintContent: \"".$key."\"
				},
				
				options = {
					draggable: false,
					strokeColor: '#00ff00',
					strokeWidth: 2
				},
				
				polyline = new ymaps.Polyline(geometry, properties, options);
				myMap.geoObjects.add(polyline);	
				";
			} ?>
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
			<div id="map"></div>
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
