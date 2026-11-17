using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

namespace ScientistsLab
{
    public static class MauiProgram
    {
        private static bool canClose = false;

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Підтвердження виходу при натисканні на хрестик вікна (Windows)
            builder.ConfigureLifecycleEvents(events =>
            {
#if WINDOWS
                events.AddWindows(windows => windows.OnWindowCreated(window =>
                {
                    window.AppWindow.Closing += async (s, e) =>
                    {
                        if (canClose)
                            return;

                        e.Cancel = true;
                        Page? page = Application.Current?.Windows[0].Page;
                        if (page == null)
                            return;

                        bool answer = await page.DisplayAlertAsync("Вихід",
                            "Чи дійсно ви хочете завершити роботу з програмою?", "Так", "Ні");
                        if (answer)
                        {
                            canClose = true;
                            Application.Current?.Quit();
                        }
                    };
                }));
#endif
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
