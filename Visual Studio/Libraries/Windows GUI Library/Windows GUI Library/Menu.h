#pragma once

#include "WindowsGUILibraryBase.h"
#include "WindowsObject.h"

class Menu : public WindowsObject < Menu, HMENU >
{
public:
    typedef HMENU HandleType;
};
