#ifndef USERPROCWINDOWTRAITS_H
#define USERPROCWINDOWTRAITS_H

template<class TProcType, class TReturnType, int proc_type>
class UserProcWindowTraits
{
public:
  typedef TProcType ProcType;
  typedef TReturnType ReturnType;

  enum { PROC_TYPE = proc_type };
};

class UserProcWindowTraitsUserWindow : public UserProcWindowTraits<WNDPROC, LRESULT, GWLP_WNDPROC>
{
};

class UserProcWindowTraitsUserDialogBox : public UserProcWindowTraits<DLGPROC, INT_PTR, DWLP_DLGPROC>
{
};

#endif // USERPROCWINDOWTRAITS_H
