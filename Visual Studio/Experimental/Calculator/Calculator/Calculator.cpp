#include "Calculator.h"

Integer Calculator::Plus(const Integer &x, const Integer &y)
{
  const Integer *min, *max;
  size_t min_size, max_size;

  if (x.data_size < y.data_size)
  {
    min = &x;
    max = &y;
    min_size = x.data_size;
    max_size = y.data_size;
  }
  else
  {
    min = &y;
    max = &x;
    min_size = y.data_size;
    max_size = x.data_size;
  }

  auto new_data = new unsigned short[max_size + 1];
  bool carry = false;
  auto calc_plus = [&new_data, &carry] (const unsigned int &value, const unsigned int &index)
  {
    carry = false;
    if (value <= USHRT_MAX)
    {
      new_data[index] = value;
    }
    else
    {
      new_data[index] = value - USHRT_MAX;
      carry = true;
    }
  };
  for (int i = 0; i < min_size; i++)
  {
    calc_plus(min->data[i] + max->data[i] + (carry ? 1u : 0u), i);
  }
  for (int i = min_size; i < max_size; i++)
  {
    calc_plus(max->data[i] + (carry ? 1u : 0u), i);
  }
  Integer new_integer;
  if (carry)
  {
    new_data[max_size] = 1u;
    new_integer.data = new_data;
    new_integer.data_size = max_size + 1;
  }
  else
  {
    auto new_data_2 = new unsigned short[max_size];
    std::copy(new_data_2, new_data_2 + max_size, new_data);
    new_integer.data = new_data_2;
    new_integer.data_size = max_size;
  }
  return std::move(new_integer);
}

Integer Calculator::Minus(const Integer &x, const Integer &y)
{
  return Integer();
}
