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
        int height, width;

        startPointX = random.Next(WidthRange - 10);
        width = random.Next(1, WidthRange - startPointX - 1);

        startPointY = random.Next(HeightRange - 10);
        height = random.Next(1, HeightRange - startPointY - 1);

        SolidColorBrush color = new(
                Color.FromArgb(
                    (byte)random.Next(150, 255),
                    (byte)random.Next(255),
                    (byte)random.Next(255),
                    (byte)random.Next(255)));

        return new RectangleObject(startPointY, startPointX, height, width, color);
    }


}
