#ifndef SPINLOCK_H
#define SPINLOCK_H

class SpinLock
{
  std::atomic_bool is_locked;

public:
  void Lock();
  void Unlock();
};

#endif // SPINLOCK_H
