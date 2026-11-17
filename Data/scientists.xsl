<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" encoding="utf-8" indent="yes" />

  <xsl:template match="/">
    <html>
      <head>
        <meta charset="utf-8" />
        <title>Кадри науковців (Звання)</title>
        <style>
          body { font-family: Arial, sans-serif; margin: 20px; }
          table { border-collapse: collapse; width: 100%; }
          th, td { border: 1px solid #888; padding: 6px; }
          th { background-color: #d9e2f3; }
        </style>
      </head>
      <body>
        <h2>Кадри науковців (Звання)</h2>
        <table>
          <tr>
            <th>П.І.П.</th>
            <th>Факультет</th>
            <th>Кафедра</th>
            <th>Науковий ступінь</th>
            <th>Спеціальність</th>
            <th>Дата ступеня</th>
            <th>Вчене звання</th>
            <th>Дата звання</th>
          </tr>
          <xsl:for-each select="Scientists/Scientist">
            <tr>
              <td><xsl:value-of select="@FullName" /></td>
              <td><xsl:value-of select="@Faculty" /></td>
              <td><xsl:value-of select="@Department" /></td>
              <td><xsl:value-of select="@Degree" /></td>
              <td><xsl:value-of select="DegreeInfo/@Speciality" /></td>
              <td><xsl:value-of select="DegreeInfo/@Date" /></td>
              <td><xsl:value-of select="@Title" /></td>
              <td><xsl:value-of select="TitleInfo/@Date" /></td>
            </tr>
          </xsl:for-each>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
