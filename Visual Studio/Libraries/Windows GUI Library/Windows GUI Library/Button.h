#pragma once

#include "Window.h"

class Button : public Window
{
public:
    Button(Ref<Window> parent,
           Ref<WindowsString> windowName = nullptr,
           int x = 0,
           int y = 0,
           int width = 75,
           int height = 23,
           WindowStyle style = WindowStyle::Child | WindowStyle::Visible,
           ExtendedWindowStyle extendedStyle = ExtendedWindowStyle::None,
           Ref<Menu> menu = nullptr,
           void *param = nullptr) : Window(WC_BUTTON, windowName, style, extendedStyle, x, y, width, height, parent, menu, param)
    {
    }
};
