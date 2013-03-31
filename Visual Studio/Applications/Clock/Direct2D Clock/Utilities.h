#ifndef UTILITIES_H
#define UTILITIES_H

template<class T>
void SafeRelease(T **pp_T)
{
  if (*pp_T)
  {
    (*pp_T)->Release();
    *pp_T = NULL;
  }
}

#endif // UTILITIES_H
