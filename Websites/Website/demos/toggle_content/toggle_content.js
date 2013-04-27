document.addEventListener("DOMContentLoaded", function ()
{
    "use strict";

    function $(id)
    {
        return document.getElementById(id);
    }

    function toggle(header, content, symbol)
    {
        var is_open = false;
        var height_save = content.offsetHeight + "px";

        toggle_state();

        function toggle_state()
        {
            if (is_open)
            {
                symbol.innerHTML = "⊟";
                content.style.height = height_save;
            }
            else
            {
                symbol.innerHTML = "⊞";
                content.style.height = "0";
            }
        }

        header.onclick = function ()
        {
            is_open = !is_open;
            toggle_state();
        };

        content.style.overflow = "hidden";
        content.style.transition = "all ease 0.25s";
    }

    toggle($("content-header"), $("content"), $("toggle-symbol"));
});
