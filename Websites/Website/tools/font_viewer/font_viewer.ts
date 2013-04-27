(document: Document, window: Window) =>
{
    "use strict";

    var element_font_families: HTMLTextAreaElement;
    var element_preview: HTMLElement;
    var element_preview_area: HTMLUListElement;
    var element_preview_area_container: HTMLElement;
    var element_sidebar: HTMLElement;
    var element_style_bold: HTMLInputElement;
    var element_style_bold_italic: HTMLInputElement;
    var element_style_italic: HTMLInputElement;
    var element_style_normal: HTMLInputElement;
    var element_text: HTMLInputElement;
    var element_text_size: HTMLInputElement;

    var textarea_height_difference: number;
    var preview_area_container_height_difference: number;

    var defualt_font_families = ["sans-serif", "serif", "monospace"];
    var default_bold_checked = false;
    var default_bold_italic_checked = false;
    var default_italic_checked = false;
    var default_normal_checked = true;
    var default_text = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    var default_text_size = "12pt";

    function $(id: string)
    {
        return document.getElementById(id);
    }

    function escape(str: string)
    {
        return str.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    }

    function preview()
    {
        var families_trimed = element_font_families.value.trim();
        var fonts = families_trimed.length === 0 ? [] : families_trimed.split(/\s*[\r\n]\s*/);

        var strs: string[] = [];
        var text = element_text.value;
        var text_size = escape(element_text_size.value);

        function build_li(style: string)
        {
            strs.push("<li class=\"");
            strs.push(style);
            strs.push("\">");
            strs.push(text);
            strs.push("</li>");
        }

        for (var i in fonts)
        {
            strs.push("<li><h3>");
            var family = escape(fonts[i]);
            strs.push(family);
            strs.push("</h3><div style=\"font: ");
            strs.push(text_size);
            strs.push(" ");
            strs.push(family.replace(/"/g, "&quot;"));
            strs.push(";\"><ul>")
            if (element_style_normal.checked)
            {
                build_li("");
            }
            if (element_style_italic.checked)
            {
                build_li("italic");
            }
            if (element_style_bold.checked)
            {
                build_li("bold");
            }
            if (element_style_bold_italic.checked)
            {
                build_li("bold italic");
            }
            strs.push("</ul></div></li>");
        }
        element_preview_area.innerHTML = strs.join("");
    }

    document.addEventListener("DOMContentLoaded", () =>
    {
        element_font_families = <HTMLTextAreaElement>$("font-families");
        element_preview = $("preview")
        element_preview_area = <HTMLUListElement>$("preview-area");
        element_preview_area_container = $("preview-area-container");
        element_sidebar = $("sidebar");
        element_style_bold = <HTMLInputElement>$("style-bold");
        element_style_bold_italic = <HTMLInputElement>$("style-bold-italic");
        element_style_italic = <HTMLInputElement>$("style-italic");
        element_style_normal = <HTMLInputElement>$("style-normal");
        element_text = <HTMLInputElement>$("text");
        element_text_size = <HTMLInputElement>$("text-size");

        window.onresize = () =>
        {
            element_font_families.style.height = window.innerHeight - textarea_height_difference + "px";
            element_preview_area_container.style.height = element_sidebar.offsetHeight - preview_area_container_height_difference + "px";
        };

        element_font_families.oninput = preview;
        element_style_bold.onchange = preview;
        element_style_bold_italic.onchange = preview;
        element_style_italic.onchange = preview;
        element_style_normal.onchange = preview;
        element_text.oninput = preview;
        element_text_size.oninput = preview;

        element_font_families.style.height = "10000px";
        textarea_height_difference = document.documentElement.scrollHeight - element_font_families.offsetHeight;
        preview_area_container_height_difference = element_preview_area_container.offsetTop - element_preview.offsetTop;
        window.onresize(null);

        element_font_families.value = defualt_font_families.join("\n");
        element_style_bold.checked = default_bold_checked;
        element_style_bold_italic.checked = default_bold_italic_checked;
        element_style_italic.checked = default_italic_checked;
        element_style_normal.checked = default_normal_checked;
        element_text.value = default_text;
        element_text_size.value = default_text_size;

        preview();
    });
} (document, window);
