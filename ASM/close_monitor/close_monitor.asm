.386
.model flat

includelib User32.Lib

extern __imp__SendMessageW@16 : proc

HWND_BROADCAST = 0ffffh
WM_SYSCOMMAND = 0112h
SC_MONITORPOWER = 0f170h

.code

_main proc
    push 2 ; The display is being shut off
    push SC_MONITORPOWER
    push WM_SYSCOMMAND
    push HWND_BROADCAST
    call dword ptr __imp__SendMessageW@16
    xor eax, eax
    ret
_main endp

end _main
