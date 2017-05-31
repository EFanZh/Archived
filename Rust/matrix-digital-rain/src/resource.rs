use direct2d::*;
use direct2d::brush::*;
use direct2d::math::*;
use configuration::*;

pub struct Resource {
    pub head_brush: SolidColor,
    pub tail_brush: SolidColor,
}

impl Resource {
    pub fn new(render_target: &RenderTarget, configuration: &Configuration) -> Resource {
        return Resource {
            head_brush: render_target
                .create_solid_color_brush(configuration.head_color, &BrushProperties::default())
                .unwrap(),
            tail_brush: render_target
                .create_solid_color_brush(configuration.tail_color_1, &BrushProperties::default())
                .unwrap(),
        };
    }
}

