using System.Windows;

namespace CS.SkiaSharpExample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnApplicationStartup(object sender, StartupEventArgs e)
        {
            var bs = new Bootstrapper();
            bs.Activate();
            Resources.Add(nameof(Bootstrapper), bs);
        }
    }
}
