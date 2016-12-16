#pragma once

#include <cassert>

namespace Test
{
    template <class T>
    struct Runner
    {
    };
}

#define TEST(name)                                                 \
    namespace Test                                                 \
    {                                                              \
        namespace TestCases                                        \
        {                                                          \
            struct name;                                           \
        }                                                          \
                                                                   \
        template <>                                                \
        struct Runner<TestCases::name>                             \
        {                                                          \
            Runner();                                              \
        };                                                         \
                                                                   \
        namespace                                                  \
        {                                                          \
            Runner<TestCases::name> __test_runner_testcase_##name; \
        }                                                          \
    }                                                              \
                                                                   \
    Test::Runner<Test::TestCases::name>::Runner()
