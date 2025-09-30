# 🖥️ MAUI Desktop Shortcut Helper

This sample shows how to **automatically create a Windows desktop shortcut** for a .NET MAUI or MAUI Blazor Hybrid app when it starts.  
Useful if you want your users to quickly launch the app after installation.

---

## ✨ Features
- Works on **.NET MAUI Windows (WinUI3)** apps  
- Creates a `.lnk` shortcut on the **user’s Desktop**  
- Uses `IWshRuntimeLibrary` (Windows Script Host Object Model)  
- Automatically names the shortcut after your app (`AppInfo.Current.Name`)  
- Prevents duplicate shortcuts (only creates if it doesn’t exist)  

---

## 📦 Requirements
- Windows only (other platforms are skipped with `OperatingSystem.IsWindows()`)  
- Add a reference to **Windows Script Host Object Model**:
  1. Right-click **References → Add Reference**  
  2. Go to **COM → Windows Script Host Object Model**  
  3. Add `IWshRuntimeLibrary`  

---

## 🛠️ Usage

Add this method to your `App.xaml.cs` (or equivalent):

```csharp
private void CreateDesktopShortcut()
{
    if (!OperatingSystem.IsWindows()) return;

    try
    {
        var shell = new IWshRuntimeLibrary.WshShell();
        string appName = Microsoft.Maui.ApplicationModel.AppInfo.Current.Name;

        string shortcutPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            $"{appName}.lnk");

        if (!File.Exists(shortcutPath))
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
        Console.WriteLine($"Shortcut creation failed: {ex.Message}");
    }
}
```

Then call it inside your app’s constructor:

```csharp
public App()
{
    CreateDesktopShortcut();
    InitializeComponent();
    MainPage = new MainPage();
}
```

---

## 📷 Example
When you run your MAUI app, a shortcut will be created on the Desktop:

```
C:\Users\<YourName>\Desktop\<AppName>.lnk
```

---

## 🚀 Why this project?
Publishing MAUI apps doesn’t automatically create shortcuts.  
This snippet provides a simple way to solve that and can be reused in any project.

---

## 🤝 Contributing
Feel free to fork, improve, and open a PR if you have enhancements (like custom icons, multiple shortcuts, etc.).

---

## 📄 License
MIT License – free to use, modify, and share.
