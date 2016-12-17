#include "context_menu.h"

HMENU CreateContextMenu()
{
    HMENU hmenu = CreatePopupMenu();

    AppendMenu(hmenu, 0, ID_COPY_HEX, TEXT("Copy &Hex Color"));
    AppendMenu(hmenu, 0, ID_COPY_DEC, TEXT("Copy &Dec Color"));

    return hmenu;
}
