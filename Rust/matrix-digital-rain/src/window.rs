use std::mem::*;
use std::ptr::*;
use direct2d::*;
use direct2d::comptr::*;
use direct2d::math::*;
use direct2d::render_target::*;
use user32::*;
use winapi::*;

pub struct Window {
    handle: HWND,
    d2d_factory: Factory,
    d2d_render_target: Option<RenderTarget>,
}

impl Window {
    pub fn new() -> Window {
        return Window {
            handle: null_mut(),
            d2d_factory: Factory::new().unwrap(),
            d2d_render_target: None,
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
        self.d2d_render_target =
            Some(self.d2d_factory
                     .create_render_target(WindowRenderTargetBacking::new(self))
                     .unwrap());

        return 0;
    }

    fn on_destroy(&self) -> LRESULT {
        unsafe {
            PostQuitMessage(0);
        }

        return 0;
    }

    fn on_paint(&mut self) -> LRESULT {
        debug_assert!(self.d2d_render_target.is_some());

        let (width, height) = self.get_client_size();
        let render_target = self.d2d_render_target.as_mut().unwrap();

        unsafe {
            let mut hwnd_render_target = render_target.hwnd_rt().unwrap();

            hwnd_render_target.Resize(&D2D1_SIZE_U {
                                          width: width,
                                          height: height,
                                      });
        }

        render_target.begin_draw();

        render_target.clear(&ColorF::uint_rgb(0x00000000, 1.0f32));

        let result = render_target.end_draw();

        debug_assert!(result.is_ok());

        return 0;
    }

    fn get_client_size(&self) -> (u32, u32) {
        unsafe {
            let mut rect = uninitialized();
            let result = GetClientRect(self.handle, &mut rect);

            debug_assert!(result != 0);

            return ((rect.right - rect.left) as u32, (rect.bottom - rect.top) as u32);
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

