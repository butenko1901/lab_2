using System.Xml.Linq;
using ScientistsLab.Models;

namespace ScientistsLab.Strategies
{
    public class LinqSearchStrategy : ISearchStrategy
    {
        public List<Scientist> Search(string filePath, SearchFilter filter)
        {
            XDocument doc = XDocument.Load(filePath);
            IEnumerable<XElement> query = doc.Descendants("Scientist");

            if (filter.FullName != "")
                query = query.Where(x => ((string?)x.Attribute("FullName") ?? "")
                    .Contains(filter.FullName, StringComparison.OrdinalIgnoreCase));
            if (filter.Faculty != "")
                query = query.Where(x => (string?)x.Attribute("Faculty") == filter.Faculty);
            if (filter.Department != "")
                query = query.Where(x => (string?)x.Attribute("Department") == filter.Department);
            if (filter.Degree != "")
                query = query.Where(x => (string?)x.Attribute("Degree") == filter.Degree);
            if (filter.Title != "")
                query = query.Where(x => (string?)x.Attribute("Title") == filter.Title);

            List<Scientist> result = query.Select(x => new Scientist
            {
                FullName = (string?)x.Attribute("FullName") ?? "",
                Faculty = (string?)x.Attribute("Faculty") ?? "",
                Department = (string?)x.Attribute("Department") ?? "",
                Degree = (string?)x.Attribute("Degree") ?? "",
                Title = (string?)x.Attribute("Title") ?? "",
                Speciality = (string?)x.Element("DegreeInfo")?.Attribute("Speciality") ?? "",
                DegreeDate = (string?)x.Element("DegreeInfo")?.Attribute("Date") ?? "",
                TitleDate = (string?)x.Element("TitleInfo")?.Attribute("Date") ?? ""
            }).ToList();

            return result;
        }
    }
}
