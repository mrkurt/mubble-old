<%@ Page Language="C#" %>
<%@ OutputCache Duration="9000" VaryByParam="url;zone;tags" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected string AdSection = "main";
    protected string Zone = "728";

    ///guides/buyer/holiday-gift-guide-2008
    protected void Page_Load(object sender, EventArgs e)
    {
        string url = Request.QueryString["url"];
        this.Zone = Request.QueryString["zone"];

        System.Collections.Generic.List<string> tags = 
            new System.Collections.Generic.List<string>(
            (Request.QueryString["tags"] ?? "").Split(',')
            );
/*

Business IT = main/businessit
Gaming = main/gaming
Microsoft = main/microsoft
Linux = main/linux

*/
        if (!string.IsNullOrEmpty(url))
        {
            if(url.Contains("guides/buyer/holiday-gift-guide-2008")){
				this.AdSection = "gift_guide";
			}
			else if(url.Contains("guides/buyer/ultimate-guides-2008") || url.Contains("guides/buyer/guide-200812-htpc")){
				this.AdSection = "ars_ultimates";
			}
			else if(url.Contains("articles/culture/ars-awards-2008")){
				this.AdSection = "ars_awards";
			}
			else if(url.Contains("guides/buyer/ultimate-road-warrior-guide-2008")){
				this.AdSection = "road_warrior";
			}
			else if(url.Contains("guides") || tags.Contains("itopia")){
				this.AdSection = "main/guides_itopia";
			}
            else if (tags.Contains("business") || tags.Contains("ftit")){
				this.AdSection = "bizitarticle";
			}
			else if(url.Contains("business"))
            {
                this.AdSection = "main/businessit";
            }
            else if (url.Contains("apple"))
            {
                this.AdSection = "main/apple";
            }
            else if (url.Contains("journals/hardware"))
            {
                this.AdSection = "main/hardware/journal";
            }
            else if (url.Contains("hardware"))
            {
                this.AdSection = "main/hardware/news";
            }
            else if (url.Contains("gadgets"))
            {
                this.AdSection = "main/gadgets";
            }
            else if (url.Contains("index"))
            {
                this.AdSection = "main/frontpage";
            }
            else if (url.Contains("microsoft"))
            {
                this.AdSection = "main/microsoft";
            }
            else if (url.Contains("gaming") || url.Contains("thumbs"))
            {
                this.AdSection = "main/gaming";
            }
            else if (url.Contains("linux"))
            {
                this.AdSection = "main/linux";
            }
            else if (url.Contains("security"))
            {
                this.AdSection = "main/security";
            }
            else if (url.Contains("news"))
            {
                this.AdSection = "main/news";
            }
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title></title>
		<style type="text/css">
			* {
				margin: 0;
				padding: 0;
			}

			html {
				background-color: #9d9a95;
			}

			body {
				background: transparent url('http://media.arstechnica.com/ars.static/images/banner-ad-back.v2.gif') no-repeat left top;
				height: 90px;
				width: 728px;
			}
			
			body.Zone730, body.Zone1405, body.Zone1676, body.Zone1919 {
				background: transparent url('http://media.arstechnica.com/ars.static/images/panel-ad-back.v2.gif') no-repeat left top;
				height: 280px;
				width: 336px;
			}
			
			body.Zone1920
			{
				background: transparent url('http://media.arstechnica.com/ars.static/images/wide-banner-ad-back.v2.gif') no-repeat left top; 
				height: 90px;
				width: 970px;
			}
			
			body.Zone1676, body.Zone1919
			{
				background: transparent url('http://media.arstechnica.com/ars.static/images/tall-panel-ad-back.gif') no-repeat left top;
				height: 630px;
				width: 336px;
			}
			
			body.Zone721 {
				background: transparent url('http://media.arstechnica.com/ars.static/images/tower-ad-back-small.v2.gif') no-repeat left top;
				height: 600px;
				width: 160px;
			}

			table {
				border: 0;
				border-collapse: collapse;
				border-spacing: 0;
			}

			td {
				height: 90px;
				text-align: center;
				vertical-align: middle;
				width: 728px;
			}
			body.Zone730 td, body.Zone1405 td {
				height: 280px;
				width: 336px;
			}
			
			body.Zone1676 td, body.Zone1919 td
			{
				height: 630px;
			}
			
			body.Zone721 td {
				height: 600px;
				width: 160px;
			}
		</style>
	</head>
	<body class="Zone<%=this.Zone %>">
		<table>
			<tr>
				<td>
					<script type="text/javascript" charset="utf-8">
					  federated_media_section = "<%= this.AdSection %>";
					</script>

					<!-- FM Leaderboard v2 Zone -->
					<script type='text/javascript' src='http://static.fmpub.net/zone/<%= this.Zone %>'></script>
					<!-- FM Leaderboard v2 Zone -->
				</td>
			</tr>
		</table>
	</body>
</html>