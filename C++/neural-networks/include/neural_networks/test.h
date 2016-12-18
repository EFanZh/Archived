#pragma once

#include <exception>

namespace test
{
    template <class T>
    struct runner
    {
    };

    void expect(bool result)
    {
        if (!result)
        {
            throw std::exception();
        }
    }
}

#define TEST(name)                                                   \
    namespace test                                                   \
    {                                                                \
        namespace test_cases                                         \
        {                                                            \
            struct name;                                             \
        }                                                            \
                                                                     \
        template <>                                                  \
        struct runner<test_cases::name>                              \
        {                                                            \
            runner();                                                \
        };                                                           \
                                                                     \
        namespace                                                    \
        {                                                            \
            runner<test_cases::name> __test_runner_test_case_##name; \
        }                                                            \
    }                                                                \
                                                                     \
    test::runner<test::test_cases::name>::runner()
