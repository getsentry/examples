using System.Windows;
using Sentry;

namespace WpfDotNetCoreCSharp
{
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SentrySdk.AddBreadcrumb($"{nameof(Button_Click)} clicked.", "ui.lifecycle");

            throw null;
        }
    }
}
