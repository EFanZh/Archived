#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include "Scene.h"
#include "Window.h"

class MainWindow : public Window
{
    Scene scene;

    virtual LPCTSTR GetWindowClassName() const;
    virtual LRESULT WindowProc(UINT uMsg, WPARAM wParam, LPARAM lParam);

    BOOL OnCreate(LPCREATESTRUCT lpCreateStruct);
    void OnDestroy();
    void OnSize(UINT state, int cx, int cy);
    void OnActivate(UINT state, HWND hWndActDeact, BOOL fMinimized);
    void OnPaint();
    BOOL OnEraseBkgnd(HDC hdc);
    void OnTimer(UINT id);

public:
    MainWindow()
    {
    }

    HDC BeginPaint(LPPAINTSTRUCT lpPaint)
    {
        return ::BeginPaint(hWnd, lpPaint);
    }

    BOOL EndPaint(const PAINTSTRUCT *lpPaint)
    {
        return ::EndPaint(hWnd, lpPaint);
    }

    BOOL InvalidateRect(const RECT *lpRect, BOOL bErase)
    {
        return ::InvalidateRect(hWnd, lpRect, bErase);
    }

    UINT_PTR SetTimer(UINT_PTR nIDEvent, UINT uElapse, TIMERPROC lpTimerFunc)
    {
        return ::SetTimer(hWnd, nIDEvent, uElapse, lpTimerFunc);
    }

    BOOL Show(int nCmdShow)
    {
        return ShowWindow(hWnd, nCmdShow);
    }

    BOOL Update()
    {
        return UpdateWindow(hWnd);
    }
};

#endif // MAINWINDOW_H
