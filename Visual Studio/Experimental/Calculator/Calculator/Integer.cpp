#include "Integer.h"

Integer::Integer(void) : data(nullptr), data_size(0), is_positive(true)
{
}

Integer::~Integer(void)
{
  delete [] data;
}

Integer::Integer(const Integer &other) : data(new unsigned short[other.data_size]), data_size(other.data_size), is_positive(other.is_positive)
{
  std::copy(other.data, other.data + data_size, data);
}

Integer::Integer(Integer &&other) : data(other.data), data_size(other.data_size), is_positive(other.is_positive)
{
  other.data = nullptr;
}

Integer &Integer::operator =(Integer other)
{
  std::swap(data, other.data);
  data_size = other.data_size;
  is_positive = other.is_positive;
  return *this;
}

bool Integer::IsPositive()
{
  return is_positive;
}
