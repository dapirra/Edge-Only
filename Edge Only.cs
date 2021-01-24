void Render(Surface dst, Surface src, Rectangle rect)
{

    ColorBgra CurrentPixel, TopPixel, BottomPixel, LeftPixel, RightPixel, TopLeftPixel, TopRightPixel, BottomLeftPixel, BottomRightPixel;
    for (int y = rect.Top; y < rect.Bottom; y++)
    {
        if (IsCancelRequested) return;
        for (int x = rect.Left; x < rect.Right; x++)
        {
            // Avoid edges of selection
            if (x == 0 || y == 0 || x == src.Width - 1 || y == src.Height - 1) continue;

            CurrentPixel = src[x, y];
            TopPixel = src[x, y - 1];
            BottomPixel = src[x, y - 1];
            LeftPixel = src[x - 1, y];
            RightPixel = src[x + 1, y];
            TopLeftPixel = src[x - 1, y - 1];
            TopRightPixel = src[x + 1, y - 1];
            BottomLeftPixel = src[x - 1, y + 1];
            BottomRightPixel = src[x + 1, y + 1];

            ColorBgra[] Pixels = {TopPixel, BottomPixel, LeftPixel, RightPixel, TopLeftPixel, TopRightPixel, BottomLeftPixel, BottomRightPixel};
            bool EraseCurrentPixel = true;

            foreach(ColorBgra Pixel in Pixels) {
                if (Pixel.A < 255) {
                    EraseCurrentPixel = false;
                    break;
                }
            }

            if (EraseCurrentPixel) {
                CurrentPixel.A = 0;
            }

            dst[x,y] = CurrentPixel;
        }
    }

    #if DEBUG
    Debug.WriteLine(DateTime.Now + " - Done");
    #endif
}
