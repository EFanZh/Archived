use direct2d::comptr::*;
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

fn create_brush(render_target: &mut ID2D1HwndRenderTarget,
                color: &ColorF)
                -> ComPtr<ID2D1SolidColorBrush> {
    let mut result = ComPtr::<ID2D1SolidColorBrush>::new();

    unsafe {
        let brush_result =
            render_target
                .CreateSolidColorBrush(&color.0, &BrushProperties::default().0, result.raw_addr());

        debug_assert!(SUCCEEDED(brush_result));
    }

    return result;
}

pub struct Resource {
    head_brush: ComPtr<ID2D1SolidColorBrush>,
    tail_brushes: Vec<ComPtr<ID2D1SolidColorBrush>>,
}

impl Resource {
    pub fn new(render_target: &mut ID2D1HwndRenderTarget,
               configuration: &Configuration)
               -> Resource {
        let get_tail_brush_color = |i| {
            generate_color_gradient(configuration.tail_color_1,
                                    configuration.tail_color_2,
                                    (i as f64) / (TAIL_SHADES as f64))
        };

        return Resource {
            head_brush: create_brush(render_target, &configuration.head_color),
            tail_brushes: (0..TAIL_SHADES)
                .map(|i| create_brush(render_target, &get_tail_brush_color(i)))
                .collect(),
        };
    }

    pub fn get_head_brush(&mut self) -> &mut ID2D1SolidColorBrush {
        return &mut self.head_brush;
    }

    pub fn get_tail_brush(&mut self, position: f64) -> &mut ID2D1SolidColorBrush {
        return &mut self.tail_brushes[(position * (TAIL_SHADES as f64)) as usize];
    }
}

