#include <stdio.h>

int main(void)
{
    const char *s = "#include <stdio.h>%c%cint main(void)%c{%c    const char *s = %c%s%c;%c%c    printf(s, 10, 10, 10, 10, 34, s, 34, 10, 10, 10, 10, 10);%c}%c";

    printf(s, 10, 10, 10, 10, 34, s, 34, 10, 10, 10, 10, 10);
}
