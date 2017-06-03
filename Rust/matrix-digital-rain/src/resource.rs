use direct2d::*;
use direct2d::brush::*;
use direct2d::math::*;
use winapi::*;
use configuration::*;

const TAIL_SHADES: usize = 256;

fn generate_color_component(from: FLOAT, to: FLOAT, position: f64) -> FLOAT {
    return from + (to - from) * (position as FLOAT);
}

fn generate_color_gradient(from: ColorF, to: ColorF, position: f64) -> ColorF {
    return ColorF(D2D1_COLOR_F {
                      r: generate_color_component(from.0.r, to.0.r, position),
                      g: generate_color_component(from.0.g, to.0.g, position),
                      b: generate_color_component(from.0.b, to.0.b, position),
                      a: generate_color_component(from.0.a, to.0.a, position),
                  });
}

pub struct Resource {
    head_brush: SolidColor,
    tail_brushes: Vec<SolidColor>,
}

impl Resource {
    pub fn new(render_target: &RenderTarget, configuration: &Configuration) -> Resource {
        let get_tail_brush_color = |i| {
            generate_color_gradient(configuration.tail_color_1,
                                    configuration.tail_color_2,
                                    (i as f64) / (TAIL_SHADES as f64))
        };


        return Resource {
            head_brush: render_target
                .create_solid_color_brush(configuration.head_color, &BrushProperties::default())
                .unwrap(),
            tail_brushes: (0..TAIL_SHADES)
                .map(|i| {
                         render_target
                             .create_solid_color_brush(get_tail_brush_color(i),
                                                       &BrushProperties::default())
                             .unwrap()
                     })
                .collect(),
        };
    }

    pub fn get_head_brush(&self) -> &SolidColor {
        return &self.head_brush;
    }

    pub fn get_tail_brush(&self, position: f64) -> &SolidColor {
        return &self.tail_brushes[(position * (TAIL_SHADES as f64)) as usize];
    }
}

