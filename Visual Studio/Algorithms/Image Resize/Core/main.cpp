#include <vector>

using namespace std;

vector<vector<unsigned char>> v;

typedef float TValue;
typedef unsigned int TSize;
typedef TValue (*TGetPixel)(TSize x, TSize y);
typedef void (*TSetPixel)(TSize x, TSize y, TValue value);

TSize MultiplyDivide(TSize x, TSize y, TSize z)
{
    return x * y / z;
}

TSize MultiplyDivide(TSize x, TSize y, TSize z)
{
    return x * y / z;
}

TSize LCM(TSize x, TSize y)
{
    return 0;
}

class AverageCalculator
{
public:
    void Begin();
    TValue End();
    void Add(TValue value);
};

void Resize(TSize inWidth, TSize inHeight, TGetPixel getPixel, TSize outWidth, TSize outHeight, TSetPixel setPixel)
{
    TSize workingWidth = LCM(inWidth, outWidth);
    TSize workingHeight = LCM(inHeight, outHeight);
    TSize inPixelWidth = workingWidth / inWidth;
    TSize outPixelWidth = workingWidth / outWidth;
    TSize inPixelHeight = workingHeight / inHeight;
    TSize outPixelHeight = workingHeight / outHeight;

    for (TSize outY = 0, workingY = 0; outY < outHeight; ++outY, workingY += outPixelHeight)
    {
        for (TSize outX = 0, workingX = 0; outX < outWidth; ++outX, workingX += outPixelWidth)
        {
            TSize inRemainPixelXBegin = workingX % inPixelWidth;
            TSize inRemainPixelYBegin = workingY % inPixelHeight;
            TSize inRemainPixelXEnd = (workingX + outPixelWidth) % inPixelWidth;
            TSize inRemainPixelYEnd = (workingY + outPixelHeight) % inPixelHeight;

            if (inRemainPixelXBegin == 0)
            {
                if (inRemainPixelYBegin == 0)
                {
                    if (inRemainPixelXEnd == 0)
                    {
                        if (inRemainPixelYEnd == 0)
                        {

                        }
                        else
                        {

                        }
                    }
                }
            }
        }
    }
}

int main()
{
}
