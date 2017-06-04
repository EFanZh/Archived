use std::mem::*;
use direct2d::math::*;
use directwrite::*;
use directwrite::text_format::*;
use winapi::*;
use com_pointer::*;
use utilities::*;

fn create_head_font_face(dwrite_factory: &Factory) -> ComPointer<IDWriteFontFace> {
    const FONT_FAMILY: &'static str = "Courier New";
    const FONT_SIZE: FLOAT = 24.0;

    let text_format: TextFormat = dwrite_factory
        .create(ParamBuilder::new()
                    .family(FONT_FAMILY)
                    .size(FONT_SIZE)
                    .build()
                    .unwrap())
        .unwrap();

    let mut font_collection = ComPointer::<IDWriteFontCollection>::new();

    unsafe {
        (*text_format.get_raw()).GetFontCollection(font_collection.get_address());
    }

    let font_family_utf_16 = to_utf_16(FONT_FAMILY);
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

fn create_tail_font_face(dwrite_factory: &Factory) -> ComPointer<IDWriteFontFace> {
    return create_head_font_face(dwrite_factory);
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
    pub fn new(dwrite_factory: &Factory) -> Configuration {
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
            head_font_face: create_head_font_face(dwrite_factory),
            tail_font_face: create_tail_font_face(dwrite_factory),
            font_size: 24.0,
        };
    }
}

