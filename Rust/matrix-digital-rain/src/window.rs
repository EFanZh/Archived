use std::mem::*;
use std::ptr::*;
use direct2d::*;
use direct2d::comptr::*;
use direct2d::math::*;
use direct2d::render_target::*;
use directwrite;
use user32::*;
use winapi::*;
use configuration::*;
use resource::*;

pub struct Window {
    handle: HWND,
    configuration: Configuration,
    d2d_factory: Factory,
    render_target: Option<RenderTarget>,
    resource: Option<Resource>,
}

impl Window {
    pub fn new() -> Window {
        let dwrite_factory = directwrite::Factory::new().unwrap();
        let configuration = Configuration::new(&dwrite_factory);

        return Window {
            handle: null_mut(),
            d2d_factory: Factory::new().unwrap(),
            configuration: configuration,
            render_target: None,
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
            .create_render_target(WindowRenderTargetBacking::new(self.handle,
                                                                 self.get_client_size()))
            .unwrap();

        self.render_target = Some(render_target);
        self.resource = Some(Resource::new(self.render_target.as_mut().unwrap(),
                                           &self.configuration));

        return 0;
    }

    fn on_destroy(&self) -> LRESULT {
        unsafe {
            PostQuitMessage(0);
        }

        return 0;
    }

    fn on_paint(&mut self) -> LRESULT {
        {
            debug_assert!(self.resource.is_some());

            let (width, height) = self.get_client_size();
            let render_target = self.render_target.as_mut().unwrap();

            unsafe {
                render_target.hwnd_rt().unwrap().Resize(&D2D1_SIZE_U {
                                                            width: width,
                                                            height: height,
                                                        });

            }

            render_target.begin_draw();

            Window::draw_scene(render_target,
                               &self.configuration,
                               &self.resource.as_ref().unwrap());

            let result = render_target.end_draw();

            debug_assert!(result.is_ok());
        }

        self.invalidate();

        return 0;
    }

    fn get_client_size(&self) -> (u32, u32) {
        unsafe {
            let mut rect = uninitialized();
            let result = GetClientRect(self.handle, &mut rect);

            debug_assert!(result != FALSE);

            return ((rect.right - rect.left) as _, (rect.bottom - rect.top) as _);
        }
    }

    fn invalidate(&self) {
        unsafe {
            let result = InvalidateRect(self.handle, null(), FALSE);

            debug_assert!(result != FALSE);
        }
    }

    fn draw_scene(render_target: &mut RenderTarget,
                  configuration: &Configuration,
                  resource: &Resource) {
        render_target.clear(&configuration.background_color);

        let text = "Test";

        render_target.draw_text(text,
                                &configuration.head_font,
                                &RectF::new(10.0, 10.0, 30.0, 40.0),
                                &resource.head_brush,
                                &[]);
    }
}

struct WindowRenderTargetBacking {
    window_handle: HWND,
    width: u32,
    height: u32,
}

impl WindowRenderTargetBacking {
    fn new(handle: HWND, (width, height): (u32, u32)) -> WindowRenderTargetBacking {
        return WindowRenderTargetBacking {
            window_handle: handle,
            width: width,
            height: height,
        };
    }
}

unsafe impl RenderTargetBacking for WindowRenderTargetBacking {
    fn create_target(self, factory: &mut ID2D1Factory) -> Result<*mut ID2D1RenderTarget, HRESULT> {
        debug_assert!(self.window_handle != null_mut());

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
            hwnd: self.window_handle,
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

