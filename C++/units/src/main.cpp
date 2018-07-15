#include <iostream>
#include <numeric>

namespace units
{
    struct UnitMetrics
    {
        const char *name;
        int scale;
    };

    template <class System>
    class QuantityPrinter
    {
        template <std::size_t UnitIndex>
        struct State
        {
            static constexpr auto unitMetrics = System::allUnitMetrics[UnitIndex];

            static void notPrinted(std::ostream &os, int value)
            {
                const auto result = std::div(value, unitMetrics.scale);

                if (result.quot == 0)
                {
                    State<UnitIndex - 1>::notPrinted(os, result.rem);
                }
                else
                {
                    os << result.quot << ' ' << unitMetrics.name;

                    State<UnitIndex - 1>::printed(os, result.rem);
                }
            }

            static void printed(std::ostream &os, int value)
            {
                const auto result = std::div(value, unitMetrics.scale);

                if (result.quot > 0)
                {
                    os << ' ' << result.quot << ' ' << unitMetrics.name;
                }

                State<UnitIndex - 1>::printed(os, result.rem);
            }
        };

        template <>
        struct State<0>
        {
            static constexpr auto unitMetrics = System::allUnitMetrics[0];

            static void notPrinted(std::ostream &os, int value)
            {
                os << (value / unitMetrics.scale) << ' ' << unitMetrics.name;
            }

            static void printed(std::ostream &os, int value)
            {
                const auto result = value / unitMetrics.scale;

                if (result > 0)
                {
                    os << ' ' << result << ' ' << unitMetrics.name;
                }
            }
        };

    public:
        static void print(std::ostream &os, int valueInBaseUnit)
        {
            State<std::size(System::allUnitMetrics) - 1>::notPrinted(os, valueInBaseUnit);
        }
    };

    template <class System>
    class DefineQuantities
    {
        template <int Scale>
        class QuantityImpl
        {
            int value;

            template <int OtherScale>
            friend class QuantityImpl;

            friend std::ostream &operator<<(std::ostream &os, const QuantityImpl &quantity)
            {
                QuantityPrinter<System>::print(os, quantity.value * Scale);

                return os;
            }

        public:
            QuantityImpl(int value) : value(value)
            {
            }

            template <int RhsScale>
            constexpr bool operator==(const QuantityImpl<RhsScale> &rhs) const
            {
                constexpr auto commonScale = std::gcd(Scale, RhsScale);

                return this->value * (Scale / commonScale) == rhs.value * (RhsScale / commonScale);
            }

            template <int RhsScale>
            constexpr bool operator!=(const QuantityImpl<RhsScale> &rhs) const
            {
                return !(*this == rhs);
            }

            template <int RhsScale>
            constexpr auto operator+(const QuantityImpl<RhsScale> &rhs) const
            {
                constexpr auto commonScale = std::gcd(Scale, RhsScale);

                return QuantityImpl<commonScale>(this->value * (Scale / commonScale) +
                                                 rhs.value * (RhsScale / commonScale));
            }
        };

    public:
        template <std::size_t I>
        using Quantity = QuantityImpl<System::allUnitMetrics[I].scale>;
    };

    namespace details
    {
        class LengthSystem
        {
            static constexpr UnitMetrics inchMetrics = { "INCH", 1 };
            static constexpr UnitMetrics feetMetrics = { "FEET", inchMetrics.scale * 12 };
            static constexpr UnitMetrics yardMetrics = { "YARD", feetMetrics.scale * 3 };
            static constexpr UnitMetrics mileMetrics = { "MILE", yardMetrics.scale * 1760 };

        public:
            static constexpr UnitMetrics allUnitMetrics[] = { inchMetrics, feetMetrics, yardMetrics, mileMetrics };
        };

        using LengthUnits = DefineQuantities<LengthSystem>;
    }

    using Inch = details::LengthUnits::Quantity<0>;
    using Feet = details::LengthUnits::Quantity<1>;
    using Yard = details::LengthUnits::Quantity<2>;
    using Mile = details::LengthUnits::Quantity<3>;
}

using namespace std;
using namespace units;

int main()
{
    cout << (Inch(1) == Feet(2)) << endl;
    cout << (Inch(12) == Feet(1)) << endl;
    cout << (Inch(12) == Feet(1)) << endl;
    cout << (Inch(12) + Feet(1) == Feet(2)) << endl;
    cout << (Mile(3) + Feet(4) + Inch(2)) << endl;
}
