// Name: Edge Only
// Submenu: Object
// Author: David Pirraglia
// Title: Edge Only
// Version: 1.2
// Desc: This plugin deletes the inside of an object, leaving only the edge.
// Keywords:
// URL: https://dapirra.github.io/edge_only_plugin/
// Help: This plugin deletes the inside of an object, leaving only the edge.

#region UICode
IntSliderControl thickness = 1; // [1,100] Thickness
#endregion

void Render(Surface dst, Surface src, Rectangle rect) {
    ColorBgra CurrentPixel;
    for (int y = rect.Top; y < rect.Bottom; y++) {
        if (IsCancelRequested) return;
        for (int x = rect.Left; x < rect.Right; x++) {
            if (isErasingPixel(src, x, y)) {
                CurrentPixel = src[x, y];
                dst[x, y] = ColorBgra.FromBgra( // Erase the pixel
                    CurrentPixel.B,
                    CurrentPixel.G,
                    CurrentPixel.R,
                    0
                );
            } else {
                dst[x, y] = src[x, y];
            }
        }
    }

    #if DEBUG
    Debug.WriteLine(DateTime.Now + " - Done");
    #endif
}

// Checks the neighboring pixels to determine if the pixel should be erased
bool isErasingPixel(Surface src, int CurrentX, int CurrentY) {
    ColorBgra CurrentPixel = src[CurrentX, CurrentY];

    if (CurrentPixel.A == 0) { // Pixel is already transparent
        return false;
    }

    int GridSize = 3 + (thickness - 1) * 2;
    int GridArea = GridSize * GridSize;
    int StartingX = CurrentX - thickness;
    int StartingY = CurrentY - thickness;

    for (int Y = StartingY; Y < StartingY + GridSize; Y++) {
        if (IsCancelRequested) return false;
        for (int X = StartingX; X < StartingX + GridSize; X++) {
            try {
                CurrentPixel = src[X, Y];
                // If neighboring pixel is transparent or partially transparent...
                if (CurrentPixel.A < 255) {
                    return false;
                }
            } catch (Exception ex) { // Must be out of bounds
                return false;
            }
        }
    }

    return true;
}
