#ifndef STDAFX_H
#define STDAFX_H

#define STRICT
#define WIN32_LEAN_AND_MEAN

#include <Windows.h>
#include <osg\MatrixTransform>
#include <osg\ShapeDrawable>
#include <osgFX\Outline>
#include <osgGA\GUIEventHandler>
#include <osgUtil\IntersectionVisitor>
#include <osgViewer\Viewer>
#include <osgViewer\config\SingleWindow>

#ifdef _DEBUG
#pragma comment(lib, "osgd.lib")
#pragma comment(lib, "osgGAd.lib")
#pragma comment(lib, "osgFXd.lib")
#pragma comment(lib, "osgUtild.lib")
#pragma comment(lib, "osgViewerd.lib")
#else
#pragma comment(lib, "osg.lib")
#pragma comment(lib, "osgGA.lib")
#pragma comment(lib, "osgFX.lib")
#pragma comment(lib, "osgUtil.lib")
#pragma comment(lib, "osgViewer.lib")
#endif

#endif // STDAFX_H
