use std::cmp::*;
use direct2d::*;
use direct2d::math::*;
use direct2d::render_target::*;
use winapi::*;
use backend::*;
use configuration::*;
use raindrop::*;
use resource::*;

fn draw_raindrop(raindrop: &Raindrop,
                 column: usize,
                 rows: usize,
                 configuration: &Configuration,
                 resource: &Resource,
                 render_target: &mut RenderTarget) {

    let integer_position = raindrop.position as usize;

    let row_start = if integer_position < rows {
        0
    } else {
        integer_position - rows
    };

    for row in row_start..min(integer_position + 1, raindrop.get_size()) {
        let text = raindrop.characters[row].to_string();
        let x = configuration.cell_width * (column as f64);
        let y = configuration.cell_height * ((integer_position - row) as f64);
        let position = ((row as f64) + raindrop.position % 1.0) / (raindrop.get_size() as f64);
        let brush = resource.get_tail_brush(1.0 - (1.0 - position).powf(1.6));

        if row == 0 {
            render_target.draw_text(text.as_str(),
                                    &configuration.head_font,
                                    &RectF::new(x as FLOAT,
                                                y as FLOAT,
                                                (x + configuration.cell_width) as FLOAT,
                                                (y + configuration.cell_height) as FLOAT),
                                    resource.get_head_brush(),
                                    &[DrawTextOption::NoSnap]);
        } else {
            render_target.draw_text(text.as_str(),
                                    &configuration.tail_font,
                                    &RectF::new(x as FLOAT,
                                                y as FLOAT,
                                                (x + configuration.cell_width) as FLOAT,
                                                (y + configuration.cell_height) as FLOAT),
                                    brush,
                                    &[DrawTextOption::NoSnap]);
        }
    }
}

pub fn draw_scene(backend: &mut Backend,
                  time_ellapsed: f64,
                  render_target: &mut RenderTarget,
                  configuration: &Configuration,
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

