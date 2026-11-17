namespace ScientistsLab.Models
{
    public class Scientist
    {
        public string FullName { get; set; } = "";
        public string Faculty { get; set; } = "";
        public string Department { get; set; } = "";
        public string Degree { get; set; } = "";
        public string Speciality { get; set; } = "";
        public string DegreeDate { get; set; } = "";
        public string Title { get; set; } = "";
        public string TitleDate { get; set; } = "";

        public override string ToString()
        {
            string text = "П.І.П.: " + FullName + "\n";
            text += "Факультет: " + Faculty + "\n";
            text += "Кафедра: " + Department + "\n";

            if (Degree != "")
                text += "Науковий ступінь: " + Degree + " (" + Speciality + ", " + DegreeDate + ")\n";
            else
                text += "Науковий ступінь: немає\n";

            if (Title != "")
                text += "Вчене звання: " + Title + " (" + TitleDate + ")\n";
            else
                text += "Вчене звання: немає\n";

            return text;
        }
    }
}
