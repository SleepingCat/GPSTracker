<?
	$sql_host="127.0.0.1";
	$sql_user="GPSTracker";
	$sql_pass="nanodesu";
	$sql_db="GPSTracker";

	$cserv = @mysql_connect("$sql_host", "$sql_user", "$sql_pass") or die ("���������� ����������� � MySQL-��������.");
	$cbase = @mysql_select_db("$sql_db") or die ("���������� ����������� � MySQL-�����.");
?>