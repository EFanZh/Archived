#pragma once

namespace DirectWriteWrapper
{
    public enum class FontStretch
    {
        Undefined = DWRITE_FONT_STRETCH_UNDEFINED,
        UltraCondensed = DWRITE_FONT_STRETCH_ULTRA_CONDENSED,
        ExtraCondensed = DWRITE_FONT_STRETCH_EXTRA_CONDENSED,
        Condensed = DWRITE_FONT_STRETCH_CONDENSED,
        SemiCondensed = DWRITE_FONT_STRETCH_SEMI_CONDENSED,
        Normal = DWRITE_FONT_STRETCH_NORMAL,
        Medium = DWRITE_FONT_STRETCH_MEDIUM,
        SemiExpanded = DWRITE_FONT_STRETCH_SEMI_EXPANDED,
        Expanded = DWRITE_FONT_STRETCH_EXPANDED,
        ExtraExpanded = DWRITE_FONT_STRETCH_EXTRA_EXPANDED,
        UltraExpanded = DWRITE_FONT_STRETCH_ULTRA_EXPANDED
    };
}
