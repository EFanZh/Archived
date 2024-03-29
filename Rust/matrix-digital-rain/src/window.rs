use backend::*;
use configuration::*;
use direct2d::*;
use direct2d::comptr::*;
use direct2d::render_target::*;
use painter::*;
use resource::*;
use std::mem::*;
use std::ptr::*;
use std::time::*;
use user32::*;
use utilities::*;
use winapi::*;

pub struct Window
{
    handle: HWND,
    configuration: Configuration,
    d2d_factory: Factory,
    render_target: ComPtr<ID2D1HwndRenderTarget>,
    resource: Option<Resource>,
    backend: Backend,
    start_time: SystemTime,
    last_frame_time: f64
}

impl Window
{
    pub fn new() -> Window
    {
        let configuration = Configuration::new();

        return Window {
            handle: null_mut(),
            d2d_factory: Factory::new().unwrap(),
            configuration: configuration,
            render_target: ComPtr::new(),
            resource: None,
            backend: Backend::new(),
            start_time: SystemTime::now(),
            last_frame_time: 0.0
        };
    }

    pub fn set_handle(&mut self, handle: HWND)
    {
        debug_assert!(self.handle == null_mut());
        debug_assert!(handle != null_mut());

        self.handle = handle;
    }

    pub fn window_proc(&mut self, u_msg: UINT, w_param: WPARAM, l_param: LPARAM) -> LRESULT
    {
        return match u_msg
        {
            WM_CREATE => self.on_create(),
            WM_DESTROY => self.on_destroy(),
            WM_PAINT => self.on_paint(),
            _ =>
            unsafe { DefWindowProcW(self.handle, u_msg, w_param, l_param) },
        };
    }

    fn on_create(&mut self) -> LRESULT
    {
        let render_target = self.d2d_factory
                                .create_render_target(WindowRenderTargetBacking::new(self.handle,
                                                                                     self.get_client_size()))
                                .unwrap();

        unsafe {
            self.render_target = render_target.hwnd_rt().unwrap();
        }

        self.resource = Some(Resource::new(&mut self.render_target, &self.configuration));

        return 0;
    }

    fn on_destroy(&self) -> LRESULT
    {
        unsafe {
            PostQuitMessage(0);
        }

        return 0;
    }

    fn on_paint(&mut self) -> LRESULT
    {
        {
            let current_time = get_total_seconds(self.start_time.elapsed().as_ref().unwrap());

            debug_assert!(self.resource.is_some());

            let (width, height) = self.get_client_size();

            unsafe {
                self.render_target.Resize(&D2D1_SIZE_U {
                                              width: width,
                                              height: height
                                          });

                self.render_target.BeginDraw();
            }

            draw_scene(&mut self.backend,
                       current_time - self.last_frame_time,
                       &mut self.render_target,
                       &mut self.configuration,
                       self.resource.as_mut().unwrap());

            unsafe {
                let mut tag_1 = uninitialized();
                let mut tag_2 = uninitialized();
                let end_draw_result = self.render_target.EndDraw(&mut tag_1, &mut tag_2);

                debug_assert!(SUCCEEDED(end_draw_result));
            }

            self.last_frame_time = current_time;
        }

        self.invalidate();

        return 0;
    }

    fn get_client_size(&self) -> (u32, u32)
    {
        unsafe {
            let mut rect = uninitialized();
            let result = GetClientRect(self.handle, &mut rect);

            debug_assert!(result != FALSE);

            return ((rect.right - rect.left) as _, (rect.bottom - rect.top) as _);
        }
    }

    fn invalidate(&self)
    {
        unsafe {
            let result = InvalidateRect(self.handle, null(), FALSE);

            debug_assert!(result != FALSE);
        }
    }
}

struct WindowRenderTargetBacking
{
    window_handle: HWND,
    width: u32,
    height: u32
}

impl WindowRenderTargetBacking
{
    fn new(handle: HWND, (width, height): (u32, u32)) -> WindowRenderTargetBacking
    {
        return WindowRenderTargetBacking {
            window_handle: handle,
            width: width,
            height: height
        };
    }
}

unsafe impl RenderTargetBacking for WindowRenderTargetBacking
{
    fn create_target(self, factory: &mut ID2D1Factory) -> Result<*mut ID2D1RenderTarget, HRESULT>
    {
        debug_assert!(self.window_handle != null_mut());

        let properties = D2D1_RENDER_TARGET_PROPERTIES {
            _type: D2D1_RENDER_TARGET_TYPE_DEFAULT,
            pixelFormat: D2D1_PIXEL_FORMAT {
                format: DXGI_FORMAT_B8G8R8A8_UNORM,
                alphaMode: D2D1_ALPHA_MODE_PREMULTIPLIED
            },
            dpiX: 0.0,
            dpiY: 0.0,
            usage: D2D1_RENDER_TARGET_USAGE_NONE,
            minLevel: D2D1_FEATURE_LEVEL_DEFAULT
        };

        let hwnd_properties = D2D1_HWND_RENDER_TARGET_PROPERTIES {
            hwnd: self.window_handle,
            pixelSize: D2D1_SIZE_U {
                width: self.width,
                height: self.height
            },
            presentOptions: D2D1_PRESENT_OPTIONS_NONE
        };

        unsafe {
            let mut render_target = ComPtr::<ID2D1HwndRenderTarget>::new();

            let result = factory.CreateHwndRenderTarget(&properties, &hwnd_properties, render_target.raw_addr());

            debug_assert!(SUCCEEDED(result));

            return Ok(render_target.detach() as _);
        }
    }
}

