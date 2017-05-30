use direct2d::comptr::*;
use direct2d::math::*;
use winapi::*;
use configuration::*;

pub struct Resource {
    pub head_brush: ComPtr<ID2D1SolidColorBrush>,
    pub tail_brush: ComPtr<ID2D1SolidColorBrush>,
}

impl Resource {
    pub fn new(render_target: &mut ID2D1HwndRenderTarget,
               configuration: &Configuration)
               -> Resource {
        unsafe {
            let mut head_brush = ComPtr::<ID2D1SolidColorBrush>::new();
            let mut tail_brush = ComPtr::<ID2D1SolidColorBrush>::new();

            let head_result =
                render_target.CreateSolidColorBrush(&configuration.head_color,
                                                    &BrushProperties::default().0,
                                                    head_brush.raw_addr());

            debug_assert!(SUCCEEDED(head_result));

            let tail_result =
                render_target.CreateSolidColorBrush(&configuration.tail_color_1,
                                                    &BrushProperties::default().0,
                                                    tail_brush.raw_addr());

            debug_assert!(SUCCEEDED(tail_result));

            return Resource {
                head_brush: head_brush,
                tail_brush: tail_brush,
            };
        }
    }
}

