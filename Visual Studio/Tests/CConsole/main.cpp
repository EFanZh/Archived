#include <iostream>
#include <map>
#include <tuple>

using namespace std;

map<int, tuple<int, double>> dict;

tuple<int, double> get_count(int count)
{
  auto r = dict.find(count);
  if (r != dict.end())
  {
    return r->second;
  }

  double c = count;
  double v = -1, min = INT_MAX;
  int guess = -1;

  // Assume guess from 0 to (count - 1).
  for (int i = 0; i < count; ++i)
  {
    double p_yes = 1 / c;
    double p_less = (count - 1 - i) / c;
    double p_more = i / c;

    double t_yes = 1;
    double t_less = 1 + get<1>(get_count(count - i - 1));
    double t_more = 1 + i / 2.0;

    v = p_yes * t_yes + p_less * t_less + p_more * t_more;

    if (v < min)
    {
      min = v;
      guess = i;
    }
  }

  auto result = make_tuple(guess, v);
  dict[count] = result;
  return result;
}

int main()
{
  for (int i = 1; i <= 1000; ++i)
  {
    auto v = get_count(i);
    cout << i << " -> " << get<0>(v) + 1 << ", " << get<1>(v) << endl;
  }
}
