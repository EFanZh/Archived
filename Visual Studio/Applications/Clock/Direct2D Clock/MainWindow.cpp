#include "MainWindow.h"

#define ID_TIMER 1

LPCTSTR MainWindow::GetWindowClassName() const
{
    return TEXT("MainWindow");
}

LRESULT MainWindow::WindowProc(UINT uMsg, WPARAM wParam, LPARAM lParam)
{
    switch (uMsg)
    {
    case WM_CREATE:
        return OnCreate(reinterpret_cast<LPCREATESTRUCT>(lParam)) ? 0 : -1;
    case WM_DESTROY:
        return OnDestroy(), 0;
    case WM_SIZE:
        return OnSize(wParam, LOWORD(lParam), HIWORD(lParam)), 0;
    case WM_PAINT:
        return OnPaint(), 0;
    case WM_ERASEBKGND:
        return OnEraseBkgnd(reinterpret_cast<HDC>(wParam));
    case WM_TIMER:
        return OnTimer(static_cast<UINT>(wParam)), 0;
    default:
        return DefWindowProc(hWnd, uMsg, wParam, lParam);
    }
}

BOOL MainWindow::OnCreate(LPCREATESTRUCT lpCreateStruct)
{
    if (SUCCEEDED(scene.Initialize()))
    {
        SetTimer(ID_TIMER, USER_TIMER_MINIMUM, NULL);
        return TRUE;
    }
    return FALSE;
}

void MainWindow::OnDestroy()
{
    scene.DiscardResources();
    PostQuitMessage(EXIT_SUCCESS);
}

void MainWindow::OnSize(UINT state, int cx, int cy)
{
    scene.Resize(cx, cy);
    InvalidateRect(NULL, FALSE);
}

void MainWindow::OnPaint()
{
    PAINTSTRUCT ps;
    BeginPaint(&ps);
    scene.Render(hWnd);
    EndPaint(&ps);
}

BOOL MainWindow::OnEraseBkgnd(HDC hdc)
{
    return TRUE;
}

void MainWindow::OnTimer(UINT id)
{
    switch (id)
    {
    case ID_TIMER:
        InvalidateRect(NULL, FALSE);
        UpdateWindow(hWnd);
        break;
    }
}
