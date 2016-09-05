<?xml version="1.0" encoding="windows-1251"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
   <xsl:template match="/">
	<html>
	  <body>
		<h2>Header</h2>
		<pre>
		 <xsl:apply-templates />
		</pre>
		 </body>
	  </html>
   </xsl:template>
   <xsl:template match="/DataSet1">
			<phone>
				  <xsl:apply-templates />
			</phone>
   </xsl:template>
   <xsl:template match="/DataSet1/TelSprav">
	<contact>
		<name><xsl:value-of select="fio"/></name>
		<organisation>ОАО "ЗарубежЭнергоПроект"</organisation>
		<departament><xsl:value-of select="depart_name"/></departament>
		<position><xsl:value-of select="dolgn_name"/></position>
		<external>8 (4932) <xsl:value-of select="gorod"/></external>
		<cellurar><xsl:value-of select="sotov"/></cellurar>
		<mail><xsl:value-of select="E_mail"/></mail>
	</contact>
   </xsl:template>
</xsl:stylesheet>
