// Name: Edge Only
// Submenu: Object
// Author: David Pirraglia
// Title: Edge Only
// Version: 1
// Desc: This plugin deletes the inside of an object, leaving only the edge.
// Keywords:
// URL: https://dapirra.github.io/edge_only_plugin/
// Help: This plugin deletes the inside of an object, leaving only the edge.

#region UICode
IntSliderControl area = 0; // [0,100] Area
#endregion

void Render(Surface dst, Surface src, Rectangle rect) {
    ColorBgra CurrentPixel;
    for (int y = rect.Top; y < rect.Bottom; y++) {
        if (IsCancelRequested) return;
        for (int x = rect.Left; x < rect.Right; x++) {
            if (ApplyEffect(src, x, y)) {
                CurrentPixel = src[x, y];
                dst[x, y] = ColorBgra.FromBgra(
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

bool ApplyEffect(Surface src, int CurrentX, int CurrentY) {
    ColorBgra CurrentPixel = src[CurrentX, CurrentY];

    if (CurrentPixel.A == 0) {
        return false;
    }

    int GridSize = 3 + area * 2;
    int GridArea = GridSize * GridSize;
    int StartingX = CurrentX - (1 + area);
    int StartingY = CurrentY - (1 + area);
    for (int Y = StartingY; Y < StartingY + GridSize; Y++) {
        if (IsCancelRequested) return false;
        for (int X = StartingX; X < StartingX + GridSize; X++) {
            try {
                CurrentPixel = src[X, Y];
                if (CurrentPixel.A < 255) {
                    return false;
                }
            } catch (Exception ex) {
                return false;
            }
        }
    }

    return true;
}
