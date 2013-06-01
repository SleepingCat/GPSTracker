<!--?
session_start();
if(!(isset($_SESSION['username']) && ($_COOKIE['auth'] == "1"))){session_destroy(); Die("Авторизуйтесь!");}
require_once("./files/DB/DBConnection.php");
$username = $_SESSION['username'];
$query = @mysql_query("SELECT Friends FROM `Users` WHERE UserName = '".$username."'") or die (mysql_error());

$friends = @mysql_fetch_array($query);
while($result = @mysql_fetch_array($query))
{ 		
	$query = @mysql_query("SELECT Latitude, Longitude FROM `$username` ORDER BY `Time` desc LIMIT 10") or die (mysql_error());

	while($result = @mysql_fetch_array($query))
	{ 				
		$coords[] = $result;
	}
}
?>
-->