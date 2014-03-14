#include "SpinLock.h"

void SpinLock::Lock()
{
  while (is_locked.exchange(true))
  {
    continue;
  }
}

void SpinLock::Unlock()
{
  is_locked.store(false);
}
