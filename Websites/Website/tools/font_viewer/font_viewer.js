window.onload = window_onload;

function $(id)
{
    return document.getElementById(id);
}

function toggle_options_onclick()
{
    if ($("options").style.height == "24px")
    {
        show_options();
    }
    else
    {
        hide_options();
    }
}

function window_onload()
{
    hide_options();
    $("font-style-bold").onchange = preview;
    $("font-style-italic").onchange = preview;
    $("font-style-bold-italic").onchange = preview;
    $("sample-text").oninput = preview;
    $("font-size").oninput = preview;
    $("font-list").oninput = preview;
    $("toggle-options").onclick = toggle_options_onclick;
    preview();
}

function hide_options()
{
    $("options").style.height = "24px";
    $("toggle-options").innerHTML = "[+] Options";
}

function preview()
{
    var fonts = $("font-list").value.split(/[\n|\r\n]{1,}/);
    var sample = $("sample-text").value;
    var size = $("font-size").value;
    var s = "";
    for (var i in fonts)
    {
        if (fonts[i] != "")
        {
            s += "<div class=\"preview-item\">";
            s += "<h3>" + fonts[i] + "</h3>";
            s += "<div style=\"font-family: " + fonts[i] + "; font-size: " + size + ";\">";
            s += "<div>" + sample + "</div>";
            if ($("font-style-bold").checked)
            {
                s+= "<div style=\"font-weight: bold;\">" + sample + "</div>";
            }
            if ($("font-style-italic").checked)
            {
                s+= "<div style=\"font-style: italic;\">" + sample + "</div>";
            }
            if ($("font-style-bold-italic").checked)
            {
                s+= "<div style=\"font-style: italic; font-weight: bold;\">" + sample + "</div>";
            }
            s += "</div>";
            s += "</div>";
        }
    }
    $("preview").innerHTML = s;
}

function show_options()
{
    $("options").style.height = "111px";
    $("toggle-options").innerHTML = "[-] Options";
}
