<?php include("database.php");?><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"><html xmlns="http://www.w3.org/1999/xhtml"><head>    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />    <title>RRsat Chat for Live Events</title>    <link href="assets/sm2_Styles.css" rel="stylesheet" type="text/css" />    <link href="assets/sm2_Menu.css" rel="stylesheet" type="text/css" />    <link href="assets/colorbox.css" rel="stylesheet" type="text/css" />        <script type="text/javascript" src="assets/jquery-latest.min.js" ></script>	<script type="text/javascript" src="assets/jquery.colorbox-min.js"></script>    <script>		$(document).ready(function(){			$(".bioSusanna_link").colorbox({width:"50%", inline:true, href:"#bioSusanna"});			$(".bioPhilip_link").colorbox({width:"50%", inline:true, href:"#bioPhilip"});		});	</script>    <script language="JavaScript" src="inc/functions.js"></script><script type="text/javascript">  var _gaq = _gaq || [];  _gaq.push(['_setAccount', 'UA-18179487-1']);  _gaq.push(['_trackPageview']);  (function() {    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);  })();</script></head><body><?php include('header.php'); ?><div id="content">            <div id="textScroll"><div style="clear:both; position:relative; width:495px; left:10px;">            <p>					<form id="login_large" name="login_large" method="post" action="do_login.php" >			<input name="name" type="text" id="name" style="width:135px; " value="username (email)" onClick="this.value='';" />			<br />			<input name="password" type="password" id="password" style="width:135px;" value="password" onClick="this.value='';" />			<br />			<div style="margin-top:5px;">				<a href="forgot.php" class="nine">Forgot your password?</a>&nbsp;&nbsp;<br />				<a href="javascript:;" class="smRoll" onClick="document.login_large.submit();"><img src="layout-images/header/signIn.png"width="67" height="28" / style="vertical-align:middle;"></a>						</div>		</form>		</p>            </div>            </div>        </div></div><?php include('footer.php'); ?></body></html>