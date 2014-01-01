close_monitor.exe: close_monitor.asm
	ml.exe close_monitor.asm /link /MERGE:.rdata=.text /SUBSYSTEM:WINDOWS
	del *.obj *.lnk
