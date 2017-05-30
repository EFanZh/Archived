use std::mem::*;
use std::ptr::*;
use direct2d::*;
use direct2d::brush::*;
use direct2d::comptr::*;
use direct2d::math::*;
use direct2d::render_target::*;
use directwrite;
use directwrite::text_format;
use directwrite::text_format::*;
use user32::*;
use winapi::*;
use utilities::*;

struct Configuration {
    background_color: D2D1_COLOR_F,
    head_color: D2D1_COLOR_F,
    tail_color_1: D2D1_COLOR_F,
    tail_color_2: D2D1_COLOR_F,
    head_font: TextFormat,
    tail_font: TextFormat,
}

struct Resource {
    head_brush: ComPtr<ID2D1SolidColorBrush>,
    tail_brush: ComPtr<ID2D1SolidColorBrush>,
}

impl Resource {
    fn new(render_target: &mut ID2D1HwndRenderTarget, configuration: &Configuration) -> Resource {
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

impl Configuration {
    pub fn new(dwrite_factory: &directwrite::Factory) -> Configuration {
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
                .create(text_format::ParamBuilder::new()
                            .family("Courier New")
                            .size(font_size)
                            .build()
                            .unwrap())
                .unwrap(),
            tail_font: dwrite_factory
                .create(text_format::ParamBuilder::new()
                            .family("Courier New")
                            .size(font_size)
                            .build()
                            .unwrap())
                .unwrap(),
        };
    }
}

pub struct Window {
    handle: HWND,
    configuration: Configuration,
    d2d_factory: Factory,
    dwrite_factory: directwrite::Factory,
    render_target: ComPtr<ID2D1HwndRenderTarget>,
    resource: Option<Resource>,
}

impl Window {
    pub fn new() -> Window {
        let dwrite_factory = directwrite::Factory::new().unwrap();
        let configuration = Configuration::new(&dwrite_factory);

        return Window {
            handle: null_mut(),
            d2d_factory: Factory::new().unwrap(),
            dwrite_factory: dwrite_factory,
            render_target: ComPtr::new(),
            configuration: configuration,
            resource: None,
        };
    }

    pub fn set_handle(&mut self, handle: HWND) {
        debug_assert!(self.handle == null_mut());
        debug_assert!(handle != null_mut());

        self.handle = handle;
    }

    pub fn window_proc(&mut self, u_msg: UINT, w_param: WPARAM, l_param: LPARAM) -> LRESULT {
        return match u_msg {
            WM_CREATE => self.on_create(),
            WM_DESTROY => self.on_destroy(),
            WM_PAINT => self.on_paint(),
            _ => unsafe { DefWindowProcW(self.handle, u_msg, w_param, l_param) },
        };
    }

    fn on_create(&mut self) -> LRESULT {
        let render_target = self.d2d_factory
            .create_render_target(WindowRenderTargetBacking::new(self))
            .unwrap();

        unsafe {
            self.render_target = render_target.hwnd_rt().unwrap();
            self.resource = Some(Resource::new(&mut self.render_target, &self.configuration));
        }

        return 0;
    }

    fn on_destroy(&self) -> LRESULT {
        unsafe {
            PostQuitMessage(0);
        }

        return 0;
    }

    fn on_paint(&mut self) -> LRESULT {
        debug_assert!(!self.render_target.is_null());

        let (width, height) = self.get_client_size();

        unsafe {
            self.render_target.Resize(&D2D1_SIZE_U {
                                          width: width,
                                          height: height,
                                      });

            self.render_target.BeginDraw();
        }

        Window::draw_scene(&mut self.render_target,
                           &self.configuration,
                           self.resource.as_ref().unwrap());

        unsafe {
            let mut _tag_1 = uninitialized();
            let mut _tag_2 = uninitialized();
            let result = self.render_target.EndDraw(&mut _tag_1, &mut _tag_2);

            debug_assert!(SUCCEEDED(result));
        }

        return 0;
    }

    fn get_client_size(&self) -> (u32, u32) {
        unsafe {
            let mut rect = uninitialized();
            let result = GetClientRect(self.handle, &mut rect);

            debug_assert!(result != 0);

            return ((rect.right - rect.left) as _, (rect.bottom - rect.top) as _);
        }
    }

    fn draw_scene(render_target: &mut ID2D1HwndRenderTarget,
                  configuration: &Configuration,
                  resource: &Resource) {
        unsafe {
            render_target.Clear(&configuration.background_color);

            let text = to_utf_16("Test");

            render_target.DrawText(text.as_ptr(),
                                   (text.len() - 1) as _,
                                   configuration.head_font.get_raw(),
                                   &D2D1_RECT_F {
                                       left: 10.0,
                                       top: 10.0,
                                       right: 30.0,
                                       bottom: 40.0,
                                   },
                                   resource.head_brush.raw_value() as _,
                                   D2D1_DRAW_TEXT_OPTIONS_NONE,
                                   DWRITE_MEASURING_MODE_NATURAL);
        }
    }
}

struct WindowRenderTargetBacking {
    handle: HWND,
    width: u32,
    height: u32,
}

impl WindowRenderTargetBacking {
    fn new(window: &Window) -> WindowRenderTargetBacking {
        let (width, height) = window.get_client_size();

        return WindowRenderTargetBacking {
            handle: window.handle,
            width: width,
            height: height,
        };
    }
}

unsafe impl RenderTargetBacking for WindowRenderTargetBacking {
    fn create_target(self, factory: &mut ID2D1Factory) -> Result<*mut ID2D1RenderTarget, HRESULT> {
        debug_assert!(self.handle != null_mut());

        let properties = D2D1_RENDER_TARGET_PROPERTIES {
            _type: D2D1_RENDER_TARGET_TYPE_DEFAULT,
            pixelFormat: D2D1_PIXEL_FORMAT {
                format: DXGI_FORMAT_B8G8R8A8_UNORM,
                alphaMode: D2D1_ALPHA_MODE_PREMULTIPLIED,
            },
            dpiX: 0.0,
            dpiY: 0.0,
            usage: D2D1_RENDER_TARGET_USAGE_NONE,
            minLevel: D2D1_FEATURE_LEVEL_DEFAULT,
        };

        let hwnd_properties = D2D1_HWND_RENDER_TARGET_PROPERTIES {
            hwnd: self.handle,
            pixelSize: D2D1_SIZE_U {
                width: self.width,
                height: self.height,
            },
            presentOptions: D2D1_PRESENT_OPTIONS_NONE,
        };

        unsafe {
            let mut render_target = ComPtr::<ID2D1HwndRenderTarget>::new();

            let result =
                factory.CreateHwndRenderTarget(&properties,
                                               &hwnd_properties,
                                               render_target.raw_addr());

            debug_assert!(SUCCEEDED(result));

            return Ok(render_target.detach() as _);
        }
    }
}

