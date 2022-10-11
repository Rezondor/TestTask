namespace TestTask;

public class TaskViewModel : INotifyPropertyChanged
{
    private ObservableCollection<RectangleObject> rectangles;
    private RectangleGenerator generator;
    private int creationInterval;
    Dictionary<int, List<RectangleObject>> deleteRectengles;
    private const int COUNTLOOP = 5;
    public int CreationInterval
    {
        get { return creationInterval; }
        set
        {
            creationInterval = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<RectangleObject> Rectangles
    {
        get { return rectangles; }
        set
        {
            rectangles = value;
            OnPropertyChanged();
        }
    }


    public RectangleGenerator Generator
    {
        get { return generator; }
        set
        {
            generator = value;
            OnPropertyChanged();
        }
    }


    public TaskViewModel(int canvasHeight, int canvasWeight)
    {
        Rectangles = new ObservableCollection<RectangleObject>();
        Generator = new RectangleGenerator(canvasHeight, canvasWeight);
        deleteRectengles = new Dictionary<int, List<RectangleObject>>();
        CreationInterval = 5;
        for (int i = 0; i < COUNTLOOP; i++)
        {
            deleteRectengles.Add(i, new List<RectangleObject>());
        }

        StartGenerateRectangle();
    }


    public async Task StartGenerateRectangle()
    {
        while (true)
        {
            await Task.Delay(CreationInterval);
            RectangleObject rectangle = await Generator.RectangleGenerate();
            await CheckIntersectionRectangles(rectangle, Rectangles);
            Rectangles.Add(rectangle);
        }
    }

    public async Task CheckIntersectionRectangles(RectangleObject rectangle, ObservableCollection<RectangleObject> rectangles)
    {
        foreach (var item in rectangles)
        {
            bool isIntersect = await Intersection(rectangle, item);

            if (!isIntersect)
            {
                continue;
            }

            for (int i = 0; i < COUNTLOOP; i++)
            {
                if (deleteRectengles[i].Contains(item))
                {
                    if (i == COUNTLOOP - 1)
                    {
                        rectangles.Remove(item);
                    }
                    else
                    {
                        deleteRectengles[i + 1].Add(item);
                    }
                    deleteRectengles[i].Remove(item);
                    break;
                }
            }
        }
    }

    public async Task<bool> Intersection(RectangleObject rectangle1, RectangleObject rectangle2)
    {
        int leftUpX1 = rectangle1.StartPointX;
        int leftUpY1 = rectangle1.StartPointY;
        int rightDownX1 = leftUpX1 + rectangle1.Width;
        int rightDownY1 = leftUpY1 + rectangle1.Height;

        int leftUpX2 = rectangle2.StartPointX;
        int leftUpY2 = rectangle2.StartPointY;
        int rightDownX2 = leftUpX2 + rectangle1.Width;
        int rightDownY2 = leftUpY2 + rectangle1.Height;
        return
            (leftUpY1 < rightDownY2 ||
            rightDownY1 > leftUpY2 ||
            rightDownX1 < leftUpX2 ||
            leftUpX1 > rightDownX2);
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
