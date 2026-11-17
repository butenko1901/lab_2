using System.Xml;
using ScientistsLab.Models;

namespace ScientistsLab.Strategies
{
    public class DomSearchStrategy : ISearchStrategy
    {
        public List<Scientist> Search(string filePath, SearchFilter filter)
        {
            List<Scientist> result = new List<Scientist>();

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            string xpath = BuildXPath(filter);
            XmlNodeList? nodes = doc.SelectNodes(xpath);
            if (nodes == null)
                return result;

            foreach (XmlNode node in nodes)
            {
                Scientist s = new Scientist();
                s.FullName = GetAttr(node, "FullName");
                s.Faculty = GetAttr(node, "Faculty");
                s.Department = GetAttr(node, "Department");
                s.Degree = GetAttr(node, "Degree");
                s.Title = GetAttr(node, "Title");

                XmlNode? degreeInfo = node.SelectSingleNode("DegreeInfo");
                if (degreeInfo != null)
                {
                    s.Speciality = GetAttr(degreeInfo, "Speciality");
                    s.DegreeDate = GetAttr(degreeInfo, "Date");
                }

                XmlNode? titleInfo = node.SelectSingleNode("TitleInfo");
                if (titleInfo != null)
                {
                    s.TitleDate = GetAttr(titleInfo, "Date");
                }

                // Ключове слово в П.І.П. перевіряємо тут (без урахування регістру)
                if (filter.IsMatch(s))
                    result.Add(s);
            }

            return result;
        }
        private string BuildXPath(SearchFilter filter)
        {
            List<string> conditions = new List<string>();

            if (filter.Faculty != "")
                conditions.Add("@Faculty=\"" + filter.Faculty + "\"");
            if (filter.Department != "")
                conditions.Add("@Department=\"" + filter.Department + "\"");
            if (filter.Degree != "")
                conditions.Add("@Degree=\"" + filter.Degree + "\"");
            if (filter.Title != "")
                conditions.Add("@Title=\"" + filter.Title + "\"");

            if (conditions.Count == 0)
                return "//Scientist";

            return "//Scientist[" + string.Join(" and ", conditions) + "]";
        }

        private string GetAttr(XmlNode node, string name)
        {
            if (node.Attributes == null || node.Attributes[name] == null)
                return "";
            return node.Attributes[name]!.Value;
        }
    }
}
