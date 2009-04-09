<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DirectionsForum.ascx.cs" Inherits="Templates_ars_DirectionsForum"  %>
    <if:Path StartsWith="!/site" runat='server'>
    <div class="Bubble OpenForum Dynamic">
        <h2><a href="#" onclick="window.location='http://dynamic.fmpub.net/adserver/adclick.php?bannerid=26202&zoneid=0&dest=http://dynamic.fmpub.net/adserver/adclick.php?bannerid=26202&zoneid=0&dest=http%3A%2F%2Fepisteme.arstechnica.com%2Feve%2Fforums%2Fa%2Ffrm%2Ff%2F901006821931';return false;"><html:Image runat="server" src="images/directions-forum.gif" alt="OpenForum" /></a></h2>
        <p style="color: White;"><span style="color: #e63f0c">Current Hot Topic:</span><br />The Energy-Efficient Datacenter</p>
        <ul class="Headlines" runat="server" id="Links">
        </ul>
        <p style="text-align: center;">
                <script type="text/javascript">
	        document.write('<img src="http://dynamic.fmpub.net/adserver/adlog.php?bannerid=26202&clientid=0&zoneid=0&cb=' + Math.floor(Math.random() * 1000) + '" width="0" height="0" alt="" style="width: 0px; height:0px; margin: 0px; padding: 0px; position: absolute;" />');
	    </script>
	    <a href="#" onclick="window.location='http://dynamic.fmpub.net/adserver/adclick.php?bannerid=26202&zoneid=0&dest=http://dynamic.fmpub.net/adserver/adclick.php?bannerid=26202&zoneid=0&dest=http%3A%2F%2Fepisteme.arstechnica.com%2Feve%2Fforums%2Fa%2Ffrm%2Ff%2F901006821931'; return false;"><html:Image  runat="server" src="images/directions-badge.gif" alt="OpenForum" /></a>
	    </p>
    </div>
    </if:Path>