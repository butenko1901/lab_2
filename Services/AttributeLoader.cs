using System.Xml.Linq;

namespace ScientistsLab.Services
{
    public class AttributeLoader
    {
        public List<string> GetValues(string filePath, string attributeName)
        {
            XDocument doc = XDocument.Load(filePath);

            List<string> values = doc.Descendants("Scientist")
                .Select(x => (string?)x.Attribute(attributeName))
                .Where(v => !string.IsNullOrEmpty(v))
                .Select(v => v!)
                .Distinct()
                .OrderBy(v => v)
                .ToList();

            return values;
        }
    }
}
