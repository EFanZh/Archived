document.addEventListener("DOMContentLoaded", function ()
{
    "use strict";

    function $(id)
    {
        return document.getElementById(id);
    }

    function generate()
    {
        var str = $("base-string").value;
        var length = parseInt($("length").value, 10);
        var result = "";

        for (var i = 0; i < length; i++)
        {
            result += str.charAt(Math.floor(Math.random() * str.length));
        }

        $("result-value").innerHTML = result.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    }

    $("length").oninput = generate;
    $("base-string").oninput = generate;
    $("generate").onclick = generate;

    generate();
});
