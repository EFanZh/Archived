#include <stdio.h>

void quote(const char *s)
{
    printf("        \"");

    for (; *s != '\0'; ++s)
    {
        switch (*s)
        {
            case '\\':
            case '"':
                putchar('\\');
                putchar(*s);
                break;

            case '\n':
                printf("\\n\"\n        \"");
                break;

            default:
                putchar(*s);
                break;
        }
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
        "    printf(\"        \\\"\");\n"
        "\n"
        "    for (; *s != '\\0'; ++s)\n"
        "    {\n"
        "        switch (*s)\n"
        "        {\n"
        "            case '\\\\':\n"
        "            case '\"':\n"
        "                putchar('\\\\');\n"
        "                putchar(*s);\n"
        "                break;\n"
        "\n"
        "            case '\\n':\n"
        "                printf(\"\\\\n\\\"\\n        \\\"\");\n"
        "                break;\n"
        "\n"
        "            default:\n"
        "                putchar(*s);\n"
        "                break;\n"
        "        }\n"
        "    }\n"
        "\n"
        "    putchar('\"');\n"
        "}\n"
        "\n"
        "int main(void)\n"
        "{\n"
        "    const char *s =\n"
        "@@;\n"
        "\n"
        "    for (const char *p = s; *p != '\\0';)\n"
        "    {\n"
        "        if (p[0] == '@' && p[1] == '@')\n"
        "        {\n"
        "            quote(s);\n"
        "            p += 2;\n"
        "        }\n"
        "        else\n"
        "        {\n"
        "            putchar(*p);\n"
        "            ++p;\n"
        "        }\n"
        "    }\n"
        "\n"
        "    putchar('\\n');\n"
        "}";

    for (const char *p = s; *p != '\0';)
    {
        if (p[0] == '@' && p[1] == '@')
        {
            quote(s);
            p += 2;
        }
        else
        {
            putchar(*p);
            ++p;
        }
    }

    putchar('\n');
}
