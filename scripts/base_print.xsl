<?xml version="1.0" encoding="windows-1251"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
   <xsl:template match="/">
	<html>
	  <body>
		<h2>���������� ����������</h2>
		 <xsl:apply-templates/>
		 </body>
	  </html>
   </xsl:template>
   <xsl:template match="/phonebook">
			<!--<h2>Phonebook</h2>-->
			<table border="0">
			   <tr>
				  <td><u>���</u></td>
				  <td><u>����������</u></td>
				  <td><u>�������</u></td>
				  <td><u>���������</u></td>
				  <td><u>E-Mail</u></td>
			   </tr>
				  <xsl:apply-templates/>
			</table>
   </xsl:template>
   <xsl:template match="/phonebook/contact">
	  <tr>
	  <td><b><xsl:value-of select="name"/></b></td>
	  <td><xsl:value-of select="phone1"/></td>
	  <td><xsl:value-of select="phone2"/></td>
	  <td><xsl:value-of select="phone3"/></td>
	  <td><xsl:value-of select="mail"/></td>
	  </tr>
   </xsl:template>
</xsl:stylesheet>
