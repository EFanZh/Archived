// https://leetcode.com/problems/guess-number-higher-or-lower/

namespace
{
    int myNumber;
}

int guess(int num)
{
    if (myNumber < num)
    {
        return -1;
    }
    else if (myNumber == num)
    {
        return 0;
    }
    else
    {
        return 1;
    }
}

int main()
{
}
