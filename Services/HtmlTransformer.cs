using System.Xml.Xsl;

namespace ScientistsLab.Services
{
    // Трансформація XML -> HTML за допомогою XSL-файлу
    public class HtmlTransformer
    {
        public void Transform(string xmlPath, string xslPath, string htmlPath)
        {
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xslPath);
            xslt.Transform(xmlPath, htmlPath);
        }
    }
}
