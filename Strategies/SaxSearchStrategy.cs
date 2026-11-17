using System.Xml;
using ScientistsLab.Models;

namespace ScientistsLab.Strategies
{
    // SAX API. У C# аналог SAX - це XmlReader:
    // він читає файл послідовно, вузол за вузлом, не завантажуючи весь файл у пам'ять.
    public class SaxSearchStrategy : ISearchStrategy
    {
        public List<Scientist> Search(string filePath, SearchFilter filter)
        {
            List<Scientist> result = new List<Scientist>();
            Scientist? current = null;

            using (XmlReader reader = XmlReader.Create(filePath))
            {
                while (reader.Read())
                {
                    // Початок елемента <...>
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "Scientist")
                        {
                            current = new Scientist();
                            current.FullName = reader.GetAttribute("FullName") ?? "";
                            current.Faculty = reader.GetAttribute("Faculty") ?? "";
                            current.Department = reader.GetAttribute("Department") ?? "";
                            current.Degree = reader.GetAttribute("Degree") ?? "";
                            current.Title = reader.GetAttribute("Title") ?? "";

                            // Порожній елемент <Scientist ... /> не має закриваючого тегу
                            if (reader.IsEmptyElement)
                            {
                                if (filter.IsMatch(current))
                                    result.Add(current);
                                current = null;
                            }
                        }
                        else if (reader.Name == "DegreeInfo" && current != null)
                        {
                            current.Speciality = reader.GetAttribute("Speciality") ?? "";
                            current.DegreeDate = reader.GetAttribute("Date") ?? "";
                        }
                        else if (reader.Name == "TitleInfo" && current != null)
                        {
                            current.TitleDate = reader.GetAttribute("Date") ?? "";
                        }
                    }
                    // Кінець елемента </Scientist>
                    else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Scientist")
                    {
                        if (current != null && filter.IsMatch(current))
                            result.Add(current);
                        current = null;
                    }
                }
            }

            return result;
        }
    }
}
