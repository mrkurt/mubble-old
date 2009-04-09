<?xml version="1.0"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:output method="html" />
	<xsl:template match="/page">
		<xsl:text disable-output-escaping="yes">
&lt;!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN"
	"http://www.w3.org/TR/html4/loose.dtd"&gt;
		</xsl:text>
<html>
	<head>
		<title></title>
	</head>
	<body>
		<p id="PageNumber">Page <xsl:value-of select="pages/@current"/> of <xsl:value-of select="pages/@total"/></p>
<xsl:for-each select="content/topic/message[author/user/@name!='JournalBot']">
		<div class="Comment">
			<h4><a href="http://episteme.arstechnica.com/eve/personal?x_myspace_page=profile&amp;u={author/user/@oid}"><xsl:value-of select="author/user/@name"/></a></h4>
			<div class="Body">
				<xsl:value-of select="content/message-body"/>
			</div>
			<div class="Tag"><xsl:value-of select="@date"/> @ <xsl:if test="substring-before(@time, ':') mod 12 = 0">12</xsl:if><xsl:if test="substring-before(@time, ':') mod 12 != 0"><xsl:value-of select="substring-before(@time, ':') mod 12"/></xsl:if>:<xsl:value-of select="substring-after(@time, ':')"/><xsl:if test="substring-before(@time, ':') &lt; 12"> AM</xsl:if><xsl:if test="substring-before(@time, ':') &gt; 11"> PM</xsl:if></div>
		</div>
</xsl:for-each>
	</body>
</html>
	</xsl:template>
</xsl:stylesheet>