#include <stdio.h>

void quote(const char *s)
{
  printf("    \"");
  while (*s)
  {
    if (*s == '\\')
    {
      printf("\\\\");
    }
    else if (*s == '"')
    {
      printf("\\\"");
    }
    else if (*s == '\n')
    {
      printf("\\n\"\n    \"");
    }
    else
    {
      printf("%c", *s);
    }
    s++;
  }
  putchar('"');
}

int main(void)
{
  const char *s =
    "#include <stdio.h>\n"
    "\n"
    "void quote(const char *s)\n"
    "{\n"
    "  printf(\"    \\\"\");\n"
    "  while (*s)\n"
    "  {\n"
    "    if (*s == '\\\\')\n"
    "    {\n"
    "      printf(\"\\\\\\\\\");\n"
    "    }\n"
    "    else if (*s == '\"')\n"
    "    {\n"
    "      printf(\"\\\\\\\"\");\n"
    "    }\n"
    "    else if (*s == '\\n')\n"
    "    {\n"
    "      printf(\"\\\\n\\\"\\n    \\\"\");\n"
    "    }\n"
    "    else\n"
    "    {\n"
    "      printf(\"%c\", *s);\n"
    "    }\n"
    "    s++;\n"
    "  }\n"
    "  putchar('\"');\n"
    "}\n"
    "\n"
    "int main(void)\n"
    "{\n"
    "  const char *s =\n"
    "@@;\n"
    "  const char *p = s;\n"
    "\n"
    "  while (*p)\n"
    "  {\n"
    "    if (*p == '@' && p[1] == '@')\n"
    "    {\n"
    "      quote(s);\n"
    "      p++;\n"
    "    }\n"
    "    else\n"
    "    {\n"
    "      putchar(*p);\n"
    "    }\n"
    "    p++;\n"
    "  }\n"
    "  putchar('\\n');\n"
    "}";
  const char *p = s;

  while (*p)
  {
    if (*p == '@' && p[1] == '@')
    {
      quote(s);
      p++;
    }
    else
    {
      putchar(*p);
    }
    p++;
  }
  putchar('\n');
}
