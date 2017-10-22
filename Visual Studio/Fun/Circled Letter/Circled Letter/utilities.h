#pragma once

#include "stdafx.h"

namespace details
{
    template <class F>
    class ScopeGuard
    {
        std::optional<F> function;

        ScopeGuard(const ScopeGuard &other) = delete;
        ScopeGuard & operator=(ScopeGuard &other) = delete;

    public:
        ScopeGuard(F &&function) : function(std::move(function))
        {
        }

        ScopeGuard(ScopeGuard &&other)
        {
            function.swap(other.function);
        }

        ~ScopeGuard()
        {
            if (function)
            {
                (*function)();
            }
        }
    };
}

template <class T, class U>
void VerifyEquals(const T &lhs, const U &rhs)
{
    if (!(lhs == rhs))
    {
        abort();
    }
}

template <class F>
details::ScopeGuard<std::decay_t<F>> MakeScopeGuard(F &&function)
{
    return details::ScopeGuard<std::decay_t<F>>{ std::forward<F>(function) };
}
