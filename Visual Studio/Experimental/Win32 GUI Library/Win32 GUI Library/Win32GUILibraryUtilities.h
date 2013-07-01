#ifndef WIN32GUILIBRARYUTILITIES_H
#define WIN32GUILIBRARYUTILITIES_H

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

#endif // WIN32GUILIBRARYUTILITIES_H
