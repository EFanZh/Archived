use std::mem::*;
use direct2d::math::*;
use dwrite::*;
use winapi::*;
use com_pointer::*;
use utilities::*;

fn create_head_font_face(font_collection: &mut IDWriteFontCollection)
                         -> ComPointer<IDWriteFontFace> {
    let font_family_utf_16 = to_utf_16("Courier New");
    let mut font_family = ComPointer::<IDWriteFontFamily>::new();
    let mut matching_font = ComPointer::<IDWriteFont>::new();
    let mut font_face = ComPointer::<IDWriteFontFace>::new();

    unsafe {
        let mut index = uninitialized();
        let mut exists = uninitialized();

        font_collection.FindFamilyName(font_family_utf_16.as_ptr(), &mut index, &mut exists);
        font_collection.GetFontFamily(index, font_family.get_address());

        font_family.GetFirstMatchingFont(DWRITE_FONT_WEIGHT_BOLD,
                                         DWRITE_FONT_STRETCH_NORMAL,
                                         DWRITE_FONT_STYLE_NORMAL,
                                         matching_font.get_address());

        matching_font.CreateFontFace(font_face.get_address());
    }

    return font_face;
}

fn create_tail_font_face(font_collection: &mut IDWriteFontCollection)
                         -> ComPointer<IDWriteFontFace> {
    return create_head_font_face(font_collection);
}

pub struct Configuration {
    pub cell_width: f64,
    pub cell_height: f64,
    pub background_color: ColorF,
    pub head_color: ColorF,
    pub tail_color_1: ColorF,
    pub tail_color_2: ColorF,
    pub head_font_face: ComPointer<IDWriteFontFace>,
    pub tail_font_face: ComPointer<IDWriteFontFace>,
    pub font_size: f64,
}

impl Configuration {
    pub fn new() -> Configuration {
        let mut dwrite_factory = ComPointer::<IDWriteFactory>::new();
        let mut font_collection = ComPointer::<IDWriteFontCollection>::new();

        let dwrite_factory_iid = IID {
            Data1: 0xb859ee5a,
            Data2: 0xd838,
            Data3: 0x4b5b,
            Data4: [0xa2, 0xe8, 0x1a, 0xdc, 0x7d, 0x93, 0xdb, 0x48],
        };

        unsafe {
            let dwrite_factory_result = DWriteCreateFactory(DWRITE_FACTORY_TYPE_SHARED,
                                                            &dwrite_factory_iid,
                                                            dwrite_factory.get_address() as _);

            debug_assert!(SUCCEEDED(dwrite_factory_result));

            let font_collection_result =
                dwrite_factory.GetSystemFontCollection(font_collection.get_address(), TRUE);

            debug_assert!(SUCCEEDED(font_collection_result));
        }

        return Configuration {
            cell_width: 24.0,
            cell_height: 24.0,
            background_color: ColorF(D2D1_COLOR_F {
                                         r: 0.0,
                                         g: 0.0,
                                         b: 0.0,
                                         a: 0.0,
                                     }),
            head_color: ColorF(D2D1_COLOR_F {
                                   r: 0.3,
                                   g: 1.0,
                                   b: 0.3,
                                   a: 1.0,
                               }),
            tail_color_1: ColorF(D2D1_COLOR_F {
                                     r: 0.0,
                                     g: 0.8,
                                     b: 0.0,
                                     a: 1.0,
                                 }),
            tail_color_2: ColorF(D2D1_COLOR_F {
                                     r: 0.0,
                                     g: 0.8,
                                     b: 0.0,
                                     a: 0.0,
                                 }),
            head_font_face: create_head_font_face(&mut font_collection),
            tail_font_face: create_tail_font_face(&mut font_collection),
            font_size: 24.0,
        };
    }
}

