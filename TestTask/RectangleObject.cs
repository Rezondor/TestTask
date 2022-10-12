namespace TestTask;

public class RectangleObject : INotifyPropertyChanged
{
    private static int CountRect = 0;
    private int id = 0;
    private int startPointY;
    private int startPointX;

    private int height;
    private int width;


    private Brush color;


    public int Id
    {
        get { return id; }
        init
        {
            id = value;
            OnPropertyChanged();
        }
    }

    public Brush Color
    {
        get { return color; }
        set
        {
            color = value;
            OnPropertyChanged();
        }
    }

    public int StartPointY
    {
        get { return startPointY; }
        private set
        {
            startPointY = value;
            OnPropertyChanged();
        }
    }

    public int StartPointX
    {
        get { return startPointX; }
        private set
        {
            startPointX = value;
            OnPropertyChanged();
        }
    }
    public int Height
    {
        get { return height; }
        private set
        {
            height = value;
            OnPropertyChanged();
        }
    }
    public int Width
    {
        get { return width; }
        private set
        {
            width = value;
            OnPropertyChanged();
        }
    }

    public RectangleObject(int startPointY, int startPointX, int height, int width, Brush color)
    {
        id = CountRect++;
        StartPointY = startPointY;
        StartPointX = startPointX;
        Height = height;
        Width = width;
        Color = color;
    }



    #region MVVM 
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
    #endregion
}
