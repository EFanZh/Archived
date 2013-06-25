#ifndef BUTTON_H
#define BUTTON_H

#include "StandardControl.h"

namespace Win32GUILibrary
{
  class Button : public StandardControl<Button>
  {
  public:
    DECLARE_STANDARD_CONTROL(BUTTON)
  };
}

#endif // BUTTON_H
