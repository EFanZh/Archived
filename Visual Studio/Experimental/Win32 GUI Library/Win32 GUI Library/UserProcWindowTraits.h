#ifndef USERPROCWINDOWTRAITS_H
#define USERPROCWINDOWTRAITS_H

template<class TProcType, class TReturnType, int proc_type>
class UserProcWindowTrait
{
public:
  typedef TProcType ProcType;
  typedef TReturnType ReturnType;

  enum { PROC_TYPE = proc_type };
};

class UserProcWindowTraitUserWindow : public UserProcWindowTrait<WNDPROC, LRESULT, GWLP_WNDPROC>
{
};

class UserProcWindowTraitUserDialogBox : public UserProcWindowTrait<DLGPROC, INT_PTR, DWLP_DLGPROC>
{
};

#endif // USERPROCWINDOWTRAITS_H
