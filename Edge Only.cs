// Name:
// Submenu:
// Author:
// Title:
// Version:
// Desc:
// Keywords:
// URL:
// Help:
#region UICode
IntSliderControl Amount1 = 0; // [0,100] Slider 1 Description
IntSliderControl Amount2 = 0; // [0,100] Slider 2 Description
IntSliderControl Amount3 = 0; // [0,100] Slider 3 Description
#endregion

void Render(Surface dst, Surface src, Rectangle rect)
{
    // Delete any of these lines you don't need
    // Rectangle selection = EnvironmentParameters.GetSelection(src.Bounds).GetBoundsInt();
    // int CenterX = ((selection.Right - selection.Left) / 2) + selection.Left;
    // int CenterY = ((selection.Bottom - selection.Top) / 2) + selection.Top;
    // ColorBgra PrimaryColor = EnvironmentParameters.PrimaryColor;
    // ColorBgra SecondaryColor = EnvironmentParameters.SecondaryColor;
    // int BrushWidth = (int)EnvironmentParameters.BrushWidth;

    ColorBgra CurrentPixel, TopPixel, BottomPixel, LeftPixel, RightPixel, TopLeftPixel, TopRightPixel, BottomLeftPixel, BottomRightPixel;
    for (int y = rect.Top; y < rect.Bottom; y++)
    {
        if (IsCancelRequested) return;
        for (int x = rect.Left; x < rect.Right; x++)
        {
            // Avoid edges of selection
            if (x == 0 || y == 0 || x == rect.Right - 1 || y == rect.Bottom - 1) break;
            
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
                //if (Pixel.R != 0 || Pixel.G != 0|| Pixel.B != 0) {
                if (Pixel.A < 255) {
                    EraseCurrentPixel = false;
                    break;
                }
            }
            
            if (EraseCurrentPixel) {
                CurrentPixel.A = 0;
            }
            
            // TODO: Add pixel processing code here
            // Access RGBA values this way, for example:
            // CurrentPixel.R = PrimaryColor.R;
            // CurrentPixel.G = PrimaryColor.G;
            // CurrentPixel.B = PrimaryColor.B;
            // CurrentPixel.A = PrimaryColor.A;
            dst[x,y] = CurrentPixel;
        }
    }
}
