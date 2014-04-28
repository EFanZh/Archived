#include <iostream>
#include "iterable.h"

int main()
{
    for (auto s : linq::make_iterable({ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })
         .append({ 11, 7, 12, 7, 13 })
         .filter([](int x) { return x % 2 == 0; })
         .map([](int x) { return x * x; })
         .filter([](int x) { return x > 10; }))
    {
        std::cout << s << std::endl;
    }
}
