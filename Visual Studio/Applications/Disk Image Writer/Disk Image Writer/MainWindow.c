#include "MainWindow.h"
#include "Layout.h"
#include "UIHelper.h"

static PCTSTR main_window_class_name = TEXT("MainWindow");

LRESULT CALLBACK MainWindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
BOOL MainWindow_OnCreate(HWND hWnd, LPCREATESTRUCT lpCreateStruct);
void MainWindow_OnDestroy(HWND hWnd);
void MainWindow_OnSize(HWND hWnd, UINT state, int cx, int cy);
void MainWindow_OnActivate(HWND hWnd, UINT state, HWND hWndActDeact, BOOL fMinimized);
void MainWindow_OnSetFocus(HWND hWnd, HWND hWndOldFocus);
void MainWindow_OnGetMinMaxInfo(HWND hWnd, LPMINMAXINFO lpMinMaxInfo);
void MainWindow_OnCommand(HWND hWnd, int id, HWND hWndCtl, UINT codeNotify);
void MainWindow_OnParentNotify(HWND hWnd, UINT uMsg, HWND hWndChild, int idChild);

static int window_width = 0, window_height = 0;
static HINSTANCE hInstance = NULL;
static HFONT hf_message = NULL;
static HWND hw_last_focus = NULL;
static HIMAGELIST hil_drive_icons = NULL;
static IFileOpenDialog *p_file_open_dialog = NULL;

static int edit_image_file_dw = 0, button_combobox_edit_h = 0;
static int button_image_file_dx = 0, button_image_file_y = 0;
static int comboboxex_drive_dw = 0;
static int button_go_dx = 0, button_go_cancel_close_y = 0;
static int button_cancel_dx = 0;
static int button_close_dx = 0;
static int statusbar_main_dw = 0;
static int statusbar_main_h = 0;

#define MAINWINDOW_STYLE ((WS_OVERLAPPEDWINDOW | WS_CLIPCHILDREN) & ~WS_MAXIMIZEBOX & ~WS_THICKFRAME)
#define GLOBAL_CONTROL_STYLE (WS_CHILD | WS_VISIBLE | WS_CLIPCHILDREN | WS_CLIPSIBLINGS)
#define GLOBAL_TAB_CONTROL_STYLE (GLOBAL_CONTROL_STYLE | WS_TABSTOP)
#define BUTTON_STYLE (GLOBAL_TAB_CONTROL_STYLE)
#define COMBOBOXEX_STYLE (GLOBAL_TAB_CONTROL_STYLE | CBS_DROPDOWNLIST | CBS_NOINTEGRALHEIGHT)
#define EDIT_STYLE (GLOBAL_TAB_CONTROL_STYLE | ES_AUTOHSCROLL | ES_READONLY)
#define PROGRESSBAR_STYLE (GLOBAL_CONTROL_STYLE)
#define STATIC_STYLE (GLOBAL_CONTROL_STYLE | SS_CENTERIMAGE)
#define STATUSBAR_STYLE (GLOBAL_CONTROL_STYLE)

// Dialog Box Command IDs
#define ID_BUTTON_GO IDOK
#define ID_BUTTON_CANCEL IDCANCEL
#define ID_BUTTON_CLOSE IDCLOSE

// Other IDs
#define ID_STATIC_IMAGEFILE 101
#define ID_EDIT_IMAGEFILE 102
#define ID_BUTTON_IMAGEFILE 103
#define ID_STATIC_DRIVE 104
#define ID_COMBOBOXEX_DRIVE 105
#define ID_STATUSBAR_MAIN 106
#define ID_PROGRESSBAR_MAIN 107

ATOM RegisterMainWindowClass(void)
{
  WNDCLASSEX wcex = { sizeof(wcex) };

  wcex.lpfnWndProc = MainWindowProc;
  wcex.hInstance = GetModuleHandle(NULL);
  wcex.hIcon = LoadIcon(NULL, IDI_APPLICATION);
  wcex.hCursor = LoadCursor(NULL, IDC_ARROW);
  wcex.hbrBackground = GetSysColorBrush(COLOR_3DFACE);
  wcex.lpszClassName = main_window_class_name;
  wcex.hIconSm = wcex.hIcon;

  return RegisterClassEx(&wcex);
}

HWND CreateMainWindow(void)
{
  // Get global hInstance
  hInstance = GetModuleHandle(NULL);
  InitializeUIHelper();

  // CreateWindow
  return CreateWindowEx(0, main_window_class_name, TEXT("Disk Image Writer"), MAINWINDOW_STYLE, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, HWND_DESKTOP, NULL, hInstance, NULL);
}

LRESULT CALLBACK MainWindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
  switch (uMsg)
  {
    HANDLE_MSG(hWnd, WM_CREATE, MainWindow_OnCreate);
    HANDLE_MSG(hWnd, WM_DESTROY, MainWindow_OnDestroy);
    HANDLE_MSG(hWnd, WM_SIZE, MainWindow_OnSize);
    HANDLE_MSG(hWnd, WM_ACTIVATE, MainWindow_OnActivate);
    HANDLE_MSG(hWnd, WM_SETFOCUS, MainWindow_OnSetFocus);
    HANDLE_MSG(hWnd, WM_GETMINMAXINFO, MainWindow_OnGetMinMaxInfo);
    HANDLE_MSG(hWnd, WM_COMMAND, MainWindow_OnCommand);
    HANDLE_MSG(hWnd, WM_PARENTNOTIFY, MainWindow_OnParentNotify);
  default:
    return DefWindowProc(hWnd, uMsg, wParam, lParam);
  }
}

BOOL MainWindow_OnCreate(HWND hWnd, LPCREATESTRUCT lpCreateStruct)
{
  HWND hw_button_image_file, hw_static_drive, hw_comboboxex_drive, hw_status_bar;
  int static_drive_height;
  int row_bottom;
  int scaled_default_button_width = ScaleX(DEFAULT_BUTTON_WIDTH);
  int scaled_dialog_box_margin_x = ScaleX(DIALOG_BOX_MARGINS), scaled_dialog_box_margin_y = ScaleY(DIALOG_BOX_MARGINS);
  int scaled_spacing_between_interactive_controls = ScaleX(SPACING_BETWEEN_INTERACTIVE_CONTROLS);
  int scaled_spacing_between_labels_and_controls = ScaleY(SPACING_BETWEEN_LABELS_AND_CONTROLS);
  int scaled_spacing_between_unrelated_controls = ScaleY(SPACING_BETWEEN_UNRELATED_CONTROLS);
  int scaled_spacing_between_buttons = ScaleX(SPACING_BETWEEN_BUTTONS);

  UNREFERENCED_PARAMETER(lpCreateStruct);
  hf_message = GetMessageFont();

  // Group 1 start
  {
    HWND hw_static_image_file = CreateWindow(WC_STATIC, TEXT("Image File (&F):"), STATIC_STYLE | WS_GROUP, scaled_dialog_box_margin_x, scaled_dialog_box_margin_y, CW_USEDEFAULT, CW_USEDEFAULT, hWnd, (HMENU)ID_STATIC_IMAGEFILE, hInstance, NULL);
    SIZE appropriate_size;

    GetAppropriateSize(hw_static_image_file, &appropriate_size);
    SetWindowPos(hw_static_image_file, NULL, 0, 0, appropriate_size.cx, appropriate_size.cy, SWP_NOMOVE | SWP_NOREDRAW | SWP_NOZORDER);
    row_bottom = scaled_dialog_box_margin_y + appropriate_size.cy + scaled_spacing_between_labels_and_controls;
  }

  {
    HWND hw_edit_image_file = CreateWindowEx(WS_EX_CLIENTEDGE, WC_EDIT, NULL, EDIT_STYLE, scaled_dialog_box_margin_x, row_bottom, 0, 0, hWnd, (HMENU)ID_EDIT_IMAGEFILE, hInstance, NULL);
    hw_last_focus = hw_edit_image_file;
  }

  hw_button_image_file = CreateWindow(WC_BUTTON, TEXT("..."), BUTTON_STYLE, 0, 0, 0, 0, hWnd, (HMENU)ID_BUTTON_IMAGEFILE, hInstance, NULL);

  // Group 2 start
  hw_static_drive = CreateWindow(WC_STATIC, TEXT("Drive (&D):"), STATIC_STYLE | WS_GROUP, 0, 0, 0, 0, hWnd, (HMENU)ID_STATIC_DRIVE, hInstance, NULL);

  // Create ComboBoxEx
  hw_comboboxex_drive = CreateWindow(WC_COMBOBOXEX, NULL, COMBOBOXEX_STYLE, CW_USEDEFAULT, CW_USEDEFAULT, 0, ScaleY(240), hWnd, (HMENU)ID_COMBOBOXEX_DRIVE, hInstance, NULL);
  hil_drive_icons = ImageList_Create(ScaleX(16), ScaleY(16), ILC_COLOR32, 0, 1);
  SendMessage(hw_comboboxex_drive, CBEM_SETIMAGELIST, 0, (WPARAM)hil_drive_icons);
  {
    RECT rect_comboboxex_drive;

    GetWindowRect(hw_comboboxex_drive, &rect_comboboxex_drive);
    comboboxex_drive_dw = 2 * scaled_dialog_box_margin_x;
    button_combobox_edit_h = rect_comboboxex_drive.bottom - rect_comboboxex_drive.top;
  }

  // Reposition previous controls
  {
    int button_image_file_width = YToX(button_combobox_edit_h);
    edit_image_file_dw = 2 * scaled_dialog_box_margin_x + scaled_spacing_between_interactive_controls + button_image_file_width;
    SetWindowPos(hw_button_image_file, NULL, 0, 0, button_image_file_width, button_combobox_edit_h, SWP_NOMOVE | SWP_NOREDRAW | SWP_NOZORDER);
    button_image_file_dx = scaled_dialog_box_margin_x + button_image_file_width;
  }
  button_image_file_y = row_bottom;
  row_bottom += button_combobox_edit_h + scaled_spacing_between_unrelated_controls;

  {
    SIZE appropriate_size;

    GetAppropriateSize(hw_static_drive, &appropriate_size);
    SetWindowPos(hw_static_drive, NULL, scaled_dialog_box_margin_x, row_bottom, appropriate_size.cx, appropriate_size.cy, SWP_NOREDRAW | SWP_NOZORDER);
    static_drive_height = appropriate_size.cy;
  }
  row_bottom += static_drive_height + scaled_spacing_between_labels_and_controls;
  SetWindowPos(hw_comboboxex_drive, NULL, scaled_dialog_box_margin_x, row_bottom, 0, 0, SWP_NOREDRAW | SWP_NOSIZE | SWP_NOZORDER);

  // Group 3 start
  CreateWindow(WC_BUTTON, TEXT("Go (&G)"), BUTTON_STYLE | WS_GROUP | BS_DEFPUSHBUTTON, 0, 0, scaled_default_button_width, button_combobox_edit_h, hWnd, (HMENU)ID_BUTTON_GO, hInstance, NULL);
  CreateWindow(WC_BUTTON, TEXT("Cancel (&C)"), BUTTON_STYLE | WS_DISABLED, 0, 0, scaled_default_button_width, button_combobox_edit_h, hWnd, (HMENU)ID_BUTTON_CANCEL, hInstance, NULL);
  CreateWindow(WC_BUTTON, TEXT("Close (&X)"), BUTTON_STYLE, 0, 0, scaled_default_button_width, button_combobox_edit_h, hWnd, (HMENU)ID_BUTTON_CLOSE, hInstance, NULL);
  {
    int interval = scaled_default_button_width + scaled_spacing_between_buttons;
    button_close_dx = scaled_dialog_box_margin_x + scaled_default_button_width;
    button_cancel_dx = button_close_dx + interval;
    button_go_dx = button_cancel_dx + interval;
  }
  row_bottom += button_combobox_edit_h + scaled_spacing_between_unrelated_controls;
  button_go_cancel_close_y = row_bottom;
  row_bottom += button_combobox_edit_h + scaled_dialog_box_margin_y;

  // Group 4 start
  hw_status_bar = CreateWindow(STATUSCLASSNAME, NULL, STATUSBAR_STYLE | WS_GROUP, 0, 0, CW_USEDEFAULT, CW_USEDEFAULT, hWnd, (HMENU)ID_STATUSBAR_MAIN, hInstance, NULL);
  {
    RECT rect_status_bar;

    GetWindowRect(hw_status_bar, &rect_status_bar);
    {
      RECT rect = { 0 };

      rect.right = ScaleX(360);
      rect.bottom = row_bottom + rect_status_bar.bottom - rect_status_bar.top;
      AdjustWindowRect(&rect, MAINWINDOW_STYLE, FALSE);
      window_width = rect.right - rect.left;
      window_height = rect.bottom - rect.top;
    }
  }

  {
    int status_bar_parts[] = { ScaleX(120), -1 };
    RECT rect_part;

    SendMessage(hw_status_bar, SB_SETPARTS, ARRAYSIZE(status_bar_parts), (LPARAM)status_bar_parts);
    SendMessage(hw_status_bar, SB_GETRECT, 1, (LPARAM)&rect_part);
    CreateWindow(PROGRESS_CLASS, NULL, PROGRESSBAR_STYLE, rect_part.left, rect_part.top, 0, 0, hw_status_bar, (HMENU)ID_PROGRESSBAR_MAIN, hInstance, NULL);
  }

  // Create a FileOpenDialog.
  {
    COMDLG_FILTERSPEC file_types[] =
    {
      { TEXT("Image Files (*.img; *.iso)"), TEXT("*.img; *.iso") },
      { TEXT("All Types"), TEXT("*.*") }
    };

    // TODO Figure this out.
    if (FAILED(CoInitializeEx(NULL, COINIT_APARTMENTTHREADED)))
    {
      return FALSE;
    }
    if (FAILED(CoCreateInstance(&CLSID_FileOpenDialog, NULL, CLSCTX_INPROC_SERVER, &IID_IFileOpenDialog, (LPVOID *)&p_file_open_dialog)))
    {
      return FALSE;
    }
    if (FAILED(IFileOpenDialog_SetFileTypes(p_file_open_dialog, ARRAYSIZE(file_types), file_types)))
    {
      return FALSE;
    }
  }

  // Initialize ComboBoxEx items
  {
    DWORD logical_drives;
    int i = 0, j = 0;

    logical_drives = GetLogicalDrives();
    while (logical_drives)
    {
      if (logical_drives & 0x1)
      {
        TCHAR buffer[32];

        StringCchPrintf(buffer, ARRAYSIZE(buffer), TEXT("%c:\\"), 'A' + i);
        if (GetDriveType(buffer) == DRIVE_REMOVABLE)
        {
          SHFILEINFO sfi;
          COMBOBOXEXITEM cbexi = { 0 };

          SHGetFileInfo(buffer, FILE_ATTRIBUTE_DEVICE, &sfi, sizeof(sfi), SHGFI_ICON);
          ImageList_AddIcon(hil_drive_icons, sfi.hIcon);
          cbexi.mask = CBEIF_DI_SETITEM | CBEIF_IMAGE | CBEIF_OVERLAY | CBEIF_SELECTEDIMAGE | CBEIF_TEXT;
          cbexi.iItem = -1;
          cbexi.pszText = buffer;
          cbexi.iImage = j;
          cbexi.iSelectedImage = cbexi.iImage;
          cbexi.iOverlay = cbexi.iImage;

          SendMessage(hw_comboboxex_drive, CBEM_INSERTITEM, 0, (WPARAM)&cbexi);
          j++;
        }
      }
      logical_drives >>= 1;
      i++;
    }
    ComboBox_SetCurSel(hw_comboboxex_drive, 0);
  }

  SetWindowPos(hWnd, NULL, 0, 0, window_width, window_height, SWP_NOMOVE | SWP_NOREDRAW | SWP_NOZORDER);
  MoveToWorkAreaCenter(hWnd);

  return TRUE;
}

void MainWindow_OnDestroy(HWND hWnd)
{
  UNREFERENCED_PARAMETER(hWnd);

  ImageList_Destroy(hil_drive_icons);
  if (p_file_open_dialog)
  {
    IFileOpenDialog_Release(p_file_open_dialog);
  }
  CoUninitialize();
  UninitializeUIHelper();
  PostQuitMessage(0);
}

void MainWindow_OnSize(HWND hWnd, UINT state, int cx, int cy)
{
  UNREFERENCED_PARAMETER(state);
  UNREFERENCED_PARAMETER(cy);

  SetWindowPos(GetDlgItem(hWnd, ID_EDIT_IMAGEFILE), NULL, 0, 0, cx - edit_image_file_dw, button_combobox_edit_h, SWP_NOMOVE | SWP_NOREDRAW | SWP_NOZORDER);
  SetWindowPos(GetDlgItem(hWnd, ID_BUTTON_IMAGEFILE), NULL, cx - button_image_file_dx, button_image_file_y, 0, 0, SWP_NOREDRAW | SWP_NOSIZE | SWP_NOZORDER);
  SetWindowPos(GetDlgItem(hWnd, ID_COMBOBOXEX_DRIVE), NULL, 0, 0, cx - comboboxex_drive_dw, button_combobox_edit_h, SWP_NOMOVE | SWP_NOREDRAW | SWP_NOZORDER);
  SetWindowPos(GetDlgItem(hWnd, ID_BUTTON_GO), NULL, cx - button_go_dx, button_go_cancel_close_y, 0, 0, SWP_NOREDRAW | SWP_NOSIZE | SWP_NOZORDER);
  SetWindowPos(GetDlgItem(hWnd, ID_BUTTON_CANCEL), NULL, cx - button_cancel_dx, button_go_cancel_close_y, 0, 0, SWP_NOREDRAW | SWP_NOSIZE | SWP_NOZORDER);
  SetWindowPos(GetDlgItem(hWnd, ID_BUTTON_CLOSE), NULL, cx - button_close_dx, button_go_cancel_close_y, 0, 0, SWP_NOREDRAW | SWP_NOSIZE | SWP_NOZORDER);

  {
    HWND hw_status_bar_main;
    RECT rect_part;

    hw_status_bar_main = GetDlgItem(hWnd, ID_STATUSBAR_MAIN);
    SendMessage(hw_status_bar_main, WM_SIZE, 0, 0);
    SendMessage(hw_status_bar_main, SB_GETRECT, 1, (LPARAM)&rect_part);
    SetWindowPos(GetDlgItem(hw_status_bar_main, ID_PROGRESSBAR_MAIN), NULL, 0, 0, rect_part.right - rect_part.left - 3, rect_part.bottom - rect_part.top - 1, SWP_NOMOVE | SWP_NOREDRAW | SWP_NOZORDER);
  }

  RedrawWindow(hWnd, NULL, NULL, RDW_ERASE | RDW_FRAME | RDW_INVALIDATE | RDW_ALLCHILDREN) ;
}

void MainWindow_OnActivate(HWND hWnd, UINT state, HWND hWndActDeact, BOOL fMinimized)
{
  UNREFERENCED_PARAMETER(hWnd);
  UNREFERENCED_PARAMETER(hWndActDeact);
  UNREFERENCED_PARAMETER(fMinimized);

  if (state == WA_INACTIVE)
  {
    hw_last_focus = GetFocus();
  }
}

void MainWindow_OnSetFocus(HWND hWnd, HWND hWndOldFocus)
{
  UNREFERENCED_PARAMETER(hWnd);
  UNREFERENCED_PARAMETER(hWndOldFocus);

  if (hw_last_focus)
  {
    SetFocus(hw_last_focus);
  }
}

void MainWindow_OnGetMinMaxInfo(HWND hWnd, LPMINMAXINFO lpMinMaxInfo)
{
  UNREFERENCED_PARAMETER(hWnd);

  lpMinMaxInfo->ptMaxTrackSize.y = window_height;
  lpMinMaxInfo->ptMinTrackSize.x = window_width;
  lpMinMaxInfo->ptMinTrackSize.y = window_height;
}

void MainWindow_OnCommand(HWND hWnd, int id, HWND hWndCtl, UINT codeNotify)
{
  UNREFERENCED_PARAMETER(hWndCtl);
  UNREFERENCED_PARAMETER(codeNotify);

  switch (id)
  {
  case ID_BUTTON_IMAGEFILE:
    {
      if (SUCCEEDED(IFileOpenDialog_Show(p_file_open_dialog, hWnd)))
      {
        IShellItem *isi;
        if (SUCCEEDED(IFileOpenDialog_GetResult(p_file_open_dialog, &isi)))
        {
          LPTSTR file_name;
          if (SUCCEEDED(IShellItem_GetDisplayName(isi, SIGDN_DESKTOPABSOLUTEEDITING, &file_name)))
          {
            SetWindowText(GetDlgItem(hWnd, ID_EDIT_IMAGEFILE), file_name);
          }
        }
      }
    }
    break;
  case ID_BUTTON_GO:
    MessageBox(hWnd, TEXT("Go."), NULL, MB_OK | MB_ICONINFORMATION);
    break;
  case ID_BUTTON_CANCEL:
    MessageBox(hWnd, TEXT("Cancel."), NULL, MB_OK | MB_ICONINFORMATION);
    break;
  case ID_BUTTON_CLOSE:
    SendMessage(hWnd, WM_CLOSE, 0, 0);
    break;
  default:
    break;
  }
}

void MainWindow_OnParentNotify(HWND hWnd, UINT uMsg, HWND hWndChild, int idChild)
{
  UNREFERENCED_PARAMETER(hWnd);
  UNREFERENCED_PARAMETER(idChild);

  if (uMsg == WM_CREATE)
  {
    SetWindowFont(hWndChild, hf_message, TRUE);
  }
}
