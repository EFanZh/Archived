#pragma once

#include "WindowsObject.h"
#include "StandardIconId.h"
#include "IconMetric.h"

class Icon : public WindowsObject < Icon, HICON >
{
public:
    typedef HICON HandleType;

    static Ref<Icon> LoadMetric(StandardIconId standardIconId, IconMetric iconMetric)
    {
        HICON icon;

        ::LoadIconMetric(NULL, MAKEINTRESOURCE(standardIconId), static_cast<int>(iconMetric), &icon);

        return icon;
    }
};
