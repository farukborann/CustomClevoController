using System;
using System.Windows;
using ControlzEx.Theming;
using MahApps.Metro.Theming;

namespace BSCustomClevoController
{
    /// <summary>
    /// App.xaml etkileşim mantığı
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Add custom theme resource dictionaries
            var theme = ThemeManager.Current.AddLibraryTheme(new LibraryTheme(
                new Uri("pack://application:,,,/BSCustomClevoController;component/Dark.RedBlack.xaml"),
                MahAppsLibraryThemeProvider.DefaultInstance));

            base.OnStartup(e);

            ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncWithAppMode;
            ThemeManager.Current.SyncTheme();

            // Create runtime themes
            /*ThemeManager.Current.AddTheme(new Theme("CustomDarkRed", "CustomDarkRed", "Dark", "Red", Colors.DarkRed, Brushes.DarkRed, true, false));
            ThemeManager.Current.AddTheme(new Theme("CustomLightRed", "CustomLightRed", "Light", "Red", Colors.DarkRed, Brushes.DarkRed, true, false));

            ThemeManager.Current.AddTheme(RuntimeThemeGenerator.Current.GenerateRuntimeTheme("Dark", Colors.Red));
            ThemeManager.Current.AddTheme(RuntimeThemeGenerator.Current.GenerateRuntimeTheme("Light", Colors.Red));

            ThemeManager.Current.AddTheme(RuntimeThemeGenerator.Current.GenerateRuntimeTheme("Dark", Colors.GreenYellow));
            ThemeManager.Current.AddTheme(RuntimeThemeGenerator.Current.GenerateRuntimeTheme("Light", Colors.GreenYellow));

            ThemeManager.Current.AddTheme(RuntimeThemeGenerator.Current.GenerateRuntimeTheme("Dark", Colors.Indigo));
            ThemeManager.Current.ChangeTheme(this, ThemeManager.Current.AddTheme(RuntimeThemeGenerator.Current.GenerateRuntimeTheme("Light", Colors.Indigo)));*/

            ThemeManager.Current.ChangeTheme(this, theme);
        }
    }
}
