#ifndef SCENE_H
#define SCENE_H

class Scene
{
    ID2D1Factory *p_factory;
    ID2D1HwndRenderTarget *p_render_target;
    ID2D1SolidColorBrush *p_brush_calibrate_12;
    ID2D1SolidColorBrush *p_brush_calibrate_60;
    ID2D1SolidColorBrush *p_brush_calibrate_120;
    ID2D1SolidColorBrush *p_brush_calibrate_600;
    ID2D1SolidColorBrush *p_brush_hour_hand;
    ID2D1SolidColorBrush *p_brush_minute_hand;
    ID2D1SolidColorBrush *p_brush_second_hand;
    ID2D1PathGeometry *p_geometry_hour_hand;
    ID2D1PathGeometry *p_geometry_minute_hand;
    ID2D1PathGeometry *p_geometry_second_hand;

    const float clock_size;

    D2D1_POINT_2F point_center;
    float radius;

    void DrawCalibrate(float length, ID2D1Brush *brush, float stroke_width);
    HRESULT CreateHandGeometry(float short_hand_length, float long_hand_length, float half_hand_width, ID2D1PathGeometry **geometry);
    void UpdateSizeCache(int width, int height);

public:
    Scene();
    ~Scene();

    HRESULT Initialize();
    void Render(HWND hWnd);
    HRESULT Resize(int width, int height);
    void DiscardResources();
};

#endif // SCENE_H
