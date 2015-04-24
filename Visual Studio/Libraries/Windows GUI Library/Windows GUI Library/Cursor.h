#pragma once

#include "WindowsGUILibraryBase.h"
#include "WindowsObject.h"
#include "StandardCursorId.h"

class Cursor : public WindowsObject < Cursor, HCURSOR >
{
public:
    static Ref<Cursor> Load(StandardCursorId standardCursorId)
    {
        return static_cast<HCURSOR>(::LoadImage(NULL, MAKEINTRESOURCE(standardCursorId), IMAGE_CURSOR, 0, 0, LR_SHARED));
    }
};
