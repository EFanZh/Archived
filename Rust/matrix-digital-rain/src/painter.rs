use std::cmp::*;
use std::mem::*;
use direct2d::*;
use direct2d::brush::*;
use winapi::*;
use backend::*;
use configuration::*;
use raindrop::*;
use resource::*;

fn draw_character(render_target: &mut RenderTarget,
                  font_face: &mut IDWriteFontFace,
                  font_size: f64,
                  character: char,
                  x: f64,
                  y: f64,
                  brush: &SolidColor) {
    unsafe {
        let raw_render_target = &mut *render_target.hwnd_rt().unwrap().raw_value();

        let baseline_origin = D2D1_POINT_2F {
            x: x as _,
            y: y as _,
        };

        let mut glyph_indices = uninitialized::<[UINT16; 1]>();

        (*font_face).GetGlyphIndices(&(character as _), 1, glyph_indices.as_mut_ptr());

        let glyph_advances = [0f32];
        let glyph_offsets = [DWRITE_GLYPH_OFFSET {
                                 advanceOffset: 0.0,
                                 ascenderOffset: 0.0,
                             }];

        let glyph_run = DWRITE_GLYPH_RUN {
            fontFace: font_face,
            fontEmSize: font_size as _,
            glyphCount: 1,
            glyphIndices: glyph_indices.as_ptr(),
            glyphAdvances: glyph_advances.as_ptr(),
            glyphOffsets: glyph_offsets.as_ptr(),
            isSideways: FALSE,
            bidiLevel: 0,
        };

        let foreground_brush = brush.get_ptr();
        let measuring_mode = DWRITE_MEASURING_MODE_NATURAL;

        raw_render_target.DrawGlyphRun(baseline_origin,
                                       &glyph_run,
                                       foreground_brush,
                                       measuring_mode);
    }
}

fn draw_raindrop(raindrop: &Raindrop,
                 column: usize,
                 rows: usize,
                 configuration: &mut Configuration,
                 resource: &Resource,
                 render_target: &mut RenderTarget) {

    let integer_position = raindrop.position as usize;

    let row_start = if integer_position < rows {
        0
    } else {
        integer_position - rows
    };

    for row in row_start..min(integer_position + 1, raindrop.get_size()) {
        let text = raindrop.characters[row];
        let x = configuration.cell_width * (column as f64);
        let y = configuration.cell_height * ((integer_position - row) as f64);
        let position = ((row as f64) + raindrop.position % 1.0) / (raindrop.get_size() as f64);
        let tail_brush = resource.get_tail_brush(1.0 - (1.0 - position).powf(1.6));

        if row == 0 {
            draw_character(render_target,
                           &mut configuration.head_font_face,
                           24.0,
                           text,
                           x,
                           y,
                           resource.get_head_brush());
        } else {
            draw_character(render_target,
                           &mut configuration.tail_font_face,
                           24.0,
                           text,
                           x,
                           y,
                           tail_brush);
        }
    }
}

pub fn draw_scene(backend: &mut Backend,
                  time_ellapsed: f64,
                  render_target: &mut RenderTarget,
                  configuration: &mut Configuration,
                  resource: &Resource) {
    let size = render_target.get_size();
    let columns = ((size.width as f64) / configuration.cell_width).ceil() as usize;
    let rows = ((size.width as f64) / configuration.cell_width).ceil() as usize;
    let view = backend.get_view(columns, rows, time_ellapsed);

    render_target.clear(&configuration.background_color);

    for column in 0..columns {
        for raindrop in &view[column] {
            draw_raindrop(raindrop,
                          column,
                          rows,
                          configuration,
                          resource,
                          render_target);
        }
    }
}

