using Windows.ApplicationModel;

namespace Desktop_Shortcut
{
    public partial class App : Application
    {
        public App()
        {
            CreateDesktopShortcut();

            InitializeComponent();

            MainPage = new MainPage();
        }


        private void CreateDesktopShortcut()
        {
            if (!OperatingSystem.IsWindows()) return;

            try
            {
                // Add reference to Windows Script Host Object Model
                // 1. Right-click References → Add Reference
                // 2. COM → Windows Script Host Object Model

                var shell = new IWshRuntimeLibrary.WshShell();


                string appName = Microsoft.Maui.ApplicationModel.AppInfo.Current.Name;



                string shortcutPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    $"{appName}.lnk");

                // Only create if it doesn't exist
                if (!System.IO.File.Exists(shortcutPath))
                {
                    IWshRuntimeLibrary.IWshShortcut shortcut =
                        (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(shortcutPath);

                    string packageFamilyName = Package.Current.Id.FamilyName + "!App";

                    shortcut.TargetPath = $"shell:appsFolder\\{packageFamilyName}";
                    shortcut.Description = "";
                    shortcut.Save();

                    Console.WriteLine("Desktop shortcut created successfully");
                }
            }
            catch (Exception ex)
            {
                // Log but don't crash the app
                Console.WriteLine($"Shortcut creation failed: {ex.Message}");
            }
        }


    }
}
