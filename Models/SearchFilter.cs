namespace ScientistsLab.Models
{
    public class SearchFilter
    {
        public string FullName { get; set; } = "";  
        public string Faculty { get; set; } = "";
        public string Department { get; set; } = "";
        public string Degree { get; set; } = "";
        public string Title { get; set; } = "";
        public bool IsMatch(Scientist s)
        {
            if (FullName != "" && !s.FullName.Contains(FullName, StringComparison.OrdinalIgnoreCase))
                return false;
            if (Faculty != "" && s.Faculty != Faculty)
                return false;
            if (Department != "" && s.Department != Department)
                return false;
            if (Degree != "" && s.Degree != Degree)
                return false;
            if (Title != "" && s.Title != Title)
                return false;

            return true;
        }
    }
}
