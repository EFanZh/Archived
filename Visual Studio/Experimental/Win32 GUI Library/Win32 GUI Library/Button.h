#ifndef BUTTON_H
#define BUTTON_H

#include "StandardControl.h"

namespace Win32GUILibrary
{
  class Button : public StandardControl<Button>
  {
  public:
    static LPCTSTR GetControlClassName()
    {
      return WC_BUTTON;
    }
  };
}

#endif // BUTTON_H
