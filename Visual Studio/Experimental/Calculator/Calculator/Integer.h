#ifndef INTEGER_H
#define INTEGER_H

class Integer
{
  friend class Calculator;

  bool is_positive;
  unsigned short *data;
  size_t data_size;

public:
  Integer(void);
  ~Integer(void);
  Integer(const Integer &other);
  Integer(Integer &&other);

  Integer &operator =(Integer other);

  bool IsPositive();
};

#endif // INTEGER_H
