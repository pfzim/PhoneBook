<?xml version="1.0" encoding="windows-1251"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
   <xsl:template match="/">
	<html>
	  <body>
		<h2>Header</h2>
		 <xsl:apply-templates />
		 </body>
	  </html>
   </xsl:template>
   <xsl:template match="/phone">
			<h2>Phonebook</h2>
			<table border="1">
			   <tr>
				  <td>Имя</td>
				  <td>Телефон</td>
				  <td>Мобильный</td>
				  <td>E-Mail</td>
			   </tr>
				  <xsl:apply-templates />
			</table>
   </xsl:template>
   <xsl:template match="/DataSet1/TelSprav">
	  <tr>
	  <td><b><xsl:value-of select="fio"/></b></td>
	  <td><xsl:value-of select="dolgn_name"/></td>
	  <td><xsl:value-of select="depart_name"/></td>
	  <td><xsl:value-of select="gorod"/></td>
	  <td><xsl:value-of select="sotov"/></td>
	  <td><xsl:value-of select="E_mail"/></td>
	  </tr>
   </xsl:template>
</xsl:stylesheet>
