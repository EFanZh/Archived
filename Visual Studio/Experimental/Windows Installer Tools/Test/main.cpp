#include "Product.h"

using namespace std;

TString EscapseCSV(TString input)
{
    TString result(TEXT("\""));

    for (auto ch : input)
    {
        if (ch == TEXT('"'))
        {
            result += TEXT("\"\"");
        }
        else
        {
            result += ch;
        }
    }

    result += '"';

    return result;
}

int _tmain()
{
    TOFStream ofs(TEXT("Products.csv"));

    ofs.imbue(locale(locale::empty(), new codecvt_utf8<TCHAR, 0x10ffff, static_cast<codecvt_mode>(codecvt_mode::generate_header)>()));

    ofs << TEXT(R"("Product Code","Product Name")") << endl;
    for (const auto &product : Product::GetProducts(NULL, NULL, MSIINSTALLCONTEXT::MSIINSTALLCONTEXT_ALL))
    {
        ofs << EscapseCSV(product.productCode) << ',' << EscapseCSV(product.GetProductName()) << endl;
    }
}
