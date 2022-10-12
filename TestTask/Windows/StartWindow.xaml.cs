
namespace TestTask.Windows;
public partial class StartWindow : Window
{
    public StartWindow()
    {
        InitializeComponent();
    }

    private void Start_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }
}
