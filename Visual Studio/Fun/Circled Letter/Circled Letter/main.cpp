#include "stdafx.h"
#include "com_objects.h"

using namespace std;
using namespace D2D1;
using namespace Microsoft::WRL;
using namespace com_objects;
using namespace com_objects::d2d;
using namespace com_objects::dwrite;
using namespace com_objects::wic;

struct Configuration
{
    double size;
    double ringSize;
    double ringThickness;
    wstring fontFamilyName;
    double fontCapSize;
    DWRITE_FONT_STYLE fontStyle;
    DWRITE_FONT_WEIGHT fontWeight;
    wstring text;
    D2D1_COLOR_F backgroundColor;
    D2D1_COLOR_F ringColor;
    D2D1_COLOR_F textColor;
    D2D1_COLOR_F ringHaloColor;
    D2D1_COLOR_F textHaloColor;
    double haloRadius;
    wstring outputFile;
    bool drawDebugInfo;
};

struct ScopedDraw
{
    DeviceContext &deviceContext;

    ScopedDraw(DeviceContext &deviceContext) : deviceContext(deviceContext)
    {
        deviceContext.BeginDraw();
    }

    ScopedDraw(const ScopedDraw &other) = delete;
    ScopedDraw &operator =(const ScopedDraw &rhs) = delete;

    ~ScopedDraw()
    {
        deviceContext.EndDraw();
    }
};

void DrawHorizontalDebugLine(DeviceContext &deviceContext,
                             double imageSize,
                             wstring_view name,
                             TextFormat &textFormat,
                             double position,
                             Brush &brush)
{
    deviceContext.DrawLine(Point2F(0.0f, static_cast<FLOAT>(position)),
                           Point2F(static_cast<FLOAT>(imageSize), static_cast<FLOAT>(position)),
                           brush);

    deviceContext.DrawText(name.data(),
                           static_cast<UINT32>(name.length()),
                           textFormat,
                           RectF(0.0f,
                                 static_cast<FLOAT>(position),
                                 numeric_limits<FLOAT>::max(),
                                 numeric_limits<FLOAT>::max()),
                           brush);
}

void DrawVerticalDebugLine(DeviceContext &deviceContext,
                           double imageSize,
                           wstring_view name,
                           TextFormat &textFormat,
                           double position,
                           Brush &brush)
{
    deviceContext.DrawLine(Point2F(static_cast<FLOAT>(position), 0.0f),
                           Point2F(static_cast<FLOAT>(position), static_cast<FLOAT>(imageSize)),
                           brush);

    deviceContext.DrawText(name.data(),
                           static_cast<UINT32>(name.length()),
                           textFormat,
                           RectF(static_cast<FLOAT>(position),
                                 0.0f,
                                 numeric_limits<FLOAT>::max(),
                                 numeric_limits<FLOAT>::max()),
                           brush);
}

double GetHorizontalOffset(FontFace &fontFace)
{
    const auto sampleCharacter = static_cast<UINT32>('I');
    auto glyphIndex = UINT16{};

    fontFace.GetGlyphIndices(&sampleCharacter, 1, &glyphIndex);

    auto glyphMetrics = DWRITE_GLYPH_METRICS{};

    fontFace.GetDesignGlyphMetrics(&glyphIndex, 1, &glyphMetrics);

    return (glyphMetrics.rightSideBearing - glyphMetrics.leftSideBearing) / 2.0;
}

template <class F>
CommandList CreateCommandList(DeviceContext &deviceContext, F &&drawing)
{
    auto commandList = deviceContext.CreateCommandList();

    {
        auto scoped_close = MakeScopeGuard([&] { commandList.Close(); });

        deviceContext.SetTarget(commandList);
        drawing();
    }

    return commandList;
}

Effect CreateEffect(DeviceContext &deviceContext, REFCLSID effectId, Image &input)
{
    auto effect = deviceContext.CreateEffect(effectId);

    effect.SetInput(0, input);

    return effect;
}

CommandList DrawImage(dwrite::Factory &dWriteFactory,
                      FontCollection &systemFontCollection,
                      DeviceContext &deviceContext,
                      dwrite::Font &textFont,
                      const Configuration &configuration)
{
    auto fontFace = textFont.CreateFontFace();
    const auto fontMetrics = fontFace.GetFontMetrics();
    const auto fontSize = configuration.fontCapSize * fontMetrics.designUnitsPerEm / static_cast<double>(fontMetrics.capHeight);

    auto textLayout = ([&] {
        auto textFormat = ([&] {
            auto result = dWriteFactory.CreateTextFormat(configuration.fontFamilyName.c_str(),
                                                         systemFontCollection,
                                                         textFont.GetWeight(),
                                                         textFont.GetStyle(),
                                                         textFont.GetStretch(),
                                                         static_cast<FLOAT>(fontSize),
                                                         L"");

            result.SetWordWrapping(DWRITE_WORD_WRAPPING::DWRITE_WORD_WRAPPING_NO_WRAP);

            return result;
        })();

        return dWriteFactory.CreateTextLayout(configuration.text.c_str(),
                                              static_cast<UINT32>(configuration.text.length()),
                                              textFormat,
                                              static_cast<FLOAT>(configuration.size),
                                              static_cast<FLOAT>(configuration.size));
    })();

    const auto lineMetrics = textLayout.GetLineMetrics();

    if (lineMetrics.size() != 1)
    {
        abort();
    }

    const auto textMetrics = textLayout.GetTextMetrics();
    const auto textLeftOriginal = (configuration.size - textMetrics.width) / 2.0;
    const auto baselinePosition = (configuration.size + configuration.fontCapSize) / 2.0;
    const auto metricsScale = fontSize / fontMetrics.designUnitsPerEm;
    const auto textLeft = textLeftOriginal + GetHorizontalOffset(fontFace) * metricsScale;
    const auto textTop = baselinePosition - lineMetrics.front().baseline;
    const auto halfImageSize = configuration.size / 2.0;

    auto drawContent = ([&] {
        const auto radius = static_cast<FLOAT>((configuration.ringSize - configuration.ringThickness) / 2.0);

        const auto ringEllipse = Ellipse(Point2F(static_cast<FLOAT>(halfImageSize), static_cast<FLOAT>(halfImageSize)),
                                         radius,
                                         radius);

        const auto ringThickness = static_cast<FLOAT>(configuration.ringThickness);
        const auto textOrigin = Point2F(static_cast<FLOAT>(textLeft), static_cast<FLOAT>(textTop));

        return [&, ringEllipse, ringThickness, textOrigin](auto &ringBrush, auto &textBrush) {
            deviceContext.DrawEllipse(ringEllipse, ringBrush, ringThickness);
            deviceContext.DrawTextLayout(textOrigin, textLayout, textBrush);
        };
    })();

    auto halo = ([&] {
        auto haloCommandList = CreateCommandList(deviceContext, [&] {
            auto textHaloBrush = deviceContext.CreateSolidColorBrush(configuration.textHaloColor);
            auto ringHaloBrush = deviceContext.CreateSolidColorBrush(configuration.ringHaloColor);

            drawContent(ringHaloBrush, textHaloBrush);
        });
        auto result = CreateEffect(deviceContext, CLSID_D2D1GaussianBlur, haloCommandList);

        result.SetValue(D2D1_GAUSSIANBLUR_PROP::D2D1_GAUSSIANBLUR_PROP_STANDARD_DEVIATION,
                        static_cast<FLOAT>(configuration.haloRadius / 3.0));

        result.SetValue(D2D1_GAUSSIANBLUR_PROP::D2D1_GAUSSIANBLUR_PROP_OPTIMIZATION,
                        D2D1_GAUSSIANBLUR_OPTIMIZATION::D2D1_GAUSSIANBLUR_OPTIMIZATION_QUALITY);

        return result;
    })();

    auto result = CreateCommandList(
        deviceContext,
        [&] {
        deviceContext.Clear(configuration.backgroundColor);
        deviceContext.DrawImage(halo);

        auto textBrush = deviceContext.CreateSolidColorBrush(configuration.textColor);
        auto ringBrush = deviceContext.CreateSolidColorBrush(configuration.ringColor);

        drawContent(ringBrush, textBrush);
    });

    if (configuration.drawDebugInfo)
    {
        return CreateCommandList(deviceContext, [&] {
            deviceContext.DrawImage(result);

            auto debugTextFormat = dWriteFactory.CreateTextFormat(L"Calibri",
                                                                  systemFontCollection,
                                                                  DWRITE_FONT_WEIGHT::DWRITE_FONT_WEIGHT_REGULAR,
                                                                  DWRITE_FONT_STYLE::DWRITE_FONT_STYLE_NORMAL,
                                                                  DWRITE_FONT_STRETCH::DWRITE_FONT_STRETCH_NORMAL,
                                                                  16.0f,
                                                                  L"");

            auto debugBrush = deviceContext.CreateSolidColorBrush(ColorF(1.0f, 0.2f, 0.1f, 0.8f));

            const auto drawHorizontalDebugLine = [&](const WCHAR *name, double position) {
                DrawHorizontalDebugLine(deviceContext, configuration.size, name, debugTextFormat, position, debugBrush);
            };

            const auto drawVerticalDebugLine = [&](const WCHAR *name, double position) {
                DrawVerticalDebugLine(deviceContext, configuration.size, name, debugTextFormat, position, debugBrush);
            };

            // Draw center cross.

            drawHorizontalDebugLine(L"Image Center", halfImageSize);
            drawVerticalDebugLine(L"Image Center", halfImageSize);

            // Draw text layout box.

            const auto textRight = textLeft + textMetrics.width;
            const auto textBottom = textTop + textMetrics.height;

            drawVerticalDebugLine(L"Original Text Left", textLeftOriginal);
            drawVerticalDebugLine(L"Text Left", textLeft);
            drawHorizontalDebugLine(L"Text Top", textTop);
            drawVerticalDebugLine(L"Text Right", textRight);
            drawHorizontalDebugLine(L"Text Bottom", textBottom);

            // Draw text metrics.

            drawHorizontalDebugLine(L"Baseline", baselinePosition);
            drawHorizontalDebugLine(L"Ascent", baselinePosition - fontMetrics.ascent * metricsScale);
            drawHorizontalDebugLine(L"Descent", baselinePosition + fontMetrics.descent * metricsScale);
            drawHorizontalDebugLine(L"Line Gap", textTop + fontMetrics.lineGap * metricsScale);
            drawHorizontalDebugLine(L"Cap Height", baselinePosition - fontMetrics.capHeight * metricsScale);
            drawHorizontalDebugLine(L"x Height", baselinePosition - fontMetrics.xHeight * metricsScale);
            drawHorizontalDebugLine(L"Underline", baselinePosition - fontMetrics.underlinePosition * metricsScale);
            drawHorizontalDebugLine(L"Strikethrough", baselinePosition - fontMetrics.strikethroughPosition * metricsScale);

            // Draw caret line.

            const auto caretMetrics = fontFace.GetCaretMetrics();
            const auto offset = halfImageSize * caretMetrics.slopeRun / static_cast<double>(caretMetrics.slopeRise);

            deviceContext.DrawLine(
                Point2F(static_cast<FLOAT>(halfImageSize + offset), 0.0f),
                Point2F(static_cast<FLOAT>(halfImageSize - offset), static_cast<FLOAT>(configuration.size)),
                debugBrush);
        });
    }
    else
    {
        return result;
    }
}

wic::Stream OpenFileForWrite(ImagingFactory &imagingFactory, const WCHAR *outputFile)
{
    auto stream = imagingFactory.CreateStream();

    stream.InitializeFromFilename(outputFile, GENERIC_WRITE);

    return move(stream);
}

PROPVARIANT CreatePropVariant(UCHAR value)
{
    auto result = PROPVARIANT{};

    result.vt = VARENUM::VT_UI1;
    result.bVal = value;

    return result;
}

PROPVARIANT CreatePropVariant(USHORT value)
{
    auto result = PROPVARIANT{};

    result.vt = VARENUM::VT_UI2;
    result.uiVal = value;

    return result;
}

PROPVARIANT CreatePropVariant(UINT value)
{
    auto result = PROPVARIANT{};

    result.vt = VARENUM::VT_UI4;
    result.uintVal = value;

    return result;
}

void WriteSRgb(MetadataQueryWriter &metadataQueryWriter)
{
    // Values are from: https://www.w3.org/TR/PNG/#11sRGB.

    metadataQueryWriter.SetMetadataByName(L"/sRGB/RenderingIntent", CreatePropVariant(UCHAR{ 3 }));
    metadataQueryWriter.SetMetadataByName(L"/gAMA/ImageGamma", CreatePropVariant(UINT{ 45455 }));
    metadataQueryWriter.SetMetadataByName(L"/cHRM/WhitePointX", CreatePropVariant(UINT{ 31270 }));
    metadataQueryWriter.SetMetadataByName(L"/cHRM/WhitePointY", CreatePropVariant(UINT{ 32900 }));
    metadataQueryWriter.SetMetadataByName(L"/cHRM/RedX", CreatePropVariant(UINT{ 64000 }));
    metadataQueryWriter.SetMetadataByName(L"/cHRM/RedY", CreatePropVariant(UINT{ 33000 }));
    metadataQueryWriter.SetMetadataByName(L"/cHRM/GreenX", CreatePropVariant(UINT{ 30000 }));
    metadataQueryWriter.SetMetadataByName(L"/cHRM/GreenY", CreatePropVariant(UINT{ 60000 }));
    metadataQueryWriter.SetMetadataByName(L"/cHRM/BlueX", CreatePropVariant(UINT{ 15000 }));
    metadataQueryWriter.SetMetadataByName(L"/cHRM/BlueY", CreatePropVariant(UINT{ 6000 }));
}

wstring GetColorProfileFile()
{
    auto result = vector<WCHAR>{};
    const auto *const fileName = L"\\sRGB Color Space Profile.icm";
    auto size = static_cast<DWORD>(result.size());

    GetColorDirectory(nullptr, result.data(), &size);

    result.resize(size);

    if (GetColorDirectory(nullptr, result.data(), &size))
    {
        return wstring{ result.data() } +fileName;
    }
    else
    {
        abort();
    }
}

void SaveBitmap(ImagingFactory &imagingFactory, const WCHAR *outputFile, Device &d2DDevice, Bitmap &bitmap)
{
    auto bitmapEncoder = ([&] {
        auto bitmapEncoder = imagingFactory.CreateEncoder(GUID_ContainerFormatPng, nullptr);
        auto stream = OpenFileForWrite(imagingFactory, outputFile);

        bitmapEncoder.Initialize(stream, WICBitmapEncoderCacheOption::WICBitmapEncoderNoCache);

        return move(bitmapEncoder);
    })();

    auto bitmapFrameEncode = ([&] {
        const auto desiredPixelFormatGuid = GUID_WICPixelFormat64bppRGBA;
        auto result = get<0>(bitmapEncoder.CreateNewFrame());

        result.Initialize();

        const auto newPixelFormatGuid = result.SetPixelFormat(desiredPixelFormatGuid);

        VerifyEquals(newPixelFormatGuid, desiredPixelFormatGuid);

        result.SetResolution(96.0, 96.0);

        auto colorContext = imagingFactory.CreateColorContext();

        colorContext.InitializeFromFilename(GetColorProfileFile().c_str());

        result.SetColorContext(colorContext);

        auto metadataQueryWriter = result.GetMetadataQueryWriter();

        WriteSRgb(metadataQueryWriter);

        return std::move(result);
    })();

    auto imageEncoder = imagingFactory.CreateImageEncoder(d2DDevice);

    imageEncoder.WriteFrame(bitmap, bitmapFrameEncode);

    bitmapFrameEncode.Commit();

    bitmapEncoder.Commit();
}

void RunProgram(Device &d2DDevice,
                dwrite::Factory &dWriteFactory,
                ImagingFactory &imagingFactory,
                FontCollection &systemFontCollection,
                const Configuration &configuration)
{
    const auto maybeTextFontIndex = systemFontCollection.FindFamilyName(configuration.fontFamilyName.c_str());

    if (maybeTextFontIndex)
    {
        auto textFontFamily = systemFontCollection.GetFontFamily(*maybeTextFontIndex);

        auto textFont = textFontFamily.GetFirstMatchingFont(configuration.fontWeight,
                                                            DWRITE_FONT_STRETCH::DWRITE_FONT_STRETCH_NORMAL,
                                                            configuration.fontStyle);

        if (textFont.GetSimulations() == DWRITE_FONT_SIMULATIONS::DWRITE_FONT_SIMULATIONS_NONE)
        {
            auto deviceContext = d2DDevice.CreateDeviceContext(D2D1_DEVICE_CONTEXT_OPTIONS::D2D1_DEVICE_CONTEXT_OPTIONS_NONE);

            auto scRgbColorContext = deviceContext.CreateColorContextFromDxgiColorSpace(
                DXGI_COLOR_SPACE_TYPE::DXGI_COLOR_SPACE_RGB_FULL_G10_NONE_P709);

            auto sRgbColorContext = deviceContext.CreateColorContextFromDxgiColorSpace(
                DXGI_COLOR_SPACE_TYPE::DXGI_COLOR_SPACE_RGB_FULL_G22_NONE_P709);

            auto bitmap = deviceContext.CreateBitmap(
                SizeU(static_cast<UINT32>(configuration.size), static_cast<UINT32>(configuration.size)),
                nullptr,
                0,
                BitmapProperties1(D2D1_BITMAP_OPTIONS::D2D1_BITMAP_OPTIONS_TARGET,
                                  PixelFormat(DXGI_FORMAT::DXGI_FORMAT_R32G32B32A32_FLOAT,
                                              D2D1_ALPHA_MODE::D2D1_ALPHA_MODE_PREMULTIPLIED)));

            {
                ScopedDraw drawGuard{ deviceContext };

                auto image = DrawImage(dWriteFactory,
                                       systemFontCollection,
                                       deviceContext,
                                       textFont,
                                       configuration);

                auto colorManageEffect = CreateEffect(deviceContext, CLSID_D2D1ColorManagement, image);

                colorManageEffect.SetValue(D2D1_COLORMANAGEMENT_PROP::D2D1_COLORMANAGEMENT_PROP_SOURCE_COLOR_CONTEXT,
                                           scRgbColorContext.GetComObject());

                colorManageEffect.SetValue(
                    D2D1_COLORMANAGEMENT_PROP::D2D1_COLORMANAGEMENT_PROP_SOURCE_RENDERING_INTENT,
                    D2D1_COLORMANAGEMENT_RENDERING_INTENT::D2D1_COLORMANAGEMENT_RENDERING_INTENT_ABSOLUTE_COLORIMETRIC);

                colorManageEffect.SetValue(
                    D2D1_COLORMANAGEMENT_PROP::D2D1_COLORMANAGEMENT_PROP_DESTINATION_COLOR_CONTEXT,
                    sRgbColorContext.GetComObject());

                colorManageEffect.SetValue(
                    D2D1_COLORMANAGEMENT_PROP::D2D1_COLORMANAGEMENT_PROP_DESTINATION_RENDERING_INTENT,
                    D2D1_COLORMANAGEMENT_RENDERING_INTENT::D2D1_COLORMANAGEMENT_RENDERING_INTENT_ABSOLUTE_COLORIMETRIC);

                colorManageEffect.SetValue(
                    D2D1_COLORMANAGEMENT_PROP::D2D1_COLORMANAGEMENT_PROP_ALPHA_MODE,
                    D2D1_COLORMANAGEMENT_ALPHA_MODE::D2D1_COLORMANAGEMENT_ALPHA_MODE_PREMULTIPLIED);

                colorManageEffect.SetValue(D2D1_COLORMANAGEMENT_PROP::D2D1_COLORMANAGEMENT_PROP_QUALITY,
                                           D2D1_COLORMANAGEMENT_QUALITY::D2D1_COLORMANAGEMENT_QUALITY_BEST);

                deviceContext.SetTarget(bitmap);
                deviceContext.DrawImage(colorManageEffect);
            }

            SaveBitmap(imagingFactory, configuration.outputFile.c_str(), d2DDevice, bitmap);
        }
        else
        {
            cout << "Simulated font is not allowed.\n";
        }
    }
    else
    {
        cout << "Failed to find the font.\n";
    }
}

void DoTheJob(const Configuration &configuration)
{
    VerifyHResult(CoInitializeEx(nullptr, COINIT::COINIT_MULTITHREADED));

    auto d2DDevice = ([] {
        auto featureLevels = { D3D_FEATURE_LEVEL::D3D_FEATURE_LEVEL_11_0 };

        auto dxgiDevice = d3d::Device::Create(nullptr,
                                              D3D_DRIVER_TYPE::D3D_DRIVER_TYPE_HARDWARE,
                                              nullptr,
                                              D3D11_CREATE_DEVICE_FLAG::D3D11_CREATE_DEVICE_BGRA_SUPPORT,
                                              featureLevels.begin(),
                                              static_cast<UINT>(featureLevels.size()),
                                              D3D11_SDK_VERSION).AsDxgiDevice();

        return Device::Create(dxgiDevice,
                              CreationProperties(D2D1_THREADING_MODE::D2D1_THREADING_MODE_SINGLE_THREADED,
                                                 D2D1_DEBUG_LEVEL::D2D1_DEBUG_LEVEL_NONE,
                                                 D2D1_DEVICE_CONTEXT_OPTIONS::D2D1_DEVICE_CONTEXT_OPTIONS_NONE));
    })();

    auto dWriteFactory = dwrite::Factory::Create(DWRITE_FACTORY_TYPE::DWRITE_FACTORY_TYPE_SHARED);
    auto imagingFactory = ImagingFactory::Create();
    auto systemFontCollection = dWriteFactory.GetSystemFontCollection();

    RunProgram(d2DDevice,
               dWriteFactory,
               imagingFactory,
               systemFontCollection,
               configuration);
}

int wmain(int argc, wchar_t *argv[])
{
    if (argc != 2)
    {
        wcout << L"Need the output path.\n";

        return EXIT_FAILURE;
    }
    else
    {
        auto configuration = Configuration{};
        const auto scale = 512;

        configuration.fontFamilyName = L"Adobe Garamond Pro";
        configuration.fontCapSize = 8.0 * scale;
        configuration.fontWeight = DWRITE_FONT_WEIGHT::DWRITE_FONT_WEIGHT_REGULAR;
        configuration.fontStyle = DWRITE_FONT_STYLE::DWRITE_FONT_STYLE_ITALIC;
        configuration.size = 16 * scale;
        configuration.ringSize = 14.0 * scale;
        configuration.ringThickness = 1.0 * scale;
        configuration.backgroundColor = { 0.0f, 0.0f, 0.0f, 1.0f };
        configuration.ringColor = { 1.0f, 1.0f, 1.0f, 1.0f };
        configuration.textColor = configuration.ringColor;
        configuration.ringHaloColor = { 0.003f, 0.3f, 1.0f, 0.9f };
        configuration.textHaloColor = configuration.ringHaloColor;
        configuration.haloRadius = 0.5 * scale;
        configuration.text = L"E";
        configuration.outputFile = argv[1];
        configuration.drawDebugInfo = false;

        DoTheJob(configuration);

        return EXIT_SUCCESS;
    }
}
