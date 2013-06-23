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
			//echo "var ".$key."PathColor='".$_COOKIE[$key."_color"]."';\n";
			echo "				
			var ".$Key."_geometry = [";
			echo "[".$val[0]['Latitude'].", ".$val[0]['Longitude']."]";
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

					echo "],\n
					properties = {
					hintContent: \"".$key."\"
					},
					options = {
					draggable: false,
					strokeColor: '".((isset($_COOKIE[$key."_color"]) && $_COOKIE[$key."_color"] != "")?$_COOKIE[$key."_color"]:'#FF0000')."',
					strokeWidth: 2
					},
				
					".$Key."_polyline = new ymaps.Polyline(".$Key."_geometry, properties, options);
					myMap.geoObjects.add(".$Key."_polyline);	
					
					var ".$Key."_geometry = [";
					echo "[".$val[$i]['Latitude'].", ".$val[$i]['Longitude']."]";
				}
				else {
				echo ",\n\t\t [".$val[$i]['Latitude'].", ".$val[$i]['Longitude']."]";}
				$time1 = $time2;
			}
			echo ",\n\t\t [".$val[$ArraySize]['Latitude'].", ".$val[$ArraySize]['Longitude']."]";
			echo "],\n
			properties = {
			hintContent: \"".$key."\"
			},
			options = {
			draggable: false,
			strokeColor: '".((isset($_COOKIE[$key."_color"]) && $_COOKIE[$key."_color"] != "")?$_COOKIE[$key."_color"]:'#FF0000')."',
			strokeWidth: 2
			},
		
			".$Key."_polyline = new ymaps.Polyline(".$Key."_geometry, properties, options);";

			$CurrentPosition[$key] = end($coords[$key]);
			echo "
			// Создаем геообъект с типом геометрии Точка.
			".$Key."GeoObject = new ymaps.GeoObject({
            // Описание геометрии.
            geometry: {
                type: \"Point\",
                coordinates: [".$CurrentPosition[$key]['Latitude'].",".$CurrentPosition[$key]['Longitude']."]
				},
				// Свойства.
				properties: {
					// Контент метки.
					iconContent: '".$key."',
					balloonContent: '".$key.'\r\n'."Проделанный путь: ".round($PathLength,3)." метров".'\r\n'."Скорость: ".Round($CurrentPosition[$key]['Speed']/1.852,3).' км/час\r\n'."Долгота:".$CurrentPosition[$key]['Latitude'].'\r\n'."Широта:".$CurrentPosition[$key]['Longitude'].'\r\n'."Дата:".$CurrentPosition[$key]['Time']."'
				}
			}, 
			{
            // Опции.
            // Иконка метки будет растягиваться под размер ее содержимого.
            preset: 'twirl#redStretchyIcon',
            draggable: false
			}),
			myMap.geoObjects
			.add(".$Key."_polyline)
			.add(".$Key."GeoObject);
			";
			}
			?>
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
					$CurrentPosition = array_pop($coords[$key]);
					echo "<tr id=".$key.">\n
						<td class=\"usrname\" onclick=\"desu(".$CurrentPosition[0].",".$CurrentPosition[1].")\">".$key."</td>\n
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
		</div>		<div class="foot" align="left"><div style="position:relative;"> <p class="MyCopyright">&copy Masalin A. 2013</p></div></div>
	</div>
</body>
</html>
