<?php include("database.php");$action=$_POST['action'];if($action=='update'){	$roomName=addslashes($_POST['roomName']);	$sql="insert into tblChatMaster (roomName,started,startedBy) values ('$roomName','".mktime()."','1')";	$result=db_query($sql);	$sql="insert into t (roomName,started,startedBy) values ('$roomName','".mktime()."','1')";	$result=db_query($sql);	$next=mysql_insert_id();	$_SESSION['chatsessID']=$next;	$_SESSION['roomName']=$roomName;	header("location:chat.php");}?><!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN"><HTML> <HEAD><meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />  <TITLE>RRsat Chat for Live Events</TITLE><script language="javascript">function submitPost(){	var txt=document.chattext.chattext.value;	$('#util').load('inc/post.php?txt='+txt);	$('#chatstatus').load('inc/chatstatus.php');	$('#chattext').load('inc/chattext.php');    $('#roomstatus').load('inc/room_participants.php','slow');	$("#chatstatus").attr({ scrollTop: $("#chatstatus").attr("scrollHeight") });}</script><script language="JavaScript" src="inc/functions.js"></script><script type="text/javascript">  var _gaq = _gaq || [];  _gaq.push(['_setAccount', 'UA-18179487-1']);  _gaq.push(['_trackPageview']);  (function() {    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);  })();</script></head> <BODY><?php $act=$_GET['act'];if(!$act){	$act="chat";} switch($act){	 case "new":?><form method="post" action="<?php echo($_SERVER['PHP_SELF']);?>"><input type="hidden" name="action" value="update">Give your chat a name:&nbsp;<input type="text" name="roomName" size="30">&nbsp;<input type="submit" value="Go!"></form><?php break;?><?php case "existing":?><?php	#baselins is NOW minus 2 hours	$baseline=mktime()-7200;	$sql="select distinct tblChatDetail.chatsessID,tblChatMaster.roomName,tblChatMaster.chatsessID from tblChatMaster LEFT OUTER JOIN tblChatDetail on tblChatDetail.chatsessID=tblChatMaster.chatsessID where stamp>='$baseline' order by chattextID desc";	$result=db_query($sql);	$count=mysql_num_rows($result);	If($count<1){		print("No active chats.<br />");		print("<a href=\"chat.php?act=new\">Start a new chat</a><br />");	}else{		print("<strong>Active Chats:</strong><br />\n");		while($row=mysql_fetch_array($result)){			$chatsessID=$row['chatsessID'];			$roomName=stripslashes($row['roomName']);			print("".$roomName." - <a href=\"switch.php?act=".$chatsessID."&room=".urlencode($roomName)."\">Enter this room</a><br />");		}	}?><?php break;?><?php case "chat":?><table border="1" width="1000">	<tr valign="top">		<td colspan="2">			<div id="announce">You are in the chat: <?php echo($_SESSION['roomName']);?></div>		</td>	</tr>	<tr valign="top">		<td width="800" valign="top">			<div id="chatstatus" style="height:500px;overflow:auto;"></div>		</td>		<td width="200" valign="top"><strong>Participants:</strong><br />			<div id="roomstatus"></div>		</td>	</tr>	<tr valign="top">		<td colspan="2" align="left">			<div id="chattext"><strong>Enter Your Text:</strong>&nbsp;</div>		</td>	</tr></table><?php break; } ?><script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script><script type="text/javascript">		$(document).ready(function(){ 			$('#roomstatus').load('inc/room_participants.php');			$('#chatstatus').load('inc/chatstatus.php');			$('#chattext').load('inc/chattext.php');		});	</script><script type="text/javascript">var auto_refresh = setInterval(function (){   $('#roomstatus').load('inc/room_participants.php','slow');   $('#chatstatus').load('inc/chatstatus.php','slow');}, 10000); // refresh every 10000 milliseconds</script><div id="util" style="display:none;"></div> </BODY></HTML>