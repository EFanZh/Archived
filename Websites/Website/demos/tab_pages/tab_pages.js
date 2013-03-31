document.addEventListener("DOMContentLoaded", function ()
{
    "use strict";

    function $(id)
    {
        return document.getElementById(id);
    }

    function tabs(tab_page_titles_children, tab_page_contents_children)
    {
        var current_tab_page = 0;

        function set_page_selected(i, value)
        {
            var title_class_list = tab_page_titles_children[i].classList;
            var content_class_list = tab_page_contents_children[i].classList;
            if (value)
            {
                title_class_list.add("tab-page-title-selected");
                content_class_list.add("tab-page-content-selected");
            }
            else
            {
                title_class_list.remove("tab-page-title-selected");
                content_class_list.remove("tab-page-content-selected");
            }
        }

        function get_tab_page_title_onclick_handler(i)
        {
            return function ()
            {
                if (i !== current_tab_page)
                {
                    set_page_selected(current_tab_page, false);
                    set_page_selected(i, true);

                    current_tab_page = i;
                }
            };
        }

        for (var i = 0; i < tab_page_titles_children.length; i++)
        {
            tab_page_titles_children[i].onclick = get_tab_page_title_onclick_handler(i);
        }

        set_page_selected(current_tab_page, true);
    }

    tabs($("tab-page-titles").children, $("tab-page-contents").children);
});
