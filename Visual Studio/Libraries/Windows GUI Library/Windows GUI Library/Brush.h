#pragma once

#include "WindowsGUILibraryBase.h"
#include "WindowsObject.h"
#include "SystemColorIndex.h"

class Brush : public WindowsObject < Brush, HBRUSH >
{
public:
    static Ref<Brush> GetSysColorBrush(SystemColorIndex index)
    {
        return ::GetSysColorBrush(static_cast<int>(index));
    }
};
