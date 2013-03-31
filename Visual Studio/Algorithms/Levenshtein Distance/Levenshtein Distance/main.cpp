#include <algorithm>
#include <functional>
#include <map>

int levenshtein_distance(const char *s, const char *t)
{
  std::map<std::tuple<int, int>, int> dict;
  std::function<int(int, int)> calc = [&dict, &calc, s, t] (int ns, int nt) -> int
  {
    auto key = std::make_tuple(ns, nt);
    auto r = dict.find(key);
    if (r != dict.end())
    {
      return r->second;
    }

    if (ns == 0)
    {
      return nt;
    }
    if (nt == 0)
    {
      return ns;
    }

    auto v = std::min(std::min(calc(ns - 1, nt) + 1, calc(ns, nt - 1) + 1), calc(ns - 1, nt - 1) + (s[ns - 1] == t[nt - 1] ? 0 : 1));
    dict[key] = v;

    return v;
  };
  return calc(strlen(s), strlen(t));
}

int main()
{
  const char *a = "hgfdhfgdhgf";
  const char *b = "hgfhgfhgfdh";

  printf("%d\n", levenshtein_distance(a, b));
}
