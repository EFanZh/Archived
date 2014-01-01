#ifndef STDAFX_H
#define STDAFX_H

#include <BasicMessageLoop.h>
#include <UserWindow.h>
#include <MessageCrackers.h>
#include <osg\MatrixTransform>
#include <osg\ShapeDrawable>
#include <osgGA\TrackballManipulator>
#include <osgViewer\Viewer>
#include <osgViewer\api\Win32\GraphicsWindowWin32>
#include <osgViewer\config\SingleWindow>

#pragma comment(lib, "ComCtl32.Lib")

#ifdef _DEBUG
#pragma comment(lib, "osgd.lib")
#pragma comment(lib, "osgViewerd.lib")
#pragma comment(lib, "osgGAd.lib")
#else
#pragma comment(lib, "osg.lib")
#pragma comment(lib, "osgViewer.lib")
#pragma comment(lib, "osgGA.lib")
#endif

#endif // STDAFX_H
