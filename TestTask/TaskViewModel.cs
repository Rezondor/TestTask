namespace TestTask;

public class TaskViewModel : INotifyPropertyChanged
{
    private ObservableCollection<RectangleObject> rectangles;
    private RectangleGenerator generator;
    private int creationInterval;
    List<List<RectangleObject>> deleteRectengles;
    private const int COUNTLOOP = 4;
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


    public TaskViewModel(int canvasHeight, int canvasWeight, int interval)
    {
        Rectangles = new ObservableCollection<RectangleObject>();
        Generator = new RectangleGenerator(canvasHeight, canvasWeight);
        deleteRectengles = new List<List<RectangleObject>>();
        CreationInterval = interval;
        for (int i = 0; i < COUNTLOOP; i++)
        {
            deleteRectengles.Add(new List<RectangleObject>());
        }

        StartGenerateRectangle();
    }


    public async Task StartGenerateRectangle()
    {
        while (true)
        {
            await UpdateLoop();
            Task wait = Task.Delay(CreationInterval);
            RectangleObject rectangle = await Generator.RectangleGenerate();
            await wait;
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

            bool isContains = false;
            foreach (var rectList in deleteRectengles)
            {
                if (rectList.Contains(item))
                {
                    isContains = true;
                    break;
                }
            }


            if (!isContains)
            {
                deleteRectengles[0].Add(item);
            }
        }
    }

    private async Task UpdateLoop()
    {
        for (int i = COUNTLOOP - 1; i >= 0; i--)
        {
            for (int j = 0; j < deleteRectengles[i].Count; j++)
            {
                RectangleObject rect = deleteRectengles[i][j];
                if (i == COUNTLOOP - 1)
                {
                    Rectangles.Remove(rect);
                }
                else
                {
                    deleteRectengles[i + 1].Add(rect);
                }
            }
            deleteRectengles[i].Clear();
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
        int rightDownX2 = leftUpX2 + rectangle2.Width;
        int rightDownY2 = leftUpY2 + rectangle2.Height;

        return !(
            leftUpY1 > rightDownY2 ||
            rightDownY1 < leftUpY2 ||
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
