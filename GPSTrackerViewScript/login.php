<?
require_once("./files/DB/DBConnection.php");

	$username = htmlspecialchars($_POST["username"]);
	$auth = mysql_fetch_row(mysql_query("SELECT Count(UserName) FROM `Users` WHERE `UserName` = '$username' and `Password` = '".MD5($_POST["psw"])."'"));
	if ($auth[0] != 1)
	{
		die("Auth fail");
	} 
	else 
	{
		session_start();
		$_SESSION['username'] = $username;
		SetCookie("username",$usename);
		SetCookie("auth",1);
		header('Location: '.$_SERVER['HTTP_REFERER']);
	}
?>