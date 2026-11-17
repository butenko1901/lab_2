using ScientistsLab.Models;
using ScientistsLab.Services;
using ScientistsLab.Strategies;

namespace ScientistsLab
{
    public partial class MainPage : ContentPage
    {
        private const string AllItems = "(усі)";

        private string xmlPath = "";
        private Searcher searcher = new Searcher(new SaxSearchStrategy());
        private AttributeLoader loader = new AttributeLoader();
        private HtmlTransformer transformer = new HtmlTransformer();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnOpenFileClicked(object sender, EventArgs e)
        {
            string? path = await PickFile(".xml", "Виберіть XML-файл");
            if (path == null)
                return;

            try
            {
                xmlPath = path;
                FileLabel.Text = "Файл: " + xmlPath;
                LoadPickers();
            }
            catch (Exception ex)
            {
                xmlPath = "";
                FileLabel.Text = "Файл не вибрано";
                await DisplayAlertAsync("Помилка", "Не вдалося прочитати файл: " + ex.Message, "OK");
            }
        }

        private async void OnSearchClicked(object sender, EventArgs e)
        {
            if (xmlPath == "")
            {
                await DisplayAlertAsync("Помилка", "Спочатку відкрийте XML-файл", "OK");
                return;
            }

            try
            {
                searcher.SetStrategy(GetSelectedStrategy());
                List<Scientist> result = searcher.Search(xmlPath, GetFilter());
                ShowResult(result);
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Помилка", ex.Message, "OK");
            }
        }

        private async void OnTransformClicked(object sender, EventArgs e)
        {
            if (xmlPath == "")
            {
                await DisplayAlertAsync("Помилка", "Спочатку відкрийте XML-файл", "OK");
                return;
            }

            string? xslPath = await PickFile(".xsl", "Виберіть XSL-файл");
            if (xslPath == null)
                return;

            try
            {
                string htmlPath = Path.Combine(FileSystem.AppDataDirectory, "scientists.html");
                transformer.Transform(xmlPath, xslPath, htmlPath);

                OutputEditor.Text = "HTML-файл створено:\n" + htmlPath;
                await Launcher.OpenAsync(new OpenFileRequest("HTML", new ReadOnlyFile(htmlPath)));
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Помилка", ex.Message, "OK");
            }
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            NameEntry.Text = "";
            FacultyPicker.SelectedIndex = 0;
            DepartmentPicker.SelectedIndex = 0;
            DegreePicker.SelectedIndex = 0;
            TitlePicker.SelectedIndex = 0;
            SaxRadio.IsChecked = true;
            OutputEditor.Text = "";
        }

        private async void OnExitClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlertAsync("Вихід",
                "Чи дійсно ви хочете завершити роботу з програмою?", "Так", "Ні");
            if (answer)
                Application.Current?.Quit();
        }

        private async Task<string?> PickFile(string extension, string title)
        {
            var fileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.WinUI, new[] { extension } },
                { DevicePlatform.Android, new[] { "*/*" } },
                { DevicePlatform.MacCatalyst, new[] { "public.xml", "public.data" } },
            });

            FileResult? file = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = title,
                FileTypes = fileType
            });

            return file?.FullPath;
        }

        private void LoadPickers()
        {
            FillPicker(FacultyPicker, loader.GetValues(xmlPath, "Faculty"));
            FillPicker(DepartmentPicker, loader.GetValues(xmlPath, "Department"));
            FillPicker(DegreePicker, loader.GetValues(xmlPath, "Degree"));
            FillPicker(TitlePicker, loader.GetValues(xmlPath, "Title"));
        }

        private void FillPicker(Picker picker, List<string> values)
        {
            List<string> items = new List<string>();
            items.Add(AllItems);
            items.AddRange(values);

            picker.ItemsSource = items;
            picker.SelectedIndex = 0;
        }

        private string GetPickerValue(Picker picker)
        {
            if (picker.SelectedItem == null)
                return "";
            string value = picker.SelectedItem.ToString() ?? "";
            if (value == AllItems)
                return "";
            return value;
        }

        private SearchFilter GetFilter()
        {
            SearchFilter filter = new SearchFilter();
            filter.FullName = (NameEntry.Text ?? "").Trim();
            filter.Faculty = GetPickerValue(FacultyPicker);
            filter.Department = GetPickerValue(DepartmentPicker);
            filter.Degree = GetPickerValue(DegreePicker);
            filter.Title = GetPickerValue(TitlePicker);
            return filter;
        }

        private ISearchStrategy GetSelectedStrategy()
        {
            if (DomRadio.IsChecked)
                return new DomSearchStrategy();
            if (LinqRadio.IsChecked)
                return new LinqSearchStrategy();
            return new SaxSearchStrategy();
        }

        private void ShowResult(List<Scientist> result)
        {
            if (result.Count == 0)
            {
                OutputEditor.Text = "Нічого не знайдено";
                return;
            }

            string text = "Знайдено: " + result.Count + "\n\n";
            foreach (Scientist s in result)
            {
                text += s.ToString() + "\n";
            }
            OutputEditor.Text = text;
        }
    }
}
