#pragma once

class Product
{
    TString GetProperty(LPCTSTR propertyName) const
    {
        DWORD size = 0;

        ::MsiGetProductInfo(productCode.c_str(), propertyName, NULL, &size);

        TString value(size, '\0');

        ++size;
        ::MsiGetProductInfo(productCode.c_str(), propertyName, &value.front(), &size);

        return value;
    }

public:
    TString productCode = TString(38, TEXT('\0'));

    TString GetProductName() const
    {
        return GetProperty(INSTALLPROPERTY_INSTALLEDPRODUCTNAME);
    }

public:
    static std::vector<Product> GetProducts(LPCWSTR productCode, LPCWSTR userSid, MSIINSTALLCONTEXT context)
    {
        using namespace std;

        vector<Product> products;
        Product product;

        for (DWORD i = 0; ::MsiEnumProductsEx(productCode, userSid, context, i, &product.productCode.front(), NULL, NULL, NULL) == ERROR_SUCCESS; ++i)
        {
            products.emplace_back(product);
        }

        return products;
    }
};
