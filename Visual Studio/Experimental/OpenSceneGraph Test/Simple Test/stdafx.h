#ifndef STDAFX_H
#define STDAFX_H

#define STRICT
#define WIN32_LEAN_AND_MEAN

#include <Windows.h>
#include <osg\MatrixTransform>
#include <osg\ShapeDrawable>
#include <osgDB\ReadFile>
#include <osgViewer\Viewer>
#include <osgViewer\config\SingleWindow>

#ifdef _DEBUG
#pragma comment(lib, "osgd.lib")
#pragma comment(lib, "osgDBd.lib")
#pragma comment(lib, "osgViewerd.lib")
#else
#pragma comment(lib, "osg.lib")
#pragma comment(lib, "osgDB.lib")
#pragma comment(lib, "osgViewer.lib")
#endif

#endif // STDAFX_H
