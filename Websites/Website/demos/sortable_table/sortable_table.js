document.addEventListener("DOMContentLoaded", function ()
{
    "use strict";

    function $(id)
    {
        return document.getElementById(id);
    }

    function sortable_table(colgroup_children, thead_cells, tbody_rows)
    {
        var current_sort_col = 0;
        var current_sort_is_ascend = true;

        function sort(col)
        {
            var arr = [], i;
            for (i = 0; i < tbody_rows.length; i++)
            {
                arr.push(tbody_rows[i]);
            }
            var less, more;
            if (current_sort_is_ascend)
            {
                less = -1;
            }
            else
            {
                less = 1;
            }
            more = -less;
            arr.sort(function (first, second)
            {
                var s1 = first.cells[col].innerText;
                var s2 = second.cells[col].innerText;
                var v1 = parseFloat(s1);
                var v2 = parseFloat(s2);
                var c1, c2;

                if (isNaN(v1) || isNaN(v2))
                {
                    c1 = s1;
                    c2 = s2;
                }
                else
                {
                    c1 = v1;
                    c2 = v2;
                }
                if (c1 < c2 || (c1 === c2 && first.rowIndex < second.rowIndex))
                {
                    return less;
                }
                else
                {
                    return more;
                }
            });
            for (i = 0; i < tbody_rows.length; i++)
            {
                tbody.appendChild(arr[i]);
            }
        }

        function set_sort_state(col, state)
        {
            var colgroup_class_list = colgroup_children[col].classList;
            var head_class_list = thead_cells[col].classList;
            if (state === 0)
            {
                colgroup_class_list.remove("sort-col");
                head_class_list.remove("sort-col-head-ascend");
                head_class_list.remove("sort-col-head-descend");
            }
            else
            {
                colgroup_class_list.add("sort-col");
                if (state < 0)
                {
                    head_class_list.remove("sort-col-head-descend");
                    head_class_list.add("sort-col-head-ascend");
                }
                else
                {
                    head_class_list.remove("sort-col-head-ascend");
                    head_class_list.add("sort-col-head-descend");
                }
            }
        }

        function get_thead_cell_onclick_handler(col)
        {
            return function ()
            {
                if (col === current_sort_col)
                {
                    current_sort_is_ascend = !current_sort_is_ascend;
                    sort(col);
                }
                else
                {
                    set_sort_state(current_sort_col, 0);
                    current_sort_is_ascend = true;
                    sort(col);
                    current_sort_col = col;
                }
                set_sort_state(col, current_sort_is_ascend ? -1 : 1);
            };
        }

        for (var i = 0; i < thead_cells.length; i++)
        {
            thead_cells[i].onclick = get_thead_cell_onclick_handler(i);
        }

        sort(current_sort_col, current_sort_is_ascend);
        set_sort_state(current_sort_col, current_sort_is_ascend ? -1 : 1);
    }

    var table = $("sortable-table");
    var thead_cells = table.tHead.rows[0].cells;
    var tbody = table.createTBody();
    var row_count = 32;
    for (var i = 0; i < row_count; i++)
    {
        var row = tbody.insertRow(-1);
        for (var j = 0; j < thead_cells.length; j++)
        {
            row.insertCell(-1).innerHTML = Math.round(100 * Math.random());
        }
    }

    sortable_table($("sortable-colgroup").children, thead_cells, tbody.rows);
});
