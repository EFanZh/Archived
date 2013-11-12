#ifndef WIN32GUILIBRARYUTILITIES_H
#define WIN32GUILIBRARYUTILITIES_H

namespace Win32GUILibrary
{
  class HMENUOrInt
  {
    HMENU hMenu;

  public:
    HMENUOrInt(HMENU hMenu) : hMenu(hMenu)
    {
    }

    HMENUOrInt(int nID) : hMenu(reinterpret_cast<HMENU>(nID))
    {
    }

    operator HMENU()
    {
      return hMenu;
    }
  };

  template<class TProcType, class TReturnType, int proc_type>
  class ThunkWindowTemplateTrait
  {
  public:
    typedef TProcType ProcType;
    typedef TReturnType ReturnType;

    enum { PROC_TYPE = proc_type };
  };

  class ThunkWindowTemplateTraitUserWindow : public ThunkWindowTemplateTrait<WNDPROC, LRESULT, GWLP_WNDPROC>
  {
  };

  class ThunkWindowTemplateTraitUserDialogBox : public ThunkWindowTemplateTrait<DLGPROC, INT_PTR, DWLP_DLGPROC>
  {
  };
}

#endif // WIN32GUILIBRARYUTILITIES_H
