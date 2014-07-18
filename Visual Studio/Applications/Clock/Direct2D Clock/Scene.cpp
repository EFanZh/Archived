#include "Scene.h"
#include "Utilities.h"

using D2D1::ColorF;
using D2D1::HwndRenderTargetProperties;
using D2D1::Matrix3x2F;
using D2D1::PixelFormat;
using D2D1::Point2F;
using D2D1::RenderTargetProperties;
using D2D1::SizeU;

Scene::Scene() : p_factory(NULL),
p_render_target(NULL),
p_brush_calibrate_12(NULL),
p_brush_calibrate_60(NULL),
p_brush_calibrate_120(NULL),
p_brush_calibrate_600(NULL),
p_brush_hour_hand(NULL),
p_brush_minute_hand(NULL),
p_brush_second_hand(NULL),
p_geometry_hour_hand(NULL),
p_geometry_minute_hand(NULL),
p_geometry_second_hand(NULL),
clock_size(0.9f)
{
}

Scene::~Scene()
{
}

void Scene::DrawCalibrate(float length, ID2D1Brush *brush, float stroke_width)
{
    D2D1_POINT_2F point_0 = Point2F(0.0f, radius - length * radius);
    D2D1_POINT_2F point_1 = Point2F(0.0f, radius);
    p_render_target->DrawLine(point_0, point_1, brush, stroke_width * radius);
}

HRESULT Scene::CreateHandGeometry(float short_hand_length, float long_hand_length, float half_hand_width, ID2D1PathGeometry **p_p_geometry)
{
    HRESULT hr;

    if (FAILED(hr = p_factory->CreatePathGeometry(p_p_geometry)))
    {
        return hr;
    }

    ID2D1GeometrySink *sink = NULL;
    if (FAILED(hr = (*p_p_geometry)->Open(&sink)))
    {
        SafeRelease(&sink);
        return hr;
    }
    sink->SetFillMode(D2D1_FILL_MODE_WINDING);
    sink->BeginFigure(Point2F(0.0f, short_hand_length * radius), D2D1_FIGURE_BEGIN_FILLED);
    D2D1_POINT_2F points[] = { Point2F(-half_hand_width * radius, 0.0f), Point2F(0.0f, -long_hand_length * radius), Point2F(half_hand_width * radius, 0.0f) };
    int c = ARRAYSIZE(points);
    sink->AddLines(points, c);
    sink->EndFigure(D2D1_FIGURE_END_CLOSED);
    if (FAILED(hr = sink->Close()))
    {
        SafeRelease(&sink);
        return hr;
    }

    SafeRelease(&sink);

    return hr;
}

void Scene::UpdateSizeCache(int width, int height)
{
    point_center.x = width / 2.0f;
    point_center.y = height / 2.0f;
    radius = min(point_center.x, point_center.y) * clock_size;
}

HRESULT Scene::Initialize()
{
    HRESULT hr;

    if (FAILED(hr = D2D1CreateFactory(D2D1_FACTORY_TYPE_SINGLE_THREADED, &p_factory)))
    {
        return hr;
    }
    return hr;
}

void Scene::Render(HWND hWnd)
{
    if (!p_render_target)
    {
        RECT rect;

        GetClientRect(hWnd, &rect);
        if (FAILED(p_factory->CreateHwndRenderTarget(RenderTargetProperties(D2D1_RENDER_TARGET_TYPE_DEFAULT, PixelFormat(DXGI_FORMAT_UNKNOWN, D2D1_ALPHA_MODE_PREMULTIPLIED)), HwndRenderTargetProperties(hWnd, SizeU(rect.right, rect.bottom)), &p_render_target)))
        {
            return;
        }
        if (FAILED(p_render_target->CreateSolidColorBrush(ColorF(ColorF::Red), &p_brush_calibrate_12)))
        {
            return;
        }
        if (FAILED(p_render_target->CreateSolidColorBrush(ColorF(ColorF::White), &p_brush_calibrate_60)))
        {
            return;
        }
        if (FAILED(p_render_target->CreateSolidColorBrush(ColorF(ColorF::Orange), &p_brush_calibrate_120)))
        {
            return;
        }
        if (FAILED(p_render_target->CreateSolidColorBrush(ColorF(ColorF::Gray), &p_brush_calibrate_600)))
        {
            return;
        }
        if (FAILED(p_render_target->CreateSolidColorBrush(ColorF(ColorF::Blue), &p_brush_hour_hand)))
        {
            return;
        }
        if (FAILED(p_render_target->CreateSolidColorBrush(ColorF(ColorF::Green), &p_brush_minute_hand)))
        {
            return;
        }
        if (FAILED(p_render_target->CreateSolidColorBrush(ColorF(ColorF::Red), &p_brush_second_hand)))
        {
            return;
        }
        UpdateSizeCache(rect.right, rect.bottom);
    }

    p_render_target->BeginDraw();
    p_render_target->Clear(ColorF(0.0f, 0.0f, 0.0f, 0.0f));
    for (int i = 1; i <= 600; i++)
    {
        p_render_target->SetTransform(Matrix3x2F::Rotation(0.6f * i) * Matrix3x2F::Translation(point_center.x, point_center.y));
        if (i % 50 == 0)
        {
            DrawCalibrate(0.0375f, p_brush_calibrate_12, 0.003f);
        }
        else if (i % 10 == 0)
        {
            DrawCalibrate(0.015f, p_brush_calibrate_60, 0.003f);
        }
        else if (i % 5 == 0)
        {
            DrawCalibrate(0.01f, p_brush_calibrate_120, 0.003f);
        }
        else
        {
            DrawCalibrate(0.0075f, p_brush_calibrate_600, 0.003f);
        }
    }
    if (!p_geometry_second_hand && FAILED(CreateHandGeometry(0.1f, 0.64f, 0.01f, &p_geometry_hour_hand)))
    {
        return;
    }
    if (!p_geometry_minute_hand && FAILED(CreateHandGeometry(0.1f, 0.8f, 0.01f, &p_geometry_minute_hand)))
    {
        return;
    }
    if (!p_geometry_second_hand && FAILED(CreateHandGeometry(0.1f, 0.96f, 0.01f, &p_geometry_second_hand)))
    {
        return;
    }
    SYSTEMTIME st;
    GetLocalTime(&st);
    float second = st.wSecond + st.wMilliseconds / 1000.0f;
    float minute = st.wMinute + second / 60.0f;
    float hour = st.wHour % 12 + minute / 60.0f;
    p_render_target->SetTransform(Matrix3x2F::Rotation(hour * 30.0f) * Matrix3x2F::Translation(point_center.x, point_center.y));
    p_render_target->FillGeometry(p_geometry_hour_hand, p_brush_hour_hand);
    p_render_target->SetTransform(Matrix3x2F::Rotation(minute * 6.0f) * Matrix3x2F::Translation(point_center.x, point_center.y));
    p_render_target->FillGeometry(p_geometry_minute_hand, p_brush_minute_hand);
    p_render_target->SetTransform(Matrix3x2F::Rotation(second * 6.0f) * Matrix3x2F::Translation(point_center.x, point_center.y));
    p_render_target->FillGeometry(p_geometry_second_hand, p_brush_second_hand);

    p_render_target->EndDraw();
}

HRESULT Scene::Resize(int width, int height)
{
    if (p_render_target)
    {
        HRESULT hr = p_render_target->Resize(SizeU(width, height));

        if (SUCCEEDED(hr))
        {
            UpdateSizeCache(width, height);
            SafeRelease(&p_geometry_hour_hand);
            SafeRelease(&p_geometry_minute_hand);
            SafeRelease(&p_geometry_second_hand);
        }

        return hr;
    }
    return S_OK;
}

void Scene::DiscardResources()
{
}
