#pragma once

#include "stdafx.h"
#include "utilities.h"

namespace com_objects
{
    // Utilities.

    void VerifyHResult(HRESULT result)
    {
        if (FAILED(result))
        {
            const auto error = _com_error(result);

            std::wcerr << error.ErrorMessage() << std::endl;

            abort();
        }
    }

    template <class U, class T>
    Microsoft::WRL::ComPtr<U> As(const Microsoft::WRL::ComPtr<T> &comPtr)
    {
        auto u = Microsoft::WRL::ComPtr<U>();

        VerifyHResult(comPtr.As(&u));

        return u;
    }

    template <class ComInterface>
    class ComObject
    {
        Microsoft::WRL::ComPtr<ComInterface> comObject;

        ComObject(const ComObject &other) = delete;
        ComObject &operator =(const ComObject &other) = delete;

    protected:
        explicit ComObject(Microsoft::WRL::ComPtr<ComInterface> &&comObject) : comObject(move(comObject))
        {
        }

        ComObject(ComObject &&other) = default;

        template <class T>
        Microsoft::WRL::ComPtr<T> AsComPtr()
        {
            return As<T>(comObject);
        }

        ComInterface *GetComObject()
        {
            return comObject.Get();
        }
    };

    // Forward declarations.

    namespace d2d
    {
        class Device;
        class DeviceContext;
    }

    namespace d3d
    {
        class Device;
    }

    namespace dwrite
    {
        class Font;
    }

    namespace wic
    {
        class BitmapEncoder;
        class ImageEncoder;
        class ImagingFactory;
    }

    // Global COM objects.

    class PropertyBag : ComObject<IPropertyBag2>
    {
        friend class wic::BitmapEncoder;

        using ComObject::ComObject;

    public:
        void Write(ULONG cProperties, PROPBAG2 *pPropBag, VARIANT *pvarValue)
        {
            VerifyHResult(this->GetComObject()->Write(cProperties, pPropBag, pvarValue));
        }
    };

    class Stream : protected ComObject<IStream>
    {
        friend class wic::BitmapEncoder;

        using ComObject::ComObject;
    };

    // Components.

    namespace dxgi
    {
        class Device : ComObject<IDXGIDevice>
        {
            friend class d2d::Device;
            friend class d3d::Device;

            using ComObject::ComObject;
        };
    }

    namespace dwrite
    {
        class FontFace : ComObject<IDWriteFontFace1>
        {
            friend class Font;

            using ComObject::ComObject;

        public:
            DWRITE_CARET_METRICS GetCaretMetrics()
            {
                auto caretMetrics = DWRITE_CARET_METRICS{};

                this->GetComObject()->GetCaretMetrics(&caretMetrics);

                return caretMetrics;
            }

            DWRITE_FONT_METRICS GetFontMetrics()
            {
                auto fontMetrics = DWRITE_FONT_METRICS{};

                this->GetComObject()->GetMetrics(&fontMetrics);

                return fontMetrics;
            }

            void GetGlyphIndices(const UINT32 *codePoints, UINT32 codePointCount, UINT16 *glyphIndices)
            {
                VerifyHResult(this->GetComObject()->GetGlyphIndices(codePoints, codePointCount, glyphIndices));
            }

            void GetDesignGlyphMetrics(const UINT16 *glyphIndices,
                                       UINT32 glyphCount,
                                       DWRITE_GLYPH_METRICS *glyphMetrics,
                                       BOOL isSideways = FALSE)
            {
                VerifyHResult(this->GetComObject()->GetDesignGlyphMetrics(glyphIndices, glyphCount, glyphMetrics, isSideways));
            }
        };

        class Font : ComObject<IDWriteFont>
        {
            friend class FontFamily;

            using ComObject::ComObject;

        public:
            FontFace CreateFontFace()
            {
                auto fontFace = Microsoft::WRL::ComPtr<IDWriteFontFace>();

                VerifyHResult(this->GetComObject()->CreateFontFace(&fontFace));

                return FontFace{ As<IDWriteFontFace1>(std::move(fontFace)) };
            }

            DWRITE_FONT_SIMULATIONS GetSimulations()
            {
                return this->GetComObject()->GetSimulations();
            }

            DWRITE_FONT_STRETCH GetStretch()
            {
                return this->GetComObject()->GetStretch();
            }

            DWRITE_FONT_STYLE GetStyle()
            {
                return this->GetComObject()->GetStyle();
            }

            DWRITE_FONT_WEIGHT GetWeight()
            {
                return this->GetComObject()->GetWeight();
            }
        };

        class FontFamily : ComObject<IDWriteFontFamily>
        {
            friend class FontCollection;

            using ComObject::ComObject;

        public:
            Font GetFirstMatchingFont(DWRITE_FONT_WEIGHT weight,
                                      DWRITE_FONT_STRETCH stretch,
                                      DWRITE_FONT_STYLE style)
            {
                auto font = Microsoft::WRL::ComPtr<IDWriteFont>();

                VerifyHResult(this->GetComObject()->GetFirstMatchingFont(weight, stretch, style, &font));

                return Font{ std::move(font) };
            }
        };

        class FontCollection : ComObject<IDWriteFontCollection>
        {
            friend class Factory;

            using ComObject::ComObject;

        public:
            std::optional<UINT32> FindFamilyName(const WCHAR *familyName)
            {
                auto index = UINT32();
                auto exists = BOOL();

                VerifyHResult(this->GetComObject()->FindFamilyName(familyName, &index, &exists));

                if (exists)
                {
                    return index;
                }
                else
                {
                    return std::nullopt;
                }
            }

            FontFamily GetFontFamily(UINT32 index)
            {
                auto fontFamily = Microsoft::WRL::ComPtr<IDWriteFontFamily>();

                VerifyHResult(this->GetComObject()->GetFontFamily(index, &fontFamily));

                return FontFamily{ std::move(fontFamily) };
            }
        };

        class TextFormat : ComObject<IDWriteTextFormat>
        {
            using ComObject::ComObject;

            friend class d2d::DeviceContext;
            friend class Factory;

        public:
            void SetWordWrapping(DWRITE_WORD_WRAPPING wordWrapping)
            {
                VerifyHResult(this->GetComObject()->SetWordWrapping(wordWrapping));
            }
        };

        class TextLayout : ComObject<IDWriteTextLayout2>
        {
            using ComObject::ComObject;

            friend class d2d::DeviceContext;
            friend class Factory;

        public:
            std::vector<DWRITE_LINE_METRICS> GetLineMetrics()
            {
                auto lineMetrics = std::vector<DWRITE_LINE_METRICS>();
                auto lines = UINT32{};

                const auto result = this->GetComObject()->GetLineMetrics(nullptr, 0, &lines);

                if (result == E_NOT_SUFFICIENT_BUFFER)
                {
                    lineMetrics.resize(lines);

                    VerifyHResult(this->GetComObject()->GetLineMetrics(lineMetrics.data(),
                                                                       static_cast<UINT32>(lineMetrics.size()),
                                                                       &lines));
                }
                else
                {
                    VerifyHResult(result);
                }

                return lineMetrics;
            }

            DWRITE_TEXT_METRICS GetTextMetrics()
            {
                auto textMetrics = DWRITE_TEXT_METRICS{};

                VerifyHResult(this->GetComObject()->GetMetrics(&textMetrics));

                return textMetrics;
            }
        };

        class Factory : ComObject<IDWriteFactory>
        {
            using ComObject::ComObject;

        public:
            TextFormat CreateTextFormat(const WCHAR *fontFamilyName,
                                        FontCollection &fontCollection,
                                        DWRITE_FONT_WEIGHT fontWeight,
                                        DWRITE_FONT_STYLE fontStyle,
                                        DWRITE_FONT_STRETCH fontStretch,
                                        FLOAT fontSize,
                                        const WCHAR *localeName)
            {
                auto textFormat = Microsoft::WRL::ComPtr<IDWriteTextFormat>();

                VerifyHResult(this->GetComObject()->CreateTextFormat(fontFamilyName,
                                                                     fontCollection.GetComObject(),
                                                                     fontWeight,
                                                                     fontStyle,
                                                                     fontStretch,
                                                                     fontSize,
                                                                     localeName,
                                                                     &textFormat));

                return TextFormat{ std::move(textFormat) };
            }

            TextLayout CreateTextLayout(const WCHAR *string,
                                        UINT32 stringLength,
                                        TextFormat &textFormat,
                                        FLOAT maxWidth,
                                        FLOAT maxHeight)
            {
                auto textLayout = Microsoft::WRL::ComPtr<IDWriteTextLayout>();

                VerifyHResult(this->GetComObject()->CreateTextLayout(string,
                                                                     stringLength,
                                                                     textFormat.GetComObject(),
                                                                     maxWidth,
                                                                     maxHeight,
                                                                     &textLayout));

                return TextLayout{ As<IDWriteTextLayout2>(std::move(textLayout)) };
            }

            FontCollection GetSystemFontCollection()
            {
                auto systemFontCollection = Microsoft::WRL::ComPtr<IDWriteFontCollection>();

                VerifyHResult(this->GetComObject()->GetSystemFontCollection(&systemFontCollection));

                return FontCollection{ std::move(systemFontCollection) };
            }

            static Factory Create(DWRITE_FACTORY_TYPE factoryType)
            {
                auto factory = Microsoft::WRL::ComPtr<IDWriteFactory>{};

                VerifyHResult(DWriteCreateFactory(factoryType, __uuidof(IDWriteFactory), &factory));

                return Factory{ std::move(factory) };
            }
        };
    }

    namespace d2d
    {
        class Factory : ComObject<ID2D1Factory>
        {
            friend class Device;

            using ComObject::ComObject;
        };

        class Image : protected ComObject<ID2D1Image>
        {
            friend class DeviceContext;
            friend class Effect;
            friend class wic::ImageEncoder;

            using ComObject::ComObject;
        };

        class Bitmap : public Image
        {
            friend class DeviceContext;

            Bitmap(Microsoft::WRL::ComPtr<ID2D1Bitmap1> &&bitmap) : Image(std::move(bitmap))
            {
            }
        };

        class CommandList : public Image
        {
            friend class DeviceContext;

            CommandList(Microsoft::WRL::ComPtr<ID2D1CommandList> &&commandList) : Image(std::move(commandList))
            {
            }

        public:
            void Close()
            {
                VerifyHResult(static_cast<ID2D1CommandList *>(this->GetComObject())->Close());
            }
        };

        class Brush : protected ComObject<ID2D1Brush>
        {
            using ComObject::ComObject;

            friend class DeviceContext;
        };

        class Effect : ComObject<ID2D1Effect>
        {
            using ComObject::ComObject;

            friend class DeviceContext;

        public:
            void SetInput(UINT32 index, Image &input, BOOL invalidate = true)
            {
                this->GetComObject()->SetInput(index, input.GetComObject(), invalidate);
            }

            template <class T, class U>
            void SetValue(U index, const T &value)
            {
                this->GetComObject()->SetValue(index, value);
            }
        };

        class SolidColorBrush : public Brush
        {
            friend class DeviceContext;

            SolidColorBrush(Microsoft::WRL::ComPtr<ID2D1SolidColorBrush> &&solidColorBrush)
                : Brush(std::move(solidColorBrush))
            {
            }
        };

        class ColorContext : ComObject<ID2D1ColorContext>
        {
            friend class DeviceContext;

            using ComObject::ComObject;

        public:
            using ComObject::GetComObject; // TODO: Hide this some time later.
        };

        class DeviceContext : ComObject<ID2D1DeviceContext5>
        {
            using ComObject::ComObject;

            friend class Device;

        public:
            void BeginDraw()
            {
                this->GetComObject()->BeginDraw();
            }

            void Clear(const D2D1_COLOR_F &clearColor)
            {
                this->GetComObject()->Clear(clearColor);
            }

            Bitmap CreateBitmap(D2D1_SIZE_U size,
                                const void *sourceData,
                                UINT32 pitch,
                                const D2D1_BITMAP_PROPERTIES1 &bitmapProperties)
            {
                auto bitmap = Microsoft::WRL::ComPtr<ID2D1Bitmap1>();

                VerifyHResult(this->GetComObject()->CreateBitmap(size, sourceData, pitch, bitmapProperties, &bitmap));

                return bitmap;
            }

            ColorContext CreateColorContext(D2D1_COLOR_SPACE space, BYTE *profile, UINT32 profileSize)
            {
                auto colorContext = Microsoft::WRL::ComPtr<ID2D1ColorContext>{};

                VerifyHResult(this->GetComObject()->CreateColorContext(space, profile, profileSize, &colorContext));

                return ColorContext{ std::move(colorContext) };
            }

            ColorContext CreateColorContextFromDxgiColorSpace(DXGI_COLOR_SPACE_TYPE colorSpace)
            {
                auto colorContext = Microsoft::WRL::ComPtr<ID2D1ColorContext1>{};

                VerifyHResult(this->GetComObject()->CreateColorContextFromDxgiColorSpace(colorSpace, &colorContext));

                return ColorContext{ std::move(colorContext) };
            }

            CommandList CreateCommandList()
            {
                auto commandList = Microsoft::WRL::ComPtr<ID2D1CommandList>{};

                VerifyHResult(this->GetComObject()->CreateCommandList(&commandList));

                return CommandList{ std::move(commandList) };
            }

            Effect CreateEffect(REFCLSID effectId)
            {
                auto effect = Microsoft::WRL::ComPtr<ID2D1Effect>{};

                VerifyHResult(this->GetComObject()->CreateEffect(effectId, &effect));

                return Effect{ std::move(effect) };
            }

            SolidColorBrush CreateSolidColorBrush(const D2D1_COLOR_F &color)
            {
                auto solidColorBrush = Microsoft::WRL::ComPtr<ID2D1SolidColorBrush>();

                VerifyHResult(this->GetComObject()->CreateSolidColorBrush(color, &solidColorBrush));

                return SolidColorBrush{ std::move(solidColorBrush) };
            }

            void DrawEllipse(const D2D1_ELLIPSE &ellipse,
                             Brush &brush,
                             FLOAT strokeWidth = 1.0f,
                             ID2D1StrokeStyle *strokeStyle = nullptr)
            {
                this->GetComObject()->DrawEllipse(ellipse, brush.GetComObject(), strokeWidth, strokeStyle);
            }

            void DrawImage(
                Effect &effect,
                D2D1_INTERPOLATION_MODE interpolationMode = D2D1_INTERPOLATION_MODE::D2D1_INTERPOLATION_MODE_LINEAR,
                D2D1_COMPOSITE_MODE compositeMode = D2D1_COMPOSITE_MODE::D2D1_COMPOSITE_MODE_SOURCE_OVER)
            {
                this->GetComObject()->DrawImage(effect.GetComObject(),
                                                nullptr,
                                                nullptr,
                                                interpolationMode,
                                                compositeMode);
            }

            void DrawImage(
                Image &image,
                D2D1_INTERPOLATION_MODE interpolationMode = D2D1_INTERPOLATION_MODE::D2D1_INTERPOLATION_MODE_LINEAR,
                D2D1_COMPOSITE_MODE compositeMode = D2D1_COMPOSITE_MODE::D2D1_COMPOSITE_MODE_SOURCE_OVER)
            {
                this->GetComObject()->DrawImage(image.GetComObject(),
                                                nullptr,
                                                nullptr,
                                                interpolationMode,
                                                compositeMode);
            }

            void DrawLine(D2D1_POINT_2F point0,
                          D2D1_POINT_2F point1,
                          Brush &brush,
                          FLOAT strokeWidth = 1.0f,
                          ID2D1StrokeStyle *strokeStyle = nullptr)
            {
                this->GetComObject()->DrawLine(point0, point1, brush.GetComObject(), strokeWidth, strokeStyle);
            }

            void DrawText(const WCHAR *string,
                          UINT32 stringLength,
                          dwrite::TextFormat &textFormat,
                          const D2D1_RECT_F &layoutRect,
                          Brush &defaultFillBrush,
                          D2D1_DRAW_TEXT_OPTIONS options = D2D1_DRAW_TEXT_OPTIONS::D2D1_DRAW_TEXT_OPTIONS_NONE,
                          DWRITE_MEASURING_MODE measuringMode = DWRITE_MEASURING_MODE::DWRITE_MEASURING_MODE_NATURAL)
            {
                this->GetComObject()->DrawText(string,
                                               stringLength,
                                               textFormat.GetComObject(),
                                               layoutRect,
                                               defaultFillBrush.GetComObject(),
                                               options,
                                               measuringMode);
            }

            void DrawTextLayout(D2D1_POINT_2F origin,
                                dwrite::TextLayout &textLayout,
                                Brush &defaultFillBrush,
                                D2D1_DRAW_TEXT_OPTIONS options = D2D1_DRAW_TEXT_OPTIONS::D2D1_DRAW_TEXT_OPTIONS_NONE)
            {
                this->GetComObject()->DrawTextLayout(origin,
                                                     textLayout.GetComObject(),
                                                     defaultFillBrush.GetComObject(),
                                                     options);
            }

            std::tuple<D2D1_TAG, D2D1_TAG> EndDraw()
            {
                auto tag1 = D2D1_TAG{};
                auto tag2 = D2D1_TAG{};

                VerifyHResult(this->GetComObject()->EndDraw(&tag1, &tag2));

                return { tag1, tag2 };
            }

            Image GetTarget()
            {
                auto image = Microsoft::WRL::ComPtr<ID2D1Image>{};

                this->GetComObject()->GetTarget(&image);

                return Image{ std::move(image) };
            }

            void SetTarget(Image &image)
            {
                this->GetComObject()->SetTarget(image.GetComObject());
            }
        };

        class Device : ComObject<ID2D1Device>
        {
            friend class wic::ImagingFactory;

            using ComObject::ComObject;

        public:
            DeviceContext CreateDeviceContext(D2D1_DEVICE_CONTEXT_OPTIONS options)
            {
                auto deviceContext = Microsoft::WRL::ComPtr<ID2D1DeviceContext>();

                VerifyHResult(this->GetComObject()->CreateDeviceContext(options, &deviceContext));

                return DeviceContext{ As<ID2D1DeviceContext5>(deviceContext) };
            }

            Factory GetFactory()
            {
                Microsoft::WRL::ComPtr<ID2D1Factory> factory;

                this->GetComObject()->GetFactory(&factory);

                return Factory{ std::move(factory) };
            }

            static Device Create(dxgi::Device &dxgiDevice, const D2D1_CREATION_PROPERTIES &creationProperties)
            {
                auto d2DDevice = Microsoft::WRL::ComPtr<ID2D1Device>();

                VerifyHResult(D2D1CreateDevice(dxgiDevice.GetComObject(), creationProperties, &d2DDevice));

                return Device{ std::move(d2DDevice) };
            }
        };
    }

    namespace d3d
    {
        class Device : ComObject<ID3D11Device>
        {
            using ComObject::ComObject;

        public:
            dxgi::Device AsDxgiDevice()
            {
                return dxgi::Device{ this->AsComPtr<IDXGIDevice>() };
            }

            static Device Create(IDXGIAdapter *pAdapter,
                                 D3D_DRIVER_TYPE DriverType,
                                 HMODULE Software,
                                 D3D11_CREATE_DEVICE_FLAG Flags,
                                 const D3D_FEATURE_LEVEL *pFeatureLevels,
                                 UINT FeatureLevels,
                                 UINT SDKVersion)
            {
                auto d3D11Device = Microsoft::WRL::ComPtr<ID3D11Device>{};

                VerifyHResult(D3D11CreateDevice(pAdapter,
                                                DriverType,
                                                Software,
                                                Flags,
                                                pFeatureLevels,
                                                FeatureLevels,
                                                SDKVersion,
                                                &d3D11Device,
                                                nullptr,
                                                nullptr));

                return Device{ std::move(d3D11Device) };
            }
        };
    }

    namespace wic
    {
        class Stream : public com_objects::Stream
        {
            friend class ImagingFactory;

            Stream(Microsoft::WRL::ComPtr<IStream> &&stream) : com_objects::Stream(std::move(stream))
            {
            }

        public:
            void InitializeFromFilename(LPCWSTR wzFileName,
                                        DWORD dwDesiredAccess)
            {
                VerifyHResult(
                    static_cast<IWICStream *>(this->GetComObject())->InitializeFromFilename(wzFileName, dwDesiredAccess));
            }
        };

        class ColorContext : ComObject<IWICColorContext>
        {
            friend class BitmapFrameEncode;
            friend class ImagingFactory;

            using ComObject::ComObject;

        public:
            WICColorContextType GetType()
            {
                auto type = WICColorContextType{};

                VerifyHResult(this->GetComObject()->GetType(&type));

                return type;
            }

            void InitializeFromExifColorSpace(UINT value)
            {
                VerifyHResult(this->GetComObject()->InitializeFromExifColorSpace(value));
            }

            void InitializeFromFilename(LPCWSTR wzFilename)
            {
                VerifyHResult(this->GetComObject()->InitializeFromFilename(wzFilename));
            }
        };

        class MetadataQueryReader : protected ComObject<IWICMetadataQueryReader>
        {
            using ComObject::ComObject;
        };

        class MetadataQueryWriter : public MetadataQueryReader
        {
            MetadataQueryWriter(Microsoft::WRL::ComPtr<IWICMetadataQueryWriter> &&metadataQueryWriter)
                : MetadataQueryReader{ std::move(metadataQueryWriter) }
            {
            }

            friend class BitmapEncoder;
            friend class BitmapFrameEncode;

        public:
            void SetMetadataByName(LPCWSTR wzName, const PROPVARIANT &varValue)
            {
                VerifyHResult(
                    static_cast<IWICMetadataQueryWriter *>(this->GetComObject())->SetMetadataByName(wzName, &varValue));
            }
        };

        class BitmapFrameEncode : ComObject<IWICBitmapFrameEncode>
        {
            friend class BitmapEncoder;
            friend class ImageEncoder;

            using ComObject::ComObject;

        public:
            void Commit()
            {
                VerifyHResult(this->GetComObject()->Commit());
            }

            MetadataQueryWriter GetMetadataQueryWriter()
            {
                auto metadataQueryWriter = Microsoft::WRL::ComPtr<IWICMetadataQueryWriter>{};

                VerifyHResult(this->GetComObject()->GetMetadataQueryWriter(&metadataQueryWriter));

                return MetadataQueryWriter{ std::move(metadataQueryWriter) };
            }

            void Initialize()
            {
                VerifyHResult(this->GetComObject()->Initialize(nullptr));
            }

            void SetColorContext(ColorContext &colorContext)
            {
                auto rawColorContext = colorContext.GetComObject();

                VerifyHResult(this->GetComObject()->SetColorContexts(1, &rawColorContext));
            }

            WICPixelFormatGUID SetPixelFormat(const WICPixelFormatGUID &pixelFormat)
            {
                auto tempPixelFormat = pixelFormat;

                VerifyHResult(this->GetComObject()->SetPixelFormat(&tempPixelFormat));

                return tempPixelFormat;
            }

            void SetResolution(double dpiX, double dpiY)
            {
                VerifyHResult(this->GetComObject()->SetResolution(dpiX, dpiY));
            }
        };

        class BitmapEncoder : ComObject<IWICBitmapEncoder>
        {
            friend class ImagingFactory;

            using ComObject::ComObject;

        public:
            void Commit()
            {
                VerifyHResult(this->GetComObject()->Commit());
            }

            std::tuple<BitmapFrameEncode, PropertyBag> CreateNewFrame()
            {
                auto bitmapFrameEncode = Microsoft::WRL::ComPtr<IWICBitmapFrameEncode>();
                auto encoderOptions = Microsoft::WRL::ComPtr<IPropertyBag2>{};

                VerifyHResult(this->GetComObject()->CreateNewFrame(&bitmapFrameEncode, &encoderOptions));

                return { BitmapFrameEncode{ std::move(bitmapFrameEncode) }, PropertyBag{ std::move(encoderOptions) } };
            }

            MetadataQueryWriter GetMetadataQueryWriter()
            {
                auto metadataQueryWriter = Microsoft::WRL::ComPtr<IWICMetadataQueryWriter>{};

                VerifyHResult(this->GetComObject()->GetMetadataQueryWriter(&metadataQueryWriter));

                return MetadataQueryWriter{ std::move(metadataQueryWriter) };
            }

            void Initialize(Stream &stream, WICBitmapEncoderCacheOption cacheOption)
            {
                VerifyHResult(this->GetComObject()->Initialize(stream.GetComObject(), cacheOption));
            }
        };

        class ImageEncoder : ComObject<IWICImageEncoder>
        {
            friend class ImagingFactory;

            using ComObject::ComObject;

        public:
            void WriteFrame(d2d::Image &image, BitmapFrameEncode &frameEncode)
            {
                VerifyHResult(this->GetComObject()->WriteFrame(image.GetComObject(), frameEncode.GetComObject(), nullptr));
            }
        };

        class ImagingFactory : ComObject<IWICImagingFactory2>
        {
            using ComObject::ComObject;

        public:
            BitmapEncoder CreateEncoder(REFGUID guidContainerFormat, const GUID *pguidVendor)
            {
                auto bitmapEncoder = Microsoft::WRL::ComPtr<IWICBitmapEncoder>();

                VerifyHResult(this->GetComObject()->CreateEncoder(guidContainerFormat, pguidVendor, &bitmapEncoder));

                return BitmapEncoder{ std::move(bitmapEncoder) };
            }

            ColorContext CreateColorContext()
            {
                auto colorContext = Microsoft::WRL::ComPtr<IWICColorContext>{};

                VerifyHResult(this->GetComObject()->CreateColorContext(&colorContext));

                return ColorContext{ std::move(colorContext) };
            }

            ImageEncoder CreateImageEncoder(d2d::Device &d2DDevice)
            {
                auto imageEncoder = Microsoft::WRL::ComPtr<IWICImageEncoder>();

                VerifyHResult(this->GetComObject()->CreateImageEncoder(d2DDevice.GetComObject(), &imageEncoder));

                return ImageEncoder{ std::move(imageEncoder) };
            }

            Stream CreateStream()
            {
                auto stream = Microsoft::WRL::ComPtr<IWICStream>();

                VerifyHResult(this->GetComObject()->CreateStream(&stream));

                return { std::move(stream) };
            }

            static ImagingFactory Create()
            {
                auto wicImagingFactory = Microsoft::WRL::ComPtr<IWICImagingFactory2>();

                VerifyHResult(CoCreateInstance(CLSID_WICImagingFactory2,
                                               nullptr,
                                               CLSCTX::CLSCTX_INPROC_SERVER,
                                               IID_PPV_ARGS(&wicImagingFactory)));

                return ImagingFactory{ std::move(wicImagingFactory) };
            }
        };
    }
}
