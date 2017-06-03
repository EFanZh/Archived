use direct2d::math::*;
use directwrite::*;
use directwrite::text_format::*;
use winapi::*;

fn create_head_font(dwrite_factory: &Factory) -> TextFormat {
    const FONT_SIZE: FLOAT = 24.0;

    let result: TextFormat = dwrite_factory
        .create(ParamBuilder::new()
                    .family("Courier New")
                    .size(FONT_SIZE)
                    .build()
                    .unwrap())
        .unwrap();

    unsafe {
        let raw_text_format = &mut *result.get_raw();

        {
            let result = raw_text_format.SetTextAlignment(DWRITE_TEXT_ALIGNMENT_CENTER);

            debug_assert!(SUCCEEDED(result));
        }

        {
            let result = raw_text_format.SetWordWrapping(DWRITE_WORD_WRAPPING_NO_WRAP);

            debug_assert!(SUCCEEDED(result));
        }
    }

    return result;
}

fn create_tail_font(dwrite_factory: &Factory) -> TextFormat {
    return create_head_font(dwrite_factory);
}

pub struct Configuration {
    pub cell_width: f64,
    pub cell_height: f64,
    pub background_color: ColorF,
    pub head_color: ColorF,
    pub tail_color_1: ColorF,
    pub tail_color_2: ColorF,
    pub head_font: TextFormat,
    pub tail_font: TextFormat,
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
            head_font: create_head_font(dwrite_factory),
            tail_font: create_tail_font(dwrite_factory),
        };
    }
}

