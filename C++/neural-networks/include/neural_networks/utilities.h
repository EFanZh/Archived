#pragma once

#include <neural_networks/tensor.h>
#include <iostream>
#include <vector>

namespace neural_networks
{
    template <class T>
    void print(const T &value)
    {
        std::cout << value;
    }

    template <class T>
    void print(const tensor<T> &value)
    {
        std::cout << value.get_value();
    }

    template <class T, class Allocator>
    void print(const std::vector<T, Allocator> &value)
    {
        std::cout << '(';

        if (value.size() > 0)
        {
            print(value[0]);

            for (std::size_t i = 1; i < value.size(); ++i)
            {
                std::cout << ' ';
                print(value[i]);
            }
        }

        std::cout << ')';
    }

    template <class T, std::size_t FirstDimensions, std::size_t... RestDimensions>
    void print(const tensor<T, FirstDimensions, RestDimensions...> &value)
    {
        std::cout << '(';

        if (value.template get_dimensions<0>() > 0)
        {
            print(value[0]);

            for (std::size_t i = 1; i < value.template get_dimensions<0>(); ++i)
            {
                std::cout << ' ';
                print(value[i]);
            }
        }

        std::cout << ')';
    }

    template <class T>
    void print_line(const T &value)
    {
        print(value);

        std::cout << '\n';
    }
}
