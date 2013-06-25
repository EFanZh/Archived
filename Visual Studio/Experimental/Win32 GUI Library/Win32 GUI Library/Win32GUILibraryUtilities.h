#ifndef WIN32GUILIBRARYUTILITIES_H
#define WIN32GUILIBRARYUTILITIES_H

class HMENUOrUINT
{
  HMENU hMenu;

public:
  HMENUOrUINT(HMENU hMenu) : hMenu(hMenu)
  {
  }

  HMENUOrUINT(UINT nID) : hMenu(reinterpret_cast<HMENU>(nID))
  {
  }

  operator HMENU()
  {
    return hMenu;
  }
};

#endif // WIN32GUILIBRARYUTILITIES_H
