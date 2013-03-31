#ifndef UIHELPER_H
#define UIHELPER_H

void InitializeUIHelper(void);
void UninitializeUIHelper(void);
int ScaleX(int x);
int ScaleY(int y);
int UnscaleX(int x);
int UnscaleY(int y);
int XToY(int x);
int YToX(int y);
HFONT GetMessageFont(void);

#endif // UIHELPER_H
