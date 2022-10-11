namespace TestTask;

public class RectangleGenerator
{
    private readonly int heightRange;
    private readonly int widthRange;

    private Random random;
    public int HeightRange
    {
        get { return heightRange; }
        init { heightRange = value; }
    }

    public int WidthRange
    {
        get { return widthRange; }
        init { widthRange = value; }
    }


    public RectangleGenerator(int heightRange, int widthRange)
    {
        HeightRange = heightRange;
        WidthRange = widthRange;
        random = new Random();
    }

    public async Task<RectangleObject> RectangleGenerate()
    {
        await Task.Delay(0);
        int startPointY, startPointX;
        int height,width;

        startPointX = random.Next(WidthRange);
        width = random.Next(startPointX, WidthRange);

        startPointY = random.Next(HeightRange);
        height = random.Next(startPointY, HeightRange);

        SolidColorBrush color = new SolidColorBrush(
                Color.FromArgb(
                    (byte)random.Next(255),
                    (byte)random.Next(255),
                    (byte)random.Next(255),
                    255));

        return new RectangleObject(startPointY, startPointX, height, width, color);
    }


}
