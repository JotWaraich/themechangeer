using System.Diagnostics;
using Microsoft.Win32;

public class ThemeChanger
{
    public void ChangeTheme(bool lightMode)
    {
        try
        {
            // Set the values in the registry directly
            using (var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", true))
            {
                if (key != null)
                {
                    key.SetValue("AppsUseLightTheme", lightMode ? 1 : 0, RegistryValueKind.DWord);
                    key.SetValue("SystemUsesLightTheme", lightMode ? 1 : 0, RegistryValueKind.DWord);
                }
            }

            // Optionally, you could use cmd.exe to refresh the UI or apply the theme changes.
            // For example, using a command to refresh the theme settings.
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/C taskkill /f /im explorer.exe & start explorer.exe"; // Restart explorer to apply changes
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.Verb = "runas"; // Run as administrator
            process.Start();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error: {ex.Message}");
        }
    }
}
