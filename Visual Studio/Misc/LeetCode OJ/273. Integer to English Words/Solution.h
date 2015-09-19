#pragma once

template <int N>
struct GetBaseHelper;

template <>
struct GetBaseHelper<1000000000>
{
    static const char *get()
    {
        return " Billion";
    }
};

template <>
struct GetBaseHelper<1000000>
{
    static const char *get()
    {
        return " Million";
    }
};

template <>
struct GetBaseHelper<1000>
{
    static const char *get()
    {
        return " Thousand";
    }
};

template <>
struct GetBaseHelper<100>
{
    static const char *get()
    {
        return " Hundred";
    }
};

template <int N1, int... N2>
struct SolutionHelper
{
    static void convert(int num, string &result)
    {
        if (num >= N1)
        {
            SolutionHelper<N2...>::convert(num / N1, result);
            result += GetBaseHelper<N1>::get();

            num %= N1;

            if (num > 0)
            {
                result += ' ';
                SolutionHelper<N2...>::convert(num, result);
            }
        }
        else
        {
            SolutionHelper<N2...>::convert(num, result);
        }
    }
};

template <>
struct SolutionHelper<1>
{
    static void convert(int num, string &result)
    {
        static const char *numbers[] = { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        static const char *tens[] = { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        int k = num / 10;

        if (k < 2)
        {
            result += numbers[num - 1];
        }
        else
        {
            result += tens[k - 2];
            num %= 10;

            if (num > 0)
            {
                result += ' ';
                result += numbers[num - 1];
            }
        }
    }
};

class Solution
{
public:
    string numberToWords(int num)
    {
        if (num == 0)
        {
            return "Zero";
        }
        else
        {
            string result;

            SolutionHelper<1000000000, 1000000, 1000, 100, 1>::convert(num, result);

            return result;
        }
    }
};
