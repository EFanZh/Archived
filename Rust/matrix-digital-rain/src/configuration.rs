use directwrite::*;
use directwrite::text_format::*;
use winapi::*;

pub struct Configuration {
    pub background_color: D2D1_COLOR_F,
    pub head_color: D2D1_COLOR_F,
    pub tail_color_1: D2D1_COLOR_F,
    pub tail_color_2: D2D1_COLOR_F,
    pub head_font: TextFormat,
    pub tail_font: TextFormat,
}

impl Configuration {
    pub fn new(dwrite_factory: &Factory) -> Configuration {
        let font_size = 18.0;

        return Configuration {
            background_color: D2D1_COLOR_F {
                r: 0.0,
                g: 0.0,
                b: 0.0,
                a: 0.0,
            },
            head_color: D2D1_COLOR_F {
                r: 0.3,
                g: 1.0,
                b: 0.3,
                a: 1.0,
            },
            tail_color_1: D2D1_COLOR_F {
                r: 0.0,
                g: 0.8,
                b: 0.0,
                a: 1.0,
            },
            tail_color_2: D2D1_COLOR_F {
                r: 0.0,
                g: 0.8,
                b: 0.0,
                a: 0.0,
            },
            head_font: dwrite_factory
                .create(ParamBuilder::new()
                            .family("Courier New")
                            .size(font_size)
                            .build()
                            .unwrap())
                .unwrap(),
            tail_font: dwrite_factory
                .create(ParamBuilder::new()
                            .family("Courier New")
                            .size(font_size)
                            .build()
                            .unwrap())
                .unwrap(),
        };
    }
}

