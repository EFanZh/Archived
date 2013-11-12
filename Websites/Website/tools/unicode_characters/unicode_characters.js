(function () {
    "use strict";

    var lines = 16, columns = 16;
    var page_character_count = lines * columns;
    var display_style = 16;
    var font_family = "Calibri, \"Segoe UI\", \"Segoe Symbol\", \"Microsoft YaHei\", \"Microsoft JhengHei\", Meiryo, \"Malgun Gothic\", Gisha, Leelawadee, sans-serif";
    var font_size = "12pt";
    var page_number = 0;
    var locate = -1;

    var lines_element;
    var columns_element;
    var display_style_element;
    var font_family_element;
    var font_size_element;

    var previous_page_element;
    var next_page_element;
    var page_number_element;
    var first_page_element;
    var locate_element;

    var table_container_element;
    var table_element;

    function $(id) {
        return document.getElementById(id);
    }

    /*
    * ES6 Unicode Shims 0.1
    * (c) 2012 Steven Levithan <http://slevithan.com/>
    * MIT License
    */
    function fromCodePoint() {
        var codes = [];
        for (var _i = 0; _i < (arguments.length - 0); _i++) {
            codes[_i] = arguments[_i + 0];
        }
        var chars = [], point, offset, units, i;
        for (i = 0; i < codes.length; i++) {
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
    function codePointAt(str, pos) {
        pos = isNaN(pos) ? 0 : pos;
        var code = str.charCodeAt(pos), next = str.charCodeAt(pos + 1);

        if (0xD800 <= code && code <= 0xDBFF && 0xDC00 <= next && next <= 0xDFFF) {
            return ((code - 0xD800) * 0x400) + (next - 0xDC00) + 0x10000;
        }
        return code;
    }

    function to_unicode_code(code) {
        var s = code.toString(16);
        if (code < 0x1000) {
            var full_hex = [];
            var zero_count = 4 - s.length;
            for (var i = 0; i < zero_count; i++) {
                full_hex.push("0");
            }
            full_hex.push(s);
            return full_hex.join("");
        }
        return s;
    }

    function update_content() {
        var i, j;

        var strs = ["<table id=\"unicode-characters\"><tbody><tr><td><a href=\"http://www.fileformat.info/info/unicode/char/"];
        var rows = [];
        var base = page_character_count * page_number;

        for (i = 0; i < lines; i++) {
            var cols = [];

            for (j = 0; j < columns; j++) {
                var temp_strs = [];
                var value = base + lines * j + i;
                var unicode = to_unicode_code(value);
                temp_strs.push(unicode);
                temp_strs.push("/index.htm\">");
                if (display_style === 16) {
                    temp_strs.push("U+");
                    temp_strs.push(unicode.toUpperCase());
                } else {
                    temp_strs.push(value.toString(10));
                }
                temp_strs.push("</a></td><td><span>");
                if (value === locate) {
                    temp_strs.push("<mark>");
                    temp_strs.push(fromCodePoint(value));
                    temp_strs.push("</mark>");
                } else {
                    temp_strs.push(fromCodePoint(value));
                }
                cols.push(temp_strs.join(""));
            }
            rows.push(cols.join("</span></td><td><a href=\"http://www.fileformat.info/info/unicode/char/"));
        }
        strs.push(rows.join("</span></td></tr><tr><td><a href=\"http://www.fileformat.info/info/unicode/char/"));
        strs.push("</span></td></tr></tbody></table>");

        table_container_element.innerHTML = strs.join("");
        table_element = table_container_element.firstElementChild;
        table_element.style.fontFamily = font_family_element.value;
        table_element.style.fontSize = font_size_element.value;
    }

    function set_page_number(number) {
        page_number = number;
        page_number_element.value = (page_number + 1).toString();
        update_content();
    }

    function cancel_locate() {
        locate = -1;
        locate_element.value = "";
    }

    function locate_character() {
        if (locate >= 0) {
            set_page_number(Math.floor(locate / page_character_count));
        } else {
            update_content();
        }
    }

    document.addEventListener("DOMContentLoaded", function () {
        lines_element = $("lines");
        columns_element = $("columns");
        display_style_element = $("display-style");
        font_family_element = $("font-family");
        font_size_element = $("font-size");

        previous_page_element = $("previous-page");
        next_page_element = $("next-page");
        page_number_element = $("page-number");
        first_page_element = $("first-page");
        locate_element = $("locate");
        table_container_element = $("unicode-characters-container");

        document.body.onkeydown = function (e) {
            if (e.target === document.body) {
                switch (e.keyCode) {
                    case 37:
                        cancel_locate();
                        if (page_number > 0) {
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

        lines_element.oninput = function () {
            var v = parseInt(lines_element.value, 10);
            lines = v > 0 ? v : 1;
            page_character_count = lines * columns;
            locate_character();
        };

        lines_element.onchange = function () {
            lines_element.value = lines.toString();
        };

        columns_element.oninput = function () {
            var v = parseInt(columns_element.value, 10);
            columns = v > 0 ? v : 1;
            page_character_count = lines * columns;
            locate_character();
        };

        columns_element.onchange = function () {
            columns_element.value = columns.toString();
        };

        display_style_element.onchange = function () {
            display_style = parseInt(display_style_element.value, 10);
            update_content();
        };

        font_family_element.oninput = function () {
            table_element.style.fontFamily = font_family_element.value;
        };

        font_size_element.oninput = function () {
            table_element.style.fontSize = font_size_element.value;
        };

        previous_page_element.onclick = function () {
            cancel_locate();
            if (page_number > 0) {
                set_page_number(page_number - 1);
            }
        };

        next_page_element.onclick = function () {
            cancel_locate();
            set_page_number(page_number + 1);
        };

        page_number_element.oninput = function () {
            cancel_locate();
            var v = parseInt(page_number_element.value, 10);
            page_number = v > 0 ? v - 1 : 0;
            update_content();
        };

        page_number_element.onchange = function () {
            page_number_element.value = (page_number + 1).toString();
        };

        first_page_element.onclick = function () {
            cancel_locate();
            set_page_number(0);
        };

        locate_element.oninput = function () {
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
