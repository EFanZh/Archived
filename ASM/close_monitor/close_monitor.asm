.386
.model flat

includelib user32

extrn __imp__SendMessageW@16:proc

.code

main proc
    push 2                                ; The display is being shut off
    push 0f170h                           ; SC_MONITORPOWER
    push 0112h                            ; WM_SYSCOMMAND
    push 0ffffh                           ; HWND_BROADCAST
    call dword ptr __imp__SendMessageW@16 ; SendMessage
    xor eax, eax                          ; return 0
    ret
main endp

end
