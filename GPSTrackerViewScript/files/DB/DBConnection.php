<?
	$sql_host="192.168.1.8";
	$sql_user="GPSTracker";
	$sql_pass="nanodesu";
	$sql_db="GPSTracker";

	$cserv = @mysql_connect("$sql_host", "$sql_user", "$sql_pass") or die ("Невозможно соединиться с MySQL-сервером.");
	$cbase = @mysql_select_db("$sql_db") or die ("Невозможно соединиться с MySQL-базой.");
?>