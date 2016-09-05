<?xml version="1.0" encoding="windows-1251"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
   <xsl:template match="/">
	<xsl:text disable-output-escaping="yes"><![CDATA[<html>]]></xsl:text>
	  <xsl:text disable-output-escaping="yes"><![CDATA[<body>]]></xsl:text>
		<xsl:text disable-output-escaping="yes"><![CDATA[<h2>Header</h2>]]></xsl:text>
		 <xsl:apply-templates/>
		 <xsl:text disable-output-escaping="yes"><![CDATA[</body>]]></xsl:text>
	  <xsl:text disable-output-escaping="yes"><![CDATA[</html>]]></xsl:text>
   </xsl:template>
   <xsl:template match="/phonebook">
			<xsl:text disable-output-escaping="yes"><![CDATA[<h2>Phonebook</h2>]]></xsl:text>
			<xsl:text disable-output-escaping="yes"><![CDATA[<table border="0">]]></xsl:text>
			   <xsl:text disable-output-escaping="yes"><![CDATA[<tr>]]></xsl:text>
				  <xsl:text disable-output-escaping="yes"><![CDATA[<td>Имя</td>]]></xsl:text>
				  <xsl:text disable-output-escaping="yes"><![CDATA[<td>Телефон</td>]]></xsl:text>
				  <xsl:text disable-output-escaping="yes"><![CDATA[<td>Мобильный</td>]]></xsl:text>
				  <xsl:text disable-output-escaping="yes"><![CDATA[<td>E-Mail</td>]]></xsl:text>
			   <xsl:text disable-output-escaping="yes"><![CDATA[</tr>]]></xsl:text>
				  <xsl:apply-templates/>
			<xsl:text disable-output-escaping="yes"><![CDATA[</table>]]></xsl:text>
   </xsl:template>
   <xsl:template match="/phonebook/contact">
	  <xsl:text disable-output-escaping="yes"><![CDATA[<tr>]]></xsl:text>
	  <xsl:text disable-output-escaping="yes"><![CDATA[<td><b>]]></xsl:text><xsl:value-of select="name"/><xsl:text disable-output-escaping="yes"><![CDATA[</b></td>]]></xsl:text>
	  <xsl:text disable-output-escaping="yes"><![CDATA[<td>]]></xsl:text><xsl:value-of select="phone1"/><xsl:text disable-output-escaping="yes"><![CDATA[</td>]]></xsl:text>
	  <xsl:text disable-output-escaping="yes"><![CDATA[<td>]]></xsl:text><xsl:value-of select="phone2"/><xsl:text disable-output-escaping="yes"><![CDATA[</td>]]></xsl:text>
	  <xsl:text disable-output-escaping="yes"><![CDATA[<td>]]></xsl:text><xsl:value-of select="phone3"/><xsl:text disable-output-escaping="yes"><![CDATA[</td>]]></xsl:text>
	  <xsl:text disable-output-escaping="yes"><![CDATA[<td>]]></xsl:text><xsl:value-of select="mail"/><xsl:text disable-output-escaping="yes"><![CDATA[</td>]]></xsl:text>
	  <xsl:text disable-output-escaping="yes"><![CDATA[</tr>]]></xsl:text>
   </xsl:template>
</xsl:stylesheet>
