(() =>
{
    "use strict";

    var lines = 16, columns = 16;
    var page_character_count = lines * columns;
    var display_style = 16;
    var font_family = "Calibri, \"Segoe UI\", \"Segoe Symbol\", \"Microsoft YaHei\", \"Microsoft JhengHei\", Meiryo, \"Malgun Gothic\", Gisha, Leelawadee, sans-serif";
    var font_size = "12pt";
    var page_number = 0;
    var locate = -1;

    var lines_element: HTMLInputElement;
    var columns_element: HTMLInputElement;
    var display_style_element: HTMLSelectElement;
    var font_family_element: HTMLInputElement;
    var font_size_element: HTMLInputElement;

    var previous_page_element: HTMLButtonElement;
    var next_page_element: HTMLButtonElement;
    var page_number_element: HTMLInputElement;
    var first_page_element: HTMLButtonElement;
    var locate_element: HTMLInputElement;

    var table_container_element: HTMLDivElement;
    var table_element: HTMLTableElement;

    function $(id: string)
    {
        return document.getElementById(id);
    }

    /*
     * ES6 Unicode Shims 0.1
     * (c) 2012 Steven Levithan <http://slevithan.com/>
     * MIT License
     */
    function fromCodePoint(...codes: number[])
    {
        var chars = [], point, offset, units, i;
        for (i = 0; i < codes.length; i++)
        {
            point = codes[i];
            offset = point - 0x10000;
            units = point > 0xFFFF ? [0xD800 + (offset >> 10), 0xDC00 + (offset & 0x3FF)] : [point];
            chars.push(String.fromCharCode.apply(null, units));
        }
        return chars.join("");
    }

    /*
     * (c) 2012 Steven Levithan <http://slevithan.com/>
     * MIT license
     */
    function codePointAt(str, pos)
    {
        pos = isNaN(pos) ? 0 : pos;
        var code = str.charCodeAt(pos), next = str.charCodeAt(pos + 1);
        // If a surrogate pair
        if (0xD800 <= code && code <= 0xDBFF && 0xDC00 <= next && next <= 0xDFFF)
        {
            return ((code - 0xD800) * 0x400) + (next - 0xDC00) + 0x10000;
        }
        return code;
    }

    function to_unicode_code(code: number)
    {
        var s = code.toString(16);
        if (code < 0x1000)
        {
            var full_hex = [];
            var zero_count = 4 - s.length
            for (var i = 0; i < zero_count; i++)
            {
                full_hex.push("0");
            }
            full_hex.push(s)
            return full_hex.join("");
        }
        return s;
    }

    function update_content()
    {
        var i, j;

        var strs = ["<table id=\"unicode-characters\"><tbody><tr><td><a href=\"http://www.fileformat.info/info/unicode/char/"]
        var rows = [];
        var base = page_character_count * page_number;

        for (i = 0; i < lines; i++)
        {
            var cols = [];

            for (j = 0; j < columns; j++)
            {
                var temp_strs = [];
                var value = base + lines * j + i;
                var unicode = to_unicode_code(value);
                temp_strs.push(unicode);
                temp_strs.push("/index.htm\">");
                if (display_style === 16)
                {
                    temp_strs.push("U+");
                    temp_strs.push(unicode.toUpperCase());
                }
                else
                {
                    temp_strs.push(value.toString(10));
                }
                temp_strs.push("</a></td><td><span>")
                if (value === locate)
                {
                    temp_strs.push("<mark>");
                    temp_strs.push(fromCodePoint(value));
                    temp_strs.push("</mark>");
                }
                else
                {
                    temp_strs.push(fromCodePoint(value));
                }
                cols.push(temp_strs.join(""));
            }
            rows.push(cols.join("</span></td><td><a href=\"http://www.fileformat.info/info/unicode/char/"));
        }
        strs.push(rows.join("</span></td></tr><tr><td><a href=\"http://www.fileformat.info/info/unicode/char/"));
        strs.push("</span></td></tr></tbody></table>");

        table_container_element.innerHTML = strs.join("");
        table_element = <HTMLTableElement>table_container_element.firstElementChild;
        table_element.style.fontFamily = font_family_element.value;
        table_element.style.fontSize = font_size_element.value;
    }

    function set_page_number(number)
    {
        page_number = number;
        page_number_element.value = (page_number + 1).toString();
        update_content();
    }

    function cancel_locate()
    {
        locate = -1;
        locate_element.value = "";
    }

    function locate_character()
    {
        if (locate >= 0)
        {
            set_page_number(Math.floor(locate / page_character_count));
        }
        else
        {
            update_content();
        }
    }

    document.addEventListener("DOMContentLoaded", () =>
    {
        lines_element = <HTMLInputElement>$("lines");
        columns_element = <HTMLInputElement>$("columns");
        display_style_element = <HTMLSelectElement>$("display-style");
        font_family_element = <HTMLInputElement>$("font-family");
        font_size_element = <HTMLInputElement>$("font-size");

        previous_page_element = <HTMLButtonElement>$("previous-page");
        next_page_element = <HTMLButtonElement>$("next-page");
        page_number_element = <HTMLInputElement>$("page-number");
        first_page_element = <HTMLButtonElement>$("first-page");
        locate_element = <HTMLInputElement>$("locate");
        table_container_element = <HTMLDivElement>$("unicode-characters-container");

        document.body.onkeydown = function (e)
        {
            if (e.target === document.body)
            {
                switch (e.keyCode)
                {
                    case 37:
                        cancel_locate();
                        if (page_number > 0)
                        {
                            set_page_number(page_number - 1);
                        }
                        break;
                    case 39:
                        cancel_locate();
                        set_page_number(page_number + 1);
                        break;
                    default:
                        return true;
                }
                return false;
            }
        };

        lines_element.oninput = () =>
        {
            var v = parseInt(lines_element.value, 10);
            lines = v > 0 ? v : 1;
            page_character_count = lines * columns;
            locate_character();
        };

        lines_element.onchange = () =>
        {
            lines_element.value = lines.toString();
        };

        columns_element.oninput = () =>
        {
            var v = parseInt(columns_element.value, 10);
            columns = v > 0 ? v : 1;
            page_character_count = lines * columns;
            locate_character();
        };

        columns_element.onchange = () =>
        {
            columns_element.value = columns.toString();
        };

        display_style_element.onchange = () =>
        {
            display_style = parseInt(display_style_element.value, 10);
            update_content();
        };

        font_family_element.oninput = () =>
        {
            table_element.style.fontFamily = font_family_element.value;
        };

        font_size_element.oninput = () =>
        {
            table_element.style.fontSize = font_size_element.value;
        };

        previous_page_element.onclick = () =>
        {
            cancel_locate();
            if (page_number > 0)
            {
                set_page_number(page_number - 1);
            }
        };

        next_page_element.onclick = () =>
        {
            cancel_locate();
            set_page_number(page_number + 1);
        };

        page_number_element.oninput = () =>
        {
            cancel_locate();
            var v = parseInt(page_number_element.value, 10);
            page_number = v > 0 ? v - 1 : 0;
            update_content();
        };

        page_number_element.onchange = () =>
        {
            page_number_element.value = (page_number + 1).toString();
        };

        first_page_element.onclick = () =>
        {
            cancel_locate();
            set_page_number(0);
        };

        locate_element.oninput = () =>
        {
            locate = locate_element.value.length > 0 ? codePointAt(locate_element.value, 0) : -1;
            locate_character();
        };

        lines_element.value = lines.toString();
        columns_element.value = columns.toString();
        display_style_element.value = display_style.toString();
        font_family_element.value = font_family;
        font_size_element.value = font_size;
        page_number_element.value = (page_number + 1).toString();
        locate_element.value = locate == -1 ? "" : fromCodePoint(locate);
        update_content();
    });
})();
