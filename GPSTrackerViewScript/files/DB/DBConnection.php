<?
	$sql_host="192.168.1.8";
	$sql_user="GPSTracker";
	$sql_pass="nanodesu";
	$sql_db="GPSTracker";

	$cserv = @mysql_connect("$sql_host", "$sql_user", "$sql_pass") or die ("���������� ����������� � MySQL-��������.");
	$cbase = @mysql_select_db("$sql_db") or die ("���������� ����������� � MySQL-�����.");
?>