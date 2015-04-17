#pragma once

#include <codecvt>
#include <fstream>
#include <string>
#include <vector>

#include <tchar.h>

#define NOMINMAX

#include <Windows.h>
#include <Msi.h>

#pragma comment(lib, "Msi.Lib")

typedef std::basic_string<TCHAR> TString;
typedef std::basic_ofstream<TCHAR> TOFStream;
