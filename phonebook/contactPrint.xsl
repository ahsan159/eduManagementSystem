<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" encoding="utf-8"/>  
  <xsl:variable name="fName"/>
  <xsl:variable name="mName"/>
  <xsl:variable name="lName"/>
  <xsl:template match="/">
    <html>
      <body>
        <xsl:apply-templates/>
      </body>
    </html>
  </xsl:template>
  <xsl:template match="person">
    <p>This is me Printing in person</p>
    <xsl:for-each select="*">
        <h1>
      <xsl:if test="name()='firstName'">
          <xsl:value-of select="."/>
        
      </xsl:if>
      <xsl:if test="name()='midName'">
        
          <xsl:value-of select="."/>          
        
      </xsl:if>
      <xsl:if test="name()='lastName'">
        
          <xsl:value-of select="."/>          
      </xsl:if>
        </h1>
      <xsl:if test="name()='address'">
        <h3>
          <xsl:value-of select="."/>
        </h3>
      </xsl:if>
      <xsl:if test="name()='city'">
        <h3>
          <xsl:value-of select="."/>
        </h3>
      </xsl:if>      
    </xsl:for-each>
    <p>      
    <xsl:value-of select="concat(person/firstName,person/lastName)"/>
    </p>
  </xsl:template>
</xsl:stylesheet>