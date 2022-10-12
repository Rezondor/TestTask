namespace TestTask;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.Hide();
        StartWindow window = new StartWindow();
        if ((bool)window.ShowDialog())
        {
            int generationInterval = (int)window.Interval.Value;
            this.Show();
            DataContext = new TaskViewModel((int)MainCanvas.Height, (int)MainCanvas.Width, generationInterval);
        }
        else
        {
            Close();
        }
    }
}
