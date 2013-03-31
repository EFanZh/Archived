document.addEventListener("DOMContentLoaded", function ()
{
    "use strict";

    var lines = 16, columns = 16;
    var page_character_count = lines * columns;
    var display_style = 10;
    var font_family = "Calibri, \"Microsoft YaHei\", \"Microsoft JhengHei\", Meiryo, \"Malgun Gothic\", Gisha, Leelawadee, sans-serif";
    var font_size = "12pt";
    var page_number = 0;
    var locate = -1;

    function $(id)
    {
        return document.getElementById(id);
    }

    var table = $("unicode-characters");

    function update_content()
    {
        var i, j;

        while (table.rows.length > 0)
        {
            table.deleteRow(-1);
        }

        var base = page_character_count * page_number;
        for (i = 0; i < lines ; i++)
        {
            var row = table.insertRow(-1);
            for (j = 0; j < columns ; j++)
            {
                var value = base + lines * j + i;
                row.insertCell(-1).innerHTML = (display_style === 16 ? "0x" : "") + value.toString(display_style);
                var span = document.createElement("span");
                span.innerHTML = String.fromCharCode(value);
                if (value === locate)
                {
                    span.classList.add("selected-character");
                }
                row.insertCell(-1).appendChild(span);
            }
        }
    }

    function set_page_number(number)
    {
        page_number = number;
        $("page-number").value = page_number + 1;
        update_content();
    }

    function cancel_locate()
    {
        locate = -1;
        $("locate").value = "";
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

    document.body.onkeydown = function (e)
    {
        if (e.target === this)
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

    $("lines").oninput = function ()
    {
        var v = parseInt(this.value, 10);
        lines = v > 0 ? v : 1;
        page_character_count = lines * columns;
        locate_character();
    };
    $("lines").onchange = function ()
    {
        this.value = lines;
    };
    $("columns").oninput = function ()
    {
        var v = parseInt(this.value, 10);
        columns = v > 0 ? v : 1;
        page_character_count = lines * columns;
        locate_character();
    };
    $("columns").onchange = function ()
    {
        this.value = columns;
    };
    $("display-style").onchange = function ()
    {
        display_style = parseInt(this.value, 10);
        update_content();
    };
    $("font-family").oninput = function ()
    {
        table.style.fontFamily = this.value;
    };
    $("font-size").oninput = function ()
    {
        table.style.fontSize = this.value;
    };
    $("previous-page").onclick = function ()
    {
        cancel_locate();
        if (page_number > 0)
        {
            set_page_number(page_number - 1);
        }
    };
    $("next-page").onclick = function ()
    {
        cancel_locate();
        set_page_number(page_number + 1);
    };
    $("page-number").oninput = function ()
    {
        cancel_locate();
        var v = parseInt(this.value, 10);
        page_number = v > 0 ? v - 1 : 0;
        update_content();
    };
    $("page-number").onchange = function ()
    {
        this.value = page_number + 1;
    };
    $("first-page").onclick = function ()
    {
        cancel_locate();
        set_page_number(0);
    };
    $("locate").oninput = function ()
    {
        locate = this.value.length > 0 ? this.value.charCodeAt(0) : -1;
        locate_character();
    };

    $("lines").value = lines;
    $("columns").value = columns;
    $("display-style").value = display_style;
    $("font-family").value = font_family;
    $("font-size").value = font_size;
    $("page-number").value = page_number + 1;
    $("locate").value = locate == -1 ? "" : String.fromCharCode(locate);
    table.style.fontFamily = $("font-family").value;
    table.style.fontSize = $("font-size").value;

    update_content();
});
